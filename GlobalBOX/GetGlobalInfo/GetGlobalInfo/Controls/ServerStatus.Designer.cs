namespace Pulsar.Controls
{
    partial class ServerStatus
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrServerOnline = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrServerOnline
            // 
            this.tmrServerOnline.Interval = 500;
            this.tmrServerOnline.Tick += new System.EventHandler(this.tmrServerOnline_Tick);
            // 
            // ServerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::Pulsar.Properties.Resources.offline;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Name = "ServerStatus";
            this.Size = new System.Drawing.Size(12, 12);
            this.Load += new System.EventHandler(this.ServerStatus_Load);
            this.Resize += new System.EventHandler(this.ServerStatus_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrServerOnline;
    }
}
