using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol.Classes
{
    public class UploadFile
    {
        public bool Upload(HttpRequest httpRequest, Action<string> loggingAction, string serverPath, string transactionGUID)
        {
            string path = "";
            //Logger.AddToLogger(Server.MapPath("."), "Load TransFTPToDB");            
            

            foreach (string f in httpRequest.Files.AllKeys)
            {
                HttpPostedFile file = httpRequest.Files[f];

                if (serverPath.Substring(0, 1) != @"\")                
                    serverPath = serverPath + @"\";
                
                string countryID = httpRequest["CountryID"] ?? httpRequest["CountryIDTo"];
                string companyVAT = httpRequest["CompanyVAT"] ?? httpRequest["CompanyVATTo"];

                try
                {
                    path = serverPath + @"TransientStorage\" + countryID;                    
                    Directory.CreateDirectory(path);
                    Directory.CreateDirectory(path = path + @"\" + companyVAT.Substring(0, 4));
                    Directory.CreateDirectory(path = path + @"\" + companyVAT);
                    file.SaveAs(path = path + @"\" + transactionGUID + ".pdf");                    
                }
                catch (Exception ex)
                {
                    loggingAction("Failed to upload file to path: " + path);
                    loggingAction(ex.Message);
                    //Logger.AddToLogger(Server.MapPath("."), "Err TransFTPToDB: " + ex.Message);
                    return false;
                }
            }

            return true;
        }
    }
}