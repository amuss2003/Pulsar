using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using GlobalInfoProtocol.Classes;

namespace GlobalInfoProtocol
{
    public partial class AddCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx Request");
            DBLayer dblayer = new DBLayer();
            dblayer.CreateConnectionString(Server.MapPath("."));

            String LoginKey = Request["LoginKey"];

            String CompanyName = Request["CompanyName"];
            String CountryID = Request["CountryID"];
            String CompanyVAT = Request["CompanyVAT"];

            //String UserName = Request["UserName"];
            //String Password = Request["Password"];
            String ReadCode = Request["ReadCode"];
            String WriteCode = Request["WriteCode"];

            String EMail = Request["EMail"];

            String MAC = Request["MAC"];
            String Payment = Request["Payment"]; //Data = Payment

            //String CompanyLogo = Request["CompanyLogo"];
            //String Active = Request["Active"];
            String CompanySerialNumber = Request["CompanySerialNumber"];

            String TermUse = Request["TermUse"];
            String MobilePhone = Request["MobilePhone"];
            String InformMyMobile = Request["InformMyMobile"];
            Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx Parse Data");

            Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx QueryString:\r\n" + Request.QueryString);

            if ((LoginKey != null) && (LoginKey == "xezp3avnniqyjf45wso0ot45"))
            {
                Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx LoginKey");
                if ((CompanyName != null) && (CompanyName != ""))
                {
                    Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx CompanyName");
                    if ((CountryID != null) && (CountryID != ""))
                    {
                        Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx CountryID");
                        if ((CompanyVAT != null) && (CompanyVAT != ""))
                        {
                            Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx CompanyVAT");
                            if ((ReadCode != null) && (ReadCode != ""))
                            {
                                Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx ReadCode");
                                if ((WriteCode != null) && (WriteCode != ""))
                                {
                                    Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx WriteCode");
                                    if ((EMail != null) && (EMail != ""))
                                    {
                                        Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx EMail");
                                        if ((MAC != null) && (MAC != ""))
                                        {
                                            Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx MAC");
                                            if ((CompanySerialNumber != null) && (CompanySerialNumber != ""))
                                            {
                                                Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx Payment");
                                                if ((Payment != null) && (Payment != ""))
                                                {
                                                    if ((TermUse != null) && (TermUse != ""))
                                                    {
                                                        Logger.AddToLogger(Server.MapPath("."), "AddCompany.aspx Pass All Data");
                                                        //CompanyName = Uri.EscapeUriString(CompanyName);
                                                        //CompanyName = HttpUtility.UrlEncode(CompanyName);
                                                        //CompanyName = Uri.EscapeDataString(CompanyName);

                                                        Company company = new Company();
                                                        company.CompanyName = CompanyName;
                                                        company.CountryID = Int32.Parse(CountryID);
                                                        company.CompanyVAT = CompanyVAT;
                                                        company.ReadCode = ReadCode;
                                                        company.WriteCode = WriteCode;
                                                        company.EMail = EMail;
                                                        company.MAC = "";// MAC;
                                                        company.Active = true; // false;
                                                        company.CompanySerialNumber = CompanySerialNumber;
                                                        company.Payment = Payment;
                                                        company.CommercialUse = (TermUse.ToLower() == "company");
                                                        company.Paid = true;    //Oded say always paid on add
                                                        company.CreationDate = DateTime.Now;
                                                        company.StartService = company.CreationDate;
                                                        
                                                        company.MobilePhone = (MobilePhone == null ? "" : MobilePhone);
                                                        company.InformMyMobile = (InformMyMobile.ToLower() == "true");

                                                        bool bSuccess = dblayer.AddCompany(company);
                                                        Response.Write(bSuccess.ToString().ToLower());

                                                        dblayer.AddStatusLog(company, "Creation");
                                                        //Update ServiceStatusLog Table
                                                        //CompanySerialNumber
                                                        //ActionDate
                                                        //Status
                                                        //CommercialUse

                                                        Logger.AddToLogger(Server.MapPath("."), dblayer.ErrorList);
                                                        //Response.Write("<br/>");
                                                        //Response.Write(dblayer.ErrorList);

                                                        if (bSuccess)
                                                        {
                                                            //Oded Ask to remove activation for now, until recomendation!
                                                            //SendActivationEmail(EMail, MAC, CompanyVAT);  //TODO: activation removed!
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

        //private void AddToLogger(String txt)
        //{
        //    string path = Server.MapPath(".") + @"/logger.txt";
        //    // This text is added only once to the file. 
        //    if (!File.Exists(path))
        //    {
        //        // Create a file to write to. 
        //        using (StreamWriter sw = File.CreateText(path))
        //        {
        //            //sw.WriteLine(DateTime.Now + " ==> " + txt);
        //            sw.WriteLine(DateTime.Now + " ==> Created." );
        //        }
        //    }

        //    // This text is always added, making the file longer over time 
        //    // if it is not deleted. 
        //    using (StreamWriter sw = File.AppendText(path))
        //    {
        //        sw.WriteLine(DateTime.Now);
        //        sw.WriteLine("");
        //        sw.WriteLine(txt);
        //        sw.WriteLine("");
        //        sw.WriteLine("");
        //    }

        ////// Open the file to read from. 
        ////using (StreamReader sr = File.OpenText(path))
        ////{
        ////    string s = "";
        ////    while ((s = sr.ReadLine()) != null)
        ////    {
        ////        Console.WriteLine(s);
        ////    }
        ////}
        //}

        //private static byte[] bytes = null;
        ///// <summary>
        ///// Encrypt a string.
        ///// </summary>
        ///// <param name="originalString">The original string.</param>
        ///// <returns>The encrypted string.</returns>
        ///// <exception cref="ArgumentNullException">This exception will be thrown when the original string is null or empty.</exception>
        ////public static string Encrypt(string originalString)
        //public string Encrypt(string originalString)
        //{
        //    if (String.IsNullOrEmpty(originalString))
        //    {
        //        throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
        //    }

        //    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

        //    MemoryStream memoryStream = new MemoryStream();

        //    try
        //    {
        //        CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
        //        StreamWriter writer = new StreamWriter(cryptoStream);

        //        writer.Write(originalString);
        //        writer.Flush();

        //        cryptoStream.FlushFinalBlock();
        //        writer.Flush();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logger("EncryptErr", ex.Message);
        //    }
        //    String convertedString = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        //    //Logger("convertedString", convertedString);
        //    return convertedString;
        //}

        ///// <summary>
        ///// Decrypt a crypted string.
        ///// </summary>
        ///// <param name="cryptedString">The crypted string.</param>
        ///// <returns>The decrypted string.</returns>
        ///// <exception cref="ArgumentNullException">This exception will be thrown when the crypted string is null or empty.</exception>
        ////public static string Decrypt(string cryptedString)
        //public string Decrypt(string cryptedString)
        //{
        //    if (String.IsNullOrEmpty(cryptedString))
        //    {
        //        throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
        //    }

        //    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        //    MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
        //    CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
        //    StreamReader reader = new StreamReader(cryptoStream);

        //    return reader.ReadToEnd();
        //}

        private static byte[] bytes = null;
        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="originalString">The original string.</param>
        /// <returns>The encrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown when the original string is null or empty.</exception>
        //public static string Encrypt(string originalString)
        public string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            MemoryStream memoryStream = new MemoryStream();

            try
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cryptoStream);

                writer.Write(originalString);
                writer.Flush();

                cryptoStream.FlushFinalBlock();
                writer.Flush();
            }
            catch (Exception ex)
            {
                //Logger("EncryptErr", ex.Message);
            }
            String convertedString = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            //Logger("convertedString", convertedString);
            return convertedString;
        }

        /// <summary>
        /// Decrypt a crypted string.
        /// </summary>
        /// <param name="cryptedString">The crypted string.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown when the crypted string is null or empty.</exception>
        //public static string Decrypt(string cryptedString)
        public string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
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

        private void SendActivationEmail(String EMailTo, String MAC, String VAT)
        {
            String strKey = "yanivzzohar"; //HttpUtility.UrlEncode(Encrypt(MAC)); //xezp3avnniqyjf45wso0ot45
            char[] delimiterChars = { '|' };
            bytes = ASCIIEncoding.ASCII.GetBytes((strKey).Substring(0, 8));

            //MAPI mapi = mapi = new MAPI();            
            String stEMail = EMailTo;
            String stSubject = "Your activation your email account for SimpliciTools";
            String stBodyFile = "Thank you for purchasing SimplicTools item" + Environment.NewLine;
            stBodyFile += "Global Box." + Environment.NewLine;

            //Uri.EscapeUriString(Encrypt(PCSignature))
            //P1=" + Encrypt(strKey) + "
            stBodyFile += "http://212.150.1.51/GlobalInfoProtocol/Activation.aspx?P1=" + Uri.EscapeUriString(EncodeTo64(MAC)) + "&P2=" + Uri.EscapeUriString(EncodeTo64(EMailTo)) + "&P3=" + Uri.EscapeUriString(EncodeTo64(VAT));
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


//if ((UserName != null) && (UserName != ""))
//{
//    if ((Password != null) && (Password != ""))
//    {
//if ((CompanyLogo != null) /*&& (CompanyLogo != "")*/)
//{
//if ((Active != null) && (Active != ""))
//{
//}
//}
//}
//}
