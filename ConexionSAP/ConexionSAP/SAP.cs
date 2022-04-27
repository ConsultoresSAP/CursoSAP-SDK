using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using System.Xml;

namespace ConexionSAP
{
    class SAP
    {
        private Company oCom;
        public string Error="";
        public string CName = "";

        public SAP()
        {
            this.oCom = new Company();
        }


        public void Conectar()
        {
            try
            {
                this.oCom.Server = "LABORATORIO";
                this.oCom.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                this.oCom.UserName = "manager";
                this.oCom.Password = "manager";
                this.oCom.CompanyDB = "SBODemoGT";

                //this.oCom.DbUserName = "sa";
                //this.oCom.DbPassword = "SAPB1Admin1";

                if (!this.oCom.Connected)
                {
                    int ErrorCode = this.oCom.Connect();

                    if (ErrorCode != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription() + "( " + ErrorCode.ToString() + ")";
                    }
                    else
                    {
                        this.CName = this.oCom.CompanyName;
                    }
                }
                else
                {
                    this.Error = "Ya esta conectado";
                }



            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {

            }
        }



        public void Desconectar()
        {
            try
            {
                this.Error = "";
                if (this.oCom != null)
                {
                    this.oCom.Disconnect();
                }
            }catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (this.oCom != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(this.oCom);
                    this.oCom = null;
                }
                
            }
        }

        #region SN

