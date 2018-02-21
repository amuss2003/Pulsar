using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Data.OleDb;
using Pulsar;
using Pulsar.Classes;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Drawing2D;
//using Proshot.UtilityLib.CommonDialogs;
using Pulsar.Forms;
using System.Runtime.InteropServices;
using System.Deployment.Application;
using System.Security.Cryptography;
using System.Configuration;

namespace Pulsar
{
    public partial class frmCloudBox : Form
    {
        //protected override bool DoubleBuffered { get; set; }
        //this.DoubleBuffered = true;
        //this.SetStyle(ControlStyles.UserPaint | 
        //      ControlStyles.AllPaintingInWmPaint |
        //      ControlStyles.ResizeRedraw |
        //      ControlStyles.ContainerControl |
        //      ControlStyles.OptimizedDoubleBuffer |
        //      ControlStyles.SupportsTransparentBackColor
        //      , true);

        //System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control)
        //    .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic |
        //    System.Reflection.BindingFlags.Instance);
        //    aProp.SetValue(ListView1, true, null);

        //public void EnableDoubleBuffering()
        //{
        //   this.SetStyle(ControlStyles.DoubleBuffer | 
        //      ControlStyles.UserPaint | 
        //      ControlStyles.AllPaintingInWmPaint,
        //      true);
        //   this.UpdateStyles();
        //} 
        public frmProcessRequest frmprocessrequest = new frmProcessRequest();

        private int _lockColumnIndex = 0;

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        public frmCloudBox()
        {
            InitializeComponent();
            lvwOutbox.Invalidated += new InvalidateEventHandler(lvwOutbox_Invalidated);
            lvwInbox.Invalidated += new InvalidateEventHandler(lvwInData_Invalidated);
            shadowBrush = new SolidBrush(Color.FromArgb(49, 106, 197));
        }

        public enum TabType
        {
            Inbox = 0,
            Outbox = 1
        }

        public bool TermOfUse { get; set; }
        public CompanyInfo Company_Info { get; set; }
        public String KEY { get; set; }
        public int SelectedCountryIndex { get; set; }
        public String CompanyName { get; set; }
        public String CountryID { get; set; }
        public String CompanyVAT { get; set; }
        public String ReadCode { get; set; }
        public String WriteCode { get; set; }
        public String Maam { get; set; }
        public Parser parser { get; set; }
        public int MailInterval { get; set; }

        public String IniFileName { get; set; }
        public IniFile iniFile { get; set; }

        //public ArrayList ActionsList = new ArrayList();
        public Dictionary<int, SugTnua> ActionsListDictionary = new Dictionary<int, SugTnua>();

