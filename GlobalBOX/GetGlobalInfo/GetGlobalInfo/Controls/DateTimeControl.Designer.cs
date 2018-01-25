namespace Pulsar
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
            this.txtDay = new Pulsar.NumericTextBox();
            this.txtMonth = new Pulsar.NumericTextBox();
            this.txtYear = new Pulsar.NumericTextBox();
            this.txtDayMonthSeperator = new Pulsar.NumericTextBox();
            this.txtMonthYearSeperator = new Pulsar.NumericTextBox();
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
            this.txtDay.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtDay.Location = new System.Drawing.Point(2, 3);
            this.txtDay.MaxLength = 2;
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(12, 13);
            this.txtDay.TabIndex = 1;
            this.txtDay.Text = "99";
            this.txtDay.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtDay.Enter += new System.EventHandler(this.txt_Enter);
            this.txtDay.Leave += new System.EventHandler(this.txt_Leave);
            this.txtDay.Validating += new System.ComponentModel.CancelEventHandler(this.txtDay_Validating);
            // 
            // txtMonth
            // 
            this.txtMonth.AllowNegative = false;
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMonth.Disabled = false;
            this.txtMonth.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtMonth.Location = new System.Drawing.Point(19, 3);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(12, 13);
            this.txtMonth.TabIndex = 2;
            this.txtMonth.Text = "99";
            this.txtMonth.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtMonth.Enter += new System.EventHandler(this.txt_Enter);
            this.txtMonth.Leave += new System.EventHandler(this.txt_Leave);
            this.txtMonth.Validating += new System.ComponentModel.CancelEventHandler(this.txtMonth_Validating);
            // 
            // txtYear
            // 
            this.txtYear.AllowNegative = false;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtYear.Disabled = false;
            this.txtYear.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
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
            // txtDayMonthSeperator
            // 
            this.txtDayMonthSeperator.AllowNegative = false;
            this.txtDayMonthSeperator.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDayMonthSeperator.Disabled = false;
            this.txtDayMonthSeperator.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtDayMonthSeperator.Location = new System.Drawing.Point(14, 3);
            this.txtDayMonthSeperator.Name = "txtDayMonthSeperator";
            this.txtDayMonthSeperator.Size = new System.Drawing.Size(5, 13);
            this.txtDayMonthSeperator.TabIndex = 4;
            this.txtDayMonthSeperator.Text = "/";
            this.txtDayMonthSeperator.Enter += new System.EventHandler(this.numericTextBox1_Enter);
            // 
            // txtMonthYearSeperator
            // 
            this.txtMonthYearSeperator.AllowNegative = false;
            this.txtMonthYearSeperator.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMonthYearSeperator.Disabled = false;
            this.txtMonthYearSeperator.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtMonthYearSeperator.Location = new System.Drawing.Point(31, 3);
            this.txtMonthYearSeperator.Name = "txtMonthYearSeperator";
            this.txtMonthYearSeperator.Size = new System.Drawing.Size(5, 13);
            this.txtMonthYearSeperator.TabIndex = 5;
            this.txtMonthYearSeperator.Text = "/";
            this.txtMonthYearSeperator.Enter += new System.EventHandler(this.numericTextBox2_Enter);
            // 
            // DateTimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtDay);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtDayMonthSeperator);
            this.Controls.Add(this.txtMonthYearSeperator);
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
        private NumericTextBox txtDayMonthSeperator;
        private NumericTextBox txtMonthYearSeperator;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
