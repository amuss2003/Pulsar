using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Collections;
using DriveMapping;

namespace Pulsar
{
    public partial class frmCheckInfo : Form
    {
        private String m_ContractNumber = "";
        private String m_Path = "S:\\Archive\\";

        public string Path { get { return m_Path; } set { m_Path = value; } }

        public frmCheckInfo()
        {
            InitializeComponent();
        }

        private void frmCheckInfo_Load(object sender, EventArgs e)
        {
            m_LocalSIP = GetStoLocalIP();
            m_Path = m_LocalSIP + @"\Archive\";

            for (int i = 1; i <= 6; i++)
            {
                cmbSnif.Items.Add(i);
            }
            cmbSnif.Items.Add(9);
            
            for (int i = 1; i <= 6; i++)
            {
                cmbSnif.Items.Add(i + 1000);
            }
            cmbSnif.Items.Add(9 + 1000);

            cmbSnif.SelectedIndex = 0;
        }

        public String GetMyIP()
        {
            String strHostName = "";
            strHostName = Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);

            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                if (addr[i].ToString().IndexOf("192.168.") > -1)
                {
                    return addr[i].ToString();
                }
            }

            return "חיבור רשת";
        }

        private String GetStoLocalIP()
        {
            ArrayList all_drives = new ArrayList();

            IniFile ini = new IniFile(Application.StartupPath + @"\test.ini");
            Int32 NumOfSnifim = Int32.Parse(ini.IniReadValue("DriveIpMaps", "NumOfSnifim"));

            for (int i = 1; i <= NumOfSnifim; i++)
            {
                String stLine = ini.IniReadValue("DriveIpMaps", "Snif" + i);
                string[] words = stLine.Split(',');

                String stTempIP = words[0].Substring(2, words[0].IndexOf(@"\", 3) - 2);
                all_drives.Add(new DriveMap(words[0], words[1]));
            }

            return ConvertsToLocalIP(all_drives);
        }

        private String ConvertsToLocalIP(ArrayList drives)
        {
            String ip = GetMyIP();
            string[] words = ip.Split('.');
            String IP_Part = words[0] + "." + words[1] + "." + words[2] + ".";

            foreach (DriveMap drive_map in drives)
            {
                if (drive_map.drive_path.IndexOf(IP_Part) > -1)
                {
                    return drive_map.drive_path;
                }
            }
            return null;
        }

        private ArrayList m_MissinImagesToLoad = new ArrayList();

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckBox();
        }

        private void CheckBox()
        {
            String stExt = "-" + cmbSnif.Text + ".jpg";
            if (txtNumber.Text.Trim() != "")
            {
                btnSearchAll.Enabled = false;
                m_MissinImagesToLoad.Clear();

                picContract.Image = picScannerNormal.Image;
                picDamage.Image = picScannerNormal.Image;
                picInOutForm.Image = picScannerNormal.Image;
                picOrder.Image = picScannerNormal.Image;
                picShovar.Image = picScannerNormal.Image;
                picIssue.Image = picScannerNormal.Image;
                picInForm.Image = picScannerNormal.Image;

                Application.DoEvents();

                m_ContractNumber = txtNumber.Text.Trim();
                String FirstLetter = m_ContractNumber.Substring(0, 1);

                if (File.Exists(m_Path + "Contract\\" + cmbSnif.Text + @"\" + FirstLetter + @"\" + m_ContractNumber + stExt))
                {
                    LoadImage(picContract, m_Path + "Contract\\" + cmbSnif.Text + @"\" + FirstLetter + @"\" + m_ContractNumber + stExt);
                }
                else
                {
                    LoadImage(picContract, m_Path + "Contract\\" + m_ContractNumber + stExt);
                }

                LoadImage(picDamage, m_Path + "Damages\\" + m_ContractNumber + stExt);
                LoadImage(picInOutForm, m_Path + "InOutFrm\\" + m_ContractNumber + stExt);
                LoadImage(picInForm, m_Path + "InFrm\\" + m_ContractNumber + stExt);                
                LoadImage(picOrder, m_Path + "Hazmana\\" + m_ContractNumber + stExt);
                LoadImage(picShovar, m_Path + "Shovar\\" + m_ContractNumber + stExt);
                LoadImage(picIssue, m_Path + "Issue\\" + m_ContractNumber + stExt);

                btnSearchAll.Enabled = (m_MissinImagesToLoad.Count > 0);
            }
        }

        private void LoadImage(PictureBox pic ,string ImageFileName)
        {
            try
            {
                pic.Image = Image.FromFile(ImageFileName);
                pic.ImageLocation = ImageFileName;
            }
            catch (Exception)
            {
                m_MissinImagesToLoad.Add(ImageFileName);
                pic.Image = picScannerNone.Image;
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void picContract_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            frmZoom frm = new frmZoom();
            frm.SetImageFilePath(pic.ImageLocation);
            frm.SetZoomImage(pic);
            frm.ShowDialog();
        }

        private void picContract_Click(object sender, EventArgs e)
        {

        }

        private bool bLoading = false;

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            //CarCheck      ,Contract
            //CredCard      ,Damages
            //DoarRsum      ,Dohot
            //EMail         ,Fax
            //Hazmana       ,ID
            //InOutFrm      ,Invoice
            //Issue         ,Licences
            //Nilvim        ,Passport
            //Rentdirect     ,shovar

            bLoading = true;

            //CopyMissingFiles();
            CheckBox();

            bLoading = false;
        }

        private bool IsIPEnable(String IP)
        {
            String strHostName = "";
            strHostName = Dns.GetHostName();

            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            IP = IP.Substring(0, IP.LastIndexOf("."));

            for (int i = 0; i < addr.Length; i++)
            {
                String LocalIP = addr[i].ToString().Substring(0, addr[i].ToString().LastIndexOf("."));
                if (LocalIP == IP)
                {
                    return false;
                }
            }

            return true;
        }

        private String m_LocalSIP = null;

        //private void CopyMissingFiles()
        //{
        //    ArrayList drives = new ArrayList();
        //    ArrayList all_drives = new ArrayList();

        //    IniFile ini = new IniFile(Application.StartupPath + @"\test.ini");
        //    Int32 NumOfSnifim = Int32.Parse(ini.IniReadValue("DriveIpMaps", "NumOfSnifim"));

        //    for (int i = 1; i <= NumOfSnifim; i++)
        //    {
        //        String stLine = ini.IniReadValue("DriveIpMaps", "Snif" + i);
        //        string[] words = stLine.Split(',');

        //        String stTempIP = words[0].Substring(2, words[0].IndexOf(@"\", 3) - 2);
        //        all_drives.Add(new DriveMap(words[0], words[1]));

        //        if (IsIPEnable(stTempIP))
        //        {
        //            drives.Add(new DriveMap(words[0], words[1]));
        //        }
        //        //drives.Add(new DriveMap(@"\\192.168.0.250\PICTURES", "בני ברק"));
        //        Console.WriteLine("Snif{0}= {1}", i, stLine);
        //    }

        //    string local_drive_s_ip = ConvertsToLocalIP(all_drives);

        //    frmConnectiongTo frmconnectiongto = new frmConnectiongTo();
        //    frmconnectiongto.LabelInfo("Searching", "Waiting");
        //    //String tmpNetDriveLetter = "Q";

        //    foreach (DriveMap drive_map in drives)
        //    {
                
        //        frmconnectiongto.Snif = drive_map.snif;
        //        frmconnectiongto.Show();
        //        Application.DoEvents();

        //        //if (DriveSettings.IsDriveMapped(tmpNetDriveLetter))
        //        //{
        //        //    frmconnectiongto.LabelInfo("Disconnecting", "Disconnect Network Drive");
        //        //    DriveSettings.DisconnectNetworkDrive(tmpNetDriveLetter, true);
        //        //}

        //        //frmconnectiongto.LabelInfo("Maping", "Map Network Drive");
        //        //DriveSettings.MapNetworkDrive(tmpNetDriveLetter, drive_map.drive_path);
        //        //Application.DoEvents();

        //        foreach (String file in m_MissinImagesToLoad)
        //        {
        //            Application.DoEvents();
        //            //String drive_path = file.Replace("S:", tmpNetDriveLetter + ":");
        //            String drive_path = file.Replace(m_LocalSIP, drive_map.drive_path);

        //            if (!File.Exists(file))
        //            {
        //                frmconnectiongto.LabelInfo("Searching", drive_path);
        //                Application.DoEvents();
        //                if (File.Exists(drive_path))
        //                {
        //                    Application.DoEvents();
        //                    frmconnectiongto.LabelInfo("Copy File", drive_path + Environment.NewLine + file);
        //                    Application.DoEvents();
        //                    File.Copy(drive_path, file);
        //                    Application.DoEvents();
        //                    frmconnectiongto.LabelInfo("FinishCopy File", drive_path);
        //                    Application.DoEvents();
        //                }
        //            }
        //        }

        //        //String drive_path = full_path.Replace("S:", "Q:");

        //        //if (File.Exists(drive_path))
        //        //{
        //        //    //frmconnectiongto.Hide();
        //        //    Application.DoEvents();

        //        //    //File.Copy(
        //        //    //frmAutoSync frm = new frmAutoSync();
        //        //    //frm.SourceFolder = drive_path;
        //        //    //frm.DestinationFolder = full_path;
        //        //    //frm.Snif = drive_map.snif;
        //        //    //frm.ShowDialog();
        //        //}

        //        //if (Directory.GetFiles(full_path).Length > 2)
        //        //{
        //        //    break;
        //        //}
        //    }

        //    //if (DriveSettings.IsDriveMapped(tmpNetDriveLetter))
        //    //{
        //    //    frmconnectiongto.LabelInfo("Disconnecting", "Disconnect Network Drive");
        //    //    DriveSettings.DisconnectNetworkDrive(tmpNetDriveLetter, true);
        //    //}

        //    btnSearchAll.Enabled = false;
        //    Application.DoEvents();
        //    frmconnectiongto.Close();
        //}

        private void chkInForm_CheckedChanged(object sender, EventArgs e)
        {
            picInForm.Visible = chkInForm.Checked;
            picInOutForm.Visible = !chkInForm.Checked;
        }
    }

    public class DriveMap
    {
        public String drive_path = "";
        public String snif = "";

        public DriveMap(String _drive_path, String _snif)
        {
            drive_path = _drive_path;
            snif = _snif;
        }
    }
}