using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Pulsar
{
    public static class ControlsValidator
    {
        public static bool ValidateControl(ComboBox cmb)
        {
            if (cmb.SelectedIndex == -1)
            {
                cmb.Focus();
                cmb.BackColor = Color.Pink;
                return false;
            }
            else
                cmb.BackColor = Color.White;

            return true;
        }

        public static bool ValidateControl(TextBox txt)
        {
            if (txt.Text.Trim().Length == 0)
            {
                txt.Focus();
                txt.BackColor = Color.Pink;
                return false;
            }
            else
                txt.BackColor = Color.White;

            return true;
        }

        public static bool ValidateControlLength(TextBox txt, int length)
        {
            if (txt.Text.Trim().Length != length)
            {
                txt.Focus();
                txt.BackColor = Color.Pink;
                return false;
            }
            else
                txt.BackColor = Color.White;

            return true;
        }

        public static bool ValidateControlMinLength(TextBox txt, int length)
        {
            if (txt.Text.Trim().Length < length)
            {
                txt.Focus();
                txt.BackColor = Color.Pink;
                return false;
            }
            else
                txt.BackColor = Color.White;

            return true;
        }
    }
}
