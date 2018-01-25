namespace Pulsar
{
    partial class frmBusinessIndex
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label countryIDLabel;
            System.Windows.Forms.Label companyVATLabel;
            System.Windows.Forms.Label companyNameLabel;
            System.Windows.Forms.Label accountCodeLabel;
            System.Windows.Forms.Label writeCodeLabel;
            System.Windows.Forms.Label label1;
            this.txtCountryID = new System.Windows.Forms.TextBox();
            this.cmbCountries = new System.Windows.Forms.ComboBox();
            this.txtCompanyVAT = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.txtWriteCode = new System.Windows.Forms.TextBox();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlActive = new System.Windows.Forms.Panel();
            this.pnlNotActive = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlNotExist = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pnlCompanyState = new System.Windows.Forms.Panel();
            this.picSeek = new System.Windows.Forms.PictureBox();
            this.cmbCompanyType = new System.Windows.Forms.ComboBox();
            this.titleBar1 = new Pulsar.TitleBar();
            countryIDLabel = new System.Windows.Forms.Label();
            companyVATLabel = new System.Windows.Forms.Label();
            companyNameLabel = new System.Windows.Forms.Label();
            accountCodeLabel = new System.Windows.Forms.Label();
            writeCodeLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlActive.SuspendLayout();
            this.pnlNotActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlNotExist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pnlCompanyState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSeek)).BeginInit();
            this.SuspendLayout();
            // 
            // countryIDLabel
            // 
            countryIDLabel.AutoSize = true;
            countryIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            countryIDLabel.Location = new System.Drawing.Point(211, 31);
            countryIDLabel.Name = "countryIDLabel";
            countryIDLabel.Size = new System.Drawing.Size(56, 15);
            countryIDLabel.TabIndex = 1;
            countryIDLabel.Text = "קוד מדינה:";
            // 
            // companyVATLabel
            // 
            companyVATLabel.AutoSize = true;
            companyVATLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            companyVATLabel.Location = new System.Drawing.Point(200, 87);
            companyVATLabel.Name = "companyVATLabel";
            companyVATLabel.Size = new System.Drawing.Size(67, 15);
            companyVATLabel.TabIndex = 3;
            companyVATLabel.Text = "עוסק מורשה:";
            // 
            // companyNameLabel
            // 
            companyNameLabel.AutoSize = true;
            companyNameLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            companyNameLabel.Location = new System.Drawing.Point(212, 57);
            companyNameLabel.Name = "companyNameLabel";
            companyNameLabel.Size = new System.Drawing.Size(55, 15);
            companyNameLabel.TabIndex = 5;
            companyNameLabel.Text = "שם העסק:";
            // 
            // accountCodeLabel
            // 
            accountCodeLabel.AutoSize = true;
            accountCodeLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            accountCodeLabel.Location = new System.Drawing.Point(209, 112);
            accountCodeLabel.Name = "accountCodeLabel";
            accountCodeLabel.Size = new System.Drawing.Size(58, 15);
            accountCodeLabel.TabIndex = 7;
            accountCodeLabel.Text = "קוד הנה\"ח:";
            // 
            // writeCodeLabel
            // 
            writeCodeLabel.AutoSize = true;
            writeCodeLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            writeCodeLabel.Location = new System.Drawing.Point(207, 141);
            writeCodeLabel.Name = "writeCodeLabel";
            writeCodeLabel.Size = new System.Drawing.Size(60, 15);
            writeCodeLabel.TabIndex = 9;
            writeCodeLabel.Text = "קוד כתיבה:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            label1.Location = new System.Drawing.Point(195, 168);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 15);
            label1.TabIndex = 173;
            label1.Text = "הגדרת העסק:";
            // 
            // txtCountryID
            // 
            this.txtCountryID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtCountryID.Location = new System.Drawing.Point(153, 28);
            this.txtCountryID.MaxLength = 4;
            this.txtCountryID.Name = "txtCountryID";
            this.txtCountryID.ReadOnly = true;
            this.txtCountryID.Size = new System.Drawing.Size(39, 21);
            this.txtCountryID.TabIndex = 8;
            this.txtCountryID.TabStop = false;
            this.txtCountryID.TextChanged += new System.EventHandler(this.countryIDTextBox_TextChanged);
            this.txtCountryID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // cmbCountries
            // 
            this.cmbCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmbCountries.FormattingEnabled = true;
            this.cmbCountries.Location = new System.Drawing.Point(22, 27);
            this.cmbCountries.Name = "cmbCountries";
            this.cmbCountries.Size = new System.Drawing.Size(125, 23);
            this.cmbCountries.TabIndex = 9;
            this.cmbCountries.SelectedIndexChanged += new System.EventHandler(this.comBoxCountry_SelectedIndexChanged);
            this.cmbCountries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtCompanyVAT
            // 
            this.txtCompanyVAT.Location = new System.Drawing.Point(92, 85);
            this.txtCompanyVAT.MaxLength = 9;
            this.txtCompanyVAT.Name = "txtCompanyVAT";
            this.txtCompanyVAT.Size = new System.Drawing.Size(100, 20);
            this.txtCompanyVAT.TabIndex = 1;
            this.txtCompanyVAT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.txtCompanyVAT.Leave += new System.EventHandler(this.txtCompanyVAT_Leave);
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(22, 56);
            this.txtCompanyName.MaxLength = 35;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(170, 20);
            this.txtCompanyName.TabIndex = 0;
            this.txtCompanyName.Enter += new System.EventHandler(this.txtCompanyName_Enter);
            this.txtCompanyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtWriteCode
            // 
            this.txtWriteCode.Location = new System.Drawing.Point(131, 140);
            this.txtWriteCode.MaxLength = 9;
            this.txtWriteCode.Name = "txtWriteCode";
            this.txtWriteCode.Size = new System.Drawing.Size(61, 20);
            this.txtWriteCode.TabIndex = 3;
            this.txtWriteCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Location = new System.Drawing.Point(131, 111);
            this.txtAccountCode.MaxLength = 9;
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Size = new System.Drawing.Size(61, 20);
            this.txtAccountCode.TabIndex = 2;
            this.txtAccountCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackgroundImage = global::Pulsar.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(12, 195);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 47);
            this.btnExit.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnExit, "יציאה");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNew.Image = global::Pulsar.Properties.Resources.icon_new;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(199, 195);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(57, 47);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "נקה";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Visible = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnDelete.Image = global::Pulsar.Properties.Resources.icon_delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(63, 195);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(62, 47);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "מחק";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSave.Image = global::Pulsar.Properties.Resources.save_icon1;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(128, 195);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 47);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "שמור";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 14);
            this.label2.TabIndex = 164;
            this.label2.Text = "פעיל";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Pulsar.Properties.Resources.active;
            this.pictureBox1.Location = new System.Drawing.Point(37, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 161;
            this.pictureBox1.TabStop = false;
            // 
            // pnlActive
            // 
            this.pnlActive.Controls.Add(this.pictureBox1);
            this.pnlActive.Controls.Add(this.label2);
            this.pnlActive.Location = new System.Drawing.Point(21, 0);
            this.pnlActive.Name = "pnlActive";
            this.pnlActive.Size = new System.Drawing.Size(54, 16);
            this.pnlActive.TabIndex = 167;
            this.pnlActive.Visible = false;
            // 
            // pnlNotActive
            // 
            this.pnlNotActive.Controls.Add(this.label3);
            this.pnlNotActive.Controls.Add(this.pictureBox2);
            this.pnlNotActive.Location = new System.Drawing.Point(2, 19);
            this.pnlNotActive.Name = "pnlNotActive";
            this.pnlNotActive.Size = new System.Drawing.Size(72, 16);
            this.pnlNotActive.TabIndex = 168;
            this.pnlNotActive.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 14);
            this.label3.TabIndex = 167;
            this.label3.Text = "לא פעיל";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Pulsar.Properties.Resources.notactive;
            this.pictureBox2.Location = new System.Drawing.Point(56, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 166;
            this.pictureBox2.TabStop = false;
            // 
            // pnlNotExist
            // 
            this.pnlNotExist.Controls.Add(this.label4);
            this.pnlNotExist.Controls.Add(this.pictureBox3);
            this.pnlNotExist.Location = new System.Drawing.Point(2, 38);
            this.pnlNotExist.Name = "pnlNotExist";
            this.pnlNotExist.Size = new System.Drawing.Size(71, 16);
            this.pnlNotExist.TabIndex = 169;
            this.pnlNotExist.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 14);
            this.label4.TabIndex = 168;
            this.label4.Text = "לא קיים";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Pulsar.Properties.Resources.inactive;
            this.pictureBox3.Location = new System.Drawing.Point(55, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 167;
            this.pictureBox3.TabStop = false;
            // 
            // pnlCompanyState
            // 
            this.pnlCompanyState.Controls.Add(this.pnlActive);
            this.pnlCompanyState.Controls.Add(this.pnlNotExist);
            this.pnlCompanyState.Controls.Add(this.pnlNotActive);
            this.pnlCompanyState.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCompanyState.Location = new System.Drawing.Point(10, 87);
            this.pnlCompanyState.Name = "pnlCompanyState";
            this.pnlCompanyState.Size = new System.Drawing.Size(75, 56);
            this.pnlCompanyState.TabIndex = 170;
            // 
            // picSeek
            // 
            this.picSeek.Image = global::Pulsar.Properties.Resources.seek;
            this.picSeek.Location = new System.Drawing.Point(270, 90);
            this.picSeek.Name = "picSeek";
            this.picSeek.Size = new System.Drawing.Size(13, 13);
            this.picSeek.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSeek.TabIndex = 171;
            this.picSeek.TabStop = false;
            this.picSeek.Visible = false;
            // 
            // cmbCompanyType
            // 
            this.cmbCompanyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyType.FormattingEnabled = true;
            this.cmbCompanyType.Location = new System.Drawing.Point(92, 166);
            this.cmbCompanyType.Name = "cmbCompanyType";
            this.cmbCompanyType.Size = new System.Drawing.Size(100, 21);
            this.cmbCompanyType.TabIndex = 4;
            // 
            // titleBar1
            // 
            this.titleBar1.company_info = null;
            this.titleBar1.Company_Info = null;
            this.titleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar1.Location = new System.Drawing.Point(0, 0);
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.titleBar1.Size = new System.Drawing.Size(296, 21);
            this.titleBar1.TabIndex = 113;
            this.titleBar1.TabStop = false;
            // 
            // frmBusinessIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(296, 254);
            this.Controls.Add(label1);
            this.Controls.Add(this.cmbCompanyType);
            this.Controls.Add(this.picSeek);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtAccountCode);
            this.Controls.Add(this.txtWriteCode);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.txtCompanyVAT);
            this.Controls.Add(this.cmbCountries);
            this.Controls.Add(countryIDLabel);
            this.Controls.Add(this.txtCountryID);
            this.Controls.Add(companyVATLabel);
            this.Controls.Add(companyNameLabel);
            this.Controls.Add(accountCodeLabel);
            this.Controls.Add(writeCodeLabel);
            this.Controls.Add(this.pnlCompanyState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBusinessIndex";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "הקמת עסק";
            this.Load += new System.EventHandler(this.frmActionType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlActive.ResumeLayout(false);
            this.pnlActive.PerformLayout();
            this.pnlNotActive.ResumeLayout(false);
            this.pnlNotActive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlNotExist.ResumeLayout(false);
            this.pnlNotExist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pnlCompanyState.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSeek)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCountryID;
        private System.Windows.Forms.ComboBox cmbCountries;
        private System.Windows.Forms.TextBox txtCompanyVAT;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.TextBox txtWriteCode;
        private System.Windows.Forms.TextBox txtAccountCode;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private TitleBar titleBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlActive;
        private System.Windows.Forms.Panel pnlNotActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel pnlNotExist;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel pnlCompanyState;
        private System.Windows.Forms.PictureBox picSeek;
        private System.Windows.Forms.ComboBox cmbCompanyType;

    }
}