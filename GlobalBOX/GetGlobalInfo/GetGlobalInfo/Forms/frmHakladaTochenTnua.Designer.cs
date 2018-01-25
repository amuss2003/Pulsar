namespace Pulsar
{
    partial class frmHakladaTochenTnua
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
            System.Windows.Forms.Label attachmnentLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.txtTeurParit = new System.Windows.Forms.TextBox();
            this.txtCodeParit = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lvwOutboxTochenTnua = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSchumShura = new Pulsar.NumericTextBox();
            this.txtMechirYechida = new Pulsar.NumericTextBox();
            this.txtKamutKlalit = new Pulsar.NumericTextBox();
            attachmnentLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // attachmnentLabel
            // 
            attachmnentLabel.AutoSize = true;
            attachmnentLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            attachmnentLabel.Location = new System.Drawing.Point(11, 39);
            attachmnentLabel.Name = "attachmnentLabel";
            attachmnentLabel.Size = new System.Drawing.Size(66, 16);
            attachmnentLabel.TabIndex = 77;
            attachmnentLabel.Text = "תאור פריט:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(11, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(58, 16);
            label1.TabIndex = 79;
            label1.Text = "קוד פריט:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(11, 65);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(70, 16);
            label4.TabIndex = 81;
            label4.Text = "כמות כללית:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(11, 91);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(71, 16);
            label2.TabIndex = 83;
            label2.Text = "מחיר יחידה:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(11, 117);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(67, 16);
            label3.TabIndex = 85;
            label3.Text = "סכום שורה:";
            // 
            // txtTeurParit
            // 
            this.txtTeurParit.Location = new System.Drawing.Point(84, 38);
            this.txtTeurParit.MaxLength = 150;
            this.txtTeurParit.Name = "txtTeurParit";
            this.txtTeurParit.Size = new System.Drawing.Size(222, 20);
            this.txtTeurParit.TabIndex = 2;
            // 
            // txtCodeParit
            // 
            this.txtCodeParit.Location = new System.Drawing.Point(84, 12);
            this.txtCodeParit.MaxLength = 20;
            this.txtCodeParit.Name = "txtCodeParit";
            this.txtCodeParit.Size = new System.Drawing.Size(124, 20);
            this.txtCodeParit.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Pulsar.Properties.Resources.icon_delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(253, 374);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 47);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "מחק";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNew.Image = global::Pulsar.Properties.Resources.icon_new;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(16, 374);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(64, 47);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "חדש";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnUpdate.Image = global::Pulsar.Properties.Resources.edit_icon;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(178, 374);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(69, 47);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "עדכן";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAdd.Image = global::Pulsar.Properties.Resources.icon_add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(87, 374);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(84, 47);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "הוספה";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImage = global::Pulsar.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(770, 376);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 47);
            this.btnExit.TabIndex = 10;
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // lvwOutboxTochenTnua
            // 
            this.lvwOutboxTochenTnua.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwOutboxTochenTnua.AutoArrange = false;
            this.lvwOutboxTochenTnua.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader10});
            this.lvwOutboxTochenTnua.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwOutboxTochenTnua.FullRowSelect = true;
            this.lvwOutboxTochenTnua.HideSelection = false;
            this.lvwOutboxTochenTnua.Location = new System.Drawing.Point(325, 12);
            this.lvwOutboxTochenTnua.MultiSelect = false;
            this.lvwOutboxTochenTnua.Name = "lvwOutboxTochenTnua";
            this.lvwOutboxTochenTnua.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lvwOutboxTochenTnua.Size = new System.Drawing.Size(491, 351);
            this.lvwOutboxTochenTnua.TabIndex = 11;
            this.lvwOutboxTochenTnua.UseCompatibleStateImageBehavior = false;
            this.lvwOutboxTochenTnua.View = System.Windows.Forms.View.Details;
            this.lvwOutboxTochenTnua.SelectedIndexChanged += new System.EventHandler(this.lvwOutboxTochenTnua_SelectedIndexChanged);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "#";
            this.columnHeader8.Width = 36;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "קוד פריט";
            this.columnHeader13.Width = 76;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "תאור פריט";
            this.columnHeader14.Width = 111;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "כמות כללית";
            this.columnHeader15.Width = 76;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "מחיר יחידה";
            this.columnHeader16.Width = 72;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "סכום שורה";
            this.columnHeader10.Width = 109;
            // 
            // txtSchumShura
            // 
            this.txtSchumShura.AllowNegative = false;
            this.txtSchumShura.Disabled = false;
            this.txtSchumShura.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtSchumShura.Location = new System.Drawing.Point(84, 116);
            this.txtSchumShura.MaxLength = 13;
            this.txtSchumShura.Name = "txtSchumShura";
            this.txtSchumShura.ReadOnly = true;
            this.txtSchumShura.Size = new System.Drawing.Size(124, 20);
            this.txtSchumShura.TabIndex = 5;
            // 
            // txtMechirYechida
            // 
            this.txtMechirYechida.AllowNegative = false;
            this.txtMechirYechida.Disabled = false;
            this.txtMechirYechida.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtMechirYechida.Location = new System.Drawing.Point(84, 90);
            this.txtMechirYechida.MaxLength = 13;
            this.txtMechirYechida.Name = "txtMechirYechida";
            this.txtMechirYechida.Size = new System.Drawing.Size(124, 20);
            this.txtMechirYechida.TabIndex = 4;
            this.txtMechirYechida.Leave += new System.EventHandler(this.txtKamut_Leave);
            // 
            // txtKamutKlalit
            // 
            this.txtKamutKlalit.AllowNegative = false;
            this.txtKamutKlalit.Disabled = false;
            this.txtKamutKlalit.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtKamutKlalit.Location = new System.Drawing.Point(84, 64);
            this.txtKamutKlalit.MaxLength = 13;
            this.txtKamutKlalit.Name = "txtKamutKlalit";
            this.txtKamutKlalit.Size = new System.Drawing.Size(124, 20);
            this.txtKamutKlalit.TabIndex = 3;
            this.txtKamutKlalit.Leave += new System.EventHandler(this.txtKamut_Leave);
            // 
            // frmHakladaTochenTnua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 433);
            this.Controls.Add(this.lvwOutboxTochenTnua);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtSchumShura);
            this.Controls.Add(label3);
            this.Controls.Add(this.txtMechirYechida);
            this.Controls.Add(label2);
            this.Controls.Add(this.txtKamutKlalit);
            this.Controls.Add(label4);
            this.Controls.Add(this.txtCodeParit);
            this.Controls.Add(label1);
            this.Controls.Add(this.txtTeurParit);
            this.Controls.Add(attachmnentLabel);
            this.Name = "frmHakladaTochenTnua";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmHakladaTochenTnua_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTeurParit;
        private System.Windows.Forms.TextBox txtCodeParit;
        private NumericTextBox txtKamutKlalit;
        private NumericTextBox txtMechirYechida;
        private NumericTextBox txtSchumShura;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListView lvwOutboxTochenTnua;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader10;


    }
}