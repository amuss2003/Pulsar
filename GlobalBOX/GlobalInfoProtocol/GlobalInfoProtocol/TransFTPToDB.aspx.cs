using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;
using System.Threading;
using GlobalInfoProtocol.Classes;


namespace GlobalInfoProtocol
{
    public partial class TransFTPToDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.AddToLogger(Server.MapPath("."), "Load TransFTPToDB");

            foreach (string f in Request.Files.AllKeys)
            {
                HttpPostedFile file = Request.Files[f];

                String server_path = Server.MapPath(".");
                if (server_path.Substring(0, 1) != @"\")
                {
                    server_path = server_path + @"\";
                }

                String CountryID = Request["CountryID"];
                String CompanyVAT = Request["CompanyVAT"];
                String FileName = Request["FileName"];                
                
                FileName = Path.GetFileName(FileName);
               
                try
                {
                    Directory.CreateDirectory(server_path + @"TransientStorage\" + CountryID);
                }
                catch (Exception ex)
                {
                    Logger.AddToLogger(Server.MapPath("."), "Err TransFTPToDB: " + ex.Message);

                }
                try
                {
                    Directory.CreateDirectory(server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4));
                }
                catch (Exception ex)
                {
                    Logger.AddToLogger(Server.MapPath("."), "Err TransFTPToDB: " + ex.Message);

                }
                try
                {
                    Directory.CreateDirectory(server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT);
                }
                catch (Exception ex)
                {
                    Logger.AddToLogger(Server.MapPath("."), "Err TransFTPToDB: " + ex.Message);
                }

                try
                {
                    Logger.AddToLogger(Server.MapPath("."), "moved TransFTPToDB" + server_path + @"TransientStorage\" + FileName);
                    Logger.AddToLogger(Server.MapPath("."), "moved TransFTPToDB" + server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName);
                    //File.Move(server_path + @"TransientStorage\" + FileName, server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName);
                    file.SaveAs(server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + file.FileName);
                    Logger.AddToLogger(Server.MapPath("."), "moved TransFTPToDB");
                    Response.Write("moved");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    Logger.AddToLogger(Server.MapPath("."), "Err TransFTPToDB: " + ex.Message);
                }

                Response.Write("<br>");
                Response.Write(server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName);            }

        }
    }
}