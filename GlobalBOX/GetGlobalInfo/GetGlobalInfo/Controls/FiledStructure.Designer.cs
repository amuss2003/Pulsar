namespace Pulsar
{
    partial class FiledStructure
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
            this.lblFieldName = new System.Windows.Forms.Label();
            this.txtLength = new Pulsar.NumericTextBox();
            this.txtPos = new Pulsar.NumericTextBox();
            this.SuspendLayout();
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.BackColor = System.Drawing.SystemColors.Control;
            this.lblFieldName.Location = new System.Drawing.Point(3, 3);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(63, 13);
            this.lblFieldName.TabIndex = 0;
            this.lblFieldName.Text = "Filed Name:";
            this.lblFieldName.Click += new System.EventHandler(this.lblFiledName_Click);
            // 
            // txtLength
            // 
            this.txtLength.AllowNegative = false;
            this.txtLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLength.BackColor = System.Drawing.SystemColors.Control;
            this.txtLength.Disabled = false;
            this.txtLength.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtLength.Location = new System.Drawing.Point(108, 0);
            this.txtLength.MaxLength = 3;
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(32, 20);
            this.txtLength.TabIndex = 2;
            this.txtLength.Text = "0";
            this.txtLength.TextChanged += new System.EventHandler(this.txtLength_TextChanged);
            this.txtLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_KeyUp);
            this.txtLength.Leave += new System.EventHandler(this.txtLength_Leave);
            // 
            // txtPos
            // 
            this.txtPos.AllowNegative = false;
            this.txtPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPos.BackColor = System.Drawing.SystemColors.Control;
            this.txtPos.Disabled = false;
            this.txtPos.InputType = Pulsar.NumericTextBox.NumericType.IntegerInput;
            this.txtPos.Location = new System.Drawing.Point(70, 0);
            this.txtPos.MaxLength = 3;
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(32, 20);
            this.txtPos.TabIndex = 1;
            this.txtPos.Text = "0";
            this.txtPos.TextChanged += new System.EventHandler(this.txtPos_TextChanged);
            this.txtPos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_KeyUp);
            this.txtPos.Leave += new System.EventHandler(this.txtPos_Leave);
            // 
            // FiledStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.lblFieldName);
            this.Controls.Add(this.txtPos);
            this.Name = "FiledStructure";
            this.Size = new System.Drawing.Size(142, 20);
            this.Load += new System.EventHandler(this.FiledStructure_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericTextBox txtLength;
        private System.Windows.Forms.Label lblFieldName;
        private NumericTextBox txtPos;
    }
}
