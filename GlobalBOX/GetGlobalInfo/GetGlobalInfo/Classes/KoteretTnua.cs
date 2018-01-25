using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar
{
    public class KoteretTnua : RecordHeader
    {
        public int MisparPnimi { get; set; }
        public int MisparMismach { get; set; }  
        public DateTime TarichMismach { get; set; }
        public DateTime TarichKovea_Divuch { get; set; }
        public DateTime TarichMishloah { get; set; }
        public DateTime TarichAher { get; set; }
        public String ShemHaLakoh { get; set; }
        public String OsekMoorshehLakoh { get; set; }
        public String MeidaNosaf { get; set; }
        public double Maam { get; set; }
        public double SchumLifneMaam { get; set; }
        public double SchumPaturMeMaam { get; set; }
        public double SchumHaMaam { get; set; }
        public double SchumKolelMaam { get; set; }
        public String SugTnua { get; set; }
        public String ShemHaSholeah { get; set; }
        public String OsekMoorshehHaSholeah { get; set; }
        public String CountryIDHaSholeah { get; set; }
        public DateTime LeTkufaMe { get; set; }
        public DateTime LeTkufaUd { get; set; }
        public String MisparProyect { get; set; }
    }
}

//כותרת תנועה		
		
//מספר פנימי		
//מספר מסמך		*
//תאריך מסמך		*
//תאריך קובע/דיווח		
//תאריך משלוח		
//תאריך אחר		
//שם הלקוח		*
//עוסק מורשה לקוח		
//מידע נוסף		*
//סכום לפני מע"מ		*
//סכום פטור ממע"מ		 
//סכום המע"מ		*
//סכום כולל מע"מ		*
