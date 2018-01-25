using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using SendFileTo;
using System.IO;

namespace Pulsar
{
    public partial class frmZoom : Form
    {
        private int m_MX;
        private int m_MY;
        private System.Windows.Forms.Button cmdPrint;
        private string m_ImageFilePath = "";

        public frmZoom()
        {
            InitializeComponent();
        }

        private void frmZoom_Load(object sender, EventArgs e)
        {
            //PrintersList();
        }

        public void SetImageFilePath(string ImageFilePath)
        {
            m_ImageFilePath = ImageFilePath;
        }

        public void SetZoomImage(PictureBox pic)
        {
            picZoom.Image = pic.Image;
            //picZoom = pic;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.Doc_PrintPage;

            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;

            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Font font = new Font("Arial", 30);

            //float x = e.MarginBounds.Left;
            //float y = e.MarginBounds.Top;

            //float lineHeight = font.GetHeight(e.Graphics);

            //for (int i = 0; i < 5; i++)
            //{
            //    e.Graphics.DrawString("This is line " + i.ToString(), font, Brushes.Black, x, y);
            //    y += lineHeight;
            //}
            //y += lineHeight;
            //Image.FromFile("c:\\YourFile.bmp")
            e.Graphics.DrawImage(picZoom.Image, 0, 0, 763, 1169);

        }

        private void PrintersList()
        {
              foreach (string printerName in PrinterSettings.InstalledPrinters)
              {
                // Display the printer name.
                Console.WriteLine("Printer: {0}", printerName);
         
                // Retrieve the printer settings.
                PrinterSettings printer = new PrinterSettings();
                printer.PrinterName = printerName;
         
                // Check that this is a valid printer.
                // (This step might be required if you read the printer name
                // from a user-supplied value or a registry or configuration file
                // setting.)
                if (printer.IsValid)
                {
                  // Display the list of valid resolutions.
                  Console.WriteLine("Supported Resolutions:");
         
                  foreach (PrinterResolution resolution in
                    printer.PrinterResolutions)
                  {
                    Console.WriteLine("  {0}", resolution);
                  }
                  Console.WriteLine();
         
                  // Display the list of valid paper sizes.
                  Console.WriteLine("Supported Paper Sizes:");
         
                  foreach (PaperSize size in printer.PaperSizes)
                  {
                    if (Enum.IsDefined(size.Kind.GetType(), size.Kind))
                    {
                      Console.WriteLine("  {0}", size);
                    }
                  }
                  Console.WriteLine();
                }
              }
              Console.ReadLine();
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEMail frm = new frmEMail();
            frm.ShowDialog();
            string stEMail = frm.GetEMail();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            if(File.Exists("c:\\send.jpg"))
            {
                File.Delete("c:\\send.jpg");
            }

            File.Copy(m_ImageFilePath, "c:\\send.jpg");

            MAPI mapi = new MAPI();
            
//            mapi.AddAttachment(m_ImageFilePath);
            mapi.AddAttachment("c:\\send.jpg");
            mapi.AddRecipientTo(stEMail);            
            mapi.SendMailPopup("My Subject", "body text");
        }
    }
}