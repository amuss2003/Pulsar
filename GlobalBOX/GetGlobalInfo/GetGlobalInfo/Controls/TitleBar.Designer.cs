namespace Pulsar
{
    partial class TitleBar
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblCompanyVAT = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(85, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Company Name:";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyName.ForeColor = System.Drawing.Color.Black;
            this.lblCompanyName.Location = new System.Drawing.Point(94, 3);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(10, 13);
            this.lblCompanyName.TabIndex = 1;
            this.lblCompanyName.Text = ".";
            // 
            // lblCompanyVAT
            // 
            this.lblCompanyVAT.AutoSize = true;
            this.lblCompanyVAT.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyVAT.ForeColor = System.Drawing.Color.Black;
            this.lblCompanyVAT.Location = new System.Drawing.Point(308, 3);
            this.lblCompanyVAT.Name = "lblCompanyVAT";
            this.lblCompanyVAT.Size = new System.Drawing.Size(10, 13);
            this.lblCompanyVAT.TabIndex = 3;
            this.lblCompanyVAT.Text = ".";
            // 
            // lblVAT
            // 
            this.lblVAT.AutoSize = true;
            this.lblVAT.BackColor = System.Drawing.Color.Transparent;
            this.lblVAT.ForeColor = System.Drawing.Color.Black;
            this.lblVAT.Location = new System.Drawing.Point(271, 3);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(31, 13);
            this.lblVAT.TabIndex = 2;
            this.lblVAT.Text = "VAT:";
            // 
            // TitleBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCompanyVAT);
            this.Controls.Add(this.lblVAT);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.lblName);
            this.Name = "TitleBar";
            this.Size = new System.Drawing.Size(529, 20);
            this.Load += new System.EventHandler(this.TitleBar_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TitleBar_Paint);
            this.Resize += new System.EventHandler(this.TitleBar_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblCompanyVAT;
        private System.Windows.Forms.Label lblVAT;
    }
}
