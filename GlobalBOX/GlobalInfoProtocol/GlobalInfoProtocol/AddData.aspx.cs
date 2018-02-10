using System;
using System.Collections.Generic;
using GlobalInfoProtocol.Classes;
using GlobalInfoProtocol.Authentication;
using GlobalInfoProtocol.Authorization;

namespace GlobalInfoProtocol
{
    public partial class AddData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: HTTP 401 or Redirect
            if (!RequestAuthentication.Authenticate(Request))
            {
                Logger.AddToLogger(Server.MapPath("."), "AddData.aspx ERROR: Request failed authentication.");
                return;
            }

            var requestValidator = new RequestValidator(error =>
                Logger.AddToLogger(Server.MapPath("."), "AddData.aspx ERROR: " + error));

            var propertiesToValidate = new List<string>
            {
                "TransactionGUID", "CountryIDFrom", "CompanyVATFrom", "CountryIDTo", "CompanyVATTo",
                "WriteCode", "Data", "CompanySerialNumber"
            };

            //TODO: HTTP 404 or Redirect
            if (!requestValidator.ValidateDataFieldsInRequest(Request, propertiesToValidate))
                return;

            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            var countryIDFrom = Request["CountryIDFrom"];
            var companyVATFrom = Request["CompanyVATFrom"];
            var countryIDTo = Request["CountryIDTo"];
            var companyVATTo = Request["CompanyVATTo"];
            var data = Request["Data"];
            var companySerialNumber = Request["CompanySerialNumber"];
            var transactionGUID = Request["TransactionGUID"];
            var writeCode = Request["WriteCode"];

            if (dblayer.IsCompanyBlocked(countryIDFrom, companyVATFrom, countryIDTo, companyVATTo))
                return;

            // TODO: WTF?
            data = data.Replace("\"\"", "\"");
            data = data.Replace("''", "'");
            data = data.Replace("\"", "\"\"");
            data = data.Replace("'", "''");

            var success = dblayer.AddData(transactionGUID, countryIDFrom, companyVATFrom, countryIDTo, 
                companyVATTo, data, writeCode);

            if (success)
            {
                Billing billing = dblayer.GetBilling(companySerialNumber, 
                    Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString()));

                if (billing == null)
                {
                    billing = new Billing
                    {
                        CompanySerialNumber = companySerialNumber,
                        DateMonth = Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString()),
                        InCounter = 1,
                        OutCounter = 0
                    };

                    dblayer.AddBilling(billing);
                }
                else
                {
                    billing.InCounter++;
                    dblayer.UpdateBilling(billing, 
                        Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString()));
                }
            }

            Response.Write(transactionGUID + ", " + countryIDFrom + ", " + companyVATFrom + ", " +
                countryIDTo + ", " + companyVATTo + ", " + data + ", " + writeCode);
        }
    }
}

