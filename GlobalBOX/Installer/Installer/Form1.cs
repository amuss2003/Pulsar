using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using Microsoft.Win32;
//msiexec.exe /x {6BF4A30B-566A-4C42-8652-4208D5417395} /qn 
namespace Installer
{
    public partial class Form1 : Form
    {
        Stopwatch stopwatch = new Stopwatch();

        private Process mv_prcInstaller = new Process();
        private string[] args;

        public Form1(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblElpesedTime.Text = String.Format("{0:00}", stopwatch.Elapsed.Hours) + ":" + String.Format("{0:00}", stopwatch.Elapsed.Minutes) + ":" + String.Format("{0:00}", stopwatch.Elapsed.Seconds);
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Install();
            // Stop timing
            this.Close();
        }

        private void Install()
        {

            //if (args[1] == "/i")
            //{
            //    //Sentence to install -> /i
            //    manipulateSoftware("/i");
            //}
            //else if (args[1] == "/x")
            //{
            //    //Sentence to uninstall -> /x
            //    manipulateSoftware("/x");
            //}
            //else if (args[1] == "/r")
            //{
                //Sentence to uninstall -> /x
                //manipulateSoftware("/x");
            uninstall("/x", GetAddRemovePrograms());
            manipulateSoftware("/i");
            //}

            mv_prcInstaller = new Process();
            mv_prcInstaller.StartInfo.FileName = Application.StartupPath.ToLower().Replace(@"\app_data", "") + @"\GetGlobalInfo.exe";
            //MessageBox.Show(mv_prcInstaller.StartInfo.FileName);
            mv_prcInstaller.Start();
            stopwatch.Stop();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            this.Show();
            Application.DoEvents();
            stopwatch.Start();
            //http://www.simplicitools.com/SimpliciTools/FileDownload2.ashx?filename=CMailBox.msi&ProductID=2
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri("http://www.simplicitools.com/SimpliciTools/FileDownload2.ashx?filename=CMailBox.msi&ProductID=2"), System.Windows.Forms.Application.StartupPath + @"\CMailBox.msi");            
        }

        private string GetAddRemovePrograms()
        {
            try
            {
                RegistryKey myUninstallKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
                string[] mySubKeyNames = myUninstallKey.GetSubKeyNames();
                for (int i = 0; i < mySubKeyNames.Length; i++)
                {
                    RegistryKey myKey = myUninstallKey.OpenSubKey(mySubKeyNames[i], true);
                    object myValue = myKey.GetValue("DisplayName");
                    if (myValue != null && myValue.ToString() == "CMailBox")
                    {
                        //MsiExec.exe /I{6BF4A30B-566A-4C42-8652-4208D5417395}
                        string myValueUninstall = (string)myKey.GetValue("UninstallString");
                        myValueUninstall = myValueUninstall.Replace("MsiExec.exe /I", "");
                        return myValueUninstall;
                        //myKey.SetValue("DisplayIcon", iconSourcePath);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }
        
        private void uninstall(string p_strAccion, string guid)
        {
            //The Argument will be like this
            mv_prcInstaller.StartInfo.FileName = Application.StartupPath + @"\msiexec.exe";
            mv_prcInstaller.StartInfo.Arguments = " " + p_strAccion + " \"" + guid + "\"" + "/qn";
            //MessageBox.Show(mv_prcInstaller.StartInfo.FileName + " " + mv_prcInstaller.StartInfo.Arguments);
            mv_prcInstaller.Start();
            mv_prcInstaller.WaitForExit();
        }  

        private void manipulateSoftware(string p_strAccion)
        {
            //The Argument will be like this
            mv_prcInstaller.StartInfo.FileName = Application.StartupPath + @"\msiexec.exe";
            //mv_prcInstaller.StartInfo.Arguments = " " + p_strAccion + " \"" + args[0] + "\"" + "/qn";            
            string st = Application.StartupPath + @"\CMailBox.msi";
            mv_prcInstaller.StartInfo.Arguments = " " + p_strAccion + " \"" + st + "\"" + " TARGETDIR=\"" + Application.StartupPath.ToLower().Replace(@"\app_data", "") + "\"" + " /qn";
            //MessageBox.Show(mv_prcInstaller.StartInfo.FileName + Environment.NewLine + mv_prcInstaller.StartInfo.Arguments);
            mv_prcInstaller.Start();
            mv_prcInstaller.WaitForExit();
        }        
    }
}
