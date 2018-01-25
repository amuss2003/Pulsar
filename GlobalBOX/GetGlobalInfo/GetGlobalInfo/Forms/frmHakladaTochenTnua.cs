using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pulsar.Classes;

namespace Pulsar
{
    public partial class frmHakladaTochenTnua : Form
    {
        public CompanyInfo Company_Info { get; set; }
        public int SelectedCountryIndex { get; set; }
        public String CountryID { get; set; }
        public String CompanyVAT { get; set; }
        public String ReadCode { get; set; }
        public String WriteCode { get; set; }
        public String Maam { get; set; }
        public Parser parser { get; set; }
        public DBLayer dblayer = null;
        public String TransactionGUID { get; set; }

        public frmHakladaTochenTnua()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        private void CleanForm()
        {
            txtCodeParit.Text = "";
            txtKamutKlalit.Text = "";
            txtMechirYechida.Text = "";
            txtSchumShura.Text = "";
            txtTeurParit.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                lvwOutboxTochenTnua.SelectedIndices.Clear();
                dblayer.AddTochenTnuaHakladaRecord(CreateTochenTnuaHaklada());
                dblayer.ReadTochenTnuaForShuratHakladaListMain(lvwOutboxTochenTnua, TransactionGUID);
                CleanForm();
                txtCodeParit.Focus();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                TochenTnua tochen_tnua = CreateTochenTnuaHaklada();
                dblayer.UpdateTochenTnuaHaklada(tochen_tnua);
                dblayer.ReadTochenTnuaForShuratHakladaListMain(lvwOutboxTochenTnua, TransactionGUID);
                CleanForm();
            }
        }

        private TochenTnua CreateTochenTnuaHaklada()
        {
            TochenTnua tochen_tnua = new TochenTnua();
            if (lvwOutboxTochenTnua.SelectedIndices.Count == 1)
            {
                if (lvwOutboxTochenTnua.Items[lvwOutboxTochenTnua.SelectedIndices[0]].Tag != null)
                {
                    tochen_tnua.RawDataNumber = ((TochenTnua)lvwOutboxTochenTnua.Items[lvwOutboxTochenTnua.SelectedIndices[0]].Tag).RawDataNumber;
                }
            }
            tochen_tnua.TransactionGUID = TransactionGUID;
            tochen_tnua.CodeParitAlpha = txtCodeParit.Text;
            tochen_tnua.KamutKlalit = txtKamutKlalit.GetInt();
            tochen_tnua.MechirYehida = txtMechirYechida.GetDouble();
            tochen_tnua.SchumShura = txtSchumShura.GetDouble();
            tochen_tnua.TeurParitAlpha = txtTeurParit.Text;

            return tochen_tnua;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwOutboxTochenTnua.SelectedIndices.Count == 1)
            {
                if (MessageBox.Show("Delete Record ", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    TochenTnua shurat_haklada = (TochenTnua)lvwOutboxTochenTnua.Items[lvwOutboxTochenTnua.SelectedIndices[0]].Tag;
                    dblayer.DeleteTochenTnuaOutboxHaklada(shurat_haklada.RawDataNumber);
                    dblayer.ReadTochenTnuaForShuratHakladaListMain(lvwOutboxTochenTnua, shurat_haklada.TransactionGUID);
                }
            }         
        }

        private bool ValidateForm()
        {
            if (IsEmptyText(txtCodeParit))
                return false;

            if (IsEmptyText(txtKamutKlalit))
                return false;

            if (IsEmptyText(txtMechirYechida))
                return false;

            if (IsEmptyText(txtSchumShura))
                return false;

            if (IsEmptyText(txtTeurParit))
                return false;

            return true;
        }

        public bool IsEmptyText(TextBox txt)
        {
            if (txt.Text.Trim() == "")
            {
                txt.BackColor = Color.Pink;
                txt.Focus();
                return true;
            }

            txt.BackColor = Color.White;

            return false;
        }

        private void lvwOutboxTochenTnua_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwOutboxTochenTnua.SelectedIndices.Count == 1)
            {
                TochenTnua tochen_tnua = (TochenTnua)lvwOutboxTochenTnua.Items[lvwOutboxTochenTnua.SelectedIndices[0]].Tag;

                txtCodeParit.Text = tochen_tnua.CodeParitNumeri.ToString();
                txtKamutKlalit.Text = tochen_tnua.KamutKlalit.ToString();
                txtMechirYechida.Text = tochen_tnua.MechirYehida.ToString();
                txtSchumShura.Text = tochen_tnua.SchumShura.ToString();
                txtTeurParit.Text = tochen_tnua.TeurParitAlpha.ToString();
            }
            else
            {
                CleanForm();
            }

            btnUpdate.Enabled = (lvwOutboxTochenTnua.SelectedIndices.Count == 1);
            btnDelete.Enabled = (lvwOutboxTochenTnua.SelectedIndices.Count == 1);
        }

        private void frmHakladaTochenTnua_Load(object sender, EventArgs e)
        {
            dblayer.ReadTochenTnuaForShuratHakladaListMain(lvwOutboxTochenTnua, TransactionGUID);
        }

        private void txtKamut_Leave(object sender, EventArgs e)
        {
            txtSchumShura.Text = (txtKamutKlalit.GetInt() * txtMechirYechida.GetInt()).ToString();
        }
    }
}
