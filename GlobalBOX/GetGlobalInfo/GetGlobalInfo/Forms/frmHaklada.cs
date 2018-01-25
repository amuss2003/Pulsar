using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pulsar;
using Pulsar.Classes;
using System.Collections;
using System.IO;

namespace Pulsar
{
    public partial class frmHaklada : Form
    {
        public CompanyInfo Company_Info { get; set; }
        public int SelectedCountryIndex { get; set; }
        public String CountryID { get; set; }
        public String CompanyVAT { get; set; }
        public String ReadCode { get; set; }
        public String WriteCode { get; set; }
        public String Maam { get; set; }
        public Parser parser { get; set; }
        public DBLayer dblayer = null;
        public Dictionary<int, SugTnua> ActionsListDictionary = new Dictionary<int, SugTnua>();

        public frmHaklada()
        {
            InitializeComponent();
            shadowBrush = new SolidBrush(Color.FromArgb(49, 106, 197));
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void companyNameComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoComplete(this.companyNameComboBox, e, true);
        }

        // AutoComplete
        public void AutoComplete(ComboBox cb, System.Windows.Forms.KeyPressEventArgs e)
        {
            AutoComplete(cb, e, false);
        }

        public void AutoComplete(ComboBox cb, System.Windows.Forms.KeyPressEventArgs e, bool blnLimitToList)
        {
            string strFindStr = "";

            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }

            int intIdx = -1;

            // Search the string in the ComboBox list.

            intIdx = cb.FindString(strFindStr);

            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
            {
                e.Handled = blnLimitToList;
            }

        }

