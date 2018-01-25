using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class IsActive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));
            //http://10.9.10.250/GlobalInfoProtocol/IsActive.aspx?CountryID=117&CompanyVAT=111111118&MAC=00000000000000E0&Read=123456789&Write=123456789&LoginKey=xezp3avnniqyjf45wso0ot45
            String LoginKey = Request["LoginKey"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String MAC = Request["MAC"];
            String ReadCode = Request["Read"];
            //String WriteCode = Request["Write"];            

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {                
                if ((CountryID != null) && (CountryID != ""))
                {                    
                    if ((CompanyVAT != null) && (CompanyVAT != ""))
                    {                        
                        if ((MAC != null) && (MAC != ""))
                        {                            
                            if ((ReadCode != null) && (ReadCode != ""))
                            {
                                //Response.Write(ReadCode + "</br>");

                                Company company = dblayer.GetCompanyReadable(CountryID, MAC, CompanyVAT, ReadCode);
                                //Response.Write(dblayer.ErrorList);
                                if (company != null)
                                {
                                    if (company.Active)
                                    {
                                        Response.Write(company.CompanySerialNumber);
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
