using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GetGlobalInfo;

namespace GetGlobalInfo
{
    public partial class frmCreateData : Form
    {
        public String CountryID { get; set; }
        public String CompanyVAT { get; set; }
        public String ReadCode { get; set; }
        public String WriteCode { get; set; }
        public String Maam { get; set; }
        public Parser parser { get; set; }
        public DBLayer dblayer = null;

        public frmCreateData()
        {
            InitializeComponent();
        }

        private void createDataBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.createDataBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.localInfoProtocolDataSet);

        }

        private void createDataBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.createDataBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.localInfoProtocolDataSet);

        }

        private void frmCreateData_Load(object sender, EventArgs e)
        {
            dblayer = new DBLayer(Application.StartupPath + @"\App_Data\");
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.Companies' table. You can move, or remove it, as needed.
            this.companiesTableAdapter.Fill(this.localInfoProtocolDataSet.Companies);
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.ActionsList' table. You can move, or remove it, as needed.
            this.actionsListTableAdapter.Fill(this.localInfoProtocolDataSet.ActionsList);
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.CreateData' table. You can move, or remove it, as needed.
            this.createDataTableAdapter.Fill(this.localInfoProtocolDataSet.CreateData);
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.CreateData' table. You can move, or remove it, as needed.
            //companiesBindingSource - CompanyVAT

            txtMaam.Text = Maam.ToString();
        }


        public String cmd(String command_number, String txt)
        {
            return command_number + txt + "|";
        }

        public String cmd(String command_number, ComboBox cmbbox)
        {
            return command_number + cmbbox.Text + "|";
        }

        public String cmd(String command_number, TextBox txtbox)
        {
            return command_number + txtbox.Text + "|";
        }

        public String cmd(String command_number, DateTimePicker dtpicker)
        {
            return command_number + dtpicker.Value.ToShortDateString() + "|";
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btnCompanies_Click(object sender, EventArgs e)
        {
            frmBuisnessIndex frm = new frmBuisnessIndex();
            frm.ShowDialog();
        }

        private void txtKolelMaam_TextChanged(object sender, EventArgs e)
        {
            double kollelMaam = txtSchumKolelMaam.GetDouble();
            double maam = Convert.ToDouble(Maam);
            double schumLefniMaam = Math.Round(kollelMaam / (1 + (maam / 100)), 2);
            txtLefniMaam.Text = schumLefniMaam.ToString();
            txtSchumHaMaam.Text = (kollelMaam - schumLefniMaam).ToString();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

            //ofdAttachment.InitialDirectory = "c:\\";
            ofdAttachment.FileName = "";
            ofdAttachment.Filter = "txt files (*.txt)|*.txt|pdf files (*.pdf)|*.pdf";
            ofdAttachment.FilterIndex = 2;
            ofdAttachment.RestoreDirectory = true;

            DialogResult result = ofdAttachment.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                attachmnentTextBox.Text = ofdAttachment.FileName;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            KoteretTnua koteret_tnua = new KoteretTnua();
            koteret_tnua.CountryIDFrom = Int32.Parse(CountryID);
            koteret_tnua.CountryIDTo = dblayer.GetCompanyCountryID(companyIDComboBox.Text);
            koteret_tnua.VatTo = companyVATComboBox.Text;
            koteret_tnua.VatFrom = CompanyVAT;

            //KT|011|021231|0301/01/2012|0401/02/2012|0503/01/2012|0604/01/2012|07ClientName|08123456789|09bla bla|10100.20|1110.5|1216.5|13116.5|141

            //01: kt.MisparPnimi           //מספר פנימי          -
            //02: kt.MisparMismach         //מספר מסמך           *
            //03: kt.TarichMismach         //תאריך מסמך          *
            //04: kt.TarichKovea_Divuch    //תאריך קובע/דיווח    -
            //05: kt.TarichMishloah        //תאריך משלוח         *                          
            //06: kt.TarichAher            //תאריך אחר           *
            //07: kt.ShemHaLakoh           //שם הלקוח            *                                   
            //08: kt.OsekMoorshehLakoh     //עוסק מורשה לקוח    *
            //09: kt.MeidaNosaf            //מידע נוסף           *
            //10: kt.SchumLifneMaam        //סכום לפני מע"מ      *
            //11: kt.SchumPaturMeMaam      //סכום פטור ממע"מ     *
            //12: kt.SchumHaMaam           //סכום המע"מ          *
            //13: kt.SchumKolelMaam        //סכום כולל מע"מ      *
            //14: kt.SugTnua               //סוג תנועה            *

            String kt = "KT|";
            kt += cmd("02", txtMisparMismach);
            kt += cmd("03", dtpTarichMismach);
            kt += cmd("05", DateTime.Now.ToShortDateString());
            kt += cmd("06", dtpTarichAcher);
            kt += cmd("07", companyNameComboBox);
            kt += cmd("08", companyVATComboBox);
            kt += cmd("09", txtActionDetails);
            kt += cmd("10", txtLefniMaam);
            kt += cmd("11", txtSchumPaturMmam);
            kt += cmd("12", txtSchumHaMaam);
            kt += cmd("13", txtSchumKolelMaam);
            kt += cmd("14", cmbActionType);

            koteret_tnua.Data = kt.Substring(0, kt.Length - 1);

            dblayer.AddCreateRecord(koteret_tnua, WriteCode, attachmnentTextBox.Text);

            CleanForm();
        }

        private void CleanForm()
        {
            //companyIDComboBox.SelectedIndex = 0;
            cmbActionType.SelectedIndex = 0;
            txtMisparMismach.Text = "";
            dtpTarichMismach.Value = DateTime.Now;
            dtpTarichAcher.Value = DateTime.Now;
            txtActionDetails.Text = "";
            txtSchumPaturMmam.Text = "";
            txtSchumKolelMaam.Text = "";
            attachmnentTextBox.Text = "";
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
