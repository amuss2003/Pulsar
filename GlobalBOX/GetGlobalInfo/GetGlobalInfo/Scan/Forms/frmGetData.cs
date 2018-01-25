using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Pulsar
{
    public partial class frmGetData : Form
    {
        private bool m_ShowSnif = true;
        private int m_LanguageID = 0;
        private bool m_Cancel = false;
        private bool m_Numeric = true;
        private bool m_7Digits = false;
        private string m_ScannerTargetPath = "";
        private string m_ScanType = "";
        private string m_FName = "";

        public String Snif { get; set; }
        public String Hoze { get; set; }

        public void SetScannerTargetPath(string ScannerTargetPath)
        {
            m_ScannerTargetPath = ScannerTargetPath;
            if (!Directory.Exists(m_ScannerTargetPath))
            {
                Directory.CreateDirectory(m_ScannerTargetPath);
            }
        }

        public frmGetData()
        {
            InitializeComponent();
        }

        internal void SetLanguage(int LanguageID)
        {
            m_LanguageID = LanguageID;
        }

        public void SetScanTypePic(Image img)
        {
            picScanType.Image = img;
        }

        public bool Eliran { get; set; }

        public String SetText { get { return txtNumber.Text; } set { txtNumber.Text = value; } }

        private void frmGetData_Load(object sender, EventArgs e)
        {
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

            if (m_LanguageID == 1)
            {
                SetHebrew();
            }

            m_7Digits = (this.Text.IndexOf("פעיל") > -1);

            if (m_7Digits)
            {
                txtNumber.MaxLength = 7;
            }

            if (m_ShowSnif)
            {
                if (Snif != "")
                {
                    cmbSnif.Text = Snif;
                }

                if (Hoze != "")
                {
                    txtNumber.Text = Hoze;
                }
            }
        }

        public string GetData()
        {
            if (m_Cancel)
            {
                return "";
            }
            else
            {
                if (m_ShowSnif)
                {
                    return txtNumber.Text.Trim() + "-" + cmbSnif.Text.Trim();
                }
                else
                {
                    return txtNumber.Text.Trim();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (m_7Digits)
            //{
            //    if (txtNumber.Text.Trim().Length < 7)
            //    {
            //        MessageBox.Show("çåáä ìäæéï 7 ñôøåú ìמספר רכב!","מספר רכב",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            //        return;
            //    }
            //}

            this.Hide();
        }

        public void HideSnif()
        {
            lblSnif.Visible = false;
            cmbSnif.Visible = false;
            m_ShowSnif = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_Cancel = true;
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_Numeric)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                base.OnKeyPress(e);
            }
        }

        public void SetNotNumeric()
        {
            m_Numeric = false;
        }

        public void SetHebrew()
        {
            lblNumber.Text = "מספר:";
            lblSnif.Text = "סניף:";
            btnCancel.Text = "בטל";
            btnOK.Text = "אשר";
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            if (m_7Digits)
            {
                btnOK.Enabled = (txtNumber.Text.Trim().Length == 7);
                if (btnOK.Enabled)
                {
                    m_FName = m_ScannerTargetPath + m_ScanType + txtNumber.Text.Trim() + ".jpg";
                    if (File.Exists(m_FName))
                    {
                        picScanType.BackColor = Color.Red;
                        //picScanType.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                    {
                        picScanType.BackColor = Color.Black;
                        //picScanType.BorderStyle = BorderStyle.None;
                    }
                }
            }
            else
            {
                btnOK.Enabled = (txtNumber.Text.Trim().Length > 0);

                if (m_ShowSnif)
                {
                    m_FName = m_ScannerTargetPath + txtNumber.Text.Trim() + "-" + cmbSnif.Text.Trim() + ".jpg";
                }
                else
                {
                    m_FName = m_ScannerTargetPath + txtNumber.Text.Trim() + ".jpg";
                }

                if (File.Exists(m_FName))
                {
                    picScanType.BackColor = Color.Red;
                    //picScanType.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    picScanType.BackColor = Color.Black;
                    //picScanType.BorderStyle = BorderStyle.None;
                }
            }


            //DirectoryInfo di = new DirectoryInfo(m_ScannerTargetPath + txtNumber.Text.Trim());
            //try
            //{
            //    // Determine whether the directory exists.
            //    if (di.Exists)
            //    {
            //        // Indicate that the directory already exists.
            //        Console.WriteLine("That path exists already.");
            //        return;
            //    }

            //    // Delete the directory.
            //    //di.Delete();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("The process failed: {0}", e.ToString());
            //}
            //finally { }                        
        }

        public void SetScanType(string ScanType)
        {
            m_ScanType = ScanType;
        }

        private void LoadImage(PictureBox pic, string ImageFileName)
        {
            try
            {
                pic.Image = Image.FromFile(ImageFileName);
                pic.ImageLocation = ImageFileName;
            }
            catch (Exception)
            {
                pic.Image = null;
            }
        }

        private void picScanType_DoubleClick(object sender, EventArgs e)
        {
            if (File.Exists(m_FName))
            {
                PictureBox pic = new PictureBox();
                LoadImage(pic, m_FName);

                frmZoom frm = new frmZoom();
                frm.SetImageFilePath(m_FName);
                frm.SetZoomImage(pic);
                frm.ShowDialog();
            }
        }
    }
}