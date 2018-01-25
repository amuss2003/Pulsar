using System;
using System.Collections.Generic;
using System.Web;

namespace Pulsar
{
    public static class IsraeliID
    {
        public static bool IsValidID(String ID)
        {
            try
            {
                ID = ID.Trim();
                string txtId = ID.PadLeft(9, '0');

                int sum = 0;
                int[] arrIdDigits = new int[9];
                for (int i = 0; i < arrIdDigits.Length; i++)
                {
                    arrIdDigits[i] = int.Parse(ID[i].ToString());

                    if (i % 2 != 0)
                    {
                        arrIdDigits[i] *= 2;
                        if (arrIdDigits[i] > 9)
                        {
                            arrIdDigits[i] = (arrIdDigits[i] % 10) + 1;
                        }
                    }

                    sum += arrIdDigits[i];
                }

                sum = (sum % 10);
                if (sum == 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}