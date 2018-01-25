using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pulsar
{
    public partial class frmEMail : Form
    {
        private string m_EMail = "";

        public frmEMail()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_EMail = txtEMail.Text;
            Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        public string GetEMail()
        {
            return m_EMail;
        }
    }
}