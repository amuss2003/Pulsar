using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace GlobalInfoProtocol
{
    public partial class TransFTPToDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String server_path = Server.MapPath(".");
            if (server_path.Substring(0, 1) != @"\")
            {
                server_path = server_path + @"\";
            }

            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String FileName = Request["FileName"];
            //String FtpPath = @"e:\FTP\adi\GlobalInfoTransfer\";
            String FtpPath = @"T:\";

            //try
            //{
            //    if (File.Exists(server_path + @"app_data\" + FileName))
            //    {
            //        File.Delete(server_path + @"app_data\" + FileName);
            //        Response.Write("File.Delete(" + server_path + @"app_data\" + FileName);
            //        Response.Write("<br>");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Response.Write(ex.Message);
            //    //Response.Write("<br>");
            //}

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = server_path + "App_Data/move_ftp.bat";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = "";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }

            //Process.Start(server_path + "App_Data/move_ftp.bat");
            
            //ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");
            //startInfo.WindowStyle = ProcessWindowStyle.Normal;//ProcessWindowStyle.Minimized;
            ////startInfo.UseShellExecute = false;
            //Process.Start(startInfo);
            //startInfo.Arguments = " move " + FtpPath + FileName + ", " + server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName;
            //Process.Start(startInfo);
            try
            {
                Directory.CreateDirectory(server_path + @"TransientStorage\" + CountryID);
            }
            catch (Exception)
            {
            }
            try
            {
                Directory.CreateDirectory(server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4));
            }
            catch (Exception)
            {
            }
            try
            {
                Directory.CreateDirectory(server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT);
            }
            catch (Exception)
            {
            }

            //server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName
            //if (File.Exists(server_path + FileName))
            //if (File.Exists(FtpPath + FileName))
            //{
            //    //Response.Write("File.Copy(" + server_path + FileName + ", " + server_path + @"app_data\" + FileName + ", true)");
            //    //Response.Write("<br>");
            //    //File.Copy(server_path + FileName, server_path + @"app_data\" + FileName, true);
            try
            {
                Thread.Sleep(1000);
                File.Move(server_path + @"TransientStorage\" + FileName, server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName);
                Response.Write("moved");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //    //try
            //    //{
            //    //    if (File.Exists(server_path + @"app_data\" + FileName))
            //    //    {
            //    //        File.Delete(server_path + FileName);
            //    //        //Response.Write("File.Delete(" + server_path + FileName);
            //    //        //Response.Write("<br>");

            //    //    }
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    //Response.Write(ex.Message);
            //    //    //Response.Write("<br>");
            //    //}
            //}
            Response.Write("<br>");
            Response.Write(FtpPath + FileName);
            Response.Write("<br>");
            Response.Write(server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName);
            //Response.Write(File.Exists(FtpPath + FileName));
            //Response.Write(server_path);
            //Response.Write("<br>");
            //Response.Write(server_path + FileName);
            //Response.Write("<br>");
            //Response.Write(server_path + @"app_data\" + FileName);
            //Response.Write("<br>");


        }
    }
}