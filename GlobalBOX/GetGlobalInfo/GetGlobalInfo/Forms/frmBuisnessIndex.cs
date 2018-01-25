using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using Pulsar.Classes;

namespace Pulsar
{
    public partial class frmBusinessIndex : Form
    {
        public Parser parser { get; set; }
        public CompanyInfo Company_Info { get; set; }
        public int SelectedCountryIndex { get; set; }
        public String CountryID { get; set; }
        public Company edit_company { get; set; }
        public DBLayer dblayer = null;
        public String Company_VAT { get; set; }
        public String Company_Name { get; set; }

        public frmBusinessIndex()
        {
            InitializeComponent();
        }

        private void frmActionType_Load(object sender, EventArgs e)
        {
            dblayer = new DBLayer(Application.StartupPath + @"\App_Data\");
            dblayer.Current_Company_Info = Company_Info;
            FillCompanyType();

            foreach (ComboboxItem countryItem in getCountryList())
            {
                cmbCountries.Items.Add(countryItem);
            }
            cmbCountries.Sorted = true;

            if (CountryID != null)
            {
                cmbCountries.SelectedItem = CountryID;
            }

            cmbCountries.SelectedIndex = SelectedCountryIndex;
            txtCountryID.Text = ((ComboboxItem)(cmbCountries.Items[SelectedCountryIndex])).Value.ToString();

            if (edit_company != null)
            {
                ViewCompany(dblayer.GetCompany(edit_company.CompanyID.ToString()));
            }
            else
            {
                NewRecord();
                txtCompanyName.Text = Company_Name;
                txtCompanyVAT.Text = Company_VAT;
                btnNew.Visible = true;
            }

            pnlNotActive.Top = pnlActive.Top;
            pnlNotExist.Top = pnlActive.Top;

            titleBar1.Company_Info = Company_Info;
        }

        private void FillCompanyType()
        {
            foreach (String type in CompanyType.types)
            {
                cmbCompanyType.Items.Add(type);
            }
        }

        private void ViewCompany(Company company)
        {
            //txtCountryID.Text = company.CountryID.ToString();
            SetComboItemByValue(cmbCountries, company.CountryID);

            txtCompanyVAT.Text = company.CompanyVAT;
            txtCompanyName.Text = company.CompanyName;
            txtWriteCode.Text = company.WriteCode;
            txtAccountCode.Text = company.AccountCode;
            cmbCompanyType.SelectedIndex = company.CompanyType;
        }

        private Company GetCompany()
        {
            Company company = new Company();

            if (edit_company != null)
            {
                company.CompanyID = edit_company.CompanyID;
            }

            company.CountryID = Convert.ToInt32(txtCountryID.Text);

            company.CompanyVAT = txtCompanyVAT.Text;
            company.CompanyName = txtCompanyName.Text;

            company.WriteCode = txtWriteCode.Text;
            company.AccountCode = txtAccountCode.Text;

            company.Blocked = false;

            company.CompanyType = cmbCompanyType.SelectedIndex;

            company.HaveCMail = HaveCMail;

            return company;
        }

        private void SetComboItemByValue(ComboBox cmb, int value)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (value == Int32.Parse(((ComboboxItem)(cmb.Items[i])).Value.ToString()))
                {
                    cmb.SelectedIndex = i;
                    break;
                }
            }
        }

        private void NewRecord()
        {
            if (CountryID != null)
            {
                txtCountryID.Text = CountryID;
            }
            txtCompanyVAT.Focus();
        }

        private bool CheckForm()
        {
            if (!ValidControl(cmbCountries))
                return false;

            if (!ValidControl(txtCompanyVAT, 7))
                return false;

            if (!ValidControl(txtCompanyName, 2))
                return false;

            if (!ValidControl(txtAccountCode, 2))
                return false;

            if (!ValidControl(txtWriteCode, 9))
                return false;

            return true;
        }

        private bool ValidControl(TextBox txt, int length)
        {
            if (txt.Text.Trim().Length < length)
            {
                txt.Focus();
                txt.BackColor = Color.Pink;
                return false;
            }

            txt.BackColor = Color.White;
            return true;
        }

