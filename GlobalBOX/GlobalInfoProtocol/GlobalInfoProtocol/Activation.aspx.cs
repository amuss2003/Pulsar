using System;
using GlobalInfoProtocol.Classes;
using GlobalInfoProtocol.Authorization;
using System.Collections.Generic;

namespace GlobalInfoProtocol
{
    public partial class Activation : System.Web.UI.Page
    {
        // TODO: Why no LoginKey required ?
        protected void Page_Load(object sender, EventArgs e)
        {
            ////TODO: HTTP 401 or Redirect
            //if (!RequestAuthentication.Authenticate(Request))
            //{
            //    Logger.AddToLogger(Server.MapPath("."), "Activation.aspx ERROR: Request failed authentication.");
            //    return;
            //}

            var requestValidator = new RequestValidator(error =>
                Logger.AddToLogger(Server.MapPath("."), "Activation.aspx ERROR: " + error));

            var propertiesToValidate = new List<string> { "P1", "P2", "P3" };

            //TODO: HTTP 404 or Redirect
            if (!requestValidator.ValidateDataFieldsInRequest(Request, propertiesToValidate))
                return;

            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            var mac = Helper.DecodeFrom64(Request["P1"]);
            var email = Helper.DecodeFrom64(Request["P2"]);
            var vat = Helper.DecodeFrom64(Request["P3"]);

            Company company = dblayer.GetCompanyByKey(email, mac, vat);
            //Response.Write(dblayer.ErrorList + "</br>");
            if (company != null)
            {
                company.Active = true;
                dblayer.UpdateCompany(company);
                Label1.Visible = true;
            }
        }
    }
}