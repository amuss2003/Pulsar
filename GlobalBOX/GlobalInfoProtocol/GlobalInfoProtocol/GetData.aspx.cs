using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;
using System.Text.RegularExpressions;

namespace GlobalInfoProtocol
{
    public partial class GetData : System.Web.UI.Page
    {
        private void ValidateRequestData(string dataValue, string dataKey)
        {
            if (!string.IsNullOrEmpty(dataValue))
                return;

            Logger.AddToLogger(Server.MapPath("."), string.Format(
                "ERROR: Validation failed. DataKey={0}, DataValue={1}", dataKey, dataValue));

            throw new Exception();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.AddToLogger(Server.MapPath("."), "Loading page GetData.aspx.cs");
            //http://212.150.1.51/GlobalInfoProtocol/GetData.aspx?CountryID=117&CompanyVAT=513638346&MAC=001CC0B16B8C&Read=123456789&Write=123456789&LoginKey=xezp3avnniqyjf45wso0ot45
            //http://212.150.1.51/GlobalInfoProtocol/GetData.aspx?CountryID=117&CompanyVAT=512242355&MAC=001CC0B16B8C&Read=peer30033&Write=ilana1234&LoginKey=xezp3avnniqyjf45wso0ot45
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];

            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String ReadCode = Request["Read"];
            String MAC = Request["MAC"];
            String WriteCode = Request["Write"];
            String CompanySerialNumber = Request["CompanySerialNumber"];

            if (LoginKey != "xezp3avnniqyjf45wso0ot45")
                return;

            //var data = new Dictionary<string, string> { };
            ValidateRequestData("CountryID", CountryID);
            ValidateRequestData("CompanyVAT", CompanyVAT);
            ValidateRequestData("ReadCode", ReadCode);
            ValidateRequestData("MAC", MAC);
            ValidateRequestData("WriteCode", WriteCode);
            ValidateRequestData("CompanySerialNumber", CompanySerialNumber);

            Company company = dblayer.GetCompanyReadable(CountryID, MAC, CompanyVAT, ReadCode);

            if (company != null)
            {
                if (company.Active)
                {
                    String data = dblayer.GetCompanyData(CountryID, CompanyVAT, WriteCode);
                    if ((data != null) && (data.Trim() != ""))
                    {
                        Response.Write(data);
                        string[] lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        Billing billing = dblayer.GetBilling(CompanySerialNumber, Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString()));
                        if (billing == null)
                        {
                            billing = new Billing();
                            billing.CompanySerialNumber = CompanySerialNumber;
                            billing.DateMonth = Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString());
                            billing.InCounter = 0;
                            billing.OutCounter = lines.Length;
                            dblayer.AddBilling(billing);
                        }
                        else
                        {
                            billing.OutCounter += lines.Length;
                            dblayer.UpdateBilling(billing, DateTime.Now.AddDays(-(DateTime.Now.Day) + 1));
                        }
                    }
                }
            }
        }
    }
}
