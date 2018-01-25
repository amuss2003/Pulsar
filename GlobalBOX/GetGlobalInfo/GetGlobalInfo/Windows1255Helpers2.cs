using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VegasCommands
{    
    /// <summary>
    /// This class decodes windows-1255 encoded strings
    /// </summary>
    public class Windows1255Helpers
    {
        private enum EncodingType { Base64, Normal };

        public static readonly string lookFor = "=?windows-1255?";//B

        private EncodingType encodingType;

        public string Text { get; private set; }

        public bool IsBase64 { get { return encodingType == EncodingType.Base64; } }

        /// <summary>
        /// This constructor takes the encoded string as parameter        
        /// </summary>
        /// <param name="text">Text to decode</param>       
        public Windows1255Helpers(string text)
        {
            Text = text;
            firstAnalysis();
        }

        private void firstAnalysis()
        {
            //in the windows-1255 encoding - 
            //The header (windows-1255) should be followed by one the following 
            //?B? - for base 64 encoding
            //?Q? - for normal text encoding (spaces are encoded as _)
            //if (!Text.Contains(lookFor))
            //    throw new ArgumentException("Text should be encoded with windows 1255");
            //if (Text.Contains(lookFor + "B"))
            //    encodingType = EncodingType.Base64;
            //else
                encodingType = EncodingType.Normal;
        }

        /// <summary>
        /// use this method to decode the Text
        /// </summary>
        /// <returns>Decoded Text</returns>
        public string Decode()
        {
            Regex windowsPattern = new Regex(@"=(\?)(windows-1255)(\?)[QB](\?)[^\?]*(\?)(=)?");
            
            string newString = Text;
            Match item = windowsPattern.Match(newString);
            while (item.Success)
            {
                //encoding character is on index 15 - Q or B
                switch (item.Value[15])
                {
                    case 'B':
                        newString = newString.Remove(item.Index, item.Length).Insert(item.Index, base64Decoding(item.Value));
                        break;
                    default:
                        newString = newString.Remove(item.Index, item.Length).Insert(item.Index, normalDecoding(item.Value));
                        break;
                }
                item = windowsPattern.Match(newString);
            }
            return newString;
        }

        private string normalDecoding(string substring)
        {
            string ret = stripEncodingTrail(substring);
            return ret.Replace("_", " ");
        }

        private string stripEncodingTrail(string text)
        {
            string ret = text.Substring(text.IndexOf(lookFor) + 15);
            int start = ret.IndexOf('?') + 1;
            int end = ret.IndexOf('?', start + 1);
            return ret.Substring(start, end - start);
        }

        private string base64Decoding(string substring)
        {
            string ret = stripEncodingTrail(substring);
            var tempResult = Convert.FromBase64String(ret);
            StringBuilder subject = new StringBuilder();
            foreach (var num in tempResult)
            {
                var nnum = (num >= 0xE0 && num <= 0xFA) ? num + 0x4F0 : num;
                subject.Append((char)(nnum));
            }
            ret = subject.ToString();
            return ret;
        }

        public static bool IsWindows1255(string text)
        {
            return text.Contains(lookFor);
        }
    }
}