        public void CrearSN()
        {
            SAPbobsCOM.BusinessPartners oSN = null;
            try
            {
                this.Error = "";
                oSN = (SAPbobsCOM.BusinessPartners)this.oCom.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                oSN.CardCode = "CL03";
                oSN.CardName = "Cliente TEST";
                oSN.CardType = BoCardTypes.cCustomer;
                oSN.FederalTaxID = "123456789023";

                if (oSN.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }

            }catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oSN != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSN);
                    oSN = null;
                }
            }
        }


        public void EditarSN(string CardCode)
        {
            SAPbobsCOM.BusinessPartners oSN = null;
            try
            {
                this.Error = "";
                oSN = (SAPbobsCOM.BusinessPartners)this.oCom.GetBusinessObject(BoObjectTypes.oBusinessPartners);

                if (oSN.GetByKey(CardCode))
                {
                    oSN.EmailAddress = "TEST@gmail.com";

                    if (oSN.Update() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                    }
                }
                else
                {
                    this.Error = "SN no existe";
                }

            }catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oSN != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSN);
                    oSN = null;
                }
            }
        }


        public void ADDContactosSN(string CardCode)
        {
            SAPbobsCOM.BusinessPartners oSN = null;
            try
            {
                this.Error = "";
                oSN = (SAPbobsCOM.BusinessPartners)this.oCom.GetBusinessObject(BoObjectTypes.oBusinessPartners);

                if (oSN.GetByKey(CardCode))
                {

                    if (oSN.ContactEmployees.Count > 1)
                    {
                        oSN.ContactEmployees.Add();
                    }else
                    {
                        if (oSN.ContactEmployees.Name != "")
                        {
                            oSN.ContactEmployees.Add();
                        }
                    }
                    
                    oSN.ContactEmployees.Name = "PRIMERO";
                    oSN.ContactEmployees.FirstName = "Pedro2";
                    oSN.ContactEmployees.MiddleName = "Juan2";
                    oSN.ContactEmployees.LastName = "Perez2";
                    oSN.ContactEmployees.Title = "Sr..";
                    //oSN.ContactEmployees.Address = "Test";
                    //oSN.ContactEmployees.Phone1 = "89451335";
                    //oSN.ContactEmployees.MobilePhone = "56784512";
                    //oSN.ContactEmployees.E_Mail = "Test@gmail.com";

                    if (oSN.Update() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                    }
                }
                else
                {
                    this.Error = "SN no existe";
                }

            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oSN != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSN);
                    oSN = null;
                }
            }
        }


        public void EDITContactosSN(string CardCode,int Line)
        {
            SAPbobsCOM.BusinessPartners oSN = null;
            try
            {
                this.Error = "";
                oSN = (SAPbobsCOM.BusinessPartners)this.oCom.GetBusinessObject(BoObjectTypes.oBusinessPartners);

                if (oSN.GetByKey(CardCode))
                {


                    oSN.ContactEmployees.SetCurrentLine(Line);
                    
                    oSN.ContactEmployees.FirstName = "";
                    oSN.ContactEmployees.MiddleName = "";
                    oSN.ContactEmployees.LastName = "";
                    oSN.ContactEmployees.Title = "";
                    //oSN.ContactEmployees.Address = "Test";
                    //oSN.ContactEmployees.Phone1 = "89451335";
                    //oSN.ContactEmployees.MobilePhone = "56784512";
                    //oSN.ContactEmployees.E_Mail = "Test@gmail.com";

                    if (oSN.Update() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                    }
                }
                else
                {
                    this.Error = "SN no existe";
                }

            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oSN != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSN);
                    oSN = null;
                }
            }
        }


        public void agregarDireccionSN(string CardCode)
        {
            SAPbobsCOM.BusinessPartners oSN = null;
            try
            {
                this.Error = "";
                oSN = (SAPbobsCOM.BusinessPartners)this.oCom.GetBusinessObject(BoObjectTypes.oBusinessPartners);

                if (oSN.GetByKey(CardCode))
                {
                    oSN.Addresses.Add();
                    oSN.Addresses.AddressName = "Dirección 3";
                    oSN.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_ShipTo;
                    

                    if (oSN.Update() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                    }
                }else
                {
                    this.Error = "SN no encontrado";
                }

               

            }
            catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oSN != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSN);
                    oSN = null;
                }
            }
        }


        #endregion

        #region ITEM    

        public void CrearItem()
        {
            SAPbobsCOM.Items oItem = null;
            try
            {
                oItem = (SAPbobsCOM.Items)this.oCom.GetBusinessObject(BoObjectTypes.oItems);
                oItem.ItemCode = "ART001";
                oItem.ItemName = "PALA";

                oItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tNO;

                oItem.WhsInfo.WarehouseCode = "01";

                if (oItem.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }

                this.Error = "";

            }catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oItem != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oItem);
                    oItem = null;
                }
            }
        }

        public void EditarItem(string ItemCode)
        {
            SAPbobsCOM.Items oItem = null;
            try
            {
                oItem = (SAPbobsCOM.Items)this.oCom.GetBusinessObject(BoObjectTypes.oItems);

                if (oItem.GetByKey(ItemCode))
                {
                    oItem.WhsInfo.WarehouseCode = "5";
                    oItem.WhsInfo.Add();


                    if (oItem.Update() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                    }
                }else
                {
                    this.Error = "Item no existe";
                }
               

                this.Error = "";

            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oItem != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oItem);
                    oItem = null;
                }
            }
        }


        #endregion


        #region DOCUMENTOS

        #region PEDIDO

        public void CrearPedido(out string DocEntry)
        {
            DocEntry = "";
            SAPbobsCOM.Documents oPedido = null;
            try
            {
                this.Error = "";
                oPedido = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oOrders);
                //oPedido.Series = 02;
                oPedido.CardCode = "CL01";
                oPedido.DocDate = DateTime.Today;
                oPedido.DocDueDate = DateTime.Today;
                oPedido.Comments = "test 2";

                oPedido.Lines.ItemCode = "A00004";
                oPedido.Lines.Quantity = 2;
                oPedido.Lines.TaxCode = "IVA";

                oPedido.Lines.Add();

                oPedido.Lines.ItemCode = "A00005";
                oPedido.Lines.Quantity = 1;
                oPedido.Lines.TaxCode = "EXE";

                if (oPedido.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    DocEntry = this.oCom.GetNewObjectKey();
                }
            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oPedido != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oPedido);
                    oPedido = null;
                }
            }
        }

        public void agregarLineaPedido(int DocEntry)
        {
            SAPbobsCOM.Documents oPedido = null;
            try
            {
                this.Error = "";
                oPedido = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oOrders);

                if (oPedido.GetByKey(DocEntry))
                {
                    if (oPedido.DocumentStatus != SAPbobsCOM.BoStatus.bost_Close)
                    {
                        oPedido.Lines.Add();
                        oPedido.Lines.ItemCode = "B10000";
                        oPedido.Lines.Quantity = 1;

                        if (oPedido.Update() != 0)
                        {
                            this.Error = this.oCom.GetLastErrorDescription();
                        }
                    }
                    else
                    {
                        this.Error = "Documento Cerrado";
                    }


                }
                else
                {
                    this.Error = "Pedido no existe";
                }

            }
            catch (Exception e)
            {

            }
            finally
            {

            }
        }


        public void CrearPedidoDeTipoServicio(out string DocEntry)
        {
            DocEntry = "";
            SAPbobsCOM.Documents oPedido = null;
            try
            {
                this.Error = "";
                oPedido = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oOrders);
                //oPedido.Series = 02;
                oPedido.CardCode = "CL01";
                oPedido.DocDate = DateTime.Today;
                oPedido.DocDueDate = DateTime.Today;
                oPedido.Comments = "Pedido de tipo servicio";

                oPedido.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service;

                oPedido.Lines.ItemDescription = "Servicio de ejemplo";
                oPedido.Lines.AccountCode = "_SYS00000000361";
                oPedido.Lines.LineTotal = 500;


                if (oPedido.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    DocEntry = this.oCom.GetNewObjectKey();
                }
            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oPedido != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oPedido);
                    oPedido = null;
                }
            }
        }


        #endregion

        #region ENTREGAS

        public void CrearEntrega(out string DocEntry)
        {
            DocEntry = "";
            SAPbobsCOM.Documents oEntrega = null;
            try
            {
                this.Error = "";
                oEntrega = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                oEntrega.CardCode = "CL01";
                oEntrega.DocDate = DateTime.Today;
                oEntrega.DocDueDate = DateTime.Today;
                oEntrega.Comments = "test 2";

                oEntrega.Lines.ItemCode = "A00004";
                oEntrega.Lines.Quantity = 2;
                oEntrega.Lines.TaxCode = "IVA";

                oEntrega.Lines.Add();

                oEntrega.Lines.ItemCode = "A00005";
                oEntrega.Lines.Quantity = 1;
                oEntrega.Lines.TaxCode = "EXE";

                if (oEntrega.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    DocEntry = this.oCom.GetNewObjectKey();
                }
            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oEntrega != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oEntrega);
                    oEntrega = null;
                }
            }
        }

        #endregion

        #region DEVOLUCION

        public void CrearDevolucion(out string DocEntry)
        {
            DocEntry = "";
            SAPbobsCOM.Documents oDevolucion = null;
            try
            {
                this.Error = "";
                oDevolucion = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oReturns);
                oDevolucion.CardCode = "CL01";
                oDevolucion.DocDate = DateTime.Today;
                oDevolucion.DocDueDate = DateTime.Today;
                oDevolucion.Comments = "test 2";

                oDevolucion.Lines.ItemCode = "A00004";
                oDevolucion.Lines.Quantity = 2;
                oDevolucion.Lines.TaxCode = "IVA";



                if (oDevolucion.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    DocEntry = this.oCom.GetNewObjectKey();
                }
            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oDevolucion != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oDevolucion);
                    oDevolucion = null;
                }
            }
        }

        #endregion

        #region SALIDAS

        public void CrearSalida(out string DocEntry)
        {
            DocEntry = "";
            SAPbobsCOM.Documents oSalida = null;
            try
            {
                this.Error = "";
                oSalida = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oInventoryGenExit);
                oSalida.DocDate = DateTime.Today;
                oSalida.DocDueDate = DateTime.Today;
                oSalida.GroupNumber = -1;

                oSalida.Lines.ItemCode = "B10000";
                oSalida.Lines.Quantity = 2;
                oSalida.Lines.CostingCode = "10001";
                oSalida.Lines.CostingCode2 = "20001";


                //Lotes
                oSalida.Lines.BatchNumbers.SetCurrentLine(0);
                oSalida.Lines.BatchNumbers.Quantity = 2;
                oSalida.Lines.BatchNumbers.BatchNumber = "L01";

                if (oSalida.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    DocEntry = this.oCom.GetNewObjectKey();
                }

            }
            catch (Exception e)
            {

            }
            finally
            {

            }
        }

        #endregion

        #region FACTURA CON DOCUMENTO BASE

        public void CrearFacturaConDocumentoBase(out string DocEntry)
        {
            DocEntry = "";
            SAPbobsCOM.Documents oFacturas = null;
            try
            {
                this.Error = "";
                oFacturas = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oInvoices);
                oFacturas.CardCode = "CL01";
                oFacturas.DocDate = DateTime.Today;
                oFacturas.DocDueDate = DateTime.Today;

                oFacturas.Lines.BaseType = (int)SAPbobsCOM.BoObjectTypes.oOrders;
                oFacturas.Lines.BaseEntry = 555;
                oFacturas.Lines.BaseLine = 0;
                oFacturas.Lines.TaxCode = "IVA";

                oFacturas.Lines.Add();
                oFacturas.Lines.BaseType = (int)SAPbobsCOM.BoObjectTypes.oOrders;
                oFacturas.Lines.BaseEntry = 555;
                oFacturas.Lines.BaseLine = 1;
                oFacturas.Lines.TaxCode = "IVA";

                oFacturas.Lines.Add();
                oFacturas.Lines.BaseType = (int)SAPbobsCOM.BoObjectTypes.oOrders;
                oFacturas.Lines.BaseEntry = 555;
                oFacturas.Lines.BaseLine = 2;
                oFacturas.Lines.TaxCode = "IVA";

                oFacturas.Lines.BatchNumbers.SetCurrentLine(0);
                oFacturas.Lines.BatchNumbers.Quantity = 1;
                oFacturas.Lines.BatchNumbers.BatchNumber = "L01";

                if (oFacturas.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    DocEntry = this.oCom.GetNewObjectKey();
                }

            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {

            }
        }

        public void CrearFacturaConDocumentoBase(out string DocEntry, string DocNumPedido)
        {
            DocEntry = "";
            SAPbobsCOM.Documents oFacturas = null;
            SAPbobsCOM.Recordset oRecord = null;
            try
            {
                this.Error = "";
                oFacturas = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oInvoices);
                oRecord = (SAPbobsCOM.Recordset)this.oCom.GetBusinessObject(BoObjectTypes.BoRecordset);


                oRecord.DoQuery("SELECT " +
                                "T0.[CardCode], " +
                                "T0.[DocEntry] AS 'N. Interno', " +
                                "T1.[LineNum] AS 'Linea', " +
                                "T1.[TaxCode] AS 'Impuesto' " +
                                "FROM[dbo].[ORDR] T0 " +
                                "INNER JOIN[dbo].[RDR1] T1 ON T0.[DocEntry] = T1.[DocEntry] " +
                                "WHERE " +
                                "T0.[DocStatus] = 'O' " +
                                "AND T0.[DocEntry] = 556  ");

                if (oRecord.RecordCount > 0)
                {
                    oRecord.MoveFirst();
                    oFacturas.CardCode = oRecord.Fields.Item("CardCode").Value.ToString();
                    oFacturas.DocDate = DateTime.Today;
                    oFacturas.DocDueDate = DateTime.Today;

                    for (int i = 1; i <= oRecord.RecordCount; i++)
                    {
                        if (i != 1)
                        {
                            oFacturas.Lines.Add();
                        }
                        oFacturas.Lines.BaseType = (int)SAPbobsCOM.BoObjectTypes.oOrders;
                        oFacturas.Lines.BaseEntry = Int32.Parse(oRecord.Fields.Item("N. Interno").Value.ToString());
                        oFacturas.Lines.BaseLine = Int32.Parse(oRecord.Fields.Item("Linea").Value.ToString()); ;
                        oFacturas.Lines.TaxCode = oRecord.Fields.Item("Impuesto").Value.ToString();
                        oRecord.MoveNext();
                    }
                }

                if (oFacturas.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    DocEntry = this.oCom.GetNewObjectKey();
                }

            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {

            }
        }



        #endregion

        #region TRANSFERENCIAS

        public void CrearTransferencia(out string Valor)
        {
            Valor = "";
            SAPbobsCOM.StockTransfer oTransf = null;
            try
            {
                this.Error = "";
                oTransf = (SAPbobsCOM.StockTransfer)this.oCom.GetBusinessObject(BoObjectTypes.oStockTransfer);
                oTransf.CardCode = "CL01";
                oTransf.DocDate = DateTime.Today;
                oTransf.DueDate = DateTime.Today;

                oTransf.FromWarehouse = "01";
                oTransf.ToWarehouse = "02";

                oTransf.Lines.ItemCode = "A00001";
                oTransf.Lines.Quantity = 2;

                oTransf.Lines.Add();
                oTransf.Lines.ItemCode = "A00002";
                oTransf.Lines.Quantity = 1;
                oTransf.Lines.FromWarehouseCode = "01";
                oTransf.Lines.WarehouseCode = "5";

                if (oTransf.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    Valor = this.oCom.GetNewObjectKey();
                }


            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oTransf != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oTransf);
                    oTransf = null;
                }
            }
        }

        #endregion

        #region Borrador

        public void CrearPedidoEnBaseABorrador(out string DocEntry)
        {
            DocEntry = "";
            SAPbobsCOM.Documents oPedido = null;
            SAPbobsCOM.Documents Draft = null;
            try
            {
                this.Error = "";
                this.oCom.XmlExportType = SAPbobsCOM.BoXmlExportTypes.xet_ExportImportMode;
                this.oCom.XMLAsString = false;
                string xml = @"C:\Users\User-02\Desktop\CursoSAP DI API\CursoSDK-DIAPI\XMLBorrador.xml";
                XmlDocument XmlBorrador = new XmlDocument();
                XmlNode NodoDocObjectCode;
                XmlNode NodoDocNum;
                XmlNode Nodo;

                Draft = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oDrafts);

                Draft.GetByKey(87);

                Draft.SaveXML(ref xml);
                XmlBorrador.Load(xml);
                XmlBorrador.SelectSingleNode("BOM/BO/AdmInfo/Object").InnerText = "17";
                NodoDocNum = XmlBorrador.SelectSingleNode("BOM/BO/Documents/row/DocNum");
                XmlBorrador.SelectSingleNode("BOM/BO/Documents/row").RemoveChild(NodoDocNum);

                NodoDocObjectCode = XmlBorrador.SelectSingleNode("BOM/BO/Documents/row/DocObjectCode");
                XmlBorrador.SelectSingleNode("BOM/BO/Documents/row").RemoveChild(NodoDocObjectCode);

                Nodo = XmlBorrador.SelectSingleNode("BOM/BO/Documents/row/ReqType");
                XmlBorrador.SelectSingleNode("BOM/BO/Documents/row").RemoveChild(Nodo);
                Nodo = XmlBorrador.SelectSingleNode("BOM/BO/Documents/row/Revision");
                XmlBorrador.SelectSingleNode("BOM/BO/Documents/row").RemoveChild(Nodo);
                Nodo = XmlBorrador.SelectSingleNode("BOM/BO/Documents/row/IssuingReason");
                XmlBorrador.SelectSingleNode("BOM/BO/Documents/row").RemoveChild(Nodo);

                for (int i = 0; i < XmlBorrador.SelectNodes("BOM/BO/Document_Lines/row").Count; i++)
                {
                    var NodoActual = XmlBorrador.SelectNodes("BOM/BO/Document_Lines/row")[i];
                    string valor = NodoActual.InnerXml;
                    Nodo = NodoActual.SelectSingleNode("EnableReturnCost");
                    XmlBorrador.SelectNodes("BOM/BO/Document_Lines/row")[i].RemoveChild(Nodo);
                    Nodo = NodoActual.SelectSingleNode("ReturnCost");
                    XmlBorrador.SelectNodes("BOM/BO/Document_Lines/row")[i].RemoveChild(Nodo);
                    Nodo = NodoActual.SelectSingleNode("LineVendor");
                    XmlBorrador.SelectNodes("BOM/BO/Document_Lines/row")[i].RemoveChild(Nodo);
                }


                XmlBorrador.Save(xml);

                oPedido = (SAPbobsCOM.Documents)this.oCom.GetBusinessObjectFromXML(xml, 0);

                if (oPedido.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }
                else
                {
                    DocEntry = this.oCom.GetNewObjectKey();
                    Draft.Remove();
                }

            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oPedido != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oPedido);
                    oPedido = null;
                }
            }
        }

        #endregion

        #region PAGOS

        public void CrearPago(out string Valor, int DocEntry)
        {
            Valor = "";
            SAPbobsCOM.Payments oPago = null;
            SAPbobsCOM.Documents oFacturaBase = null;
            try
            {
                this.Error = "";
                oPago = (SAPbobsCOM.Payments)this.oCom.GetBusinessObject(BoObjectTypes.oIncomingPayments);
                oFacturaBase = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oInvoices);

                if (oFacturaBase.GetByKey(DocEntry))
                {
                    oPago.CardCode = oFacturaBase.CardCode;
                    oPago.DocDate = DateTime.Today;
                    oPago.DueDate = DateTime.Today;

                    //Medios de Pago
                    //Efectivo
                    //oPago.CashSum = oFacturaBase.DocTotal; Pagar Total de la factura
                    oPago.CashSum = 10;

                    //Transferencia
                    oPago.TransferAccount = "_SYS00000000001";
                    oPago.TransferDate = DateTime.Today;
                    oPago.TransferReference = "4561278";
                    oPago.TransferSum = 10;

                    //Cheques
                    //oPago.Checks.CheckSum = 20;
                    //oPago.Checks.CountryCode = "GT";
                    //oPago.Checks.BankCode = "BBANK";
                    //oPago.Checks.AccounttNum = "8945127456";
                    //oPago.Checks.CheckNumber = 567845125;
                    //oPago.Checks.DueDate = DateTime.Today;
                    //oPago.Checks.Trnsfrable = SAPbobsCOM.BoYesNoEnum.tYES;

                    //TC
                    //oPago.CreditCards.CreditCard = 3;
                    //oPago.CreditCards.CreditCardNumber = "567845125678";
                    //oPago.CreditCards.CardValidUntil = DateTime.Parse("10/10/2024");
                    //oPago.CreditCards.VoucherNum = "56784523512";
                    //oPago.CreditCards.CreditSum = 15;

                    oPago.Invoices.DocEntry = oFacturaBase.DocEntry;
                    oPago.Invoices.SumApplied = 20;


                    if (oPago.Add() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                    }
                    else
                    {
                        Valor = this.oCom.GetNewObjectKey();
                    }

                }
                else
                {
                    this.Error = "Factura no existe";
                }

            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oPago != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oPago);
                }
                if (oFacturaBase != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oFacturaBase);
                }
                oPago = null;
                oFacturaBase = null;
            }
        }

        public void CrearPago(out string Valor, string DocNum)
        {
            Valor = "";
            SAPbobsCOM.Payments oPago = null;
            SAPbobsCOM.Recordset oRecord = null;
            try
            {
                this.Error = "";
                oPago = (SAPbobsCOM.Payments)this.oCom.GetBusinessObject(BoObjectTypes.oIncomingPayments);
                oRecord = (SAPbobsCOM.Recordset)this.oCom.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRecord.DoQuery("SELECT * FROM [OINV] T0 WHERE T0.[DocNum]=" + DocNum);

                if (oRecord.RecordCount > 0)
                {
                    oPago.CardCode = oRecord.Fields.Item("CardCode").Value.ToString();
                    oPago.DocDate = DateTime.Today;
                    oPago.DueDate = DateTime.Today;

                    //Medios de Pago
                    //Efectivo
                    //oPago.CashSum = oFacturaBase.DocTotal; Pagar Total de la factura
                    oPago.CashSum = Double.Parse(oRecord.Fields.Item("DocTotal").Value.ToString()) - Double.Parse(oRecord.Fields.Item("PaidToDate").Value.ToString());

                    //Transferencia
                    //oPago.TransferAccount = "_SYS00000000001";
                    //oPago.TransferDate = DateTime.Today;
                    //oPago.TransferReference = "4561278";
                    //oPago.TransferSum = 10;

                    //Cheques
                    //oPago.Checks.CheckSum = 20;
                    //oPago.Checks.CountryCode = "GT";
                    //oPago.Checks.BankCode = "BBANK";
                    //oPago.Checks.AccounttNum = "8945127456";
                    //oPago.Checks.CheckNumber = 567845125;
                    //oPago.Checks.DueDate = DateTime.Today;
                    //oPago.Checks.Trnsfrable = SAPbobsCOM.BoYesNoEnum.tYES;

                    //TC
                    //oPago.CreditCards.CreditCard = 3;
                    //oPago.CreditCards.CreditCardNumber = "567845125678";
                    //oPago.CreditCards.CardValidUntil = DateTime.Parse("10/10/2024");
                    //oPago.CreditCards.VoucherNum = "56784523512";
                    //oPago.CreditCards.CreditSum = 15;

                    oPago.Invoices.DocEntry = Int32.Parse(oRecord.Fields.Item("DocEntry").Value.ToString());
                    oPago.Invoices.SumApplied = Double.Parse(oRecord.Fields.Item("DocTotal").Value.ToString()) - Double.Parse(oRecord.Fields.Item("PaidToDate").Value.ToString());


                    if (oPago.Add() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                    }
                    else
                    {
                        Valor = this.oCom.GetNewObjectKey();
                    }

                }
                else
                {
                    this.Error = "Factura no existe";
                }

            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oPago != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oPago);
                }
                if (oRecord != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord);
                }
                oPago = null;
                oRecord = null;
            }
        }

        #endregion

        #endregion

        #region RECORDSET

        public void Record(out string Datos,int DocEntry)
        {
            Datos = "";
            SAPbobsCOM.Recordset oRecod = null;
            try
            {
                this.Error = "";
                oRecod = (SAPbobsCOM.Recordset)this.oCom.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRecod.DoQuery("SELECT * FROM [OINV] T0 WHERE T0.[DocEntry]="+DocEntry.ToString());

                if (oRecod.RecordCount > 0)
                {
                    Datos = "Cliente: "+oRecod.Fields.Item("CardCode").Value.ToString()
                            +"-"+oRecod.Fields.Item("CardName").Value.ToString()
                            +", Total Factura: "+oRecod.Fields.Item("DocTotal").Value.ToString();
                }else
                {
                    this.Error = "No hay datos";
                }

            }catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oRecod != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecod);
                    oRecod = null;
                }
            }
        }

        #endregion

        #region TRANSACTION

        public void EjemploTransaction()
        {
            this.Error = "";
            SAPbobsCOM.Documents oFactura = null;
            SAPbobsCOM.Payments oPago = null;
            try
            {
                oFactura = (SAPbobsCOM.Documents)this.oCom.GetBusinessObject(BoObjectTypes.oInvoices);
                oPago = (SAPbobsCOM.Payments)this.oCom.GetBusinessObject(BoObjectTypes.oIncomingPayments);

                if (!this.oCom.InTransaction)
                {
                    this.oCom.StartTransaction();
                }
                string DocEntry = "";
                oFactura.CardCode = "CL01";
                oFactura.DocDate = DateTime.Today;
                oFactura.DocDueDate = DateTime.Today;

                oFactura.Lines.ItemCode = "A00001";
                oFactura.Lines.Quantity = 1;
                oFactura.Lines.TaxCode = "IVA";

                if (oFactura.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                    if (this.oCom.InTransaction)
                    {
                        this.oCom.EndTransaction(BoWfTransOpt.wf_RollBack);
                    }
                }else
                {
                    DocEntry = this.oCom.GetNewObjectKey();

                    oPago.CardCode = "CL01";
                    oPago.DocDate = DateTime.Today;
                    oPago.DocDate = DateTime.Today;

                    oPago.CashSum = 200;

                    oPago.Invoices.DocEntry = Int32.Parse("700");
                    oPago.Invoices.SumApplied = 200;

                    if (oPago.Add() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                        if (this.oCom.InTransaction)
                        {
                            this.oCom.EndTransaction(BoWfTransOpt.wf_RollBack);
                        }
                    }else
                    {
                        if (this.oCom.InTransaction)
                        {
                            this.oCom.EndTransaction(BoWfTransOpt.wf_Commit);
                        }
                    }
                }
                
                



            }catch(Exception e)
            {
                this.Error = e.Message;
                if (this.oCom.InTransaction)
                {
                    this.oCom.EndTransaction(BoWfTransOpt.wf_RollBack);
                }
            }
            finally
            {
                if (this.oCom != null)
                {
                    if (this.oCom.InTransaction)
                    {
                        this.oCom.EndTransaction(BoWfTransOpt.wf_RollBack);
                    }
                }
            }
        }

        #endregion

        #region TABLAS

        public void CrearTabla(string Nombre, string Desc,SAPbobsCOM.BoUTBTableType Type)
        {
            SAPbobsCOM.IUserTablesMD oTabla = null;
            try
            {
                this.Error = "";
                oTabla = (SAPbobsCOM.IUserTablesMD)this.oCom.GetBusinessObject(BoObjectTypes.oUserTables);

                if (!oTabla.GetByKey(Nombre))
                {
                    oTabla.TableName = Nombre;
                    oTabla.TableDescription = Desc;
                    oTabla.TableType = Type;

                    if (oTabla.Add() != 0)
                    {
                        this.Error = this.oCom.GetLastErrorDescription();
                    }
                }else
                {
                    this.Error = "Tabla ya existe";
                }
                

            }catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oTabla != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oTabla);
                    oTabla = null;
                    GC.Collect();
                }
                
            }
        }

        #endregion

        #region UDF

        public void CrearUDF(string Tabla, string Code, string Desc, int Tam, 
                            SAPbobsCOM.BoFieldTypes Type,SAPbobsCOM.BoFldSubTypes SubType,
                            SAPbobsCOM.BoYesNoEnum obligatorio,string TablaEnlazada,
                            string VDefecto,List<ValoresValidos> Valores)
        {
            SAPbobsCOM.UserFieldsMD oUDF = null;
            SAPbobsCOM.Recordset oRec = null;
            try
            {

                this.Error = "";
                oUDF = (SAPbobsCOM.UserFieldsMD)this.oCom.GetBusinessObject(BoObjectTypes.oUserFields);
                oRec = (SAPbobsCOM.Recordset)this.oCom.GetBusinessObject(BoObjectTypes.BoRecordset);

                int Key;
                oRec.DoQuery("SELECT T0.[FieldID] FROM CUFD T0 WHERE T0.[TableID]='"+Tabla+"' AND T0.[AliasID]='"+Code+"'");
                bool Existe = false;
                if (oRec.RecordCount > 0)
                {
                    
                    if (oRec.Fields.Item("FieldID").Value.ToString() != "")
                    {
                        Key = Convert.ToInt32(oRec.Fields.Item("FieldID").Value.ToString());
                        oUDF.GetByKey(Tabla, Key);
                        
                        Existe = true;
                    }

                }

                if (oRec != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRec);
                    oRec = null;
                    GC.Collect();
                }

                    oUDF.TableName = Tabla;
                    oUDF.Name = Code;
                    oUDF.Description = Desc;
                    oUDF.Size = Tam;
                    oUDF.Type = Type;
                    oUDF.SubType = SubType;
                    oUDF.Mandatory = obligatorio;

                    if (TablaEnlazada != "")
                    {
                        oUDF.LinkedTable = TablaEnlazada;
                    }
                    if (VDefecto != "")
                    {
                        oUDF.DefaultValue = VDefecto;
                    }
                    if (Valores != null)
                    {
                        for (int i = 0; i < Valores.Count; i++)
                        {
                            oUDF.ValidValues.Value = Valores[i].Valor;
                            oUDF.ValidValues.Description = Valores[i].descripcion;
                            oUDF.ValidValues.Add();
                        }

                    }

                    if (!Existe)
                    {
                        if (oUDF.Add() != 0)
                        {
                            this.Error = this.oCom.GetLastErrorDescription();
                        }
                    }else
                    {
                        if (oUDF.Update() != 0)
                        {
                            this.Error = this.oCom.GetLastErrorDescription();
                        }
                    }

            }
            catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oUDF != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUDF);
                    oUDF = null;
                    GC.Collect();
                }
            }
        }

        #endregion

        #region UDOS

        public void CrearUDO()
        {
            SAPbobsCOM.UserObjectsMD oUDO = null;
            try
            {
                this.Error = "";
                oUDO = (SAPbobsCOM.UserObjectsMD)this.oCom.GetBusinessObject(BoObjectTypes.oUserObjectsMD);

                oUDO.Code = "CPADRE";
                oUDO.Name = "Udo Padre";
                oUDO.ObjectType = SAPbobsCOM.BoUDOObjType.boud_Document;
                oUDO.TableName = "PADRE";

                oUDO.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                oUDO.CanClose = SAPbobsCOM.BoYesNoEnum.tYES;
                oUDO.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                oUDO.CanCancel = SAPbobsCOM.BoYesNoEnum.tNO;
                oUDO.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO;

                //oUDO.FindColumns.ColumnAlias = "Precio";
                //oUDO.FindColumns.Add();

                //oUDO.FindColumns.ColumnAlias = "Tipo";
                //oUDO.FindColumns.Add();

                oUDO.ChildTables.TableName = "HIJA";
                oUDO.ChildTables.Add();

                if (oUDO.Add() != 0)
                {
                    this.Error = this.oCom.GetLastErrorDescription();
                }

            }
            catch(Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (oUDO != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUDO);
                    oUDO = null;
                    GC.Collect();
                }
            }
        }

        #endregion

        #region Insertar Regsitros UDO

        private void ADD()
        {
            SAPbobsCOM.CompanyService oCompanyService = null;
            SAPbobsCOM.GeneralService oGeneralServices = null;
            SAPbobsCOM.GeneralData oGeneralData = null;
            SAPbobsCOM.GeneralDataParams oGeneralParams = null;

            SAPbobsCOM.GeneralDataCollection Lineas = null;
            SAPbobsCOM.GeneralData Linea = null;
            try
            {
                oCompanyService = this.oCom.GetCompanyService();
                oGeneralServices = oCompanyService.GetGeneralService("HC_CONT");
                oGeneralData = ((SAPbobsCOM.GeneralData)(oGeneralServices.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)));

                oGeneralData.SetProperty("U_HC_QQAb", "Valor");

                Lineas = oGeneralData.Child("HC_CON1");

                Linea = Lineas.Add();
                Linea.SetProperty("U_HC_QQPend", "ValorLinea");

                oGeneralParams = oGeneralServices.Add(oGeneralData);

                oGeneralServices.Update(oGeneralData);
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (oCompanyService != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompanyService);
                    oCompanyService = null;
                }
                if (oGeneralServices != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralServices);
                    oGeneralServices = null;
                }
                if (oGeneralData != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralData);
                    oGeneralData = null;
                }
                if (oGeneralParams != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralParams);
                    oGeneralParams = null;
                }
                if (Lineas != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Lineas);
                    Lineas = null;
                }
                if (Linea != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Linea);
                    Linea = null;
                }
            }
        }

        private void Editar()
        {
            SAPbobsCOM.CompanyService oCompanyService = null;
            SAPbobsCOM.GeneralService oGeneralServices = null;
            SAPbobsCOM.GeneralData oGeneralData = null;
            SAPbobsCOM.GeneralDataParams oGeneralParams = null;

            SAPbobsCOM.GeneralDataCollection Lineas = null;
            SAPbobsCOM.GeneralData Linea = null;
            try
            {  
                oCompanyService = this.oCom.GetCompanyService();
                oGeneralServices = oCompanyService.GetGeneralService("HC_CONT");    
                  
                oGeneralParams = (SAPbobsCOM.GeneralDataParams)oGeneralServices.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);  
                oGeneralParams.SetProperty("DocEntry", Int32.Parse("Entry"));

                oGeneralData = oGeneralServices.GetByParams(oGeneralParams);
                oGeneralData.SetProperty("U_HC_QQAb", "Valor");

                Lineas = oGeneralData.Child("HC_CON1");

                Linea = Lineas.Item(1);
                Linea.SetProperty("U_HC_QQPend", "ValorLinea");

                oGeneralServices.Update(oGeneralData);
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (oCompanyService != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompanyService);
                    oCompanyService = null;
                }
                if (oGeneralServices != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralServices);
                    oGeneralServices = null;
                }
                if (oGeneralData != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralData);
                    oGeneralData = null;
                }
                if (oGeneralParams != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralParams);
                    oGeneralParams = null;
                }
                if (Lineas != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Lineas);
                    Lineas = null;
                }
                if (Linea != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Linea);
                    Linea = null;
                }
            }
        }

        #endregion


        #region Insertar Registros UDT



        #endregion

        #region PRICELIST

        public void ActualizarListaDePrecios()
        {
            SAPbobsCOM.Items Item = null;
            try
            {
                if (true)
                {
                    Item = (SAPbobsCOM.Items)this.oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                    int listaEditar = 2;
                    int listaBase = 2;
                    double Precio=500;
                    double Factor=1;
                    string ItemCode= "A00002";

                    if (Item.GetByKey(ItemCode))
                    {
                        Item.PriceList.SetCurrentLine(listaEditar);
                           
                            Item.PriceList.BasePriceList = listaBase;
                            Item.PriceList.Factor = Factor;
                            //Item.PriceList.Price = Precio;
                            int lretcode = Item.Update();
                            if (lretcode != 0)
                            {
                                this.Error= "Error en Articulo:" + ItemCode + " No se continuará con la actualización de la lista, " + this.oCom.GetLastErrorDescription().ToString();
                            }
                    }
                }
            }
            catch (Exception e)
            {
                this.Error = e.Message;
            }
            finally
            {
                if (Item != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Item);
                    Item = null;
                }

            }
        }

        #endregion




    }

    class ValoresValidos
    {
        public string Valor;
        public string descripcion;
    }


}
