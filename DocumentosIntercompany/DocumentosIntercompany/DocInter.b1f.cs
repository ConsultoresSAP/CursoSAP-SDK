
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
        SAPbouiCOM.Button Btn_Parametros;
        SAPbouiCOM.Matrix Matrix;
        SAPbouiCOM.Form oForm;
        bool Modal = false;
        ParametrosConexion ParametrosConexion;
        List<SAPbouiCOM.DBDataSource> DB;
        string idForm;

        public DocInter_b1f()
        {
            this.ParametrosConexion = new ParametrosConexion();
            this.ParametrosConexion.oCom = new SAPbobsCOM.Company();
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.OnCustomInitialize();
            this.Matrix.ChooseFromListAfter += new SAPbouiCOM._IMatrixEvents_ChooseFromListAfterEventHandler(this.Matrix_ChooseFromListAfter);
            Application.SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(this.SBO_Application_MenuEvent);
            this.Btn_Parametros.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Btn_Parametros_PressedAfter);
            this.Btn_Exportar.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Btn_Exportar_PressedAfter);

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
                this.Btn_Parametros = ((SAPbouiCOM.Button)(this.GetItem("B_2").Specific));
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

                this.Btn_Parametros.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Visible, 2, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                this.Btn_Parametros.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Visible, 4, SAPbouiCOM.BoModeVisualBehavior.mvb_False);

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
            this.CloseBefore += new CloseBeforeHandler(this.Form_CloseBefore);

        }


        private void C_EventoModal(object sender, EventArgs e)
        {
            this.Modal = false;
        }

        private string CrearFactura(int DocEntry)
        {
            SAPbobsCOM.Documents FacturaOrigen = null;
            SAPbobsCOM.Documents FacturaDestino = null;
            try
            {
                FacturaOrigen = (SAPbobsCOM.Documents)Program.oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                if (FacturaOrigen.GetByKey(DocEntry))
                {
                    FacturaDestino = (SAPbobsCOM.Documents)this.ParametrosConexion.oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

                    FacturaDestino.CardCode = FacturaOrigen.CardCode;
                    FacturaDestino.DocDate = FacturaOrigen.DocDate;
                    FacturaDestino.DocDueDate = FacturaOrigen.DocDueDate;
                    FacturaDestino.Comments = FacturaOrigen.Comments;

                    if (FacturaOrigen.Lines.Count > 0)
                    {
                        for(int i = 0; i < FacturaOrigen.Lines.Count; i++)
                        {
                            if (i != 0)
                            {
                                FacturaDestino.Lines.Add();
                            }
                            FacturaDestino.Lines.ItemCode = FacturaOrigen.Lines.ItemCode;
                            FacturaDestino.Lines.Quantity = FacturaOrigen.Lines.Quantity;
                            FacturaDestino.Lines.TaxCode = FacturaOrigen.Lines.TaxCode;
                        }
                    }

                    if (FacturaDestino.Add() != 0)
                    {
                        return this.ParametrosConexion.oCom.GetLastErrorDescription();
                    }else
                    {
                        return "";
                    }

                }
                else
                {
                    return "Documento no existe";
                }
            }catch(Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (FacturaDestino != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(FacturaDestino);
                    FacturaDestino = null;
                }
                if (FacturaOrigen != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(FacturaOrigen);
                    FacturaOrigen = null;
                }
            }
        }

        private string CrearPedido(int DocEntry)
        {
            SAPbobsCOM.Documents PedidoOrigen = null;
            SAPbobsCOM.Documents PedidoDestino = null;
            try
            {
                PedidoOrigen = (SAPbobsCOM.Documents)Program.oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
                if (PedidoOrigen.GetByKey(DocEntry))
                {
                    PedidoDestino = (SAPbobsCOM.Documents)this.ParametrosConexion.oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);

                    PedidoDestino.CardCode = PedidoOrigen.CardCode;
                    PedidoDestino.DocDate = PedidoOrigen.DocDate;
                    PedidoDestino.DocDueDate = PedidoOrigen.DocDueDate;
                    PedidoDestino.Comments = PedidoOrigen.Comments;

                    if (PedidoOrigen.Lines.Count > 0)
                    {
                        for (int i = 0; i < PedidoOrigen.Lines.Count; i++)
                        {
                            if (i != 0)
                            {
                                PedidoDestino.Lines.Add();
                            }
                            PedidoDestino.Lines.ItemCode = PedidoOrigen.Lines.ItemCode;
                            PedidoDestino.Lines.Quantity = PedidoOrigen.Lines.Quantity;
                            PedidoDestino.Lines.TaxCode = PedidoOrigen.Lines.TaxCode;
                        }
                    }

                    if (PedidoDestino.Add() != 0)
                    {
                        return this.ParametrosConexion.oCom.GetLastErrorDescription();
                    }
                    else
                    {
                        return "";
                    }

                }
                else
                {
                    return "Documento no existe";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (PedidoDestino != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(PedidoDestino);
                    PedidoDestino = null;
                }
                if (PedidoOrigen != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(PedidoOrigen);
                    PedidoOrigen = null;
                }
            }
        }

        private string CrearPago(int DocEntry)
        {
            SAPbobsCOM.Payments PagoOrigen = null;
            SAPbobsCOM.Payments PagoDestino = null;
            try
            {
                PagoOrigen = (SAPbobsCOM.Payments)Program.oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                if (PagoOrigen.GetByKey(DocEntry))
                {
                    PagoDestino = (SAPbobsCOM.Payments)this.ParametrosConexion.oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);

                    PagoDestino.CardCode = PagoOrigen.CardCode;
                    PagoDestino.DocDate = PagoOrigen.DocDate;
                    PagoDestino.DueDate = PagoOrigen.DueDate;
                    PagoDestino.Remarks = PagoOrigen.Remarks;


                    if (PagoOrigen.CashSum>0)
                    {
                        PagoDestino.CashSum = PagoOrigen.CashSum;
                    }

                    if (PagoOrigen.TransferSum > 0)
                    {
                        PagoDestino.TransferAccount = PagoOrigen.TransferAccount;
                        PagoDestino.TransferDate = PagoOrigen.TransferDate;
                        PagoDestino.TransferReference = PagoOrigen.TransferReference;
                        PagoDestino.TransferSum = PagoOrigen.TransferSum;
                    }

                    if (PagoOrigen.Checks.Count > 0)
                    {
                        for(int i = 0; i < PagoOrigen.Checks.Count; i++)
                        {
                            PagoOrigen.Checks.SetCurrentLine(i);
                            if (PagoOrigen.Checks.CheckSum > 0)
                            {
                                PagoDestino.Checks.CheckSum = PagoOrigen.Checks.CheckSum;
                                PagoDestino.Checks.CountryCode = PagoOrigen.Checks.CountryCode;
                                PagoDestino.Checks.BankCode = PagoOrigen.Checks.BankCode;
                                PagoDestino.Checks.AccounttNum = PagoOrigen.Checks.AccounttNum;
                                PagoDestino.Checks.CheckNumber = PagoOrigen.Checks.CheckNumber;
                                PagoDestino.Checks.DueDate = PagoOrigen.Checks.DueDate;
                                PagoDestino.Checks.Trnsfrable = PagoOrigen.Checks.Trnsfrable;
                                PagoDestino.Checks.Add();
                            }
                        }
                    }

                    if (PagoOrigen.CreditCards.Count > 0)
                    {
                        for(int i = 0; i > PagoOrigen.CreditCards.Count; i++)
                        {
                            PagoOrigen.CreditCards.SetCurrentLine(i);
                            if (PagoOrigen.CreditCards.CreditSum > 0)
                            {
                                PagoDestino.CreditCards.CreditCard = PagoOrigen.CreditCards.CreditCard;
                                PagoDestino.CreditCards.CreditCardNumber = PagoOrigen.CreditCards.CreditCardNumber;
                                PagoDestino.CreditCards.CardValidUntil = PagoOrigen.CreditCards.CardValidUntil;
                                PagoDestino.CreditCards.VoucherNum = PagoOrigen.CreditCards.VoucherNum;
                                PagoDestino.CreditCards.CreditSum = PagoOrigen.CreditCards.CreditSum;
                                PagoDestino.CreditCards.Add();
                            }
                        }
                        
                        
                    }

                    if (PagoOrigen.Invoices.Count > 0)
                    {
                        for(int i = 0; i < PagoOrigen.Invoices.Count; i++)
                        {
                            PagoOrigen.Invoices.SetCurrentLine(i);
                            PagoDestino.Invoices.DocEntry = PagoOrigen.Invoices.DocEntry;
                            PagoDestino.Invoices.SumApplied = PagoOrigen.Invoices.SumApplied;
                            PagoDestino.Invoices.Add();
                        }
                    }

                    if (PagoDestino.Add() != 0)
                    {
                        return this.ParametrosConexion.oCom.GetLastErrorDescription();
                    }else
                    {
                        return "";
                    }



                }else
                {
                    return "Documento no existe";
                }

            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (PagoOrigen != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(PagoOrigen);
                    PagoOrigen = null;
                }
                if (PagoDestino != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(PagoDestino);
                    PagoDestino = null;
                }
            }
        }


        private void CerrarUDO()
        {
            try
            {
                SAPbobsCOM.CompanyService oCompanyService;
                SAPbobsCOM.GeneralService oGeneralServices;
                SAPbobsCOM.GeneralDataParams oGeneralParams;

                oCompanyService = Program.oCom.GetCompanyService();
                oGeneralServices = oCompanyService.GetGeneralService("CDOCINTE");

                oGeneralParams = (SAPbobsCOM.GeneralDataParams)oGeneralServices.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

                oGeneralParams.SetProperty("DocEntry", Int32.Parse(this.DB[0].GetValue("DocEntry",0)));

                oGeneralServices.Close(oGeneralParams);

            }
            catch (Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + e.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
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
                        if (this.oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                        {
                            //this.DB[0].SetValue("U_Comentarios",0,"Hola");
                            this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim() + @"+";
                            this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Substring(0, this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Length - 1);
                        }
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
                            if (this.oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                            {
                                //this.DB[0].SetValue("U_Comentarios",0,"Hola");
                                this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim() + @"+";
                                this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Substring(0, this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Length - 1);
                            }
                        }
                    }
                    if (pVal.MenuUID == "1283" &&  pVal.BeforeAction)
                    {
                        int respuesta = Application.SBO_Application.MessageBox("Esta seguro de borrar este registro?", 1, "ok", "Cancelar");
                        if (respuesta != 1)
                        {
                            BubbleEvent = false;
                        }
                    }
                    if (pVal.MenuUID == "1281" && pVal.BeforeAction)
                    {
                        this.oForm.EnableMenu("1292", false);//Add
                        this.oForm.EnableMenu("1293", false);//delete
                    }
                    if (pVal.MenuUID == "1282" && pVal.BeforeAction)
                    {
                        this.oForm.EnableMenu("1292", true);//Add
                        this.oForm.EnableMenu("1293", true);//delete
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

                        if (this.oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                        {
                            //this.DB[0].SetValue("U_Comentarios",0,"Hola");
                            this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim() + @"+";
                            this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Substring(0, this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Length - 1);
                        }

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

                        if (this.oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                        {
                            //this.DB[0].SetValue("U_Comentarios",0,"Hola");
                            this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim() + @"+";
                            this.Txt_Comentario.Value = this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Substring(0, this.DB[0].GetValue("U_Comentarios", 0).ToString().Trim().Length - 1);
                        }
                    }
                }
            }catch(Exception ex)
            {
                Application.SBO_Application.MessageBox("Error: " + ex);
            }

        }

        #endregion

        #region FORM

        private void Form_CloseBefore(SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (this.Modal)
            {
                BubbleEvent = false;
            }
        }


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
                        this.Btn_Parametros.Item.Visible = true;
                    }
                    else
                    {
                        this.oForm.EnableMenu("1292", false);//Add
                        this.oForm.EnableMenu("1293", false);//delete
                        this.Txt_Nombre.Item.Enabled = false;
                        this.Txt_Fecha.Item.Enabled = false;
                        this.Matrix.Item.Enabled = false;
                        this.Btn_Exportar.Item.Visible = false;
                        this.Btn_Parametros.Item.Visible = false;
                    }
                }
            }catch(Exception ex)
            {
                Application.SBO_Application.MessageBox("Error: "+ex.Message);
            }
        }



        #endregion

        #region BOTONES

        private void Btn_Parametros_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if (!this.Modal)
                {
                    
                    Parametros_b1f FormParametros = new Parametros_b1f(ref this.ParametrosConexion,this.idForm,this.oForm.Top,this.oForm.Left);
                    this.Modal = true;
                    FormParametros.EventoModal += C_EventoModal;
                    FormParametros.Show();
                }
                
            }catch(Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        private void Btn_Exportar_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.ParametrosConexion.SociedadName))
                {
                    int Errores = 0;
                    string ErrorDesc = "";
                    if (this.DB[1].Size > 0)
                    {
                        for(int i = 0; i < this.DB[1].Size; i++)
                        {
                            if (!String.IsNullOrEmpty(this.DB[1].GetValue("U_Type", i)))
                            {
                                if (!this.ParametrosConexion.oCom.InTransaction)
                                {
                                    this.ParametrosConexion.oCom.StartTransaction();
                                }
                                
                                if(this.DB[1].GetValue("U_Type", i) == "13")//Facturas
                                {
                                    int DocEntry = Int32.Parse(this.DB[1].GetValue("U_DocEntryFact", i));
                                    string Error = CrearFactura(DocEntry);
                                    ErrorDesc +=  "/ Error Fact("+DocEntry+"): "+Error;
                                    if (Error != "")
                                    {
                                        Errores++;
                                    }
                                }
                                else if (this.DB[1].GetValue("U_Type", i) == "17")//Pedidos
                                {
                                    int DocEntry = Int32.Parse(this.DB[1].GetValue("U_DocEntryPedido", i));
                                    string Error = CrearPedido(DocEntry);
                                    ErrorDesc += "/ Error Pedido(" + DocEntry + "): " + Error;
                                    if (Error != "")
                                    {
                                        Errores++;
                                    }
                                }
                                else//Pago
                                {
                                    int DocEntry = Int32.Parse(this.DB[1].GetValue("U_DocEntryPago", i));
                                    string Error = CrearPago(DocEntry);
                                    ErrorDesc += "/ Error Pago(" + DocEntry + "): " + Error;
                                    if (Error != "")
                                    {
                                        Errores++;
                                    }

                                }
                            }
                        }

                        if (Errores > 0)
                        {
                            if (this.ParametrosConexion.oCom.InTransaction)
                            {
                                this.ParametrosConexion.oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            }
                            Application.SBO_Application.MessageBox("Hay algunos errores: "+ErrorDesc);
                        }else
                        {
                            if (this.ParametrosConexion.oCom.InTransaction)
                            {
                                this.ParametrosConexion.oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                            }
                            CerrarUDO();
                            Application.SBO_Application.MessageBox("Documentos Exportados con exito.");
                            
                        }
                    }
                }
                else
                {
                    Application.SBO_Application.MessageBox("Debe estar conectado a una sociedad");
                }
            }
            catch (Exception ex)
            {
                if (this.ParametrosConexion.oCom.InTransaction)
                {
                    this.ParametrosConexion.oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
                Application.SBO_Application.SetStatusBarMessage("Error: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }


        #endregion

        #endregion


    }
}



