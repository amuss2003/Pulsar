using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CommandLine.Utility;
//"/countryid:972"
//"/companyvat:513638346"
//"/file:I:\Documents and Settings\yanivz\Desktop\pervasive_sqlref.pdf"

namespace TestUploader
{
    /// <summary>
    /// A test form used to upload a file from a windows application using
    /// the Uploader Web Service
    /// </summary>
    public partial class Form1 : Form
    {
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
            Arguments CommandLine = new Arguments(args);

            //if (CommandLine["url"] != null)
            //    URL = CommandLine["url"];

            if (CommandLine["filename"] != null)
                FileName = CommandLine["filename"];

            //if (CommandLine["targetpath"] != null)
            //    TargetPath = CommandLine["targetpath"];

            if (CommandLine["countryid"] != null)
                CountryID = CommandLine["countryid"];

            if (CommandLine["companyvat"] != null)
                CompanyVAT = CommandLine["companyvat"];

            if (args.Length == 3)
            {
                UploadFile(FileName);
                //this.Close();
            }

            // do nothing
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.BalloonTipTitle = "Uploading Data";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.Visible = true;
            this.Close();
        }

        /// <summary>
        /// Upload any file to the web service; this function may be
        /// used in any application where it is necessary to upload
        /// a file through a web service
        /// </summary>
        /// <param name="filename">Pass the file path to upload</param>
        private void UploadFile(string filename)
        {
            try
            {
                tmrUpload.Enabled = true;
                notifyIcon1.BalloonTipText = propelor[0];
                notifyIcon1.ShowBalloonTip(1000);
                Application.DoEvents();
                // get the exact file name from the path
                String strFile = System.IO.Path.GetFileName(filename);

                // create an instance fo the web service
                TestUploader.Uploader.FileUploader srv = new TestUploader.Uploader.FileUploader();
               
                // get the file information form the selected file
                FileInfo fInfo = new FileInfo(filename);

                // get the length of the file to see if it is possible
                // to upload it (with the standard 4 MB limit)
                long numBytes = fInfo.Length;
                double dLen = Convert.ToDouble(fInfo.Length / 1000000);

                // Default limit of 4 MB on web server
                // have to change the web.config to if
                // you want to allow larger uploads
                if (dLen < 10)
                {
                    // set up a file stream and binary reader for the 
                    // selected file
                    FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);

                    // convert the file to a byte array
                    byte[] data = br.ReadBytes((int)numBytes);
                    br.Close();

                    // pass the byte array (file) and file name to the web service
                    string sTmp = srv.UploadFile(data, strFile, CountryID, CompanyVAT); //"972", "513638346"
                    fStream.Close();
                    fStream.Dispose();
                    
                    tmrUpload.Enabled = false;

                    // this will always say OK unless an error occurs,
                    // if an error occurs, the service returns the error message
                    //MessageBox.Show("File Upload Status: " + sTmp, "File Upload");
                    notifyIcon1.BalloonTipText = "File Upload Status: " + sTmp;
                    notifyIcon1.ShowBalloonTip(1000);
                    Application.DoEvents();                    
                }
                else
                {
                    notifyIcon1.BalloonTipText = "The file selected exceeds the size limit for uploads.";
                    notifyIcon1.ShowBalloonTip(1000);
                    Application.DoEvents();
                    // Display message if the file was too large to upload
                    //MessageBox.Show("The file selected exceeds the size limit for uploads.", "File Size");
                }
            }
            catch (Exception ex)
            {
                // display an error message to the user
                //MessageBox.Show(ex.Message.ToString(), "Upload Error");
                notifyIcon1.BalloonTipText = ex.Message.ToString();
                notifyIcon1.ShowBalloonTip(1000);
                Application.DoEvents();
            }
            tmrUpload.Enabled = false;
        }


        /// <summary>
        /// Allow the user to browse for a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open File";
            openFileDialog1.Filter = "All Files|*.*";
            openFileDialog1.FileName = "";

            try
            {
                openFileDialog1.InitialDirectory = "C:\\Temp";
            }
            catch
            {
                // skip it 
            }

            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == "")
                return;
            else
                txtFileName.Text = openFileDialog1.FileName;

        }



        /// <summary>
        /// If the user has selected a file, send it to the upload method, 
        /// the upload method will convert the file to a byte array and
        /// send it through the web service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtFileName.Text != string.Empty)
                UploadFile(txtFileName.Text);
            else
                MessageBox.Show("You must select a file first.", "No File Selected");
        }

        public int Index { get; set; }
        String[] propelor = new String[] { "|", "/", "-", "\\" };
        private string[] args;

        private void tmrUpload_Tick(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipText = propelor[Index];
            notifyIcon1.ShowBalloonTip(1000);
            Index++;
            if (Index == 4)
            {
                Index = 0;
            }
        }
    }
}