using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class IsCompanyExist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((CountryID != null) && (CountryID != ""))
                {
                    if ((CompanyVAT != null) && (CompanyVAT != ""))
                    {
                        Company company = dblayer.IsComapnyExist(CountryID, CompanyVAT);

                        //Response.Write(dblayer.ErrorList);
                        if (company != null)
                        {
                            if (company.Active)
                            {
                                Response.Write("Active");
                            }
                            else
                            {
                                Response.Write("Not Active");
                            }
                        }
                        else
                        {
                            Response.Write("Not Exist");
                        }
                    }
                }
            }
        }
    }
}
