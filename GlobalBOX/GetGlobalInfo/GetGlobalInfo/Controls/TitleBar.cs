using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Pulsar.Classes;

namespace Pulsar
{
    public partial class TitleBar : UserControl
    {
        public CompanyInfo company_info  { get; set; }

        public CompanyInfo Company_Info
        {
            get { return company_info; }
            set 
            {
                company_info = value;
                if (company_info != null)
                {
                    lblCompanyName.Text = Company_Info.CompanyName;
                    lblCompanyVAT.Text = Company_Info.CompanyVAT;
                }
            }
        }

        public TitleBar()
        {
            InitializeComponent();
        }

        private void TitleBar_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);

            using (LinearGradientBrush brush = new LinearGradientBrush(rc, Color.LightBlue, Color.White, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rc);
            }
        }

        private void TitleBar_Load(object sender, EventArgs e)
        {
            if (Company_Info != null)
            {
                lblCompanyName.Text = Company_Info.CompanyName;
                lblCompanyVAT.Text = Company_Info.CompanyVAT;
            }
        }

        private void TitleBar_Resize(object sender, EventArgs e)
        {
            this.Height = lblName.Height + 8;
        }
    }
}
