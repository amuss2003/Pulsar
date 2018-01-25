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
    public partial class frmActionType : Form
    {
        public CompanyInfo Company_Info { get; set; }

        public frmActionType()
        {
            InitializeComponent();
        }

        private void actionsListBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.actionsListBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.localInfoProtocolDataSet);

        }

        private void frmActionType_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.ActionsList' table. You can move, or remove it, as needed.
            this.actionsListTableAdapter.Fill(this.localInfoProtocolDataSet.ActionsList);
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.ActionsList' table. You can move, or remove it, as needed.
            this.actionsListTableAdapter.Fill(this.localInfoProtocolDataSet.ActionsList);
            titleBar1.Company_Info = Company_Info;
        }

        private void actionsListBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.actionsListBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.localInfoProtocolDataSet);

        }
    }
}
