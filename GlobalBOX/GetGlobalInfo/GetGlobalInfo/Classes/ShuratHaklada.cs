using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar.Classes
{
    public class ShuratHaklada
    {
        public string TransactionGUID { get; set; }
        public int CompanyID { get; set; }
        public int ActionCode { get; set; }
        public int MisparMismach { get; set; }
        public DateTime TarichMismach { get; set; }
        public DateTime TarichAcher { get; set; }
        public String ActionDetails { get; set; }
        public double AhuzHaMaam { get; set; }
        public double SchumPaturMaam { get; set; }
        public double SchumMaam { get; set; }
        public double SchumKolelMaam { get; set; }
        public string Attachment { get; set; }
        public bool Transfered { get; set; }
        public int CompamyInfoCountryID { get; set; }
        public string CompamyInfoVAT { get; set; }
        public bool Readen { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime LeTkufaMe { get; set; }
        public DateTime LeTkufaUd { get; set; }
        public String MisparProyect { get; set; }
        public String CompanyName { get; set; }
        public int CountryID { get; set; }
        public String CompanyVAT { get; set; }
    }
}
