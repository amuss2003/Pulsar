using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pulsar.Classes;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pulsar.Forms
{
    public partial class frmReport : Form
    {
        public Parser parser { get; set; }
        public CompanyInfo Company_Info { get; set; }
        public DBLayer dblayer = null;

        public frmReport()
        {
            InitializeComponent();
            SetDoubleBuffered(lvwReport);
        }

        public static void SetDoubleBuffered(Control ctrl)
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

            aProp.SetValue(ctrl, true, null);
        }

        public bool bLoading { get; set; }

        private void frmReport_Load(object sender, EventArgs e)
        {
            bLoading = true;
            FillCombos();
            SetColumns();
            FillData();
            titleBar1.Company_Info = Company_Info;
            bLoading = false;
        }

        private void FillCombos()
        {
            //chartSeries.ChartType = SeriesChartType.Line;
            String[] names = Enum.GetNames(typeof(SeriesChartType));
            foreach (String name in names)
            {
                cmbGraphStyle.Items.Add(name);
            }
            cmbGraphStyle.Text = "Column";

            for (int i = 2012; i <= DateTime.Now.Year; i++)
            {
                cmbYears.Items.Add(i);
            }
            cmbYears.SelectedIndex = cmbYears.Items.Count - 1;
        }

        private void FillData()
        {
            if (bLoading)
                return;

            lvwReport.SuspendLayout();
            lvwReport.Items.Clear();

            DateTime recordsMaxDate = dblayer.FillReportDataMaxDate(cmbYears.Text);
            int recordsMaxMonth = recordsMaxDate.Month + 1;
            ArrayList records = dblayer.FillReportData(cmbYears.Text);
            // Adds a new group that has a left-aligned header
            for (int i = 0; i < records.Count; i++)
            {
                ArrayList record = (ArrayList)records[i];
                String GroupName = record[3].ToString();
                String NameOfCompany = record[0].ToString();
                DateTime LetkufaUd = DateTime.Parse(record[1].ToString(), new System.Globalization.CultureInfo("en-AU", false));
                Double SchumKolelelMaam = Double.Parse(record[2].ToString());

                ListViewGroup lvg = new ListViewGroup(GroupName);
                lvg.Name = GroupName;
                if (!lvwReport.Groups.Contains(lvg))
                {
                    lvwReport.Groups.Add(lvg);
                }

                //if (lvwReport.Items.Find(CompanyName.Replace(" ", "_"), true).Length == 0)
                ListViewItem foundItem = null;
                try
                {
                    foundItem = lvwReport.FindItemWithText(NameOfCompany, false, 0, true);
                }
                catch (Exception)
                {
                }

                if (foundItem == null)
                {
                    foundItem = lvwReport.Items.Add(NameOfCompany);
                }

                foundItem.UseItemStyleForSubItems = false;
                foundItem.Group = lvg;

                for (int j = 1; j < lvwReport.Columns.Count; j++)
                {
                    ListViewItem.ListViewSubItem lviSbi = foundItem.SubItems.Add("0.00");
                    lviSbi.Tag = (Double)0;
                    lviSbi.ForeColor = Color.LightGray;
                }

                if (cmbYears.Text == LetkufaUd.Year.ToString()) //if (DateTime.Now.Year == LetkufaUd.Year)
                {
                    Double monthValue = (Double)foundItem.SubItems[LetkufaUd.Month + 1].Tag;
                    foundItem.SubItems[LetkufaUd.Month + 1].Tag = (monthValue + SchumKolelelMaam);
                    foundItem.SubItems[LetkufaUd.Month + 1].Text = string.Format("{0:N2}", (Double)foundItem.SubItems[LetkufaUd.Month + 1].Tag);

                    if (((Double)foundItem.SubItems[LetkufaUd.Month + 1].Tag) > 0)
                    {
                        foundItem.SubItems[LetkufaUd.Month + 1].ForeColor = Color.Black;
                    }
                }
            }

            foreach (ListViewItem lvi in lvwReport.Items)
            {
                Double yearValue = 0;

                for (int i = 1; i < 12; i++)
                {
                    yearValue += (Double)lvi.SubItems[i].Tag;
                }

                lvi.SubItems[13].Tag = yearValue;
                lvi.SubItems[14].Tag = yearValue / 12;

                lvi.SubItems[13].Text = string.Format("{0:N2}", yearValue);
                lvi.SubItems[14].Text = string.Format("{0:N2}", yearValue / recordsMaxMonth);


                if (yearValue > 0)
                {
                    lvi.SubItems[13].ForeColor = Color.Black;
                    lvi.SubItems[14].ForeColor = Color.Black;
                }
            }
            lvwReport.ResumeLayout();
        }

        private void SetColumns()
        {
            lvwReport.Columns.Add("שם חברה", 148);

            lvwReport.Columns.Add("ינואר");
            lvwReport.Columns.Add("פברואר");
            lvwReport.Columns.Add("מרץ");
            lvwReport.Columns.Add("אפריל");
            lvwReport.Columns.Add("מאי");
            lvwReport.Columns.Add("יוני");
            lvwReport.Columns.Add("יולי");
            lvwReport.Columns.Add("אוגוסט");
            lvwReport.Columns.Add("ספטמבר");
            lvwReport.Columns.Add("אוקטובר");
            lvwReport.Columns.Add("נובמבר");
            lvwReport.Columns.Add("דצמבר");

            lvwReport.Columns.Add("שנתי");
            lvwReport.Columns.Add("ממוצע");

            foreach (ColumnHeader item in lvwReport.Columns)
            {
                item.TextAlign = HorizontalAlignment.Right;
            }

            // Adds a new group that has a left-aligned header
            //lvwReport.Groups.Add(new ListViewGroup("List item text"));

            //// Adds a new group that has a left-aligned header
            //listView1.Groups.Add(new ListViewGroup("List item text", HorizontalAlignment.Left));

            //// Adds the first item to the first group
            //listView1.Items[0].Group = listView1.Groups[0];

            //// Removes the first group in the collection.
            //listView1.Groups.RemoveAt(0);
            //// Clears all groups.
            //listView1.Groups.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvwReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawChart();
        }

        private void DrawChart()
        {
            if (lvwReport.SelectedIndices.Count == 1)
            {
                ListViewItem lvi = lvwReport.Items[lvwReport.SelectedIndices[0]];

                if (chart1.ChartAreas.Count > 0)
                    chart1.ChartAreas.Clear();

                if (chart1.Series.Count > 0)
                    chart1.Series.RemoveAt(0);

                chart1.ChartAreas.Add("");
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                chart1.ChartAreas[0].AxisX.LabelStyle.IntervalOffset = 1;
                //chart1.Series[0].Points.Add(new DataPoint(4, 4) { AxisLabel = "your label" });

                //chart1.Series["Series1"].IsValueShownAsLabel = false;
                //Random random = new Random();
                //for (int pointIndex = 0; pointIndex < 12; pointIndex++)
                //{
                //    chart1.Series["Series1"].Points.AddY(random.Next(45, 2095));
                //}
                DataTable dtSales = new DataTable();
                dtSales.Columns.Add("Month", typeof(string));
                //dtSales.Columns.Add("PreviousYear", typeof(int));
                dtSales.Columns.Add("ThisYear", typeof(int));
                Random rndm = new Random();
                //dtSales.Rows.Add("Dec", rndm.Next(1000, 10001), rndm.Next(1000, 10001));
                for (int i = 1; i < 13; i++)
                {
                    dtSales.Rows.Add(lvwReport.Columns[i].Text, (Double)lvi.SubItems[i].Tag);
                }
                //dtSales.Rows.Add("Jan", (Double)lvi.SubItems[i].Tag, null);
                //dtSales.Rows.Add("Feb", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("Mar", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("Apr", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("May", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("June", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("July", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("Aug", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("Sept", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("Oct", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("Nov", rndm.Next(1000, 10001), null);
                //dtSales.Rows.Add("Dec", rndm.Next(1000, 10001), null);

                //DataGridView1.DataSource = dtSales.DefaultView;
                //chart1.Series.RemoveAt(0);

                try
                {
                    chart1.DataBindTable(dtSales.DefaultView, "Month");
                }
                catch (Exception)
                {
                }

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;

                try
                {
                    chart1.Series["ThisYear"].IsValueShownAsLabel = true;
                }
                catch (Exception)
                {
                }
                try
                {
                    chart1.Series["ThisYear"]["LabelStyle"] = "Top";
                }
                catch (Exception)
                {
                }


                //try
                //{
                //    chart1.Series["PreviousYear"].IsValueShownAsLabel = true;
                //}
                //catch (Exception)
                //{
                //}

                //try
                //{
                //    chart1.Series["PreviousYear"]["LabelStyle"] = "Top";
                //}
                //catch (Exception)
                //{
                //}


                try
                {
                    chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
                }
                catch (Exception)
                {
                }
                chart1.Series[0].ChartType = (SeriesChartType)cmbGraphStyle.SelectedIndex;
            }
        }

        private void cmbGraphStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = (SeriesChartType)cmbGraphStyle.SelectedIndex;
        }

        private void cmbYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillData();
            DrawChart();
        }
    }
}



//lvwReport.Items[0].UseItemStyleForSubItems = false;
//lvwReport.Items[0].SubItems[1].ForeColor = Color.Red;
