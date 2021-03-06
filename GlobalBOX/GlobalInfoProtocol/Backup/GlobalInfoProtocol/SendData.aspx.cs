﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class SendData : System.Web.UI.Page
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
                                    Company company = dblayer.GetCompanyReadable(CountryID, MAC, CompanyVAT, ReadCode);
                                    if (company != null)
                                    {
                                        if (company.Active)
                                        {
                                            Response.Write(dblayer.GetCompanyData(CountryID, CompanyVAT, WriteCode));
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
