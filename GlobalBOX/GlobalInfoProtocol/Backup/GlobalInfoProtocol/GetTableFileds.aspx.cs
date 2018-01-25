﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class GetTableFileds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String TableName = Request["TableName"];
            String LoginKey = Request["LoginKey"];

            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                Response.Write(dblayer.GetTableFileds(TableName));
            }
        }
    }
}