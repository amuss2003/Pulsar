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
    public partial class frmDataStructure : Form
    {
        public CompanyInfo Company_Info { get; set; }
        public DBLayer dblayer = null;
        public Parser parser { get; set; }
        public FiledStructure SelectedFiledStructure { get; set; }

        public enum OrderDirection
        {
            Up = 1,
            Down = 2
        }

        public frmDataStructure()
        {
            InitializeComponent();
        }

        private void frmDataStructure_Load(object sender, EventArgs e)
        {
            filedHolder1.Company_Info = Company_Info;
            FillFilesHolder();
        }

        private void FillFilesHolder()
        {
            filedHolder1.Add("CountryID");
            filedHolder1.Add("CompanyVAT");
            filedHolder1.Add("CompanyName");
            filedHolder1.Add("ActionCode");
            filedHolder1.Add("MisparMismach");
            filedHolder1.Add("TarichMismach");
            filedHolder1.Add("TarichAcher");
            filedHolder1.Add("ActionDetails");
            filedHolder1.Add("Maam");
            filedHolder1.Add("SchumPaturMaam");
            filedHolder1.Add("SchumMaam");
            filedHolder1.Add("SchumKolelMaam");
            filedHolder1.Add("Attachment");
            filedHolder1.LoadData();
            //filedHolder1.Add("Transfered");
            //filedHolder1.Add("CompamyInfoCountryID");
            //filedHolder1.Add("CompamyInfoVAT");
            //filedHolder1.Add("Readen");
            //filedHolder1.Add("DateSent");        
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            filedHolder1.Save();
        }
        
        private void btnExitImport_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void filedHolder1_Load(object sender, EventArgs e)
        {

        }

        private void filedHolder1_Changed(object sender, EventArgs e)
        {
            try
            {
                FiledStructure filed = (FiledStructure)sender;
                lblFiledSample.Text = txtDataSample.Text.Substring(filed.Pos, filed.Length);
            }
            catch (Exception)
            {
                
            }
        }
    }
}

//layoutPanel.GetControlFromPosition(x,y);
//Panel p = layoutPanel.Cell(x,y).Controls[0] as Panel;
//p.dosomethingCool();

