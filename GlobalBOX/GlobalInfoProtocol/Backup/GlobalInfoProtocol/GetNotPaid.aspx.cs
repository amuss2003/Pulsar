using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;

//http://212.150.1.51/GlobalInfoProtocol/GetNotPaid.aspx?CountryID=117&CompanyVAT=028753390&MAC=001CC0B16B8C&Read=123456789&Write=987654321&CompanySerialNumber={1a4f7844-6484-4853-9ae1-fc32efd3657b}&LoginKey=xezp3avnniqyjf45wso0ot45
namespace GlobalInfoProtocol
{
    public partial class GetNotPaid : System.Web.UI.Page
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
                                                if (company.CompanySerialNumber == CompanySerialNumber)
                                                {
                                                    Response.Write(dblayer.GetNotPaidBilling(company.CompanySerialNumber));
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