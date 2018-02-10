using System;
using System.Text;
using System.Net.Mail;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol.Email
{
    public class EmailService
    {
        public void SendActivationEmail(String EMailTo, String MAC, String VAT)
        {
            String strKey = "yanivzzohar"; //HttpUtility.UrlEncode(Encrypt(MAC)); //xezp3avnniqyjf45wso0ot45
            char[] delimiterChars = { '|' };
            var bytes = Encoding.ASCII.GetBytes((strKey).Substring(0, 8));

            //MAPI mapi = mapi = new MAPI();            
            String stEMail = EMailTo;
            String stSubject = "Your activation your email account for SimpliciTools";
            String stBodyFile = "Thank you for purchasing SimplicTools item" + Environment.NewLine;
            stBodyFile += "Global Box." + Environment.NewLine;

            //Uri.EscapeUriString(Encrypt(PCSignature))
            //P1=" + Encrypt(strKey) + "
            stBodyFile += "http://212.150.1.51/GlobalInfoProtocol/Activation.aspx?P1=" + Uri.EscapeUriString(Helper.EncodeTo64(MAC)) + "&P2=" + Uri.EscapeUriString(Helper.EncodeTo64(EMailTo)) + "&P3=" + Uri.EscapeUriString(Helper.EncodeTo64(VAT));
            //stBodyFile += "http://adirim.info/Activation.aspx?GUID=" + GUID;

            String Body = stBodyFile;

            String From = "adirim@adirim.info"; //Need to be simplicitools email!!!!
            MailMessage mailObj = new MailMessage(From, stEMail, stSubject, Body); //"hagar@hagar-rent.co.il"

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
    }
}