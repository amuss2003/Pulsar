using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Pulsar
{

//כותרת תנועה		חד פעמי	KT
//תוכן	             	    רב פעמי 	TT
//נגדי	               	רב פעמי	        NG
//הערות	               	רב פעמי	        RM
//פרמטרים	            	הגדרה	    PR
//קבצים מצורפים		חד פעמי	        KM

    public class DataTypesList
    {
        public enum DataTypes
        {
            TT = 1,
            NG = 2,
            RM = 3,
            PR = 4,
            KM = 5
        }

        public static String[] strDataTypes = new String[] { "TT", "NG", "RM", "PR", "KM" };


    }
}
