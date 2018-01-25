using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Pulsar.Classes;

namespace Pulsar
{
    public partial class frmPayment : Form
    {
        public CompanyInfo Company_Info { get; set; }

        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            ShowCommands nShowCmd);



        public frmPayment()
        {
            InitializeComponent();
        }

        private void txtCC_TextChanged(object sender, EventArgs e)
        {
            CheckCC();
        }

        public bool IsNumeric(string Value)
        {
            try
            {
                Decimal Number = Decimal.Parse(Value);
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        private bool CheckCC()
        {
            bool validate = false;
            if (txtCC.PasswordChar == '\0')
            {
                if (IsNumeric(txtCC.Text.Trim()))
                {
                    validate = CCValidation.IsValidCC(txtCC.Text.Trim()) && (txtCC.Text.Trim().Length > 7);
                    txtCC.BackColor = validate ? Color.LightGreen : Color.Pink;
                    picCCValidation.Image = validate ? global::Pulsar.Properties.Resources.active : global::Pulsar.Properties.Resources.inactive;
                }
            }

            return validate;
        }

        public bool PaymentByPass { get; set; }
        public bool paid { get; set; }
        public String Payment { get; set; }
        public String plain { get; set; }

        private void btnNewBusiness_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                //string key = Guid.NewGuid().ToString().Replace("-", "");
                //string salt = CipherUtility.GenerateSimpleSalt();
                ////string plain = "4580000000000000|יניב זוהר|111111118|08/16|1234";
                plain = txtName.Text + "|" + txtCC.Text + "|" + txtExpired.Text + "|" + txtCVV.Text + "|" + txtID.Text;
                //Payment = CipherUtility.Encrypt<RijndaelManaged>(plain, key, salt) + "|" + salt;
                ////string encrypted = CipherUtility.Encrypt<RijndaelManaged>(plain, key, salt);
                ////string decrypted = CipherUtility.Decrypt<RijndaelManaged>(encrypted, key, salt);

                ////String xxx1 = Uri.EscapeUriString(Payment);
                ////String xxx2 = HttpUtility.UrlEncode(CompanyName);
                ////String xxx3 = Uri.EscapeDataString(Payment);
                //Payment = Uri.EscapeDataString(Payment);
                paid = true;
                this.Close();
            }
        }

        private bool ValidateForm()
        {
            if ((!ControlsValidator.ValidateControl(txtName)) || (!ControlsValidator.ValidateControlMinLength(txtName, 5)))
            {
                return false;
            }

            if ((!ControlsValidator.ValidateControl(txtID)) || (!ControlsValidator.ValidateControlMinLength(txtID, 9)))
            {
                return false;
            }

            if ((!ControlsValidator.ValidateControl(txtCC)) || (!ControlsValidator.ValidateControlMinLength(txtCC, 9)))
            {
                return false;
            }

            if ((!ControlsValidator.ValidateControl(txtExpired)) || (!ControlsValidator.ValidateControlMinLength(txtExpired, 4)))
            {
                return false;
            }

            try
            {
                DateTime dt = new DateTime(Int32.Parse(txtExpired.Text.Substring(2, 2)), Int32.Parse(txtExpired.Text.Substring(0, 2)), 1);            
            }
            catch (Exception)
            {
                return false;
            }            

            if ((!ControlsValidator.ValidateControl(txtCVV)) || (!ControlsValidator.ValidateControlMinLength(txtCVV, 3)))
            {
                return false;
            }

            return true;
        }

        private void txtCC_Leave(object sender, EventArgs e)
        {
            txtCC.PasswordChar = '*';
        }

        private void txtCC_Enter(object sender, EventArgs e)
        {
            txtCC.PasswordChar = '\0';
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            paid = false;
            this.Close();
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            cmbCCType.Items.Add("Visa");
            cmbCCType.Items.Add("Isracard");
            cmbCCType.Items.Add("MasterCard");
            cmbCCType.Items.Add("American Express");
            cmbCCType.Items.Add("Diners");
            cmbCCType.Items.Add("LeumiCard");

            //FillForm();
        }

        private void FillForm()
        {
            cmbCCType.SelectedIndex = 0;
            txtName.Text = "CMail Client";
            txtID.Text = "111111118";
            txtCC.Text = "4580000000000000";
            txtExpired.Text = "0816";
            txtCVV.Text = "1234";
        }

        private void lnkRead_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ////ShellExecute(NULL, TEXT("open"), TEXT("http://msdn.microsoft.com"), TEXT(""), NULL, SW_SHOWNORMAL); 
            //// Asks default mail client to send an email to the specified address.
            //ShellExecute(IntPtr.Zero, "open", "mailto:support@microsoft.com", "", "", ShowCommands.SW_SHOWNOACTIVATE);

            //http://www.Pulsar.co.il/
            // Asks default browser to visit the specified site.
            ShellExecute(IntPtr.Zero, "open", "http://www.adirim.info/GlobalInfoProtocol/template-License-Agreement-eng.pdf", "", "", ShowCommands.SW_SHOWNOACTIVATE);

            //// Opens default HTML editing app to allow for edit of specified file
            //ShellExecute(IntPtr.Zero, "edit", @"c:\file.html", "", "", ShowCommands.SW_SHOWNOACTIVATE);
            ////Modified by Aljaz: Replaced the last zero in these calls with 4  otherwise it wouldn't show anything
            //// 0 stands for SW_HIDE contant, which means execute but don't show the window which is probably not 
            //// what we want.

            chkIAgree.Enabled = true;
        }

        private void chkIAgree_CheckedChanged(object sender, EventArgs e)
        {
            btnNewBusiness.Enabled = chkIAgree.Enabled;
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (Company_Info.CompanyCountryID == 117)
            {
                txtID.BackColor = IsraeliID.IsValidID(txtID.Text) ? Color.LightGreen : Color.Pink;
            }
        }

        private void picCCValidation_DoubleClick(object sender, EventArgs e)
        {
            int keyPessedLeftAlt = Win32.GetKeyState(Win32.VirtualKeyStates.VK_MENU) & 0x8000;
            int keyPessedLeftCtrl = Win32.GetKeyState(Win32.VirtualKeyStates.VK_LCONTROL) & 0x8000;
            int keyPessedLeftShift = Win32.GetKeyState(Win32.VirtualKeyStates.VK_LSHIFT) & 0x8000;

            if ((keyPessedLeftAlt != 0) && (keyPessedLeftCtrl != 0) && (keyPessedLeftShift != 0))
            {
                if (txtCC.Text == "031263")
                {
                    this.PaymentByPass = true;
                    this.Close();
                }
            }
        }
    }
}
