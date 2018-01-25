using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Pulsar
{
    public partial class FiledStructure : UserControl
    {
        public int Order { get; set; }
        private string _FieldName { get; set; }
        private int _Pos { get; set; }
        private int _Length { get; set; }

        public enum FieldType
        {
            Fixed = 1,
            Delmited = 2
        }
        
        public int Pos
        {
            get { return _Pos; }
            set
            {
                _Pos = value;
                txtPos.Text = _Pos.ToString();
            }
        }

        public int Length
        {
            get { return _Length; }
            set
            {
                _Length = value;
                txtLength.Text = _Length.ToString();
            }
        }

        public String FieldName
        {
            get { return _FieldName; }
            set
            {
                _FieldName = value;
                lblFieldName.Text = _FieldName;
            }
        }

        public FiledStructure()
        {
            InitializeComponent();
        }

        private void FiledStructure_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void FiledStructure_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromKnownColor(KnownColor.LightSteelBlue);
        }

        private void lblFiledName_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void FiledStructure_Load(object sender, EventArgs e)
        {

        }

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Down)
            //{
            //    //((UserControl)this.Parent).SelectNextControl(((UserControl)this.Parent).ActiveControl, true, true, true, true);
            //    Control ctrl = ((Form)this.Parent.Parent).ActiveControl;
            //    this.Parent.SelectNextControl(ctrl, true, true, true, true);
            //}
            //else if (e.KeyCode == Keys.Up)
            //{
            //    //((UserControl)this.Parent).SelectNextControl(((UserControl)this.Parent).ActiveControl, true, true, true, true);
            //    Control ctrl = ((Form)this.Parent.Parent).ActiveControl;
            //    this.Parent.SelectNextControl(ctrl, false, true, true, true);
            //}

            Length = txtLength.GetInt();
            Pos = txtPos.GetInt();

            if (Changed != null)
                Changed(this, e);
        }

        //public delegate void ChangedEventHandler(object sender, EventArgs e);
        //public event ChangedEventHandler Changed;

        public event EventHandler Changed;

        private void txtPos_TextChanged(object sender, EventArgs e)
        {
            //if (Changed != null)
            //    Changed(this, e);
        }

        private void txtLength_TextChanged(object sender, EventArgs e)
        {
            //if (Changed != null)
            //    Changed(this, e);
        }

        private void txtPos_Leave(object sender, EventArgs e)
        {
            Pos = txtPos.GetInt();
            if (Changed != null)
                Changed(this, e);
        }

        private void txtLength_Leave(object sender, EventArgs e)
        {
            Length = txtLength.GetInt();
            if (Changed != null)
                Changed(this, e);
        }
    }
}
