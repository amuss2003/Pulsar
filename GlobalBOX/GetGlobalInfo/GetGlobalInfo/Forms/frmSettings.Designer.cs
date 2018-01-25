namespace Pulsar.Forms
{
    partial class frmSettings
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
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label lblMinuets;
            this.cmbIntervalMailCheck = new System.Windows.Forms.NumericUpDown();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpdateCompany = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radAdirim = new System.Windows.Forms.RadioButton();
            this.radRivhit = new System.Windows.Forms.RadioButton();
            this.radZoomPriority = new System.Windows.Forms.RadioButton();
            this.radHashavshevt = new System.Windows.Forms.RadioButton();
            label10 = new System.Windows.Forms.Label();
            lblMinuets = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIntervalMailCheck)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(464, 27);
            label10.Name = "label10";
            label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            label10.Size = new System.Drawing.Size(80, 15);
            label10.TabIndex = 62;
            label10.Text = "בדוק נתונים כל:";
            // 
            // lblMinuets
            // 
            lblMinuets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblMinuets.AutoSize = true;
            lblMinuets.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblMinuets.Location = new System.Drawing.Point(378, 27);
            lblMinuets.Name = "lblMinuets";
            lblMinuets.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lblMinuets.Size = new System.Drawing.Size(31, 15);
            lblMinuets.TabIndex = 63;
            lblMinuets.Text = "דקות";
            // 
            // cmbIntervalMailCheck
            // 
            this.cmbIntervalMailCheck.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cmbIntervalMailCheck.Location = new System.Drawing.Point(411, 26);
            this.cmbIntervalMailCheck.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.cmbIntervalMailCheck.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cmbIntervalMailCheck.Name = "cmbIntervalMailCheck";
            this.cmbIntervalMailCheck.ReadOnly = true;
            this.cmbIntervalMailCheck.Size = new System.Drawing.Size(47, 20);
            this.cmbIntervalMailCheck.TabIndex = 61;
            this.cmbIntervalMailCheck.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cmbIntervalMailCheck.ValueChanged += new System.EventHandler(this.cmbIntervalMailCheck_ValueChanged);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackgroundImage = global::Pulsar.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(527, 327);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(45, 47);
            this.btnExit.TabIndex = 64;
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnUpdateCompany
            // 
            this.btnUpdateCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateCompany.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnUpdateCompany.Image = global::Pulsar.Properties.Resources.edit_icon;
            this.btnUpdateCompany.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateCompany.Location = new System.Drawing.Point(12, 325);
            this.btnUpdateCompany.Name = "btnUpdateCompany";
            this.btnUpdateCompany.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnUpdateCompany.Size = new System.Drawing.Size(70, 47);
            this.btnUpdateCompany.TabIndex = 65;
            this.btnUpdateCompany.Text = "עדכן";
            this.btnUpdateCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateCompany.UseVisualStyleBackColor = true;
            this.btnUpdateCompany.Click += new System.EventHandler(this.btnUpdateCompany_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(label10);
            this.groupBox1.Controls.Add(lblMinuets);
            this.groupBox1.Controls.Add(this.cmbIntervalMailCheck);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 311);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "הגדרות פרמטרים ב Pulsar ";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radAdirim);
            this.groupBox2.Controls.Add(this.radRivhit);
            this.groupBox2.Controls.Add(this.radZoomPriority);
            this.groupBox2.Controls.Add(this.radHashavshevt);
            this.groupBox2.Location = new System.Drawing.Point(378, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 121);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ממשקים";
            // 
            // radAdirim
            // 
            this.radAdirim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radAdirim.AutoSize = true;
            this.radAdirim.Location = new System.Drawing.Point(86, 24);
            this.radAdirim.Name = "radAdirim";
            this.radAdirim.Size = new System.Drawing.Size(66, 17);
            this.radAdirim.TabIndex = 3;
            this.radAdirim.TabStop = true;
            this.radAdirim.Text = "משרדית";
            this.radAdirim.UseVisualStyleBackColor = true;
            // 
            // radRivhit
            // 
            this.radRivhit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radRivhit.AutoSize = true;
            this.radRivhit.Location = new System.Drawing.Point(86, 93);
            this.radRivhit.Name = "radRivhit";
            this.radRivhit.Size = new System.Drawing.Size(65, 17);
            this.radRivhit.TabIndex = 2;
            this.radRivhit.TabStop = true;
            this.radRivhit.Text = "ריווחית";
            this.radRivhit.UseVisualStyleBackColor = true;
            // 
            // radZoomPriority
            // 
            this.radZoomPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radZoomPriority.AutoSize = true;
            this.radZoomPriority.Location = new System.Drawing.Point(60, 70);
            this.radZoomPriority.Name = "radZoomPriority";
            this.radZoomPriority.Size = new System.Drawing.Size(91, 17);
            this.radZoomPriority.TabIndex = 1;
            this.radZoomPriority.TabStop = true;
            this.radZoomPriority.Text = "זום פריוריטי";
            this.radZoomPriority.UseVisualStyleBackColor = true;
            // 
            // radHashavshevt
            // 
            this.radHashavshevt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radHashavshevt.AutoSize = true;
            this.radHashavshevt.Location = new System.Drawing.Point(80, 47);
            this.radHashavshevt.Name = "radHashavshevt";
            this.radHashavshevt.Size = new System.Drawing.Size(71, 17);
            this.radHashavshevt.TabIndex = 0;
            this.radHashavshevt.TabStop = true;
            this.radHashavshevt.Text = "חשבשבת";
            this.radHashavshevt.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 386);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUpdateCompany);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSettings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmParameters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbIntervalMailCheck)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown cmbIntervalMailCheck;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUpdateCompany;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radAdirim;
        private System.Windows.Forms.RadioButton radRivhit;
        private System.Windows.Forms.RadioButton radZoomPriority;
        private System.Windows.Forms.RadioButton radHashavshevt;
    }
}