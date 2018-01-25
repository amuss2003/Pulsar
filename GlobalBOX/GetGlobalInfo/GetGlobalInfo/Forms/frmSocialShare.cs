using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pulsar.Forms
{
    public partial class frmSocialShare : Form
    {
        public frmSocialShare()
        {
            InitializeComponent();
        }

        private void frmWebShare_Load(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://ct1.addthis.com/static/r07/bookmark038.html");
        }
    }
}