        private void PrepareIniFileName()
        {
            IniFileName = Path.GetFileName(Application.ExecutablePath);

            FileInfo fi = new FileInfo(IniFileName);
            fi = new FileInfo(fi.FullName);
            IniFileName = fi.Name;

            IniFileName = IniFileName.Substring(0, IniFileName.Length - ".exe".Length) + ".ini";
            iniFile = new IniFile(Application.StartupPath + @"\" + IniFileName);
        }

        private void ReadIniFile()
        {
            String InboxFilterRadioFilter = iniFile.IniReadValue("InboxFilter", "RadioFilter");

            if (InboxFilterRadioFilter == "Day")
            {
                dtpFromInbox.Value = DateTime.Today;
                dtpToInbox.Value = DateTime.Today;
                radDayInbox.Checked = true;
                radDayInboxPnl.Checked = true;
            }

            if (InboxFilterRadioFilter == "Week")
            {
                radWeekInbox.Checked = true;
                radWeekInboxPnl.Checked = true;
            }

            if (InboxFilterRadioFilter == "Month")
            {
                radMonthInbox.Checked = true;
                radMonthInboxPnl.Checked = true;
            }

            if (InboxFilterRadioFilter == "Year")
            {
                radYearInbox.Checked = true;
                radYearInboxPnl.Checked = true;
            }

            String OutboxFilterRadioFilter = iniFile.IniReadValue("OutboxFilter", "RadioFilter");

            if (OutboxFilterRadioFilter == "Day")
            {
                dtpFromOutbox.Value = DateTime.Today;
                dtpToOutbox.Value = DateTime.Today;
                radDayOutbox.Checked = true;
                radDayOutboxPnl.Checked = true;
            }

            if (OutboxFilterRadioFilter == "Week")
            {
                radWeekOutbox.Checked = true;
                radWeekOutboxPnl.Checked = true;
            }

            if (OutboxFilterRadioFilter == "Month")
            {
                radMonthOutbox.Checked = true;
                radMonthOutboxPnl.Checked = true;
            }

            if (OutboxFilterRadioFilter == "Year")
            {
                radYearOutbox.Checked = true;
                radYearOutboxPnl.Checked = true;
            }

            chkWaiting.Checked = iniFile.IniReadValue("OutboxFilter", "chkWaiting").ToLower() == true.ToString().ToLower();
            chkTransferd.Checked = iniFile.IniReadValue("OutboxFilter", "chkSend").ToLower() == true.ToString().ToLower();
            chkReaden.Checked = iniFile.IniReadValue("OutboxFilter", "chkReaden").ToLower() == true.ToString().ToLower();
        }

        private void SaveIniFile()
        {
            if (radDayInbox.Checked)
                iniFile.IniWriteValue("InboxFilter", "RadioFilter", "Day");
            if (radWeekInbox.Checked)
                iniFile.IniWriteValue("InboxFilter", "RadioFilter", "Week");
            if (radMonthInbox.Checked)
                iniFile.IniWriteValue("InboxFilter", "RadioFilter", "Month");
            if (radYearInbox.Checked)
                iniFile.IniWriteValue("InboxFilter", "RadioFilter", "Year");

            if (radDayOutbox.Checked)
                iniFile.IniWriteValue("OutboxFilter", "RadioFilter", "Day");
            if (radWeekOutbox.Checked)
                iniFile.IniWriteValue("OutboxFilter", "RadioFilter", "Week");
            if (radMonthOutbox.Checked)
                iniFile.IniWriteValue("OutboxFilter", "RadioFilter", "Month");
            if (radYearOutbox.Checked)
                iniFile.IniWriteValue("OutboxFilter", "RadioFilter", "Year");

            iniFile.IniWriteValue("OutboxFilter", "chkWaiting", chkWaiting.Checked.ToString());
            iniFile.IniWriteValue("OutboxFilter", "chkSend", chkTransferd.Checked.ToString());
            iniFile.IniWriteValue("OutboxFilter", "chkReaden", chkReaden.Checked.ToString());
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            GetMail();
        }

        private void GetMail()
        {
            GetMail(false);
        }

        private void GetMail(bool bShowWindow)
        {
            tmrMail.Enabled = false;
            btnGetData.Enabled = false;

            //If more than 5 mails total ask for credit card on non commercial or no payment
            //GetBillingForThisMonth
            if (parser.IsCompanyHasCCToPay(Company_Info).ToLower() != true.ToString().ToLower())
            {
                String BillingInOut = parser.GetBillingForThisMonth(Company_Info);

                int in_mails_used = Int32.Parse(BillingInOut.Split(new char[] { '|' })[0]);

                int MailsToRead = Int32.Parse(parser.GetWaitingMails(Company_Info));

                if (MailsToRead == 0)
                {
                    return;
                }
                else if (in_mails_used + MailsToRead > 5)
                {
                    bool payment = false;

                    payment = Payment(Company_Info, false);
                    if (!payment)
                    {
                        return;
                    }
                }
            }

            if (bShowWindow)
            {
                frmprocessrequest.Show();
            }
            else
            {
                notifyIcon1.Visible = true;
            }

            this.Enabled = !bShowWindow;
            //ArrayList AllData = parser.GetData(CountryID, CompanyVAT, ReadCode, WriteCode);
            ArrayList AllData = parser.GetData(Company_Info);
            dblayer.Current_Company_Info = Company_Info;

            foreach (var item in AllData)
            {
                if (item.GetType() == typeof(KoteretTnua))
                {
                    KoteretTnua kt = (KoteretTnua)item;
                    kt.Data = kt.Data.Replace("'", "''");
                    try
                    {
                        if (dblayer.AddCreateInboxRecord(kt, Company_Info.WriteCode, ""))
                        {
                            //inboxTableAdapter.Insert(kt.TransactionGUID, kt.CountryIDFrom, kt.VatFrom, kt.CountryIDTo, kt.VatTo, kt.Data, kt.TimeStampWrite, DateTime.Now, false, Company_Info.WriteCode, 0, false, "", Company_Info.CompanyCountryID, Company_Info.CompanyVAT, false, false, false);
                            parser.ConfirmTransaction(kt.TransactionGUID.ToString(), Company_Info.WriteCode);       //Dont Forget To active the transaction confirmation
                            DownloadFile(kt.TransactionGUID);
                        }
                    }
                    catch (Exception ex)
                    {
                        parser.ConfirmTransaction(kt.TransactionGUID.ToString(), WriteCode);
                    }
                }
                else if (item.GetType() == typeof(TochenTnua))
                {
                    TochenTnua tt = (TochenTnua)item;

                    try
                    {
                        dblayer.AddCreateInboxRecord(tt, Company_Info.WriteCode, "");
                        inboxTableAdapter.Insert(tt.TransactionGUID, tt.CountryIDFrom, tt.VatFrom, tt.CountryIDTo, tt.VatTo, tt.Data, tt.TimeStampWrite, DateTime.Now, false, Company_Info.WriteCode, 0, false, "", Company_Info.CompanyCountryID, Company_Info.CompanyVAT, false, false, false);
                        parser.ConfirmTransaction(tt.TransactionGUID.ToString(), Company_Info.WriteCode);       //Dont Forget To active the transaction confirmation
                    }
                    catch (Exception ex)
                    {
                        parser.ConfirmTransaction(tt.TransactionGUID.ToString(), WriteCode);
                    }
                }
                else if (item.GetType() == typeof(WriteCodeRequest))
                {
                    WriteCodeRequest wr = (WriteCodeRequest)item;

                    try
                    {
                        dblayer.AddCreateInboxRecord(wr, Company_Info.WriteCode, "");
                        //inboxTableAdapter.Insert(wr.TransactionGUID, wr.CountryIDFrom, wr.VatFrom, wr.CountryIDTo, wr.VatTo, wr.Data, wr.TimeStampWrite, DateTime.Now, false, Company_Info.WriteCode, 0, false,"", Company_Info.CompanyCountryID, Company_Info.CompanyVAT, false, false, false);
                        parser.ConfirmTransaction(wr.TransactionGUID.ToString(), Company_Info.WriteCode);       //Dont Forget To active the transaction confirmation
                        DownloadFile(wr.TransactionGUID);

                        try
                        {
                            dblayer.Current_Company_Info = Company_Info;
                            CompanyInfo company_info = dblayer.GetCompanyInfo(wr.CountryIDFrom.ToString(), wr.VatFrom);
                            company_info.WriteCode = wr.Answer;
                            dblayer.UpdateCompanyInfo(company_info);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            Company company = dblayer.GetCompany(wr.CountryIDFrom, wr.VatFrom);
                            company.WriteCode = wr.Answer;
                            dblayer.UpdateCompany(company);
                        }
                        catch (Exception)
                        {
                        }

                        //frmPopup popup = new frmPopup(PopupSkins.SmallInfoSkin);
                        //popup.ShowPopup("CMail", "You got a waiting request!", 300, 3000, 3000);
                        //ShareUtils.PlaySound(ShareUtils.SoundType.NewClientEntered);

                        OpenRequests(bShowWindow);
                    }
                    catch (Exception ex)
                    {
                        parser.ConfirmTransaction(wr.TransactionGUID.ToString(), WriteCode);
                    }
                }
            }

            ViewData();
            CheckOutboxFiltersState();
            this.Enabled = true;
            if (bShowWindow)
            {
                frmprocessrequest.Hide();
            }
            else
            {
                notifyIcon1.Visible = false;
            }

            btnGetData.Enabled = true;
            tmrMail.Enabled = true;
        }

        private bool Payment(CompanyInfo company_info, bool bAddCompany)
        {
            bool bResult = false;
            frmPayment frm = new frmPayment();
            frm.Company_Info = company_info;
            frm.ShowDialog();
            String Payment = null;

            if (frm.paid)
            {
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
            }

            if (frm.PaymentByPass)
            {
                frmprocessrequest.Show();
                company_info.CompanySerialNumber = dblayer.AddCompanyInfo(company_info);
                parser.CreateCompany(company_info, "no_payment|no_payment|no_payment", true);
                bResult = true;
            }

            return bResult;
        }

        private void DownloadFile(String TransactionGUID)
        {
            try
            {
                //TransientStorage\117\0287\028753390
                //string remoteUri = "http://192.168.10.250/GlobalInfoProtocol/TransientStorage/" + CountryID + "/" + CompanyVAT.Substring(0, 4) + "/" + CompanyVAT + "/";
                string remoteUri = ConfigurationManager.AppSettings["ServerUrl"] + "TransientStorage/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT.Substring(0, 4) + "/" + Company_Info.CompanyVAT + "/";
                //string fileName = TransactionGUID + ".pdf";

                //TODO::
                string fileName = parser.GetFileNameForDownload(Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, TransactionGUID);

                string myStringWebResource = null;
                string ServerPath = Application.StartupPath + @"\TransientStorage";

                if (!Directory.Exists(ServerPath))
                {
                    Directory.CreateDirectory(ServerPath);
                }

                if (Company_Info.CompanyCountryID != 0)
                {
                    if (!Directory.Exists(ServerPath + "/" + Company_Info.CompanyCountryID))
                    {
                        Directory.CreateDirectory(ServerPath + "/" + Company_Info.CompanyCountryID);
                    }
                }

                if (Company_Info.CompanyVAT != null)
                {
                    if (!Directory.Exists(ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT))
                    {
                        Directory.CreateDirectory(ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT);
                    }
                }

                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();

                // Concatenate the domain with the Web resource filename.
                myStringWebResource = remoteUri + fileName;
                Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);

                // Download the Web resource and save it into the current filesystem folder.                
                myWebClient.DownloadFile(myStringWebResource, ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT + "/" + fileName);

                Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
                Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + Application.StartupPath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            this.Enabled = false;

            //If more than 5 mails total ask for credit card on non commercial or no payment
            //GetBillingForThisMonth
            if (parser.IsCompanyHasCCToPay(Company_Info).ToLower() != true.ToString().ToLower())
            {
                String BillingInOut = parser.GetBillingForThisMonth(Company_Info);

                int out_mails_used = Int32.Parse(BillingInOut.Split(new char[] { '|' })[1]);

                String SQL = "SELECT * from Haklada WHERE Transfered = 0";
                ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

                int MailsToSend = all_records.Count;

                if (MailsToSend == 0)
                {
                    return;
                }
                else if (out_mails_used + MailsToSend > 5)
                {
                    bool payment = false;

                    payment = Payment(Company_Info, false);
                    if (!payment)
                    {
                        return;
                    }
                }
            }

            frmprocessrequest.Show();
            SendOutboxData.Send(Company_Info, parser, dblayer);

            dblayer.CleanOutboxTable();
            dblayer.ReadShuratHakladaListMain(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, chkWaiting.Checked, chkTransferd.Checked, chkReaden.Checked, ActionsListDictionary, dtpFromOutbox.Value, dtpToOutbox.Value);
            dblayer.IsTransactionReaden(parser, lvwOutbox);
            frmprocessrequest.Hide();
            this.Enabled = true;
            btnSend.Enabled = true;
        }

        private void btnCreateActionCode_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateBusiness_Click(object sender, EventArgs e)
        {

        }
        private void ListView1_DrawColumnHeader(object sender, System.Windows.Forms.DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        public DBLayer dblayer = null;
        public bool bLoading { get; set; }

        //const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        //const int SET_FEATURE_ON_PROCESS = 0x00000002;

        //[DllImport("urlmon.dll")]
        //[PreserveSig]
        //[return: MarshalAs(UnmanagedType.Error)]
        //static extern int CoInternetSetFeatureEnabled(int FeatureEntry, [MarshalAs(UnmanagedType.U4)] int dwFlags, bool fEnable);

        //static void DisableClickSounds()
        //{
        //    CoInternetSetFeatureEnabled(
        //        FEATURE_DISABLE_NAVIGATION_SOUNDS,
        //        SET_FEATURE_ON_PROCESS,
        //        true);
        //}

        const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        const int SET_FEATURE_ON_THREAD = 0x00000001;
        const int SET_FEATURE_ON_PROCESS = 0x00000002;
        const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);

        static void DisableClickSounds()
        {
            CoInternetSetFeatureEnabled(
                FEATURE_DISABLE_NAVIGATION_SOUNDS,
                SET_FEATURE_ON_PROCESS,
                true);
        }

        //// backup value
        //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\Explorer\Navigating\.Current");
        //string BACKUP_keyValue = (string)key.GetValue(null);

        //// write nothing
        //key = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\Explorer\Navigating\.Current", true);
        //key.SetValue(null, "",  RegistryValueKind.ExpandString);

        //// do navigation ...

        //// write backup key
        //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\Explorer\Navigating\.Current", true);
        //key.SetValue(null, BACKUP_keyValue,  RegistryValueKind.ExpandString);

        public bool IncominMailCheckOnly { get; set; }

        private void frmCloudBox_Load(object sender, EventArgs e)
        {
            //if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed  && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            //{
            //}

            DisableClickSounds();
            bLoading = true;

            SetDoubleBuffered(lvwInbox);
            SetDoubleBuffered(lvwOutbox);

            PrepareIniFileName();
            PrepareImport();

            dblayer = new DBLayer(Application.StartupPath + @"\App_Data\");

            SetTitle();

            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.inbox' table. You can move, or remove it, as needed.
            //this.inboxTableAdapter.Fill(this.localInfoProtocolDataSet.Inbox);

            for (int i = (DateTime.Now.Year - 7); i < (DateTime.Now.Year + 1); i++)
            {
                cmbYearsInbox.Items.Add(i.ToString());
                cmbYearsOutbox.Items.Add(i.ToString());
            }

            cmbYearsInbox.Text = DateTime.Now.Year.ToString();
            cmbYearsOutbox.Text = DateTime.Now.Year.ToString();

            for (int i = 0; i < 13; i++)
            {
                cmbMonthsInbox.Items.Add(i.ToString());
                cmbMonthsOutbox.Items.Add(i.ToString());
            }

            cmbMonthsInbox.Text = DateTime.Now.Month.ToString();
            cmbMonthsOutbox.Text = DateTime.Now.Month.ToString();

            //dblayer.ReadShuratHakladaList(lvwOutbox, CountryID, CompanyVAT);

            bLoading = false;

            //Do not Change The Lines Order!!!!!
            ReadIniFile();

            //Do not Change The Lines Order!!!!!
            GetActionsList();

            //Do not Change The Lines Order!!!!!
            ViewData();

            CheckOutboxFiltersState();
            titleBar1.Company_Info = Company_Info;

            SetControls();
            //if (frmprocessrequest != null)
            //{
            //    frmprocessrequest.Close();
            //}

            if (!TermOfUse)
            {
                tabMain.TabPages.RemoveAt(1);
                webBrowser1.Url = new Uri("http://adirim.info/GlobalInfoProtocol/Advertisment.aspx");
            }
            else
            {
                webBrowser1.Visible = false;
                tabMain.Height = tabMain.Height + 105;
            }

            notifyIcon1.Icon = global::Pulsar.Properties.Resources.Clock;

            GetMail();

            if (IncominMailCheckOnly)
            {
                tmrMail.Enabled = false;
                this.Close();
            }

            tmrMail.Interval = MailInterval;
        }

        private void SetControls()
        {
            pnlInboxFilter.Left = pnlOutboxFilter.Left;

            grpFilterInbox.Left = (this.Width - grpFilterInbox.Width) / 2;
            grpFilterOutbox.Left = grpFilterInbox.Left;
        }

        private void PrepareImport()
        {
            iniFile.IniWriteValue("InboxImport", "F1", "5");
            iniFile.IniWriteValue("InboxImport", "F2", "8");
            iniFile.IniWriteValue("InboxImport", "F3", "12");
            iniFile.IniWriteValue("InboxImport", "F4", "9");
            iniFile.IniWriteValue("InboxImport", "F5", "4");
        }

        private void SetTitle()
        {
            if (this.Text.IndexOf(CompanyVAT) == -1)
            {
                lvwOutbox.Items.Clear();
                lvwInbox.Items.Clear();
            }

            //this.Text = "Company: " + this.CompanyName + " [" + CountryID + " - " + CompanyVAT + "]";
            this.Text = "VAT: " + Company_Info.CompanyVAT + " Company: " + Company_Info.CompanyName;
        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            //this.CancelButton = btnExitFilter;
            ReadIniFile();
            tabMain.Enabled = false;
            grpFilterInbox.Visible = true;
        }

        private void ViewData()
        {
            dblayer.ReadKotertTnuaListMain(parser, lvwInbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, ActionsListDictionary, dtpFromInbox.Value, dtpToInbox.Value);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain frm = new frmMain();
            frm.CancelSelection = false;
            frm.Company_Info = Company_Info;
            frm.form_mode = Pulsar.frmMain.form_mode_type.Selection;
            frm.SettingsMode = true;
            frm.ShowDialog();
            this.Show();
            if (frm.lvwCompaniesInfoList.SelectedIndices.Count == 1)
            {
                if (frm.Company_Info != null)
                {
                    if (!frm.CancelSelection)
                    {
                        this.Company_Info = frm.Company_Info;
                    }
                    this.CompanyName = frm.txtCompanyName.Text;
                    this.CountryID = frm.txtCountryID.Text;
                    this.CompanyVAT = frm.txtCompanyVAT.Text;
                    this.ReadCode = frm.txtReadCode.Text;
                    this.WriteCode = frm.txtWriteCode.Text;
                    this.Maam = frm.txtMaam.Text;
                }
            }
            frm.Close();

            SetTitle();

            RefreshTabs();
            //this.parser = frm.parser;

            //this.Close();
        }

        private void btnCreateActions_Click(object sender, EventArgs e)
        {
            frmHaklada frm = new frmHaklada();
            frm.ActionsListDictionary = ActionsListDictionary;
            frm.Company_Info = Company_Info;
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.CountryID = CountryID;
            frm.CompanyVAT = CompanyVAT;
            frm.ReadCode = ReadCode;
            frm.WriteCode = WriteCode;
            frm.Maam = Maam;
            frm.parser = parser;
            frm.ShowDialog();
            dblayer.ReadShuratHakladaListMain(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, chkWaiting.Checked, chkTransferd.Checked, chkReaden.Checked, ActionsListDictionary, dtpFromOutbox.Value, dtpToOutbox.Value);

            if (lvwOutbox.SelectedIndices.Count == 1)
            {
                ShuratHaklada shurat_haklada = (ShuratHaklada)GetListViewObj(lvwOutbox);
                dblayer.ReadTochenTnuaForShuratHakladaListMain(lvwTochenTnuaOutbox, shurat_haklada.TransactionGUID);
            }
            else
            {
                lvwTochenTnuaOutbox.Items.Clear();
            }

            dblayer.IsTransactionReaden(parser, lvwOutbox);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inboxBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.inboxBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.localInfoProtocolDataSet);

        }

        private void fileFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp(frmHelp.eHelpType.ImportKotertTnua);
        }

