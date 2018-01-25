using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace GlobalInfoProtocol.Classes
{
    public static class Logger
    {
        public static void AddToLogger(String path, String txt)
        {
            //string path = Server.MapPath(".") + @"/logger.txt";
            path += @"/logger.txt";
            // This text is added only once to the file. 
            if (!File.Exists(path))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(path))
                {
                    //sw.WriteLine(DateTime.Now + " ==> " + txt);
                    sw.WriteLine(DateTime.Now + " ==> Created.");
                }
            }

            // This text is always added, making the file longer over time 
            // if it is not deleted. 
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(txt);
                sw.WriteLine("");
            }
        }
    }
}