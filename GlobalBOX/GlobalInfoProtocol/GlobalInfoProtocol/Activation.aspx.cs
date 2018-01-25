using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace GlobalInfoProtocol
{
    public partial class Activation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String MAC = Request["P1"];
            String EMail = Request["P2"];
            String VAT = Request["P3"];

            if ((MAC != null) && (MAC != ""))
            {
                if ((EMail != null) && (EMail != ""))
                {
                    if ((VAT != null) && (VAT != ""))
                    {
                        MAC = DecodeFrom64(MAC);
                        EMail = DecodeFrom64(EMail);
                        VAT = DecodeFrom64(VAT);

                        Company company = dblayer.GetCompanyByKey(EMail, MAC, VAT);
                        //Response.Write(dblayer.ErrorList + "</br>");
                        if (company != null)
                        {
                            company.Active = true;
                            dblayer.UpdateCompany(company);
                            Label1.Visible = true;
                        }
                    }
                }
            }
        }

        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
}