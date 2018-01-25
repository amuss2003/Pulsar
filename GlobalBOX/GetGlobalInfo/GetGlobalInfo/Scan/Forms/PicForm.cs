using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using GdiPlusLib;

namespace Pulsar
{

    public class PicForm : System.Windows.Forms.Form
    {
        private IContainer components;
        private System.Windows.Forms.MenuItem menuItemClose;
        private System.Windows.Forms.MenuItem menuItemInfo;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemSaveAs;
        private System.Windows.Forms.MainMenu picformMenu;
        private System.Windows.Forms.MenuItem menuItemSepPic;
        private MenuItem menuItemSave;
        private string m_ScannerPath = "";
        private bool m_bAutoScanNext = false;

        public void SetLanguage(int LanguageID)
        {
            if (LanguageID == 0)
            {
                SetEnglish();
            }
            if (LanguageID == 1)
            {
                SetHebrew();
            }
        }

        private void SetHebrew()
        {
            menuItemFile.Text = "&קובץ";
            menuItemInfo.Text = "&מידע...";
            menuItemSaveAs.Text = "&שמירה בשם";
            menuItemClose.Text = "&סגור";
            menuItemSave.Text = "&שמירה";
        }

        private void SetEnglish()
        {
            menuItemFile.Text = "&File";
            menuItemInfo.Text = "&Info...";
            menuItemSaveAs.Text = "&Save As...";
            menuItemClose.Text = "&Close";
        }

        public PicForm(IntPtr dibhandp)
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            bmprect = new Rectangle(0, 0, 0, 0);
            dibhand = dibhandp;
            bmpptr = GlobalLock(dibhand);
            pixptr = GetPixelInfo(bmpptr);

