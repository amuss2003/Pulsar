using System;
using System.Collections.Generic;
using System.Web;

namespace GlobalInfoProtocol
{
    public class Company
    {
        public int CompanyID { get; set; }
        public String CompanyName { get; set; }
        public int CountryID { get; set; }
        public String CompanyVAT { get; set; }
        public String ReadCode { get; set; }
        public String WriteCode { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String EMail { get; set; }
        public String CompanyLogo { get; set; }
        public bool Active { get; set; }
        public String MAC { get; set; }
        public int TransactionCounter { get; set; }
        public String CompanySerialNumber { get; set; }
        public String Payment { get; set; }
        public bool CommercialUse { get; set; }
        public bool Paid { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StopService { get; set; }
        public DateTime StartService { get; set; }
        public String MobilePhone { get; set; }
        public bool InformMyMobile { get; set; }
    }
}