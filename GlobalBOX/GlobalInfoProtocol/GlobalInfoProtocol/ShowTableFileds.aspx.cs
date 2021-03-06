﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class ShowTableFileds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String TableName = Request["TableName"];
            String LoginKey = Request["LoginKey"];
            String PageSize = Request["PageSize"];

            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));
            //dblayer.connectionString = Session["connectionString"].ToString();

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if (PageSize != null)
                {
                    dblayer.PageSize = Int32.Parse(PageSize);
                }
                else
                {
                    dblayer.PageSize = 10;
                }

                Response.Write(dblayer.ShowTableFileds(TableName));
            }
        }
    }
}