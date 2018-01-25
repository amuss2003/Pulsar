using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;

namespace GlobalInfoProtocol
{
    public partial class EmailSend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String LoginKey = Request["LoginKey"];

            //String CMailBoxID = Request["CMailBoxID"];

            String EMail = Request["EMail"];
            String CompanyName = Request["CompanyName"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];

            SendActivationEmail(EMail, CompanyName, CompanyVAT, CountryID); //, CMailBoxID
        }

        private static byte[] bytes = null;

        private void SendActivationEmail(String EMailTo, String CompanyName, String CompanyVAT, String CountryID) //, String CMailBoxID
        {
            //string BODY = String.Empty;
            //string PageName = "[Path to resource]"; //example C:\DataSource\Website\DomainName\RousourceFolder\page.html

            //BODY = new StreamReader(PageName).ReadToEnd();

            String strKey = "yanivzzohar"; //HttpUtility.UrlEncode(Encrypt(MAC)); //xezp3avnniqyjf45wso0ot45
            char[] delimiterChars = { '|' };
            bytes = ASCIIEncoding.ASCII.GetBytes((strKey).Substring(0, 8));

            //MAPI mapi = mapi = new MAPI();            
            String stEMail = EMailTo;
            String stSubject = "Your invitation to join CMailBox Requested by " + CompanyName + ", VAT:" + CompanyVAT;
            String stBodyFile = "";// "Thank you for accepting to Downloading CMailBox.</br>" + Environment.NewLine;
            //stBodyFile += "CMailBox.</br>" + Environment.NewLine;

            //Uri.EscapeUriString(Encrypt(PCSignature))
            //P1=" + Encrypt(strKey) + "
            //stBodyFile += "<table background=\"http://www.simplicitools.com/SimpliciTools/Images/CMailBox2.jpg\">";
            // background=\"http://www.simplicitools.com/SimpliciTools/Images/CMailBox2.jpg\"
            stBodyFile += "<table  cellpadding=\"0\" cellspacing=\"0\" style=\"height: 334px; width:499px; background:#dedede url(http://www.simplicitools.com/SimpliciTools/Images/CMailBox2.jpg) \">";
            
            stBodyFile += "<tr>";
            stBodyFile += "<td>";
            stBodyFile += "Thank you for accepting to Downloading CMailBox.";
            stBodyFile += "</td>";
            stBodyFile += "</tr>";

            stBodyFile += "<tr>";
            stBodyFile += "<td>";
            stBodyFile += "<a href=\"http://www.simplicitools.com/SimpliciTools/Tools/CMailBox.msi\">Download CMail Box</a></br>" + Environment.NewLine;
            stBodyFile += "</td>";
            stBodyFile += "</tr>";

            stBodyFile += "<tr>";
            stBodyFile += "<td>";
            stBodyFile += "<img src=\"http://www.simplicitools.com/SimpliciTools/Images/CMailBox2.jpg\"></br>";
            stBodyFile += "</td>";
            stBodyFile += "</tr>";

            stBodyFile += "<tr>";
            stBodyFile += "<td>";
            stBodyFile += "<a href=\"http://www.simplicitools.com/SimpliciTools/SoftwareDownloads.aspx\">Simplicitools Web Site</a></br>" + Environment.NewLine;
            stBodyFile += "</td>";
            stBodyFile += "</tr>";
            
            stBodyFile += "</table>";

            String Body = stBodyFile;

            String From = "adirim@adirim.info"; //Need to be simplicitools email!!!!
            MailMessage mailObj = new MailMessage(From, stEMail, stSubject, Body); //"hagar@hagar-rent.co.il"
            mailObj.IsBodyHtml = true;
            //Attachment attachment = new Attachment(DllName);
            //mailObj.Attachments.Add(attachment);

            //mapi.AddRecipientTo(stEMail); //fi.FullName //<=======================================
            //mapi.SendMailPopup(stSubject, Body);
            //mapi.SendMailDirect(stSubject, Body); //fi.FullName //<=======================================

            //SmtpClient SMTPServer = new SmtpClient("smtp.012.net.il", 25); //"mail.naltec.co.il" //pop.012.net.il //587
            String host = "smtp.012.net.il";

            SmtpClient SMTPServer = new SmtpClient(host, 25); //"mail.naltec.co.il" //pop.012.net.il //587

            SMTPServer.Credentials = new System.Net.NetworkCredential("support_misradit", "9o89ooeq");
            //SMTPServer.Credentials = new System.Net.NetworkCredential("yanivz_es", "a1a2a3");

            //SMTPServer.Credentials = new System.Net.NetworkCredential(UserName, Password);
            SMTPServer.EnableSsl = true;

            try
            {
                //SMTPServer.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory; 
                //SMTPServer.PickupDirectoryLocation = @"c:\tmp\emailout\";                        
                //Logger("SMTPServer_Send", "");                
                SMTPServer.Send(mailObj);
                //Logger("SMTPServer_Send", "OK");
                Console.WriteLine("OK");
            }
            catch (Exception ex)
            {
                //Logger("SMTPServer_SendFail", ex.Message);
                Console.WriteLine(ex.Message);
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