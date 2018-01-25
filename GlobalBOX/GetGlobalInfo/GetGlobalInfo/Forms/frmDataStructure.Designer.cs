namespace Pulsar
{
    partial class frmDataStructure
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataStructure));
            this.btnExitImport = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtDataSample = new System.Windows.Forms.TextBox();
            this.lblFiledSample = new System.Windows.Forms.Label();
            this.filedHolder1 = new Pulsar.Controls.FiledHolder();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExitImport
            // 
            this.btnExitImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExitImport.BackgroundImage = global::Pulsar.Properties.Resources.exit;
            this.btnExitImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExitImport.Location = new System.Drawing.Point(199, 416);
            this.btnExitImport.Name = "btnExitImport";
            this.btnExitImport.Size = new System.Drawing.Size(48, 39);
            this.btnExitImport.TabIndex = 2;
            this.btnExitImport.UseVisualStyleBackColor = true;
            this.btnExitImport.Click += new System.EventHandler(this.btnExitImport_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnUpdate.Image = global::Pulsar.Properties.Resources.edit_icon;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(97, 416);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(96, 39);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtDataSample
            // 
            this.txtDataSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDataSample.Location = new System.Drawing.Point(12, 390);
            this.txtDataSample.Name = "txtDataSample";
            this.txtDataSample.Size = new System.Drawing.Size(231, 20);
            this.txtDataSample.TabIndex = 3;
            this.txtDataSample.Text = resources.GetString("txtDataSample.Text");
            // 
            // lblFiledSample
            // 
            this.lblFiledSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFiledSample.Location = new System.Drawing.Point(12, 374);
            this.lblFiledSample.Name = "lblFiledSample";
            this.lblFiledSample.Size = new System.Drawing.Size(231, 13);
            this.lblFiledSample.TabIndex = 4;
            this.lblFiledSample.Text = "...";
            // 
            // filedHolder1
            // 
            this.filedHolder1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.filedHolder1.Company_Info = null;
            this.filedHolder1.CurrentDataLine = null;
            this.filedHolder1.IniFileName = "devenv.ini";
            this.filedHolder1.Location = new System.Drawing.Point(15, 35);
            this.filedHolder1.Name = "filedHolder1";
            this.filedHolder1.SelectedFiledStructure = null;
            this.filedHolder1.Size = new System.Drawing.Size(228, 336);
            this.filedHolder1.TabIndex = 0;
            this.filedHolder1.Changed += new System.EventHandler(this.filedHolder1_Changed);
            this.filedHolder1.Load += new System.EventHandler(this.filedHolder1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Pos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Length";
            // 
            // frmDataStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 467);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFiledSample);
            this.Controls.Add(this.txtDataSample);
            this.Controls.Add(this.filedHolder1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnExitImport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDataStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Structure";
            this.Load += new System.EventHandler(this.frmDataStructure_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExitImport;
        private System.Windows.Forms.Button btnUpdate;
        private Controls.FiledHolder filedHolder1;
        private System.Windows.Forms.TextBox txtDataSample;
        private System.Windows.Forms.Label lblFiledSample;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}