using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Pulsar
{
    public partial class DateTimeControl : UserControl
    {
        public enum eDateType
        {
            FullDate = 1,
            MonthYear = 2,
            Year = 3
        }

        private eDateType _DateType = eDateType.FullDate;

        public eDateType DateType
        {
            get
            {
                return _DateType;
            }

            set
            {
                _DateType = value;
                ResetControl();
            }
        }

        private void ResetControl()
        {
            int deltaX = 0;
            switch (_DateType)
            {
                case eDateType.FullDate:
                    // 
                    // txtBackground
                    // 
                    this.txtBackground.Size = new System.Drawing.Size(64, 20);
                    // 
                    // txtDay
                    // 
                    this.txtDay.Location = new System.Drawing.Point(2, 3);
                    this.txtDay.Size = new System.Drawing.Size(12, 13);
                    this.txtDay.Visible = true;
                    // 
                    // txtDayMonthSeperator /
                    // 
                    this.txtDayMonthSeperator.Location = new System.Drawing.Point(14, 3);
                    this.txtDayMonthSeperator.Size = new System.Drawing.Size(5, 13);
                    this.txtDayMonthSeperator.Visible = true;
                    // 
                    // txtMonth
                    // 
                    this.txtMonth.Location = new System.Drawing.Point(19, 3);
                    this.txtMonth.Size = new System.Drawing.Size(12, 13);
                    this.txtMonth.Visible = true;
                    // 
                    // txtMonthYearSeperator /
                    // 
                    this.txtMonthYearSeperator.Location = new System.Drawing.Point(31, 3);
                    this.txtMonthYearSeperator.Size = new System.Drawing.Size(5, 13);
                    this.txtMonthYearSeperator.Visible = true;
                    // 
                    // txtYear
                    // 
                    this.txtYear.Location = new System.Drawing.Point(36, 3);
                    this.txtYear.Size = new System.Drawing.Size(25, 13);
                    this.txtYear.Visible = true;

                    this.Width = 64;
                    this.Height = 20;

                    break;
                case eDateType.MonthYear:
                    deltaX = txtMonth.Width + txtMonthYearSeperator.Width + txtYear.Width + 5;
                    // 
                    // txtBackground
                    // 
                    this.txtBackground.Size = new System.Drawing.Size(deltaX, 20);
                    // 
                    // txtDay
                    // 
                    this.txtDay.Text = "01";
                    this.txtDay.Visible = false;
                    // 
                    // txtDayMonthSeperator /
                    // 
                    this.txtDayMonthSeperator.Visible = false;
                    // 
                    // txtMonth
                    // 
                    this.txtMonth.Location = new System.Drawing.Point(2, 3);
                    this.txtMonth.Size = new System.Drawing.Size(12, 13);
                    this.txtMonth.Visible = true;
                    // 
                    // txtMonthYearSeperator /
                    // 
                    this.txtMonthYearSeperator.Location = new System.Drawing.Point(this.txtMonth.Width + 2, 3);
                    this.txtMonthYearSeperator.Size = new System.Drawing.Size(5, 13);
                    this.txtMonthYearSeperator.Visible = true;
                    // 
                    // txtYear
                    // 
                    this.txtYear.Location = new System.Drawing.Point(this.txtMonthYearSeperator.Width + this.txtMonth.Width + 2, 3);
                    this.txtYear.Size = new System.Drawing.Size(25, 13);
                    this.txtYear.Visible = true;

                    this.Width = deltaX;
                    this.Height = 20;

                    break;
                case eDateType.Year:
                    deltaX = this.txtYear.Width + 4;
                    // 
                    // txtBackground
                    // 
                    this.txtBackground.Size = new System.Drawing.Size(deltaX, 20);
                    // 
                    // txtDay
                    // 
                    this.txtDay.Text = "01";
                    this.txtDay.Visible = false;
                    // 
                    // txtDayMonthSeperator /
                    // 
                    this.txtDayMonthSeperator.Visible = false;
                    // 
                    // txtMonth
                    // 
                    this.txtMonth.Text = "01";
                    this.txtMonth.Visible = false;
                    // 
                    // txtMonthYearSeperator /
                    // 
                    this.txtMonthYearSeperator.Visible = false;
                    // 
                    // txtYear
                    // 
                    this.txtYear.Location = new System.Drawing.Point(2, 3);
                    this.txtYear.Size = new System.Drawing.Size(25, 13);
                    this.txtYear.Visible = true;

                    this.Width = deltaX;
                    this.Height = 20;

                    break;
                default:
                    break;
            }
        }

        public DateTimeControl()
        {
            InitializeComponent();

            txtDay.Text = Under10(DateTime.Now.Day);
            txtMonth.Text = Under10(DateTime.Now.Month);
            txtYear.Text = DateTime.Now.Year.ToString();
        }

        private String Under10(int number)
        {
            return (number < 10) ? "0" + number : number + "";
        }

        private void DateTimeControl_Resize(object sender, EventArgs e)
        {
            //this.Width = 64;
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

        private void txt_Enter(object sender, EventArgs e)
        {
            SelectAllText(sender);
        }

        private void SelectAllText(object sender)
        {
            ((TextBox)sender).SelectionStart = 0;
            ((TextBox)sender).SelectionLength = ((TextBox)sender).Text.Length;
        }

        public bool AutoComplete0 { get; set; }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (AutoComplete0)
            {
                AutoComplete0 = false;
                return;
            }
            else
            {
                if (((TextBox)sender).MaxLength == ((TextBox)sender).Text.Length)
                {
                    AutoComplete0 = true;
                    this.SelectNextControl(ActiveControl, true, true, true, true);
                    //SendKeys.SendWait("{TAB}");
                }
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
                //DateTime today = new DateTime(txtYear.GetInt(), txtMonth.GetInt(), txtDay.GetInt());
                try
                {
                    DateTime today = DateTime.Parse(txtDay.GetInt() + @"/" + txtMonth.GetInt() + @"/" + txtYear.GetInt(), new System.Globalization.CultureInfo("en-AU", false)); //Convert.ToDateTime(parts[5]);
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
                catch (Exception)
                {
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

        public bool Jumping { get; set; }

        private void JumpToNextControl()
        {
            try
            {
                Jumping = true;
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
            //if ((Control.ModifierKeys & Keys.Shift) != 0 //Ctrl Shift Alt Shift (Key + Shift)
            //if (Control.ModifierKeys != Keys.Shift) //Shift Only
            if (Jumping)
            {
                Jumping = false;
                return;
            }

            if (Control.ModifierKeys != Keys.Shift)
            {
                JumpToNextControl();
            }
        }

        public DateTime Value
        {
            get
            {
                return new DateTime(txtYear.GetInt(), txtMonth.GetInt(), txtDay.GetInt());
            }
            set
            {
                DateTime CurrentDate = value;
                txtDay.Text = Under10(CurrentDate.Day);
                txtMonth.Text = Under10(CurrentDate.Month);
                txtYear.Text = CurrentDate.Year.ToString();
            }
        }

        //public DateTime Value()
        //{
        //    return new DateTime(txtYear.GetInt(), txtMonth.GetInt(), txtDay.GetInt());
        //}

        private void txt_Leave(object sender, EventArgs e)
        {
            NumericTextBox txtbox = (NumericTextBox)sender;
            AutoComplete0 = true;
            txtbox.Text = Under10(txtbox.GetInt());
        }
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