        private void actionTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmActionType frm = new frmActionType();
            frm.Company_Info = Company_Info;
            frm.ShowDialog();

            GetActionsList();
        }

        private void GetActionsList()
        {
            ActionsListDictionary.Clear();

            foreach (ArrayList record in dblayer.ActionsList())
            {
                SugTnua sug_tnua = new SugTnua();
                sug_tnua.ActionCode = Convert.ToInt32(record[0]);
                sug_tnua.ActionTypeIN = record[1].ToString();
                sug_tnua.ActionTypeOUT = record[2].ToString();
                sug_tnua.Description = record[3].ToString();
                sug_tnua.ActionCodeOut = Convert.ToInt32(record[4]);
                sug_tnua.DescriptionOut = record[5].ToString();

                ActionsListDictionary.Add(Convert.ToInt32(record[0]), sug_tnua);
            }
        }

        private void businessListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBusinessIndexTable();
        }

        private void LoadBusinessIndexTable()
        {
            frmBusinessIndexTable frm = new frmBusinessIndexTable();
            frm.parser = parser;
            frm.Company_Info = Company_Info;
            frm.CountryID = CountryID;
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            tmrMail.Enabled = false;
            this.Close();
            //this.Hide();
            //frmMain frm = new frmMain();
            //frm.CancelSelection = false;
            //frm.Company_Info = Company_Info;
            //frm.form_mode = Pulsar.frmMain.form_mode_type.Selection;
            //frm.SettingsMode = true;
            //frm.ShowDialog();
            //this.Show();
            //if (frm.lvwCompaniesInfoList.SelectedIndices.Count == 1)
            //{
            //    if (frm.Company_Info != null)
            //    {
            //        if (!frm.CancelSelection)
            //        {
            //            this.Company_Info = frm.Company_Info;
            //        }
            //        this.CompanyName = frm.txtCompanyName.Text;
            //        this.CountryID = frm.txtCountryID.Text;
            //        this.CompanyVAT = frm.txtCompanyVAT.Text;
            //        this.ReadCode = frm.txtReadCode.Text;
            //        this.WriteCode = frm.txtWriteCode.Text;
            //        this.Maam = frm.txtMaam.Text;
            //    }
            //}
            //frm.Close();

            //SetTitle();

            //RefreshTabs();
            //this.parser = frm.parser;

            //this.Close();
        }

        private void btnCloseFilter_Click(object sender, EventArgs e)
        {
            grpFilterInbox.Visible = false;
        }

        private void cmbYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetInboxDatePickers();
        }

        private void cmbMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetInboxDatePickers();
        }

        private void SetInboxDatePickers()
        {
            if (!bLoading)
            {
                if (cmbMonthsInbox.Text == "0")
                {
                    dtpFromInbox.Value = new DateTime(Int32.Parse(cmbYearsInbox.Text), 1, 1);
                    dtpToInbox.Value = new DateTime(Int32.Parse(cmbYearsInbox.Text), 12, 1).AddMonths(1).AddDays(-1);
                }
                else
                {
                    dtpFromInbox.Value = new DateTime(Int32.Parse(cmbYearsInbox.Text), Int32.Parse(cmbMonthsInbox.Text), 1);
                    dtpToInbox.Value = new DateTime(Int32.Parse(cmbYearsInbox.Text), Int32.Parse(cmbMonthsInbox.Text), 1).AddMonths(1).AddDays(-1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grpFilterInbox.Visible = false;
            tabMain.Enabled = true;
            //this.CancelButton = btnExit;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTabs();
        }

        private void RefreshTabs()
        {
            //switch ((TabType)((System.Windows.Forms.TabControl)(sender)).SelectedTab.TabIndex)                    
            switch ((TabType)tabMain.SelectedTab.TabIndex)
            {
                case TabType.Inbox:
                    ViewData();
                    pnlInboxButtons.Visible = true;
                    pnlOutboxButtons.Visible = false;
                    pnlOutboxFilter.Visible = false;
                    pnlInboxFilter.Visible = true;
                    break;
                case TabType.Outbox:
                    pnlInboxButtons.Visible = false;
                    pnlOutboxButtons.Visible = true;
                    pnlOutboxFilter.Visible = true;
                    pnlInboxFilter.Visible = false;
                    FillOutbox();
                    break;
                default:
                    break;
            }

            CheckOutboxFiltersState();
        }

        private void FillOutbox()
        {
            lvwOutbox.SuspendLayout();
            dblayer.ReadShuratHakladaListMain(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, chkWaiting.Checked, chkTransferd.Checked, chkReaden.Checked, ActionsListDictionary, dtpFromOutbox.Value, dtpToOutbox.Value);

            dblayer.IsTransactionReaden(parser, lvwOutbox);
            lvwOutbox.ResumeLayout();
        }

        public bool bOutboxFilter { get; set; }

        private void btnFilterOutbox_Click(object sender, EventArgs e)
        {
            bOutboxFilter = true;

            radDayOutboxPnl.Checked = radDayOutbox.Checked;
            radWeekOutboxPnl.Checked = radWeekOutbox.Checked;
            radMonthOutboxPnl.Checked = radMonthOutbox.Checked;
            radYearOutboxPnl.Checked = radYearOutbox.Checked;

            FilterOutbox();
            bOutboxFilter = false;
        }

        private void FilterOutbox()
        {
            grpFilterOutbox.Visible = false;
            SaveIniFile();
            FillOutbox();
            tabMain.Enabled = true;
        }

        private void btnExitFilterOutbox_Click(object sender, EventArgs e)
        {
            tabMain.Enabled = true;
            grpFilterOutbox.Visible = false;
        }

        private void btnViewDataOutbox_Click(object sender, EventArgs e)
        {
            ReadIniFile();
            tabMain.Enabled = false;
            grpFilterOutbox.Visible = true;
        }

        private void radDayOutbox_CheckedChanged(object sender, EventArgs e)
        {
            radDayOutboxFilter();
        }

        private void radDayOutboxFilter()
        {
            dtpFromOutbox.Value = DateTime.Today;
            dtpToOutbox.Value = DateTime.Today;
        }

        private void radWeekOutbox_CheckedChanged(object sender, EventArgs e)
        {
            radWeekOutboxFilter();
        }

        private void radWeekOutboxFilter()
        {
            dtpFromOutbox.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-(int)DateTime.Today.DayOfWeek);
            dtpToOutbox.Value = dtpFromOutbox.Value.AddDays(6);
        }

        private void radMonthOutbox_CheckedChanged(object sender, EventArgs e)
        {
            radMonthOutboxFilter();
        }

        private void radMonthOutboxFilter()
        {
            if (cmbMonthsOutbox.SelectedIndex == -1)
                return;

            cmbYearsOutbox.Text = DateTime.Today.Year.ToString();
            cmbMonthsOutbox.SelectedIndex = 0;
            cmbMonthsOutbox.SelectedIndex = DateTime.Today.Month;
        }

        private void radYearOutbox_CheckedChanged(object sender, EventArgs e)
        {
            radYearOutboxFilter();
        }

        private void radYearOutboxFilter()
        {
            if (cmbMonthsOutbox.Items.Count == 0)
                return;

            cmbYearsOutbox.Text = DateTime.Today.Year.ToString();
            cmbMonthsOutbox.SelectedIndex = cmbMonthsInbox.SelectedIndex % 12 + 1;
            cmbMonthsOutbox.SelectedIndex = 0;
        }

        private void radDayInbox_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromInbox.Value = DateTime.Today;
            dtpToInbox.Value = DateTime.Today;
        }

        private void radWeekInbox_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromInbox.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-(int)DateTime.Today.DayOfWeek);
            dtpToInbox.Value = dtpFromInbox.Value.AddDays(6);
        }

        private void radMonthInbox_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbMonthsInbox.SelectedIndex == -1)
                return;

            cmbYearsInbox.Text = DateTime.Today.Year.ToString();
            cmbMonthsInbox.SelectedIndex = 0;
            cmbMonthsInbox.SelectedIndex = DateTime.Today.Month;
        }

        private void radYearInbox_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbMonthsInbox.Items.Count == 0)
                return;

            cmbYearsInbox.Text = DateTime.Today.Year.ToString();
            cmbMonthsInbox.SelectedIndex = cmbMonthsInbox.SelectedIndex % 12 + 1;
            cmbMonthsInbox.SelectedIndex = 0;
        }

        public int Index { get; set; }
        String[] propelor = new String[] { "|", "/", "-", "\\" };

        private void UploadFileFTP(string filename, String TransactionGuid, string countryIDTo, string companyVatTo)
        {
            String FileExt = Path.GetExtension(filename);
            String FileName = Path.GetFileName(filename);
            String FilePath = Path.GetDirectoryName(filename);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.FileName = Application.StartupPath + @"\FTPTrans.exe";
            startInfo.Arguments = "\"/ftpServerIP:adirim.info\" \"/ftpUserID:adi\" \"/ftpPassword:9363\" \"/filename:" + FileName + "\" \"/cmd:upload\" \"/path:" + FilePath + "/\" \"/targetpath:GlobalInfoTransfer/\" \"/targetfilename:" + TransactionGuid + FileExt + "\" \"/countryid:" + countryIDTo + "\" \"/companyvat:" + companyVatTo + "\"";
            Process.Start(startInfo);
        }

        ///// <summary>
        ///// Upload any file to the web service; this function may be
        ///// used in any application where it is necessary to upload
        ///// a file through a web service
        ///// </summary>
        ///// <param name="filename">Pass the file path to upload</param>
        //private void UploadFile(string filename, String TransactionGuid, string countryIDTo, string companyVatTo)
        //{
        //    // do nothing
        //    notifyIcon1.Icon = this.Icon;
        //    notifyIcon1.BalloonTipTitle = "Uploading Data";
        //    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
        //    notifyIcon1.Visible = true;

        //    try
        //    {
        //        tmrUpload.Enabled = true;
        //        notifyIcon1.BalloonTipText = propelor[0];
        //        notifyIcon1.ShowBalloonTip(1000);
        //        Application.DoEvents();
        //        // get the exact file name from the path
        //        String TargetFile = System.IO.Path.GetFileName(filename);

        //        // create an instance fo the web service
        //        Uploader.FileUploader srv = new Uploader.FileUploader();

        //        // get the file information form the selected file
        //        FileInfo fInfo = new FileInfo(filename);

        //        // get the length of the file to see if it is possible
        //        // to upload it (with the standard 4 MB limit)
        //        long numBytes = fInfo.Length;
        //        double dLen = Convert.ToDouble(fInfo.Length / 1000000);

        //        // Default limit of 4 MB on web server
        //        // have to change the web.config to if
        //        // you want to allow larger uploads
        //        if (dLen < 10)
        //        {
        //            // set up a file stream and binary reader for the 
        //            // selected file
        //            FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fStream);

        //            // convert the file to a byte array
        //            byte[] data = br.ReadBytes((int)numBytes);
        //            br.Close();

        //            //File.Copy(filename, TransactionGuid + "." + Path.GetExtension);
        //            // pass the byte array (file) and file name to the web service
        //            //TargetFile
        //            string sTmp = srv.UploadFile(data, TransactionGuid + Path.GetExtension(TargetFile), countryIDTo, companyVatTo); //"972", "513638346"
        //            fStream.Close();
        //            fStream.Dispose();

        //            tmrUpload.Enabled = false;

        //            // this will always say OK unless an error occurs,
        //            // if an error occurs, the service returns the error message
        //            //MessageBox.Show("File Upload Status: " + sTmp, "File Upload");
        //            notifyIcon1.BalloonTipText = "File Upload Status: " + sTmp;
        //            notifyIcon1.ShowBalloonTip(1000);
        //            Application.DoEvents();
        //        }
        //        else
        //        {
        //            notifyIcon1.BalloonTipText = "The file selected exceeds the size limit for uploads.";
        //            notifyIcon1.ShowBalloonTip(1000);
        //            Application.DoEvents();
        //            // Display message if the file was too large to upload
        //            //MessageBox.Show("The file selected exceeds the size limit for uploads.", "File Size");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // display an error message to the user
        //        //MessageBox.Show(ex.Message.ToString(), "Upload Error");
        //        notifyIcon1.BalloonTipText = ex.Message.ToString();
        //        notifyIcon1.ShowBalloonTip(1000);
        //        Application.DoEvents();
        //    }
        //    tmrUpload.Enabled = false;
        //}

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

        private void lvwInData_DoubleClick(object sender, EventArgs e)
        {
            ShowAttachment();
        }

        private void ShowAttachment()
        {
            try
            {
                String FileName = btnAttachment.Tag.ToString();
                if (File.Exists(FileName))
                {
                    Process.Start(FileName);
                }
            }
            catch (Exception)
            {
            }
            //if (lvwInbox.SelectedIndices.Count == 1)
            //{
            //    KoteretTnua kt = (KoteretTnua)lvwInbox.Items[lvwInbox.SelectedIndices[0]].Tag;
            //    String TransactionID = kt.TransactionGUID;
            //    try
            //    {
            //        //String FileName = Application.StartupPath + "/TransientStorage/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT + "/" + TransactionID + ".pdf";
            //        String FileName = btnAttachment.Tag.ToString();
            //        if (File.Exists(FileName))
            //        {
            //            Process.Start(FileName);
            //        }
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
        }

        private void btnAttachment_Click(object sender, EventArgs e)
        {
            ShowAttachment();
        }

        private void lvwInData_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAttachment.Visible = false;
            if (lvwInbox.SelectedIndices.Count == 1)
            {
                grpDetails.Enabled = true;
                try
                {
                    ShuratHaklada shurat_haklada = (ShuratHaklada)GetListViewObj(lvwOutbox);
                    dblayer.ReadTochenTnuaForShuratHakladaInboxListMain(lvwTochenTnuaInbox, shurat_haklada.TransactionGUID);
                }
                catch (Exception)
                {
                }

                object item = GetListViewObj(lvwInbox);

                DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "/TransientStorage/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT + "/");
                FileInfo[] fi = di.GetFiles(((RecordHeader)item).TransactionGUID + ".*");
                if (fi.Length > 0)
                {
                    btnAttachment.Tag = Application.StartupPath + "/TransientStorage/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT + "/" + fi[0];
                }
                //btnAttachment.Tag = Application.StartupPath + "/TransientStorage/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT + "/" + kt.TransactionGUID + ".pdf";
                btnAttachment.Visible = CheckAttachment(); //kt.TransactionGUID
                btnAddCompany.Visible = lvwInbox.Items[lvwInbox.SelectedIndices[0]].BackColor != Color.White;

                if (item.GetType() == typeof(KoteretTnua))
                {
                    KoteretTnua kt = (KoteretTnua)item;
                    chkConfirm0.Checked = kt.Confirm0;
                    chkConfirm1.Checked = kt.Confirm1;
                    chkConfirm2.Checked = kt.Confirm2;
                }
            }
            else
            {
                grpDetails.Enabled = false;
                chkConfirm0.Checked = false;
                chkConfirm1.Checked = false;
                chkConfirm2.Checked = false;

                btnAddCompany.Visible = false;
            }
        }

        private object GetListViewObj(ListView lvw)
        {
            if (lvw.SelectedIndices.Count == 0)
                return null;

            return lvw.Items[lvw.SelectedIndices[0]].Tag;
        }


        private void lvwOutbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAttachment.Visible = false;
            if (lvwOutbox.SelectedIndices.Count == 1)
            {
                ShuratHaklada shurat_haklada = (ShuratHaklada)GetListViewObj(lvwOutbox);
                dblayer.ReadTochenTnuaForShuratHakladaListMain(lvwTochenTnuaOutbox, shurat_haklada.TransactionGUID);

                btnDeleteOutboxRecord.Enabled = !shurat_haklada.Transfered;
                if (File.Exists(shurat_haklada.Attachment))
                {
                    btnAttachment.Visible = true;
                    btnAttachment.Tag = shurat_haklada.Attachment;
                }
                else
                {
                    btnAttachment.Visible = false;
                }
            }
            else
            {
                lvwTochenTnuaOutbox.Items.Clear();
            }
        }

        private bool CheckAttachment() //string TransactionID
        {
            if (btnAttachment.Tag == null)
                return false;

            if (btnAttachment.Tag.ToString() == null)
                return false;

            //String FileName = Application.StartupPath + "/TransientStorage/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT + "/" + TransactionID + ".pdf";
            return File.Exists(btnAttachment.Tag.ToString()); //FileName
        }

        private void btnDeleteInboxRecord_Click(object sender, EventArgs e)
        {
            btnDeleteInboxRecord.Enabled = false;
            if (lvwOutbox.SelectedIndices.Count == 1)
            {
                ShuratHaklada sh = (ShuratHaklada)GetListViewObj(lvwOutbox);
                dblayer.DeleteInbox(sh.TransactionGUID);
                ViewData();
            }
            btnDeleteInboxRecord.Enabled = true;
        }

        private void btnDeleteOutboxRecord_Click(object sender, EventArgs e)
        {
            btnDeleteOutboxRecord.Enabled = false;
            if (lvwOutbox.SelectedIndices.Count == 1)
            {
                ShuratHaklada sh = (ShuratHaklada)GetListViewObj(lvwOutbox);
                //dblayer.DeleteOutbox(sh.TransactionGUID);
                dblayer.DeleteOutboxHaklada(sh.TransactionGUID);
                FillOutbox();
            }
            btnDeleteOutboxRecord.Enabled = true;
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            AddCompany();
        }

        private void AddCompany()
        {
            KoteretTnua kt = (KoteretTnua)GetListViewObj(lvwInbox);
            frmBusinessIndex frm = new frmBusinessIndex();
            if (kt != null)
            {
                frm.CountryID = kt.CountryIDHaSholeah;
                frm.Company_Name = kt.ShemHaLakoh;
                frm.Company_VAT = kt.OsekMoorshehHaSholeah;
            }
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.ShowDialog();
        }

        private void cmbYearsOutbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOuboxDatePickers();
        }

        private void cmbMonthsOutbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOuboxDatePickers();
        }

        private void SetOuboxDatePickers()
        {
            if (!bLoading)
            {
                if (cmbMonthsOutbox.Text == "0")
                {
                    dtpFromOutbox.Value = new DateTime(Int32.Parse(cmbYearsOutbox.Text), 1, 1);
                    dtpToOutbox.Value = new DateTime(Int32.Parse(cmbYearsOutbox.Text), 12, 1).AddMonths(1).AddDays(-1);
                }
                else
                {
                    dtpFromOutbox.Value = new DateTime(Int32.Parse(cmbYearsOutbox.Text), Int32.Parse(cmbMonthsOutbox.Text), 1);
                    dtpToOutbox.Value = new DateTime(Int32.Parse(cmbYearsOutbox.Text), Int32.Parse(cmbMonthsOutbox.Text), 1).AddMonths(1).AddDays(-1);
                }
            }
        }

        private void chkWaiting_CheckedChanged(object sender, EventArgs e)
        {
            picWaitingOutboxFilter.Image = chkWaiting.Checked ? global::Pulsar.Properties.Resources.icon_ticket_history : global::Pulsar.Properties.Resources.icon_ticket_history_disable;
            CheckOutboxFiltersState();
        }

        private void Transferd_CheckedChanged(object sender, EventArgs e)
        {
            picTransferdOutboxFilter.Image = chkTransferd.Checked ? global::Pulsar.Properties.Resources.icon_download : global::Pulsar.Properties.Resources.icon_download_disable;
            CheckOutboxFiltersState();
        }

        private void chkReaden_CheckedChanged(object sender, EventArgs e)
        {
            picReadenOutboxFilter.Image = chkReaden.Checked ? global::Pulsar.Properties.Resources.icon_eye : global::Pulsar.Properties.Resources.icon_eye_disable;
            CheckOutboxFiltersState();
        }

        private void CheckOutboxFiltersState()
        {
            pnlOutboxFilter.Visible = (TabType)tabMain.SelectedIndex == TabType.Outbox;
        }

        private void btnTrasferToOutbox_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvwInbox.CheckedItems)
            {
                KoteretTnua kt = (KoteretTnua)lvi.Tag;
                dblayer.AddHakladaRecord(CreateShuratHaklada(kt));
            }
        }

        private ShuratHaklada CreateShuratHaklada(KoteretTnua kt)
        {
            ShuratHaklada shurat_haklada = new ShuratHaklada();
            shurat_haklada.TransactionGUID = "{" + Guid.NewGuid().ToString() + "}";

            CompanyInfo company_info = dblayer.GetCompanyInfo(Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT);   //Destination
            shurat_haklada.CompanyID = company_info.CompanyID;   //Destination
            shurat_haklada.ActionCode = Convert.ToInt32(kt.SugTnua);

            shurat_haklada.MisparMismach = kt.MisparMismach;
            shurat_haklada.TarichMismach = kt.TarichMismach;
            shurat_haklada.TarichAcher = kt.TarichAher;

            shurat_haklada.ActionDetails = kt.MeidaNosaf;
            shurat_haklada.AhuzHaMaam = kt.Maam;
            shurat_haklada.SchumPaturMaam = kt.SchumPaturMeMaam;

            shurat_haklada.SchumMaam = kt.SchumHaMaam;
            shurat_haklada.SchumKolelMaam = kt.SchumKolelMaam;
            shurat_haklada.Attachment = kt.TransactionGUID;

            shurat_haklada.Transfered = false;

            shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID; // kt.CountryIDFrom;
            shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT; // kt.VatFrom;

            shurat_haklada.Readen = false;

            shurat_haklada.DateSent = DateTime.Now;

            return shurat_haklada;
        }

        private void picWaitingFilter_Click(object sender, EventArgs e)
        {
            chkWaiting.Checked = !chkWaiting.Checked;
            FilterOutbox();
        }

        private void picTransferdFilter_Click(object sender, EventArgs e)
        {
            chkTransferd.Checked = !chkTransferd.Checked;
            FilterOutbox();
        }

        private void picReadenFilter_Click(object sender, EventArgs e)
        {
            chkReaden.Checked = !chkReaden.Checked;
            FilterOutbox();
        }

        private void dtpFromOutbox_ValueChanged(object sender, EventArgs e)
        {
            lblOutboxDateFrom.Text = dtpFromOutbox.Value.ToShortDateString();
        }

        private void dtpToOutbox_ValueChanged(object sender, EventArgs e)
        {
            lblOutboxDateTo.Text = dtpToOutbox.Value.ToShortDateString();
        }

        private void radDayOutboxPnl_Click(object sender, EventArgs e)
        {
            if (bOutboxFilter)
                return;

            radDayOutboxFilter();
            radDayOutbox.Checked = radDayOutboxPnl.Checked;
            SaveIniFile();
            FilterOutbox();
        }

        private void radWeekOutboxPnl_Click(object sender, EventArgs e)
        {
            if (bOutboxFilter)
                return;

            radWeekOutboxFilter();
            radWeekOutbox.Checked = radWeekOutboxPnl.Checked;
            SaveIniFile();
            FilterOutbox();
        }

        private void radMonthOutboxPnl_Click(object sender, EventArgs e)
        {
            if (bOutboxFilter)
                return;

            radMonthOutboxFilter();
            radMonthOutbox.Checked = radMonthOutboxPnl.Checked;
            SaveIniFile();
            FilterOutbox();
        }

        private void radYearOutboxPnl_Click(object sender, EventArgs e)
        {
            if (bOutboxFilter)
                return;

            radYearOutboxFilter();
            radYearOutbox.Checked = radYearOutboxPnl.Checked;
            SaveIniFile();
            FilterOutbox();
        }

        private void btnImportInbox_Click(object sender, EventArgs e)
        {

        }

        private void picFilter_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void picFilter_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void picFilter_MouseDown(object sender, MouseEventArgs e)
        {
            using (MemoryStream ms = new MemoryStream(Properties.Resources.closedhand))
            {
                this.Cursor = new Cursor(ms);
            }
        }

        private void picFilter_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void dtpOutbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            FixDateEnter(e.KeyChar);
        }


        private void dtpInbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            FixDateEnter(e.KeyChar);
        }

        private void FixDateEnter(char KeyChar)
        {
            if (KeyChar == 13)
            {
                SendKeys.Send("{LEFT}");
            }
        }

        private void lvwInData_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            //e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            if (((e.State & ListViewItemStates.Focused) != 0) &&
                ((e.State & ListViewItemStates.Selected) != 0)
               )
            {
                if (lvwInbox.SelectedIndices.Count > 0)
                {
                    // Draw the background and focus rectangle for a selected item.
                    Rectangle rect = new Rectangle(e.Bounds.X + e.Item.ImageList.Images[0].Width * 2, e.Bounds.Y, e.Bounds.Width - e.Item.ImageList.Images[0].Width, e.Bounds.Height);
                    e.Graphics.FillRectangle(shadowBrush, rect);
                    //e.DrawFocusRectangle();
                }
            }

            // Draw the item text for views other than the Details view. 
            if (lvwInbox.View != View.Details)
            {
                e.DrawText();
            }
        }

        private void lvwInData_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.Left;

            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default 
                // to Left if it has not been set to Center or Right. 
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        flags = TextFormatFlags.HorizontalCenter;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        flags = TextFormatFlags.Right;
                        break;
                }

                // Unless the item is selected, draw the standard  
                // background to make it stand out from the gradient. 
                //if ((e.ItemState & ListViewItemStates.Selected) == 0)
                //{
                //    e.DrawBackground();
                //}

                if (((e.ItemState & ListViewItemStates.Focused) != 0) &&
                    ((e.ItemState & ListViewItemStates.Selected) != 0) &&
                    (lvwInbox.SelectedIndices.Count > 0)
                   )
                {
                    //e.DrawBackground();
                    e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.White, e.Bounds, sf);
                }
                else
                {
                    e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.Black, e.Bounds, sf);
                }

                if (e.ColumnIndex == 0)
                {
                    // Draw the subitem text in red to highlight it. 
                    //e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.Red, e.Bounds, sf);
                    if (e.Item.ImageIndex != -1)
                    {
                        e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);
                    }
                }
                else
                {
                    // Draw normal text for a subitem with a nonnegative  
                    // or nonnumerical value.
                    //e.DrawText(flags);
                }
            }
        }

        private void lvwInData_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            //using (StringFormat sf = new StringFormat())
            //{
            //    // Store the column text alignment, letting it default
            //    // to Left if it has not been set to Center or Right.
            //    switch (e.Header.TextAlign)
            //    {
            //        case HorizontalAlignment.Center:
            //            sf.Alignment = StringAlignment.Center;
            //            break;
            //        case HorizontalAlignment.Right:
            //            sf.Alignment = StringAlignment.Far;
            //            break;
            //    }

            //    // Draw the standard header background.
            //    // e.DrawBackground();

            //    // Draw the header text.
            //    //using (Font headerFont = new Font("Courier New", 8, FontStyle.Regular))
            //    //{
            //    //    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
            //    //}

            //    e.Graphics.DrawString(e.Header.Text, lvwInData.Font, Brushes.Black, e.Bounds, sf);
            //}
            //return;

        }

        private void lvwOutbox_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        public SolidBrush shadowBrush { get; set; }

        private void lvwOutbox_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            //if ((e.State & ListViewItemStates.Selected) != 0)
            //{
            //    if (lvwOutbox.SelectedIndices.Count > 0)
            //    {
            //        if (lvwOutbox.SelectedIndices.Contains(e.ItemIndex))
            //        {
            if (((e.State & ListViewItemStates.Focused) != 0) &&
                ((e.State & ListViewItemStates.Selected) != 0)
               )
            {
                if (lvwOutbox.SelectedIndices.Count > 0)
                {
                    // Draw the background and focus rectangle for a selected item.
                    Rectangle rect = new Rectangle(e.Bounds.X + e.Item.ImageList.Images[0].Width * 2, e.Bounds.Y, e.Bounds.Width - e.Item.ImageList.Images[0].Width * 2, e.Bounds.Height);
                    e.Graphics.FillRectangle(shadowBrush, rect);
                    //e.DrawFocusRectangle();
                }
            }

            //else
            //{
            //    // Draw the background for an unselected item. 
            //    using (LinearGradientBrush brush =
            //        new LinearGradientBrush(e.Bounds, Color.Orange,
            //        Color.Maroon, LinearGradientMode.Horizontal))
            //    {
            //        e.Graphics.FillRectangle(brush, e.Bounds);
            //    }
            //}

            // Draw the item text for views other than the Details view. 
            if (lvwOutbox.View != View.Details)
            {
                e.DrawText();
            }

            ////e.DrawDefault = true;            
            //lvwOutbox.SuspendLayout();

            //if (lvwOutbox.SelectedItems.Count > 0 && e.Item.Text == lvwOutbox.SelectedItems[0].Text)
            //{
            //    e.Graphics.FillRectangle(shadowBrush, e.Bounds);
            //    e.DrawFocusRectangle();
            //}
            //else
            //{
            //    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //}
            //lvwOutbox.ResumeLayout();
        }

        private void lvwOutbox_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListView lvw = (ListView)sender;

            ShuratHaklada shurat_haklada = (ShuratHaklada)lvwOutbox.Items[e.ItemIndex].Tag;

            TextFormatFlags flags = TextFormatFlags.Left;

            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default 
                // to Left if it has not been set to Center or Right. 
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        flags = TextFormatFlags.HorizontalCenter;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        flags = TextFormatFlags.Right;
                        break;
                }

                if (((e.ItemState & ListViewItemStates.Focused) != 0) &&
                    ((e.ItemState & ListViewItemStates.Selected) != 0) &&
                    (lvwOutbox.SelectedIndices.Count > 0)
                  )
                {
                    //e.DrawBackground();
                    e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.White, e.Bounds, sf);
                }
                else
                {
                    e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.Black, e.Bounds, sf);
                }

                if (e.ColumnIndex == 0)
                {
                    Point pt;
                    // Draw the subitem text in red to highlight it. 
                    //e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.Red, e.Bounds, sf);
                    if (e.Item.ImageIndex != -1)
                        e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);

                    //ShuratHaklada shurat_haklada = (ShuratHaklada)lvwOutbox.Items[e.ItemIndex].Tag;
                    if (File.Exists(shurat_haklada.Attachment))
                    {
                        pt = new Point(e.SubItem.Bounds.Location.X + e.Item.ImageList.Images[0].Width, e.SubItem.Bounds.Location.Y);
                        e.Graphics.DrawImage(e.Item.ImageList.Images[5], pt);
                    }

                    pt = new Point(e.SubItem.Bounds.Location.X + e.Item.ImageList.Images[0].Width * 2, e.SubItem.Bounds.Location.Y);
                    e.Graphics.DrawImage(e.Item.ImageList.Images[((String.IsNullOrEmpty(shurat_haklada.WriteCode)) ? 7 : 8)], pt); //Key image/ missing

                    //TODO: ZManTK
                }
                else
                {
                    // Draw normal text for a subitem with a nonnegative  
                    // or nonnumerical value.
                    //e.DrawText(flags);
                }
                // Unless the item is selected, draw the standard  
                // background to make it stand out from the gradient. 
                //if ((e.ItemState & ListViewItemStates.Selected) == 0)
                //{
                //    e.DrawBackground();
                //}

                //if (e.ColumnIndex == 0)
                //{
                //    // Draw the subitem text in red to highlight it. 
                //    e.Graphics.DrawString(e.SubItem.Text, lvwOutbox.Font, Brushes.Red, e.Bounds, sf);
                //    if (e.Item.ImageIndex != -1)
                //    {
                //        e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);
                //    }

                //    ShuratHaklada shurat_haklada = (ShuratHaklada)lvwOutbox.Items[e.ItemIndex].Tag;
                //    if (File.Exists(shurat_haklada.Attachment))
                //    {
                //        Point pt = new Point(e.SubItem.Bounds.Location.X + e.Item.ImageList.Images[0].Width, e.SubItem.Bounds.Location.Y);
                //        e.Graphics.DrawImage(e.Item.ImageList.Images[5], pt);
                //    }
                //}
                //else
                //{
                //    // Draw normal text for a subitem with a nonnegative  
                //    // or nonnumerical value.
                //    e.DrawText(flags);
                //}
            }
            ////e.DrawDefault = true;

            //TextFormatFlags flags = TextFormatFlags.Left;

            //using (StringFormat sf = new StringFormat())
            //{
            //    switch (e.Header.TextAlign)
            //    {
            //        case HorizontalAlignment.Center:
            //            sf.Alignment = StringAlignment.Center;
            //            flags = TextFormatFlags.HorizontalCenter;
            //            break;
            //        case HorizontalAlignment.Right:
            //            sf.Alignment = StringAlignment.Far;
            //            flags = TextFormatFlags.Right;
            //            break;
            //        case HorizontalAlignment.Left:
            //            sf.Alignment = StringAlignment.Near;
            //            flags = TextFormatFlags.Left;
            //            break;
            //    }

            //    if ((e.ItemState & ListViewItemStates.Selected) == 0)
            //    {
            //        e.DrawBackground();
            //    }

            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.Item.ImageIndex != -1)
            //        {
            //            e.DrawBackground();
            //            e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);
            //            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), e.SubItem.Bounds.Location.X + this.imageList1.Images[0].Width, e.SubItem.Bounds.Location.Y);
            //        }
            //    }
            //    else
            //    {
            //        //e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), e.SubItem.Bounds.Location.X + this.imageList1.Images[0].Width, e.SubItem.Bounds.Location.Y);
            //        e.DrawText(flags);
            //        e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);
            //        //e.DrawText();
            //    }
            //}
        }

        //private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        //{
        //    DateTime rs;
        //    CultureInfo ci = new CultureInfo("en-IE");

        //    if (!DateTime.TryParseExact(this.maskedTextBox1.Text, "dd/MM/yyyy", ci, DateTimeStyles.None, out rs))
        //    {
        //        e.Cancel = true;
        //    }
        //}
        //public String GetCompanyData(String CountryID, String CompanyVAT, String WriteCode)
        //{
        //    //localInfoProtocolDataSet.inbox.Select(
        //}
        //public String GetCompanyData(String CountryID, String CompanyVAT, String WriteCode)
        //{
        //    //String SQL = "SELECT *";
        //    //SQL += " FROM inbox";
        //    //SQL += " WHERE (CountryIDTo = " + CountryID + ") AND (CompanyVATTo = '" + CompanyVAT + "') AND (WriteCode = '" + WriteCode + "')";

        //    //return ExpoertSQLLines(SQL);
        //}

        //public String ExpoertSQLLines(String SQL)
        //{            
        //    OleDbDataReader reader = returnData(SQL, connectionString);
        //    String line = "";
        //    //int counter = 0;
        //    while (reader.Read())
        //    {
        //        //counter++;
        //        for (int i = 0; i < reader.FieldCount; i++)
        //        {
        //            line += reader[i].ToString() + "|$";
        //        }
        //        line += Environment.NewLine; // @"\r\n";
        //    }
        //    reader.Close();
        //    CloseDB();
        //    //line = counter + Environment.NewLine + line;

        //    return line;
        //}

        //public static OleDbDataReader returnData(String SQL, String connectionString)
        //{
        //    //"server=(local);database=[somedatabase];user id=sa;password=sa"

        //    //SqlDataReader
        //    OleDbDataReader dr = null;

        //    //SqlConnection
        //    if (m_conn == null)
        //    {
        //        m_conn = new OleDbConnection(connectionString);
        //    }

        //    if (m_conn.State != ConnectionState.Open)
        //    {
        //        m_conn.Open();
        //    }

        //    try
        //    {
        //        //SqlCommand
        //        OleDbCommand cmd = new OleDbCommand(SQL, m_conn);

        //        //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); return dr;
        //        dr = cmd.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Console.Write(ex.Message);
        //    }

        //    return dr;
        //}


        // Resets the item tags.  
        void lvwOutbox_Invalidated(object sender, InvalidateEventArgs e)
        {
            //foreach (ListViewItem item in lvwOutbox.Items)
            //{
            //    if (item == null) return;
            //    //item.Tag = null;
            //}
        }

        // Resets the item tags.  
        void lvwInData_Invalidated(object sender, InvalidateEventArgs e)
        {
            //foreach (ListViewItem item in lvwInData.Items)
            //{
            //    if (item == null) return;
            //    //item.Tag = null;
            //}
        }

        private void lvwInData_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            lvwInbox.Invalidate();
        }

        private void lvwOutbox_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            lvwOutbox.Invalidate();
        }

        private void lvwInbox_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == _lockColumnIndex)
            {
                //Keep the width not changed.
                e.NewWidth = this.lvwInbox.Columns[e.ColumnIndex].Width;
                //Cancel the event.
                e.Cancel = true;
            }
        }

        private void lvwOutbox_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == _lockColumnIndex)
            {
                //Keep the width not changed.
                e.NewWidth = this.lvwOutbox.Columns[e.ColumnIndex].Width;
                //Cancel the event.
                e.Cancel = true;
            }
        }

        private void lvwOutbox_DoubleClick(object sender, EventArgs e)
        {
            ShowAttachment();
        }

        private void changeSearchFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Company_Info.FilesSearch != null) && (Company_Info.FilesSearch != ""))
            {
                folderBrowserDialog1.SelectedPath = Company_Info.FilesSearch;
            }

            folderBrowserDialog1.ShowNewFolderButton = true;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Company_Info.FilesSearch = folderBrowserDialog1.SelectedPath;
                dblayer.UpdateCompanyInfo(Company_Info);
            }
        }

        private void frmCloudBox_Activated(object sender, EventArgs e)
        {
            if (frmprocessrequest != null)
            {
                frmprocessrequest.Hide();
            }
        }

        private void btnCreateInboxAction_Click(object sender, EventArgs e)
        {
            btnCreateInboxAction.Enabled = false;
            frmHakladaInbox frm = new frmHakladaInbox();
            frm.ActionsListDictionary = ActionsListDictionary;
            frm.Company_Info = Company_Info;
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.parser = parser;
            frm.ShowDialog();
            ViewData();
            CheckOutboxFiltersState();
            btnCreateInboxAction.Enabled = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        public bool bInboxFilter { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            grpFilterInbox.Visible = false;

            radDayInboxPnl.Checked = radDayInbox.Checked;
            radWeekInboxPnl.Checked = radWeekInbox.Checked;
            radMonthInboxPnl.Checked = radMonthInbox.Checked;
            radYearInboxPnl.Checked = radYearInbox.Checked;

            ViewData();
            SaveIniFile();
            tabMain.Enabled = true;
        }

        private void radDayInboxPnl_Click(object sender, EventArgs e)
        {
            if (bInboxFilter)
                return;

            radDayInboxFilter();
            radDayInbox.Checked = radDayInboxPnl.Checked;
            SaveIniFile();
            ViewData();
        }

        private void radDayInboxFilter()
        {
            dtpFromInbox.Value = DateTime.Today;
            dtpToInbox.Value = DateTime.Today;
        }

        private void radWeekInboxPnl_Click(object sender, EventArgs e)
        {
            if (bInboxFilter)
                return;

            radWeekInboxFilter();
            radWeekInbox.Checked = radWeekInboxPnl.Checked;
            SaveIniFile();
            ViewData();
        }

        private void radWeekInboxFilter()
        {
            dtpFromInbox.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-(int)DateTime.Today.DayOfWeek);
            dtpToInbox.Value = dtpFromInbox.Value.AddDays(6);
        }

        private void radMonthInboxPnl_Click(object sender, EventArgs e)
        {
            if (bInboxFilter)
                return;

            radMonthInboxFilter();
            radMonthInbox.Checked = radMonthInboxPnl.Checked;
            SaveIniFile();
            ViewData();
        }

        private void radMonthInboxFilter()
        {
            if (cmbMonthsInbox.SelectedIndex == -1)
                return;

            cmbYearsInbox.Text = DateTime.Today.Year.ToString();
            cmbMonthsInbox.SelectedIndex = 0;
            cmbMonthsInbox.SelectedIndex = DateTime.Today.Month;
        }

        private void radYearInboxPnl_Click(object sender, EventArgs e)
        {
            if (bInboxFilter)
                return;

            radYearInboxFilter();
            radYearInbox.Checked = radYearInboxPnl.Checked;
            SaveIniFile();
            ViewData();
        }

        private void radYearInboxFilter()
        {
            if (cmbMonthsInbox.Items.Count == 0)
                return;

            cmbYearsInbox.Text = DateTime.Today.Year.ToString();
            cmbMonthsInbox.SelectedIndex = cmbMonthsInbox.SelectedIndex % 12 + 1;
            cmbMonthsInbox.SelectedIndex = 0;
        }

        private void dtpFromInbox_ValueChanged(object sender, EventArgs e)
        {
            lblInboxDateFrom.Text = dtpFromInbox.Value.ToShortDateString();
        }

        private void dtpToInbox_ValueChanged(object sender, EventArgs e)
        {
            lblInboxDateTo.Text = dtpToInbox.Value.ToShortDateString();
        }

        private void requestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRequests(false);
        }

        private void OpenRequests(bool bShowWindow)
        {
            if (!bShowWindow)
            {
                frmprocessrequest.Hide();
            }

            frmRequests frm = new frmRequests();
            frm.parser = parser;
            frm.dblayer = dblayer;
            frm.Company_Info = Company_Info;
            frm.ShowDialog();

            if (bShowWindow)
            {
                frmprocessrequest.Show();
            }
        }

        private void dataStructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDataStructure frm = new frmDataStructure();
            frm.parser = parser;
            frm.dblayer = dblayer;
            frm.Company_Info = Company_Info;
            frm.ShowDialog();
        }

        private void aquireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainFrame frm = new MainFrame();
            frm.Company_Info = Company_Info;
            frm.ShowDialog();
        }

        private void UpdateDetails()
        {
            if (lvwInbox.SelectedIndices.Count == 1)
            {
                object item = GetListViewObj(lvwInbox);

                if (item.GetType() == typeof(KoteretTnua))
                {
                    KoteretTnua kt = (KoteretTnua)item;
                    kt.Confirm0 = chkConfirm0.Checked;
                    kt.Confirm1 = chkConfirm1.Checked;
                    kt.Confirm2 = chkConfirm2.Checked;

                    dblayer.Current_Company_Info = Company_Info;
                    dblayer.UpdateInboxConfirm(kt);

                    Color color = Color.Pink;

                    if (kt.Confirm2)
                        color = Color.FromArgb(255, 255, 192);

                    if (kt.Confirm1)
                        color = Color.FromArgb(255, 224, 192);

                    if (kt.Confirm0)
                        color = Color.FromArgb(192, 255, 192);

                    lvwInbox.Items[lvwInbox.SelectedIndices[0]].BackColor = color;
                }
            }
        }

        private void chkConfirm0_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkConfirm1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkConfirm2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkConfirm0_Click(object sender, EventArgs e)
        {
            UpdateDetails();
        }

        private void chkConfirm1_Click(object sender, EventArgs e)
        {
            UpdateDetails();
        }

        private void chkConfirm2_Click(object sender, EventArgs e)
        {
            UpdateDetails();
        }

        private void btnImportInbox_Click_1(object sender, EventArgs e)
        {
            btnCreateInboxAction.Enabled = false;
            frmHakladaInbox frm = new frmHakladaInbox();
            frm.ActionsListDictionary = ActionsListDictionary;
            frm.Company_Info = Company_Info;
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.parser = parser;
            frm.grpImport.Visible = true;
            frm.ShowDialog();
            ViewData();
            CheckOutboxFiltersState();
            btnCreateInboxAction.Enabled = true;
        }

        private void btnImportOubox_Click(object sender, EventArgs e)
        {
            frmHaklada frm = new frmHaklada();
            frm.ActionsListDictionary = ActionsListDictionary;
            frm.Company_Info = Company_Info;
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.CountryID = CountryID;
            frm.CompanyVAT = CompanyVAT;
            frm.ReadCode = ReadCode;
            frm.WriteCode = WriteCode;
            frm.Maam = Maam;
            frm.parser = parser;
            frm.grpImport.Visible = true;
            frm.LoadData();
            frm.ShowDialog();
            dblayer.ReadShuratHakladaListMain(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, chkWaiting.Checked, chkTransferd.Checked, chkReaden.Checked, ActionsListDictionary, dtpFromOutbox.Value, dtpToOutbox.Value);
            dblayer.IsTransactionReaden(parser, lvwOutbox);
        }

        private void importOutboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp(frmHelp.eHelpType.ImportOutbox);
        }

        private void ShowHelp(frmHelp.eHelpType eHelpType)
        {
            frmHelp frm = new frmHelp();
            frm.HelpType = eHelpType;
            frm.ShowDialog();
        }

        private void tabMain_ParentChanged(object sender, EventArgs e)
        {

        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            this.Hide();
            frmReport frm = new frmReport();
            frm.Company_Info = Company_Info;
            frm.parser = parser;
            frm.dblayer = dblayer;
            frm.ShowDialog();
            this.Show();
             * */
        }

        private void tmrMail_Tick(object sender, EventArgs e)
        {
            GetMail(false);
        }

        private void frmCloudBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrMail.Enabled = false;
        }

        //private void setNumberOfSearchLettersToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    InputBox inputbox = new InputBox();
        //    inputbox.NumericInput = true;
        //    String input = Company_Info.NumOfSearchLetters.ToString();

        //    InputBoxResult result = inputbox.Show("Please Number Of Search Letters", "Number Of Search Letters", input);
        //    if (result.Text != "")
        //    {
        //        Company_Info.NumOfSearchLetters = Int32.Parse(result.Text);
        //        dblayer.UpdateCompanyInfo(Company_Info);
        //    }
        //    //dblayer.UpdateCompanyInfo(Company_Info);
        //}
    }
}


