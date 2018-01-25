using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;

namespace Pulsar.Forms
{
    public partial class frmDownload : Form
    {
        Stopwatch stopwatch = new Stopwatch();

        public frmDownload()
        {
            InitializeComponent();
        }

        private void frmDownload_Load(object sender, EventArgs e)
        {
            this.Show();
            Application.DoEvents();

            //http://www.simplicitools.com/SimpliciTools/FileDownload2.ashx?filename=Pulsar.msi&ProductID=2
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri("http://www.simplicitools.com/SimpliciTools/FileDownload2.ashx?filename=Pulsar.msi&ProductID=2"), System.Windows.Forms.Application.StartupPath + @"\Pulsar.msi");
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblElpesedTime.Text = String.Format("{0:00}", stopwatch.Elapsed.Hours) + ":" + String.Format("{0:00}", stopwatch.Elapsed.Minutes) + ":" + String.Format("{0:00}", stopwatch.Elapsed.Seconds);
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Stop timing
            this.Close();
        }
    }
}
