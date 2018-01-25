using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar
{
    public class RecordHeader
    {
        public String TransactionGUID { get; set; }
        
        public int CountryIDFrom { get; set; }
        public String VatFrom { get; set; }
        
        public int CountryIDTo { get; set; }
        public String VatTo { get; set; }

        public DateTime TimeStampWrite { get; set; }
        public DateTime TimeStampRead { get; set; }
        public bool Readen { get; set; }

        public String Data { get; set; }

        public bool Confirm0 { get; set; }
        public bool Confirm1 { get; set; }
        public bool Confirm2 { get; set; }
    }
}