// The following generates a cursor from an embedded resource.
// To add a custom cursor, create or use an existing 16x16 bitmap
//        1. Add a new cursor file to your project: 
//                File->Add New Item->Local Project Items->Cursor File
//        2. Select 16x16 image type:
//                Image->Current Icon Image Types->16x16
// --- To make the custom cursor an embedded resource  ---
// In Visual Studio:
//        1. Select the cursor file in the Solution Explorer
//        2. Choose View->Properties.
//        3. In the properties window switch "Build Action" to "Embedded"
// On the command line:
//        Add the following flag:
//            /res:CursorFileName.Cur,Namespace.CursorFileName.Cur
//        
//        Where "Namespace" is the namespace in which you want to use
//        the cursor and   "CursorFileName.Cur" is the cursor filename.
// The following line uses the namespace from the passed-in type
// and looks for CustomCursor.MyCursor.Cur in the assemblies manifest.
// NOTE: The cursor name is case sensitive.

//this.Cursor = new Cursor(GetType(), "MyCursor.Cur");

//Add Icon file to Project resources (ex : Processing.ico)

//And in properties window of image switch "Build Action" to "Embedded"

//Cursor cur = new Cursor(Properties.Resources.**Imagename**.Handle);
//Me.Cursor = cur;

//Ex:

