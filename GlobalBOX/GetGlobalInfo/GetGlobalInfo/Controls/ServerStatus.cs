using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Pulsar.Controls
{
    public partial class ServerStatus : UserControl
    {
        public bool ServerOnline { get; set; }
        public Parser parser { get; set; }

        public ServerStatus()
        {
            InitializeComponent();
            this.BackgroundImage = global::Pulsar.Properties.Resources.offline;
            BitmapRegion.CreateControlRegion(this, global::Pulsar.Properties.Resources.offline);
        }

        private void ServerStatus_Load(object sender, EventArgs e)
        {
            CheckServerOnline();
        }

        private void CheckServerOnline()
        {
            if (parser != null)
            {
                ServerOnline = parser.Ping();
                this.BackgroundImage = ServerOnline ? global::Pulsar.Properties.Resources.online : global::Pulsar.Properties.Resources.offline;
                tmrServerOnline.Enabled = !ServerOnline;
            }
        }

        private void ServerStatus_Resize(object sender, EventArgs e)
        {
            Width = 12;
            Height = 12;
        }

        private void tmrServerOnline_Tick(object sender, EventArgs e)
        {
            CheckServerOnline();
        }
    }
}

