using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pulsar;
using Pulsar.Classes;
using System.IO;
//using BalloonCS;

namespace Pulsar
{
    public partial class frmBusinessIndexTable : Form
    {
        //private MessageBalloon m_mb;
        public Parser parser { get; set; }
        public CompanyInfo Company_Info { get; set; }
        public int SelectedCountryIndex { get; set; }
        public String CountryID { get; set; }

        public DBLayer dblayer = null;

        public frmBusinessIndexTable()
        {
            InitializeComponent();
        }

        public static void SetDoubleBuffered(Control ctrl)
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

            aProp.SetValue(ctrl, true, null);
        }

        private void frmBusinessIndexTable_Load(object sender, EventArgs e)
        {
            SetDoubleBuffered(lvwBusinessList);

            lvwColumnSorter = new ListViewColumnSorter();
            this.lvwBusinessList.ListViewItemSorter = lvwColumnSorter;

            dblayer = new DBLayer(Application.StartupPath + @"\App_Data\");

            PrepareIniFileName();
            ReadCompanies();

            //if (lvwBusinessList.Items.Count == 0)
            //{
            //    AddCompany();
            //}
            titleBar1.Company_Info = Company_Info;
            FillCompanyType();
            cmbCompanyType.SelectedIndex = 0;
            //ShowTips();
        }

        //private void ShowTips()
        //{
        //    if (lvwBusinessList.Items.Count == 0)
        //    {
        //        m_mb = new MessageBalloon();
        //        m_mb.Parent = btnAdd;
        //        m_mb.Title = "CMailBox";
        //        m_mb.TitleIcon = TooltipIcon.Info;
        //        m_mb.Text = "Add new company.";

        //        m_mb.Align = BalloonAlignment.BottomMiddle;
        //        m_mb.CenterStem = true;
        //        //m_mb.UseAbsolutePositioning = checkBox2.Checked ? true : false;
        //        m_mb.Show();
        //    }
        //}

        private void FillCompanyType()
        {
            cmbCompanyType.Items.Add("הכל");
            foreach (String type in CompanyType.types)
            {
                cmbCompanyType.Items.Add(type);
            }
        }

        private void ReadCompanies()
        {
            lvwBusinessList.SuspendLayout();
            String where_company_name = "";
            string CompanyName = txtCompanyName.Text.Replace("'", "''");

            if ((CompanyName.Trim() != "") && (cmbCompanyType.SelectedIndex > 0))
            {
                where_company_name = " AND CompanyName LIKE '%" + CompanyName + "%' AND CompanyType = " + (cmbCompanyType.SelectedIndex - 1);
            }
            else if ((CompanyName.Trim() != "") && (cmbCompanyType.SelectedIndex == 0))
            {
                where_company_name = " AND CompanyName LIKE '%" + CompanyName + "%'";
            }
            else if (cmbCompanyType.SelectedIndex > 0)
            {
                where_company_name = " AND CompanyType = " + (cmbCompanyType.SelectedIndex - 1);
            }

            BuisnessInfo buisnessInfo = dblayer.ReadBusinessList(lvwBusinessList, where_company_name, Company_Info);

            lblActive.Text = buisnessInfo.Active.ToString();
            lblNotActive.Text = buisnessInfo.NotActive.ToString();
            lblNotExist.Text = buisnessInfo.NotExist.ToString();
            lblBlocked.Text = buisnessInfo.Blocked.ToString();
            lblTotal.Text = buisnessInfo.Total.ToString(); //lvwBusinessList.Items.Count.ToString();
            lvwBusinessList.ResumeLayout();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (m_mb != null)
            //{
            //    m_mb.Dispose();
            //}
            AddCompany();
        }

        private void AddCompany()
        {
            frmBusinessIndex frm = new frmBusinessIndex();
            frm.parser = parser;
            frm.Company_Info = Company_Info;
            frm.CountryID = CountryID;
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.ShowDialog();
            dblayer.ReadBusinessList(lvwBusinessList, "", Company_Info);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCompany();
        }

        private void UpdateCompany()
        {
            if (lvwBusinessList.SelectedIndices.Count == 1)
            {
                ListViewItem lvi = lvwBusinessList.Items[lvwBusinessList.SelectedIndices[0]];

                Company company = (Company)lvi.Tag;
                frmBusinessIndex frm = new frmBusinessIndex();
                frm.parser = parser;
                frm.Company_Info = Company_Info;
                frm.CountryID = CountryID;
                frm.SelectedCountryIndex = SelectedCountryIndex;
                frm.edit_company = company;
                frm.ShowDialog();
                company = dblayer.GetCompany(company.CountryID, company.CompanyVAT);
                lvi.Tag = company;
                lvi.SubItems[1].Text = company.CompanyName;
                lvi.SubItems[2].Text = company.CompanyVAT;
                lvi.SubItems[3].Text = company.WriteCode;
                lvi.SubItems[4].Text = CompanyType.types[company.CompanyType];
                lvi.SubItems[5].Text = company.AccountCode;
                
                switch (company.CompanyType)
                {
                    case 0:
                        lvi.BackColor = Color.LightGreen;
                        break;
                    case 1:
                        lvi.BackColor = Color.LightPink;
                        break;
                    case 2:
                        lvi.BackColor = Color.LightSteelBlue;
                        break;
                    default:
                        break;
                }
            }
        }

        private void lvwBusinessList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = (lvwBusinessList.SelectedIndices.Count == 1);
            if (btnUpdate.Enabled)
            {
                Company company = (Company)lvwBusinessList.Items[lvwBusinessList.SelectedIndices[0]].Tag;
                btnBlock.Tag = company.Blocked; // (lvwBusinessList.Items[lvwBusinessList.SelectedIndices[0]].BackColor == Color.Red);
                btnBlock.Text = company.Blocked ? "פתח" : "חסום";
                btnDelete.Enabled = btnUpdate.Enabled;
                btnBlock.Enabled = btnUpdate.Enabled;
                btnWriteCodeRequest.Enabled = btnUpdate.Enabled;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvwBusinessList_DoubleClick(object sender, EventArgs e)
        {
            UpdateCompany();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        private void DeleteRecord()
        {
            if (lvwBusinessList.SelectedIndices.Count != 1)
                return;

            Company company = (Company)lvwBusinessList.Items[lvwBusinessList.SelectedIndices[0]].Tag;

            if (!dblayer.CheckCompanyHasData(company))
            {
                if (MessageBox.Show("Delete Company " + company.CompanyName, "Delete Company", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    dblayer.DeleteCompany(company.CompanyID);
                    lvwBusinessList.Items[lvwBusinessList.SelectedIndices[0]].Remove();
                    //ReadCompanies();
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    //lvwBusinessList.SelectedIndices.Clear();
                    //lvwBusinessList.SelectedIndices.Add(0);
                    //lvwBusinessList.Items[0].EnsureVisible();
                    lvwBusinessList.Focus();
                }
            }
            else
                MessageBox.Show("Can not delete company " + company.CompanyName + " it has data.", "Delete Company", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void btnSelection_Click(object sender, EventArgs e)
        {
            MessageBox.Show("עדיין לא בוצע בפיתוח!", "פיתוח", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            ReadCompanies();
        }

        private void btnSelectionImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Company_Info.DataPath != null)
                {
                    if (Directory.Exists(Company_Info.DataPath))
                    {
                        ofdAttachment.InitialDirectory = Company_Info.DataPath;
                    }
                }

                ofdAttachment.FileName = "";
                ofdAttachment.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|dat files (*.dat)|*.dat|all files (*.*)|*.*";
                ofdAttachment.FilterIndex = 0;
                ofdAttachment.RestoreDirectory = true;

                DialogResult result = ofdAttachment.ShowDialog();

                if (result == DialogResult.OK) // Test result.
                {
                    ExternalDataManager externalDataManager = new ExternalDataManager();
                    if (radFixedSizeImport.Checked)
                    {
                        externalDataManager.ImportCompanies(ofdAttachment.FileName, Company_Info, dblayer);
                    }
                    else
                    {
                        string delimiter = "";

                        if (radTabImport.Checked)
                            delimiter = "\t";

                        if (radPipeImport.Checked)
                            delimiter = "|";

                        if (radCommaImport.Checked)
                            delimiter = ",";

                        if (radOtherImport.Checked)
                            delimiter = txtDelimiterImport.Text;

                        //is space can be a delimiter?
                        externalDataManager.ImportCompanies(ofdAttachment.FileName, Company_Info, dblayer, delimiter);
                    }
                    dblayer.ReadBusinessList(lvwBusinessList, "", Company_Info);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File structure error!" + Environment.NewLine + "OR" + Environment.NewLine + "Directory not decleared!", "Import Buisness", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            grpImport.Visible = false;
        }

        private void btnExitImport_Click(object sender, EventArgs e)
        {
            grpImport.Visible = false;
        }

        private void btnImportInbox_Click(object sender, EventArgs e)
        {
            LoadData();
            grpImport.Visible = true;
        }

        private void btnFindCompanies_Click(object sender, EventArgs e)
        {
            btnFindCompanies.Enabled = false;
            //frmProcessRequest frm = new frmProcessRequest();
            //frm.Show();

            foreach (ListViewItem lvi in lvwBusinessList.Items)
            {
                Company company = (Company)lvi.Tag;
                lvi.ImageIndex = 4;
                lvi.EnsureVisible();
                Application.DoEvents();
                String result = parser.IsCompanyExist(company.CountryID.ToString(), company.CompanyVAT).ToLower();

                if (result == "active")
                {
                    company.HaveCMail = 0;
                    lvi.ImageIndex = 0;
                }
                else if (result == "not active")
                {
                    company.HaveCMail = 1;
                    lvi.ImageIndex = 1;
                }
                else if (result == "not exist")
                {
                    company.HaveCMail = 2;
                    lvi.ImageIndex = 2;
                }

                dblayer.Current_Company_Info = Company_Info;
                if (!dblayer.UpdateCompany(company))
                {
                    lvi.ImageIndex = 5;
                }
            }

            //frm.Close();
            btnFindCompanies.Enabled = true;
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            if (lvwBusinessList.SelectedIndices.Count == 1)
            {
                ListViewItem lvi = lvwBusinessList.Items[lvwBusinessList.SelectedIndices[0]];
                Company company = (Company)lvi.Tag;

                if (company != null)
                {
                    if (!(bool)btnBlock.Tag)
                    {
                        parser.BlockClient(company.CountryID.ToString(), company.CompanyVAT, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, Company_Info.ReadCode);
                        if (parser.IsClientBlocked(company.CountryID.ToString(), company.CompanyVAT, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT))
                        {
                            company.Blocked = true;
                            dblayer.Current_Company_Info = Company_Info;
                            dblayer.UpdateCompany(company);
                            lvi.ImageIndex = 3;
                            lvi.BackColor = Color.Pink;
                            MessageBox.Show("Client successfully blocked!", "Client Blocking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Faile to block Client!" + Environment.NewLine + " Please try again later.", "Client Blocking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        parser.UnBlockClient(company.CountryID.ToString(), company.CompanyVAT, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, Company_Info.ReadCode);
                        if (!parser.IsClientBlocked(company.CountryID.ToString(), company.CompanyVAT, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT))
                        {
                            company.Blocked = false;
                            dblayer.Current_Company_Info = Company_Info;
                            dblayer.UpdateCompany(company);
                            lvi.ImageIndex = 0;
                            lvi.BackColor = Color.White;
                            MessageBox.Show("Client successfully unblocked!", "Client Unblocking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Faile to unblock client!" + Environment.NewLine + " Please try again later.", "Client Unblocking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    btnBlock.Tag = (lvwBusinessList.Items[lvwBusinessList.SelectedIndices[0]].BackColor == Color.Pink);
                    btnBlock.Text = (bool)btnBlock.Tag ? "פתח" : "חסום";
                }
            }
        }

        private void btnWriteCodeRequest_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvwBusinessList.Items[lvwBusinessList.SelectedIndices[0]];
            Company company = (Company)lvi.Tag;

            if (company != null)
            {
                if (parser.IsCompanyExist(company.CountryID.ToString(), company.CompanyVAT).ToLower() != "not exist")
                {
                    SendOutboxData.SendRequest(Company_Info, company, parser, dblayer, "בקשה לקוד כתיבה");
                    MessageBox.Show("Your request successfully sent to " + company.CompanyName, "Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(company.CompanyName + " does not have Pulsar Account!", "Request", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void cmbCompanyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadCompanies();
        }

        private void btnImportHelp_Click(object sender, EventArgs e)
        {
            frmHelp frm = new frmHelp();
            frm.HelpType = frmHelp.eHelpType.ImportCompany;
            frm.ShowDialog();
        }

        private void frmBusinessIndexTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    //if (m_mb != null)
            //    //{
            //    //    m_mb.Dispose();
            //    //}
            //}
            //catch (Exception)
            //{
            //}
        }

        private void btnUpdateImport_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void LoadData()
        {
            foreach (ListViewItem lviFld in lvwImportFileds.Items)
            {
                lviFld.Text = iniFile.IniReadValue(Company_Info.CompanySerialNumber + " Company Import Structure", lviFld.Tag.ToString());
            }
        }

        public void Save()
        {
            foreach (ListViewItem lviFld in lvwImportFileds.Items)
            {
                iniFile.IniWriteValue(Company_Info.CompanySerialNumber + " Company Import Structure", lviFld.Tag.ToString(), lviFld.Text);
            }
        }

        public String IniFileName { get; set; }
        public IniFile iniFile { get; set; }

        private void PrepareIniFileName()
        {
            IniFileName = Path.GetFileName(Application.ExecutablePath);

            FileInfo fi = new FileInfo(IniFileName);
            fi = new FileInfo(fi.FullName);
            IniFileName = fi.Name;

            IniFileName = IniFileName.Substring(0, IniFileName.Length - ".exe".Length) + ".ini";
            iniFile = new IniFile(Application.StartupPath + @"\" + IniFileName);
        }

        private void lvwImportFileds_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (lvwImportFileds.SelectedIndices.Count == 1)
            //{
            //    lvwImportFileds.Items[((ListView)(sender)).SelectedIndices[0]].BeginEdit();
            //}
        }

        private ListViewColumnSorter lvwColumnSorter;

        private void lvwBusinessList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView myListView = (ListView)sender;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblNotExist_Click(object sender, EventArgs e)
        {

        }

        private void txtCompanyName_Enter(object sender, EventArgs e)
        {
            SetControlLanguage((Control)sender, CountryID == "117");
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
    }
}