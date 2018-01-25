using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class BlockClientForCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];

            String CountryIDBlocked = Request["CountryIDBlocked"];
            String CompanyVATBlocked = Request["CompanyVATBlocked"];

            String CountryIDRequest = Request["CountryIDRequest"];
            String CompanyVATRequest = Request["CompanyVATRequest"];
            String ReadCode = Request["ReadCode"];

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((ReadCode != null) && (ReadCode != ""))
                {
                    if ((CountryIDBlocked != null) && (CountryIDBlocked != ""))
                    {
                        if ((CompanyVATBlocked != null) && (CompanyVATBlocked != ""))
                        {
                            if ((CountryIDRequest != null) && (CountryIDRequest != ""))
                            {
                                if ((CompanyVATRequest != null) && (CompanyVATRequest != ""))
                                {
                                    Company company = dblayer.GetCompanyReadable(CountryIDRequest, CompanyVATRequest, ReadCode);

                                    if ((company != null) && (company.Active))
                                    {
                                        dblayer.AddCompanyBlocking(CountryIDBlocked, CompanyVATBlocked, CountryIDRequest, CompanyVATRequest);
                                        //Response.Write(dblayer.ErrorList);
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