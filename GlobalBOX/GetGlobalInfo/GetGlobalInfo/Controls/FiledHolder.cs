using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Pulsar.Classes;
using System.Collections;

namespace Pulsar.Controls
{
    public partial class FiledHolder : UserControl
    {
        public CompanyInfo Company_Info { get; set; }
        public FiledStructure SelectedFiledStructure { get; set; }
        public String CurrentDataLine { get; set; }

        public enum OrderDirection
        {
            Up = 1,
            Down = 2
        }

        public FiledHolder()
        {
            InitializeComponent();
        }

        public void InitINI()
        {
            PrepareIniFileName();
        }

        public void Add(String FiledName)
        {
            FiledStructure filedStructure = new Pulsar.FiledStructure();
            // 
            // filedStructure
            // 
            filedStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            filedStructure.FieldName = FiledName;
            filedStructure.Location = new System.Drawing.Point(0, (24 * pnlFileds.Controls.Count));
            filedStructure.Name = "filedStructure" + pnlFileds.Controls.Count;
            filedStructure.Order = pnlFileds.Controls.Count;
            filedStructure.Size = new System.Drawing.Size(181, 20);
            filedStructure.TabIndex = pnlFileds.Controls.Count;
            filedStructure.Enter += new System.EventHandler(filedStructure_Enter);
            filedStructure.Changed += new System.EventHandler(this.filedStructure_Changed);
            //filedStructure.Leave += new System.EventHandler(filedStructure_Leave);
            pnlFileds.Controls.Add(filedStructure);
            filedStructure.Visible = true;
        }

        public event EventHandler Changed;

        private void filedStructure_Changed(object sender, EventArgs e)
        {
            if (Changed != null)
                Changed(sender, e);
        }

