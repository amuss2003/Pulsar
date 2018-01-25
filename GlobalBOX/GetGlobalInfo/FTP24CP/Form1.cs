//Author             : Mohammed Habeeb - habeeb_matrix@hotmail.com  
//Date Created       : 17th January 2007 
//Description        : This class demonstartes common FTP functionalities using .net 2.0.
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using CommandLine.Utility;

namespace FTP24CP
{
    //"/ftpServerIP:adirim.info" "/ftpUserID:adi" "/ftpPassword:9363" "/filename:{1ef5c476-4d9c-4bf6-93fc-62a36dc4963d}.pdf" "/targetfilename:{1ef5c476-4d9c-4bf6-93fc-62a36dc4963d}.pdf" "/cmd:upload" "/path:c:\temp/" "/targetpath:GlobalInfoTransfer/" "/countryid:117" "/companyvat:058005455"
    //"/ftpServerIP:adirim.info" "/ftpUserID:adi" "/ftpPassword:9363" "/filename:{1ef5c476-4d9c-4bf6-93fc-62a36dc4963d}.pdf" "/targetfilename:test.pdf" "/cmd:upload" "/path:c:\temp/" "/targetpath:GlobalInfoTransfer/" "/countryid:117" "/companyvat:058005455"
    //"/ftpServerIP:misradit.info" "/ftpUserID:misradit" "/ftpPassword:dfg23gd" "/filename:1001_YP0001.mdb" "/cmd:upload" "/path:c:\temp/" "/targetpath:../misradit.info/www/"
    //"/ftpServerIP:192.168.10.250" "/ftpUserID:adi" "/ftpPassword:9363" "/filename:1001_YP0001.mdb" "/cmd:upload" "/path:c:\temp/" "/targetpath:../TestFTP/"
    //"/ftpServerIP:misradit.info" "/ftpUserID:misradit" "/ftpPassword:dfg23gd" "/filename:1001_YP0001.mdb" "/cmd:upload" "/path:c:\temp/" "/targetpath:../misradit.info/www/"
    //global::FTP24CP.Properties.Resources.ui_progress_bar_1
    //"/ftpServerIP:adirim.info" "/ftpUserID:adi" "/ftpPassword:9363" "/filename:{9454a0f0-2841-4312-bef8-c4b869f52970}.pdf" "/targetfilename:{9454a0f0-2841-4312-bef8-c4b869f52970}.pdf" "/cmd:upload" "/path:c:\temp/" "/targetpath:GlobalInfoTransfer/" "/countryid:117" "/companyvat:058005455"
    public partial class Form1 : Form
    {
        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;
        private string[] args;
        //private string fullPath = "";
        public String command { get; set; }
        public String file_name { get; set; }
        public String fullPath { get; set; }
        public String targetPath { get; set; }
        public String targetfilename { get; set; }
        public String CompanyVAT { get; set; }
        public String CountryID { get; set; }
        public String server_url { get; set; }

