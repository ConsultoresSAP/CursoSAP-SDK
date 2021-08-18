using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentosIntercompany
{
    class ParametrosConexion
    {
        public SAPbobsCOM.Company oCom { get; set; }
        public string server { get; set; } 
        public string TypeDB { get; set; }
        public string Sociedad { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string SociedadName { get; set; }

        
    }
}