        public void LoadData()
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    FiledStructure fld = (FiledStructure)ctrl;
                    String info = iniFile.IniReadValue(Company_Info.CompanySerialNumber + " Import Structure", fld.FieldName); //fld.Pos + "|" + fld.Length + "|" + fld.Order + "|" + fld.Top
                    if ((info != null) && (info != ""))
                    {
                        string[] parts = info.Split(new string[] { "|" }, StringSplitOptions.None);
                        fld.Pos = Int32.Parse(parts[0]);
                        fld.Length = Int32.Parse(parts[1]);
                        fld.Order = Int32.Parse(parts[2]);
                        fld.Top = Int32.Parse(parts[3]);
                    }
                }
            }
        }

        public void Save()
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    FiledStructure fld = (FiledStructure)ctrl;
                    iniFile.IniWriteValue(Company_Info.CompanySerialNumber + " Import Structure", fld.FieldName, fld.Pos + "|" + fld.Length + "|" + fld.Order + "|" + fld.Top);
                }
            }
        }

        public String IniFileName { get; set; }
        public IniFile iniFile { get; set; }

        private void PrepareIniFileName()
        {
            IniFileName = Path.GetFileName(Application.ExecutablePath);

            FileInfo fi = new FileInfo(IniFileName);
            fi = new FileInfo(fi.FullName);
            IniFileName = fi.Name;

            IniFileName = IniFileName.Substring(0, IniFileName.Length - ".exe".Length) + ".ini";
            iniFile = new IniFile(Application.StartupPath + @"\" + IniFileName);
        }

        private void ReadIniFile()
        {
            //FileMonitorPath = iniFile.IniReadValue("Structure", "Filed");
        }

        private void SaveIniFile()
        {
            //iniFile.IniWriteValue("Files", "Monitor", FileMonitorPath);
        }

        private void FiledHolder_Load(object sender, EventArgs e)
        {
            InitINI();
        }

        private void filedStructure_Enter(object sender, EventArgs e)
        {
            ResetControls();
            SelectedFiledStructure = (FiledStructure)sender;
            ((FiledStructure)sender).BackColor = Color.FromKnownColor(KnownColor.LightSteelBlue);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            SwapFileds(SelectedFiledStructure, OrderDirection.Up);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            SwapFileds(SelectedFiledStructure, OrderDirection.Down);
        }

        private void SwapFileds(FiledStructure selected_filed_structure, OrderDirection order_direction)
        {
            int top = -1;
            int order = -1;
            FiledStructure target_filed_structure = null;

            switch (order_direction)
            {
                case OrderDirection.Up:
                    target_filed_structure = GetPrev(selected_filed_structure);
                    if (target_filed_structure != null)
                    {
                        order = selected_filed_structure.Order;
                        top = selected_filed_structure.Top;
                        selected_filed_structure.Top = target_filed_structure.Top;
                        selected_filed_structure.Order = target_filed_structure.Order;
                        target_filed_structure.Top = top;
                        target_filed_structure.Order = order;
                    }
                    break;
                case OrderDirection.Down:
                    target_filed_structure = GetNext(selected_filed_structure);
                    if (target_filed_structure != null)
                    {
                        order = target_filed_structure.Order;
                        top = target_filed_structure.Top;
                        target_filed_structure.Top = selected_filed_structure.Top;
                        target_filed_structure.Order = selected_filed_structure.Order;
                        selected_filed_structure.Top = top;
                        selected_filed_structure.Order = order;
                    }
                    break;
                default:
                    break;
            }
        }

        private FiledStructure GetPrev(FiledStructure filed)
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    if ((((FiledStructure)ctrl).Order) == ((filed.Order) - 1))
                    {
                        return (FiledStructure)ctrl;
                    }
                }
            }

            return null;
        }

        private FiledStructure GetNext(FiledStructure filed)
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    if ((((FiledStructure)ctrl).Order) == ((filed.Order) + 1))
                    {
                        return (FiledStructure)ctrl;
                    }
                }
            }

            return null;
        }

        private void ResetControls()
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    ((FiledStructure)ctrl).BackColor = Color.FromKnownColor(KnownColor.Control);
                }
            }
        }

        public ArrayList GetFileds()
        {
            ArrayList result = new ArrayList();
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    result.Add((FiledStructure)ctrl);
                }
            }

            return result;
        }

        public FiledStructure GetFiled(String name)
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    if (((FiledStructure)ctrl).FieldName == name)
                    {
                        return ((FiledStructure)ctrl);
                    }
                }
            }

            return null;
        }

        public String GetFiledValue(String name, String line)
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    if (((FiledStructure)ctrl).FieldName == name)
                    {
                        return line.Substring(((FiledStructure)ctrl).Pos, ((FiledStructure)ctrl).Length);
                    }
                }
            }

            return null;
        }

        public String GetFiledValue(String name)
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    if (((FiledStructure)ctrl).FieldName == name)
                    {
                        if (((FiledStructure)ctrl).Length == 0)
                        {
                            return null;
                        }
                        else
                        {
                            return CurrentDataLine.Substring(((FiledStructure)ctrl).Pos, ((FiledStructure)ctrl).Length);
                        }
                    }
                }
            }

            return null;
        }

        public int GetFiledPosition(String name)
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    if (((FiledStructure)ctrl).FieldName == name)
                    {
                        if (((FiledStructure)ctrl).Pos == 0)
                        {
                            return -1;
                        }
                        else
                        {
                            return ((FiledStructure)ctrl).Pos;
                        }
                    }
                }
            }

            return -1;
        }
        public int GetFiledOrder(String name)
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    if (((FiledStructure)ctrl).FieldName == name)
                    {
                        if (((FiledStructure)ctrl).Pos == 0)
                        {
                            return -1;
                        }
                        else
                        {
                            return ((FiledStructure)ctrl).Order;
                        }
                    }
                }
            }

            return -1;
        }

        public String GetFiledName(Int32 order)
        {
            foreach (Control ctrl in pnlFileds.Controls)
            {
                if (ctrl is FiledStructure)
                {
                    if (((FiledStructure)ctrl).Order == order)
                    {
                        if (((FiledStructure)ctrl).Length == -1)
                        {
                            return null;
                        }
                        else
                        {
                            return ((FiledStructure)ctrl).FieldName;
                        }
                    }
                }
            }

            return null;
        }

        public String GetFiledValue(Int32 order, char deleimiter)
        {
            try
            {
                return CurrentDataLine.Split(deleimiter)[order];
            }
            catch (Exception)
            {                
                
            }

            return null;
        }
    }
}

//