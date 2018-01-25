using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol
{
    public partial class GetTermUse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];
            String CMailBoxInstallID = Request["CMailBoxInstallID"];

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((CMailBoxInstallID != null) && (CMailBoxInstallID != ""))
                {
                    CMailBox cMailBox = dblayer.GetCMailBox(CMailBoxInstallID);
                    Response.Write(dblayer.ErrorList);
                    if (cMailBox != null)
                    {
                        Response.Write(cMailBox.CommercialUse ? "OK" : "Cancel");
                    }
                    else
                    {
                        Response.Write("Cancel");
                    }
                }
            }
        }
    }
}
