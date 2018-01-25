using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTP24CP
{
    public partial class frmProgress : Form
    {
        public bool CloseProgress { get; set; }

        public frmProgress()
        {
            InitializeComponent();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {

        }

        private void frmProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseProgress)
            {
                e.Cancel = true; // this cancels the close event. 
            }
        }
    }
}
