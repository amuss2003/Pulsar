using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar.Classes
{
    public class WriteCodeRequest : RecordHeader
    {
        public String Message { get; set; }                 //99
        public String Answer { get; set; }                  //98
        public String ShemHaLakoh { get; set; }             //07
        public String OsekMoorshehLakoh { get; set; }       //08
        public String ShemHaSholeah { get; set; }           //15        
        public String OsekMoorshehHaSholeah { get; set; }   //16
        public String CountryIDHaSholeah { get; set; }      //17
    }
}