//Cursor cur = new Cursor(Properties.Resources.Processing.Handle);
//Me.Cursor = cur;

//"name of control".Cursor = new System.Windows.Forms.Cursor(Properties.Resources."name of image".Handle);


//canvas1.Cursor = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("WpfApplication2.mallet.cur"));
//String[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
//using (MemoryStream ms = new MemoryStream(Properties.Resources.magnify))
//{
//  myMagCursor = New Cursor(ms);
//}

//void maskedTextBox1_Validating(object sender, CancelEventArgs e)

//{

//    DateTime rs;

//    CultureInfo ci = new CultureInfo("en-IE");

//    if (!DateTime.TryParseExact(this.maskedTextBox1.Text,"dd/MM/yyyy",ci, DateTimeStyles.None,out rs))

//    {

//        e.Cancel = true;

//    }

//}



//public class MaskedDateTextBox : MaskedTextBox
//{
//    // Default setting is to require a valid date string before allowing 
//    // the user to navigate away from the control:
//    public bool _RequireValidEntry = true;

//    // The default mask is traditional, USA-centric mm/dd/yyyy format. 
//    private const string DEFAULT_MASK = "00/00/0000";
//    private const char DEFAULT_PROMPT = '_';

//    // A flag is set when control initialization is complete. This 
//    // will be used to determine if the Mask property of the control
//    // (inherited from the Base class) can be changed. 
//    private bool _Initialized = false;
//}


