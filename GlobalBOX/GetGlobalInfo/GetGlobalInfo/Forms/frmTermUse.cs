using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pulsar
{
    public partial class frmTermUse : Form
    {
        public String TermUse { get; set; }

        public frmTermUse()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radCompany_CheckedChanged(object sender, EventArgs e)
        {
            TermUse = "company";
            btnNext.Enabled = true;
        }

        private void radPersonal_CheckedChanged(object sender, EventArgs e)
        {
            TermUse = "personal";
            btnNext.Enabled = true;
        }
    }
}
