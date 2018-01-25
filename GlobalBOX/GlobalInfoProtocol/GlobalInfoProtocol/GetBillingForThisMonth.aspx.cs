using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol
{
    public partial class GetBillingForThisMonth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //http://212.150.1.51/GlobalInfoProtocol/GetBillingForThisMonth.aspx?CountryID=117&CompanyVAT=513638346&MAC=001CC0B16B8C&Read=123456789&Write=123456789&LoginKey=xezp3avnniqyjf45wso0ot45
            //http://212.150.1.51/GlobalInfoProtocol/GetBillingForThisMonth.aspx?CountryID=117&CompanyVAT=512242355&MAC=001CC0B16B8C&Read=peer30033&Write=ilana1234&LoginKey=xezp3avnniqyjf45wso0ot45
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];

            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String ReadCode = Request["Read"];
            String MAC = Request["MAC"];
            String WriteCode = Request["Write"];
            String CompanySerialNumber = Request["CompanySerialNumber"];

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((CountryID != null) && (CountryID != ""))
                {
                    if ((CompanyVAT != null) && (CompanyVAT != ""))
                    {
                        if ((ReadCode != null) && (ReadCode != ""))
                        {
                            if ((MAC != null) && (MAC != ""))
                            {
                                if ((WriteCode != null) && (WriteCode != ""))
                                {
                                    if ((CompanySerialNumber != null) && (CompanySerialNumber != ""))
                                    {
                                        Company company = dblayer.GetCompanyReadable(CountryID, MAC, CompanyVAT, ReadCode);

                                        if (company != null)
                                        {
                                            if (company.Active)
                                            {
                                                Billing billing = dblayer.GetBillingForThisMonth(CompanySerialNumber);
                                                if (billing == null)
                                                {
                                                    Response.Write("0|0");
                                                }
                                                else
                                                {
                                                    Response.Write((billing.InCounter + "|" + billing.OutCounter));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}