            this.AutoScrollMinSize = new System.Drawing.Size(bmprect.Width, bmprect.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dibhand != IntPtr.Zero)
                {
                    GlobalFree(dibhand);
                    dibhand = IntPtr.Zero;
                }

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
            this.menuItemSepPic = new System.Windows.Forms.MenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItemInfo = new System.Windows.Forms.MenuItem();
            this.menuItemClose = new System.Windows.Forms.MenuItem();
            this.picformMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemSave = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // menuItemSepPic
            // 
            this.menuItemSepPic.Index = 3;
            this.menuItemSepPic.MergeOrder = 4;
            this.menuItemSepPic.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemSepPic.Text = "-";
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Index = 1;
            this.menuItemSaveAs.MergeOrder = 2;
            this.menuItemSaveAs.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemSaveAs.Text = "&Save As...";
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // menuItemInfo
            // 
            this.menuItemInfo.Index = 0;
            this.menuItemInfo.MergeOrder = 1;
            this.menuItemInfo.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemInfo.Text = "&Info...";
            this.menuItemInfo.Click += new System.EventHandler(this.menuItemInfo_Click);
            // 
            // menuItemClose
            // 
            this.menuItemClose.Index = 2;
            this.menuItemClose.MergeOrder = 3;
            this.menuItemClose.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemClose.Text = "&Close";
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // picformMenu
            // 
            this.picformMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemSave});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemInfo,
            this.menuItemSaveAs,
            this.menuItemClose,
            this.menuItemSepPic});
            this.menuItemFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemFile.Text = "&File";
            // 
            // menuItemSave
            // 
            this.menuItemSave.Index = 1;
            this.menuItemSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // PicForm
            // 
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(256, 256);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(577, 414);
            this.Menu = this.picformMenu;
            this.MinimumSize = new System.Drawing.Size(80, 80);
            this.Name = "PicForm";
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.Text = "PicForm";
            this.Load += new System.EventHandler(this.PicForm_Load);
            this.Enter += new System.EventHandler(this.PicForm_Enter);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PicForm_FormClosing);
            this.ResumeLayout(false);

        }
        #endregion

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle cltrect = ClientRectangle;
            Rectangle clprect = e.ClipRectangle;
            Point scrol = AutoScrollPosition;

            Rectangle realrect = clprect;
            realrect.X -= scrol.X;
            realrect.Y -= scrol.Y;

            SolidBrush brbg = new SolidBrush(Color.Black);
            if (realrect.Right > bmprect.Width)
            {
                Rectangle bgri = clprect;
                int ovri = bmprect.Width - realrect.X;
                if (ovri > 0)
                {
                    bgri.X += ovri;
                    bgri.Width -= ovri;
                }
                e.Graphics.FillRectangle(brbg, bgri);
            }

            if (realrect.Bottom > bmprect.Height)
            {
                Rectangle bgbo = clprect;
                int ovbo = bmprect.Height - realrect.Y;
                if (ovbo > 0)
                {
                    bgbo.Y += ovbo;
                    bgbo.Height -= ovbo;
                }
                e.Graphics.FillRectangle(brbg, bgbo);
            }

            realrect.Intersect(bmprect);
            if (!realrect.IsEmpty)
            {
                int bot = bmprect.Height - realrect.Bottom;
                IntPtr hdc = e.Graphics.GetHdc();
                SetDIBitsToDevice(hdc, clprect.X, clprect.Y, realrect.Width, realrect.Height,
                        realrect.X, bot, 0, bmprect.Height, pixptr, bmpptr, 0);
                e.Graphics.ReleaseHdc(hdc);
            }
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Menu.MenuItems.Clear();
            base.OnClosing(e);
        }



        private void menuItemClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void menuItemInfo_Click(object sender, System.EventArgs e)
        {
            InfoForm iform = new InfoForm(bmi);
            iform.ShowDialog(this);
        }

        private void menuItemSaveAs_Click(object sender, System.EventArgs e)
        {
            //Gdip.SaveDIBAs( this.Text, bmpptr, pixptr );
            //Gdip.AutoSaveDIBAs(m_ScannerPath, this.Text, bmpptr, pixptr);
            Save();
        }

        public void Save()
        {
            Gdip.AutoSaveDIBAs(m_ScannerPath, this.Text, bmpptr, pixptr);
            menuItemSave.Visible = false;
            if (m_bAutoScanNext)
            {
                this.Close();
            }
        }

        public void SaveTemp(String pFileName)
        {
            Gdip.SaveDIBAsTemp(pFileName ,bmpptr, pixptr);
        }

        public void AutoScanNext()
        {
            m_bAutoScanNext = true;
        }

        protected IntPtr GetPixelInfo(IntPtr bmpptr)
        {
            bmi = new BITMAPINFOHEADER();
            Marshal.PtrToStructure(bmpptr, bmi);

            bmprect.X = bmprect.Y = 0;
            bmprect.Width = bmi.biWidth;
            bmprect.Height = bmi.biHeight;

            if (bmi.biSizeImage == 0)
                bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

            int p = bmi.biClrUsed;
            if ((p == 0) && (bmi.biBitCount <= 8))
                p = 1 << bmi.biBitCount;
            p = (p * 4) + bmi.biSize + (int)bmpptr;
            return (IntPtr)p;
        }

        BITMAPINFOHEADER bmi;
        Rectangle bmprect;
        IntPtr dibhand;
        IntPtr bmpptr;
        IntPtr pixptr;

        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst,
                                                int width, int height, int xsrc, int ysrc, int start, int lines,
                                                IntPtr bitsptr, IntPtr bmiptr, int color);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string outstr);

        private void PicForm_Load(object sender, EventArgs e)
        {

        }

        internal void SetPath(string ScannerPath)
        {
            m_ScannerPath = ScannerPath;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void PicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (menuItemSave.Visible)
            {
                if (MessageBox.Show("äàí áøöåðê ìשמור ìôðé ñâéøä?", "ùîéøä", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                }
            }
        }

        private void PicForm_Enter(object sender, EventArgs e)
        {
            //((MainFrame)this.Parent).SetActiveScanForm((PicForm)this);
        }
    } // class PicForm


    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }

} // namespace TwainGui
