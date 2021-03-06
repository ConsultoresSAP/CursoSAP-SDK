
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace DocumentosIntercompany
{

    [FormAttribute("139", "Orden de venta.b1f")]
    class Orden_de_venta : SystemFormBase
    {

        private SAPbouiCOM.Form oForm;
        private SAPbouiCOM.DBDataSource ORDR;
        private SAPbouiCOM.EditText Txt_comentarios;

        public Orden_de_venta()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataAddAfter += new DataAddAfterHandler(this.Form_DataAddAfter);
        }

        private void OnCustomInitialize()
        {
            try
            {
                this.oForm = Application.SBO_Application.Forms.Item(this.UIAPIRawForm.UniqueID);
                agregarCajaDeTexto();
                FiltroCFL();
                this.ORDR = this.oForm.DataSources.DBDataSources.Item("ORDR");
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox("Error: " + ex.Message);
            }

        }

        private void agregarCajaDeTexto()
        {
            try
            {
                SAPbouiCOM.Item Item_Comentario;
                Item_Comentario = this.oForm.Items.Add("Item_Comen", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                Item_Comentario.Visible = false;
                this.Txt_comentarios = (SAPbouiCOM.EditText)Item_Comentario.Specific;
                this.Txt_comentarios.DataBind.SetBound(true, "ORDR", "U_FacNit");
            }
            catch (Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Error 2: " + e.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        private void FiltroCFL()
        {
            try
            {
                SAPbouiCOM.ChooseFromListCollection oCfls;
                SAPbouiCOM.ChooseFromList oCFL;
                SAPbouiCOM.Conditions oCons;
                SAPbouiCOM.Condition oCon;

                oCfls = this.oForm.ChooseFromLists;
                oCFL = oCfls.Item("DOCINTER");

                oCons = oCFL.GetConditions();

                oCon = oCons.Add();

                oCon.Alias = "Status";
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                oCon.CondVal = "O";

                oCFL.SetConditions(oCons);


            }
            catch (Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + e.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        private void AgregarLineaUDO(string DocEntry, string ObjectType, string DocNum, string DocEntryUDO)
        {
            try
            {
                SAPbobsCOM.CompanyService oCompanyService;
                SAPbobsCOM.GeneralService oGeneralServices;
                SAPbobsCOM.GeneralData oGeneralData;
                SAPbobsCOM.GeneralDataParams oGeneralParams;

                SAPbobsCOM.GeneralDataCollection Lineas;
                SAPbobsCOM.GeneralData Linea;

                oCompanyService = Program.oCom.GetCompanyService();
                oGeneralServices = oCompanyService.GetGeneralService("CDOCINTE");

                oGeneralParams = (SAPbobsCOM.GeneralDataParams)oGeneralServices.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

                oGeneralParams.SetProperty("DocEntry", Int32.Parse(DocEntryUDO));

                oGeneralData = oGeneralServices.GetByParams(oGeneralParams);

                Lineas = oGeneralData.Child("DOCINTER2");

                Linea = Lineas.Add();

                Linea.SetProperty("U_Type", ObjectType);
                Linea.SetProperty("U_DocEntryFact", "");
                Linea.SetProperty("U_DocEntryPedido", DocEntry);
                Linea.SetProperty("U_DocEntryPago", "");
                Linea.SetProperty("U_DocNum", DocNum);

                oGeneralServices.Update(oGeneralData);

            }
            catch (Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + e.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }



        #region EVENTOS


        #region FORM

        private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            try
            {
                if (pVal.ActionSuccess && pVal.Type == "17" && !String.IsNullOrEmpty(this.ORDR.GetValue("U_FacNit", 0)))
                {
                    string DocEntry = this.ORDR.GetValue("DocEntry", 0);
                    string ObjectType = "17";
                    string DocNum = this.ORDR.GetValue("DocNum", 0);
                    string DocEntryUDO = this.ORDR.GetValue("U_FacNit", 0);

                    AgregarLineaUDO(DocEntry, ObjectType, DocNum, DocEntryUDO);
                }


            }
            catch (Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + e.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }

        }

        #endregion



        #endregion




    }
}
