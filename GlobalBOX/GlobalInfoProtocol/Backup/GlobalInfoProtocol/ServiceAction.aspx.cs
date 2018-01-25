using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class StopConnection : System.Web.UI.Page
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

            String NewCompanyName = Request["NewCompanyName"];
            String NewReadCode = Request["NewReadCode"];
            String NewWriteCode = Request["NewWriteCode"];
            String NewEMail = Request["NewEMail"];
            String Commercial = Request["TermUse"];
            String Action = Request["Action"];            

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
                                            if ((Action != null) && (Action != ""))
                                            {
                                                //CompanyName = Uri.EscapeUriString(CompanyName);
                                                //CompanyName = HttpUtility.UrlEncode(CompanyName);
                                                //CompanyName = Uri.EscapeDataString(CompanyName);
                                                Company companyCurrent = dblayer.GetCompany(CountryID, CompanyVAT);

                                                if (dblayer.IsComapnyExist(companyCurrent) != null)
                                                {
                                                    companyCurrent.StopService = DateTime.Now;

                                                    dblayer.UpdateCompany(companyCurrent);
                                                    dblayer.AddStatusLog(companyCurrent, Action);
                                                    //Update ServiceStatusLog Table
                                                    //CompanySerialNumber
                                                    //ActionDate
                                                    //Status
                                                    //CommercialUse
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
