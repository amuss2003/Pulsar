using System;
using System.Collections.Generic;
using System.Web;

namespace GlobalInfoProtocol.Classes
{
    public class Billing
    {
        public String CompanySerialNumber { get; set; }
        public DateTime DateMonth { get; set; }
        public int InCounter { get; set; }
        public int OutCounter { get; set; }
        public bool Paid { get; set; }
    }
}