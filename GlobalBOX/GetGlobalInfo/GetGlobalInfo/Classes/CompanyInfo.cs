using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar.Classes
{
    public class CompanyInfo
    {
        public int CompanyID { get; set; }
        public String CompanyName { get; set; }
        public int CompanyCountryID { get; set; }
        public String CompanyVAT { get; set; }
        public string ReadCode { get; set; }
        public string WriteCode { get; set; }
        public String EMail { get; set; }
        public double maam { get; set; }
        public String FilesSearch { get; set; }
        public String DataPath { get; set; }
        public String CompanySerialNumber { get; set; }
        public Int32 SystemColor { get; set; }
        public String MobilePhone { get; set; }
        public bool InformMyMobile { get; set; }
        //public int IntervalMailCheck { get; set; }
    }
}
