using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace GlobalInfoProtocol
{
    public partial class Advertisment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["files"] == null)
            {
                DirectoryInfo di = new DirectoryInfo(Server.MapPath(".") + @"/Images/Advertise");
                Session["files"] = di.GetFiles("*.jpg");
                Session["files_index"] = 0;
            }

            if (Session["files"] != null)
            {
                FileInfo[] fi = (FileInfo[])Session["files"];

                int files_index = (int)Session["files_index"];
                imagePreview.ImageUrl = "/GlobalInfoProtocol/Images/Advertise/" + Uri.UnescapeDataString(fi[files_index].ToString());
                files_index++;
                Session["files_index"] = files_index;
                if(files_index >= fi.Length)
                {
                    Session["files_index"] = 0;
                }
            }
        }
    }
}
