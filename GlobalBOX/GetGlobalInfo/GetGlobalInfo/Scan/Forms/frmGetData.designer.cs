namespace Pulsar
{
    partial class frmGetData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        ///// <summary>
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.cmbSnif = new System.Windows.Forms.ComboBox();
            this.lblSnif = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.picScanType = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picScanType)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(124, 38);
            this.txtNumber.MaxLength = 25;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(109, 20);
            this.txtNumber.TabIndex = 0;
            this.txtNumber.TextChanged += new System.EventHandler(this.txtNumber_TextChanged);
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // cmbSnif
            // 
            this.cmbSnif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSnif.FormattingEnabled = true;
            this.cmbSnif.Location = new System.Drawing.Point(124, 11);
            this.cmbSnif.Name = "cmbSnif";
            this.cmbSnif.Size = new System.Drawing.Size(60, 21);
            this.cmbSnif.TabIndex = 1;
            // 
            // lblSnif
            // 
            this.lblSnif.AutoSize = true;
            this.lblSnif.Location = new System.Drawing.Point(73, 14);
            this.lblSnif.Name = "lblSnif";
            this.lblSnif.Size = new System.Drawing.Size(44, 13);
            this.lblSnif.TabIndex = 2;
            this.lblSnif.Text = "Branch:";
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(73, 41);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(47, 13);
            this.lblNumber.TabIndex = 2;
            this.lblNumber.Text = "Number:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(209, 80);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(124, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(239, 38);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(24, 20);
            this.btnShow.TabIndex = 4;
            this.btnShow.Text = "...";
            this.btnShow.UseVisualStyleBackColor = true;
            // 
            // picScanType
            // 
            this.picScanType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picScanType.Location = new System.Drawing.Point(12, 12);
            this.picScanType.Name = "picScanType";
            this.picScanType.Padding = new System.Windows.Forms.Padding(1);
            this.picScanType.Size = new System.Drawing.Size(50, 50);
            this.picScanType.TabIndex = 5;
            this.picScanType.TabStop = false;
            this.picScanType.DoubleClick += new System.EventHandler(this.picScanType_DoubleClick);
            // 
            // frmGetData
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(299, 115);
            this.ControlBox = false;
            this.Controls.Add(this.picScanType);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblSnif);
            this.Controls.Add(this.cmbSnif);
            this.Controls.Add(this.txtNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGetData";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert Details";
            this.Load += new System.EventHandler(this.frmGetData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picScanType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.ComboBox cmbSnif;
        private System.Windows.Forms.Label lblSnif;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.PictureBox picScanType;
    }
}