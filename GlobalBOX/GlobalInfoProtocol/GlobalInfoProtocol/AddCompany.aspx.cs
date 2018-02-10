using System;
using System.Collections.Generic;


using GlobalInfoProtocol.Classes;
using GlobalInfoProtocol.Authorization;
using GlobalInfoProtocol.Authentication;

namespace GlobalInfoProtocol
{
    public partial class AddCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: HTTP 401 or Redirect
            if (!RequestAuthentication.Authenticate(Request))
            {
                Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx ERROR: Request failed authentication.");
                return;
            }

            var requestValidator = new RequestValidator(error =>
                Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx ERROR: " + error));

            var propertiesToValidate = new List<string>
            {
                "CompanyName", "CountryID", "CompanyVAT", "ReadCode", "WriteCode",
                "EMail", "MAC", "CompanySerialNumber", "Payment", "TermUse", "InformMyMobile"
            };

            //TODO: HTTP 404 or Redirect
            if (!requestValidator.ValidateDataFieldsInRequest(Request, propertiesToValidate))
                return;

            var company = new Company
            {
                CompanyName = Request["CompanyName"],
                CountryID = Int32.Parse(Request["CountryID"]),
                CompanyVAT = Request["CompanyVAT"],
                ReadCode = Request["ReadCode"],
                WriteCode = Request["WriteCode"],
                EMail = Request["EMail"],
                MAC = "",// MAC;
                Active = true, // false;
                CompanySerialNumber = Request["CompanySerialNumber"],
                Payment = Request["Payment"],
                CommercialUse = Request["TermUse"].ToLower() == "company",
                Paid = true,    //Oded say always paid on add
                CreationDate = DateTime.Now,
                StartService = DateTime.Now,
                MobilePhone = Request["MobilePhone"] ?? "",
                InformMyMobile = Request["InformMyMobile"].ToLower() == "true"
            };

            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));
            bool bSuccess = dblayer.AddCompany(company);        
            dblayer.AddStatusLog(company, "Creation");

            Response.Write(bSuccess.ToString().ToLower());

            //Update ServiceStatusLog Table
            //CompanySerialNumber
            //ActionDate
            //Status
            //CommercialUse

            //Logger.AddToLogger(Server.MapPath("."), dblayer.ErrorList);
            //Response.Write("<br/>");
            //Response.Write(dblayer.ErrorList);

            if (bSuccess)
            {
                //Oded Ask to remove activation for now, until recomendation!
                //SendActivationEmail(EMail, MAC, CompanyVAT);  //TODO: activation removed!
            }
        }
    }
}