using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlobalInfoProtocol
{
    public partial class DeleteData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];

            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];
            String TimeStamp =Request["TimeStamp"];
            
            //634815800923593750

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                if ((CountryID != null) && (CountryID != ""))
                {
                    if ((CompanyVAT != null) && (CompanyVAT != ""))
                    {
                        if ((TimeStamp != null) && (TimeStamp != ""))
                        {
                            dblayer.DeleteData(long.Parse(CountryID), CompanyVAT, TimeStamp);
                        }
                    }
                }
            }
        }
    }
}
