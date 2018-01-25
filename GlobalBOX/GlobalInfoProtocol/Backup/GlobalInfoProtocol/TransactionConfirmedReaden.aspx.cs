using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class TransactionConfirmedReaden : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];
            String TransactionGUID = Request["TransactionGUID"];

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((TransactionGUID != null) && (TransactionGUID != ""))
                {
                    Response.Write(dblayer.IsTransactionReaden(TransactionGUID).ToString().ToLower());
                }
            }
        }
    }
}