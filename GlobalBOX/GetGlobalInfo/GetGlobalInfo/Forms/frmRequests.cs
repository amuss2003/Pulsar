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
    public partial class frmRequests : Form
    {
        public CompanyInfo Company_Info { get; set; }
        public DBLayer dblayer = null;
        public Parser parser { get; set; }

        public frmRequests()
        {
            InitializeComponent();
        }

        private void frmRequests_Load(object sender, EventArgs e)
        {
            lvwRequests.DoubleBuffer();
            //lvwRequests.ShadowBrush = new SolidBrush(Color.FromArgb(49, 106, 197));
            titleBar1.Company_Info = Company_Info;
            //dblayer = new DBLayer(Application.StartupPath + @"\App_Data\");
            dblayer.Current_Company_Info = Company_Info;
            RefreshData();
        }

        private void RefreshData()
        {
            dblayer.ReadRequestsList(parser, lvwRequests, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, DateTime.Now.AddDays(-365), DateTime.Now);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            if (lvwRequests.SelectedIndices.Count == 1)
            {
                var item = lvwRequests.Items[lvwRequests.SelectedIndices[0]].Tag;
                if (item != null)
                {
                    if (item.GetType() == typeof(WriteCodeRequest))
                    {
                        WriteCodeRequest write_code_request = (WriteCodeRequest)item;
                        dblayer.Current_Company_Info = Company_Info;
                        
                        Company company = dblayer.GetCompany(write_code_request.CountryIDFrom, write_code_request.VatFrom);

                        if (write_code_request != null)
                        {
                            if (company == null)
                            {
                                company = new Company();
                                company.CompamyInfoCountryID = Company_Info.CompanyCountryID;
                                company.CompamyInfoVAT = Company_Info.CompanyVAT;
                                company.CountryID = Int32.Parse(write_code_request.CountryIDHaSholeah);
                                company.CompanyVAT = write_code_request.OsekMoorshehHaSholeah;
                                company.CompanyName = write_code_request.ShemHaSholeah;
                                dblayer.AddCompany(company);
                            }

                            dblayer.DeleteInbox(((RecordHeader)(write_code_request)).TransactionGUID);
                            SendOutboxData.SendAnswer(Company_Info, company, parser, dblayer, Company_Info.WriteCode);
                            RefreshData();
                            MessageBox.Show("Your request successfully sent to " + company.CompanyName, "Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
        }

        private void lvwRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = (lvwRequests.SelectedIndices.Count == 1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwRequests.SelectedIndices.Count == 1)
            {
                if (MessageBox.Show("Are you sure you wanty to delete request!?", "Delete Request", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    WriteCodeRequest wr = (WriteCodeRequest)lvwRequests.Items[lvwRequests.SelectedIndices[0]].Tag;
                    dblayer.Current_Company_Info = Company_Info;
                    dblayer.DeleteInbox(wr.TransactionGUID);
                    dblayer.ReadRequestsList(parser, lvwRequests, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, DateTime.Now.AddDays(-365), DateTime.Now);
                }
            }
        }
    }
}
