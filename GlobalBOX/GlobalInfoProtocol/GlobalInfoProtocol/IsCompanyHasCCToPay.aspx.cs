using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol
{
    public partial class IsCompanyHasCCToPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String ReadCode = Request["Read"];
            String MAC = Request["MAC"];
            String WriteCode = Request["Write"];
            String CompanySerialNumber = Request["CompanySerialNumber"];
 //"IsCompanyHasCCToPay.aspx?CountryID=" + company_info.CompanyCountryID + "&CompanyVAT=" + company_info.CompanyVAT + 
//"&MAC=" + GetMacAddress() + "&Read=" + company_info.ReadCode + "&Write=" + company_info.WriteCode + "&CompanySerialNumber=" + company_info.CompanySerialNumber

            Logger.AddToLogger(Server.MapPath("."), "IsCompanyHasCCToPay.aspx Request");

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                Logger.AddToLogger(Server.MapPath("."), "LoginKey.aspx Request");
                if ((CountryID != null) && (CountryID != ""))
                {
                    Logger.AddToLogger(Server.MapPath("."), "CountryID.aspx Request");
                    if ((CompanyVAT != null) && (CompanyVAT != ""))
                    {
                        Logger.AddToLogger(Server.MapPath("."), "CompanyVAT.aspx Request");
                        if ((ReadCode != null) && (ReadCode != ""))
                        {
                            Logger.AddToLogger(Server.MapPath("."), "ReadCode.aspx Request");
                            if ((MAC != null) && (MAC != ""))
                            {
                                Logger.AddToLogger(Server.MapPath("."), "WriteCode.aspx Request");
                                if ((WriteCode != null) && (WriteCode != ""))
                                {
                                    Logger.AddToLogger(Server.MapPath("."), "WriteCode.aspx Request");
                                    if ((CompanySerialNumber != null) && (CompanySerialNumber != ""))
                                    {
                                        Company company = dblayer.GetCompanyReadable(CountryID, MAC, CompanyVAT, ReadCode);
                                        Logger.AddToLogger(Server.MapPath("."), "GetCompanyReadable.aspx Request");

                                        if (company != null)
                                        {
                                            Logger.AddToLogger(Server.MapPath("."), "company.aspx Request");
                                            if (company.Active)
                                            {
                                                Logger.AddToLogger(Server.MapPath("."), "Active.aspx Request");
                                                if (company.CompanySerialNumber == CompanySerialNumber)
                                                {
                                                    Logger.AddToLogger(Server.MapPath("."), "CompanySerialNumber.aspx Request");
                                                    Response.Write(dblayer.IsCompanyHasCC(company.CompanySerialNumber));
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
}