//    public MaskedDateTextBox() : this(true) { }


//    public MaskedDateTextBox(bool RequireValidEntry = true)
//        : base()
//    {

//        // This is the only mask that will work in the current implementation:
//        this.Mask = DEFAULT_MASK;
//        this.PromptChar = DEFAULT_PROMPT;

//        // Handle Events:
//        this.Enter += new EventHandler(MaskedDateTextBox_SelectAllOnEnter);
//        this.PreviewKeyDown += new PreviewKeyDownEventHandler(MaskedDateBox_PreviewKeyDown);

//        // prevent further changes to the mask:
//        _Initialized = true;
//    }


//    protected override void OnMaskChanged(EventArgs e)
//    {
//        if (_Initialized)
//        {
//            throw new NotImplementedException("The Mask is not chageable in this control");
//        }
//    }
//}

//    void CorrectDateText(MaskedTextBox dateTextBox)

//{
//    // Replace any odd date separators with the mm/dd/yyyy Standard:
//    Regex rgx = new Regex(@"(\\|-|\.)");
//    string FormattedDate = rgx.Replace(dateTextBox.Text, @"/");

//    // Separate the date components as delimited by standard mm/dd/yyyy formatting:
//    string[] dateComponents = FormattedDate.Split('/');
//    string month = dateComponents[0].Trim(); ;
//    string day = dateComponents[1].Trim();
//    string year = dateComponents[2].Trim();

