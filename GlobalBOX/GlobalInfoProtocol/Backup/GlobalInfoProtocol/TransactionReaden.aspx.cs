using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol
{
    public partial class TransactionReaden : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.AddToLogger(Server.MapPath("."), "TransactionReaden.aspx Request");
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];
            String WriteCode = Request["Write"];
            String TransactionGUID = Request["TransactionGUID"];
            Logger.AddToLogger(Server.MapPath("."), "TransactionReaden.aspx Parse Data");

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                Logger.AddToLogger(Server.MapPath("."), "TransactionReaden.aspx LoginKey");
                if ((TransactionGUID != null) && (TransactionGUID != ""))
                {
                    Logger.AddToLogger(Server.MapPath("."), "TransactionReaden.aspx TransactionGUID");
                    if ((WriteCode != null) && (WriteCode != ""))
                    {
                        Logger.AddToLogger(Server.MapPath("."), "TransactionReaden.aspx WriteCode");
                        Logger.AddToLogger(Server.MapPath("."), dblayer.UpdateTransaction(TransactionGUID, WriteCode));
                    }
                }
            }
        }
    }
}