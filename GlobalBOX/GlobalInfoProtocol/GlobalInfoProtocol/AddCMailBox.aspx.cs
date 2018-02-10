using System;
using GlobalInfoProtocol.Classes;
using GlobalInfoProtocol.Authentication;
using GlobalInfoProtocol.Authorization;
using System.Collections.Generic;

namespace GlobalInfoProtocol
{
    public partial class AddCMailBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: HTTP 401 or Redirect
            if (!RequestAuthentication.Authenticate(Request))
            {
                Logger.AddToLogger(Server.MapPath("."), "AddCMailBox.aspx ERROR: Request failed authentication.");
                return;
            }

            var requestValidator = new RequestValidator(error =>
                Logger.AddToLogger(Server.MapPath("."), "AddCMailBox.aspx ERROR: " + error));

            var propertiesToValidate = new List<string> { "CMailBoxInstallID", "TermUse" };

            //TODO: HTTP 404 or Redirect
            if (!requestValidator.ValidateDataFieldsInRequest(Request, propertiesToValidate))
                return;

            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            CMailBox cMailBox = new CMailBox
            {
                CMailBoxInstallID = Request["CMailBoxInstallID"],
                CommercialUse = Request["Commercial"].ToLower() == "company"
            };

            bool bSuccess = dblayer.AddCMailBox(cMailBox);
            Response.Write(bSuccess.ToString().ToLower());
        }
    }
}