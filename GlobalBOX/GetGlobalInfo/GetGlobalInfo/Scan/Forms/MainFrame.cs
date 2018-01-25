using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

//using TwainLib;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;
using Pulsar.Classes;

namespace Pulsar
{
    public class MainFrame : System.Windows.Forms.Form, IMessageFilter
    {
        private System.Windows.Forms.MdiClient mdiClient1;
        private System.Windows.Forms.MenuItem menuMainFile;
        private System.Windows.Forms.MenuItem menuItemScan;
        private System.Windows.Forms.MenuItem menuItemSelSrc;
        private System.Windows.Forms.MenuItem menuMainWindow;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemSepr;
        private System.Windows.Forms.MainMenu mainFrameMenu;
        private MenuItem menuItemLanguage;
        private MenuItem menuItemEnglish;
        private MenuItem menuItemHebrew;
        private MenuItem menuItemID;
        private IContainer components;

        private string m_FileName = "";
        private string m_ScannerPath = "E:\\Archive\\";
        private string m_LicenceAndInsurancePath = "E:\\ביטוחים ורישיונות\\";
        private MenuItem menuItemOptions;
        private MenuItem menuItemAutoScanNext;
        private string m_TargetPath = "";
        private bool m_ShowSnif = true;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonID;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButtonSave;
        private bool m_TargetNumeric = true;
        private PicForm m_ActiveScanForm = null;
        private MenuItem menuItemCheckData;
        private Image m_CurrentImage = null;
        private MenuItem menuItemHelp;
        private ToolStripMenuItem toolStripButtonInsuranceScan;
        private ToolStripMenuItem toolStripButtonInsuranceReNew;
        private PictureBox picPreview;
        private PictureBox picOCR;
        private PictureBox picNoImage;
        private ToolStripMenuItem TofesOutToolStripMenuItem;
        private ToolStripMenuItem TofesInToolStripMenuItem;
        private ToolStripButton toolStripButtonSearch;
        private string m_ScanType = "";

        public CompanyInfo Company_Info { get; set; }
        // hook the windows form load event
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public MainFrame()
        {
            InitializeComponent();
            tw = new Twain();
            tw.Init(this.Handle);
        }

