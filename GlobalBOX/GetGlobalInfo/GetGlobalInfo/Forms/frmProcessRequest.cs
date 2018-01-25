using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pulsar
{
    public partial class frmProcessRequest : Form
    {

        public frmProcessRequest()
        {
            InitializeComponent();
        }

        private void frmProcessRequest_Load(object sender, EventArgs e)
        {
            BitmapRegion.CreateControlRegion(this, global::Pulsar.Properties.Resources.please_wait450x1381);
            Application.DoEvents();
        }
    }
}
