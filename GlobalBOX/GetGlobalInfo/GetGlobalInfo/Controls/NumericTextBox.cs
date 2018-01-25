using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Pulsar
{
    public class NumericTextBox : TextBox
    {
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (this.Disabled)
            {
                e.Handled = true;
                return;
            }

            Decimal d = -1;

            if (!AllowNegative)
            {
                if (e.KeyChar == '-')
                {
                    e.Handled = true;
                    return;
                }
            }

            if (InputType == NumericType.IntegerInput)
            {
                if (e.KeyChar == '.')
                {
                    e.Handled = true;
                    return;
                }
            }

            if ((this.SelectionStart == 0) && (e.KeyChar == '.'))
            {
                e.Handled = true;
                return;
            }

            if ((this.SelectionStart == 0) && (e.KeyChar == '-'))
            {
                e.Handled = false;
                return;
            }

            if ((this.SelectionStart != 0) && (e.KeyChar == '-'))
            {
                e.Handled = true;
                return;
            }

            if ((Decimal.TryParse(this.Text + e.KeyChar.ToString(), out d)) || (e.KeyChar == '\b'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool Disabled { get; set; }
        public bool AllowNegative { get; set; }
        public NumericType InputType { get; set; }

        public enum NumericType
        {
            IntegerInput = 0,
            DecimalInput = 1,            
        }

        public int GetInt()
        {
            return (this.Text == "") ? 0 : int.Parse(this.Text);
        }

        public double GetDouble()
        {
            return (this.Text == "") ? 0 : double.Parse(this.Text);
        }

        public float GetFloat()
        {
            return (this.Text == "") ? 0 : float.Parse(this.Text);
        }

        public decimal GetDecimal()
        {
            return (this.Text == "") ? 0 : decimal.Parse(this.Text);
        }
    }
}
