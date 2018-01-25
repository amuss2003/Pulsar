namespace GetGlobalInfo
{
    partial class frmCreateData
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
            System.Windows.Forms.Label companyVATToLabel;
            System.Windows.Forms.Label attachmnentLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label companyNameLabel;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label maamLabel;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            this.localInfoProtocolDataSet = new GetGlobalInfo.App_Data.LocalInfoProtocolDataSet();
            this.createDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.createDataTableAdapter = new GetGlobalInfo.App_Data.LocalInfoProtocolDataSetTableAdapters.CreateDataTableAdapter();
            this.tableAdapterManager = new GetGlobalInfo.App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager();
            this.actionsListTableAdapter = new GetGlobalInfo.App_Data.LocalInfoProtocolDataSetTableAdapters.ActionsListTableAdapter();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.companiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.actionsListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.companiesTableAdapter = new GetGlobalInfo.App_Data.LocalInfoProtocolDataSetTableAdapters.CompaniesTableAdapter();
            this.companyVATComboBox = new System.Windows.Forms.ComboBox();
            this.companyNameComboBox = new System.Windows.Forms.ComboBox();
            this.companyIDComboBox = new System.Windows.Forms.ComboBox();
            this.btnCompanies = new System.Windows.Forms.Button();
            this.dtpTarichMismach = new System.Windows.Forms.DateTimePicker();
            this.txtActionDetails = new System.Windows.Forms.TextBox();
            this.txtSchumKolelMaam = new GetGlobalInfo.NumericTextBox();
            this.txtLefniMaam = new System.Windows.Forms.TextBox();
            this.txtSchumHaMaam = new System.Windows.Forms.TextBox();
            this.txtMisparMismach = new System.Windows.Forms.TextBox();
            this.txtMaam = new System.Windows.Forms.TextBox();
            this.txtSchumPaturMmam = new System.Windows.Forms.TextBox();
            this.dtpTarichAcher = new System.Windows.Forms.DateTimePicker();
            this.ofdAttachment = new System.Windows.Forms.OpenFileDialog();
            this.attachmnentTextBox = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            companyVATToLabel = new System.Windows.Forms.Label();
            attachmnentLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            companyNameLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            maamLabel = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.localInfoProtocolDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.createDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // companyVATToLabel
            // 
            companyVATToLabel.AutoSize = true;
            companyVATToLabel.Location = new System.Drawing.Point(14, 62);
            companyVATToLabel.Name = "companyVATToLabel";
            companyVATToLabel.Size = new System.Drawing.Size(73, 13);
            companyVATToLabel.TabIndex = 9;
            companyVATToLabel.Text = "עוסק מורשה:";
            // 
            // attachmnentLabel
            // 
            attachmnentLabel.AutoSize = true;
            attachmnentLabel.Location = new System.Drawing.Point(9, 445);
            attachmnentLabel.Name = "attachmnentLabel";
            attachmnentLabel.Size = new System.Drawing.Size(70, 13);
            attachmnentLabel.TabIndex = 25;
            attachmnentLabel.Text = "Attachmnent:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 90);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(62, 13);
            label1.TabIndex = 31;
            label1.Text = "סוג תנועה:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(14, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(58, 13);
            label2.TabIndex = 32;
            label2.Text = "קוד לקוח:";
            // 
            // companyNameLabel
            // 
            companyNameLabel.AutoSize = true;
            companyNameLabel.Location = new System.Drawing.Point(14, 35);
            companyNameLabel.Name = "companyNameLabel";
            companyNameLabel.Size = new System.Drawing.Size(56, 13);
            companyNameLabel.TabIndex = 34;
            companyNameLabel.Text = "שם חברה:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(14, 117);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 13);
            label4.TabIndex = 38;
            label4.Text = "מספר מסמך:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(16, 144);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(74, 13);
            label5.TabIndex = 41;
            label5.Text = "תאריך מסמך:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(16, 195);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(83, 13);
            label6.TabIndex = 42;
            label6.Text = "פרטים לתנועה:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(13, 368);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(92, 13);
            label7.TabIndex = 44;
            label7.Text = "סכום לפני מע\"מ:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(11, 393);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(72, 13);
            label8.TabIndex = 46;
            label8.Text = "סכום המע\"מ:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(11, 419);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(94, 13);
            label9.TabIndex = 48;
            label9.Text = "סכום כולל מע\"מ:";
            // 
            // maamLabel
            // 
            maamLabel.AutoSize = true;
            maamLabel.Location = new System.Drawing.Point(16, 316);
            maamLabel.Name = "maamLabel";
            maamLabel.Size = new System.Drawing.Size(36, 13);
            maamLabel.TabIndex = 53;
            maamLabel.Text = "מע\"מ:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(16, 342);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(100, 13);
            label10.TabIndex = 55;
            label10.Text = "סכום פטור ממע\"מ:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(16, 170);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(67, 13);
            label11.TabIndex = 58;
            label11.Text = "תאריך אחר:";
            // 
            // localInfoProtocolDataSet
            // 
            this.localInfoProtocolDataSet.DataSetName = "LocalInfoProtocolDataSet";
            this.localInfoProtocolDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // createDataBindingSource
            // 
            this.createDataBindingSource.DataMember = "CreateData";
            this.createDataBindingSource.DataSource = this.localInfoProtocolDataSet;
            // 
            // createDataTableAdapter
            // 
            this.createDataTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.ActionsListTableAdapter = this.actionsListTableAdapter;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CompaniesTableAdapter = null;
            this.tableAdapterManager.CompanyInfoTableAdapter = null;
            this.tableAdapterManager.CreateDataTableAdapter = this.createDataTableAdapter;
            this.tableAdapterManager.HakladaTableAdapter = null;
            this.tableAdapterManager.IndexListTableAdapter = null;
            this.tableAdapterManager.SendDataTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = GetGlobalInfo.App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // actionsListTableAdapter
            // 
            this.actionsListTableAdapter.ClearBeforeFill = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(400, 440);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 23);
            this.btnBrowse.TabIndex = 15;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // companiesBindingSource
            // 
            this.companiesBindingSource.DataMember = "Companies";
            this.companiesBindingSource.DataSource = this.localInfoProtocolDataSet;
            // 
            // cmbActionType
            // 
            this.cmbActionType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.actionsListBindingSource, "ActionTypeIN", true));
            this.cmbActionType.DataSource = this.actionsListBindingSource;
            this.cmbActionType.DisplayMember = "ActionTypeIN";
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Location = new System.Drawing.Point(90, 87);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(121, 21);
            this.cmbActionType.TabIndex = 4;
            this.cmbActionType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // actionsListBindingSource
            // 
            this.actionsListBindingSource.DataMember = "ActionsList";
            this.actionsListBindingSource.DataSource = this.localInfoProtocolDataSet;
            // 
            // companiesTableAdapter
            // 
            this.companiesTableAdapter.ClearBeforeFill = true;
            // 
            // companyVATComboBox
            // 
            this.companyVATComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.companiesBindingSource, "CompanyVAT", true));
            this.companyVATComboBox.DataSource = this.companiesBindingSource;
            this.companyVATComboBox.DisplayMember = "CompanyVAT";
            this.companyVATComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companyVATComboBox.FormattingEnabled = true;
            this.companyVATComboBox.Location = new System.Drawing.Point(90, 59);
            this.companyVATComboBox.Name = "companyVATComboBox";
            this.companyVATComboBox.Size = new System.Drawing.Size(211, 21);
            this.companyVATComboBox.TabIndex = 3;
            this.companyVATComboBox.ValueMember = "CompanyID";
            this.companyVATComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // companyNameComboBox
            // 
            this.companyNameComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.companiesBindingSource, "CompanyName", true));
            this.companyNameComboBox.DataSource = this.companiesBindingSource;
            this.companyNameComboBox.DisplayMember = "CompanyName";
            this.companyNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companyNameComboBox.FormattingEnabled = true;
            this.companyNameComboBox.Location = new System.Drawing.Point(90, 32);
            this.companyNameComboBox.Name = "companyNameComboBox";
            this.companyNameComboBox.Size = new System.Drawing.Size(211, 21);
            this.companyNameComboBox.TabIndex = 2;
            this.companyNameComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // companyIDComboBox
            // 
            this.companyIDComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.companiesBindingSource, "CompanyID", true));
            this.companyIDComboBox.DataSource = this.companiesBindingSource;
            this.companyIDComboBox.DisplayMember = "CompanyID";
            this.companyIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companyIDComboBox.FormattingEnabled = true;
            this.companyIDComboBox.Location = new System.Drawing.Point(90, 6);
            this.companyIDComboBox.Name = "companyIDComboBox";
            this.companyIDComboBox.Size = new System.Drawing.Size(211, 21);
            this.companyIDComboBox.TabIndex = 0;
            this.companyIDComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // btnCompanies
            // 
            this.btnCompanies.Location = new System.Drawing.Point(307, 6);
            this.btnCompanies.Name = "btnCompanies";
            this.btnCompanies.Size = new System.Drawing.Size(28, 23);
            this.btnCompanies.TabIndex = 1;
            this.btnCompanies.Text = "...";
            this.btnCompanies.UseVisualStyleBackColor = true;
            this.btnCompanies.Click += new System.EventHandler(this.btnCompanies_Click);
            // 
            // dtpTarichMismach
            // 
            this.dtpTarichMismach.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarichMismach.Location = new System.Drawing.Point(90, 140);
            this.dtpTarichMismach.Name = "dtpTarichMismach";
            this.dtpTarichMismach.RightToLeftLayout = true;
            this.dtpTarichMismach.Size = new System.Drawing.Size(88, 20);
            this.dtpTarichMismach.TabIndex = 6;
            this.dtpTarichMismach.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtActionDetails
            // 
            this.txtActionDetails.Location = new System.Drawing.Point(17, 211);
            this.txtActionDetails.Multiline = true;
            this.txtActionDetails.Name = "txtActionDetails";
            this.txtActionDetails.Size = new System.Drawing.Size(282, 94);
            this.txtActionDetails.TabIndex = 8;
            this.txtActionDetails.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtSchumKolelMaam
            // 
            this.txtSchumKolelMaam.AllowNegative = false;
            this.txtSchumKolelMaam.Disabled = false;
            this.txtSchumKolelMaam.InputType = GetGlobalInfo.NumericTextBox.NumericType.IntegerInput;
            this.txtSchumKolelMaam.Location = new System.Drawing.Point(118, 416);
            this.txtSchumKolelMaam.Name = "txtSchumKolelMaam";
            this.txtSchumKolelMaam.Size = new System.Drawing.Size(100, 20);
            this.txtSchumKolelMaam.TabIndex = 13;
            this.txtSchumKolelMaam.TextChanged += new System.EventHandler(this.txtKolelMaam_TextChanged);
            this.txtSchumKolelMaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtLefniMaam
            // 
            this.txtLefniMaam.Enabled = false;
            this.txtLefniMaam.Location = new System.Drawing.Point(118, 365);
            this.txtLefniMaam.MaxLength = 12;
            this.txtLefniMaam.Name = "txtLefniMaam";
            this.txtLefniMaam.Size = new System.Drawing.Size(121, 20);
            this.txtLefniMaam.TabIndex = 11;
            this.txtLefniMaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtSchumHaMaam
            // 
            this.txtSchumHaMaam.Enabled = false;
            this.txtSchumHaMaam.Location = new System.Drawing.Point(118, 390);
            this.txtSchumHaMaam.MaxLength = 12;
            this.txtSchumHaMaam.Name = "txtSchumHaMaam";
            this.txtSchumHaMaam.Size = new System.Drawing.Size(121, 20);
            this.txtSchumHaMaam.TabIndex = 12;
            this.txtSchumHaMaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtMisparMismach
            // 
            this.txtMisparMismach.Location = new System.Drawing.Point(90, 114);
            this.txtMisparMismach.Name = "txtMisparMismach";
            this.txtMisparMismach.Size = new System.Drawing.Size(100, 20);
            this.txtMisparMismach.TabIndex = 5;
            this.txtMisparMismach.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtMaam
            // 
            this.txtMaam.Enabled = false;
            this.txtMaam.Location = new System.Drawing.Point(118, 313);
            this.txtMaam.Name = "txtMaam";
            this.txtMaam.Size = new System.Drawing.Size(100, 20);
            this.txtMaam.TabIndex = 9;
            this.txtMaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtSchumPaturMmam
            // 
            this.txtSchumPaturMmam.Location = new System.Drawing.Point(118, 339);
            this.txtSchumPaturMmam.Name = "txtSchumPaturMmam";
            this.txtSchumPaturMmam.Size = new System.Drawing.Size(100, 20);
            this.txtSchumPaturMmam.TabIndex = 10;
            this.txtSchumPaturMmam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // dtpTarichAcher
            // 
            this.dtpTarichAcher.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarichAcher.Location = new System.Drawing.Point(90, 166);
            this.dtpTarichAcher.Name = "dtpTarichAcher";
            this.dtpTarichAcher.RightToLeftLayout = true;
            this.dtpTarichAcher.Size = new System.Drawing.Size(88, 20);
            this.dtpTarichAcher.TabIndex = 7;
            this.dtpTarichAcher.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // ofdAttachment
            // 
            this.ofdAttachment.FileName = "openFileDialog1";
            // 
            // attachmnentTextBox
            // 
            this.attachmnentTextBox.Location = new System.Drawing.Point(118, 442);
            this.attachmnentTextBox.Name = "attachmnentTextBox";
            this.attachmnentTextBox.Size = new System.Drawing.Size(276, 20);
            this.attachmnentTextBox.TabIndex = 14;
            this.attachmnentTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = global::GetGlobalInfo.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Location = new System.Drawing.Point(374, 477);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 47);
            this.btnExit.TabIndex = 59;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAdd.Image = global::GetGlobalInfo.Properties.Resources.icon_add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(279, 477);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(89, 47);
            this.btnAdd.TabIndex = 60;
            this.btnAdd.Text = "הוספה";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmCreateData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 534);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.attachmnentTextBox);
            this.Controls.Add(this.dtpTarichAcher);
            this.Controls.Add(this.txtSchumPaturMmam);
            this.Controls.Add(this.txtMaam);
            this.Controls.Add(this.txtMisparMismach);
            this.Controls.Add(this.txtSchumHaMaam);
            this.Controls.Add(this.txtLefniMaam);
            this.Controls.Add(this.txtActionDetails);
            this.Controls.Add(this.dtpTarichMismach);
            this.Controls.Add(this.btnCompanies);
            this.Controls.Add(this.companyIDComboBox);
            this.Controls.Add(this.companyNameComboBox);
            this.Controls.Add(this.companyVATComboBox);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.cmbActionType);
            this.Controls.Add(this.txtSchumKolelMaam);
            this.Controls.Add(companyVATToLabel);
            this.Controls.Add(attachmnentLabel);
            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(companyNameLabel);
            this.Controls.Add(label4);
            this.Controls.Add(label5);
            this.Controls.Add(label6);
            this.Controls.Add(label7);
            this.Controls.Add(label8);
            this.Controls.Add(label9);
            this.Controls.Add(maamLabel);
            this.Controls.Add(label10);
            this.Controls.Add(label11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCreateData";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Data";
            this.Load += new System.EventHandler(this.frmCreateData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.localInfoProtocolDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.createDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private App_Data.LocalInfoProtocolDataSet localInfoProtocolDataSet;
        private System.Windows.Forms.BindingSource createDataBindingSource;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.CreateDataTableAdapter createDataTableAdapter;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ComboBox cmbActionType;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.ActionsListTableAdapter actionsListTableAdapter;
        private System.Windows.Forms.BindingSource actionsListBindingSource;
        private System.Windows.Forms.BindingSource companiesBindingSource;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.CompaniesTableAdapter companiesTableAdapter;
        private System.Windows.Forms.ComboBox companyVATComboBox;
        private System.Windows.Forms.ComboBox companyNameComboBox;
        private System.Windows.Forms.ComboBox companyIDComboBox;
        private System.Windows.Forms.Button btnCompanies;
        private System.Windows.Forms.DateTimePicker dtpTarichMismach;
        private System.Windows.Forms.TextBox txtActionDetails;
        private System.Windows.Forms.TextBox txtLefniMaam;
        private System.Windows.Forms.TextBox txtSchumHaMaam;
        private GetGlobalInfo.NumericTextBox txtSchumKolelMaam;
        private System.Windows.Forms.TextBox txtMisparMismach;
        private System.Windows.Forms.TextBox txtMaam;
        private System.Windows.Forms.TextBox txtSchumPaturMmam;
        private System.Windows.Forms.DateTimePicker dtpTarichAcher;
        private System.Windows.Forms.OpenFileDialog ofdAttachment;
        private System.Windows.Forms.TextBox attachmnentTextBox;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAdd;
    }
}