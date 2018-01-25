using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.IO;

namespace Uploader
{
    /// <summary>
    /// This web method will provide an web method to load any
    /// file onto the server; the UploadFile web method
    /// will accept the report and store it in the local file system.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class FileUploader : System.Web.Services.WebService
    {
        [WebMethod]
        public string UploadFile(byte[] f, string fileName, string CountryID, string CompanyVAT)
        {
            String ServerPath = System.Web.Hosting.HostingEnvironment.MapPath("~/TransientStorage/");

            // the byte array argument contains the content of the file
            // the string argument contains the name and extension
            // of the file passed in the byte array
            try
            {
                if (CountryID != null)
                {
                    if (!Directory.Exists(ServerPath + CountryID))
                    {
                        Directory.CreateDirectory(ServerPath + CountryID);
                    }
                }

                if (CompanyVAT != null)
                {
                    if (!Directory.Exists(ServerPath + CountryID + "/" + CompanyVAT.Substring(0, 4)))
                    {
                        Directory.CreateDirectory(ServerPath + CountryID + "/" + CompanyVAT.Substring(0, 4));
                    }
                    
                    if (!Directory.Exists(ServerPath + CountryID + "/" + CompanyVAT.Substring(0, 4) + "/" + CompanyVAT))
                    {
                        Directory.CreateDirectory(ServerPath + CountryID + "/" + CompanyVAT.Substring(0, 4) + "/" + CompanyVAT);
                    }
                }

                // instance a memory stream and pass the
                // byte array to its constructor
                MemoryStream ms = new MemoryStream(f);

                // instance a filestream pointing to the 
                // storage folder, use the original file name
                // to name the resulting file
                FileStream fs = new FileStream(ServerPath + CountryID + "/" + CompanyVAT.Substring(0, 4) + "/" + CompanyVAT + "/" + fileName, FileMode.Create);

                // write the memory stream containing the original
                // file as a byte array to the filestream
                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();

                // return OK if we made it this far
                return "OK";
            }
            catch (Exception ex)
            {
                // return the error message if the operation fails
                return ex.Message.ToString();
            }
        }
    }
}
