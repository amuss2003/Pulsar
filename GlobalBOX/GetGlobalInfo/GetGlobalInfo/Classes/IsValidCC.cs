using System;
using System.Collections.Generic;
using System.Web;

namespace Pulsar
{
    public static class CCValidation
    {
        //public static bool isLuhnValid(String s)
        //{
        //    //return s.Reverse().SelectMany((c, i) => ((c - '0') << (i & 1)).ToString()).Sum(c => (c - '0')) % 10 == 0;
        //    return false;
        //}

        public static bool IsValidCC(String number)
        {

            int[][] sumTable = new int[][] { new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new int[] { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 } };
            int sum = 0, flip = 0;

            for (int i = number.Length - 1; i >= 0; i--)
            {
                int row_number = flip++ & 0x1;
                int column_number = Convert.ToInt32(Convert.ToChar(number[i]).ToString());

                sum += sumTable[row_number][column_number];
            }
            return sum % 10 == 0;
        }

        private static bool IsValidNumber(string number)
        {
            int[] DELTAS = new int[] { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };
            int checksum = 0; 
            char[] chars = number.ToCharArray();

            for (int i = chars.Length - 1; i > -1; i--) 
            {
                int j = ((int)chars[i]) - 48; 
                checksum += j; 

                if (((i - chars.Length) % 2) == 0)  
                    checksum += DELTAS[j]; 
            } 
            return ((checksum % 10) == 0);
        }

        public static bool IsValidLuhnAlgorithm(String cc)
        {
            byte[] number = System.Text.Encoding.UTF8.GetBytes(cc.Trim());
            //byte[] number = new byte[16]; // number to validate

            // Remove non-digits
            int len = number.Length; 

            // Use Luhn Algorithm to validate 
            int sum = 0;
            for (int i = len - 1; i >= 0; i--)
            {
                if (i % 2 == len % 2)
                {
                    int n = number[i] * 2;
                    sum += (n / 10) + (n % 10);
                }
                else
                    sum += number[i];
            }

            return (sum % 10 == 0);
        }
    }
}

