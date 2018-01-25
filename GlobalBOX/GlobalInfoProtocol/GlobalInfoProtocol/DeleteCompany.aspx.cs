using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol
{
    public partial class DeleteCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Logger.AddToLogger(Server.MapPath("."), "DeleteCompany.aspx Request");
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));
            
            //LoginKey=xezp3avnniqyjf45wso0ot45
            String LoginKey = Request["LoginKey"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String ReadCode = Request["ReadCode"];
            String WriteCode = Request["WriteCode"];
            String EMail = Request["EMail"];
            //Logger.AddToLogger(Server.MapPath("."), "DeleteCompany.aspx Parse Data");
            //Logger.AddToLogger(Server.MapPath("."), "DeleteCompany.aspx QueryString:\r\n" + Request.QueryString);
            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                //Logger.AddToLogger(Server.MapPath("."), "DeleteCompany.aspx LoginKey");
                if ((CountryID != null) && (CountryID != ""))
                {
                    //Logger.AddToLogger(Server.MapPath("."), "DeleteCompany.aspx CompanyName");

                    if ((CompanyVAT != null) && (CompanyVAT != ""))
                    {
                        //Logger.AddToLogger(Server.MapPath("."), "DeleteCompany.aspx CompanyVAT");
                        if ((ReadCode != null) && (ReadCode != ""))
                        {
                            //Logger.AddToLogger(Server.MapPath("."), "DeleteCompany.aspx WriteCode");
                            if ((WriteCode != null) && (WriteCode != ""))
                            {
                                if ((EMail != null) && (EMail != ""))
                                {
                                    //Logger.AddToLogger(Server.MapPath("."), "DeleteCompany.aspx Pass All Data");
                                    //Company company = new Company();
                                    //company.CountryID =  Int32.Parse(CountryID);
                                    //company.CompanyVAT = CompanyVAT;
                                    //company.ReadCode = ReadCode;
                                    //company.WriteCode = WriteCode;
                                    //company.EMail = EMail;
                                    //dblayer.DeleteCompany(company);
                                    Company company = dblayer.GetCompany(CountryID, CompanyVAT);
                                    dblayer.DeleteCompany(Int32.Parse(CountryID), CompanyVAT, ReadCode, WriteCode, EMail);
                                    dblayer.AddStatusLog(company, "Delete");
                                    //Logger.AddToLogger(Server.MapPath("."), dblayer.ErrorList);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
