namespace Pulsar
{
    partial class frmPayment
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtCVV = new System.Windows.Forms.TextBox();
            this.btnNewBusiness = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.picName = new System.Windows.Forms.PictureBox();
            this.picCCValidation = new System.Windows.Forms.PictureBox();
            this.cmbCCType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkIAgree = new System.Windows.Forms.CheckBox();
            this.lnkRead = new System.Windows.Forms.LinkLabel();
            this.titleBar1 = new Pulsar.TitleBar();
            this.txtCC = new Pulsar.NumericTextBox();
            this.txtExpired = new Pulsar.NumericTextBox();
            this.lblDocPathTip = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCCValidation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "CC:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(56, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(56, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "CVV:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(100, 27);
            this.txtName.MaxLength = 32;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(224, 20);
            this.txtName.TabIndex = 0;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(100, 75);
            this.txtID.MaxLength = 9;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(103, 20);
            this.txtID.TabIndex = 1;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // txtCVV
            // 
            this.txtCVV.Location = new System.Drawing.Point(100, 219);
            this.txtCVV.MaxLength = 4;
            this.txtCVV.Name = "txtCVV";
            this.txtCVV.Size = new System.Drawing.Size(31, 20);
            this.txtCVV.TabIndex = 5;
            // 
            // btnNewBusiness
            // 
            this.btnNewBusiness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewBusiness.Enabled = false;
            this.btnNewBusiness.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNewBusiness.Image = global::Pulsar.Properties.Resources.credit_card_24;
            this.btnNewBusiness.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNewBusiness.Location = new System.Drawing.Point(236, 265);
            this.btnNewBusiness.Name = "btnNewBusiness";
            this.btnNewBusiness.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnNewBusiness.Size = new System.Drawing.Size(119, 47);
            this.btnNewBusiness.TabIndex = 8;
            this.btnNewBusiness.Text = "Payment";
            this.btnNewBusiness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewBusiness.UseVisualStyleBackColor = true;
            this.btnNewBusiness.Click += new System.EventHandler(this.btnNewBusiness_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImage = global::Pulsar.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(361, 265);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(45, 47);
            this.btnExit.TabIndex = 9;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Pulsar.Properties.Resources.anti_teft_cc;
            this.pictureBox1.Location = new System.Drawing.Point(170, 180);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Anti Crefit Card Teft";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Pulsar.Properties.Resources.cvv;
            this.pictureBox2.Location = new System.Drawing.Point(12, 206);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 29);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Pulsar.Properties.Resources.Credit_card;
            this.pictureBox3.Location = new System.Drawing.Point(12, 111);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 23;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Pulsar.Properties.Resources.ID;
            this.pictureBox4.Location = new System.Drawing.Point(12, 63);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(32, 32);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 24;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Pulsar.Properties.Resources.calender;
            this.pictureBox5.Location = new System.Drawing.Point(12, 159);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(32, 32);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 25;
            this.pictureBox5.TabStop = false;
            // 
            // picName
            // 
            this.picName.Image = global::Pulsar.Properties.Resources._24_tag_pencil;
            this.picName.Location = new System.Drawing.Point(12, 27);
            this.picName.Name = "picName";
            this.picName.Size = new System.Drawing.Size(24, 24);
            this.picName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picName.TabIndex = 26;
            this.picName.TabStop = false;
            // 
            // picCCValidation
            // 
            this.picCCValidation.Image = global::Pulsar.Properties.Resources.inactive;
            this.picCCValidation.Location = new System.Drawing.Point(209, 124);
            this.picCCValidation.Name = "picCCValidation";
            this.picCCValidation.Size = new System.Drawing.Size(16, 16);
            this.picCCValidation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picCCValidation.TabIndex = 27;
            this.picCCValidation.TabStop = false;
            this.picCCValidation.DoubleClick += new System.EventHandler(this.picCCValidation_DoubleClick);
            // 
            // cmbCCType
            // 
            this.cmbCCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCCType.FormattingEnabled = true;
            this.cmbCCType.Location = new System.Drawing.Point(247, 122);
            this.cmbCCType.Name = "cmbCCType";
            this.cmbCCType.Size = new System.Drawing.Size(121, 21);
            this.cmbCCType.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(247, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 16);
            this.label7.TabIndex = 78;
            this.label7.Text = "CC Company:";
            // 
            // chkIAgree
            // 
            this.chkIAgree.AutoSize = true;
            this.chkIAgree.Enabled = false;
            this.chkIAgree.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIAgree.Location = new System.Drawing.Point(14, 295);
            this.chkIAgree.Name = "chkIAgree";
            this.chkIAgree.Size = new System.Drawing.Size(68, 20);
            this.chkIAgree.TabIndex = 7;
            this.chkIAgree.Text = "I Agree";
            this.chkIAgree.UseVisualStyleBackColor = true;
            this.chkIAgree.CheckedChanged += new System.EventHandler(this.chkIAgree_CheckedChanged);
            // 
            // lnkRead
            // 
            this.lnkRead.AutoSize = true;
            this.lnkRead.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkRead.Location = new System.Drawing.Point(9, 279);
            this.lnkRead.Name = "lnkRead";
            this.lnkRead.Size = new System.Drawing.Size(154, 16);
            this.lnkRead.TabIndex = 6;
            this.lnkRead.TabStop = true;
            this.lnkRead.Text = "Read Licence Agreement";
            this.lnkRead.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRead_LinkClicked);
            // 
            // titleBar1
            // 
            this.titleBar1.company_info = null;
            this.titleBar1.Company_Info = null;
            this.titleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar1.Location = new System.Drawing.Point(0, 0);
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.Size = new System.Drawing.Size(416, 21);
            this.titleBar1.TabIndex = 76;
            this.titleBar1.TabStop = false;
            // 
            // txtCC
            // 
            this.txtCC.AllowNegative = false;
            this.txtCC.Disabled = false;
            this.txtCC.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtCC.Location = new System.Drawing.Point(100, 123);
            this.txtCC.MaxLength = 16;
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(103, 20);
            this.txtCC.TabIndex = 2;
            this.txtCC.TextChanged += new System.EventHandler(this.txtCC_TextChanged);
            this.txtCC.Enter += new System.EventHandler(this.txtCC_Enter);
            this.txtCC.Leave += new System.EventHandler(this.txtCC_Leave);
            // 
            // txtExpired
            // 
            this.txtExpired.AllowNegative = false;
            this.txtExpired.Disabled = false;
            this.txtExpired.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtExpired.Location = new System.Drawing.Point(100, 171);
            this.txtExpired.MaxLength = 4;
            this.txtExpired.Name = "txtExpired";
            this.txtExpired.Size = new System.Drawing.Size(31, 20);
            this.txtExpired.TabIndex = 4;
            // 
            // lblDocPathTip
            // 
            this.lblDocPathTip.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.lblDocPathTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDocPathTip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocPathTip.Location = new System.Drawing.Point(236, 180);
            this.lblDocPathTip.Name = "lblDocPathTip";
            this.lblDocPathTip.Size = new System.Drawing.Size(168, 82);
            this.lblDocPathTip.TabIndex = 79;
            this.lblDocPathTip.Text = "Company fundamentals cost 3$ Per Month for basic 100 Transactions, Each transacti" +
                "on above 100 cost 0.03$";
            this.lblDocPathTip.Visible = false;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::Pulsar.Properties.Resources.email_continuity_icon;
            this.picLogo.Location = new System.Drawing.Point(350, 27);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(56, 50);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 80;
            this.picLogo.TabStop = false;
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 324);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblDocPathTip);
            this.Controls.Add(this.txtExpired);
            this.Controls.Add(this.lnkRead);
            this.Controls.Add(this.chkIAgree);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbCCType);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.txtCC);
            this.Controls.Add(this.picCCValidation);
            this.Controls.Add(this.picName);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnNewBusiness);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtCVV);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCCValidation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtCVV;
        private System.Windows.Forms.Button btnNewBusiness;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox picName;
        private System.Windows.Forms.PictureBox picCCValidation;
        private NumericTextBox txtCC;
        private TitleBar titleBar1;
        private System.Windows.Forms.ComboBox cmbCCType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkIAgree;
        private System.Windows.Forms.LinkLabel lnkRead;
        private NumericTextBox txtExpired;
        private System.Windows.Forms.Label lblDocPathTip;
        private System.Windows.Forms.PictureBox picLogo;
    }
}