
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace DocumentosIntercompany
{

    [FormAttribute("133", "Factura de deudores.b1f")]
    class Factura_de_deudores : SystemFormBase
    {
        private SAPbouiCOM.Button Btn_EditarComentario;
        private SAPbouiCOM.EditText Txt_Comments;
        private SAPbouiCOM.EditText Txt_Comentario;
        private SAPbouiCOM.Form oForm;
        private SAPbouiCOM.DBDataSource OINV;

        public Factura_de_deudores()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.OnCustomInitialize();
            this.Btn_EditarComentario.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Btn_EditarComentario_PressedAfter);
            

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
                this.Txt_Comments = ((SAPbouiCOM.EditText)(this.GetItem("16").Specific));
                this.Btn_EditarComentario = ((SAPbouiCOM.Button)(this.GetItem("B_1").Specific));
                this.oForm = Application.SBO_Application.Forms.Item(this.UIAPIRawForm.UniqueID);
                agregarCajaDeTexto();
                FiltroCFL();
                this.OINV = this.oForm.DataSources.DBDataSources.Item("OINV");
            }
            catch(Exception ex)
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
                this.Txt_Comentario = (SAPbouiCOM.EditText)Item_Comentario.Specific;
                this.Txt_Comentario.DataBind.SetBound(true, "OINV", "U_FacNit");
            }catch(Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: "+e.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
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


            }catch(Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + e.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }


        private void AgregarLineaUDO(string DocEntry,string ObjectType,string DocNum, string DocEntryUDO)
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
                Linea.SetProperty("U_DocEntryFact", DocEntry);
                Linea.SetProperty("U_DocEntryPedido", "");
                Linea.SetProperty("U_DocEntryPago", "");
                Linea.SetProperty("U_DocNum", DocNum);

                oGeneralServices.Update(oGeneralData);

            }catch(Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + e.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }



        #region EVENTOS

        #region BUTTON

        private void Btn_EditarComentario_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                this.oForm.Freeze(true);
                this.Txt_Comments.Value = "Nuevo Comentario";
                this.Txt_Comentario.Value = "";
                this.Txt_Comentario.Value = "Comentario en UDF";
                //VALOR 2
                //VALOR 3
                this.oForm.Freeze(false);
                //this.OINV.SetValue("U_Comentario", 0, "Nuevo Comentario");
            }
            catch (Exception ex)
            {
                if (this.oForm != null)
                {
                    this.oForm.Freeze(false);
                }
                Application.SBO_Application.SetStatusBarMessage("Error: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        #endregion

        #region FORM

        private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            try
            {
                if(pVal.ActionSuccess && pVal.Type == "13" && !String.IsNullOrEmpty(this.OINV.GetValue("U_FacNit", 0)))
                {
                    string DocEntry = this.OINV.GetValue("DocEntry", 0);
                    string ObjectType = "13";
                    string DocNum = this.OINV.GetValue("DocNum", 0);
                    string DocEntryUDO = this.OINV.GetValue("U_FacNit", 0);

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
