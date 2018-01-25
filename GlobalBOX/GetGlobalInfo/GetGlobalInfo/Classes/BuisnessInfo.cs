using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar.Classes
{
    public class BuisnessInfo
    {
        public int Blocked { get; set; }
        public int Total { get; set; }
        //Have CMail
        //0 Active, 1 Not Active, 2 Not Exist
        public int Active { get; set; }  
        public int NotActive { get; set; } 
        public int NotExist { get; set; }  
    }
}