//    // We require a two-digit month. If there is only one digit, add a leading zero:
//    if (month.Length == 1)
//    {
//        month = "0" + month;
//    }

//    // We require a two-digit day. If there is only one digit, add a leading zero:

//    if (day.Length == 1)
//    {
//        day = "0" + day;
//    }

//    // We require a four-digit year. If there are only two digits, add 
//    // two digits denoting the current century as leading numerals:
//    if (year.Length == 2)
//    {
//        year = "20" + year;
//    }

//    // Put the date back together again with proper delimiters, and 
//    dateTextBox.Text = month + "/" + day + "/" + year;
//}

//    protected virtual void MaskedDateBox_PreviewKeyDown(object sender, 
//                                        PreviewKeyDownEventArgs e)
//{
//    MaskedTextBox txt = (MaskedTextBox)sender;

//    // Check for common date delimiting characters. When encountered, 
//    // adjust the text entry for proper date formatting:
//    if (e.KeyCode == Keys.Divide
//        || e.KeyCode == Keys.Oem5
//        || e.KeyCode == Keys.OemQuestion
//        || e.KeyCode == Keys.OemPeriod
//        || e.KeyValue == 190
//        || e.KeyValue == 110)

//        // If any of the above key values are encountered, apply a formatting 
//        // check to the text entered so far, and make adjustments as needed. 
//        this.CorrectDateText(txt);

