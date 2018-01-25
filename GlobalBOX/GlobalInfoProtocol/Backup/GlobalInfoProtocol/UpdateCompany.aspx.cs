using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
//http://212.150.1.51/GlobalInfoProtocol/UpdateCompany.aspx?CountryID=117&CompanyVAT=513638346&CompanyName=אדירים&ReadCode=123123123&WriteCode=123123123&EMail=a@b.com&NewCountryID=117&NewCompanyVAT=513638346&NewCompanyName=אדירים בע"מ&NewReadCode=123123123&NewWriteCode=321321321&NewEMail=a1@b1.com&LoginKey=xezp3avnniqyjf45wso0ot45
namespace GlobalInfoProtocol
{
    public partial class UpdateCompany : System.Web.UI.Page
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
            String MobilePhone = Request["MobilePhone"];
            String InformMyMobile = Request["InformMyMobile"];

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
                                            //CompanyName = Uri.EscapeUriString(CompanyName);
                                            //CompanyName = HttpUtility.UrlEncode(CompanyName);
                                            //CompanyName = Uri.EscapeDataString(CompanyName);
                                            Company companyCurrent = dblayer.GetCompany(CountryID, CompanyVAT);

                                            Company company = new Company();
                                            company.CompanyName = CompanyName;
                                            company.CountryID = Int32.Parse(CountryID);
                                            company.CompanyVAT = CompanyVAT;
                                            company.ReadCode = ReadCode;
                                            company.WriteCode = WriteCode;
                                            company.EMail = EMail;
                                            company.Active = true; // false;
                                            company.CommercialUse = (Commercial.ToLower() == "company");
                                            company.CompanySerialNumber = companyCurrent.CompanySerialNumber;
                                            company.MobilePhone = (MobilePhone == null ? "" : MobilePhone);
                                            company.InformMyMobile = (InformMyMobile.ToLower() == "true");

                                            //Response.Write("xxxxxxxxxxxxx");
                                            if (dblayer.IsComapnyExist(company) != null)
                                            {
                                                company.CompanyName = NewCompanyName;
                                                company.ReadCode = NewReadCode;
                                                company.WriteCode = NewWriteCode;
                                                company.EMail = NewEMail;
                                                company.Paid = companyCurrent.Paid;

                                                dblayer.UpdateCompany(company);
                                                dblayer.AddStatusLog(company, "Update");
                                            }

                                            Response.Write(dblayer.ErrorList);
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