        private void frmHaklada_Load(object sender, EventArgs e)
        {
            dblayer = new DBLayer(Application.StartupPath + @"\App_Data\");
            dblayer.Current_Company_Info = Company_Info;

            dblayer.ReadShuratHakladaList(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, ActionsListDictionary);
            FillData();


            txtMaam.Text = Maam.ToString();
            dtpTarichMismachNew.Value = DateTime.Now;
            dtpTarichAcherNew.Value = DateTime.Now;
            titleBar1.Company_Info = Company_Info;
            PrepareIniFileName();
        }

        private void FillData()
        {
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.ActionsList' table. You can move, or remove it, as needed.
            this.actionsListTableAdapter.Fill(this.localInfoProtocolDataSet.ActionsList);
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.Haklada' table. You can move, or remove it, as needed.
            this.hakladaTableAdapter.Fill(this.localInfoProtocolDataSet.Haklada);

            FillCompanies();
            foreach (System.Data.DataRow dr in this.localInfoProtocolDataSet.ActionsList.Rows)
            {
                ComboboxItem itemID = new ComboboxItem();
                itemID.Text = dr.ItemArray[1].ToString();
                itemID.Value = dr.ItemArray[0].ToString();
                cmbActionType.Items.Add(itemID);
            }

            if (companyIDComboBox.Items.Count > 1)
            {
                companyIDComboBox.SelectedIndex = 0;
            }
            else
            {
                //AddCompany();
                if (companyIDComboBox.Items.Count == 1)
                {
                    MessageBox.Show(@"רשימת עסקים למשלוח לא קיימת," + Environment.NewLine + "יש להזין לפחות עסק אחת להמשך עבודה תקין!", "הקמת עסק", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBusinessIndexTable();
                    this.Close();
                }
                else
                {
                    companyIDComboBox.SelectedIndex = 0;
                }
            }
        }

        private void AddCompany()
        {
            frmBusinessIndex frm = new frmBusinessIndex();
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.CountryID = Company_Info.CompanyCountryID.ToString();//CountryID;
            frm.ShowDialog();
            FillCompanies();
        }

        private void FillCompanies()
        {
            // TODO: This line of code loads data into the 'localInfoProtocolDataSet.Companies' table. You can move, or remove it, as needed.
            //this.companiesTableAdapter.Fill(this.localInfoProtocolDataSet.Companies);

            companyIDComboBox.Items.Clear();
            companyNameComboBox.Items.Clear();
            companyVATComboBox.Items.Clear();

            ComboboxItem cmb_item = new ComboboxItem();
            cmb_item.Text = "";
            cmb_item.Value = "";
            companyIDComboBox.Items.Add(cmb_item);
            companyNameComboBox.Items.Add(cmb_item);
            companyVATComboBox.Items.Add(cmb_item);

            foreach (ArrayList item in dblayer.ReadCompaniesList())
            {
                ComboboxItem itemID = new ComboboxItem();
                itemID.Text = item[0].ToString();
                itemID.Value = item[0].ToString();
                companyIDComboBox.Items.Add(itemID);

                ComboboxItem itemCompanyName = new ComboboxItem();
                itemCompanyName.Text = item[1].ToString();
                itemCompanyName.Value = item[0].ToString();
                companyNameComboBox.Items.Add(itemCompanyName);

                ComboboxItem itemVAT = new ComboboxItem();
                itemVAT.Text = item[3].ToString();
                itemVAT.Value = item[0].ToString();
                companyVATComboBox.Items.Add(itemVAT);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                txtActionDetails.Text = txtActionDetails.Text.Replace("\r\n", " ").Trim();
                lvwOutbox.SelectedIndices.Clear();
                dblayer.AddHakladaRecord(CreateShuratHaklada());
                //this.hakladaTableAdapter.Fill(this.localInfoProtocolDataSet.Haklada);
                dblayer.ReadShuratHakladaList(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, ActionsListDictionary);
                CleanForm();
                companyIDComboBox.Focus();
            }
        }

        private bool ValidateForm()
        {
            if (txtSchumPaturMmam.Text.Trim() == "")
                txtSchumPaturMmam.Text = "0";

            if (cmbActionType.SelectedIndex == -1)
            {
                cmbActionType.Focus();
                cmbActionType.BackColor = Color.Pink;
                return false;
            }
            else
                cmbActionType.BackColor = Color.White;

            //if (txtMisparMismach.Text == "")
            //{
            //    txtMisparMismach.Focus();
            //    txtMisparMismach.BackColor = Color.Pink;
            //    return false;
            //}
            //else
            //    txtMisparMismach.BackColor = Color.White;

            if (txtSchumKolelMaam.Text == "")
            {
                txtSchumKolelMaam.Focus();
                txtSchumKolelMaam.BackColor = Color.Pink;
                return false;
            }
            else
                txtSchumKolelMaam.BackColor = Color.White;

            return true;
        }

        private ShuratHaklada CreateShuratHaklada()
        {
            ShuratHaklada shurat_haklada = new ShuratHaklada();

            //shurat_haklada.TransactionGUID = Guid.NewGuid().ToString();
            shurat_haklada.CompanyID = Int32.Parse(((ComboboxItem)(companyIDComboBox.SelectedItem)).Value.ToString());
            shurat_haklada.ActionCode = Int32.Parse(((ComboboxItem)(cmbActionType.SelectedItem)).Value.ToString());
            shurat_haklada.ActionDetails = txtActionDetails.Text;
            shurat_haklada.MisparMismach = txtMisparMismach.GetInt();
            shurat_haklada.TarichMismach = dtpTarichMismachNew.Value; // dtpTarichMismach.Value;
            shurat_haklada.TarichAcher = dtpTarichAcherNew.Value;  // dtpTarichAcher.Value;
            shurat_haklada.AhuzHaMaam = Double.Parse(txtMaam.Text);
            shurat_haklada.SchumPaturMaam = Double.Parse(txtSchumPaturMmam.Text);
            shurat_haklada.SchumMaam = Double.Parse(txtSchumHaMaam.Text);
            shurat_haklada.SchumKolelMaam = Double.Parse(txtSchumKolelMaam.Text);
            shurat_haklada.Attachment = attachmnentTextBox.Text;
            shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID;
            shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT;
            shurat_haklada.LeTkufaMe = dtpFrom.Value;
            shurat_haklada.LeTkufaMe = dtpTo.Value;
            shurat_haklada.MisparProyect = txtMisparProyect.Text;

            return shurat_haklada;
        }

        private void CleanForm()
        {
            //companyIDComboBox.SelectedIndex = 0;
            companyNameComboBox.SelectedIndex = -1;
            cmbActionType.SelectedIndex = 0;
            txtMisparMismach.Text = "";
            dtpTarichMismachNew.Value = DateTime.Now;
            dtpTarichAcherNew.Value = DateTime.Now;
            txtActionDetails.Text = "";
            txtSchumPaturMmam.Text = "";
            txtSchumKolelMaam.Text = "";
            attachmnentTextBox.Text = "";
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSchumKolelMaam_TextChanged(object sender, EventArgs e)
        {
            double kollelMaam = txtSchumKolelMaam.GetDouble();
            double maam = Convert.ToDouble(Maam);
            double schumLefniMaam = Math.Round(kollelMaam / (1 + (maam / 100)), 2);
            txtLefniMaam.Text = schumLefniMaam.ToString();
            txtSchumHaMaam.Text = (kollelMaam - schumLefniMaam).ToString();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //ofdAttachment.InitialDirectory = "c:\\";
            ofdAttachment.FileName = "";
            ofdAttachment.Filter = "txt files (*.txt)|*.txt|pdf files (*.pdf)|*.pdf";
            ofdAttachment.FilterIndex = 2;
            ofdAttachment.RestoreDirectory = true;

            DialogResult result = ofdAttachment.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                attachmnentTextBox.Text = ofdAttachment.FileName;
            }
        }

        private void companyIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAllCombos(companyIDComboBox.SelectedIndex);
        }

        private void companyNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAllCombos(companyNameComboBox.SelectedIndex);
        }

