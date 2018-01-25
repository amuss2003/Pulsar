using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Pulsar.Forms
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            PrepareIniFileName();
        }

        public String IniFileName { get; set; }
        public IniFile iniFile { get; set; }

        private void PrepareIniFileName()
        {
            IniFileName = Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
            IniFileName = IniFileName.Substring(0, IniFileName.Length - ".exe".Length) + ".ini";
            iniFile = new IniFile(System.Windows.Forms.Application.StartupPath + @"\" + IniFileName);
        }

        public String FileMonitorPath { get; set; }
        public String PdfFilePath { get; set; }
        public String DataFilePath { get; set; }
        public String SendToCompany { get; set; }

        private void ReadIniFile()
        {
            cmbIntervalMailCheck.Value = Int32.Parse(iniFile.IniReadValue("MailCheck", "IntervalMailCheckValue"));

            String WorkWith = iniFile.IniReadValue("Meshakim", "WorkWith");

            radHashavshevt.Checked = (WorkWith == radHashavshevt.Name);
            radZoomPriority.Checked = (WorkWith == radZoomPriority.Name);
            radRivhit.Checked = (WorkWith == radRivhit.Name);
            radAdirim.Checked = (WorkWith == radAdirim.Name);
        }

        private void cmbIntervalMailCheck_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnUpdateCompany_Click(object sender, EventArgs e)
        {
            iniFile.IniWriteValue("MailCheck", "IntervalMailCheckValue", cmbIntervalMailCheck.Value.ToString());
            
            if(radHashavshevt.Checked)
                iniFile.IniWriteValue("Meshakim", "WorkWith", "radHashavshevt");
            if (radZoomPriority.Checked)
                iniFile.IniWriteValue("Meshakim", "WorkWith", "radZoomPriority");
            if (radRivhit.Checked)
                iniFile.IniWriteValue("Meshakim", "WorkWith", "radRivhit");
            if (radAdirim.Checked)
                iniFile.IniWriteValue("Meshakim", "WorkWith", "radAdirim");

            this.Close();
        }

        private void frmParameters_Load(object sender, EventArgs e)
        {
            ReadIniFile();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