        //global::FTP24CP.Properties.Resources.ui_progress_bar_1
        Icon[] progress_icons = new Icon[4];

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string[] args)
        {
            progress_icons[0] = ConvertFromBitmapToIcon(global::FTP24CP.Properties.Resources.ui_progress_bar_1);
            progress_icons[1] = ConvertFromBitmapToIcon(global::FTP24CP.Properties.Resources.ui_progress_bar_2);
            progress_icons[2] = ConvertFromBitmapToIcon(global::FTP24CP.Properties.Resources.ui_progress_bar_3);
            progress_icons[3] = ConvertFromBitmapToIcon(global::FTP24CP.Properties.Resources.ui_progress_bar_4);

            InitializeComponent();
            // TODO: Complete member initialization
            this.args = args;

            Arguments CommandLine = new Arguments(args);

            if (CommandLine["cmd"] != null)
                command = CommandLine["cmd"];

            if (CommandLine["filename"] != null)
                file_name = CommandLine["filename"];

            if (CommandLine["targetfilename"] != null)
                targetfilename = CommandLine["targetfilename"];

            if (CommandLine["path"] != null)
                fullPath = CommandLine["path"];

            if (CommandLine["ftpServerIP"] != null)
                ftpServerIP = CommandLine["ftpServerIP"];

            if (CommandLine["ftpUserID"] != null)
                ftpUserID = CommandLine["ftpUserID"];

            if (CommandLine["ftpPassword"] != null)
                ftpPassword = CommandLine["ftpPassword"];

            if (CommandLine["targetpath"] != null)
                targetPath = CommandLine["targetpath"];

            if (CommandLine["countryid"] != null)
                CountryID = CommandLine["countryid"];

            if (CommandLine["companyvat"] != null)
                CompanyVAT = CommandLine["companyvat"];
            
            //if (CommandLine["server_url"] != null)
            //    server_url = CommandLine["server_url"];

            if (targetfilename == null)
            {
                targetfilename = file_name;
            }

            //ftpServerIP = "192.168.10.250";
            //ftpUserID = "adi";
            //ftpPassword = "9363";

            this.Hide();

            txtServerIP.Text = ftpServerIP;
            txtUsername.Text = ftpUserID;
            txtPassword.Text = ftpPassword;
            this.Text += ftpServerIP;

            btnFTPSave.Enabled = false;

            if (command == "upload")
            {
                if (File.Exists(fullPath + file_name))
                {
                    Upload(fullPath, file_name, targetfilename);
                    CommandExecute(targetfilename, CountryID, CompanyVAT); //server_url,
                }
                else
                {
                    MessageBox.Show("File Not Found: " + Environment.NewLine + fullPath + file_name, "File Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (command == "download")
            {
                Download(fullPath, file_name);
            }

            if (args.Length == 0)
            {
                String Usage = "";
                Usage += "Usage:" + Environment.NewLine;
                Usage += "command: upload" + Environment.NewLine;
                Usage += "value: Full Path File Name" + Environment.NewLine;
                Usage += @"e.g: FTPTrans upload c:\FolderName\MyFileName.ext" + Environment.NewLine;
                Usage += @"Error Case: In the Path of the file name it will create 'MyFileName.ext.err'" + Environment.NewLine;
                Usage += @"the same file name with added extention 'err'" + Environment.NewLine;

                MessageBox.Show(Usage, "How To Use", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();

            //ftpServerIP = "192.168.10.250";
            //ftpUserID = "adi";
            //ftpPassword = "9363";
            //txtServerIP.Text = ftpServerIP;
            //txtUsername.Text = ftpUserID;
            //txtPassword.Text = ftpPassword;
            //this.Text += ftpServerIP;

            //btnFTPSave.Enabled = false;
        }

        //"/server_url:misradit.info" "/filename:1001_yp0025.mdb"
        //"/server_url:adirim.info" "/filename:1001_yp0025.mdb"
        /// <summary>
        /// Method to upload the specified file to the specified FTP Server
        /// </summary>
        /// <param name="filename">file full name to be uploaded</param>
        private void Upload(string fullPath, string filename, string targetfilename)
        {            
            String errFileName = fullPath + filename + ".err";
            if (File.Exists(errFileName))
            {
                File.Delete(errFileName);
            }

            FileInfo fileInf = new FileInfo(fullPath + filename);
            //string uri = "ftp://" + ftpServerIP + "/" + targetPath + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + targetPath + fileInf.Name));
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + targetPath + targetfilename));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();
            int progress_length = 0;
            //int counter = 0;

            notifyIcon1.BalloonTipTitle = "Transfer";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "Started";
            notifyIcon1.ShowBalloonTip(500);            

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);
                // Till Stream content ends
                while (contentLen != 0)
                {
                    //contentLen;
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    progress_length += contentLen;
                    String tmp = ((double)progress_length / (double)reqFTP.ContentLength * 100).ToString();

                    notifyIcon1.Text = ((int)Double.Parse(tmp)).ToString() + "%";
                    notifyIcon1.Icon = progress_icons[(((int)Double.Parse(tmp)) % 100) % 4];

                    //notifyIcon1.Icon = progress_icons[counter++ % 4];

                    if (((int)Double.Parse(tmp)) % 5 == 0)
                    {
                        if (((int)Double.Parse(tmp)) > 0)
                        {
                            notifyIcon1.BalloonTipTitle = "Transfer";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.BalloonTipText = new String(' ', 13) + notifyIcon1.Text;// +" progress";
                            notifyIcon1.ShowBalloonTip(500);
                        }
                    }
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();

                notifyIcon1.BalloonTipTitle = "Transfer";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipText = "Finished";
                notifyIcon1.ShowBalloonTip(500);
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(errFileName);
                sw.Close();
                //MessageBox.Show(ex.Message, "Upload Error");
            }
        }

        private void CommandExecute(String FileName, String CountryID, String CompanyVAT)
        {
            //SqlCommand = SqlCommand.Substring(0, SqlCommand.Length - 1);
            //String server_url, 
            //" + server_url + "
            try
            {
                //lblTableName.Text = SqlCommand;

                Application.DoEvents();
                //StreamWriter sr = new StreamWriter(Application.StartupPath + @"\ftp_url.txt");
                //sr.WriteLine("http://adirim.info/GlobalInfoProtocol/TransFTPToDB.aspx?FileName=" + FileName + "&CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT);
                //sr.Close();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://adirim.info/GlobalInfoProtocol/TransFTPToDB.aspx?FileName=" + FileName + "&CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT);
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

        public void DeleteFTP(string fileName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FTP 2.0 Delete");
            }
        }

        private string[] GetFilesDetailList()
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
                //MessageBox.Show(result.ToString().Split('\n'));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //MessageBox.Show(reader.ReadToEnd());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                //MessageBox.Show(response.StatusDescription);
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }
        private void Download(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                //filePath = <<The full path where the file is to be created.>>, 
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFilDlg = new OpenFileDialog();
            if (opFilDlg.ShowDialog() == DialogResult.OK)
            {
                Upload(fullPath, opFilDlg.FileName, targetfilename);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldDlg = new FolderBrowserDialog();
            if (txtUpload.Text.Trim().Length > 0)
            {
                if (fldDlg.ShowDialog() == DialogResult.OK)
                {
                    Download(fldDlg.SelectedPath, txtUpload.Text.Trim());
                }
            }
            else
            {
                MessageBox.Show("Please enter the File name to download");
            }
        }

        private void btnLstFiles_Click(object sender, EventArgs e)
        {
            string[] filenames = GetFileList();
            lstFiles.Items.Clear();
            foreach (string filename in filenames)
            {
                lstFiles.Items.Add(filename);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            OpenFileDialog fldDlg = new OpenFileDialog();
            if (txtUpload.Text.Trim().Length > 0)
            {
                DeleteFTP(txtUpload.Text.Trim());
            }
            else
            {
                MessageBox.Show("Please enter the File name to delete");
            }
        }

        private long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fileSize;
        }

        private void Rename(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName = name of the directory to create.
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnFileSize_Click(object sender, EventArgs e)
        {
            long size = GetFileSize(txtUpload.Text.Trim());
            MessageBox.Show(size.ToString() + " bytes");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rename(txtCurrentFilename.Text.Trim(), txtNewFilename.Text.Trim());
        }

        private void btnewDir_Click(object sender, EventArgs e)
        {
            MakeDir(txtNewDir.Text.Trim());
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnFTPSave.Enabled = true;
        }

        private void txtServerIP_TextChanged(object sender, EventArgs e)
        {
            btnFTPSave.Enabled = true;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            btnFTPSave.Enabled = true;
        }

        private void btnFTPSave_Click(object sender, EventArgs e)
        {
            ftpServerIP = txtServerIP.Text.Trim();
            ftpUserID = txtUsername.Text.Trim();
            ftpPassword = txtPassword.Text.Trim();
            btnFTPSave.Enabled = false;
        }

        private void btnFileDetailList_Click(object sender, EventArgs e)
        {
            string[] filenames = GetFilesDetailList();
            lstFiles.Items.Clear();
            foreach (string filename in filenames)
            {
                lstFiles.Items.Add(filename);
            }
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public static Bitmap ConvertFromIconToBitmap(Icon ic, Size sz)
        {

            Bitmap bmp = new Bitmap(sz.Width, sz.Height);
            using (Graphics gp = Graphics.FromImage(bmp))
            {
                gp.Clear(Color.Transparent);
                gp.DrawIcon(ic, new Rectangle(0, 0, sz.Width, sz.Height));
            }
            return bmp;
        }

        public static Icon ConvertFromBitmapToIcon(Bitmap bmp)
        {
            Icon ico = Icon.FromHandle(bmp.GetHicon());
            return ico;
        }

    }
}

//"/filename:1001_YP0001.mdb" "/cmd:upload" "/path:I:\Documents and Settings\yanivz\My Documents\Visual Studio 2010\Projects\FTPTrans\FTPTrans\bin\Debug\"

//Bitmap b = new Bitmap(@"d:\file.jpg");     Icon i = Icon.FromHandle(b.GetHicon()); 