        private bool ValidControl(ComboBox cmb)
        {
            if (cmbCountries.SelectedIndex == -1)
            {
                cmb.Focus();
                cmb.BackColor = Color.Pink;
                return false;
            }

            cmb.BackColor = Color.White;
            return true;
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

        private void comBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem cmbItem = (ComboboxItem)cmbCountries.SelectedItem;
            txtCountryID.Text = cmbItem.Value.ToString();
        }

        private void companyVATTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidControl(txtCompanyVAT, 7);
        }

        private void companyNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidControl(txtCompanyName, 2);
        }

        private void accountCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidControl(txtAccountCode, 2);
        }

        private void writeCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidControl(txtWriteCode, 9);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            CheckForm();
            cmbCountries.SelectedIndex = 0;
            cmbCountries.SelectedIndex = SelectedCountryIndex; //Text = "Israel";
            ValidControl(txtCompanyVAT, 7);
            ValidControl(txtCompanyName, 2);
            ValidControl(txtAccountCode, 2);
            ValidControl(txtWriteCode, 9);
            cmbCountries.Focus();
        }

        private void btnCreateBusiness_Click(object sender, EventArgs e)
        {
            frmBusinessIndexTable frm = new frmBusinessIndexTable();
            frm.ShowDialog();
        }

        private void cmbCountries_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboboxItem cmbItem = (ComboboxItem)cmbCountries.SelectedItem;
            txtCountryID.Text = cmbItem.Value.ToString();
        }

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
                    break;
                }
            }
        }

        private void countryIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (cmbCountries.SelectedIndex == -1)
            {
                if (txtCountryID.Text != "")
                {
                    SelectCountryByID(txtCountryID.Text);
                }
            }
        }

        private bool SaveRecord()
        {
            txtCompanyVAT.BackColor = Color.White;

            if (CountryID == "117")
            {
                if (!IsraeliID.IsValidID(txtCompanyVAT.Text))
                {
                    txtCompanyVAT.BackColor = Color.Pink;
                    return false;
                }
            }

            dblayer.Current_Company_Info = Company_Info;
            if (edit_company != null)
            {
                dblayer.UpdateCompany(GetCompany());
            }
            else
            {
                dblayer.AddCompany(GetCompany());
            }

            return true;
        }

        private void bindingNavigatorAddNewItem_Click_1(object sender, EventArgs e)
        {
            txtCountryID.Text = CountryID;
        }

        private void DeleteRecord()
        {
            if (txtCompanyVAT.Text == "0")
                return;

            if (MessageBox.Show("Delete Company " + txtCompanyName.Text, "Delete Company", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                if (edit_company != null)
                {
                    dblayer.DeleteCompany(edit_company.CompanyID);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveRecord())
                return;

            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRecord();
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (edit_company == null)
            {
                CleanForm();
            }
        }

        private bool ValidateForm()
        {
            if (!ValidateControl(cmbCountries))
                return false;

            if (!ValidateControl(txtCompanyName))
                return false;

            if (!ValidateControl(txtCompanyVAT))
                return false;

            if (!ValidateControlLength(txtWriteCode, 9))
                return false;

            return true;
        }

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


        private void CleanForm()
        {
            txtCountryID.Text = CountryID;
            txtCompanyVAT.Focus();

            txtCompanyVAT.Text = "";
            txtCompanyName.Text = "";
            txtAccountCode.Text = "";
            txtWriteCode.Text = "";
        }

        public int HaveCMail { get; set; }

        private void txtCompanyVAT_Leave(object sender, EventArgs e)
        {
            picSeek.Visible = true;
            Application.DoEvents();

            if (txtCompanyVAT.Text.Trim().Length == 9)
            {
                String result = parser.IsCompanyExist(txtCountryID.Text, txtCompanyVAT.Text).ToLower();
                pnlActive.Visible = false;
                pnlNotActive.Visible = false;
                pnlNotExist.Visible = false;

                if (result == "active")
                {
                    HaveCMail = 0;
                    pnlActive.Visible = true;
                }
                else if (result == "not active")
                {
                    HaveCMail = 1;
                    pnlNotActive.Visible = true;
                }
                else if (result == "not exist")
                {
                    HaveCMail = 2;
                    pnlNotExist.Visible = true;
                }
            }
            picSeek.Visible = false;
        }

        private void txtCompanyName_Enter(object sender, EventArgs e)
        {
            SetControlLanguage((Control)sender, txtCountryID.Text == "117");
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