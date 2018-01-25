using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class UpdatePayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];

            String CompanyName = Request["CompanyName"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String ReadCode = Request["ReadCode"];
            String WriteCode = Request["WriteCode"];
            String EMail = Request["EMail"];

            String Commercial = Request["TermUse"];
            String Payment = Request["Data"];
            //Response.Write(Request.QueryString);
            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((CompanyName != null) && (CompanyName != ""))
                {
                    if ((CountryID != null) && (CountryID != ""))
                    {
                        if ((CompanyVAT != null) && (CompanyVAT != ""))
                        {
                            if ((ReadCode != null) && (ReadCode != ""))
                            {
                                if ((WriteCode != null) && (WriteCode != ""))
                                {
                                    if ((EMail != null) && (EMail != ""))
                                    {
                                        if ((Commercial != null) && (Commercial != ""))
                                        {
                                            if ((Payment != null) && (Payment != ""))
                                            {
                                                //CompanyName = Uri.EscapeUriString(CompanyName);
                                                //CompanyName = HttpUtility.UrlEncode(CompanyName);
                                                //CompanyName = Uri.EscapeDataString(CompanyName);
                                                //Company companyCurrent = dblayer.GetCompany(CountryID, CompanyVAT);

                                                Company company = new Company();
                                                company.CompanyName = CompanyName;
                                                company.CountryID = Int32.Parse(CountryID);
                                                company.CompanyVAT = CompanyVAT;
                                                company.ReadCode = ReadCode;
                                                company.WriteCode = WriteCode;
                                                company.EMail = EMail;
                                                company.Active = true; // false;
                                                company.CommercialUse = (Commercial.ToLower() == "company");
                                                company.Paid = true;

                                                if (dblayer.IsComapnyExist(company) != null)
                                                {
                                                    company.Payment = Payment;
                                                    //company.Paid = companyCurrent.Paid;

                                                    dblayer.UpdateCompany(company);
                                                }
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
    }
}