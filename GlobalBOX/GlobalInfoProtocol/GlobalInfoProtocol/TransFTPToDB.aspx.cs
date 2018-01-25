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
            Thread.Sleep(60000);
            
            Logger.AddToLogger(Server.MapPath("."), "Load TransFTPToDB");

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
            FileName = Path.GetFileName(FileName);
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
            startInfo.FileName = server_path + @"App_Data\move_ftp.bat";
            startInfo.WindowStyle = ProcessWindowStyle.Normal; //ProcessWindowStyle.Hidden;
            //startInfo.Arguments = "";
            //Logger.AddToLogger(Server.MapPath("."), "TransFTPToDB: " + startInfo.FileName);

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                //using (Process exeProcess = Process.Start(startInfo))
                //{
                //    exeProcess.WaitForExit();
                //    Logger.AddToLogger(Server.MapPath("."), "Exit TransFTPToDB: " + exeProcess.ExitCode);
                //}
                Logger.AddToLogger(Server.MapPath("."), "from: " + @"D:\FTP\ADI\GlobalInfoTransfer\" + FileName + "   to: " + server_path + @"TransientStorage\" + FileName);
                File.Move(@"D:\FTP\ADI\GlobalInfoTransfer\" + FileName, server_path + @"TransientStorage\" + FileName);
            }
            catch(Exception ex)
            {
                Logger.AddToLogger(Server.MapPath("."), "Err TransFTPToDB: " + ex.Message);
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
                Logger.AddToLogger(Server.MapPath("."), "moved TransFTPToDB" + server_path + @"TransientStorage\" + FileName);
                Logger.AddToLogger(Server.MapPath("."), "moved TransFTPToDB" + server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName);
                File.Move(server_path + @"TransientStorage\" + FileName, server_path + @"TransientStorage\" + CountryID + @"\" + CompanyVAT.Substring(0, 4) + @"\" + CompanyVAT + @"\" + FileName);
                Logger.AddToLogger(Server.MapPath("."), "moved TransFTPToDB" );
                Response.Write("moved");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Logger.AddToLogger(Server.MapPath("."), "Err TransFTPToDB: " + ex.Message);
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