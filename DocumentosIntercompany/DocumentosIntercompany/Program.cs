using System;
using System.Collections.Generic;
using SAPbouiCOM.Framework;

namespace DocumentosIntercompany
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static SAPbobsCOM.Company oCom;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new Application();
                }
                else
                {
                    oApp = new Application(args[0]);
                }

                oCom = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();

                Menu MyMenu = new Menu();
                MyMenu.AddMenuItems();
                oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);
                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Program.oCom);
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Program.oCom);
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Program.oCom);
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Program.oCom);
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Program.oCom);
                    System.Windows.Forms.Application.Exit();
                    break;
                default:
                    break;
            }
        }
    }
}
