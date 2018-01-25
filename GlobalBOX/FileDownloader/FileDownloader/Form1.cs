using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using CommandLine.Utility;
//using System.Net.WebClient.Download;
//"/url:http://www.misradit.info/Images/" 
//"/filename:c:\API SMS DIRECT VER 2.5.pdf"
//"/CountryID:972"
//"/CompanyVAT:511364889"
//"/TargetPath:c:\\"
//http://www.boi.org.il/he/Markets/Documents/yazdayrr.xls
//"/url:http://www.misradit.info/Images/" "/filename:API SMS DIRECT VER 2.5.pdf" "/CountryID:972" "/CompanyVAT:511364889" "/TargetPath:c:\\"
namespace FileDownloader
{
    public partial class Form1 : Form
    {
        private string[] args;
        public string FileName { get; set; }
        public String URL { get; set; }
        public String CountryID { get; set; }
        public String CompanyVAT { get; set; }
        public String TargetPath { get; set; }

        public Form1(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.BalloonTipTitle = "Downloading Data";
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "0";
            notifyIcon1.ShowBalloonTip(1000);
            
            Arguments CommandLine = new Arguments(args);

            if (CommandLine["url"] != null)
                URL = CommandLine["url"];

            if (CommandLine["filename"] != null)
                FileName = CommandLine["filename"];

            if (CommandLine["targetpath"] != null)
                TargetPath = CommandLine["targetpath"];

            //if (CommandLine["countryid"] != null)
            //    CountryID = CommandLine["countryid"];

            //if (CommandLine["companyvat"] != null)
            //    CompanyVAT = CommandLine["companyvat"];

            if (args.Length == 3) //5
            {
                DownloadFile(FileName);
            }

            if (args.Length == 5) //5
            {
                DownloadFile(FileName, CountryID, CompanyVAT);
            }
        }

        private void DownloadFile(String FileName)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(URL + FileName), TargetPath + FileName);
        }

        private void DownloadFile(String FileName, String CountryID, String CompanyVAT)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(URL + FileName), TargetPath + FileName);
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {            
            progressBar1.Value = e.ProgressPercentage;
            notifyIcon1.BalloonTipText = progressBar1.Value.ToString() + "%";
            notifyIcon1.ShowBalloonTip(1000);
            Application.DoEvents();
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            this.Close();
        }
    }
}


//using (WebClient Client = new WebClient()) { Client.DownloadFile("http://www.abc.com/file/song/a.mpeg", "a.mpeg"); } 

//string remoteUri = "http://www.contoso.com/library/homepage/images/";
//string fileName = "ms-banner.gif", myStringWebResource = null;
//// Create a new WebClient instance. WebClient myWebClient = new WebClient();
//myStringWebResource = remoteUri + fileName;
//// Download the Web resource and save it into the current filesystem folder.
//myWebClient.DownloadFile(myStringWebResource,fileName); 

//using System.Net;

//WebClient webClient = new WebClient();
//webClient.DownloadFile("http://mysite.com/myfile.txt", @"c:\myfile.txt");