namespace Pulsar
{
    partial class frmCheckInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckInfo));
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblSnif = new System.Windows.Forms.Label();
            this.cmbSnif = new System.Windows.Forms.ComboBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblContract = new System.Windows.Forms.Label();
            this.lblInOutForm = new System.Windows.Forms.Label();
            this.lblIssue = new System.Windows.Forms.Label();
            this.lblShovar = new System.Windows.Forms.Label();
            this.lblOrder = new System.Windows.Forms.Label();
            this.lblDamage = new System.Windows.Forms.Label();
            this.picDamage = new System.Windows.Forms.PictureBox();
            this.picOrder = new System.Windows.Forms.PictureBox();
            this.picInOutForm = new System.Windows.Forms.PictureBox();
            this.picIssue = new System.Windows.Forms.PictureBox();
            this.picShovar = new System.Windows.Forms.PictureBox();
            this.picContract = new System.Windows.Forms.PictureBox();
            this.picScannerNormal = new System.Windows.Forms.PictureBox();
            this.picScannerAccept = new System.Windows.Forms.PictureBox();
            this.picScannerNone = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearchAll = new System.Windows.Forms.Button();
            this.picInForm = new System.Windows.Forms.PictureBox();
            this.chkInForm = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInOutForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIssue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShovar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScannerNormal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScannerAccept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScannerNone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInForm)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(147, 9);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(47, 13);
            this.lblNumber.TabIndex = 14;
            this.lblNumber.Text = "Number:";
            // 
            // lblSnif
            // 
            this.lblSnif.AutoSize = true;
            this.lblSnif.Location = new System.Drawing.Point(12, 9);
            this.lblSnif.Name = "lblSnif";
            this.lblSnif.Size = new System.Drawing.Size(44, 13);
            this.lblSnif.TabIndex = 15;
            this.lblSnif.Text = "Branch:";
            // 
            // cmbSnif
            // 
            this.cmbSnif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSnif.FormattingEnabled = true;
            this.cmbSnif.Location = new System.Drawing.Point(71, 6);
            this.cmbSnif.Name = "cmbSnif";
            this.cmbSnif.Size = new System.Drawing.Size(60, 21);
            this.cmbSnif.TabIndex = 13;
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(198, 6);
            this.txtNumber.MaxLength = 25;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(109, 20);
            this.txtNumber.TabIndex = 12;
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(316, 4);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(70, 22);
            this.btnCheck.TabIndex = 16;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblContract
            // 
            this.lblContract.AutoSize = true;
            this.lblContract.Location = new System.Drawing.Point(12, 32);
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(31, 13);
            this.lblContract.TabIndex = 17;
            this.lblContract.Text = "חוזה";
            // 
            // lblInOutForm
            // 
            this.lblInOutForm.AutoSize = true;
            this.lblInOutForm.Location = new System.Drawing.Point(258, 32);
            this.lblInOutForm.Name = "lblInOutForm";
            this.lblInOutForm.Size = new System.Drawing.Size(68, 13);
            this.lblInOutForm.TabIndex = 17;
            this.lblInOutForm.Text = "טופס יציאה";
            // 
            // lblIssue
            // 
            this.lblIssue.AutoSize = true;
            this.lblIssue.Location = new System.Drawing.Point(519, 32);
            this.lblIssue.Name = "lblIssue";
            this.lblIssue.Size = new System.Drawing.Size(73, 13);
            this.lblIssue.TabIndex = 17;
            this.lblIssue.Text = "טופס הרשאה";
            // 
            // lblShovar
            // 
            this.lblShovar.AutoSize = true;
            this.lblShovar.Location = new System.Drawing.Point(12, 299);
            this.lblShovar.Name = "lblShovar";
            this.lblShovar.Size = new System.Drawing.Size(34, 13);
            this.lblShovar.TabIndex = 17;
            this.lblShovar.Text = "שובר";
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.Location = new System.Drawing.Point(258, 299);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(38, 13);
            this.lblOrder.TabIndex = 17;
            this.lblOrder.Text = "הזמנה";
            // 
            // lblDamage
            // 
            this.lblDamage.AutoSize = true;
            this.lblDamage.Location = new System.Drawing.Point(519, 299);
            this.lblDamage.Name = "lblDamage";
            this.lblDamage.Size = new System.Drawing.Size(24, 13);
            this.lblDamage.TabIndex = 17;
            this.lblDamage.Text = "נזק";
            // 
            // picDamage
            // 
            this.picDamage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picDamage.Image = ((System.Drawing.Image)(resources.GetObject("picDamage.Image")));
            this.picDamage.Location = new System.Drawing.Point(513, 317);
            this.picDamage.Name = "picDamage";
            this.picDamage.Size = new System.Drawing.Size(240, 240);
            this.picDamage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDamage.TabIndex = 11;
            this.picDamage.TabStop = false;
            this.picDamage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContract_MouseDoubleClick);
            // 
            // picOrder
            // 
            this.picOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picOrder.Image = ((System.Drawing.Image)(resources.GetObject("picOrder.Image")));
            this.picOrder.Location = new System.Drawing.Point(261, 317);
            this.picOrder.Name = "picOrder";
            this.picOrder.Size = new System.Drawing.Size(240, 240);
            this.picOrder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOrder.TabIndex = 10;
            this.picOrder.TabStop = false;
            this.picOrder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContract_MouseDoubleClick);
            // 
            // picInOutForm
            // 
            this.picInOutForm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picInOutForm.Image = ((System.Drawing.Image)(resources.GetObject("picInOutForm.Image")));
            this.picInOutForm.Location = new System.Drawing.Point(261, 50);
            this.picInOutForm.Name = "picInOutForm";
            this.picInOutForm.Size = new System.Drawing.Size(240, 240);
            this.picInOutForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInOutForm.TabIndex = 9;
            this.picInOutForm.TabStop = false;
            this.picInOutForm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContract_MouseDoubleClick);
            // 
            // picIssue
            // 
            this.picIssue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picIssue.Image = ((System.Drawing.Image)(resources.GetObject("picIssue.Image")));
            this.picIssue.Location = new System.Drawing.Point(513, 50);
            this.picIssue.Name = "picIssue";
            this.picIssue.Size = new System.Drawing.Size(240, 240);
            this.picIssue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIssue.TabIndex = 8;
            this.picIssue.TabStop = false;
            this.picIssue.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContract_MouseDoubleClick);
            // 
            // picShovar
            // 
            this.picShovar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picShovar.Image = ((System.Drawing.Image)(resources.GetObject("picShovar.Image")));
            this.picShovar.Location = new System.Drawing.Point(14, 317);
            this.picShovar.Name = "picShovar";
            this.picShovar.Size = new System.Drawing.Size(240, 240);
            this.picShovar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picShovar.TabIndex = 7;
            this.picShovar.TabStop = false;
            this.picShovar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContract_MouseDoubleClick);
            // 
            // picContract
            // 
            this.picContract.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picContract.Image = ((System.Drawing.Image)(resources.GetObject("picContract.Image")));
            this.picContract.Location = new System.Drawing.Point(14, 50);
            this.picContract.Name = "picContract";
            this.picContract.Size = new System.Drawing.Size(240, 240);
            this.picContract.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picContract.TabIndex = 6;
            this.picContract.TabStop = false;
            this.picContract.Click += new System.EventHandler(this.picContract_Click);
            this.picContract.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContract_MouseDoubleClick);
            // 
            // picScannerNormal
            // 
            this.picScannerNormal.Image = global::Pulsar.Properties.Resources.scannerNormal;
            this.picScannerNormal.Location = new System.Drawing.Point(630, 5);
            this.picScannerNormal.Name = "picScannerNormal";
            this.picScannerNormal.Size = new System.Drawing.Size(24, 24);
            this.picScannerNormal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picScannerNormal.TabIndex = 18;
            this.picScannerNormal.TabStop = false;
            this.picScannerNormal.Visible = false;
            // 
            // picScannerAccept
            // 
            this.picScannerAccept.Image = global::Pulsar.Properties.Resources.scanner_accept;
            this.picScannerAccept.Location = new System.Drawing.Point(660, 5);
            this.picScannerAccept.Name = "picScannerAccept";
            this.picScannerAccept.Size = new System.Drawing.Size(24, 24);
            this.picScannerAccept.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picScannerAccept.TabIndex = 19;
            this.picScannerAccept.TabStop = false;
            this.picScannerAccept.Visible = false;
            // 
            // picScannerNone
            // 
            this.picScannerNone.Image = global::Pulsar.Properties.Resources.scanner_remove;
            this.picScannerNone.Location = new System.Drawing.Point(690, 5);
            this.picScannerNone.Name = "picScannerNone";
            this.picScannerNone.Size = new System.Drawing.Size(24, 24);
            this.picScannerNone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picScannerNone.TabIndex = 20;
            this.picScannerNone.TabStop = false;
            this.picScannerNone.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(778, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 21);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSearchAll
            // 
            this.btnSearchAll.Enabled = false;
            this.btnSearchAll.Location = new System.Drawing.Point(453, 4);
            this.btnSearchAll.Name = "btnSearchAll";
            this.btnSearchAll.Size = new System.Drawing.Size(139, 23);
            this.btnSearchAll.TabIndex = 22;
            this.btnSearchAll.Text = "חפש בסניף אחר";
            this.btnSearchAll.UseVisualStyleBackColor = true;
            this.btnSearchAll.Click += new System.EventHandler(this.btnSearchAll_Click);
            // 
            // picInForm
            // 
            this.picInForm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picInForm.Image = ((System.Drawing.Image)(resources.GetObject("picInForm.Image")));
            this.picInForm.Location = new System.Drawing.Point(261, 50);
            this.picInForm.Name = "picInForm";
            this.picInForm.Size = new System.Drawing.Size(240, 240);
            this.picInForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInForm.TabIndex = 23;
            this.picInForm.TabStop = false;
            this.picInForm.Visible = false;
            this.picInForm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContract_MouseDoubleClick);
            // 
            // chkInForm
            // 
            this.chkInForm.Location = new System.Drawing.Point(450, 31);
            this.chkInForm.Name = "chkInForm";
            this.chkInForm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkInForm.Size = new System.Drawing.Size(51, 15);
            this.chkInForm.TabIndex = 24;
            this.chkInForm.Text = "חזרה";
            this.chkInForm.UseVisualStyleBackColor = true;
            this.chkInForm.CheckedChanged += new System.EventHandler(this.chkInForm_CheckedChanged);
            // 
            // frmCheckInfo
            // 
            this.AcceptButton = this.btnCheck;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(766, 572);
            this.Controls.Add(this.chkInForm);
            this.Controls.Add(this.btnSearchAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.picScannerNone);
            this.Controls.Add(this.picScannerAccept);
            this.Controls.Add(this.picScannerNormal);
            this.Controls.Add(this.lblDamage);
            this.Controls.Add(this.lblIssue);
            this.Controls.Add(this.lblOrder);
            this.Controls.Add(this.lblInOutForm);
            this.Controls.Add(this.lblShovar);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblSnif);
            this.Controls.Add(this.cmbSnif);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.picDamage);
            this.Controls.Add(this.picOrder);
            this.Controls.Add(this.picInOutForm);
            this.Controls.Add(this.picIssue);
            this.Controls.Add(this.picShovar);
            this.Controls.Add(this.picContract);
            this.Controls.Add(this.picInForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "איתור לפי חוזה";
            this.Load += new System.EventHandler(this.frmCheckInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInOutForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIssue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShovar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScannerNormal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScannerAccept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScannerNone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picContract;
        private System.Windows.Forms.PictureBox picShovar;
        private System.Windows.Forms.PictureBox picInOutForm;
        private System.Windows.Forms.PictureBox picIssue;
        private System.Windows.Forms.PictureBox picDamage;
        private System.Windows.Forms.PictureBox picOrder;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblSnif;
        private System.Windows.Forms.ComboBox cmbSnif;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.Label lblInOutForm;
        private System.Windows.Forms.Label lblIssue;
        private System.Windows.Forms.Label lblShovar;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.Label lblDamage;
        private System.Windows.Forms.PictureBox picScannerNormal;
        private System.Windows.Forms.PictureBox picScannerAccept;
        private System.Windows.Forms.PictureBox picScannerNone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSearchAll;
        private System.Windows.Forms.PictureBox picInForm;
        private System.Windows.Forms.CheckBox chkInForm;
    }
}