//}

//    bool IsValidDate(MaskedTextBox dateTextBox)
//{
//    // Remove delimiters from the text contained in the control. 
//    string DateContents = dateTextBox.Text.Replace("/", "").Trim();

//    // if no date was entered, we will be left with an empty string 
//    // or whitespace.
//    if (!string.IsNullOrEmpty(DateContents) && DateContents != "")
//    {
//        // Split the original date into components:
//        string[] dateSoFar = dateTextBox.Text.Split('/');
//        string month = dateSoFar[0].Trim(); ;
//        string day = dateSoFar[1].Trim();
//        string year = dateSoFar[2].Trim();

//        // If the component values are of the proper length for mm/dd/yyyy formatting:
//        if (month.Length == 2
//            && day.Length == 2
//            && year.Length == 4
//            && (year.StartsWith("19") || year.StartsWith("20")))
//        {
//            // Check to see if the string resolves to a valid date:
//            DateTime d;
//            if (!DateTime.TryParse(dateTextBox.Text, out d))
//            {
//                // The string did NOT resolve to a valid date:
//                return false;
//            }
//            else
//                // The string resolved to a valid date:
//                return true;
//        }
//        else
//        {
//            // The Components are not of the correct size, and automatic adjustment
//            // is unsuccessful:
//            return false;

//        } // End if Components are correctly sized
//    }
//    else
//        // The date string is empty or whitespace - no date is a valid return:
//        return true;
//} 


//protected override void OnLeave(EventArgs e)
//{
//    // Perform a final adjustment of the text entry to fit the mm/dd/yyyy format:
//    this.CorrectDateText(this);

//    // If the entry is a valid date, fire the leave event. We are done here. 
//    if (this.IsValidDate(this))
//    {
//        base.OnLeave(e);
//    }
//    else
//    {
//        this.OnInvalidDateEntry(this, new InvalidDateTextEventArgs(this.Text.Trim()));

//        // if a valid date entry is not required, the user is free to navigate away
//        // from the control:
//        if (!_RequireValidEntry)
//        {
//            base.OnLeave(e);
//        }
//    }
//}


//protected virtual void OnInvalidDateEntry(object sender, InvalidDateTextEventArgs e)
//{
//    if (_RequireValidEntry)
//    {
//        // Force the user to address the problem before 
//        // navigating away from the control:
//        MessageBox.Show(e.Message);
//        this.Focus();
//        this.MaskedDateTextBox_SelectAllOnEnter(this, new EventArgs());
//    }


//    // Raise the invalid entry event either way. Client code can determine 
//    // if and how invalid entry should be dealt with:
//    if (InvalidDateEntered != null)
//    {
//        InvalidDateEntered(this, e);
//    }
//}

//    using System;


//namespace MaskedDateEntryControl
//{
//    public class InvalidDateTextEventArgs : EventArgs
//    {

//        private string _Message = "" 
//            + "Text does not resolve to a valid date. "
//            + "Enter a date in mm/dd/yyyy format, "
//            + "or clear the text to represent an empty date.";

//        private string _InvalidDateString = "";


//        public InvalidDateTextEventArgs(string InvalidDateString) : base()
//        {
//            _InvalidDateString = InvalidDateString;
//        }


//        public InvalidDateTextEventArgs(string InvalidDateString, string Message) 
//            : this(InvalidDateString)
//            {
//                _Message = Message;
//            }


//        public String Message
//        {
//            get { return _Message; }
//            set { _Message = value; }
//        }


//        public String InvalidDateString
//        {
//            get { return _InvalidDateString; }
//        }
//    }
//}

//    public DateTime? DateValue
//{
//    get
//    {
//        DateTime d;
//        DateTime? Result = null;
//        if (DateTime.TryParse(this.Text, out d))
//        {
//            Result = d;
//        }
//        return Result;
//    }
//    set
//    {
//        string DateString = "";
//        if (value.HasValue)
//            DateString = value.Value.ToString("MM/dd/yyyy");
//        this.Text = DateString;
//    }
//}

//    void MaskedDateTextBox_SelectAllOnEnter(object sender, EventArgs e)
//{
//    MaskedTextBox m = (MaskedTextBox)sender;
//    this.BeginInvoke((MethodInvoker)delegate()
//    {
//        m.SelectAll();
//    });
//}