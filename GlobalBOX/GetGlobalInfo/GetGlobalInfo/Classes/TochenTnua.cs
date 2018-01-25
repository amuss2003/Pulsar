using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar.Classes
{
    public class TochenTnua : RecordHeader
    {
        public String TransactionGUID { get; set; }
        public int CompanyID { get; set; }
        public int MisparPnimi { get; set; }
        public int MisparShuraBaTnua { get; set; }
        public int CodeParitNumeri { get; set; }
        public String CodeParitAlpha { get; set; }  //20
        public String TeurParitAlpha { get; set; }  //150
        public int Kamut1 { get; set; }
        public int Kamut2 { get; set; }
        public int Kamut3 { get; set; }
        public int Kamut4 { get; set; }
        public int KamutKlalit { get; set; }
        public String TeurYahida { get; set; }  //10
        public double MechirYehida { get; set; }
        public double SchumLefniHanacha { get; set; }
        public double AhuzHanacha { get; set; }
        public double SchumHanacha { get; set; }
        public double SchumShura { get; set; }
        public double AhuzHaMaam { get; set; }
        public int CodeMatbea { get; set; }
        public double SharMatbea { get; set; }
        public double MechirYehidaBeMatbea { get; set; }
        public double SchumShuraMatbeaLefniHanacha { get; set; }
        public double SchumHanachaBeMatbea { get; set; }
        public double SchumShuraMatbeaLacharHanacha { get; set; }
        public String BarCodeParit { get; set; }
        public bool Transfered { get; set; }
        public double RawDataNumber { get; set; }
    }
}

//תוכן תנועה
//======================================
//קוד מדינה מערכת	    *
//עוסק מורשה מערכת	*
//מספר תנועה חיצוני	// מספר פנימי*
//מספר שורה בתנועה	*
//קוד פריט נומרי חיצוני	*
//קוד פריט אלפה	*
//תאור פריט אלפה	
//כמות 1	
//כמות 2	
//כמות 3	
//כמות כללית	
//תאור יחידה	
//מחיר יחידה	
//סכום לפני הנחה	
//אחוז הנחה	
//סכום הנחה	
//סכום לאחר הנחה	
//אחוז המע"מ	
//קוד מטבע	
//שער מטבע	
//מחיר יחידה במטבע	
//סכום שורה מטבע לפני הנחה	
//סכום הנחה במטבע	
//סכום שורה במטבע לאחר הנחה	
//בר קוד פריט

