using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Pulsar.Classes;
using System.Runtime.InteropServices;
using CommandLine.Utility;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using Pulsar;
using System.Collections;
using System.Globalization;
//using Proshot.UtilityLib.CommonDialogs;
using Microsoft.Win32;
//using BalloonCS;
using System.Security.Cryptography;
using Microsoft.Office.Interop.Outlook;
using Pulsar.Forms;
using System.Reflection;
using System.Diagnostics;
//"/countryid:117" "/companyvat:513638346" "/importoutbox:X:\ADIRIM\EXPORT\cmailcmd.txt" "/importdelimiter:|" "/ImportEncoding:iso-8859-8"
//"/countryid:117" "/companyvat:513638346" "/importoutbox:X:\ADIRIM\EXPORT\cmailcmd.txt" "/importdelimiter:|" "/ImportEncoding:DOS-862"
//"/countryid:117" "/companyvat:513638346" "/importoutbox:Z:\ADIRIM\EXPORT\doclist.txt" "/importdelimiter:|" "/ImportEncoding:DOS-862"

namespace Pulsar
{
    public partial class frmMain : Form
    {
        //private HoverBalloon m_hb = new HoverBalloon();
        //private MessageBalloon m_mb;
        public CompanyInfo Company_Info { get; set; }
        public String SqlCommand { get; set; }
        public string[] args = null;
        public bool bSucsess = true;
        public String KEY { get; set; }

        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            int nShowCmd);

        public String CountryID { get; set; }
        public String CompanyVAT { get; set; }
        public String NineDigits { get; set; }
        public String TargetFileName { get; set; }
        public String SendOutBoxData { get; set; }
        public String ImportOutbox { get; set; }
        public String ImportDelimiter { get; set; }
        public String ImportCompanies { get; set; }
        public String ImportEncoding { get; set; }

        public bool bCommandLineExecution { get; set; }

        public DBLayer dblayer = null;

        public enum form_mode_type
        {
            AutoLogin = 1,
            Selection = 2
        }

        public form_mode_type form_mode { get; set; }

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(string[] _args)
        {
            FirstRun();

            DateTime dt1 = new DateTime(1900, 1, 1);
            //double number = (DateTime.Now - dt1).TotalDays;
            InitializeComponent();
            serverStatus1.parser = parser;

            args = _args;
            // TODO: Complete member initialization            
            Arguments CommandLine = new Arguments(args);

            if (CommandLine["countryid"] != null)
                CountryID = CommandLine["countryid"];

            if (CommandLine["companyvat"] != null)
                CompanyVAT = CommandLine["companyvat"];

            if (CommandLine["ninedigits"] != null)
                NineDigits = CommandLine["ninedigits"];

            if (CommandLine["targetfilename"] != null)
                TargetFileName = CommandLine["targetfilename"];

            if (CommandLine["sendoutboxdata"] != null)
                SendOutBoxData = CommandLine["sendoutboxdata"];

            if (CommandLine["importoutbox"] != null)
                ImportOutbox = CommandLine["importoutbox"];

            if (CommandLine["importdelimiter"] != null)
                ImportDelimiter = CommandLine["importdelimiter"];

            if (CommandLine["importcompanies"] != null)
                ImportCompanies = CommandLine["importcompanies"];

            if (CommandLine["importencoding"] != null)
                ImportEncoding = CommandLine["importencoding"];

            bCommandLineExecution = args.Length > 0;
        }

