using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GetGlobalInfo
{
    public partial class DateTimeControl : UserControl
    {
        public DateTimeControl()
        {
            InitializeComponent();

            txtDay.Text = DateTime.Now.Day.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();
        }

        private void DateTimeControl_Resize(object sender, EventArgs e)
        {
            this.Width = 64;
            this.Height = 20;
        }

        private void txtBackground_Enter(object sender, EventArgs e)
        {
            txtDay.Focus();
        }

        private void numericTextBox1_Enter(object sender, EventArgs e)
        {
            txtDay.Focus();
        }

        private void numericTextBox2_Enter(object sender, EventArgs e)
        {
            txtMonth.Focus();
        }

        public bool JustGotFocus { get; set; }

        private void txt_Enter(object sender, EventArgs e)
        {
            JustGotFocus = true;
            SelectAllText(sender);
        }

        private void SelectAllText(object sender)
        {
            ((TextBox)sender).SelectionStart = 0;
            ((TextBox)sender).SelectionLength = ((TextBox)sender).Text.Length;
        }

        public bool Jumping { get; set; }
        public bool YearJumping { get; set; }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).MaxLength == ((TextBox)sender).Text.Length)
            {
                Jumping = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtDay_Validating(object sender, CancelEventArgs e)
        {
            if ((txtDay.GetInt() == 0) || (txtDay.GetInt() > 31))
            {
                toolTip1.ToolTipTitle = "";
                //toolTip1.SetToolTip((Control)sender, "Day Error");
                toolTip1.Show("Day Error", this, 0);
                e.Cancel = true;
            }
            else
            {
                toolTip1.Hide(this);
            }
        }

        private void txtMonth_Validating(object sender, CancelEventArgs e)
        {
            if ((txtMonth.GetInt() == 0) || (txtMonth.GetInt() > 12))
            {
                toolTip1.ToolTipTitle = "";
                //toolTip1.SetToolTip((Control)sender, "Month Error");
                toolTip1.Show("Month Error", this, 0);
                e.Cancel = true;
            }
            else
            {
                DateTime today = new DateTime(txtYear.GetInt(), txtMonth.GetInt(), txtDay.GetInt());
                DateTime lastDayOfThisMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(-1);

                if (txtDay.GetInt() > lastDayOfThisMonth.Day)
                {
                    toolTip1.ToolTipTitle = "";
                    //toolTip1.SetToolTip((Control)sender, "Day Error");
                    toolTip1.Show("Day Error", this, 0);
                    e.Cancel = true;
                }
                else
                {
                    toolTip1.Hide(this);
                }
            }
        }

        private void txtYear_Validating(object sender, CancelEventArgs e)
        {
            if ((txtYear.GetInt() == 0) || (txtYear.GetInt() < 1900))
            {
                toolTip1.ToolTipTitle = "";
                //toolTip1.SetToolTip((Control)sender, "Day Error");
                toolTip1.Show("Year Error", this, 0);
                e.Cancel = true;
            }
            else
            {
                toolTip1.Hide(this);
            }
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                if (((TextBox)sender).MaxLength == ((TextBox)sender).Text.Length)
                {
                    JumpToNextControl();
                }
            }
        }

        private void JumpToNextControl()
        {
            try
            {
                ((Form)this.Parent).SelectNextControl(((Form)this.Parent).ActiveControl, true, true, true, true);
            }
            catch (Exception)
            {
                Form frm = (Form)(((Panel)this.Parent).Parent);
                frm.SelectNextControl(frm.ActiveControl, true, true, true, true);
            }
        }

        private void txtYear_Leave(object sender, EventArgs e)
        {
            JumpToNextControl();
        }

        //private void txtYear_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (JustGotFocus)
        //    {
        //        JustGotFocus = false;
        //        return;
        //    }

        //    if (e.KeyCode == Keys.Tab)
        //    {
        //        JumpToNextControl();
        //    }
        //}

        //private void txt_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Tab)
        //    {
        //        if (!YearJumping)
        //        {
        //            Jumping = true;
        //        }
        //        YearJumping = false;
        //    }
        //}
    }
}

//private void Form1_Click(object sender, EventArgs e)
//{
//    Control ctl;
//    ctl = (Control)sender;
//    ctl.SelectNextControl(ActiveControl, true, true, true, true);
//}
//private void button1_Click(object sender, EventArgs e)
//{
//    Control p;
//    p = ((Button) sender).Parent;
//    p.SelectNextControl(ActiveControl, true, true, true, true);
//}