        private void companyVATComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAllCombos(companyVATComboBox.SelectedIndex);
        }

        private void cmbActionType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SetAllCombos(int SelectedIndex)
        {
            companyIDComboBox.SelectedIndex = SelectedIndex;
            companyNameComboBox.SelectedIndex = SelectedIndex;
            companyVATComboBox.SelectedIndex = SelectedIndex;
        }

        //private void hakladaDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    //SelectedRows[0].DataBoundItem
        //    //List<DataRow> dgv = GetSelected(hakladaDataGridView);
        //    //if (hakladaDataGridView.CurrentCell != null)
        //    //if (hakladaDataGridView.CurrentRow != null)
        //    if (hakladaDataGridView.CurrentCell != null)
        //    {
        //        //this.Text = hakladaDataGridView.CurrentCell.RowIndex.ToString() + " => " + hakladaDataGridView.CurrentRow.Index.ToString();
        //        this.Text = hakladaDataGridView.CurrentRow.Cells[0].Value.ToString();
        //    }

        //    if (hakladaDataGridView.SelectedRows.Count > 0)
        //    {
        //        this.Text = hakladaDataGridView.SelectedRows[0].Cells[0].Value.ToString();
        //    }
        //}

        private List<DataRow> GetSelected(DataGridView dataGridView)
        {
            List<DataRow> list = new List<DataRow>();
            //If the dataGridView selected rows are null
            if (dataGridView.SelectedRows == null || dataGridView.SelectedRows.Count == 0)
            {
                //Return list
                return list;
            }
            foreach (DataGridViewRow dataGridViewRow in dataGridView.SelectedRows)
            {
                //Declare a null DataRow
                DataRow dataRow = null;
                try
                {
                    //Declare a DataRowView(DataRowView)
                    DataRowView dataRowView = (DataRowView)dataGridViewRow.DataBoundItem;
                    dataRow = dataRowView.Row;
                }
                //Catch the exception
                catch (Exception ex)
                {
                    //Write the error message
                    System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                }
                //If the row is null
                if (dataRow == null)
                {
                    //Continue
                    continue;
                }
                //Add a row(DataRow) to list
                list.Add(dataRow);
            }
            //Return list
            return list;
        }

        private void lvwOutbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwOutbox.SelectedIndices.Count == 1)
            {
                ShuratHaklada shurat_haklada = (ShuratHaklada)lvwOutbox.Items[lvwOutbox.SelectedIndices[0]].Tag;

                SetComboItemByValue(companyIDComboBox, shurat_haklada.CompanyID);
                //companyIDComboBox.SelectedValue = shurat_haklada.CompanyID;
                SugTnua sug_tnua = ActionsListDictionary[shurat_haklada.ActionCode];
                //SetComboItemByValue2(cmbActionType, shurat_haklada.ActionCode);
                SetComboItemByText2(cmbActionType, sug_tnua.ActionTypeIN);
                //cmbActionType.Text = sug_tnua.ActionTypeOUT;// shurat_haklada.ActionCode;
                txtActionDetails.Text = shurat_haklada.ActionDetails;
                txtMisparMismach.Text = shurat_haklada.MisparMismach.ToString();
                dtpTarichMismachNew.Value = shurat_haklada.TarichMismach;
                dtpTarichAcherNew.Value = shurat_haklada.TarichAcher;
                txtMaam.Text = shurat_haklada.AhuzHaMaam.ToString();
                txtSchumPaturMmam.Text = shurat_haklada.SchumPaturMaam.ToString();
                txtSchumHaMaam.Text = shurat_haklada.SchumMaam.ToString();
                txtSchumKolelMaam.Text = shurat_haklada.SchumKolelMaam.ToString();
                attachmnentTextBox.Text = shurat_haklada.Attachment;
                //shurat_haklada.CompamyInfoCountryID = Convert.ToInt32(CountryID);
                //shurat_haklada.CompamyInfoVAT = CompanyVAT;
            }

            btnUpdate.Enabled = (lvwOutbox.SelectedIndices.Count == 1);
            btnDelete.Enabled = btnUpdate.Enabled;
            btnTochenTnua.Enabled = btnUpdate.Enabled;
        }

        private void SetComboItemByValue2(ComboBox cmb, int value)
        {
            for (int i = 1; i < cmb.Items.Count; i++)
            {
                if (((ComboboxItem)(cmb.Items[i])).Value.ToString() != "")
                {
                    if (value == Int32.Parse(((ComboboxItem)cmb.Items[i]).Value.ToString()))
                    {
                        cmb.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void SetComboItemByText2(ComboBox cmb, String value)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (((ComboboxItem)(cmb.Items[i])).Value.ToString() != "")
                {
                    //if (value == ((ComboboxItem)cmb.Items[i]).Text)
                    if (value == cmb.Items[i].ToString())
                    {
                        cmb.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        private void SetComboItemByValue(ComboBox cmb, int value)
        {
            for (int i = 1; i < cmb.Items.Count; i++)
            {
                if (((ComboboxItem)(cmb.Items[i])).Value.ToString() != "")
                {
                    if (value == Int32.Parse(((ComboboxItem)(cmb.Items[i])).Value.ToString()))
                    {
                        cmb.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                ShuratHaklada shurat_haklada = CreateShuratHaklada();

                shurat_haklada.TransactionGUID = ((ShuratHaklada)lvwOutbox.Items[lvwOutbox.SelectedIndices[0]].Tag).TransactionGUID;
                dblayer.UpdateShuratHaklada(shurat_haklada);
                dblayer.ReadShuratHakladaList(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, ActionsListDictionary);
                CleanForm();
            }
        }

        private void txtSchumPaturMmam_Leave(object sender, EventArgs e)
        {
            if (txtSchumPaturMmam.Text.Trim() == "")
            {
                txtSchumPaturMmam.Text = "0";
            }
        }

        private void btnCompanies_Click(object sender, EventArgs e)
        {
            //AddCompany();            
            LoadBusinessIndexTable();
            FillData();
        }

        private void LoadBusinessIndexTable()
        {
            frmBusinessIndexTable frm = new frmBusinessIndexTable();
            frm.parser = parser;
            frm.Company_Info = Company_Info;
            frm.CountryID = Company_Info.CompanyCountryID.ToString();
            frm.SelectedCountryIndex = SelectedCountryIndex;
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            CleanForm();
            lvwOutbox.SelectedIndices.Clear();
        }

        private void btnSearchDocument_Click(object sender, EventArgs e)
        {
            if (Company_Info.FilesSearch != null)
            {
                try
                {
                    string[] files = Directory.GetFiles(Company_Info.FilesSearch, "*" + txtMisparMismach.Text.Trim() + "*.*");
                    if (files.Length == 1)
                    {
                        attachmnentTextBox.Text = files[0].ToString();
                    }
                    else
                    {
                        lstResultSearchFiles.Items.Clear();
                        foreach (String file in files)
                        {
                            lstResultSearchFiles.Items.Add(Path.GetFileName(file));
                        }
                        grpSearchFiles.Visible = true;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Folder not decleared!" + Environment.NewLine + "Folder not found!.", "Folder Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnExitSearchFiles_Click(object sender, EventArgs e)
        {
            grpSearchFiles.Visible = false;
        }

        private void btnSelection_Click(object sender, EventArgs e)
        {
            attachmnentTextBox.Text = Company_Info.FilesSearch + @"\" + lstResultSearchFiles.Text;
            grpSearchFiles.Visible = false;
        }

        public SolidBrush shadowBrush { get; set; }
        private int _lockColumnIndex = 0;

        private void lvwOutbox_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            lvwOutbox.Invalidate();
        }

        private void lvwOutbox_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == _lockColumnIndex)
            {
                //Keep the width not changed.
                e.NewWidth = this.lvwOutbox.Columns[e.ColumnIndex].Width;
                //Cancel the event.
                e.Cancel = true;
            }
        }

        private void lvwOutbox_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }
        private void lvwOutbox_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            //if ((e.State & ListViewItemStates.Selected) != 0)
            //{
            //    if (lvwOutbox.SelectedIndices.Count > 0)
            //    {
            //        if (lvwOutbox.SelectedIndices.Contains(e.ItemIndex))
            //        {
            if (((e.State & ListViewItemStates.Focused) != 0) &&
                ((e.State & ListViewItemStates.Selected) != 0)
               )
            {
                if (lvwOutbox.SelectedIndices.Count > 0)
                {
                    // Draw the background and focus rectangle for a selected item.
                    Rectangle rect = new Rectangle(e.Bounds.X + e.Item.ImageList.Images[0].Width * 2, e.Bounds.Y, e.Bounds.Width - e.Item.ImageList.Images[0].Width * 2, e.Bounds.Height);
                    e.Graphics.FillRectangle(shadowBrush, rect);
                    //e.DrawFocusRectangle();
                }
            }

            //else
            //{
            //    // Draw the background for an unselected item. 
            //    using (LinearGradientBrush brush =
            //        new LinearGradientBrush(e.Bounds, Color.Orange,
            //        Color.Maroon, LinearGradientMode.Horizontal))
            //    {
            //        e.Graphics.FillRectangle(brush, e.Bounds);
            //    }
            //}

            // Draw the item text for views other than the Details view. 
            if (lvwOutbox.View != View.Details)
            {
                e.DrawText();
            }
            //e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            ////if ((e.State & ListViewItemStates.Selected) != 0)
            ////{
            ////    if (lvwOutbox.SelectedIndices.Count > 0)
            ////    {
            ////        if (lvwOutbox.SelectedIndices.Contains(e.ItemIndex))
            ////        {
            //if (((e.State & ListViewItemStates.Focused) != 0) &&
            //        ((e.State & ListViewItemStates.Selected) != 0)
            //       )
            //{
            //    if (lvwOutbox.SelectedIndices.Count > 0)
            //    {
            //        // Draw the background and focus rectangle for a selected item.
            //        Rectangle rect = new Rectangle(e.Bounds.X + e.Item.ImageList.Images[0].Width * 2, e.Bounds.Y, e.Bounds.Width - e.Item.ImageList.Images[0].Width * 2, e.Bounds.Height);
            //        e.Graphics.FillRectangle(shadowBrush, rect);
            //        //e.DrawFocusRectangle();
            //    }
            //}

            //// Draw the item text for views other than the Details view. 
            //if (lvwOutbox.View != View.Details)
            //{
            //    e.DrawText();
            //}
        }

        private void lvwOutbox_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.Left;

            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default 
                // to Left if it has not been set to Center or Right. 
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        flags = TextFormatFlags.HorizontalCenter;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        flags = TextFormatFlags.Right;
                        break;
                }

                if (((e.ItemState & ListViewItemStates.Focused) != 0) &&
                    ((e.ItemState & ListViewItemStates.Selected) != 0) &&
                    (lvwOutbox.SelectedIndices.Count > 0)
                  )
                {
                    //e.DrawBackground();
                    e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.White, e.Bounds, sf);
                }
                else
                {
                    e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.Black, e.Bounds, sf);
                }

                if (e.ColumnIndex == 0)
                {
                    // Draw the subitem text in red to highlight it. 
                    //e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.Red, e.Bounds, sf);
                    if (e.Item.ImageIndex != -1)
                    {
                        e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);
                    }

                    ShuratHaklada shurat_haklada = (ShuratHaklada)lvwOutbox.Items[e.ItemIndex].Tag;
                    if (File.Exists(shurat_haklada.Attachment))
                    {
                        Point pt = new Point(e.SubItem.Bounds.Location.X + e.Item.ImageList.Images[0].Width, e.SubItem.Bounds.Location.Y);
                        e.Graphics.DrawImage(e.Item.ImageList.Images[5], pt);
                    }
                }
                else
                {
                    // Draw normal text for a subitem with a nonnegative  
                    // or nonnumerical value.
                    //e.DrawText(flags);
                }
            }
            //TextFormatFlags flags = TextFormatFlags.Left;

            //using (StringFormat sf = new StringFormat())
            //{
            //    // Store the column text alignment, letting it default 
            //    // to Left if it has not been set to Center or Right. 
            //    switch (e.Header.TextAlign)
            //    {
            //        case HorizontalAlignment.Center:
            //            sf.Alignment = StringAlignment.Center;
            //            flags = TextFormatFlags.HorizontalCenter;
            //            break;
            //        case HorizontalAlignment.Right:
            //            sf.Alignment = StringAlignment.Far;
            //            flags = TextFormatFlags.Right;
            //            break;
            //    }

            //    // Unless the item is selected, draw the standard  
            //    // background to make it stand out from the gradient. 
            //    //if ((e.ItemState & ListViewItemStates.Selected) == 0)
            //    //{
            //    //    e.DrawBackground();
            //    //}

            //    //if (e.ColumnIndex == 0)
            //    //{
            //    if (((e.ItemState & ListViewItemStates.Focused) != 0) &&
            //         ((e.ItemState & ListViewItemStates.Selected) != 0) &&
            //         (lvwOutbox.SelectedIndices.Count > 0)
            //        )
            //    {
            //        // Draw the subitem text in red to highlight it. 
            //        e.Graphics.DrawString(e.SubItem.Text, lvwOutbox.Font, Brushes.Red, e.Bounds, sf);
            //        if (e.Item.ImageIndex != -1)
            //        {
            //            e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);
            //        }

            //        ShuratHaklada shurat_haklada = (ShuratHaklada)lvwOutbox.Items[e.ItemIndex].Tag;
            //        if (File.Exists(shurat_haklada.Attachment))
            //        {
            //            Point pt = new Point(e.SubItem.Bounds.Location.X + e.Item.ImageList.Images[0].Width, e.SubItem.Bounds.Location.Y);
            //            e.Graphics.DrawImage(e.Item.ImageList.Images[5], pt);
            //        }
            //    }
            //    else
            //    {
            //        // Draw normal text for a subitem                    
            //        //e.DrawText(flags);
            //    }
            //}
        }

        private void lstResultSearchFiles_DoubleClick(object sender, EventArgs e)
        {
            attachmnentTextBox.Text = Company_Info.FilesSearch + @"\" + lstResultSearchFiles.Text;
            grpSearchFiles.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            if (lvwOutbox.SelectedIndices.Count == 1)
            {
                if (MessageBox.Show("Delete Record ", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    ShuratHaklada shurat_haklada = (ShuratHaklada)lvwOutbox.Items[lvwOutbox.SelectedIndices[0]].Tag;
                    dblayer.DeleteOutboxHaklada(shurat_haklada.TransactionGUID);
                    dblayer.ReadShuratHakladaList(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, ActionsListDictionary);
                }
            }
            btnDelete.Enabled = true;
        }

        private void btnImportInbox_Click(object sender, EventArgs e)
        {
            LoadData();
            grpImport.Visible = true;
        }

        private void btnSelectionImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Company_Info.DataPath != null)
                {
                    if (Directory.Exists(Company_Info.DataPath))
                    {
                        ofdAttachment.InitialDirectory = Company_Info.DataPath;
                    }
                }

                ofdAttachment.FileName = "";
                ofdAttachment.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|dat files (*.dat)|*.dat|all files (*.*)|*.*";
                ofdAttachment.FilterIndex = 0;
                ofdAttachment.RestoreDirectory = true;

                DialogResult result = ofdAttachment.ShowDialog();

                if (result == DialogResult.OK) // Test result.
                {
                    ExternalDataManager externalDataManager = new ExternalDataManager();
                    if (radFixedSizeImport.Checked)
                    {
                        externalDataManager.ImportHaklada(ofdAttachment.FileName, Company_Info, dblayer);
                    }
                    else if (radMokup.Checked)
                    {
                        externalDataManager.ImportHakladaMokup(ofdAttachment.FileName, Company_Info, dblayer);
                    }
                    else
                    {
                        string delimiter = "";

                        if (radTabImport.Checked)
                            delimiter = "\t";
                        else if (radPipeImport.Checked)
                            delimiter = "|";
                        else if (radCommaImport.Checked)
                            delimiter = ",";
                        else if (radOtherImport.Checked)
                            delimiter = txtDelimiterImport.Text;

                        //is space can be a delimiter?
                        //externalDataManager.ImportHaklada(ofdAttachment.FileName, Company_Info, dblayer, delimiter);                        
                        externalDataManager.Company_Info = Company_Info;
                        externalDataManager.ImportHakladaMokupINI(ofdAttachment.FileName, Company_Info, dblayer, delimiter);
                    }

                    dblayer.ReadShuratHakladaList(lvwOutbox, Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, ActionsListDictionary);
                    CleanForm();
                    companyIDComboBox.Focus();
                }
            }
            catch (Exception ex)
            {
            }

            grpImport.Visible = false;
        }

        private void btnExitImport_Click(object sender, EventArgs e)
        {
            grpImport.Visible = false;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            MainFrame frm = new MainFrame();
            frm.Company_Info = Company_Info;
            frm.ShowDialog();
        }

        private void btnImportHelp_Click(object sender, EventArgs e)
        {
            frmDataStructure frm = new frmDataStructure();
            frm.parser = parser;
            frm.dblayer = dblayer;
            frm.Company_Info = Company_Info;
            frm.ShowDialog();
        }

        private void btnUpdateImport_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void LoadData()
        {
            if (iniFile == null)
            {
                PrepareIniFileName();
            }

            foreach (ListViewItem lviFld in lvwImportFileds.Items)
            {
                lviFld.Text = iniFile.IniReadValue(Company_Info.CompanySerialNumber + " Import Outbox Structure", lviFld.Tag.ToString());
            }
        }

        public void Save()
        {
            foreach (ListViewItem lviFld in lvwImportFileds.Items)
            {
                iniFile.IniWriteValue(Company_Info.CompanySerialNumber + " Import Outbox Structure", lviFld.Tag.ToString(), lviFld.Text);
            }
        }

        public void SaveNewSystem()
        {
            PrepareIniFileName();
            int i = 1;
            foreach (ListViewItem lviFld in lvwImportFileds.Items)
            {
                iniFile.IniWriteValue(Company_Info.CompanySerialNumber + " Import Outbox Structure", lviFld.Tag.ToString(), (i++).ToString());
            }
        }

        public String IniFileName { get; set; }
        public IniFile iniFile { get; set; }

        private void PrepareIniFileName()
        {
            IniFileName = Path.GetFileName(Application.ExecutablePath);

            FileInfo fi = new FileInfo(IniFileName);
            fi = new FileInfo(fi.FullName);
            IniFileName = fi.Name;

            IniFileName = IniFileName.Substring(0, IniFileName.Length - ".exe".Length) + ".ini";
            iniFile = new IniFile(Application.StartupPath + @"\" + IniFileName);
        }

        private void grpImport_Enter(object sender, EventArgs e)
        {

        }

        private void btnTochenTnua_Click(object sender, EventArgs e)
        {
            EditTochenTnua();
        }

        private void EditTochenTnua()
        {
            if (lvwOutbox.SelectedIndices.Count == 1)
            {
                ShuratHaklada shurat_haklada = (ShuratHaklada)lvwOutbox.Items[lvwOutbox.SelectedIndices[0]].Tag;
                frmHakladaTochenTnua frm = new frmHakladaTochenTnua();
                frm.Company_Info = Company_Info;
                frm.dblayer = dblayer;
                frm.parser = parser;
                frm.TransactionGUID = shurat_haklada.TransactionGUID;
                frm.ShowDialog();
            }
        }

        private void lvwOutbox_DoubleClick(object sender, EventArgs e)
        {
            EditTochenTnua();
        }

    }
}
