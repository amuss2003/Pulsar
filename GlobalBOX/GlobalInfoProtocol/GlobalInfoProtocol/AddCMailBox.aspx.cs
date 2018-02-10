using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol
{
    public partial class AddCMailBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];

            String CMailBoxInstallID = Request["CMailBoxInstallID"];
            String Commercial = Request["TermUse"];

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((CMailBoxInstallID != null) && (CMailBoxInstallID != ""))
                {
                    if ((Commercial != null) && (Commercial != ""))
                    {
                        CMailBox cMailBox = new CMailBox();
                        cMailBox.CMailBoxInstallID = CMailBoxInstallID;
                        cMailBox.CommercialUse = (Commercial.ToLower() == "company");

                        bool bSuccess = dblayer.AddCMailBox(cMailBox);
                        Response.Write(bSuccess.ToString().ToLower());
                    }
                }
            }
        }
    }
}