        public void SetActiveScanForm(PicForm ActiveScanForm)
        {
            m_ActiveScanForm = ActiveScanForm;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tw.Finish();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.menuMainFile = new System.Windows.Forms.MenuItem();
            this.menuItemSelSrc = new System.Windows.Forms.MenuItem();
            this.menuItemScan = new System.Windows.Forms.MenuItem();
            this.menuItemID = new System.Windows.Forms.MenuItem();
            this.menuItemSepr = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.mainFrameMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuMainWindow = new System.Windows.Forms.MenuItem();
            this.menuItemLanguage = new System.Windows.Forms.MenuItem();
            this.menuItemEnglish = new System.Windows.Forms.MenuItem();
            this.menuItemHebrew = new System.Windows.Forms.MenuItem();
            this.menuItemOptions = new System.Windows.Forms.MenuItem();
            this.menuItemAutoScanNext = new System.Windows.Forms.MenuItem();
            this.menuItemCheckData = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.mdiClient1 = new System.Windows.Forms.MdiClient();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonID = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.TofesOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TofesInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonInsuranceScan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonInsuranceReNew = new System.Windows.Forms.ToolStripMenuItem();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.picOCR = new System.Windows.Forms.PictureBox();
            this.picNoImage = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOCR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMainFile
            // 
            this.menuMainFile.Index = 0;
            this.menuMainFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSelSrc,
            this.menuItemScan,
            this.menuItemSepr,
            this.menuItemExit});
            this.menuMainFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuMainFile.Text = "&File";
            // 
            // menuItemSelSrc
            // 
            this.menuItemSelSrc.Index = 0;
            this.menuItemSelSrc.MergeOrder = 11;
            this.menuItemSelSrc.Text = "&Select Source...";
            this.menuItemSelSrc.Click += new System.EventHandler(this.menuItemSelSrc_Click);
            // 
            // menuItemScan
            // 
            this.menuItemScan.Index = 1;
            this.menuItemScan.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemID});
            this.menuItemScan.MergeOrder = 12;
            this.menuItemScan.Text = "&Acquire...";
            this.menuItemScan.Click += new System.EventHandler(this.menuItemScan_Click);
            // 
            // menuItemID
            // 
            this.menuItemID.Index = 0;
            this.menuItemID.Text = "I&D";
            this.menuItemID.Click += new System.EventHandler(this.menuItemID_Click);
            // 
            // menuItemSepr
            // 
            this.menuItemSepr.Index = 2;
            this.menuItemSepr.MergeOrder = 19;
            this.menuItemSepr.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 3;
            this.menuItemExit.MergeOrder = 21;
            this.menuItemExit.Text = "&Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // mainFrameMenu
            // 
            this.mainFrameMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuMainFile,
            this.menuMainWindow,
            this.menuItemLanguage,
            this.menuItemOptions,
            this.menuItemHelp});
            // 
            // menuMainWindow
            // 
            this.menuMainWindow.Index = 1;
            this.menuMainWindow.MdiList = true;
            this.menuMainWindow.Text = "&Window";
            // 
            // menuItemLanguage
            // 
            this.menuItemLanguage.Index = 2;
            this.menuItemLanguage.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEnglish,
            this.menuItemHebrew});
            this.menuItemLanguage.Text = "&Language";
            // 
            // menuItemEnglish
            // 
            this.menuItemEnglish.Checked = true;
            this.menuItemEnglish.Index = 0;
            this.menuItemEnglish.Text = "&English";
            this.menuItemEnglish.Click += new System.EventHandler(this.menuItemEnglish_Click);
            // 
            // menuItemHebrew
            // 
            this.menuItemHebrew.Index = 1;
            this.menuItemHebrew.Text = "&Hebrew";
            this.menuItemHebrew.Click += new System.EventHandler(this.menuItemHebrew_Click);
            // 
            // menuItemOptions
            // 
            this.menuItemOptions.Index = 3;
            this.menuItemOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAutoScanNext,
            this.menuItemCheckData});
            this.menuItemOptions.Text = "&Options";
            // 
            // menuItemAutoScanNext
            // 
            this.menuItemAutoScanNext.Index = 0;
            this.menuItemAutoScanNext.Text = "&Auto Scan Next";
            this.menuItemAutoScanNext.Click += new System.EventHandler(this.menuItemAutoScanNext_Click);
            // 
            // menuItemCheckData
            // 
            this.menuItemCheckData.Index = 1;
            this.menuItemCheckData.Text = "&Check Data";
            this.menuItemCheckData.Click += new System.EventHandler(this.menuItemCheckData_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 4;
            this.menuItemHelp.Text = "&Help";
            // 
            // mdiClient1
            // 
            this.mdiClient1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdiClient1.Location = new System.Drawing.Point(0, 68);
            this.mdiClient1.Name = "mdiClient1";
            this.mdiClient1.Size = new System.Drawing.Size(632, 357);
            this.mdiClient1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonID,
            this.toolStripSeparator1,
            this.toolStripButtonSearch,
            this.toolStripButtonSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(632, 68);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonID
            // 
            this.toolStripButtonID.Image = global::Pulsar.Properties.Resources.scanner2;
            this.toolStripButtonID.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonID.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonID.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonID.Name = "toolStripButtonID";
            this.toolStripButtonID.Size = new System.Drawing.Size(72, 65);
            this.toolStripButtonID.Text = "סרוק מסמך";
            this.toolStripButtonID.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonID.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonID.ToolTipText = "תעודת זהות";
            this.toolStripButtonID.Click += new System.EventHandler(this.toolStripButtonID_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 68);
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.Image = global::Pulsar.Properties.Resources.SearchSmall;
            this.toolStripButtonSearch.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Size = new System.Drawing.Size(52, 65);
            this.toolStripButtonSearch.Text = "חיפוש";
            this.toolStripButtonSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonSearch.ToolTipText = "תעודת זהות";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.Enabled = false;
            this.toolStripButtonSave.Image = global::Pulsar.Properties.Resources.SaveOn;
            this.toolStripButtonSave.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(52, 65);
            this.toolStripButtonSave.Text = "שמור";
            this.toolStripButtonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // TofesOutToolStripMenuItem
            // 
            this.TofesOutToolStripMenuItem.Name = "TofesOutToolStripMenuItem";
            this.TofesOutToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // TofesInToolStripMenuItem
            // 
            this.TofesInToolStripMenuItem.Name = "TofesInToolStripMenuItem";
            this.TofesInToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripButtonInsuranceScan
            // 
            this.toolStripButtonInsuranceScan.Name = "toolStripButtonInsuranceScan";
            this.toolStripButtonInsuranceScan.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripButtonInsuranceReNew
            // 
            this.toolStripButtonInsuranceReNew.Name = "toolStripButtonInsuranceReNew";
            this.toolStripButtonInsuranceReNew.Size = new System.Drawing.Size(32, 19);
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(12, 81);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(100, 50);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPreview.TabIndex = 2;
            this.picPreview.TabStop = false;
            this.picPreview.Visible = false;
            // 
            // picOCR
            // 
            this.picOCR.Location = new System.Drawing.Point(160, 81);
            this.picOCR.Name = "picOCR";
            this.picOCR.Size = new System.Drawing.Size(200, 50);
            this.picOCR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picOCR.TabIndex = 3;
            this.picOCR.TabStop = false;
            this.picOCR.Visible = false;
            // 
            // picNoImage
            // 
            this.picNoImage.Image = global::Pulsar.Properties.Resources.scannerNormal;
            this.picNoImage.Location = new System.Drawing.Point(402, 81);
            this.picNoImage.Name = "picNoImage";
            this.picNoImage.Size = new System.Drawing.Size(128, 128);
            this.picNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picNoImage.TabIndex = 4;
            this.picNoImage.TabStop = false;
            this.picNoImage.Visible = false;
            // 
            // MainFrame
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(632, 425);
            this.Controls.Add(this.picNoImage);
            this.Controls.Add(this.picOCR);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mdiClient1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mainFrameMenu;
            this.Name = "MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanning Control Center System  Ver 4";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrame_FormClosing);
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOCR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNoImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion



        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void menuItemScan_Click(object sender, System.EventArgs e)
        {

        }

        private void StartScan()
        {
            if (!msgfilter)
            {
                this.Enabled = false;
                msgfilter = true;
                Application.AddMessageFilter(this);
            }
            tw.Acquire();
        }

        private void menuItemSelSrc_Click(object sender, System.EventArgs e)
        {
            tw.Select();
        }


        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            TwainCommand cmd = tw.PassMessage(ref m);
            if (cmd == TwainCommand.Not)
                return false;

            switch (cmd)
            {
                case TwainCommand.CloseRequest:
                    {
                        EndingScan();
                        tw.CloseSrc();
                        break;
                    }
                case TwainCommand.CloseOk:
                    {
                        EndingScan();
                        tw.CloseSrc();
                        break;
                    }
                case TwainCommand.DeviceEvent:
                    {
                        break;
                    }
                case TwainCommand.TransferReady:
                    {
                        ArrayList pics = tw.TransferPictures();
                        EndingScan();
                        tw.CloseSrc();
                        picnumber++;
                        for (int i = 0; i < pics.Count; i++)
                        {
                            IntPtr img = (IntPtr)pics[i];
                            PicForm newpic = new PicForm(img);
                            newpic.SetLanguage(menuItemEnglish.Checked ? 0 : 1);
                            newpic.MdiParent = this;
                            int picnum = i + 1;
                            //newpic.Text = "ScanPass" + picnumber.ToString() + "_Pic" + picnum.ToString();

                            if (m_TargetPath == "פעיל")
                            {
                                newpic.Text = m_ScanType + m_FileName;
                                newpic.SetPath(m_LicenceAndInsurancePath + m_TargetPath + "\\");
                            }
                            else
                            {
                                newpic.Text = m_FileName;
                                newpic.SetPath(m_ScannerPath + m_TargetPath + "\\");
                            }

                            if (menuItemAutoScanNext.Checked)
                            {
                                newpic.AutoScanNext();
                            }

                            newpic.Show();
                            toolStripButtonSave.Enabled = true;

                            if (menuItemAutoScanNext.Checked)
                            {
                                while (newpic.Visible)
                                {
                                    Application.DoEvents();
                                }

                                Scan(m_TargetPath, m_ShowSnif, m_TargetNumeric);
                            }
                        }
                        break;
                    }
            }

            return true;
        }

        private void EndingScan()
        {
            if (msgfilter)
            {
                Application.RemoveMessageFilter(this);
                msgfilter = false;
                this.Enabled = true;
                this.Activate();
            }
        }

        private bool msgfilter;
        private Twain tw;
        private int picnumber = 0;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "PostMessage", CharSet = CharSet.Ansi)]
        static extern int PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const uint WM_SYSCOMMAND = 0x0112;
        const int SC_RESTORE = 0xF120;

        //[STAThread]
        public void Main() //static 
        {
            if (Twain.ScreenBitDepth < 15)
            {
                MessageBox.Show("Need high/true-color video mode!", "Screen Bit Depth", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool createdNew = true;

            using (Mutex mutex = new Mutex(true, "HagarScannerSystem", out createdNew))
            {
                if (createdNew)
                {
                    MainFrame mf = new MainFrame();
                    Application.Run(mf);
                }
                else
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            if (IsIconic(process.MainWindowHandle))
                            {
                                PostMessage(process.MainWindowHandle, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, IntPtr.Zero);
                            }
                            else
                            {
                                SwitchToThisWindow(process.MainWindowHandle, true);
                                SetActiveWindow(process.MainWindowHandle);
                            }
                            break;
                        }
                    }
                }
            }

        }

        private void menuItemEnglish_Click(object sender, EventArgs e)
        {
            menuItemEnglish.Checked = true;
            menuItemHebrew.Checked = false;
            SetEnglish();
        }

        private void menuItemHebrew_Click(object sender, EventArgs e)
        {
            menuItemEnglish.Checked = false;
            menuItemHebrew.Checked = true;
            SetHebrew();
        }

        private void SetEnglish()
        {
            menuMainFile.Text = "&File";
            menuItemSelSrc.Text = "&Select Source...";
            menuItemScan.Text = "&Acquire...";
            menuItemExit.Text = "&Exit";
            menuMainWindow.Text = "&Window";
            menuItemLanguage.Text = "&Language";

            menuItemID.Text = "&ID";

            menuItemAutoScanNext.Text = "&Auto Scan Next";
            menuItemOptions.Text = "&Options";
            menuItemCheckData.Text = "&Check Data";

            menuItemHelp.Text = "&Help";
        }

        private void SetHebrew()
        {
            menuMainFile.Text = "&קובץ";
            menuItemSelSrc.Text = "&בחר/י מקור";
            menuItemScan.Text = "&סריקה";
            menuItemExit.Text = "&יציאה";
            menuMainWindow.Text = "&חלונות";
            menuItemLanguage.Text = "&שפה";

            menuItemID.Text = "&תעודת זהות";

            menuItemAutoScanNext.Text = "&סריקה אוטומטית";
            menuItemOptions.Text = "&אפשרויות";
            menuItemCheckData.Text = "&בדיקת נתונים";

            menuItemHelp.Text = "&עזרה";
        }

        private void Scan(string Target, bool bShowSnif, bool bNumeric)
        {
            frmGetData frm = new frmGetData();
            frm.SetLanguage(menuItemEnglish.Checked ? 0 : 1);

            this.Text = "Scanning Control Center System - ";// +(mnuEliran.Checked ? "אלירן" : "זוהר");

            m_ShowSnif = bShowSnif;
            m_TargetNumeric = bNumeric;

            if (!bShowSnif)
            {
                frm.HideSnif();
            }
            else
            {
                //if (m_HozeAndSnifSearch != "")
                //{
                //    if (MessageBox.Show("ðîöà חיפוש ðúåðéí ÷åãí äàí ìùúåì?", "÷ìéèä îחיפוש", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        frm.Snif = m_Snif;
                //        frm.Hoze = m_Hoze;
                //    }
                //    else
                //    {
                //        m_HozeAndSnifSearch = "";
                //        m_Hoze = "0";
                //        //m_Snif = "";
                //        m_Snif = ((Branch == 0) ? "" : Branch.ToString());
                //    }
                //}
            }

            if (!bNumeric)
            {
                frm.SetNotNumeric();
            }

            if (frm.Snif == null)
            {
                frm.Snif = Branch.ToString();
                frm.Hoze = "0";
            }

            //if (Target == "פעיל")
            //    frm.SetScannerTargetPath(m_LicenceAndInsurancePath + Target + "\\");
            //else
            frm.SetScannerTargetPath(m_ScannerPath + Target + "\\");

            frm.SetScanType(m_ScanType);
            frm.SetScanTypePic(m_CurrentImage);

            frm.Text = "Scan " + Target;
            if (m_ShowSnif)
            {
                frm.Show();
                frm.Hide();
            }
            else
            {
                frm.ShowDialog();
            }

            m_FileName = frm.GetData().Trim();

            if (Target == "פעיל")
            {
                if (m_FileName.Trim().Length != 7)
                {
                    m_FileName = "";
                }
            }

            if (m_FileName.Trim() != "")
            {
                m_TargetPath = Target;
                StartScan();
            }

            frm.Close();
        }

        private void MultiSave(PicForm scannedForm)
        {
            string Target = m_TargetPath;
            bool bShowSnif = m_ShowSnif;
            bool bNumeric = m_TargetNumeric;

            scannedForm.Focus();

            frmGetData frm = new frmGetData();
            frm.SetLanguage(menuItemEnglish.Checked ? 0 : 1);
            this.Text = "Scanning Control Center System - ";// +(mnuEliran.Checked ? "אלירן" : "זוהר");

            m_ShowSnif = bShowSnif;
            m_TargetNumeric = bNumeric;

            if (!bShowSnif)
            {
                frm.HideSnif();
            }

            if (!bNumeric)
            {
                frm.SetNotNumeric();
            }

            if (frm.Snif == null)
            {
                frm.Snif = Branch.ToString();
                frm.Hoze = "";
            }

            //if (Target == "פעיל")
            //    frm.SetScannerTargetPath(m_LicenceAndInsurancePath + Target + "\\");
            //else
            frm.SetScannerTargetPath(m_ScannerPath + Target + "\\");

            frm.SetScanType(m_ScanType);
            frm.SetScanTypePic(m_CurrentImage);
            frm.SetText = txtNumber;
            frm.Text = "Scan " + Target;
            frm.ShowDialog();
            m_FileName = frm.GetData().Trim();

            //if (Target == "פעיל")
            //{
            //    if (m_FileName.Trim().Length != 7)
            //    {
            //        m_FileName = "";
            //    }
            //}

            if (m_FileName.Trim() != "")
            {
                if (m_FileName.Trim().Length == 7)
                {
                    m_FileName = m_ScanType + m_FileName.Trim();
                }

                m_TargetPath = Target;
                scannedForm.Text = m_FileName.Trim();
                //StartScan();
            }

            frm.Close();

            txtNumber = "";
        }

        private void menuItemID_Click(object sender, EventArgs e)
        {
            Scan("ID", false, true);
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            menuItemEnglish.Checked = false;
            menuItemHebrew.Checked = true;
            //mnuBranchAll.Checked = true;
            Branch = 0;
            SetHebrew();
            //ControlPainter.PaintControl(this.CreateGraphics(), this);
        }

        private void menuItemAutoScanNext_Click(object sender, EventArgs e)
        {
            menuItemAutoScanNext.Checked = !menuItemAutoScanNext.Checked;
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void SetImage(object sender)
        {
            try
            {
                m_CurrentImage = ((ToolStripButton)sender).Image;
            }
            catch (Exception)
            {
                m_CurrentImage = ((ToolStripMenuItem)sender).Image;
            }
        }

        private void SetImage(ToolStripSplitButton sender)
        {
            m_CurrentImage = sender.Image;
        }

        private void toolStripButtonID_Click(object sender, EventArgs e)
        {
            SetImage(sender);
            Scan("ID", false, true);
        }

        private void toolStripButtonShowHideMenu_Click(object sender, EventArgs e)
        {
            menuMainFile.Visible = !menuMainFile.Visible;
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            Form[] charr = this.MdiChildren;

            //For each child form set the window state to Maximized 
            foreach (PicForm chform in charr)
            {
                chform.SaveTemp(chform.Handle.ToString());
                if (m_ScanType == "R")
                {
                    GetLicenseOCR(chform.Handle.ToString());
                    //chform.Text = m_ScanType + (txtNumber != "" ? txtNumber : chform.Text);
                    chform.Text = "R" + (txtNumber != "" ? txtNumber : chform.Text);
                }

                if (m_ScanType == "B")
                {
                    GetInsuranceOCR(chform.Handle.ToString());
                    //chform.Text = m_ScanType + (txtNumber != "" ? txtNumber : chform.Text);
                    chform.Text = "B" + (txtNumber != "" ? txtNumber : chform.Text);
                }


                //if (charr.Length > 1)
                //{                    
                //    MultiSave(chform);
                //}
                //else
                //{
                //    chform.Save();
                //}
                //chform.AutoScrollPosition.X
                //chform.AutoScrollPosition.Y
                //chform.AutoScrollMargin
                //chform.AutoScrollMinSize
                MultiSave(chform);
                chform.Save();
                chform.Close();
            }

            toolStripButtonSave.Enabled = false;
        }

        private void menuItemCheckData_Click(object sender, EventArgs e)
        {
            frmCheckInfo frm = new frmCheckInfo();
            frm.Path = m_ScannerPath;
            frm.ShowDialog();
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
        }

        //private String m_HozeAndSnifSearch = "";
        //private String m_Snif = "";
        //private String m_Hoze = "";

        private String txtNumber = "";
        private void GetLicenseOCR(String pFileName)
        {
            using (Stream s = new FileStream(@"c:\temp\temp_ocr_" + pFileName + ".jpg", FileMode.Open))
            {
                picPreview.Image = Image.FromStream(s);
                s.Close();
            }

            //picPreview.Image = Image.FromFile(@"c:\temp\temp_ocr.jpg");
            int retriers = 0;
            int d = 7;
            do
            {
                picOCR.Image = cropImageArea(picPreview, new Rectangle(1430, 270 + (d * retriers), 220, 45), picOCR);

                //OCR Prepare
                SaveToMonoChrome(picOCR.Image, @"c:\temp\pic.bmp");
                retriers++;
            }
            while ((txtNumber == "") && (retriers < 4));

            picPreview.Image = picNoImage.Image;
        }

        private void GetInsuranceOCR(String pFileName)
        {
            using (Stream s = new FileStream(@"c:\temp\temp_ocr_" + pFileName + ".jpg", FileMode.Open))
            {
                picPreview.Image = Image.FromStream(s);
                s.Close();
            }

            //picPreview.Image = Image.FromFile(@"c:\temp\temp_ocr.jpg");
            int retriers = 0;
            int d = 7;
            do
            {
                picOCR.Image = cropImageArea(picPreview, new Rectangle(1255, 530 + (d * retriers), 300, 45), picOCR);

                //OCR Prepare
                SaveToMonoChrome(picOCR.Image, @"c:\temp\pic.bmp");
                retriers++;
            }
            while ((txtNumber == "") && (retriers < 4));

            picPreview.Image = picNoImage.Image;
        }

        private Image cropImageArea(PictureBox pic, Rectangle rect)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics gImage = Graphics.FromImage(bmp);

            Graphics gPic = pic.CreateGraphics();

            gImage.Clear(Color.White);
            gImage.DrawImage(pic.Image, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);

            gPic.DrawImage(bmp, 0, 0);
            return (Image)bmp;
        }

        private Image cropImageArea(PictureBox pic, Rectangle rect, PictureBox picTarget)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics gImage = Graphics.FromImage(bmp);

            Graphics gPic = picTarget.CreateGraphics();

            gImage.Clear(Color.White);
            gImage.DrawImage(pic.Image, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);

            gPic.DrawImage(bmp, 0, 0);
            return (Image)bmp;
        }
        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path">Path to which the image would be saved.</param> 
        // <param name="quality">An integer from 0 to 100, with 100 being the 
        /// highest quality</param> 
        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");


            // Encoder parameter for image quality 
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // Jpeg image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        private void SaveToMonoChrome(Image image, String FileName)
        {
            Bitmap bmp = BitmapTo1Bpp((Bitmap)image);
            bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            bmp.Dispose();
            bmp = null;
            LaunchCommandLineApp(FileName);
            String st = GetCarNumber(FileName);

            if (st != null)
            {
                txtNumber = st;
            }
            else
            {
                txtNumber = "";
            }

            Application.DoEvents();
        }

        private String GetCarNumber(String FileName)
        {
            StreamReader sr = new StreamReader(FileName + ".txt");
            String stCarNumber = sr.ReadLine();
            sr.Close();
            stCarNumber = stCarNumber.Replace(" ", "");
            stCarNumber = stCarNumber.Replace("-", "");
            stCarNumber = stCarNumber.Replace("—", "");
            stCarNumber = stCarNumber.Replace("_", "");
            stCarNumber = stCarNumber.Replace("~", "");
            stCarNumber = stCarNumber.Replace("\0", "");
            stCarNumber = stCarNumber.Replace("'", "");

            for (int i = 0; i <= '/'; i++)
            {
                Byte[] b = new Byte[1];
                stCarNumber = stCarNumber.Replace(System.Text.Encoding.ASCII.GetString(b), "");
            }

            for (int i = 58; i <= 255; i++)
            {
                Byte[] b = new Byte[1];
                stCarNumber = stCarNumber.Replace(System.Text.Encoding.ASCII.GetString(b), "");
            }
            if (stCarNumber.Length == 7)
            {
                //stCarNumber = stCarNumber.Substring(0, 2) + stCarNumber.Substring(3, 3) + stCarNumber.Substring(7, 2);
                try
                {
                    int CarNumber = Int32.Parse(stCarNumber);
                    if (CarNumber > 1000000)
                    {
                        return stCarNumber;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Launch the legacy application with some options set.
        /// </summary>
        static void LaunchCommandLineApp(String FileName)
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = Application.StartupPath + @"\tesseract.exe";
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "\"" + FileName + "\" \"" + FileName + "\"";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat); //,bmpImage.PixelFormat
            return (Image)(bmpCrop);
        }

        public static Bitmap BitmapTo1Bpp(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format1bppIndexed);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);
            byte[] scan = new byte[(w + 7) / 8];
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (x % 8 == 0) scan[x / 8] = 0;
                    Color c = img.GetPixel(x, y);
                    if (c.GetBrightness() >= 0.5) scan[x / 8] |= (byte)(0x80 >> (x % 8));
                }
                Marshal.Copy(scan, 0, (IntPtr)((long)data.Scan0 + data.Stride * y), scan.Length);
            }
            bmp.UnlockBits(data);
            return bmp;
        }

        private void TofesOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetImage(sender);
            Scan("InOutFrm", true, true);
        }

        private void TofesInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetImage(sender);
            Scan("InFrm", true, true);
        }

        private void toolStripButtonHasava_Click(object sender, EventArgs e)
        {
            SetImage(sender);
            Scan("hasava", true, true);
        }

        public int Branch { get; set; }
    } // class MainFrame

    public class ControlPainter
    {
        private const int
          WM_PRINT = 0x317, PRF_CLIENT = 4,
          PRF_CHILDREN = 0x10, PRF_NON_CLIENT = 2,
          COMBINED_PRINTFLAGS = PRF_CLIENT | PRF_CHILDREN | PRF_NON_CLIENT;

        [DllImport("USER32.DLL")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, int lParam);

        public static void PaintControl(Graphics graphics, Control control)
        { // paint control onto graphics
            IntPtr hWnd = control.Handle;
            IntPtr hDC = graphics.GetHdc();
            SendMessage(hWnd, WM_PRINT, hDC, COMBINED_PRINTFLAGS);
            graphics.ReleaseHdc(hDC);
        }
    }

} // namespace TwainGui
