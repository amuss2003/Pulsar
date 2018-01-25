namespace Pulsar
{
    partial class frmActionType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActionType));
            this.localInfoProtocolDataSet = new Pulsar.App_Data.LocalInfoProtocolDataSet();
            this.actionsListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.actionsListTableAdapter = new Pulsar.App_Data.LocalInfoProtocolDataSetTableAdapters.ActionsListTableAdapter();
            this.tableAdapterManager = new Pulsar.App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager();
            this.actionsListBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actionsListBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.actionsListDataGridView = new System.Windows.Forms.DataGridView();
            this.titleBar1 = new Pulsar.TitleBar();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.localInfoProtocolDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListBindingNavigator)).BeginInit();
            this.actionsListBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // localInfoProtocolDataSet
            // 
            this.localInfoProtocolDataSet.DataSetName = "LocalInfoProtocolDataSet";
            this.localInfoProtocolDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // actionsListBindingSource
            // 
            this.actionsListBindingSource.DataMember = "ActionsList";
            this.actionsListBindingSource.DataSource = this.localInfoProtocolDataSet;
            // 
            // actionsListTableAdapter
            // 
            this.actionsListTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.ActionsListTableAdapter = this.actionsListTableAdapter;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CompaniesTableAdapter = null;
            this.tableAdapterManager.CompanyInfoTableAdapter = null;
            this.tableAdapterManager.HakladaTableAdapter = null;
            this.tableAdapterManager.InboxTableAdapter = null;
            this.tableAdapterManager.OutboxTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = Pulsar.App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // actionsListBindingNavigator
            // 
            this.actionsListBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.actionsListBindingNavigator.BindingSource = this.actionsListBindingSource;
            this.actionsListBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.actionsListBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.actionsListBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.actionsListBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.actionsListBindingNavigatorSaveItem});
            this.actionsListBindingNavigator.Location = new System.Drawing.Point(0, 465);
            this.actionsListBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.actionsListBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.actionsListBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.actionsListBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.actionsListBindingNavigator.Name = "actionsListBindingNavigator";
            this.actionsListBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.actionsListBindingNavigator.Size = new System.Drawing.Size(443, 25);
            this.actionsListBindingNavigator.TabIndex = 0;
            this.actionsListBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Visible = false;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Visible = false;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // actionsListBindingNavigatorSaveItem
            // 
            this.actionsListBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionsListBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("actionsListBindingNavigatorSaveItem.Image")));
            this.actionsListBindingNavigatorSaveItem.Name = "actionsListBindingNavigatorSaveItem";
            this.actionsListBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.actionsListBindingNavigatorSaveItem.Text = "Save Data";
            this.actionsListBindingNavigatorSaveItem.Click += new System.EventHandler(this.actionsListBindingNavigatorSaveItem_Click_1);
            // 
            // actionsListDataGridView
            // 
            this.actionsListDataGridView.AutoGenerateColumns = false;
            this.actionsListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.actionsListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.actionsListDataGridView.DataSource = this.actionsListBindingSource;
            this.actionsListDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.actionsListDataGridView.Location = new System.Drawing.Point(0, 27);
            this.actionsListDataGridView.Name = "actionsListDataGridView";
            this.actionsListDataGridView.RowHeadersVisible = false;
            this.actionsListDataGridView.Size = new System.Drawing.Size(443, 438);
            this.actionsListDataGridView.TabIndex = 1;
            // 
            // titleBar1
            // 
            this.titleBar1.Company_Info = null;
            this.titleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar1.Location = new System.Drawing.Point(0, 0);
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.titleBar1.Size = new System.Drawing.Size(443, 21);
            this.titleBar1.TabIndex = 113;
            this.titleBar1.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ActionCode";
            this.dataGridViewTextBoxColumn1.HeaderText = "ActionCode";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 110;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ActionTypeIN";
            this.dataGridViewTextBoxColumn2.HeaderText = "ActionTypeIN";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 110;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ActionTypeOUT";
            this.dataGridViewTextBoxColumn3.HeaderText = "ActionTypeOUT";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 110;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn4.HeaderText = "Description";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 110;
            // 
            // frmActionType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 490);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.actionsListDataGridView);
            this.Controls.Add(this.actionsListBindingNavigator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmActionType";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Action Type";
            this.Load += new System.EventHandler(this.frmActionType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.localInfoProtocolDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListBindingNavigator)).EndInit();
            this.actionsListBindingNavigator.ResumeLayout(false);
            this.actionsListBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actionsListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private App_Data.LocalInfoProtocolDataSet localInfoProtocolDataSet;
        private System.Windows.Forms.BindingSource actionsListBindingSource;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.ActionsListTableAdapter actionsListTableAdapter;
        private App_Data.LocalInfoProtocolDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator actionsListBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton actionsListBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView actionsListDataGridView;
        private TitleBar titleBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

    }
}