using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Pulsar
{
    public partial class ListViewEx : ListView
    {
        public SolidBrush ShadowBrush { get; set; }

        private int _lockColumnIndex = 0;

        public void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        public void DoubleBuffer()
        {
            SetDoubleBuffered(this);
        }

        public ListViewEx(IContainer container)
        {
            container.Add(this);
            ShadowBrush = new SolidBrush(Color.FromArgb(49, 106, 197));
            this.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvw_ColumnWidthChanged);
            this.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvw_ColumnWidthChanging);
            this.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvw_DrawColumnHeader);
            this.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvw_DrawItem);
            this.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvw_DrawSubItem);
            InitializeComponent();
        }

        private void lvw_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //ListViewItem lvi = (ListViewItem)((System.Windows.Forms.ListView)(sender)).Items[e.ItemIndex];
            //lvi.UseItemStyleForSubItems = false;

            //this.ForeColor = SystemColors.WindowText;
            //e.Item.ForeColor = Color.White;
            e.DrawBackground();
            //e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //ShadowBrush = new SolidBrush(Color.FromArgb(49, 106, 197));

            //if ((e.State & ListViewItemStates.Selected) != 0)
            //{
            //    if (this.SelectedIndices.Count > 0)
            //    {
            //        if (this.SelectedIndices.Contains(e.ItemIndex))
            //        {
            //            // Draw the background and focus rectangle for a selected item.
            //            Rectangle rect = new Rectangle(e.Bounds.X + e.Item.ImageList.Images[0].Width, e.Bounds.Y, e.Bounds.Width - e.Item.ImageList.Images[0].Width, e.Bounds.Height);
            //            e.Graphics.FillRectangle(ShadowBrush, rect);
            //            //e.DrawFocusRectangle();
            //        }
            //    }
            //}
            if (((e.State & ListViewItemStates.Focused) != 0) &&
                ((e.State & ListViewItemStates.Selected) != 0)
               )
            {
                if (this.SelectedIndices.Count > 0)
                {
                    // Draw the background and focus rectangle for a selected item.
                    Rectangle rect = new Rectangle(e.Bounds.X + e.Item.ImageList.Images[0].Width, e.Bounds.Y, e.Bounds.Width - e.Item.ImageList.Images[0].Width, e.Bounds.Height);
                    e.Graphics.FillRectangle(ShadowBrush, rect);
                }
            }
            //else
            //{
            //    //// Draw the background for an unselected item. 
            //    //using (LinearGradientBrush brush =
            //    //    new LinearGradientBrush(e.Bounds, Color.Orange,
            //    //    Color.Maroon, LinearGradientMode.Horizontal))
            //    //{
            //    //    e.Graphics.FillRectangle(brush, e.Bounds);
            //    //}
            //    SolidBrush brush = new SolidBrush(Color.FromArgb(49, 106, 197));
            //    e.Graphics.FillRectangle(brush, e.Bounds);
            //}

            // Draw the item text for views other than the Details view. 
            if (this.View != View.Details)
            {
                e.DrawText();
            }
            ////////////////////////////////////////////
            //e.DrawBackground();
            //if ((e.State & ListViewItemStates.Selected) == ListViewItemStates.Selected)
            //{
            //    Rectangle r = new Rectangle(e.Bounds.Left + 4, e.Bounds.Top, TextRenderer.MeasureText(e.Item.Text, e.Item.Font).Width, e.Bounds.Height);
            //    e.Graphics.FillRectangle(SystemBrushes.Highlight, r);
            //    e.Item.ForeColor = SystemColors.HighlightText;
            //}
            //else
            //{
            //    e.Item.ForeColor = SystemColors.WindowText;
            //}
            //e.DrawText();
            //e.DrawFocusRectangle();


            ////////////////////////////////////////////

            //shadowBrush = new SolidBrush(Color.FromArgb(49, 106, 197));
            //e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            //if ((e.State & ListViewItemStates.Selected) != 0)
            //{
            //    if (this.SelectedIndices.Count > 0)
            //    {
            //        if (this.SelectedIndices.Contains(e.ItemIndex))
            //        {
            //            // Draw the background and focus rectangle for a selected item.
            //            Rectangle rect = new Rectangle(e.Bounds.X + e.Item.ImageList.Images[0].Width, e.Bounds.Y, e.Bounds.Width - e.Item.ImageList.Images[0].Width, e.Bounds.Height);
            //            e.Graphics.FillRectangle(shadowBrush, rect);
            //            //e.DrawFocusRectangle();
            //        }
            //    }
            //}
            ////else
            ////{
            ////    // Draw the background for an unselected item. 
            ////    using (LinearGradientBrush brush =
            ////        new LinearGradientBrush(e.Bounds, Color.Orange,
            ////        Color.Maroon, LinearGradientMode.Horizontal))
            ////    {
            ////        e.Graphics.FillRectangle(brush, e.Bounds);
            ////    }
            ////}

            //// Draw the item text for views other than the Details view. 
            //if (this.View != View.Details)
            //{
            //    e.DrawText();
            //}

            //lvwInData.SuspendLayout();
            ////if (actionBuffer.ContainsKey(int.Parse(e.Item.Text)))
            ////{
            ////    if (currentProcess.Id.ToString() == e.Item.Text)
            ////    {
            ////        int c = currentProcess.ExitCode;
            ////        if (currentProcess.HasExited)
            ////        {
            ////            actionBuffer.Remove(int.Parse(e.Item.Text));
            ////            e.DrawBackground();
            ////        }
            ////    }
            ////    e.Graphics.FillRectangle(Brushes.Wheat, e.Bounds);
            ////    lvwInData.EndUpdate();
            ////    return;
            ////}

            //if (lvwInData.SelectedItems.Count > 0 && e.Item.Text == lvwInData.SelectedItems[0].Text)
            //{
            //    e.Graphics.FillRectangle(shadowBrush, e.Bounds);
            //    e.DrawFocusRectangle();
            //}
            //else
            //{
            //    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //}

            //lvwInData.ResumeLayout();
        }

        private void lvw_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            //((System.Windows.Forms.ListView)(sender)).Items[e.ItemIndex]
            //ListViewItem lvi = (ListViewItem)((System.Windows.Forms.ListView)(sender)).Items[e.ItemIndex];
            //lvi.UseItemStyleForSubItems = false;

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

                // Unless the item is selected, draw the standard  
                // background to make it stand out from the gradient. 

                if (((e.ItemState & ListViewItemStates.Focused) != 0) &&
                    ((e.ItemState & ListViewItemStates.Selected) != 0) &&
                    (this.SelectedIndices.Count > 0)
                   )
                {
                    //e.DrawBackground();
                    //e.Item.UseItemStyleForSubItems = false;
                    //e.Item.ForeColor = Color.White;
                    e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.White, e.Bounds, sf);
                }
                else
                {
                    //e.Item.ForeColor = SystemColors.WindowText;
                    e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.Black, e.Bounds, sf);
                }

                if (e.ColumnIndex == 0)
                {
                    //lvi.ForeColor = Color.White;
                    // Draw the subitem text in red to highlight it. 
                    //e.Graphics.DrawString(e.SubItem.Text, this.Font, Brushes.Red, e.Bounds, sf);
                    if (e.Item.ImageIndex != -1)
                    {
                        e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);
                    }
                }
                else
                {
                    // Draw normal text for a subitem with a nonnegative  
                    // or nonnumerical value.
                    //this.ForeColor = Color.Black;
                    //lvi.ForeColor = SystemColors.WindowText;

                    //e.DrawText(flags);
                    //e.DrawText(flags);
                }
            }
            //////////////////////////////////////////////////
            //if (e.ColumnIndex > 0)
            //{
            //    e.DrawBackground();

            //    string searchTerm = "Term";
            //    int index = e.SubItem.Text.IndexOf(searchTerm);

            //    if (index >= 0)
            //    {
            //        string sBefore = e.SubItem.Text.Substring(0, index);

            //        Size bounds = new Size(e.Bounds.Width, e.Bounds.Height);
            //        Size s1 = TextRenderer.MeasureText(e.Graphics, sBefore, this.Font, bounds);
            //        Size s2 = TextRenderer.MeasureText(e.Graphics, searchTerm, this.Font, bounds);

            //        Rectangle rect = new Rectangle(e.Bounds.X + s1.Width, e.Bounds.Y, s2.Width, e.Bounds.Height);

            //        e.Graphics.SetClip(e.Bounds);
            //        e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), rect);
            //        e.Graphics.ResetClip();
            //    }

            //    e.DrawText();
            //}

            //////////////////////////////////////////////////
            //TextFormatFlags flags = TextFormatFlags.Left;

            //using (StringFormat sf = new StringFormat())
            //{
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
            //        case HorizontalAlignment.Left:
            //            sf.Alignment = StringAlignment.Near;
            //            flags = TextFormatFlags.Left;
            //            break;
            //    }

            //    if ((e.ItemState & ListViewItemStates.Selected) == 0)
            //    {
            //        e.DrawBackground();
            //    }

            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.Item.ImageIndex != -1)
            //        {
            //            e.DrawBackground();
            //            e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], e.SubItem.Bounds.Location);
            //            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), e.SubItem.Bounds.Location.X + this.imageList1.Images[0].Width, e.SubItem.Bounds.Location.Y);
            //        }
            //    }
            //    else
            //    {
            //        //e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), e.SubItem.Bounds.Location.X + this.imageList1.Images[0].Width, e.SubItem.Bounds.Location.Y);
            //        e.DrawText(flags);
            //        //e.DrawText();
            //    }
            //}
        }

        private void lvw_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            //using (StringFormat sf = new StringFormat())
            //{
            //    // Store the column text alignment, letting it default
            //    // to Left if it has not been set to Center or Right.
            //    switch (e.Header.TextAlign)
            //    {
            //        case HorizontalAlignment.Center:
            //            sf.Alignment = StringAlignment.Center;
            //            break;
            //        case HorizontalAlignment.Right:
            //            sf.Alignment = StringAlignment.Far;
            //            break;
            //    }

            //    // Draw the standard header background.
            //    // e.DrawBackground();

            //    // Draw the header text.
            //    //using (Font headerFont = new Font("Courier New", 8, FontStyle.Regular))
            //    //{
            //    //    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
            //    //}

            //    e.Graphics.DrawString(e.Header.Text, lvwInData.Font, Brushes.Black, e.Bounds, sf);
            //}
            //return;
        }


        private void lvw_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            this.Invalidate();
        }

        private void lvw_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == _lockColumnIndex)
            {
                //Keep the width not changed.
                e.NewWidth = this.Columns[e.ColumnIndex].Width;
                //Cancel the event.
                e.Cancel = true;
            }
        }
    }
}