        private static void FirstRun()
        {
            String start_path = System.Windows.Forms.Application.StartupPath;
            if (!Directory.Exists(start_path + @"\App_Data"))
            {
                //MessageBox.Show("Directory.CreateDirectory");
                Directory.CreateDirectory(start_path + @"\App_Data");
                if (File.Exists(start_path + @"\LocalInfoProtocol.mdb"))
                {
                    //MessageBox.Show("File.Move");
                    File.Move(start_path + @"\LocalInfoProtocol.mdb", start_path + @"\App_Data\LocalInfoProtocol.mdb");
                }

                if (File.Exists(start_path + @"\Installer.exe"))
                {
                    //MessageBox.Show("File.Move");
                    File.Move(start_path + @"\Installer.exe", start_path + @"\App_Data\Installer.exe");
                }

                if (File.Exists(start_path + @"\msiexec.exe"))
                {
                    //MessageBox.Show("File.Move");
                    File.Copy(start_path + @"\msiexec.exe", start_path + @"\App_Data\msiexec.exe");
                }
            }
            else
            {
                if (File.Exists(start_path + @"\LocalInfoProtocol.mdb"))
                {
                    if (File.Exists(start_path + @"\App_Data\LocalInfoProtocol.mdb"))
                    {
                        DBLayer dblayer1 = new DBLayer(start_path+ @"\");
                        int db1Ver = dblayer1.GetDBVersion();

                        DBLayer dblayer2 = new DBLayer(start_path + @"\App_Data\");
                        int db2Ver = dblayer2.GetDBVersion();
                        //MessageBox.Show("db1Ver = " + db1Ver + ", db2Ver = " + db2Ver);
                        if (db1Ver != db2Ver)
                        {
                            DBCopy.copy(start_path + @"\App_Data\LocalInfoProtocol.mdb", start_path + @"\LocalInfoProtocol.mdb");
                            //MessageBox.Show(@"\App_Data\LocalInfoProtocol.mdb" + ", " + start_path + start_path + @"\LocalInfoProtocol.mdb");
                            try
                            {
                                File.Delete(start_path + @"\App_Data\LocalInfoProtocol.mdb");
                                File.Move(start_path + @"\LocalInfoProtocol.mdb", start_path + @"\App_Data\LocalInfoProtocol.mdb");
                            }
                            catch (System.Exception ex)
                            {
                            }
                        }
                    }
                }
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void SetControlLanguage(Control ctrl, bool bHebrew)
        {
            if (bHebrew)
            {
                ctrl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                ActiveLayout.Hebrew();
            }
            else
            {
                ctrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
                ActiveLayout.English();
            }

            ctrl.Focus();
        }

        //private void SetControlLanguage(Control ctrl)
        //{
        //    if (ActiveLayout.bHebrew)
        //    {
        //        ctrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
        //        ActiveLayout.English();
        //    }
        //    else
        //    {
        //        ctrl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        //        ActiveLayout.Hebrew();
        //    }

        //    ctrl.Focus();
        //}

        public Parser parser = new Parser(Parser.HostType.realHost);
        //private Parser parser = new Parser(Parser.HostType.localHost);

        private void Form1_Load(object sender, EventArgs e)
        {
            //TochenTnua tt = new TochenTnua();
            //GetClassProperties.ReadProperties(tt);

            //Ping();
            //"/SendOutBoxData:" "/countryid:117" "/companyvat:028753390"
            //"/countryid:117" "/companyvat:028753390" "/importoutbox:fullpath_filename" ["/importdelimiter:|"]
            //Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString())
            //FirstRun();
            SetLicence();

            dblayer = new DBLayer(System.Windows.Forms.Application.StartupPath + @"\App_Data\");
            //dblayer = new DBLayer(@"C:\GlobalBOX\GetGlobalInfo\GetGlobalInfo\App_Data\LocalInfoProtocol.mdb");

            if (bCommandLineExecution)
            {
                if ((CountryID != null) && (CompanyVAT != null))
                {
                    ExternalDataManager externalDataManager = new ExternalDataManager();
                    externalDataManager.ImportEncoding = ImportEncoding;

                    if (SendOutBoxData != null)
                    {
                        Company_Info = dblayer.GetCompanyInfo(CountryID, CompanyVAT);
                        SendOutboxData.Send(Company_Info, parser, dblayer);
                    }

                    if (ImportOutbox != null)
                    {
                        if (ImportDelimiter == null)
                        {
                            ImportDelimiter = "|";
                        }

                        Company_Info = dblayer.GetCompanyInfo(CountryID, CompanyVAT);

                        //MessageBox.Show("CountryID , CompanyVAT ", CountryID + "|" + CompanyVAT);
                        //MessageBox.Show("Company_Info ", Company_Info.ToString());
                        //MessageBox.Show("ImportOutbox ", ImportOutbox);
                        //MessageBox.Show("ImportDelimiter ", ImportDelimiter);

                        externalDataManager.ImportHakladaMokupINI(ImportOutbox, Company_Info, dblayer, ImportDelimiter);
                    }

                    if (ImportCompanies != null)
                    {
                        if (ImportDelimiter == null)
                        {
                            ImportDelimiter = "|";
                        }

                        Company_Info = dblayer.GetCompanyInfo(CountryID, CompanyVAT);

                        externalDataManager.ImportCompanies(ImportCompanies, Company_Info, dblayer, ImportDelimiter);
                    }
                }

                this.Close();
            }
            else
            {
                dblayer.ReadCompaniesInfoList(parser, lvwCompaniesInfoList);

                LoadCountries();
                PrepareIniFileName();
                InitIniFile();

                //btnSwitchCompany.Enabled = (lvwCompaniesInfoList.Items.Count > 0);

                SelectCountryByID(RegionAndLanguageHelper.GetMachineCurrentGeoID().ToString());

                //frmPopup popup = new frmPopup(PopupSkins.SmallInfoSkin);
                //popup.ShowPopup("CMail", "Program Loaded!", 300, 3000, 3000);
                //ShareUtils.PlaySound(ShareUtils.SoundType.NewClientEntered);

                ShowTips();
            }
        }

        private void ShowTips()
        {
            //m_hb.Title = "CMailBox";
            //m_hb.TitleIcon = TooltipIcon.Info;
            //m_hb.SetToolTip(btnNewBusiness, "Add new system");

            //if (lvwCompaniesInfoList.Items.Count == 0)
            //{
            //    m_mb = new MessageBalloon();
            //    m_mb.Parent = btnNewBusiness;
            //    m_mb.Title = "CMailBox";
            //    m_mb.TitleIcon = TooltipIcon.Info;
            //    m_mb.Text = "Add new system.";


            //    m_mb.Align = BalloonAlignment.BottomMiddle;
            //    m_mb.CenterStem = true;
            //    //m_mb.UseAbsolutePositioning = checkBox2.Checked ? true : false;
            //    m_mb.Show();
            //}

            txtCompanyName.Tag = lblCompanyNameTip;
            txtCompanyVAT.Tag = lblVATNumberTip;
            txtReadCode.Tag = lblReadCodeTip;
            txtWriteCode.Tag = lblWriteCodeTip;
            txtEMail.Tag = lblEMailTip;
            txtMaam.Tag = lblVatTip;
            txtSearchPath.Tag = lblDocPathTip;
            txtDataPath.Tag = lblDataPathTip;
            btnBrowseSearch.Tag = lblDocPathTip;
            btnBrowseDataPath.Tag = lblDataPathTip;
            radPersonal.Tag = lblPersonal;
            radCompany.Tag = lblBuisness;
        }

        //public string GetMaam()
        //{
        //    //string sql = "SELECT F02 FROM AdiParams WHERE TableID=1 AND TableRowID=1";
        //    //parser.AddRequest
        //}

        //private void Ping()
        //{
        //    serverStatus1.BackgroundImage = parser.Ping() ? global::Pulsar.Properties.Resources.online : global::Pulsar.Properties.Resources.offline;
        //}

        public bool TermOfUse { get; set; }

        private void SetLicence()
        {
            RegistryKey keySoftware = Registry.LocalMachine.OpenSubKey("Software", true);

            keySoftware.CreateSubKey("CMailBox");
            RegistryKey keyCMailBox = keySoftware.OpenSubKey("CMailBox", true);

            if (!ValidateKey(keyCMailBox, "SerialNumber"))
            {
                keyCMailBox.CreateSubKey("SerialNumber");
            }

            RegistryKey keySerialNumber = keyCMailBox.OpenSubKey("SerialNumber", true);
            string serial_number = null;
            object obj = keySerialNumber.GetValue("number");

            if (obj != null)
            {
                serial_number = keySerialNumber.GetValue("number").ToString();
                //TermOfUse = parser.GetCMailBox(serial_number);                
            }

            if ((serial_number == null) || (serial_number == "") || (serial_number.Length != 38))
            {
                String TransactionGUID = "{" + Guid.NewGuid().ToString() + "}";
                keySerialNumber.SetValue("number", TransactionGUID, RegistryValueKind.String);
                serial_number = keySerialNumber.GetValue("number").ToString();
                //frmTermUse frm = new frmTermUse();
                //frm.ShowDialog();
                //parser.CreateCMailBox(serial_number, frm.TermUse);
            }

            this.Text = (serial_number.ToUpper() + " - " + this.Text);
            keySerialNumber.Close();

            keyCMailBox.CreateSubKey("AppVersion");
            RegistryKey keyAppVersion = keyCMailBox.OpenSubKey("AppVersion", true);

            keyAppVersion.SetValue("Version", "1.0130");

            keyAppVersion.Close();
            keyCMailBox.Close();
        }

        //private void FirstRun()
        //{
        //    if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\App_Data"))
        //    {
        //        Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\App_Data");
        //        if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\LocalInfoProtocol.mdb"))
        //        {
        //            File.Move(System.Windows.Forms.Application.StartupPath + @"\LocalInfoProtocol.mdb", System.Windows.Forms.Application.StartupPath + @"\App_Data\LocalInfoProtocol.mdb");
        //        }
        //    }

        //    //if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\App_Data\LocalInfoProtocol.mdb"))
        //    //{
        //    //    File.Move(System.Windows.Forms.Application.StartupPath + @"\App_Data\LocalInfoProtocol.mdb", System.Windows.Forms.Application.StartupPath + @"\App_Data\LocalInfoProtocol.mdb" + DateTime.Now);
        //    //}

        //    //if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\LocalInfoProtocol.mdb"))
        //    //{
        //    //    File.Move(System.Windows.Forms.Application.StartupPath + @"\LocalInfoProtocol.mdb", System.Windows.Forms.Application.StartupPath + @"\App_Data\LocalInfoProtocol.mdb");
        //    //}

        //    if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Documents"))
        //    {
        //        Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Documents");
        //    }
        //    if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Data"))
        //    {
        //        Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Data");
        //    }
        //}

        public bool ValidateKey(RegistryKey key, String key_name)
        {
            bool bResult = false;

            RegistryKey keyCheck = key.OpenSubKey(key_name);
            bResult = (keyCheck != null);
            if (keyCheck != null)
            {
                keyCheck.Close();
            }

            return bResult;
        }

        private void LoadCountries()
        {
            foreach (ComboboxItem countryItem in getCountryList())
            {
                cmbCountries.Items.Add(countryItem);
            }
            cmbCountries.Sorted = true;
        }


        public static ArrayList getCountryList()
        {
            ArrayList checkCultureList = new ArrayList();
            ArrayList cultureList = new ArrayList();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            foreach (CultureInfo culture in cultures)
            {
                if (culture.LCID != 127)
                {
                    RegionInfo region = new RegionInfo(culture.LCID);
                    if (!(checkCultureList.Contains(region.EnglishName)))
                    {
                        ComboboxItem item = new ComboboxItem();
                        item.Text = region.EnglishName;
                        item.Value = region.GeoId;
                        cultureList.Add(item);
                        checkCultureList.Add(region.EnglishName);
                    }
                }
            }
            //cultureList.Sort();
            //put the country list in alphabetic order.
            return cultureList;
        }

        public bool SettingsMode { get; set; }
        public frmProcessRequest frmprocessrequest = new frmProcessRequest();

        private bool IsCompamnyRegistered()
        {
            bool bResult = false;

            if (!SettingsMode)
            {
                Company_Info = (CompanyInfo)lvwCompaniesInfoList.Items[lvwCompaniesInfoList.SelectedIndices[0]].Tag;
                String CompantSerialNumber = parser.IsActiveCompany(Company_Info);
                if (CompantSerialNumber == Company_Info.CompanySerialNumber)
                {
                    bResult = true;
                    this.Hide();
                    LoadCloud();
                    this.Show();
                    //this.Close();
                }
            }

            frmprocessrequest.Hide();
            //picNewClient.Visible = false;
            //TODO: Open Login Failed MessageBox!!!
            return bResult;
        }

        public bool IncominMailCheckOnly { get; set; }

        private void LoadCloud()
        {
            if (!IncominMailCheckOnly)
            {
                frmprocessrequest.Show();
            }
            System.Windows.Forms.Application.DoEvents();

            bool TermOfUse = parser.IsCompanyCommercial(Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT);

            bool Paid = parser.IsCompanyPaid(Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT);

            bool payment = false;

            if (TermOfUse && !Paid)
            {
                frmprocessrequest.Hide();
                payment = Payment(Company_Info, false);
                //if (!payment)
                //{
                //    ////radPersonal.Checked = true;
                //    //return;
                //}
            }

            if (!IncominMailCheckOnly)
            {
                frmprocessrequest.Show();
            }

            //this.Hide();
            notifyIcon1.Dispose();
            if (!IncominMailCheckOnly)
            {
                Company_Info = (CompanyInfo)lvwCompaniesInfoList.Items[lvwCompaniesInfoList.SelectedIndices[0]].Tag;
            }

            frmCloudBox frm = new frmCloudBox();
            frm.TermOfUse = TermOfUse;
            frm.frmprocessrequest = frmprocessrequest;
            frm.Company_Info = Company_Info;
            frm.SelectedCountryIndex = cmbCountries.SelectedIndex;
            frm.CompanyName = Company_Info.CompanyName;// txtCompanyName.Text;
            frm.CountryID = Company_Info.CompanyCountryID.ToString();// txtCountryID.Text;
            frm.CompanyVAT = Company_Info.CompanyVAT;// company_infotxtCompanyVAT.Text;
            frm.ReadCode = Company_Info.ReadCode;//txtReadCode.Text;
            frm.WriteCode = Company_Info.WriteCode;// txtWriteCode.Text;
            frm.Maam = Company_Info.maam.ToString();// txtMaam.Text;
            frm.parser = parser;
            frm.IncominMailCheckOnly = IncominMailCheckOnly;
            frm.MailInterval = tmrMail.Interval;
            frm.ShowDialog();

            //picNewClient.Visible = false;
            frmprocessrequest.Hide();
            System.Windows.Forms.Application.DoEvents();
        }

        public void SaveLogsToWeb(string logFileName)
        {
            WebClient webClient = new WebClient();
            string webAddress = null;
            try
            {
                String localHost = @"http://192.168.10.250/GlobalInfoProtocol/";
                String realHost = @"https://www.misradit.info/GlobalInfoProtocol/";
                String host = localHost; // realHost;

                webAddress = host; // @"http://myCompany/ShareDoc/";
                webClient.Credentials = CredentialCache.DefaultCredentials;
                WebRequest serverRequest = WebRequest.Create(webAddress);
                WebResponse serverResponse;
                serverResponse = serverRequest.GetResponse();
                serverResponse.Close();
                webClient.UploadFile(webAddress + logFileName, "PUT", logFileName);
                webClient.Dispose();
                webClient = null;
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void SimpleWebClientUpload(String _postHandlerUri)
        {
            //string filePath = Path.GetFullPath("TestFiles/TextFile1.txt");

            //var client = new WebClient();

            //client.UploadFile("http://localhost/myhandler.ashx", filePath);

            // usage example 

            string filePath = Path.GetFullPath("TestFiles/TextFile1.txt");

            // possibly an existing CookieContainer containing authentication
            // or session cookies
            var cookies = new CookieContainer();

            var client = new CustomWebClient(cookies);

            // existing cookies are sent
            client.UploadFile(_postHandlerUri, filePath);

            // cookies is an instance of a reference type so it now
            // contains updated/new cookies resulting from the upload
        }


        public class CustomWebClient : WebClient
        {
            private CookieContainer _cookies;

            public CustomWebClient(CookieContainer cookies)
            {
                _cookies = cookies;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
                request.CookieContainer = _cookies;
                return request;
            }
        }

        private void btnCreateBox_Click(object sender, EventArgs e)
        {
            String result = parser.IsCompanyExist(txtCountryID.Text, txtCompanyVAT.Text);

            if (result.ToLower().IndexOf("active") != -1)   //Active / Not Active
            {
                //frmPopup popup = new frmPopup(PopupSkins.SmallInfoSkin);
                //popup.ShowPopup("CMail", "CMail Allready Exist And " + Environment.NewLine + result, 500, 3000, 3000);
                //ShareUtils.PlaySound(ShareUtils.SoundType.NewMessageReceived);
                MessageBox.Show("CMail Allready Exist And " + Environment.NewLine + result, "CMail", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            //pnlButtons.Visible = false;
            btnCreateBox.Enabled = false;

            if (ValidateForm())
            {                
                //picNewClient.Visible = true;                
                System.Windows.Forms.Application.DoEvents();

                CompanyInfo company_info = CreateCompanyInfo();

                if (result.ToLower().IndexOf("active") != -1)
                {
                    frmprocessrequest.Show();
                    company_info.CompanySerialNumber = parser.IsActiveCompany(company_info);
                    dblayer.AddExistingCompanyInfo(company_info);
                }
                else
                {
                    if (radCompany.Checked)
                    {
                        Payment(company_info, true);
                    }
                    else
                    {
                        frmprocessrequest.Show();
                        company_info.CompanySerialNumber = dblayer.AddCompanyInfo(company_info);
                        parser.CreateCompany(company_info, "home_use|home_use|home_use", false);
                    }
                }

                frmHaklada frm = new frmHaklada();
                frm.Company_Info = company_info;
                frm.SaveNewSystem();
                frm.Close();

                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\" + txtCompanyVAT.Text.Trim() + @"\Documents"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\" + txtCompanyVAT.Text.Trim() + @"\Documents");
                }

                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\" + txtCompanyVAT.Text.Trim() + @"\Data"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\" + txtCompanyVAT.Text.Trim() + @"\Data");
                }

                dblayer.ReadCompaniesInfoList(parser, lvwCompaniesInfoList);
                btnCreateBox.Enabled = true;

                CleanForm();
                //picNewClient.Visible = false;

                frmprocessrequest.Hide();
                //if (IsCompamnyRegistered())
                //{
                //    this.Close();
                //}
                btnDelete.Enabled = false;
                btnUpdateCompany.Enabled = false;
                btnCreateBox.Enabled = false;

                btnCreateBox.Enabled = false;
                //pnlButtons.Visible = true;
                pnlBusinessTable.Visible = true;
                pnlNewBusiness.Visible = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnUpdateCompany.Enabled = true;
                btnCreateBox.Enabled = true;
            }
        }

        private bool Payment(CompanyInfo company_info, bool bAddCompany)
        {
            bool bResult = false;
            frmPayment frm = new frmPayment();
            frm.Company_Info = company_info;
            frm.ShowDialog();
            String Payment = null;

            if (!frm.paid)
            {
                radPersonal.Checked = true;
            }

            //if (frm.paid)
            //{
                frmprocessrequest.Show();
                if (bAddCompany)
                {
                    company_info.CompanySerialNumber = dblayer.AddCompanyInfo(company_info);
                }

                ///////////////////////////////////////////////
                string key = company_info.CompanySerialNumber.Replace("-", "").Replace("{", "").Replace("}", ""); //Guid.NewGuid().ToString().Replace("-", "");
                string salt = CipherUtility.GenerateSimpleSalt();
                //string plain = "4580000000000000|יניב זוהר|111111118|08/16|1234";
                //plain = txtName.Text + "|" + txtCC.Text + "|" + txtExpired.Text + "|" + txtCVV.Text + "|" + txtID.Text;
                Payment = CipherUtility.Encrypt<RijndaelManaged>(frm.plain, key, salt) + "|" + salt;
                //string encrypted = CipherUtility.Encrypt<RijndaelManaged>(plain, key, salt);
                //string decrypted = CipherUtility.Decrypt<RijndaelManaged>(encrypted, key, salt);

                //String xxx1 = Uri.EscapeUriString(Payment);
                //String xxx2 = HttpUtility.UrlEncode(CompanyName);
                //String xxx3 = Uri.EscapeDataString(Payment);
                Payment = Uri.EscapeDataString(Payment);
                ///////////////////////////////////////////////
                //parser.CreateCompany(txtCountryID.Text, txtCompanyVAT.Text, txtCompanyName.Text, txtReadCode.Text, txtWriteCode.Text, txtEMail.Text, company_info.CompanySerialNumber);
                //parser.CreateCompany(company_info, frm.Payment);

                if (bAddCompany)
                {
                    parser.CreateCompany(company_info, Payment, true);
                }
                else
                {
                    //Company_Info, txtCountryID.Text, txtCompanyVAT.Text, txtCompanyName.Text, txtReadCode.Text, txtWriteCode.Text, txtEMail.Text, true
                    parser.UpdatePayment(company_info, true, Payment);
                }

                bResult = true;
            //}

            if (frm.PaymentByPass)
            {
                frmprocessrequest.Show();
                company_info.CompanySerialNumber = dblayer.AddCompanyInfo(company_info);
                parser.CreateCompany(company_info, "no_payment|no_payment|no_payment", true);
                bResult = true;
            }

            return bResult;
        }

        private bool ValidateForm()
        {
            Control ctrl = null;

            if (!ValidateControl(cmbCountries))
                ctrl = cmbCountries;
            else if (!ValidateControl(txtCompanyName))
                ctrl = txtCompanyName;
            else if (txtCountryID.Text == "117")
            {
                if (!IsraeliID.IsValidID(txtCompanyVAT.Text))
                {
                    txtCompanyVAT.BackColor = Color.Pink;
                    ctrl = txtCompanyVAT;
                }
                else
                {
                    txtCompanyVAT.BackColor = Color.White;
                }
            }

            if (ctrl == null)
            {
                if ((!ValidateControl(txtReadCode)) || (!ValidateControlMinLength(txtReadCode, 9)))
                    ctrl = txtReadCode;
                else if ((!ValidateControl(txtWriteCode)) || (!ValidateControlMinLength(txtWriteCode, 6)))
                    ctrl = txtWriteCode;
                else if ((!ValidateControl(txtEMail)) || (!ValidateControlMinLength(txtEMail, 4)))
                    ctrl = txtEMail;
                else if (!ValidateControl(txtMaam))
                    ctrl = txtMaam;
            }

            if (ctrl != null)
                ctrl.Focus();

            return (ctrl == null);
        }

        //private bool ValidateForm()
        //{
        //    if (!ValidateControl(cmbCountries))
        //        return false;

        //    if (!ValidateControl(txtCompanyName))
        //        return false;

        //    if (txtCountryID.Text == "117")
        //    {
        //        if (!IsraeliID.IsValidID(txtCompanyVAT.Text))
        //        {
        //            txtCompanyVAT.BackColor = Color.Pink;
        //            txtCompanyVAT.Focus();
        //            return false;
        //        }
        //        else
        //        {
        //            txtCompanyVAT.BackColor = Color.White;
        //        }
        //    }

        //    if ((!ValidateControl(txtReadCode)) || (!ValidateControlLength(txtReadCode, 9)))
        //        return false;

        //    if ((!ValidateControl(txtWriteCode)) || (!ValidateControlLength(txtWriteCode, 9)))
        //        return false;

        //    if ((!ValidateControl(txtEMail)) || (!ValidateControlMinLength(txtEMail, 4)))
        //        return false;

        //    if (!ValidateControl(txtMaam))
        //        return false;

        //    return true;
        //}
        private bool ValidateControl(ComboBox cmb)
        {
            if (cmb.SelectedIndex == -1)
            {
                cmb.Focus();
                cmb.BackColor = Color.Pink;
                return false;
            }
            else
                cmb.BackColor = Color.White;

            return true;
        }

        private bool ValidateControl(TextBox txt)
        {
            if (txt.Text.Trim().Length == 0)
            {
                txt.Focus();
                txt.BackColor = Color.Pink;
                return false;
            }
            else
                txt.BackColor = Color.White;

            return true;
        }

        private bool ValidateControlLength(TextBox txt, int length)
        {
            if (txt.Text.Trim().Length != length)
            {
                txt.Focus();
                txt.BackColor = Color.Pink;
                return false;
            }
            else
                txt.BackColor = Color.White;

            return true;
        }

        private bool ValidateControlMinLength(TextBox txt, int length)
        {
            if (txt.Text.Trim().Length < length)
            {
                txt.Focus();
                txt.BackColor = Color.Pink;
                return false;
            }
            else
                txt.BackColor = Color.White;

            return true;
        }
        /// <summary>     
        /// returns the mac address of the first operation nic found.  
        /// /// </summary>     /// <returns></returns>    
        private string GetMacAddress()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

        /// <summary>
        /// The method create a Base64 encoded string from a normal string.
        /// </summary>
        /// <param name="toEncode">The String containing the characters to encode.</param>
        /// <returns>The Base64 encoded string.</returns>
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.Encoding.Unicode.GetBytes(toEncode);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }

        /// <summary>
        /// The method to Decode your Base64 strings.
        /// </summary>
        /// <param name="encodedData">The String containing the characters to decode.</param>
        /// <returns>A String containing the results of decoding the specified sequence of bytes.</returns>
        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);

            string returnValue = System.Text.Encoding.Unicode.GetString(encodedDataAsBytes);

            return returnValue;
        }

        private void btnSwitchCompany_Click(object sender, EventArgs e)
        {
            SwitchCompany();
        }

        private void SwitchCompany()
        {
            IncominMailCheckOnly = false;

            frmprocessrequest.Show();

            btnCreateBox.Enabled = false;
            IsCompamnyRegistered();
            btnCreateBox.Enabled = true;

            tmrMail.Enabled = true;
            IncominMailCheckOnly = true;
        }

        private void SaveRecord()
        {
            // in the future we need to find way to update company
            if (ValidateForm())
            {
                CompanyInfo company_info = CreateCompanyInfo();
                dblayer.AddCompanyInfo(company_info);
                dblayer.ReadCompaniesInfoList(parser, lvwCompaniesInfoList);
                btnCreateBox.Enabled = true;
            }
        }

        private CompanyInfo CreateCompanyInfo()
        {
            CompanyInfo company_info = new CompanyInfo();
            company_info.CompanyCountryID = Int32.Parse(txtCountryID.Text.Trim());
            company_info.CompanyName = txtCompanyName.Text.Trim().Replace("'", "''");
            company_info.CompanyVAT = txtCompanyVAT.Text.Trim();
            company_info.EMail = txtEMail.Text.Trim();
            company_info.maam = double.Parse(txtMaam.Text.Trim());
            company_info.ReadCode = txtReadCode.Text.Trim();
            company_info.WriteCode = txtWriteCode.Text.Trim();
            company_info.FilesSearch = txtSearchPath.Text.Trim();
            company_info.DataPath = txtDataPath.Text.Trim();
            company_info.SystemColor = lblSystemColor.BackColor.ToArgb();
            company_info.MobilePhone = txtMobilePhone.Text.Trim();
            company_info.InformMyMobile = chkInformMyMobile.Checked;
            //company_info.IntervalMailCheck = (int)cmbIntervalMailCheck.Value;

            return company_info;
        }

        private void cmbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCountryID();
        }

        //private void txtCountryID_TextChanged(object sender, EventArgs e)
        //{
        //    //if (cmbCountries.SelectedIndex == -1)
        //    //{
        //    //    if (txtCountryID.Text != "")
        //    //    {
        //    //        SelectCountryByID(txtCountryID.Text);
        //    //    }
        //    //}
        //}

        private void SetCountryID()
        {
            ComboboxItem cmbItem = (ComboboxItem)cmbCountries.SelectedItem;
            if (cmbItem != null)
            {
                txtCountryID.Text = cmbItem.Value.ToString();
            }
        }

        private void SelectCountryByID(string txt)
        {
            for (int i = 0; i < cmbCountries.Items.Count; i++)
            {
                ComboboxItem item = (ComboboxItem)cmbCountries.Items[i];

                if (item.Value.ToString() == txt)
                {
                    cmbCountries.SelectedIndex = i;
                    txtCountryID.Text = item.Value.ToString();
                    break;
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        public String IniFileName { get; set; }
        public IniFile iniFile { get; set; }

        private void PrepareIniFileName()
        {
            IniFileName = Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
            IniFileName = IniFileName.Substring(0, IniFileName.Length - ".exe".Length) + ".ini";
            iniFile = new IniFile(System.Windows.Forms.Application.StartupPath + @"\" + IniFileName);
        }

        private void InitIniFile()
        {
            if (!System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + @"\" + IniFileName))
            {
                ResetIniFile();
            }
            else
            {
                ReadIniFile();
            }
        }

        public String FileMonitorPath { get; set; }
        public String PdfFilePath { get; set; }
        public String DataFilePath { get; set; }
        public String SendToCompany { get; set; }
        public int IntervalMailCheck { get; set; }

        private void ReadIniFile()
        {
            FileMonitorPath = iniFile.IniReadValue("Files", "Monitor");
            PdfFilePath = iniFile.IniReadValue("Files", "pdf");
            DataFilePath = iniFile.IniReadValue("Files", "data");
            SendToCompany = iniFile.IniReadValue("Company", "SendTo");
            SendToCompany = iniFile.IniReadValue("Company", "Working");
            txtMaam.Text = iniFile.IniReadValue("VAT", "VatValue");
            IntervalMailCheck = Int32.Parse(iniFile.IniReadValue("MailCheck", "IntervalMailCheckValue"));
            tmrMail.Interval = (int)IntervalMailCheck * 1000 * 60;
        }

        private void SaveIniFile()
        {
            iniFile.IniWriteValue("Files", "Monitor", FileMonitorPath);
            iniFile.IniWriteValue("Files", "pdf", PdfFilePath);
            iniFile.IniWriteValue("Files", "data", DataFilePath);
            iniFile.IniWriteValue("Company", "SendTo", SendToCompany);
            iniFile.IniWriteValue("Company", "Working", SendToCompany);
            iniFile.IniWriteValue("VAT", "VatValue", txtMaam.Text);
            iniFile.IniWriteValue("MailCheck", "IntervalMailCheckValue", IntervalMailCheck.ToString());
        }

        private void ResetIniFile()
        {
            iniFile.IniWriteValue("Files", "Monitor", "path needed");
            iniFile.IniWriteValue("Files", "pdf", "path needed");
            iniFile.IniWriteValue("Files", "data", "path needed");
            iniFile.IniWriteValue("Company", "SendTo", "default company to send needed");
            iniFile.IniWriteValue("Company", "Working", "no default working company");
            iniFile.IniWriteValue("VAT", "VatValue", "0");
            iniFile.IniWriteValue("MailCheck", "IntervalMailCheckValue", "10");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            if (lvwCompaniesInfoList.SelectedIndices.Count == 1)
            {
                if (MessageBox.Show("Delete Company " + txtCompanyName.Text, "Delete Company", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    CompanyInfo company_info = (CompanyInfo)lvwCompaniesInfoList.Items[lvwCompaniesInfoList.SelectedIndices[0]].Tag;
                    dblayer.DeleteCompanyInfo(company_info.CompanyID);
                    dblayer.ReadCompaniesInfoList(parser, lvwCompaniesInfoList);
                    parser.DeleteCompany(company_info.CompanyCountryID.ToString(), company_info.CompanyVAT, company_info.ReadCode, company_info.WriteCode, company_info.EMail);
                }
            }

            btnDelete.Enabled = (lvwCompaniesInfoList.SelectedIndices.Count == 1);
            btnSwitchCompany.Enabled = btnDelete.Enabled;
            btnUpdateCompany.Enabled = btnDelete.Enabled;
        }

        public bool CancelSelection { get; set; }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CancelSelection = true;
            this.Close();
        }

        private void lvwCompaniesInfoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewCompanyInfoDetails();
        }

        private void ViewCompanyInfoDetails()
        {
            btnSwitchCompany.Enabled = (lvwCompaniesInfoList.SelectedIndices.Count == 1);
            btnDelete.Enabled = btnSwitchCompany.Enabled;
            btnUpdateCompany.Enabled = btnSwitchCompany.Enabled;
            btnShare.Enabled = btnSwitchCompany.Enabled;
            btnStopConnection.Enabled = btnSwitchCompany.Enabled;

            if (lvwCompaniesInfoList.SelectedIndices.Count == 1)
            {
                Company_Info = (CompanyInfo)lvwCompaniesInfoList.Items[lvwCompaniesInfoList.SelectedIndices[0]].Tag;

                bool TermOfUse = parser.IsCompanyCommercial(Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT);

                if (!TermOfUse)
                {
                    webBrowser1.Url = new Uri("http://adirim.info/GlobalInfoProtocol/Advertisment.aspx");
                    webBrowser1.Visible = true;
                    lvwCompaniesInfoList.Height = this.Height - lvwCompaniesInfoList.Top - 170;
                }
                else
                {
                    webBrowser1.Visible = false;
                    lvwCompaniesInfoList.Height = this.Height - lvwCompaniesInfoList.Top - 110;
                }
            }
            //if (btnSwitchCompany.Enabled)
            //{
            //    CompanyInfo company_info = (CompanyInfo)lvwCompaniesInfoList.Items[lvwCompaniesInfoList.SelectedIndices[0]].Tag;
            //    SelectCountryByID(company_info.CompanyCountryID.ToString());
            //    txtWriteCode.Text = company_info.WriteCode;
            //    txtReadCode.Text = company_info.ReadCode;
            //    txtMaam.Text = company_info.maam.ToString();
            //    txtEMail.Text = company_info.EMail;
            //    txtCompanyVAT.Text = company_info.CompanyVAT.ToString();
            //    txtCompanyName.Text = company_info.CompanyName;
            //}
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        private void CleanForm()
        {
            btnSwitchCompany.Enabled = (lvwCompaniesInfoList.SelectedIndices.Count == 1);
            btnDelete.Enabled = btnSwitchCompany.Enabled;
            btnUpdateCompany.Enabled = btnSwitchCompany.Enabled;

            SelectCountryByID(RegionAndLanguageHelper.GetMachineCurrentGeoID().ToString());
            txtWriteCode.Text = "";
            txtReadCode.Text = "";
            txtMaam.Text = "";
            txtEMail.Text = "";
            txtCompanyVAT.Text = "";
            txtCompanyName.Text = "";
            txtSearchPath.Text = "";
            txtDataPath.Text = "";
            ReadIniFile();
        }

        private void lvwCompaniesInfoList_DoubleClick(object sender, EventArgs e)
        {
            if (lvwCompaniesInfoList.SelectedIndices.Count == 1)
            {
                SwitchCompany();
            }
        }

        private void txtCompanyName_Enter(object sender, EventArgs e)
        {
            SetControlLanguage((Control)sender, txtCountryID.Text == "117");
            ShowTip(sender);
        }

        private void txtEMail_Enter(object sender, EventArgs e)
        {
            SetControlLanguage((Control)sender, false);
            ShowTip(sender);
        }

        private void btnNewBusiness_Click(object sender, EventArgs e)
        {
            txtSearchPath.Text = System.Windows.Forms.Application.StartupPath + @"\" + txtCompanyVAT.Text + @"\Documents";
            txtDataPath.Text = System.Windows.Forms.Application.StartupPath + @"\" + txtCompanyVAT.Text + @"\Data";

            btnCreateBox.Enabled = true;
            txtCompanyVAT.Enabled = true;
            cmbCountries.Enabled = true;
            pnlBusinessTable.Visible = false;
            pnlNewBusiness.Visible = true;
            //txtCompanyName.Focus();
            radPersonal.Visible = true;
            radCompany.Visible = true;
            radCompany.Focus();

            //if (m_mb != null)
            //{
            //    m_mb.Dispose();
            //}
        }

        private void btnExitCreateBox_Click(object sender, EventArgs e)
        {
            CleanForm();
            pnlNewBusiness.Visible = false;
            pnlBusinessTable.Visible = true;
            //m_hb.SetToolTip(btnNewBusiness, "Add new system");
        }

        private void btnBrowseSearch_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtSearchPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnCreateBox.Enabled = false;

            if (ValidateForm())
            {
                //picNewClient.Visible = true;
                frmprocessrequest.Show();
                System.Windows.Forms.Application.DoEvents();

                CompanyInfo company_info = CreateCompanyInfo();
                company_info.SystemColor = lblSystemColor.BackColor.ToArgb();
                company_info.CompanyID = Company_Info.CompanyID;
                company_info.CompanySerialNumber = Company_Info.CompanySerialNumber;
                dblayer.UpdateCompanyInfo(company_info);
                dblayer.ReadCompaniesInfoList(parser, lvwCompaniesInfoList);
                btnCreateBox.Enabled = true;

                //parser.UpdateCompany(txtCountryID.Text, txtCompanyVAT.Text, txtCompanyName.Text, txtReadCode.Text, txtWriteCode.Text, txtEMail.Text);

                parser.UpdateCompany(Company_Info, txtCountryID.Text, txtCompanyVAT.Text, txtCompanyName.Text, txtReadCode.Text, txtWriteCode.Text, txtEMail.Text, radCompany.Checked, company_info.MobilePhone, company_info.InformMyMobile);

                CleanForm();
                //picNewClient.Visible = false;

                frmprocessrequest.Hide();
                //if (IsCompamnyRegistered())
                //{
                //    this.Close();
                //}
                btnDelete.Enabled = false;
                btnUpdateCompany.Enabled = false;
                btnCreateBox.Enabled = false;

                btnCreateBox.Enabled = true;
                //pnlButtons.Visible = true;
                pnlBusinessTable.Visible = true;
                pnlNewBusiness.Visible = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnUpdateCompany.Enabled = true;
                btnCreateBox.Enabled = true;
            }
        }

        private void btnUpdateCompany_Click(object sender, EventArgs e)
        {
            EditCompany();
            btnCreateBox.Enabled = false;
            btnUpdate.Enabled = true;
            txtCompanyVAT.Enabled = false;
            cmbCountries.Enabled = false;
            pnlBusinessTable.Visible = false;
            pnlNewBusiness.Visible = true;
            txtCompanyName.Focus();
        }

        private void EditCompany()
        {
            if (lvwCompaniesInfoList.SelectedIndices.Count == 1)
            {
                SelectCountryByID(Company_Info.CompanyCountryID.ToString());
                txtWriteCode.Text = Company_Info.WriteCode;
                txtReadCode.Text = Company_Info.ReadCode;
                txtMaam.Text = Company_Info.maam.ToString();
                txtEMail.Text = Company_Info.EMail;
                txtCompanyVAT.Text = Company_Info.CompanyVAT.ToString();
                txtCompanyName.Text = Company_Info.CompanyName;
                txtSearchPath.Text = Company_Info.FilesSearch;
                txtDataPath.Text = Company_Info.DataPath;
                lblSystemColor.BackColor = Color.FromArgb(Company_Info.SystemColor);

                //cmbIntervalMailCheck.Value = Company_Info.IntervalMailCheck;

                bool bCommercial = parser.IsCompanyCommercial(Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT);

                if (bCommercial)
                {
                    radCompany.Checked = true;
                    radPersonal.Visible = false;
                    radPersonal.Checked = false;
                }
                else
                {
                    radCompany.Checked = false;
                    radPersonal.Visible = true;
                    radPersonal.Checked = true;
                }
            }
        }

        private void btnBrowseDataPath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDataPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            HideTip(sender);
        }

        private void ShowTip(object sender)
        {
            Tip(sender).Visible = true;
        }

        private void HideTip(object sender)
        {
            Tip(sender).Visible = false;
        }

        public Label Tip(object sender)
        {
            return ((Label)((Control)sender).Tag);
            //return ((Label)((TextBox)sender).Tag);
        }

        private void btnBusinessConverter_Click(object sender, EventArgs e)
        {

        }

        private void txtCompanyVAT_TextChanged(object sender, EventArgs e)
        {
            txtSearchPath.Text = System.Windows.Forms.Application.StartupPath + @"\" + txtCompanyVAT.Text + @"\Documents";
            txtDataPath.Text = System.Windows.Forms.Application.StartupPath + @"\" + txtCompanyVAT.Text + @"\Data";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radCompany_CheckedChanged(object sender, EventArgs e)
        {
            lblCompanyNameTip.Text = "שם בית העסק.";
            lblVATNumberTip.Text = "מספר העוסק מורשה.";
        }

        private void radPersonal_CheckedChanged(object sender, EventArgs e)
        {
            lblCompanyNameTip.Text = "גוף פרטי.";
            lblVATNumberTip.Text = "מספר ת.ז.";
        }

        private void btnShare_Click(object sender, EventArgs e)
        {
            grpShare.Visible = true;
            txtShareByEMail.Focus();
            //GetOutlookContacts();
        }

        //public DataSet GetOutlookContacts()
        //{
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add("Contacts");
        //    ds.Tables[0].Columns.Add("Email");
        //    ds.Tables[0].Columns.Add("FirstName");
        //    ds.Tables[0].Columns.Add("LastName");

        //    Microsoft.Office.Interop.Outlook.Items OutlookItems;
        //    Microsoft.Office.Interop.Outlook.Application outlookObj;
        //    MAPIFolder Folder_Contacts;

        //    outlookObj = new Microsoft.Office.Interop.Outlook.Application();
        //    Folder_Contacts = (MAPIFolder)outlookObj.Session.GetDefaultFolder(OlDefaultFolders.olFolderContacts);
        //    OutlookItems = Folder_Contacts.Items;

        //    for (int i = 0; i < OutlookItems.Count; i++)
        //    {
        //        try
        //        {
        //            Microsoft.Office.Interop.Outlook.ContactItem contact = (Microsoft.Office.Interop.Outlook.ContactItem)OutlookItems[i + 1];
        //            DataRow dr = ds.Tables[0].NewRow();
        //            dr[0] = contact.Email1Address;
        //            dr[1] = contact.FirstName;
        //            dr[2] = contact.LastName;

        //            ds.Tables[0].Rows.Add(dr);
        //        }
        //        catch (System.Exception ex)
        //        {

        //        }
        //    }

        //    return ds;
        //}

        private void btnOutlookContactsEmail_Click(object sender, EventArgs e)
        {
            GetOutlookContacts();
        }

        public ArrayList GetOutlookContacts()
        {
            frmprocessrequest.Show();
            lvwOutlookContacts.SuspendLayout();
            lvwOutlookContacts.Items.Clear();
            ArrayList OutlookContactsList = new ArrayList();

            Microsoft.Office.Interop.Outlook.Items OutlookItems;
            Microsoft.Office.Interop.Outlook.Application outlookObj;
            MAPIFolder Folder_Contacts;

            outlookObj = new Microsoft.Office.Interop.Outlook.Application();
            Folder_Contacts = (MAPIFolder)outlookObj.Session.GetDefaultFolder(OlDefaultFolders.olFolderContacts);
            OutlookItems = Folder_Contacts.Items;

            for (int i = 0; i < OutlookItems.Count; i++)
            {
                try
                {
                    Microsoft.Office.Interop.Outlook.ContactItem contact = (Microsoft.Office.Interop.Outlook.ContactItem)OutlookItems[i + 1];
                    OutlookContact oc = new OutlookContact();
                    oc.EMail = contact.Email1Address;
                    oc.FirstName = contact.FirstName;
                    oc.LastName = contact.LastName;
                    ListViewItem lvi = lvwOutlookContacts.Items.Add(oc.EMail);
                    lvi.Tag = oc;
                    lvi.SubItems.Add(oc.FirstName);
                    lvi.SubItems.Add(oc.LastName);
                    OutlookContactsList.Add(oc);
                }
                catch (System.Exception ex)
                {

                }
            }
            lvwOutlookContacts.ResumeLayout();
            frmprocessrequest.Hide();
            return OutlookContactsList;
        }

        private void btnCloseShare_Click(object sender, EventArgs e)
        {
            txtShareByEMail.Text = "";
            grpShare.Visible = false;
        }

        private void btnSendShare_Click(object sender, EventArgs e)
        {
            frmprocessrequest.Show();

            if (txtShareByEMail.Text.Trim() != "")
            {
                parser.InvatationSend(Company_Info, txtShareByEMail.Text);
            }

            var selectedItems = lvwOutlookContacts.CheckedItems;

            foreach (ListViewItem lvi in selectedItems)
            {
                if (lvi.Text.Trim() != "")
                {
                    parser.InvatationSend(Company_Info, lvi.Text);
                }
            }

            foreach (ListViewItem lvi in lvwOutlookContacts.Items)
            {
                lvi.Checked = false;
            }

            grpShare.Visible = false;
            frmprocessrequest.Hide();
            MessageBox.Show("Your Invataion successfully sent to " + Environment.NewLine + txtShareByEMail.Text, "Share", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtShareByEMail.Text = "";
        }

        private void txtShareByEMail_Enter(object sender, EventArgs e)
        {
            SetControlLanguage((Control)sender, false);
        }

        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            //colorDlg.AllowFullOpen = false; 
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                lblSystemColor.BackColor = colorDlg.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSocialShare frm = new frmSocialShare();
            frm.ShowDialog();
        }

        private void btnStopConnection_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to stop using Pulsar For " + lvwCompaniesInfoList.Items[lvwCompaniesInfoList.SelectedIndices[0]].SubItems[1].Text, "Stop Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                parser.StopService(Company_Info);
            }
        }

        private void btnStartConnection_Click(object sender, EventArgs e)
        {
            parser.StartService(Company_Info);
        }

        //private void cmbIntervalMailCheck_ValueChanged(object sender, EventArgs e)
        //{
        //    iniFile.IniWriteValue("MailCheck", "IntervalMailCheckValue", cmbIntervalMailCheck.Value.ToString());
        //}

        private void tmrMail_Tick(object sender, EventArgs e)
        {
            tmrMail.Enabled = this.Visible;
            CheckMail();
        }

        private void CheckMail()
        {
            if (this.Visible)
            {
                int LastImageIndex = 0;

                foreach (ListViewItem lvi in lvwCompaniesInfoList.Items)
                {
                    if (this.Visible)
                    {
                        Company_Info = (CompanyInfo)lvi.Tag;
                        String CompantSerialNumber = parser.IsActiveCompany(Company_Info);
                        if (CompantSerialNumber == Company_Info.CompanySerialNumber)
                        {
                            LastImageIndex = lvi.ImageIndex;
                            lvi.ImageIndex = 1;
                            lvi.EnsureVisible();
                            IncominMailCheckOnly = true;
                            LoadCloud();
                            lvi.ImageIndex = LastImageIndex;
                        }
                    }
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.ShowDialog();
            IntervalMailCheck = Int32.Parse(iniFile.IniReadValue("MailCheck", "IntervalMailCheckValue"));
            tmrMail.Interval = (int)IntervalMailCheck * 1000 * 60;
        }

        private void mnuUpgrade_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to upgrade/update Pulsar!", "Pulsar Upgarde", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                ////http://www.simplicitools.com/SimpliciTools/FileDownload2.ashx?filename=Pulsar.msi&ProductID=2
                //WebClient webClient = new WebClient();
                //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                //webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                //webClient.DownloadFile(new Uri("http://www.simplicitools.com/SimpliciTools/FileDownload2.ashx?filename=Pulsar.msi&ProductID=2"), System.Windows.Forms.Application.StartupPath + @"\Pulsar.msi");

                //Process.Start(new ProcessStartInfo()
                //{
                //    FileName = System.Windows.Forms.Application.StartupPath + @"\Installer.exe",
                //    UseShellExecute = true,
                //    Arguments = "\"" +System.Windows.Forms.Application.StartupPath + @"\Pulsar.msi" + "\"" + " /r"
                //});
                //frmDownload frm = new frmDownload();
                //frm.ShowDialog();

                Process.Start(new ProcessStartInfo()
                {
                    FileName = System.Windows.Forms.Application.StartupPath + @"\App_Data\Installer.exe",
                    UseShellExecute = true,
                    Arguments = "Pulsar.msi"
                });

                this.Close();
            }
        }
    }
}


//// Hex to Control Color
//var myColor = "#[color from database]";
//var myControlColor = System.Drawing.ColorTranslator.FromHtml(myColor);

//// Control Color to Hex
//var colorBlue = System.Drawing.Color.Blue;
//var hexBlue = System.Drawing.ColorTranslator.ToHtml(colorBlue);

//ColorDialog col = new ColorDialog();
//col.ShowDialog();
//string color = col.Color.ToArgb().ToString("x");
//color = color.Substring(2, 6);
//color = "#" + color;
//MessageBox.Show(color);
//button1.BackColor = System.Drawing.ColorTranslator.FromHtml(color);

//System.Drawing.Color c = System.Drawing.Color.FromArgb(int);
//int x = c.ToArgb();
