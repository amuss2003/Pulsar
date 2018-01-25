using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class ShowTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String TableName = Request["TableName"];
            String Where = Request["Where"];
            String SQL_REQ = Request["SQL"];
            String LoginKey = Request["LoginKey"];
            String PageSize = Request["PageSize"];

            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));
            
            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {            
                if (SQL_REQ != null)
                {                    
                    if (SQL_REQ.ToLower().StartsWith("delete from "))
                    {                     
                       Response.Write(dblayer.ExecuteNonQuery(SQL_REQ));
                       Response.Write(dblayer.ErrorList);
                    }
                }

                String SQL = "SELECT * From " + TableName;
                if (Where != null)
                {
                    SQL += " Where " + Where;
                }

                if (PageSize != null)
                {
                    GridView1.PageSize = Int32.Parse(PageSize);
                }

                AccessDataSource1.SelectCommand = SQL;

                String num_of_records = dblayer.GetSQLString("SELECT count(*) From [" + TableName + "]");

                if (num_of_records == "0")
                {
                    Response.Redirect("ShowTableFileds.aspx?TableName=" + TableName + "&LoginKey=" + LoginKey);
                }
                else
                {
                    lblTableInfo.Text = "[" + TableName + "] Records: " + num_of_records;
                }
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int i = 0;
                // loop all data rows
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    // check all cells in one row
                    foreach (Control control in cell.Controls)
                    {
                        // Must use LinkButton here instead of ImageButton
                        // if you are having Links (not images) as the command button.
                        LinkButton button = control as LinkButton;
                        if (button != null)
                        {
                            button.Text = ("[" + ((i++) + 1) + "]</br>" + button.Text);
                        }
                    }
                }

                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{
                //    e.Row.Cells[i].Text = ((i + 1) + " " + e.Row.Cells[i].Text);
                //}
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    try
                    {
                        //if (cell.Text.IndexOf(" ") != -1)
                        if ((cell.Text.IndexOf(":") != -1) && (cell.Text.IndexOf("/") != -1) && (cell.Text.IndexOf(" ") != -1))
                        {
                            cell.Text = Convert.ToDateTime(cell.Text).ToShortDateString();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}