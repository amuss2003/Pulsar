﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace Pulsar.Classes
{
    public class Cryptographer 
    {
        private static byte[] Keys { get; set; } 
        
        public Cryptographer(string password)
        { 
            Keys = Encoding.ASCII.GetBytes(password); 
        }

        public void Encrypt(byte[] data)
        { 
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ Keys[i % Keys.Length]); 
            } 
        }

        public void Decrypt(byte[] data) 
        {
            for (int i = 0; i < data.Length; i++) 
            {
                data[i] = (byte)(Keys[i % Keys.Length] ^ data[i]); 
            } 
        } 
    }
}

//  1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679  
//  8214808651328230664709384460955058223172535940812848111745028410270193852110555964462294895493038196  
//  4428810975665933446128475648233786783165271201909145648566923460348610454326648213393607260249141273  
//  724587006606315588174881520920962829254091715364367892590360011330530548820466521384146951941511609
//  qBxHfihiSBNhhYy24_7IxCeOgTppYvH0PpHgtJQ1$35Q