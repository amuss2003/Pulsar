namespace GetGlobalInfo
{
    partial class DateTimeControl
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
            this.components = new System.ComponentModel.Container();
            this.txtBackground = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtDay = new GetGlobalInfo.NumericTextBox();
            this.txtMonth = new GetGlobalInfo.NumericTextBox();
            this.txtYear = new GetGlobalInfo.NumericTextBox();
            this.numericTextBox1 = new GetGlobalInfo.NumericTextBox();
            this.numericTextBox2 = new GetGlobalInfo.NumericTextBox();
            this.SuspendLayout();
            // 
            // txtBackground
            // 
            this.txtBackground.Location = new System.Drawing.Point(0, 0);
            this.txtBackground.Name = "txtBackground";
            this.txtBackground.Size = new System.Drawing.Size(64, 20);
            this.txtBackground.TabIndex = 0;
            this.txtBackground.TabStop = false;
            this.txtBackground.Enter += new System.EventHandler(this.txtBackground_Enter);
            // 
            // txtDay
            // 
            this.txtDay.AllowNegative = false;
            this.txtDay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDay.Disabled = false;
            this.txtDay.InputType = GetGlobalInfo.NumericTextBox.NumericType.IntegerInput;
            this.txtDay.Location = new System.Drawing.Point(2, 3);
            this.txtDay.MaxLength = 2;
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(12, 13);
            this.txtDay.TabIndex = 1;
            this.txtDay.Text = "99";
            this.txtDay.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtDay.Enter += new System.EventHandler(this.txt_Enter);
            this.txtDay.Validating += new System.ComponentModel.CancelEventHandler(this.txtDay_Validating);
            // 
            // txtMonth
            // 
            this.txtMonth.AllowNegative = false;
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMonth.Disabled = false;
            this.txtMonth.InputType = GetGlobalInfo.NumericTextBox.NumericType.IntegerInput;
            this.txtMonth.Location = new System.Drawing.Point(19, 3);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(12, 13);
            this.txtMonth.TabIndex = 2;
            this.txtMonth.Text = "99";
            this.txtMonth.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtMonth.Enter += new System.EventHandler(this.txt_Enter);
            this.txtMonth.Validating += new System.ComponentModel.CancelEventHandler(this.txtMonth_Validating);
            // 
            // txtYear
            // 
            this.txtYear.AllowNegative = false;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtYear.Disabled = false;
            this.txtYear.InputType = GetGlobalInfo.NumericTextBox.NumericType.IntegerInput;
            this.txtYear.Location = new System.Drawing.Point(36, 3);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(25, 13);
            this.txtYear.TabIndex = 3;
            this.txtYear.Text = "9999";
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            this.txtYear.Enter += new System.EventHandler(this.txt_Enter);
            this.txtYear.Leave += new System.EventHandler(this.txtYear_Leave);
            this.txtYear.Validating += new System.ComponentModel.CancelEventHandler(this.txtYear_Validating);
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.AllowNegative = false;
            this.numericTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericTextBox1.Disabled = false;
            this.numericTextBox1.InputType = GetGlobalInfo.NumericTextBox.NumericType.IntegerInput;
            this.numericTextBox1.Location = new System.Drawing.Point(14, 3);
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.Size = new System.Drawing.Size(5, 13);
            this.numericTextBox1.TabIndex = 4;
            this.numericTextBox1.Text = "/";
            this.numericTextBox1.Enter += new System.EventHandler(this.numericTextBox1_Enter);
            // 
            // numericTextBox2
            // 
            this.numericTextBox2.AllowNegative = false;
            this.numericTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericTextBox2.Disabled = false;
            this.numericTextBox2.InputType = GetGlobalInfo.NumericTextBox.NumericType.IntegerInput;
            this.numericTextBox2.Location = new System.Drawing.Point(31, 3);
            this.numericTextBox2.Name = "numericTextBox2";
            this.numericTextBox2.Size = new System.Drawing.Size(5, 13);
            this.numericTextBox2.TabIndex = 5;
            this.numericTextBox2.Text = "/";
            this.numericTextBox2.Enter += new System.EventHandler(this.numericTextBox2_Enter);
            // 
            // DateTimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtDay);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.numericTextBox1);
            this.Controls.Add(this.numericTextBox2);
            this.Controls.Add(this.txtBackground);
            this.Name = "DateTimeControl";
            this.Size = new System.Drawing.Size(109, 40);
            this.Resize += new System.EventHandler(this.DateTimeControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericTextBox txtDay;
        private NumericTextBox txtMonth;
        private NumericTextBox txtYear;
        private System.Windows.Forms.TextBox txtBackground;
        private NumericTextBox numericTextBox1;
        private NumericTextBox numericTextBox2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
