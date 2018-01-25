using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class IsCompanyPaid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            //Response.Write(Request.QueryString);
            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((CountryID != null) && (CountryID != ""))
                {
                    if ((CompanyVAT != null) && (CompanyVAT != ""))
                    {
                        Company company = dblayer.IsComapnyExist(CountryID, CompanyVAT);
                        //Response.Write("company=" + company.CompanyID + "," + company.Paid);
                        //Response.Write(dblayer.ErrorList);
                        if (company != null)
                        {
                            if (company.Paid)
                            {
                                Response.Write("ok");
                            }
                            else
                            {
                                Response.Write("cancel");
                            }
                        }
                        else
                        {
                            Response.Write("cancel");
                        }
                    }
                }
            }
        }
    }
}
