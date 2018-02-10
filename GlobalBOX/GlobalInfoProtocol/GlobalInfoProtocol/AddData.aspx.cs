using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol
{
    public partial class AddData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //http://212.150.1.51/GlobalInfoProtocol/AddData.aspx?CountryIDFrom=117&CompanyVATFrom=513638346&CountryIDTo=117&CompanyVATTo=513638346&WriteCode=123456789&Data=KT|024444|0303/12/2012|0503/12/2012|0603/12/2012|07אדירים|08513638346|09xxx|10|111|12|13222|14חז&LoginKey=xezp3avnniqyjf45wso0ot45
            //http://212.150.1.51/GlobalInfoProtocol/AddData.aspx?
            //CountryIDFrom=117&CompanyVATFrom=513638346&CountryIDTo=117
            //&CompanyVATTo=513638346&WriteCode=123456789&
            //Data=KT|024444|0303/12/2012|0503/12/2012|0603/12/2012|07אדירים|08513638346|09xxx|10|111|12|13222|14חז
            //&LoginKey=xezp3avnniqyjf45wso0ot45

            //http://212.150.1.51/GlobalInfoProtocol/AddData.aspx?
            //CountryIDFrom=117
            //&CompanyVATFrom=513638346
            //&CountryIDTo=117
            //&CompanyVATTo=513638346
            //&WriteCode=123456789&
            //Data=KT|02222|0303/12/2012|0503/12/2012|0603/12/2012|07אדירים|08513638346|09ww|10|111|12|13333|14חז
            //&LoginKey=xezp3avnniqyjf45wso0ot45
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];

            String TransactionGUID = Request["TransactionGUID"];

            String CountryIDFrom = Request["CountryIDFrom"];
            String CompanyVATFrom = Request["CompanyVATFrom"];

            String CountryIDTo = Request["CountryIDTo"];
            String CompanyVATTo = Request["CompanyVATTo"];

            String WriteCode = Request["WriteCode"];

            String Data = Request["Data"];
            String CompanySerialNumber = Request["CompanySerialNumber"];

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((TransactionGUID != null) && (TransactionGUID != ""))
                {
                    if ((WriteCode != null) && (WriteCode != ""))
                    {
                        if ((CountryIDFrom != null) && (CountryIDFrom != ""))
                        {
                            if ((CompanyVATFrom != null) && (CompanyVATFrom != ""))
                            {
                                if ((CountryIDTo != null) && (CountryIDTo != ""))
                                {
                                    if ((CompanyVATTo != null) && (CompanyVATTo != ""))
                                    {
                                        if ((Data != null) && (Data != ""))
                                        {
                                            if ((CompanySerialNumber != null) && (CompanySerialNumber != ""))
                                            {                                                
                                                if (!dblayer.IsCompanyBlocked(CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo))
                                                {
                                                    Data = Data.Replace("\"\"", "\"");
                                                    Data = Data.Replace("''", "'");

                                                    Data = Data.Replace("\"", "\"\"");
                                                    Data = Data.Replace("'", "''");

                                                    Response.Write(TransactionGUID + ", " + CountryIDFrom + ", " + CompanyVATFrom + ", " + CountryIDTo + ", " + CompanyVATTo + ", " + Data + ", " + WriteCode);
                                                    if (dblayer.AddData(TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, WriteCode))
                                                    {                                                        
                                                        Billing billing = dblayer.GetBilling(CompanySerialNumber, Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString()));
                                                        if (billing == null)
                                                        {
                                                            billing = new Billing();
                                                            billing.CompanySerialNumber = CompanySerialNumber;
                                                            billing.DateMonth = Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString());
                                                            billing.InCounter = 1;
                                                            billing.OutCounter = 0;
                                                            dblayer.AddBilling(billing);
                                                        }
                                                        else
                                                        {
                                                            billing.InCounter++;
                                                            dblayer.UpdateBilling(billing, Convert.ToDateTime(DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToShortDateString()));
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
    }
}

