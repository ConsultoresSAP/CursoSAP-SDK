
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace DocumentosIntercompany
{

    [FormAttribute("DocumentosIntercompany.Parametros_b1f", "Parametros.b1f")]
    class Parametros_b1f : UserFormBase
    {
        private ParametrosConexion _Parametros;
        public event EventHandler EventoModal;
        private string IDFormBase;
        private SAPbouiCOM.Form oForm;
        private SAPbouiCOM.ComboBox TypeDB;
        private SAPbouiCOM.ComboBox Sociedades;
        private SAPbouiCOM.Button Btn_Conectar;
        private List<SAPbouiCOM.UserDataSource> UDS;
        private string SociedadAntes = "";

        public Parametros_b1f(ref ParametrosConexion Parametros, string IDFormBase,int Top,int left)
        {
            _Parametros = Parametros;
            this.IDFormBase = IDFormBase;
            this.oForm.Top = Top;
            this.oForm.Left = left;
            AsignarValores();
            
        }

        protected virtual void OeventoModal(EventArgs e)
        {
            EventHandler handler = EventoModal;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.OnCustomInitialize();
            Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(this.SBO_Application_ItemEvent);
            this.TypeDB.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.TypeDB_ComboSelectAfter);
            
            this.Btn_Conectar.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Btn_Conectar_PressedAfter);
            

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.CloseAfter += new CloseAfterHandler(this.Form_CloseAfter);

        }

        private void Form_CloseAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            OeventoModal(EventArgs.Empty);
            Application.SBO_Application.ItemEvent -= new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(this.SBO_Application_ItemEvent);
        }

        private void OnCustomInitialize()
        {
            this.oForm = (SAPbouiCOM.Form)this.UIAPIRawForm;
            this.TypeDB = ((SAPbouiCOM.ComboBox)(this.GetItem("txt_2").Specific));
            this.Sociedades = ((SAPbouiCOM.ComboBox)(this.GetItem("txt_3").Specific));
            this.Btn_Conectar= ((SAPbouiCOM.Button)(this.GetItem("btn_1").Specific));
            this.UDS = new List<SAPbouiCOM.UserDataSource>();

            for (int i = 0; i < this.oForm.DataSources.UserDataSources.Count; i++)
            {
                this.UDS.Add(this.oForm.DataSources.UserDataSources.Item(i));
            }
        }

        private void AsignarValores()
        {
            try
            {
                if (!String.IsNullOrEmpty(this._Parametros.SociedadName))
                {
                    this.UDS[0].Value = this._Parametros.server;
                    this.UDS[1].Value = this._Parametros.TypeDB;

                    this._Parametros.oCom.Server = this.UDS[0].Value;
                    this._Parametros.server = this.UDS[0].Value;
                    this._Parametros.oCom.DbServerType = TipoDB(this.UDS[1].Value);
                    this._Parametros.TypeDB = this.UDS[1].Value;

                    SAPbobsCOM.Recordset oRecord = this._Parametros.oCom.GetCompanyList();

                    if (oRecord.Fields.Count > 0 && this.Sociedades.ValidValues.Count == 0)
                    {
                        for (int i = 0; i < oRecord.Fields.Count; i++)
                        {
                            this.Sociedades.ValidValues.Add(oRecord.Fields.Item("dbName").Value.ToString(), oRecord.Fields.Item("cmpName").Value.ToString());
                            oRecord.MoveNext();
                        }

                    }

                    
                    this.UDS[2].Value = this._Parametros.Sociedad;
                    this.UDS[3].Value = this._Parametros.User;
                    this.UDS[4].Value = this._Parametros.Pass;
                }
            }catch(Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }


        public void SBO_Application_ItemEvent(string FormID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if(this.IDFormBase==FormID && pVal.BeforeAction)
            {
                this.oForm.Select();
                BubbleEvent = false;
            }
        }

        private SAPbobsCOM.BoDataServerTypes TipoDB(string Type)
        {
            if(Type == "MSSQL2012")
            {
                return SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
            }else if(Type == "MSSQL2014")
            {
                return SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
            }else
            {
                return SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
            }
            
        }



        private void TypeDB_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.UDS[0].Value))
                {
                    this._Parametros.oCom.Server = this.UDS[0].Value;
                    this._Parametros.server = this.UDS[0].Value;
                    this._Parametros.oCom.DbServerType = TipoDB(this.UDS[1].Value);
                    this._Parametros.TypeDB = this.UDS[1].Value;

                    SAPbobsCOM.Recordset oRecord = this._Parametros.oCom.GetCompanyList();

                    if (oRecord.Fields.Count > 0 && this.Sociedades.ValidValues.Count==0)
                    {
                        for(int i = 0; i < oRecord.Fields.Count; i++)
                        {
                            this.Sociedades.ValidValues.Add(oRecord.Fields.Item("dbName").Value.ToString(), oRecord.Fields.Item("cmpName").Value.ToString());
                            oRecord.MoveNext();
                        }
                        
                    }

                }
            }catch(Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }

        }

        private void AsignarParametros()
        {
            this._Parametros.oCom.Server = this.UDS[0].Value;
            this._Parametros.server = this.UDS[0].Value;
            this._Parametros.oCom.DbServerType = TipoDB(this.UDS[1].Value);
            this._Parametros.TypeDB = this.UDS[1].Value;
            this._Parametros.Sociedad = this.UDS[2].Value;
            this._Parametros.oCom.CompanyDB = this.UDS[2].Value;
            this._Parametros.User = this.UDS[3].Value;
            this._Parametros.oCom.UserName = this.UDS[3].Value;
            this._Parametros.Pass = this.UDS[4].Value;
            this._Parametros.oCom.Password = this.UDS[4].Value;
        }

        

        private void Btn_Conectar_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if(!(String.IsNullOrEmpty(this.UDS[0].Value) || String.IsNullOrEmpty(this.UDS[1].Value) || String.IsNullOrEmpty(this.UDS[2].Value) || String.IsNullOrEmpty(this.UDS[3].Value) || String.IsNullOrEmpty(this.UDS[4].Value)))
                {
                    this._Parametros.Sociedad = this.UDS[2].Value;
                    this._Parametros.oCom.CompanyDB = this.UDS[2].Value;
                    this._Parametros.User = this.UDS[3].Value;
                    this._Parametros.oCom.UserName = this.UDS[3].Value;
                    this._Parametros.Pass = this.UDS[4].Value;
                    this._Parametros.oCom.Password = this.UDS[4].Value;

                    if (!this._Parametros.oCom.Connected)
                    {
                        

                        int ErrorCode = this._Parametros.oCom.Connect();

                        if (ErrorCode != 0)
                        {
                            Application.SBO_Application.MessageBox(this._Parametros.oCom.GetLastErrorDescription() + "( " + ErrorCode.ToString() + ")");
                        }
                        else
                        {
                            this.SociedadAntes = this.UDS[2].Value;
                            this._Parametros.SociedadName = this._Parametros.oCom.CompanyName;
                            Application.SBO_Application.MessageBox("Conectados a "+ this._Parametros.SociedadName);
                            this.oForm.Close();
                        }
                    }
                    else
                    {
                        if(this._Parametros.oCom.CompanyDB!= this.SociedadAntes)
                        {
                            this._Parametros.oCom.Disconnect();
                            AsignarParametros();
                            int ErrorCode = this._Parametros.oCom.Connect();

                            if (ErrorCode != 0)
                            {
                                Application.SBO_Application.MessageBox(this._Parametros.oCom.GetLastErrorDescription() + "( " + ErrorCode.ToString() + ")");
                            }
                            else
                            {
                                this.SociedadAntes = this.UDS[2].Value;
                                this._Parametros.SociedadName = this._Parametros.oCom.CompanyName;
                                Application.SBO_Application.MessageBox("Conectados a " + this._Parametros.SociedadName);
                                this.oForm.Close();
                            }
                        }
                        else
                        {
                            Application.SBO_Application.MessageBox("Ya esta conectado");
                        }
                        
                    }

                }
                else
                {
                    Application.SBO_Application.MessageBox("Hay parametros vacios");
                }
            }catch(Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage("Error: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }
    }
}
