using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar.Classes
{
    public static class CompanyIdentification
    {
        public static Company Identify(DBLayer dblayer, CompanyInfo company_info, String CountryID, String CompanyVAT, String CompanyName, String AccountCode)
        {
            Company company = null;

            if (AccountCode != null)
            {
                if (AccountCode.Trim() != "")
                {
                    company = dblayer.GetCompanyByAccountCode(AccountCode);
                    if (company != null)
                    {
                        return company;
                    }
                }
            }

            if ((CompanyVAT != "") || (CompanyName != ""))
            {
                if ((CompanyVAT != "") && (CompanyName != ""))
                {
                    company = dblayer.GetCompanyByVAT(CompanyVAT);

                    if (company == null)
                    {
                        company = dblayer.GetCompanyByName(CompanyName);
                    }

                    if (company == null)
                    {
                        company = new Company();
                        company.CompamyInfoCountryID = company_info.CompanyCountryID;
                        company.CompamyInfoVAT = company_info.CompanyVAT;

                        if (CountryID != null)
                        {
                            company.CountryID = Int32.Parse(CountryID);
                        }
                        else
                        {
                            company.CountryID = company_info.CompanyCountryID;
                        }

                        company.CompanyVAT = CompanyVAT;
                        company.CompanyName = CompanyName;
                        company.CompanyType = 2;
                        dblayer.Current_Company_Info = company_info;
                        dblayer.AddCompany(company);
                        company = dblayer.GetCompanyByVAT(CompanyVAT);
                    }
                }
                else if ((CompanyVAT != "") && (CompanyName == ""))
                {
                    company = dblayer.GetCompanyByVAT(CompanyVAT);
                }
                else if ((CompanyVAT == "") && (CompanyName != ""))
                {
                    company = dblayer.GetCompanyByName(CompanyName);
                }

                if (company != null)
                {
                    return company;
                }
            }

            return company; //null
        }
    }
}
