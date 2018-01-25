using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar.Classes
{
    public class Company
    {
        public int CompanyID { get; set; }
        public String CompanyName { get; set; }
        public int CountryID { get; set; }
        public String CompanyVAT { get; set; }
        public String AccountCode { get; set; }
        public String WriteCode { get; set; }
        public int CompamyInfoCountryID { get; set; }
        public string CompamyInfoVAT { get; set; }
        public bool Blocked { get; set; }
        public int HaveCMail { get; set; }
        public int CompanyType { get; set; }
        public String MobilePhone { get; set; }
        public int GroupTypeID { get; set; }
        public bool InformMyMobile { get; set; }
    }
}
