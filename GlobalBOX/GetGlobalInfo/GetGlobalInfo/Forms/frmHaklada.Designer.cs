namespace Pulsar
{
    partial class frmHaklada
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
            System.Windows.Forms.Label attachmnentLabel;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label maamLabel;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label companyVATToLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label companyNameLabel;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHaklada));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "קוד מדינה"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "עוסק מורשה"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "שם חברה"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "קוד פעולה"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "מ.מסמך"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "ת.מסמך"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "ת.אחר"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "פרטים לחן"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "מע\"מ"}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "ס.פטור מע\"מ"}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "ס.מע\"מ"}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "ס.כולל מע\"מ"}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "מסמך מצורף"}, -1);
            this.localInfoProtocolDataSet = new Pulsar.App_Data.LocalInfoProtocolDataSet();
            this.hakladaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hakladaTableAdapter = new Pulsar.App_Data.LocalInfoProtocolDataSetTableAdapters.HakladaTableAdapter();
            this.tableAdapterManager = new Pulsar.App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager();
            this.actionsListTableAdapter = new Pulsar.App_Data.LocalInfoProtocolDataSetTableAdapters.ActionsListTableAdapter();
            this.companiesTableAdapter = new Pulsar.App_Data.LocalInfoProtocolDataSetTableAdapters.CompaniesTableAdapter();
            this.companiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.companyNameComboBox = new System.Windows.Forms.ComboBox();
            this.companyVATComboBox = new System.Windows.Forms.ComboBox();
            this.actionsListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.companyIDComboBox = new System.Windows.Forms.ComboBox();
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.btnCompanies = new System.Windows.Forms.Button();
            this.attachmnentTextBox = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dtpTarichAcher = new System.Windows.Forms.DateTimePicker();
            this.txtLefniMaam = new System.Windows.Forms.TextBox();
            this.txtActionDetails = new System.Windows.Forms.TextBox();
            this.dtpTarichMismach = new System.Windows.Forms.DateTimePicker();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.ofdAttachment = new System.Windows.Forms.OpenFileDialog();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.dtpTarichAcherNew = new Pulsar.DateTimeControl();
            this.dtpTarichMismachNew = new Pulsar.DateTimeControl();
            this.txtSchumPaturMmam = new Pulsar.NumericTextBox();
            this.txtMaam = new Pulsar.NumericTextBox();
            this.txtMisparMismach = new Pulsar.NumericTextBox();
            this.txtSchumHaMaam = new Pulsar.NumericTextBox();
            this.txtSchumKolelMaam = new Pulsar.NumericTextBox();
            this.btnSearchDocument = new System.Windows.Forms.Button();
            this.lstResultSearchFiles = new System.Windows.Forms.ListBox();
            this.grpSearchFiles = new System.Windows.Forms.GroupBox();
            this.btnSelection = new System.Windows.Forms.Button();
            this.btnExitSearchFiles = new System.Windows.Forms.Button();
            this.lvwOutbox = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.titleBar1 = new Pulsar.TitleBar();
            this.grpImport = new System.Windows.Forms.GroupBox();
            this.btnUpdateImport = new System.Windows.Forms.Button();
            this.lvwImportFileds = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnImportHelp = new System.Windows.Forms.Button();
            this.radMokup = new System.Windows.Forms.RadioButton();
            this.radFixedSizeImport = new System.Windows.Forms.RadioButton();
            this.radTabImport = new System.Windows.Forms.RadioButton();
            this.radPipeImport = new System.Windows.Forms.RadioButton();
            this.radCommaImport = new System.Windows.Forms.RadioButton();
            this.radOtherImport = new System.Windows.Forms.RadioButton();
            this.txtDelimiterImport = new System.Windows.Forms.TextBox();
            this.btnSelectionImport = new System.Windows.Forms.Button();
            this.btnExitImport = new System.Windows.Forms.Button();
            this.btnImportInbox = new System.Windows.Forms.Button();
            this.txtMisparProyect = new Pulsar.NumericTextBox();
            this.dtpTo = new Pulsar.DateTimeControl();
            this.dtpFrom = new Pulsar.DateTimeControl();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnTochenTnua = new System.Windows.Forms.Button();
            attachmnentLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            maamLabel = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            companyVATToLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            companyNameLabel = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.localInfoProtocolDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hakladaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListBindingSource)).BeginInit();
            this.grpSearchFiles.SuspendLayout();
            this.grpImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // attachmnentLabel
            // 
            attachmnentLabel.AutoSize = true;
            attachmnentLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            attachmnentLabel.Location = new System.Drawing.Point(12, 468);
            attachmnentLabel.Name = "attachmnentLabel";
            attachmnentLabel.Size = new System.Drawing.Size(77, 16);
            attachmnentLabel.TabIndex = 72;
            attachmnentLabel.Text = "מסמך מצורף:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(12, 140);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(75, 16);
            label4.TabIndex = 73;
            label4.Text = "מספר מסמך:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(12, 167);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(78, 16);
            label5.TabIndex = 74;
            label5.Text = "תאריך מסמך:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(12, 221);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(86, 16);
            label6.TabIndex = 75;
            label6.Text = "פרטים לתנועה:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.Location = new System.Drawing.Point(12, 391);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(93, 16);
            label7.TabIndex = 76;
            label7.Text = "סכום לפני מע\"מ:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.Location = new System.Drawing.Point(12, 416);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(76, 16);
            label8.TabIndex = 77;
            label8.Text = "סכום המע\"מ:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.Location = new System.Drawing.Point(12, 442);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(93, 16);
            label9.TabIndex = 78;
            label9.Text = "סכום כולל מע\"מ:";
            // 
            // maamLabel
            // 
            maamLabel.AutoSize = true;
            maamLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            maamLabel.Location = new System.Drawing.Point(12, 339);
            maamLabel.Name = "maamLabel";
            maamLabel.Size = new System.Drawing.Size(40, 16);
            maamLabel.TabIndex = 79;
            maamLabel.Text = "מע\"מ:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(12, 365);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(105, 16);
            label10.TabIndex = 80;
            label10.Text = "סכום פטור ממע\"מ:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label11.Location = new System.Drawing.Point(12, 193);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(70, 16);
            label11.TabIndex = 81;
            label11.Text = "תאריך אחר:";
            // 
            // companyVATToLabel
            // 
            companyVATToLabel.AutoSize = true;
            companyVATToLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            companyVATToLabel.Location = new System.Drawing.Point(12, 88);
            companyVATToLabel.Name = "companyVATToLabel";
            companyVATToLabel.Size = new System.Drawing.Size(75, 16);
            companyVATToLabel.TabIndex = 82;
            companyVATToLabel.Text = "עוסק מורשה:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(12, 116);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(62, 16);
            label1.TabIndex = 83;
            label1.Text = "סוג תנועה:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(12, 35);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 16);
            label2.TabIndex = 84;
            label2.Text = "קוד העסק:";
            // 
            // companyNameLabel
            // 
            companyNameLabel.AutoSize = true;
            companyNameLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            companyNameLabel.Location = new System.Drawing.Point(12, 61);
            companyNameLabel.Name = "companyNameLabel";
            companyNameLabel.Size = new System.Drawing.Size(62, 16);
            companyNameLabel.TabIndex = 85;
            companyNameLabel.Text = "שם העסק:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(12, 545);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(83, 16);
            label3.TabIndex = 156;
            label3.Text = "מספר פרוייקט:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label12.Location = new System.Drawing.Point(12, 493);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(63, 16);
            label12.TabIndex = 157;
            label12.Text = "לתקופה מ:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label13.Location = new System.Drawing.Point(12, 519);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(69, 16);
            label13.TabIndex = 158;
            label13.Text = "לתקופה עד:";
            // 
            // localInfoProtocolDataSet
            // 
            this.localInfoProtocolDataSet.DataSetName = "LocalInfoProtocolDataSet";
            this.localInfoProtocolDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // hakladaBindingSource
            // 
            this.hakladaBindingSource.DataMember = "Haklada";
            this.hakladaBindingSource.DataSource = this.localInfoProtocolDataSet;
            // 
            // hakladaTableAdapter
            // 
            this.hakladaTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.ActionsListTableAdapter = this.actionsListTableAdapter;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CompaniesTableAdapter = this.companiesTableAdapter;
            this.tableAdapterManager.CompanyInfoTableAdapter = null;
            this.tableAdapterManager.HakladaTableAdapter = this.hakladaTableAdapter;
            this.tableAdapterManager.HakladatInboxTochenTnuaTableAdapter = null;
            this.tableAdapterManager.HakladatTochenTnuaTableAdapter = null;
            this.tableAdapterManager.InboxTableAdapter = null;
            this.tableAdapterManager.OutboxTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = Pulsar.App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // actionsListTableAdapter
            // 
            this.actionsListTableAdapter.ClearBeforeFill = true;
            // 
            // companiesTableAdapter
            // 
            this.companiesTableAdapter.ClearBeforeFill = true;
            // 
            // companiesBindingSource
            // 
            this.companiesBindingSource.DataMember = "Companies";
            this.companiesBindingSource.DataSource = this.localInfoProtocolDataSet;
            // 
            // companyNameComboBox
            // 
            this.companyNameComboBox.FormattingEnabled = true;
            this.companyNameComboBox.Location = new System.Drawing.Point(89, 59);
            this.companyNameComboBox.Name = "companyNameComboBox";
            this.companyNameComboBox.Size = new System.Drawing.Size(200, 21);
            this.companyNameComboBox.TabIndex = 2;
            this.companyNameComboBox.SelectedIndexChanged += new System.EventHandler(this.companyNameComboBox_SelectedIndexChanged);
            this.companyNameComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.companyNameComboBox_KeyPress);
            // 
            // companyVATComboBox
            // 
            this.companyVATComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companyVATComboBox.FormattingEnabled = true;
            this.companyVATComboBox.Location = new System.Drawing.Point(89, 86);
            this.companyVATComboBox.Name = "companyVATComboBox";
            this.companyVATComboBox.Size = new System.Drawing.Size(200, 21);
            this.companyVATComboBox.TabIndex = 3;
            this.companyVATComboBox.SelectedIndexChanged += new System.EventHandler(this.companyVATComboBox_SelectedIndexChanged);
            this.companyVATComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // actionsListBindingSource
            // 
            this.actionsListBindingSource.DataMember = "ActionsList";
            this.actionsListBindingSource.DataSource = this.localInfoProtocolDataSet;
            // 
            // companyIDComboBox
            // 
            this.companyIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companyIDComboBox.FormattingEnabled = true;
            this.companyIDComboBox.Location = new System.Drawing.Point(89, 34);
            this.companyIDComboBox.Name = "companyIDComboBox";
            this.companyIDComboBox.Size = new System.Drawing.Size(121, 21);
            this.companyIDComboBox.TabIndex = 0;
            this.companyIDComboBox.SelectedIndexChanged += new System.EventHandler(this.companyIDComboBox_SelectedIndexChanged);
            this.companyIDComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // cmbActionType
            // 
            this.cmbActionType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.actionsListBindingSource, "ActionTypeIN", true));
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Location = new System.Drawing.Point(89, 113);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(121, 21);
            this.cmbActionType.TabIndex = 4;
            this.cmbActionType.SelectedIndexChanged += new System.EventHandler(this.cmbActionType_SelectedIndexChanged);
            this.cmbActionType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // btnCompanies
            // 
            this.btnCompanies.Location = new System.Drawing.Point(261, 32);
            this.btnCompanies.Name = "btnCompanies";
            this.btnCompanies.Size = new System.Drawing.Size(28, 23);
            this.btnCompanies.TabIndex = 1;
            this.btnCompanies.TabStop = false;
            this.btnCompanies.Text = "...";
            this.btnCompanies.UseVisualStyleBackColor = true;
            this.btnCompanies.Click += new System.EventHandler(this.btnCompanies_Click);
            // 
            // attachmnentTextBox
            // 
            this.attachmnentTextBox.Location = new System.Drawing.Point(116, 468);
            this.attachmnentTextBox.MaxLength = 255;
            this.attachmnentTextBox.Name = "attachmnentTextBox";
            this.attachmnentTextBox.Size = new System.Drawing.Size(124, 20);
            this.attachmnentTextBox.TabIndex = 14;
            this.attachmnentTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAdd.Image = global::Pulsar.Properties.Resources.icon_add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(81, 647);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(84, 47);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "הוספה";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dtpTarichAcher
            // 
            this.dtpTarichAcher.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarichAcher.Location = new System.Drawing.Point(159, 192);
            this.dtpTarichAcher.Name = "dtpTarichAcher";
            this.dtpTarichAcher.RightToLeftLayout = true;
            this.dtpTarichAcher.Size = new System.Drawing.Size(88, 20);
            this.dtpTarichAcher.TabIndex = 107;
            this.dtpTarichAcher.Visible = false;
            this.dtpTarichAcher.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtLefniMaam
            // 
            this.txtLefniMaam.Enabled = false;
            this.txtLefniMaam.Location = new System.Drawing.Point(116, 391);
            this.txtLefniMaam.MaxLength = 12;
            this.txtLefniMaam.Name = "txtLefniMaam";
            this.txtLefniMaam.Size = new System.Drawing.Size(121, 20);
            this.txtLefniMaam.TabIndex = 11;
            this.txtLefniMaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtActionDetails
            // 
            this.txtActionDetails.Location = new System.Drawing.Point(15, 237);
            this.txtActionDetails.MaxLength = 70;
            this.txtActionDetails.Multiline = true;
            this.txtActionDetails.Name = "txtActionDetails";
            this.txtActionDetails.Size = new System.Drawing.Size(282, 94);
            this.txtActionDetails.TabIndex = 8;
            this.txtActionDetails.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // dtpTarichMismach
            // 
            this.dtpTarichMismach.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarichMismach.Location = new System.Drawing.Point(159, 166);
            this.dtpTarichMismach.Name = "dtpTarichMismach";
            this.dtpTarichMismach.RightToLeftLayout = true;
            this.dtpTarichMismach.Size = new System.Drawing.Size(88, 20);
            this.dtpTarichMismach.TabIndex = 106;
            this.dtpTarichMismach.Visible = false;
            this.dtpTarichMismach.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(275, 466);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 24);
            this.btnBrowse.TabIndex = 16;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // ofdAttachment
            // 
            this.ofdAttachment.FileName = "openFileDialog1";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnUpdate.Image = global::Pulsar.Properties.Resources.edit_icon;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(171, 647);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(69, 47);
            this.btnUpdate.TabIndex = 20;
            this.btnUpdate.Text = "עדכן";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackgroundImage = global::Pulsar.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(956, 647);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 47);
            this.btnExit.TabIndex = 22;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNew.Image = global::Pulsar.Properties.Resources.icon_new;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(11, 647);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(64, 47);
            this.btnNew.TabIndex = 17;
            this.btnNew.Text = "חדש";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // dtpTarichAcherNew
            // 
            this.dtpTarichAcherNew.AutoComplete0 = false;
            this.dtpTarichAcherNew.BackColor = System.Drawing.Color.Transparent;
            this.dtpTarichAcherNew.DateType = Pulsar.DateTimeControl.eDateType.FullDate;
            this.dtpTarichAcherNew.Jumping = false;
            this.dtpTarichAcherNew.Location = new System.Drawing.Point(89, 192);
            this.dtpTarichAcherNew.Name = "dtpTarichAcherNew";
            this.dtpTarichAcherNew.Size = new System.Drawing.Size(64, 20);
            this.dtpTarichAcherNew.TabIndex = 7;
            this.dtpTarichAcherNew.Value = new System.DateTime(2013, 1, 15, 0, 0, 0, 0);
            // 
            // dtpTarichMismachNew
            // 
            this.dtpTarichMismachNew.AutoComplete0 = false;
            this.dtpTarichMismachNew.BackColor = System.Drawing.Color.Transparent;
            this.dtpTarichMismachNew.DateType = Pulsar.DateTimeControl.eDateType.FullDate;
            this.dtpTarichMismachNew.Jumping = false;
            this.dtpTarichMismachNew.Location = new System.Drawing.Point(89, 166);
            this.dtpTarichMismachNew.Name = "dtpTarichMismachNew";
            this.dtpTarichMismachNew.Size = new System.Drawing.Size(64, 20);
            this.dtpTarichMismachNew.TabIndex = 6;
            this.dtpTarichMismachNew.Value = new System.DateTime(2013, 1, 15, 0, 0, 0, 0);
            // 
            // txtSchumPaturMmam
            // 
            this.txtSchumPaturMmam.AllowNegative = false;
            this.txtSchumPaturMmam.Disabled = false;
            this.txtSchumPaturMmam.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtSchumPaturMmam.Location = new System.Drawing.Point(116, 365);
            this.txtSchumPaturMmam.MaxLength = 16;
            this.txtSchumPaturMmam.Name = "txtSchumPaturMmam";
            this.txtSchumPaturMmam.Size = new System.Drawing.Size(100, 20);
            this.txtSchumPaturMmam.TabIndex = 10;
            this.txtSchumPaturMmam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.txtSchumPaturMmam.Leave += new System.EventHandler(this.txtSchumPaturMmam_Leave);
            // 
            // txtMaam
            // 
            this.txtMaam.AllowNegative = false;
            this.txtMaam.Disabled = false;
            this.txtMaam.Enabled = false;
            this.txtMaam.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtMaam.Location = new System.Drawing.Point(116, 339);
            this.txtMaam.MaxLength = 16;
            this.txtMaam.Name = "txtMaam";
            this.txtMaam.Size = new System.Drawing.Size(100, 20);
            this.txtMaam.TabIndex = 9;
            this.txtMaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtMisparMismach
            // 
            this.txtMisparMismach.AllowNegative = false;
            this.txtMisparMismach.Disabled = false;
            this.txtMisparMismach.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtMisparMismach.Location = new System.Drawing.Point(89, 140);
            this.txtMisparMismach.MaxLength = 24;
            this.txtMisparMismach.Name = "txtMisparMismach";
            this.txtMisparMismach.Size = new System.Drawing.Size(100, 20);
            this.txtMisparMismach.TabIndex = 5;
            this.txtMisparMismach.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtSchumHaMaam
            // 
            this.txtSchumHaMaam.AllowNegative = false;
            this.txtSchumHaMaam.Disabled = false;
            this.txtSchumHaMaam.Enabled = false;
            this.txtSchumHaMaam.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtSchumHaMaam.Location = new System.Drawing.Point(116, 416);
            this.txtSchumHaMaam.MaxLength = 12;
            this.txtSchumHaMaam.Name = "txtSchumHaMaam";
            this.txtSchumHaMaam.Size = new System.Drawing.Size(121, 20);
            this.txtSchumHaMaam.TabIndex = 12;
            this.txtSchumHaMaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtSchumKolelMaam
            // 
            this.txtSchumKolelMaam.AllowNegative = false;
            this.txtSchumKolelMaam.Disabled = false;
            this.txtSchumKolelMaam.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtSchumKolelMaam.Location = new System.Drawing.Point(116, 442);
            this.txtSchumKolelMaam.MaxLength = 16;
            this.txtSchumKolelMaam.Name = "txtSchumKolelMaam";
            this.txtSchumKolelMaam.Size = new System.Drawing.Size(100, 20);
            this.txtSchumKolelMaam.TabIndex = 13;
            this.txtSchumKolelMaam.TextChanged += new System.EventHandler(this.txtSchumKolelMaam_TextChanged);
            this.txtSchumKolelMaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // btnSearchDocument
            // 
            this.btnSearchDocument.BackgroundImage = global::Pulsar.Properties.Resources.search_green;
            this.btnSearchDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearchDocument.Location = new System.Drawing.Point(246, 466);
            this.btnSearchDocument.Name = "btnSearchDocument";
            this.btnSearchDocument.Size = new System.Drawing.Size(24, 24);
            this.btnSearchDocument.TabIndex = 15;
            this.btnSearchDocument.UseVisualStyleBackColor = true;
            this.btnSearchDocument.Click += new System.EventHandler(this.btnSearchDocument_Click);
            // 
            // lstResultSearchFiles
            // 
            this.lstResultSearchFiles.FormattingEnabled = true;
            this.lstResultSearchFiles.ItemHeight = 16;
            this.lstResultSearchFiles.Location = new System.Drawing.Point(15, 20);
            this.lstResultSearchFiles.Name = "lstResultSearchFiles";
            this.lstResultSearchFiles.Size = new System.Drawing.Size(315, 212);
            this.lstResultSearchFiles.TabIndex = 109;
            this.lstResultSearchFiles.DoubleClick += new System.EventHandler(this.lstResultSearchFiles_DoubleClick);
            // 
            // grpSearchFiles
            // 
            this.grpSearchFiles.Controls.Add(this.btnSelection);
            this.grpSearchFiles.Controls.Add(this.btnExitSearchFiles);
            this.grpSearchFiles.Controls.Add(this.lstResultSearchFiles);
            this.grpSearchFiles.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSearchFiles.Location = new System.Drawing.Point(456, 113);
            this.grpSearchFiles.Name = "grpSearchFiles";
            this.grpSearchFiles.Size = new System.Drawing.Size(344, 305);
            this.grpSearchFiles.TabIndex = 110;
            this.grpSearchFiles.TabStop = false;
            this.grpSearchFiles.Text = "תוצאות חיפוש";
            this.grpSearchFiles.Visible = false;
            // 
            // btnSelection
            // 
            this.btnSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSelection.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSelection.Image = global::Pulsar.Properties.Resources.icon_pan;
            this.btnSelection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelection.Location = new System.Drawing.Point(69, 249);
            this.btnSelection.Name = "btnSelection";
            this.btnSelection.Size = new System.Drawing.Size(64, 47);
            this.btnSelection.TabIndex = 111;
            this.btnSelection.Text = "בחר";
            this.btnSelection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelection.UseVisualStyleBackColor = true;
            this.btnSelection.Click += new System.EventHandler(this.btnSelection_Click);
            // 
            // btnExitSearchFiles
            // 
            this.btnExitSearchFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExitSearchFiles.BackgroundImage = global::Pulsar.Properties.Resources.exit;
            this.btnExitSearchFiles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExitSearchFiles.Location = new System.Drawing.Point(15, 249);
            this.btnExitSearchFiles.Name = "btnExitSearchFiles";
            this.btnExitSearchFiles.Size = new System.Drawing.Size(48, 47);
            this.btnExitSearchFiles.TabIndex = 110;
            this.btnExitSearchFiles.UseVisualStyleBackColor = true;
            this.btnExitSearchFiles.Click += new System.EventHandler(this.btnExitSearchFiles_Click);
            // 
            // lvwOutbox
            // 
            this.lvwOutbox.AutoArrange = false;
            this.lvwOutbox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader10,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19});
            this.lvwOutbox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwOutbox.FullRowSelect = true;
            this.lvwOutbox.HideSelection = false;
            this.lvwOutbox.Location = new System.Drawing.Point(311, 35);
            this.lvwOutbox.MultiSelect = false;
            this.lvwOutbox.Name = "lvwOutbox";
            this.lvwOutbox.OwnerDraw = true;
            this.lvwOutbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lvwOutbox.Size = new System.Drawing.Size(693, 595);
            this.lvwOutbox.SmallImageList = this.imageList1;
            this.lvwOutbox.TabIndex = 111;
            this.lvwOutbox.UseCompatibleStateImageBehavior = false;
            this.lvwOutbox.View = System.Windows.Forms.View.Details;
            this.lvwOutbox.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvwOutbox_ColumnWidthChanged);
            this.lvwOutbox.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvwOutbox_ColumnWidthChanging);
            this.lvwOutbox.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvwOutbox_DrawColumnHeader);
            this.lvwOutbox.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvwOutbox_DrawItem);
            this.lvwOutbox.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvwOutbox_DrawSubItem);
            this.lvwOutbox.SelectedIndexChanged += new System.EventHandler(this.lvwOutbox_SelectedIndexChanged);
            this.lvwOutbox.DoubleClick += new System.EventHandler(this.lvwOutbox_DoubleClick);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "";
            this.columnHeader8.Width = 36;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "ס\"ת";
            this.columnHeader13.Width = 38;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "מס.מסמך";
            this.columnHeader14.Width = 71;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "ת.מסמך";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "ת.אחר";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "שם העסק";
            this.columnHeader10.Width = 109;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "פרטים לחן";
            this.columnHeader17.Width = 420;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "סכום כולל";
            this.columnHeader18.Width = 70;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "ת.משלוח";
            this.columnHeader19.Width = 76;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "inbox.png");
            this.imageList1.Images.SetKeyName(1, "outbox.png");
            this.imageList1.Images.SetKeyName(2, "icon_ticket_history.gif");
            this.imageList1.Images.SetKeyName(3, "icon_download.png");
            this.imageList1.Images.SetKeyName(4, "icon_eye.gif");
            this.imageList1.Images.SetKeyName(5, "attachment-icon.png");
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Pulsar.Properties.Resources.icon_delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(246, 647);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 47);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "מחק";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // titleBar1
            // 
            this.titleBar1.company_info = null;
            this.titleBar1.Company_Info = null;
            this.titleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar1.Location = new System.Drawing.Point(0, 0);
            this.titleBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.titleBar1.Size = new System.Drawing.Size(1016, 24);
            this.titleBar1.TabIndex = 112;
            this.titleBar1.TabStop = false;
            // 
            // grpImport
            // 
            this.grpImport.Controls.Add(this.btnUpdateImport);
            this.grpImport.Controls.Add(this.lvwImportFileds);
            this.grpImport.Controls.Add(this.btnImportHelp);
            this.grpImport.Controls.Add(this.radMokup);
            this.grpImport.Controls.Add(this.radFixedSizeImport);
            this.grpImport.Controls.Add(this.radTabImport);
            this.grpImport.Controls.Add(this.radPipeImport);
            this.grpImport.Controls.Add(this.radCommaImport);
            this.grpImport.Controls.Add(this.radOtherImport);
            this.grpImport.Controls.Add(this.txtDelimiterImport);
            this.grpImport.Controls.Add(this.btnSelectionImport);
            this.grpImport.Controls.Add(this.btnExitImport);
            this.grpImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpImport.Location = new System.Drawing.Point(438, 94);
            this.grpImport.Name = "grpImport";
            this.grpImport.Size = new System.Drawing.Size(397, 390);
            this.grpImport.TabIndex = 153;
            this.grpImport.TabStop = false;
            this.grpImport.Text = "סוג יבוא";
            this.grpImport.Visible = false;
            this.grpImport.Enter += new System.EventHandler(this.grpImport_Enter);
            // 
            // btnUpdateImport
            // 
            this.btnUpdateImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateImport.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnUpdateImport.Image = global::Pulsar.Properties.Resources.edit_icon;
            this.btnUpdateImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateImport.Location = new System.Drawing.Point(15, 283);
            this.btnUpdateImport.Name = "btnUpdateImport";
            this.btnUpdateImport.Size = new System.Drawing.Size(68, 33);
            this.btnUpdateImport.TabIndex = 38;
            this.btnUpdateImport.Text = "עדכן";
            this.btnUpdateImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateImport.UseVisualStyleBackColor = true;
            this.btnUpdateImport.Click += new System.EventHandler(this.btnUpdateImport_Click);
            // 
            // lvwImportFileds
            // 
            this.lvwImportFileds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvwImportFileds.FullRowSelect = true;
            listViewItem1.Tag = "CountryID";
            listViewItem2.Tag = "VAT";
            listViewItem3.Tag = "CompanyName";
            listViewItem4.Tag = "ActionCode";
            listViewItem5.Tag = "MisparMismach";
            listViewItem6.Tag = "TarichMismach";
            listViewItem7.Tag = "TarichAcher";
            listViewItem8.Tag = "ActionDetails";
            listViewItem9.Tag = "Maam";
            listViewItem10.Tag = "SchumPaturMaam";
            listViewItem11.Tag = "SchumMaam";
            listViewItem12.Tag = "SchumKolelMaam";
            listViewItem13.Tag = "Attachment";
            this.lvwImportFileds.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13});
            this.lvwImportFileds.LabelEdit = true;
            this.lvwImportFileds.Location = new System.Drawing.Point(15, 21);
            this.lvwImportFileds.Name = "lvwImportFileds";
            this.lvwImportFileds.Size = new System.Drawing.Size(158, 256);
            this.lvwImportFileds.TabIndex = 37;
            this.lvwImportFileds.UseCompatibleStateImageBehavior = false;
            this.lvwImportFileds.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "מיקום";
            this.columnHeader2.Width = 47;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "שם שדה";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader1.Width = 98;
            // 
            // btnImportHelp
            // 
            this.btnImportHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnImportHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnImportHelp.Image = global::Pulsar.Properties.Resources.help24;
            this.btnImportHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportHelp.Location = new System.Drawing.Point(140, 281);
            this.btnImportHelp.Name = "btnImportHelp";
            this.btnImportHelp.Size = new System.Drawing.Size(33, 32);
            this.btnImportHelp.TabIndex = 35;
            this.btnImportHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImportHelp.UseVisualStyleBackColor = true;
            this.btnImportHelp.Click += new System.EventHandler(this.btnImportHelp_Click);
            // 
            // radMokup
            // 
            this.radMokup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radMokup.AutoSize = true;
            this.radMokup.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radMokup.Location = new System.Drawing.Point(324, 46);
            this.radMokup.Name = "radMokup";
            this.radMokup.Size = new System.Drawing.Size(57, 20);
            this.radMokup.TabIndex = 34;
            this.radMokup.Text = "תבנית";
            this.radMokup.UseVisualStyleBackColor = true;
            // 
            // radFixedSizeImport
            // 
            this.radFixedSizeImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radFixedSizeImport.AutoSize = true;
            this.radFixedSizeImport.Checked = true;
            this.radFixedSizeImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radFixedSizeImport.Location = new System.Drawing.Point(306, 25);
            this.radFixedSizeImport.Name = "radFixedSizeImport";
            this.radFixedSizeImport.Size = new System.Drawing.Size(75, 20);
            this.radFixedSizeImport.TabIndex = 33;
            this.radFixedSizeImport.TabStop = true;
            this.radFixedSizeImport.Text = "גודל קבוע";
            this.radFixedSizeImport.UseVisualStyleBackColor = true;
            // 
            // radTabImport
            // 
            this.radTabImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radTabImport.AutoSize = true;
            this.radTabImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radTabImport.Location = new System.Drawing.Point(333, 67);
            this.radTabImport.Name = "radTabImport";
            this.radTabImport.Size = new System.Drawing.Size(48, 20);
            this.radTabImport.TabIndex = 32;
            this.radTabImport.Text = "טאב";
            this.radTabImport.UseVisualStyleBackColor = true;
            // 
            // radPipeImport
            // 
            this.radPipeImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPipeImport.AutoSize = true;
            this.radPipeImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPipeImport.Location = new System.Drawing.Point(335, 88);
            this.radPipeImport.Name = "radPipeImport";
            this.radPipeImport.Size = new System.Drawing.Size(46, 20);
            this.radPipeImport.TabIndex = 31;
            this.radPipeImport.Text = "פייפ";
            this.radPipeImport.UseVisualStyleBackColor = true;
            // 
            // radCommaImport
            // 
            this.radCommaImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radCommaImport.AutoSize = true;
            this.radCommaImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCommaImport.Location = new System.Drawing.Point(331, 109);
            this.radCommaImport.Name = "radCommaImport";
            this.radCommaImport.Size = new System.Drawing.Size(50, 20);
            this.radCommaImport.TabIndex = 30;
            this.radCommaImport.Text = "פסיק";
            this.radCommaImport.UseVisualStyleBackColor = true;
            // 
            // radOtherImport
            // 
            this.radOtherImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radOtherImport.AutoSize = true;
            this.radOtherImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radOtherImport.Location = new System.Drawing.Point(333, 130);
            this.radOtherImport.Name = "radOtherImport";
            this.radOtherImport.Size = new System.Drawing.Size(48, 20);
            this.radOtherImport.TabIndex = 29;
            this.radOtherImport.Text = "אחר";
            this.radOtherImport.UseVisualStyleBackColor = true;
            // 
            // txtDelimiterImport
            // 
            this.txtDelimiterImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDelimiterImport.Location = new System.Drawing.Point(311, 128);
            this.txtDelimiterImport.MaxLength = 1;
            this.txtDelimiterImport.Name = "txtDelimiterImport";
            this.txtDelimiterImport.Size = new System.Drawing.Size(18, 22);
            this.txtDelimiterImport.TabIndex = 28;
            // 
            // btnSelectionImport
            // 
            this.btnSelectionImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectionImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSelectionImport.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSelectionImport.Image = global::Pulsar.Properties.Resources.icon_pan;
            this.btnSelectionImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectionImport.Location = new System.Drawing.Point(69, 334);
            this.btnSelectionImport.Name = "btnSelectionImport";
            this.btnSelectionImport.Size = new System.Drawing.Size(64, 47);
            this.btnSelectionImport.TabIndex = 26;
            this.btnSelectionImport.Text = "בחר";
            this.btnSelectionImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectionImport.UseVisualStyleBackColor = true;
            this.btnSelectionImport.Click += new System.EventHandler(this.btnSelectionImport_Click);
            // 
            // btnExitImport
            // 
            this.btnExitImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExitImport.BackgroundImage = global::Pulsar.Properties.Resources.exit;
            this.btnExitImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExitImport.Location = new System.Drawing.Point(15, 334);
            this.btnExitImport.Name = "btnExitImport";
            this.btnExitImport.Size = new System.Drawing.Size(48, 47);
            this.btnExitImport.TabIndex = 27;
            this.btnExitImport.UseVisualStyleBackColor = true;
            this.btnExitImport.Click += new System.EventHandler(this.btnExitImport_Click);
            // 
            // btnImportInbox
            // 
            this.btnImportInbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportInbox.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportInbox.Image = global::Pulsar.Properties.Resources.import;
            this.btnImportInbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportInbox.Location = new System.Drawing.Point(452, 647);
            this.btnImportInbox.Name = "btnImportInbox";
            this.btnImportInbox.Size = new System.Drawing.Size(59, 47);
            this.btnImportInbox.TabIndex = 155;
            this.btnImportInbox.Text = "יבא";
            this.btnImportInbox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImportInbox.UseVisualStyleBackColor = true;
            this.btnImportInbox.Click += new System.EventHandler(this.btnImportInbox_Click);
            // 
            // txtMisparProyect
            // 
            this.txtMisparProyect.AllowNegative = false;
            this.txtMisparProyect.Disabled = false;
            this.txtMisparProyect.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtMisparProyect.Location = new System.Drawing.Point(116, 544);
            this.txtMisparProyect.MaxLength = 30;
            this.txtMisparProyect.Name = "txtMisparProyect";
            this.txtMisparProyect.Size = new System.Drawing.Size(100, 20);
            this.txtMisparProyect.TabIndex = 170;
            // 
            // dtpTo
            // 
            this.dtpTo.AutoComplete0 = true;
            this.dtpTo.BackColor = System.Drawing.Color.Transparent;
            this.dtpTo.DateType = Pulsar.DateTimeControl.eDateType.MonthYear;
            this.dtpTo.Jumping = false;
            this.dtpTo.Location = new System.Drawing.Point(116, 520);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(47, 20);
            this.dtpTo.TabIndex = 169;
            this.dtpTo.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // dtpFrom
            // 
            this.dtpFrom.AutoComplete0 = true;
            this.dtpFrom.BackColor = System.Drawing.Color.Transparent;
            this.dtpFrom.DateType = Pulsar.DateTimeControl.eDateType.MonthYear;
            this.dtpFrom.Jumping = true;
            this.dtpFrom.Location = new System.Drawing.Point(116, 494);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(47, 20);
            this.dtpFrom.TabIndex = 168;
            this.dtpFrom.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnScan.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScan.Image = global::Pulsar.Properties.Resources.scanner24;
            this.btnScan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.Location = new System.Drawing.Point(517, 647);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(74, 47);
            this.btnScan.TabIndex = 171;
            this.btnScan.Text = "סרוק";
            this.btnScan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnTochenTnua
            // 
            this.btnTochenTnua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTochenTnua.Enabled = false;
            this.btnTochenTnua.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTochenTnua.Image = global::Pulsar.Properties.Resources.registration2_16x16;
            this.btnTochenTnua.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTochenTnua.Location = new System.Drawing.Point(321, 647);
            this.btnTochenTnua.Name = "btnTochenTnua";
            this.btnTochenTnua.Size = new System.Drawing.Size(125, 47);
            this.btnTochenTnua.TabIndex = 172;
            this.btnTochenTnua.Text = "תוכן תנועה";
            this.btnTochenTnua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTochenTnua.UseVisualStyleBackColor = true;
            this.btnTochenTnua.Click += new System.EventHandler(this.btnTochenTnua_Click);
            // 
            // frmHaklada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1016, 706);
            this.Controls.Add(this.btnTochenTnua);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.txtMisparProyect);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(label3);
            this.Controls.Add(label12);
            this.Controls.Add(label13);
            this.Controls.Add(this.btnImportInbox);
            this.Controls.Add(this.grpImport);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grpSearchFiles);
            this.Controls.Add(this.btnSearchDocument);
            this.Controls.Add(this.dtpTarichAcherNew);
            this.Controls.Add(this.dtpTarichMismachNew);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(companyVATToLabel);
            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(companyNameLabel);
            this.Controls.Add(this.attachmnentTextBox);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtpTarichAcher);
            this.Controls.Add(this.txtSchumPaturMmam);
            this.Controls.Add(this.txtMaam);
            this.Controls.Add(this.txtMisparMismach);
            this.Controls.Add(this.txtSchumHaMaam);
            this.Controls.Add(this.txtLefniMaam);
            this.Controls.Add(this.txtActionDetails);
            this.Controls.Add(this.dtpTarichMismach);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtSchumKolelMaam);
            this.Controls.Add(attachmnentLabel);
            this.Controls.Add(label4);
            this.Controls.Add(label5);
            this.Controls.Add(label6);
            this.Controls.Add(label7);
            this.Controls.Add(label8);
            this.Controls.Add(label9);
            this.Controls.Add(maamLabel);
            this.Controls.Add(label10);
            this.Controls.Add(label11);
            this.Controls.Add(this.btnCompanies);
            this.Controls.Add(this.companyIDComboBox);
            this.Controls.Add(this.cmbActionType);
            this.Controls.Add(this.companyVATComboBox);
            this.Controls.Add(this.companyNameComboBox);
            this.Controls.Add(this.lvwOutbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHaklada";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "יצירת תנועה יוצאת";
            this.Load += new System.EventHandler(this.frmHaklada_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.localInfoProtocolDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hakladaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListBindingSource)).EndInit();
            this.grpSearchFiles.ResumeLayout(false);
            this.grpImport.ResumeLayout(false);
            this.grpImport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource hakladaBindingSource;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.HakladaTableAdapter hakladaTableAdapter;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.CompaniesTableAdapter companiesTableAdapter;
        private System.Windows.Forms.BindingSource companiesBindingSource;
        private System.Windows.Forms.ComboBox companyNameComboBox;
        private System.Windows.Forms.ComboBox companyVATComboBox;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.ActionsListTableAdapter actionsListTableAdapter;
        private System.Windows.Forms.BindingSource actionsListBindingSource;
        private System.Windows.Forms.ComboBox companyIDComboBox;
        private System.Windows.Forms.ComboBox cmbActionType;
        private System.Windows.Forms.Button btnCompanies;
        private System.Windows.Forms.TextBox attachmnentTextBox;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DateTimePicker dtpTarichAcher;
        private Pulsar.NumericTextBox txtSchumPaturMmam;
        private Pulsar.NumericTextBox txtMaam;
        private Pulsar.NumericTextBox txtMisparMismach;
        private Pulsar.NumericTextBox txtSchumHaMaam;
        private System.Windows.Forms.TextBox txtLefniMaam;
        private System.Windows.Forms.TextBox txtActionDetails;
        private System.Windows.Forms.DateTimePicker dtpTarichMismach;
        private System.Windows.Forms.Button btnBrowse;
        private Pulsar.NumericTextBox txtSchumKolelMaam;
        private System.Windows.Forms.OpenFileDialog ofdAttachment;
        private App_Data.LocalInfoProtocolDataSet localInfoProtocolDataSet;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnNew;
        private DateTimeControl dtpTarichMismachNew;
        private DateTimeControl dtpTarichAcherNew;
        private System.Windows.Forms.Button btnSearchDocument;
        private System.Windows.Forms.ListBox lstResultSearchFiles;
        private System.Windows.Forms.GroupBox grpSearchFiles;
        private System.Windows.Forms.Button btnExitSearchFiles;
        private System.Windows.Forms.Button btnSelection;
        private System.Windows.Forms.ListView lvwOutbox;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnDelete;
        private TitleBar titleBar1;
        public System.Windows.Forms.GroupBox grpImport;
        private System.Windows.Forms.RadioButton radFixedSizeImport;
        private System.Windows.Forms.RadioButton radTabImport;
        private System.Windows.Forms.RadioButton radPipeImport;
        private System.Windows.Forms.RadioButton radCommaImport;
        private System.Windows.Forms.RadioButton radOtherImport;
        private System.Windows.Forms.TextBox txtDelimiterImport;
        private System.Windows.Forms.Button btnSelectionImport;
        private System.Windows.Forms.Button btnExitImport;
        private System.Windows.Forms.Button btnImportInbox;
        private System.Windows.Forms.RadioButton radMokup;
        private NumericTextBox txtMisparProyect;
        private DateTimeControl dtpTo;
        private DateTimeControl dtpFrom;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnImportHelp;
        private System.Windows.Forms.Button btnUpdateImport;
        private System.Windows.Forms.ListView lvwImportFileds;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnTochenTnua;

    }
}