
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace DocumentosIntercompany
{

    [FormAttribute("DocumentosIntercompany.DocInter_b1f", "DocInter.b1f")]
    class DocInter_b1f : UserFormBase
    {
        SAPbouiCOM.EditText Txt_DocEntry;
        SAPbouiCOM.EditText Txt_DocNum;
        SAPbouiCOM.EditText Txt_Nombre;
        SAPbouiCOM.EditText Txt_Fecha;
        SAPbouiCOM.EditText Txt_Comentario;
        SAPbouiCOM.EditText Txt_Status;
        SAPbouiCOM.Button Btn_Exportar;
        SAPbouiCOM.Matrix Matrix;
        SAPbouiCOM.Form oForm;
        List<SAPbouiCOM.DBDataSource> DB;
        string idForm;

        public DocInter_b1f()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.OnCustomInitialize();
            this.Matrix.ChooseFromListAfter += new SAPbouiCOM._IMatrixEvents_ChooseFromListAfterEventHandler(this.Matrix_ChooseFromListAfter);
            Application.SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(this.SBO_Application_MenuEvent);
        }

        private void OnCustomInitialize()
        {
            try
            {
                this.oForm = (SAPbouiCOM.Form)this.UIAPIRawForm;
                this.idForm = this.oForm.UniqueID;

                this.Txt_DocEntry = (SAPbouiCOM.EditText)this.oForm.Items.Item("T_1").Specific;
                this.Txt_DocNum = (SAPbouiCOM.EditText)this.oForm.Items.Item("T_2").Specific;
                this.Txt_Nombre = (SAPbouiCOM.EditText)this.oForm.Items.Item("T_3").Specific;
                this.Txt_Fecha = (SAPbouiCOM.EditText)this.oForm.Items.Item("T_4").Specific;
                this.Txt_Comentario = (SAPbouiCOM.EditText)this.oForm.Items.Item("T_5").Specific;
                this.Txt_Status = ((SAPbouiCOM.EditText)(this.GetItem("T_6").Specific));
                this.Btn_Exportar = ((SAPbouiCOM.Button)(this.GetItem("B_1").Specific));
                this.Matrix = (SAPbouiCOM.Matrix)this.oForm.Items.Item("M_1").Specific;

                this.DB = new List<SAPbouiCOM.DBDataSource>();

                this.oForm.EnableMenu("1292", true);//Add
                this.oForm.EnableMenu("1293", true);//delete

                //(2:add; 1:update / ok; -1:all; 4:find; 8:view )
                this.Txt_DocEntry.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 2, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                this.Txt_DocEntry.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 4, SAPbouiCOM.BoModeVisualBehavior.mvb_Default);
                this.Txt_DocEntry.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 1, SAPbouiCOM.BoModeVisualBehavior.mvb_False);

                this.Txt_DocNum.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 2, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                this.Txt_DocNum.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 4, SAPbouiCOM.BoModeVisualBehavior.mvb_Default);
                this.Txt_DocNum.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 1, SAPbouiCOM.BoModeVisualBehavior.mvb_False);

                this.Txt_Status.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 2, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                this.Txt_Status.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 4, SAPbouiCOM.BoModeVisualBehavior.mvb_Default);
                this.Txt_Status.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, 1, SAPbouiCOM.BoModeVisualBehavior.mvb_False);

                this.Btn_Exportar.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Visible, 2, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                this.Btn_Exportar.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Visible, 4, SAPbouiCOM.BoModeVisualBehavior.mvb_False);

                for (int i = 0; i < this.oForm.DataSources.DBDataSources.Count; i++)
                {
                    this.DB.Add(this.oForm.DataSources.DBDataSources.Item(i));
                }

               

            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.CloseAfter += new CloseAfterHandler(this.Form_CloseAfter);
            this.DataLoadAfter += new DataLoadAfterHandler(this.Form_DataLoadAfter);

        }


        #region EVENTOS

        #region MENU

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (Application.SBO_Application.Forms.ActiveForm.UniqueID == this.idForm)
                {
                    if (pVal.MenuUID == "1292" && !pVal.BeforeAction)
                    {
                        this.DB[1].Clear();
                        this.Matrix.AddRow();
                        this.Matrix.FlushToDataSource();
                        this.Matrix.LoadFromDataSource();
                        this.Matrix.SelectRow(this.Matrix.RowCount, true, false);
                        this.oForm.Refresh();
                    }
                    if (pVal.MenuUID == "1293" && pVal.BeforeAction)
                    {
                        BubbleEvent = false;
                        int Row = this.Matrix.GetNextSelectedRow(0, SAPbouiCOM.BoOrderType.ot_SelectionOrder);
                        if (Row != -1)
                        {
                            this.Matrix.FlushToDataSource();
                            this.DB[1].RemoveRecord(Row - 1);
                            this.Matrix.LoadFromDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

        #endregion

        #region CHOOSEFROMLIST

        private void Matrix_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if (pVal.FormUID == this.idForm)
                {
                    if (pVal.ColUID == "C_2")
                    {
                        SAPbouiCOM.SBOChooseFromListEventArg CFLEvento = null;
                        CFLEvento = (SAPbouiCOM.SBOChooseFromListEventArg)pVal;
                        string IdCFL = null;
                        IdCFL = CFLEvento.ChooseFromListUID;
                        SAPbouiCOM.ChooseFromList CFL = null;
                        CFL = oForm.ChooseFromLists.Item(IdCFL);

                        SAPbouiCOM.DataTable Data = null;
                        Data = CFLEvento.SelectedObjects;

                        if (Data!=null)
                        {
                            string DocEntry = Data.GetValue("DocEntry", 0).ToString();
                            string DocNum = Data.GetValue("DocNum", 0).ToString();
                            string Typo = Data.GetValue("ObjType", 0).ToString();
                            this.DB[1].SetValue("U_DocNum", pVal.Row - 1, DocNum);
                            this.DB[1].SetValue("U_Type", pVal.Row - 1, Typo);
                            this.DB[1].SetValue("U_DocEntryFact", pVal.Row - 1, DocEntry);
                            this.DB[1].SetValue("U_DocEntryPedido", pVal.Row - 1, "");
                            this.DB[1].SetValue("U_DocEntryPago", pVal.Row - 1, "");

                            if (this.oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                            {
                                //this.DB[0].SetValue("U_Comentarios",0,"Hola");
                                this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim() + @"+";
                                this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Substring(0, this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Length-1);
                            }

                        }
                        this.Matrix.LoadFromDataSource();
                        
                    }
                    if (pVal.ColUID == "C_3")
                    {
                        SAPbouiCOM.SBOChooseFromListEventArg CFLEvento = null;
                        CFLEvento = (SAPbouiCOM.SBOChooseFromListEventArg)pVal;
                        string IdCFL = null;
                        IdCFL = CFLEvento.ChooseFromListUID;
                        SAPbouiCOM.ChooseFromList CFL = null;
                        CFL = oForm.ChooseFromLists.Item(IdCFL);

                        SAPbouiCOM.DataTable Data = null;
                        Data = CFLEvento.SelectedObjects;

                        if (Data != null)
                        {
                            string DocEntry = Data.GetValue("DocEntry", 0).ToString();
                            string DocNum = Data.GetValue("DocNum", 0).ToString();
                            string Typo = Data.GetValue("ObjType", 0).ToString();
                            this.DB[1].SetValue("U_DocNum", pVal.Row - 1, DocNum);
                            this.DB[1].SetValue("U_Type", pVal.Row - 1, Typo);
                            this.DB[1].SetValue("U_DocEntryPedido", pVal.Row - 1, DocEntry);
                            this.DB[1].SetValue("U_DocEntryFact", pVal.Row - 1, "");
                            this.DB[1].SetValue("U_DocEntryPago", pVal.Row - 1, "");
                        }
                        this.Matrix.LoadFromDataSource();

                    }
                    if (pVal.ColUID == "C_4")
                    {
                        SAPbouiCOM.SBOChooseFromListEventArg CFLEvento = null;
                        CFLEvento = (SAPbouiCOM.SBOChooseFromListEventArg)pVal;
                        string IdCFL = null;
                        IdCFL = CFLEvento.ChooseFromListUID;
                        SAPbouiCOM.ChooseFromList CFL = null;
                        CFL = oForm.ChooseFromLists.Item(IdCFL);

                        SAPbouiCOM.DataTable Data = null;
                        Data = CFLEvento.SelectedObjects;

                        if (Data != null)
                        {
                            string DocEntry = Data.GetValue("DocEntry", 0).ToString();
                            string DocNum = Data.GetValue("DocNum", 0).ToString();
                            string Typo = Data.GetValue("ObjType", 0).ToString();
                            this.DB[1].SetValue("U_DocNum", pVal.Row - 1, DocNum);
                            this.DB[1].SetValue("U_Type", pVal.Row - 1, Typo);
                            this.DB[1].SetValue("U_DocEntryPago", pVal.Row - 1, DocEntry);
                            this.DB[1].SetValue("U_DocEntryPedido", pVal.Row - 1, "");
                            this.DB[1].SetValue("U_DocEntryFact", pVal.Row - 1, "");
                        }
                        this.Matrix.LoadFromDataSource();

                    }
                }
            }catch(Exception ex)
            {
                Application.SBO_Application.MessageBox("Error: " + ex);
            }

        }

        #endregion

        #region FORM

        private void Form_CloseAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                Application.SBO_Application.MenuEvent -= new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SBO_Application_MenuEvent);
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox("Error: " + ex.Message);
            }

        }

        private void Form_DataLoadAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            try
            {
                if (pVal.FormUID == this.idForm)
                {
                    string Status = this.DB[0].GetValue("Status", 0);
                    if (Status == "O")
                    {
                        this.oForm.EnableMenu("1292", true);//Add
                        this.oForm.EnableMenu("1293", true);//delete
                        this.Txt_Nombre.Item.Enabled = true;
                        this.Txt_Fecha.Item.Enabled = true;
                        this.Matrix.Item.Enabled = true;
                        this.Btn_Exportar.Item.Visible = true;
                    }else
                    {
                        this.oForm.EnableMenu("1292", false);//Add
                        this.oForm.EnableMenu("1293", false);//delete
                        this.Txt_Nombre.Item.Enabled = false;
                        this.Txt_Fecha.Item.Enabled = false;
                        this.Matrix.Item.Enabled = false;
                        this.Btn_Exportar.Item.Visible = false;
                    }
                }
            }catch(Exception ex)
            {
                Application.SBO_Application.MessageBox("Error: "+ex.Message);
            }
        }

        #endregion

        #endregion


    }
}



