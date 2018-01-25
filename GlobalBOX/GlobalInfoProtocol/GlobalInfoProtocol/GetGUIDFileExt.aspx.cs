using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace GlobalInfoProtocol
{
    public partial class GetGUIDFileExt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String LoginKey = Request["LoginKey"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String TransactionGUID = Request["TransactionGUID"];

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((CountryID != null) && (CountryID != ""))
                {
                    if ((CompanyVAT != null) && (CompanyVAT != ""))
                    {
                        if ((TransactionGUID != null) && (TransactionGUID != ""))
                        {                            
                            string fileName = TransactionGUID + ".*";
                            String path = Server.MapPath(".") + "/TransientStorage/" + CountryID + "/" + CompanyVAT.Substring(0, 4) + "/" + CompanyVAT + "/";                            
                            DirectoryInfo di = new DirectoryInfo(path);
                            FileInfo [] fi =  di.GetFiles(fileName);
                            if (fi.Length > 0)
                            {                                
                                Response.Write(fi[0].Name);
                            }
                        }
                    }
                }
            }
        }
    }
}

//Server.MapPath specifies the relative or virtual path to map to a physical directory.

//•Server.MapPath(".") returns the current physical directory of the file (e.g. aspx) being executed
//•Server.MapPath("..") returns the parent directory
//•Server.MapPath("~") returns the physical path to the root of the application
//•Server.MapPath("/") returns the physical path to the root of the domain name (is not necessarily the same as the root of the application)
