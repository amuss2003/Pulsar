using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommandLine.Utility;
using System.Net;
using System.IO;

namespace TransFtpToDB
{
    public partial class frmTransFtpToDB : Form
    {
        private String FileName = "";
        public String server_url { get; set; }
        public String CompanyVAT { get; set; }
        public String CountryID { get; set; }

        public frmTransFtpToDB(string [] args)
        {
            Arguments commandLine = new Arguments(args);

            InitializeComponent();

            if (commandLine["countryid"] != null)
                CountryID = commandLine["countryid"];

            if (commandLine["companyvat"] != null)
                CompanyVAT = commandLine["companyvat"];

            if (commandLine["filename"] != null)
                FileName = commandLine["filename"];

            if (commandLine["server_url"] != null)
                server_url = commandLine["server_url"];

            if (FileName.Trim() != "")
            {
                //http://192.168.10.250/Adirim/TransFTPToDB.aspx?FileName=1000_yp0117.mdb
                //http://misradit.info/TransFTPToDB.aspx?FileName=1002_yp0117.mdb
                CommandExecute(server_url, FileName, CountryID, CompanyVAT);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CommandExecute(String URL, String FileName, String CountryID, String CompanyVAT)
        {
            //SqlCommand = SqlCommand.Substring(0, SqlCommand.Length - 1);

            try
            {
                //lblTableName.Text = SqlCommand;

                Application.DoEvents();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + server_url + "/TransFTPToDB.aspx?FileName=" + FileName + "&CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT);
                // Set some reasonable limits on resources used by this request
                request.MaximumAutomaticRedirections = 4;
                //request.MaximumResponseHeadersLength = 4;
                // Set credentials to use for this request.
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Console.WriteLine("Content length is {0}", response.ContentLength);
                Console.WriteLine("Content type is {0}", response.ContentType);

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                Console.WriteLine("Response stream received.");
                //int number_of_orders = int.Parse(readStream.ReadLine());             
                Application.DoEvents();

                response.Close();
                readStream.Close();

                //Encoding ecp28598 = Encoding.GetEncoding(28598);
                //StreamWriter sr28598 = new StreamWriter(SqlCommand + @"\" + TableName + "_" + TimeStamp + ".txt", false, ecp28598);
                //sr28598.Write(AllOrders);
                //sr28598.Close();

                //StreamWriter sw = new StreamWriter(DownloadFolderPath + @"\" + TableName + "_" + TimeStamp + ".txt");
                //sw.Write(AllOrders);
                //sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
