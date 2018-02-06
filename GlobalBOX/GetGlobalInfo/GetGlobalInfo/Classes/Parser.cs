using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using Pulsar.Classes;
using System.Configuration;

namespace Pulsar
{
    public class Parser
    {
        public Parser(HostType host)
        {
            SetHost(host);
        }

        public const String KEY = "xezp3avnniqyjf45wso0ot45";

        public String GetFileNameForDownload(String CountryID, String CompanyVAT, String TransactionGUID)
        {
            return CommandExecute(host, "GetGUIDFileExt.aspx?CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT + "&TransactionGUID=" + TransactionGUID);
        }

        public ArrayList GetInternalData(String data)
        {
            ArrayList AllData = CreateRowClasses(data);
            return AllData;
        }

        //public ArrayList GetData(String CountryID, String CompanyVAT, String ReadCode, String WriteCode, String CompanySerialNumber)
        public ArrayList GetData(CompanyInfo company_info)
        {
            //String data = GetDataFromWeb(company_info.CompanyCountryID.ToString(), company_info.CompanyVAT, company_info.ReadCode, company_info.WriteCode, company_info.CompanySerialNumber);
            String data = GetDataFromWeb(company_info);
            ArrayList AllData = CreateRowClasses(data);
            return AllData;
        }

        /// <summary>     
        /// returns the mac address of the first operation nic found.  
        /// /// </summary>     /// <returns></returns>    
        private string GetMacAddress()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        macAddresses = nic.GetPhysicalAddress().ToString();
                        //break;
                    }
                }
            }
            return macAddresses;
        }

        public String host { get; set; }

        public enum HostType
        {
            localHost = 1,
            realHost = 2
        }

        //host = @"https://www.misradit.info/GlobalInfoProtocol/";

        public void SetHost(HostType hostType)
        {
            host = ConfigurationManager.AppSettings["ServerUrl"];
            //switch (hostType)
            //{
            //    case HostType.localHost:
            //        host = @"http://localhost:3305/";
            //        break;
            //    case HostType.realHost:
            //        //host = @"http://212.150.1.51/GlobalInfoProtocol/";
            //        host = @"http://10.9.10.250/GlobalInfoProtocol/";
            //        break;
            //    default:
            //        break;
            //}
        }

        public bool Ping()
        {
            var response = CommandExecute(host, "Ping.aspx?");
            return !string.IsNullOrEmpty(response) && response.ToLower() == "pong";
        }

        public bool GetCMailBox(String CMailBoxInstallID)
        {
            return (CommandExecute(host, "GetTermUse.aspx?CMailBoxInstallID=" + CMailBoxInstallID).ToLower() == "ok");
        }

        public void CreateCMailBox(String CMailBoxInstallID, String TermUse)
        {
            CommandExecute(host, "AddCMailBox.aspx?CMailBoxInstallID=" + CMailBoxInstallID + "&TermUse=" + TermUse);
        }

        public bool IsClientBlocked(String CountryIDBlocked, String CompanyVATBlocked, String CountryIDRequest, String CompanyVATRequest)
        {
            return (CommandExecute(host, "IsClientBlockedForCompany.aspx?CountryIDBlocked=" + CountryIDBlocked + "&CompanyVATBlocked=" + CompanyVATBlocked + "&CountryIDRequest=" + CountryIDRequest + "&CompanyVATRequest=" + CompanyVATRequest).ToLower() == true.ToString().ToLower());
        }

        public void BlockClient(String CountryIDBlocked, String CompanyVATBlocked, String CountryIDRequest, String CompanyVATRequest, String ReadCode)
        {
            CommandExecute(host, "BlockClientForCompany.aspx?CountryIDBlocked=" + CountryIDBlocked + "&CompanyVATBlocked=" + CompanyVATBlocked + "&CountryIDRequest=" + CountryIDRequest + "&CompanyVATRequest=" + CompanyVATRequest + "&ReadCode=" + ReadCode);
        }

        public void UnBlockClient(String CountryIDBlocked, String CompanyVATBlocked, String CountryIDRequest, String CompanyVATRequest, String ReadCode)
        {
            CommandExecute(host, "UnBlockClientForCompany.aspx?CountryIDBlocked=" + CountryIDBlocked + "&CompanyVATBlocked=" + CompanyVATBlocked + "&CountryIDRequest=" + CountryIDRequest + "&CompanyVATRequest=" + CompanyVATRequest + "&ReadCode=" + ReadCode);
        }

        public bool IsTransactionReaden(String TransactionGUID)
        {
            return (CommandExecute(host, "TransactionConfirmedReaden.aspx?TransactionGUID=" + TransactionGUID) == true.ToString().ToLower());
        }

        public void AddData(String TransactionGUID, String CountryID, String CompanyVAT, String CountryIDTo, String CompanyVATTo, String WriteCode, String Data, String CompanySerialNumber)
        {
            CommandExecute(host, "AddData.aspx?TransactionGUID=" + TransactionGUID + "&CountryIDFrom=" + CountryID + "&CompanyVATFrom=" + CompanyVAT + "&CountryIDTo=" + CountryIDTo + "&CompanyVATTo=" + CompanyVATTo + "&WriteCode=" + WriteCode + "&Data=" + Uri.EscapeDataString(Data) + "&CompanySerialNumber=" + CompanySerialNumber);
        }

        public void AddData(String TransactionGUID, String CountryID, String CompanyVAT, String CountryIDTo, String CompanyVATTo, String WriteCode, String Data, String CompanySerialNumber, String FileName)
        {
            CommandExecuteFileTransfer(host, "AddData.aspx?TransactionGUID=" + TransactionGUID + "&CountryIDFrom=" + CountryID + "&CompanyVATFrom=" + CompanyVAT + "&CountryIDTo=" + CountryIDTo + "&CompanyVATTo=" + CompanyVATTo + "&WriteCode=" + WriteCode + "&Data=" + Uri.EscapeDataString(Data) + "&CompanySerialNumber=" + CompanySerialNumber, FileName);
        }

        public void AddRequest(String TransactionGUID, String CountryID, String CompanyVAT, String CountryIDTo, String CompanyVATTo, String Data, String CompanySerialNumber)
        {
            CommandExecute(host, "AddRequest.aspx?TransactionGUID=" + TransactionGUID + "&CountryIDFrom=" + CountryID + "&CompanyVATFrom=" + CompanyVAT + "&CountryIDTo=" + CountryIDTo + "&CompanyVATTo=" + CompanyVATTo + "&Data=" + Uri.EscapeDataString(Data) + "&CompanySerialNumber=" + CompanySerialNumber);
        }

        public void AddRequest(String TransactionGUID, String CountryID, String CompanyVAT, String CountryIDTo, String CompanyVATTo, String Data, String CompanySerialNumber, String FileName)
        {
            CommandExecuteFileTransfer(host, "AddRequest.aspx?TransactionGUID=" + TransactionGUID + "&CountryIDFrom=" + CountryID + "&CompanyVATFrom=" + CompanyVAT + "&CountryIDTo=" + CountryIDTo + "&CompanyVATTo=" + CompanyVATTo + "&Data=" + Uri.EscapeDataString(Data) + "&CompanySerialNumber=" + CompanySerialNumber, FileName);
        }

        public void DeleteCompany(String CountryID, String CompanyVAT, String ReadCode, String WriteCode, String EMail)
        {
            CommandExecute(host, "DeleteCompany.aspx?CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT + "&ReadCode=" + ReadCode + "&WriteCode=" + WriteCode + "&EMail=" + EMail); //+ "&MAC=" + GetMacAddress()
        }

        //public void UpdateCompany(String CountryID, String CompanyVAT, String CompanyName, String ReadCode, String WriteCode, String EMail)
        //{
        //    CommandExecute(host, "UpdateCompany.aspx?CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT + "&CompanyName=" + CompanyName + "&ReadCode=" + ReadCode + "&WriteCode=" + WriteCode + "&EMail=" + EMail + "&MAC=" + GetMacAddress());
        //}

        public void StopService(CompanyInfo Company_Info)
        {
            ServiceAction(Company_Info, "Stop");
        }

        public void StartService(CompanyInfo Company_Info)
        {
            ServiceAction(Company_Info, "Start");
        }

        public void ServiceAction(CompanyInfo Company_Info, String Action)
        {
            CommandExecute(host, "ServiceAction.aspx?CountryID=" + Company_Info.CompanyCountryID + "&CompanyVAT=" + Company_Info.CompanyVAT + "&CompanyName=" + Uri.EscapeDataString(Company_Info.CompanyName) + "&ReadCode=" + Company_Info.ReadCode + "&WriteCode=" + Company_Info.WriteCode + "&EMail=" + Company_Info.EMail + "&Action=" + Action);
        }

        public void InvatationSend(CompanyInfo Company_Info, String EMail)
        {
            CommandExecute(host, "InvatationSend.aspx?CountryID=" + Company_Info.CompanyCountryID + "&CompanyVAT=" + Company_Info.CompanyVAT + "&CompanyName=" + Uri.EscapeDataString(Company_Info.CompanyName) + "&EMail=" + EMail);
        }

        public void UpdatePayment(CompanyInfo Company_Info, bool bTermOfUse, String Payment)
        {
            CommandExecute(host, "UpdatePayment.aspx?CountryID=" + Company_Info.CompanyCountryID + "&CompanyVAT=" + Company_Info.CompanyVAT + "&CompanyName=" + Uri.EscapeDataString(Company_Info.CompanyName) + "&ReadCode=" + Company_Info.ReadCode + "&WriteCode=" + Company_Info.WriteCode + "&EMail=" + Company_Info.EMail + "&TermUse=" + (bTermOfUse ? "company" : "home") + "&Payment=" + Payment);
        }

        //public void CreateCompany(String CountryID, String CompanyVAT, String CompanyName, String ReadCode, String WriteCode, String EMail, String CompanySerialNumber)
        public void UpdateCompany(CompanyInfo Company_Info, String CountryID, String CompanyVAT, String CompanyName, String ReadCode, String WriteCode, String EMail, bool bTermOfUse, String MobilePhone, bool InformMyMobile)
        {
            CommandExecute(host, "UpdateCompany.aspx?CountryID=" + Company_Info.CompanyCountryID + "&CompanyVAT=" + Company_Info.CompanyVAT + "&CompanyName=" + Uri.EscapeDataString(Company_Info.CompanyName) + "&ReadCode=" + Company_Info.ReadCode + "&WriteCode=" + Company_Info.WriteCode + "&EMail=" + Company_Info.EMail + "&NewCountryID=" + CountryID + "&NewCompanyVAT=" + CompanyVAT + "&NewCompanyName=" + Uri.EscapeDataString(CompanyName) + "&NewReadCode=" + ReadCode + "&NewWriteCode=" + WriteCode + "&NewEMail=" + EMail + "&TermUse=" + (bTermOfUse ? "company" : "home") + "&MobilePhone=" + MobilePhone + "&InformMyMobile=" + InformMyMobile);
        }

        public void CreateCompany(CompanyInfo Company_Info, String Payment, bool bTermOfUse)
        {
            CommandExecute(host, "AddCompany.aspx?CountryID=" + Company_Info.CompanyCountryID + "&CompanyVAT=" + Company_Info.CompanyVAT + "&CompanyName=" + Company_Info.CompanyName + "&ReadCode=" + Company_Info.ReadCode + "&WriteCode=" + Company_Info.WriteCode + "&EMail=" + Company_Info.EMail + "&CompanySerialNumber=" + Company_Info.CompanySerialNumber + "&MAC=" + GetMacAddress() + "&Payment=" + Payment + "&TermUse=" + (bTermOfUse ? "company" : "home") + "&MobilePhone=" + Company_Info.MobilePhone + "&InformMyMobile=" + Company_Info.InformMyMobile + "&Commercial=" ); // + "&MAC=" + GetMacAddress()
        }

        //public String GetNotPaid(String CountryID, String CompanyVAT, String ReadCode, String WriteCode, String CompanySerialNumber)
        public String GetNotPaid(CompanyInfo company_info)
        {
            return (CommandExecute(host, "GetNotPaid.aspx?CountryID=" + company_info.CompanyCountryID + "&CompanyVAT=" + company_info.CompanyVAT + "&MAC=" + GetMacAddress() + "&Read=" + company_info.ReadCode + "&Write=" + company_info.WriteCode + "&CompanySerialNumber=" + company_info.CompanySerialNumber));
        }

        public bool IsCompanyPaid(String CountryID, String CompanyVAT)
        {
            return (CommandExecute(host, "IsCompanyPaid.aspx?CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT).ToLower() == "ok");
        }

        //TODO: does not work when server is offline
        public bool IsCompanyCommercial(String CountryID, String CompanyVAT)
        {
            if (!Program.IsServerOnline) return false;
            return (CommandExecute(host, "IsCompanyCommercial.aspx?CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT).ToLower() == "ok");
        }

        public String IsCompanyExist(String CountryID, String CompanyVAT)
        {
            return (CommandExecute(host, "IsCompanyExist.aspx?CountryID=" + CountryID + "&CompanyVAT=" + CompanyVAT));
        }

        public String IsActiveCompany(CompanyInfo company_info) //String CountryID, String CompanyVAT, String ReadCode, String WriteCode
        {
            return CommandExecute(host, "IsActive.aspx?CountryID=" + company_info.CompanyCountryID + "&CompanyVAT=" + company_info.CompanyVAT + "&MAC=" + GetMacAddress() + "&Read=" + company_info.ReadCode + "&Write=" + company_info.WriteCode);
        }

        //private String GetDataFromWeb(String CountryID, String CompanyVAT, String ReadCode, String WriteCode, String CompanySerialNumber)
        private String GetDataFromWeb(CompanyInfo company_info)
        {
            return CommandExecute(host, "GetData.aspx?CountryID=" + company_info.CompanyCountryID + "&CompanyVAT=" + company_info.CompanyVAT + "&MAC=" + GetMacAddress() + "&Read=" + company_info.ReadCode + "&Write=" + company_info.WriteCode + "&CompanySerialNumber=" + company_info.CompanySerialNumber);
        }

        public String ConfirmTransaction(String TransactionGUID, String WriteCode)
        {
            return CommandExecute(host, "TransactionReaden.aspx?TransactionGUID=" + TransactionGUID + "&Write=" + WriteCode);
        }

        public String GetBillingForThisMonth(CompanyInfo company_info)
        {
            return CommandExecute(host, "GetBillingForThisMonth.aspx?CountryID=" + company_info.CompanyCountryID + "&CompanyVAT=" + company_info.CompanyVAT + "&MAC=" + GetMacAddress() + "&Read=" + company_info.ReadCode + "&Write=" + company_info.WriteCode + "&CompanySerialNumber=" + company_info.CompanySerialNumber);
        }

        public String IsCompanyHasCCToPay(CompanyInfo company_info)
        {
            return CommandExecute(host, "IsCompanyHasCCToPay.aspx?CountryID=" + company_info.CompanyCountryID + "&CompanyVAT=" + company_info.CompanyVAT + "&MAC=" + GetMacAddress() + "&Read=" + company_info.ReadCode + "&Write=" + company_info.WriteCode + "&CompanySerialNumber=" + company_info.CompanySerialNumber);
        }

        public String GetWaitingMails(CompanyInfo company_info)
        {
            return CommandExecute(host, "GetWaitingMails.aspx?CountryID=" + company_info.CompanyCountryID + "&CompanyVAT=" + company_info.CompanyVAT + "&MAC=" + GetMacAddress() + "&Read=" + company_info.ReadCode + "&Write=" + company_info.WriteCode + "&CompanySerialNumber=" + company_info.CompanySerialNumber);
        }

        public String TransFTPToDB(CompanyInfo company_info, String FileName)
        {
            WebClient myWebClient = new WebClient();            
            string _path = Application.StartupPath;

            myWebClient.UploadFile(_path, FileName);

            return CommandExecute(host, "TransFTPToDB.aspx?CountryID=" + company_info.CompanyCountryID + "&CompanyVAT=" + company_info.CompanyVAT + "&FileName=" + FileName);
        }

        public String SiteURL { get; set; }

        private string CommandExecuteFileTransfer(string url, string sqlCommand, string fileName)
        {
            web_content = null;
            var urlWithData = new Uri(url + sqlCommand + "&LoginKey=" + KEY);

            WebClient client = new WebClient();
            byte[] responseBinary = client.UploadFile(urlWithData, fileName);
            web_content = Encoding.UTF8.GetString(responseBinary);

            return web_content;
        } 

        private String CommandExecute(String URL, String SqlCommand)
        {
            //return null;

            web_content = null;
            WebClient client = new WebClient();
            try
            {
                web_content = client.DownloadString(new Uri(URL + SqlCommand + "&LoginKey=" + KEY));
                if (web_content.ToLower().IndexOf("<html>") != -1)
                    web_content = null;
            }
            catch (Exception e)
            {
                web_content = null;
            }

            //// Create a WebBrowser instance. 
            //WebBrowser webBrowser = new WebBrowser();
            //// Add an event handler that prints the document after it loads.
            //webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(SurfedDocument);

            //// Set the Url property to load the document.
            //webBrowser.Url = new Uri(URL + SqlCommand + "&LoginKey=" + KEY);

            //while (webBrowser.IsBusy)
            //{
            //    Application.DoEvents();
            //}

            //while (web_content == null)
            //{
            //    Application.DoEvents();
            //}

            //if (web_content.ToLower().IndexOf("<html>") != -1)
            //{
            //    web_content = null;
            //}

            return web_content;
        }

        public String web_content { get; set; }

        private void SurfedDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            web_content = ((System.Windows.Forms.WebBrowser)(sender)).DocumentText;
            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }

        //private String CommandExecute(String URL, String SqlCommand)
        //{
        //    //bool b = (SqlCommand.ToLower().IndexOf("iscompanyexist.aspx".ToLower()) != -1);

        //    String retVal = null;
        //    //SqlCommand = SqlCommand.Substring(0, SqlCommand.Length - 1);
        //    //MessageBox.Show("CommandExecute");
        //    try
        //    {
        //        //if (b)
        //            //MessageBox.Show(URL + SqlCommand + "&LoginKey=" + KEY);

        //        Application.DoEvents();
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + SqlCommand + "&LoginKey=" + KEY/*+ "&LoginKey=xezp3avnniqyjf45wso0ot45"*/);
        //        //if (b)
        //            //MessageBox.Show("request");
        //        //
        //        // Set some reasonable limits on resources used by this request
        //        request.MaximumAutomaticRedirections = 4;
        //        //request.MaximumResponseHeadersLength = 4;
        //        // Set credentials to use for this request.
        //        request.Credentials = CredentialCache.DefaultCredentials;
        //        //if (b)
        //        //    MessageBox.Show("Credentials");

        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        //if (b)
        //        //    MessageBox.Show("GetRespons");

        //        //Console.WriteLine("Content length is {0}", response.ContentLength);
        //        //Console.WriteLine("Content type is {0}", response.ContentType);

        //        // Get the stream associated with the response.
        //        Stream receiveStream = response.GetResponseStream();
        //        //if (b)
        //        //    MessageBox.Show("GetResponseStream");

        //        // Pipes the stream to a higher level stream reader with the required encoding format. 
        //        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

        //        String result = readStream.ReadToEnd();
        //        //if (b)
        //        //    MessageBox.Show("result=" + result);
        //        //result = CleanRowsWithFrodNineKey(result);

        //        if ((result != null) && (result != ""))
        //        {
        //            retVal = result;
        //            //if (b)
        //            //    MessageBox.Show("retVal=" + result);
        //            //    try
        //            //    {
        //            //        Clipboard.SetText(result, TextDataFormat.UnicodeText);
        //            //    }
        //            //    catch (Exception ex)
        //            //    {
        //            //    }
        //        }
        //        //if (b)
        //        //    MessageBox.Show("end request");
        //        Application.DoEvents();

        //        response.Close();
        //        readStream.Close();
        //        //if (b)
        //        //    MessageBox.Show("close request");
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (b)
        //        //    MessageBox.Show("ex=" + ex.Message);
        //    }
        //    //if (b)
        //    //    MessageBox.Show("retVal=" + retVal);
        //    return retVal;
        //}

        //private String CommandExecute(String URL, String SqlCommand, int timeout)
        //{
        //    String retVal = null;
        //    //SqlCommand = SqlCommand.Substring(0, SqlCommand.Length - 1);
        //    //MessageBox.Show("CommandExecute");
        //    try
        //    {
        //        //MessageBox.Show(URL + SqlCommand + "&LoginKey=" + KEY);
        //        Application.DoEvents();
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + SqlCommand + "&LoginKey=" + KEY/*+ "&LoginKey=xezp3avnniqyjf45wso0ot45"*/);
        //        request.Timeout = timeout;
        //        //
        //        // Set some reasonable limits on resources used by this request
        //        request.MaximumAutomaticRedirections = 4;
        //        //request.MaximumResponseHeadersLength = 4;
        //        // Set credentials to use for this request.
        //        request.Credentials = CredentialCache.DefaultCredentials;

        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //        Console.WriteLine("Content length is {0}", response.ContentLength);
        //        Console.WriteLine("Content type is {0}", response.ContentType);

        //        // Get the stream associated with the response.
        //        Stream receiveStream = response.GetResponseStream();

        //        // Pipes the stream to a higher level stream reader with the required encoding format. 
        //        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

        //        String result = readStream.ReadToEnd();
        //        //MessageBox.Show("result=" + result);
        //        //result = CleanRowsWithFrodNineKey(result);

        //        if ((result != null) && (result != ""))
        //        {
        //            retVal = result;
        //            //    try
        //            //    {
        //            //        Clipboard.SetText(result, TextDataFormat.UnicodeText);
        //            //    }
        //            //    catch (Exception ex)
        //            //    {

        //            //    }
        //        }
        //        //MessageBox.Show("end request");
        //        Application.DoEvents();

        //        response.Close();
        //        readStream.Close();
        //        //MessageBox.Show("close request");
        //    }
        //    catch (Exception ex)
        //    {
        //        retVal = "timeout";
        //    }
        //    //MessageBox.Show("retVal=" + retVal);
        //    return retVal;
        //}

        private ArrayList CreateRowClasses(String AllLines)
        {
            ArrayList result = new ArrayList();

            if (AllLines != null)
            {
                string[] lines = Regex.Split(AllLines, "\r\n"); //, StringSplitOptions.RemoveEmptyEntries

                string[] delimiters = new string[] { "|$" };

                foreach (string line in lines)
                {
                    if (line != "")
                    {
                        string[] parts = line.Split(delimiters, StringSplitOptions.None);
                        GetKoteretTnua(parts, result);
                        GetTochenTnua(parts, result);
                        GetWriteCodeRequest(parts, result);
                    }
                }
            }

            return result;
        }

        private void GetWriteCodeRequest(string[] parts, ArrayList result)
        {
            if (parts[5].Substring(0, 2) == "WR")
            {
                char[] dataDelimiters = new char[] { '|' };

                WriteCodeRequest wr = new WriteCodeRequest();
                wr.TransactionGUID = parts[0].ToString();
                wr.CountryIDFrom = Int32.Parse(parts[1].ToString());
                wr.VatFrom = parts[2].ToString();
                wr.CountryIDTo = Int32.Parse(parts[3].ToString());
                wr.VatTo = parts[4].ToString();
                //wr.Data = parts[5].ToString(); //"WR|99Request Write Code";
                try
                {
                    if ((parts[6].ToString() != null) && (parts[6].ToString() != ""))
                    {
                        wr.TimeStampWrite = ConvertToDDMMYYYY(parts[6].ToString());
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if ((parts[7].ToString() != null) && (parts[7].ToString() != ""))
                    {
                        wr.TimeStampRead = ConvertToDDMMYYYY(parts[7].ToString());
                    }
                }
                catch (Exception)
                {
                }

                string[] dataParts = parts[5].ToString().Split(dataDelimiters, StringSplitOptions.None);

                bool bIsValidRecord = true;

                wr.Data = parts[5];

                for (int i = 1; i < dataParts.Length; i++)
                {
                    bIsValidRecord = true;
                    if (dataParts[i].ToString().Length >= FiledNumberPrefixLength)
                    {
                        int ValidFieldNumber = GetNumeric(dataParts[i].ToString().Substring(0, FiledNumberPrefixLength));
                        if (ValidFieldNumber != -1)
                        {
                            String FieldValue = dataParts[i].ToString().Substring(FiledNumberPrefixLength, dataParts[i].ToString().Length - FiledNumberPrefixLength);
                            switch (ValidFieldNumber)
                            {
                                case 7:
                                    wr.ShemHaLakoh = FieldValue;                //שם הלקוח
                                    break;
                                case 8:
                                    wr.OsekMoorshehLakoh = FieldValue;          //עוסק מורשה לקוח
                                    break;
                                case 15:
                                    wr.ShemHaSholeah = FieldValue;              //שם השולח
                                    break;
                                case 16:
                                    wr.OsekMoorshehHaSholeah = FieldValue;      //עוסק מורשה השולח
                                    break;
                                case 17:
                                    wr.CountryIDHaSholeah = FieldValue;         //קוד מדינת השולח
                                    break;
                                case 98:
                                    wr.Answer = FieldValue;                     //תשובה
                                    break;
                                case 99:
                                    wr.Message = FieldValue;                    //הודעה
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            bIsValidRecord = false;
                            break;
                        }
                    }
                }

                if (bIsValidRecord)
                {
                    result.Add(wr);
                }
            }
        }

        private const int FiledNumberPrefixLength = 2;

        public void GetKoteretTnua(String[] parts, ArrayList result)
        {
            if (parts[5].Substring(0, 2) == "KT")
            {
                char[] dataDelimiters = new char[] { '|' };

                KoteretTnua kt = new KoteretTnua();

                kt.TransactionGUID = parts[0].ToString();
                kt.CountryIDFrom = Int32.Parse(parts[1].ToString());
                kt.VatFrom = parts[2].ToString();
                kt.CountryIDTo = Int32.Parse(parts[3].ToString());
                kt.VatTo = parts[4].ToString();

                if (parts.Length == 18)
                {
                    kt.Confirm0 = Boolean.Parse(parts[15].ToString());
                    kt.Confirm1 = Boolean.Parse(parts[16].ToString());
                    kt.Confirm2 = Boolean.Parse(parts[17].ToString());
                }

                try
                {
                    if ((parts[6].ToString() != null) && (parts[6].ToString() != ""))
                    {
                        //kt.TimeStampRead = ConvertToDDMMYYYY(parts[6].ToString().Substring(0, 19));
                        kt.TimeStampWrite = ConvertToDDMMYYYY(parts[6].ToString());
                    }
                }
                catch (Exception)
                {

                }

                try
                {
                    if ((parts[7].ToString() != null) && (parts[7].ToString() != ""))
                    {
                        //kt.TimeStampWrite = ConvertToDDMMYYYY(parts[7].ToString().Substring(0, 19));
                        kt.TimeStampRead = ConvertToDDMMYYYY(parts[7].ToString());
                    }
                }
                catch (Exception)
                {
                }

                string[] dataParts = parts[5].ToString().Split(dataDelimiters, StringSplitOptions.None);

                bool bIsValidRecord = true;

                kt.Data = parts[5];

                for (int i = 1; i < dataParts.Length; i++)
                {
                    bIsValidRecord = true;
                    if (dataParts[i].ToString().Length >= FiledNumberPrefixLength)
                    {
                        int ValidFieldNumber = GetNumeric(dataParts[i].ToString().Substring(0, FiledNumberPrefixLength));
                        if (ValidFieldNumber != -1)
                        {
                            String FieldValue = dataParts[i].ToString().Substring(FiledNumberPrefixLength, dataParts[i].ToString().Length - FiledNumberPrefixLength);
                            switch (ValidFieldNumber)
                            {
                                case 1:
                                    kt.MisparPnimi = Int32.Parse(FieldValue);              //מספר פנימי
                                    break;
                                case 2:
                                    kt.MisparMismach = Int32.Parse(FieldValue);            //מספר מסמך
                                    break;
                                case 3:
                                    kt.TarichMismach = ConvertToDDMMYYYY(FieldValue);      //תאריך מסמך
                                    break;
                                case 4:
                                    kt.TarichKovea_Divuch = ConvertToDDMMYYYY(FieldValue); //תאריך קובע/דיווח
                                    break;
                                case 5:
                                    kt.TarichMishloah = ConvertToDDMMYYYY(FieldValue);    //תאריך משלוח
                                    break;
                                case 6:
                                    kt.TarichAher = ConvertToDDMMYYYY(FieldValue);        //תאריך אחר
                                    break;
                                case 7:
                                    kt.ShemHaLakoh = FieldValue;                          //שם הלקוח
                                    break;
                                case 8:
                                    kt.OsekMoorshehLakoh = FieldValue;                    //עוסק מורשה לקוח
                                    break;
                                case 9:
                                    kt.MeidaNosaf = FieldValue;                           //מידע נוסף
                                    break;
                                case 10:
                                    kt.SchumLifneMaam = Double.Parse(FieldValue);         //סכום לפני מע"מ
                                    break;
                                case 11:
                                    kt.SchumPaturMeMaam = Double.Parse(FieldValue);       //סכום פטור ממע"מ
                                    break;
                                case 12:
                                    kt.SchumHaMaam = Double.Parse(FieldValue);            //סכום המע"מ
                                    break;
                                case 13:
                                    kt.SchumKolelMaam = Double.Parse(FieldValue);         //סכום כולל מע"מ
                                    break;
                                case 14:
                                    kt.SugTnua = FieldValue;                              //סוג תנועה
                                    break;
                                case 15:
                                    kt.ShemHaSholeah = FieldValue;                        //שם השולח
                                    break;
                                case 16:
                                    kt.OsekMoorshehHaSholeah = FieldValue;                //עוסק מורשה השולח
                                    break;
                                case 17:
                                    kt.CountryIDHaSholeah = FieldValue;                   //קוד מדינת השולח
                                    break;
                                case 18:
                                    kt.Maam = Double.Parse(FieldValue);                   //מע"מ
                                    break;
                                case 19:
                                    kt.LeTkufaMe = DateTime.Parse("01/" + FieldValue.Substring(0, 2) + "/" + FieldValue.Substring(2, 4));    //מתקופה
                                    break;
                                case 20:
                                    kt.LeTkufaUd = DateTime.Parse("01/" + FieldValue.Substring(0, 2) + "/" + FieldValue.Substring(2, 4));    //עד תקופה
                                    break;
                                case 21:
                                    kt.MisparProyect = FieldValue;                        //מספר פרוייקט
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            bIsValidRecord = false;
                            break;
                        }
                    }
                }

                if (bIsValidRecord)
                {
                    result.Add(kt);
                }
            }
        }


        public void GetTochenTnua(String[] parts, ArrayList result)
        {
            if (parts[5].Substring(0, 2) == "TT")
            {
                char[] dataDelimiters = new char[] { '|' };

                TochenTnua tt = new TochenTnua();

                tt.TransactionGUID = parts[0].ToString();
                tt.CountryIDFrom = Int32.Parse(parts[1].ToString());
                tt.VatFrom = parts[2].ToString();
                tt.CountryIDTo = Int32.Parse(parts[3].ToString());
                tt.VatTo = parts[4].ToString();

                //if (parts.Length == 18)
                //{
                //    kt.Confirm0 = Boolean.Parse(parts[15].ToString());
                //    kt.Confirm1 = Boolean.Parse(parts[16].ToString());
                //    kt.Confirm2 = Boolean.Parse(parts[17].ToString());
                //}

                try
                {
                    if ((parts[6].ToString() != null) && (parts[6].ToString() != ""))
                    {
                        //kt.TimeStampRead = ConvertToDDMMYYYY(parts[6].ToString().Substring(0, 19));
                        tt.TimeStampWrite = ConvertToDDMMYYYY(parts[6].ToString());
                    }
                }
                catch (Exception)
                {

                }

                try
                {
                    if ((parts[7].ToString() != null) && (parts[7].ToString() != ""))
                    {
                        //kt.TimeStampWrite = ConvertToDDMMYYYY(parts[7].ToString().Substring(0, 19));
                        tt.TimeStampRead = ConvertToDDMMYYYY(parts[7].ToString());
                    }
                }
                catch (Exception)
                {
                }

                string[] dataParts = parts[5].ToString().Split(dataDelimiters, StringSplitOptions.None);

                bool bIsValidRecord = true;

                tt.Data = parts[5];

                for (int i = 1; i < dataParts.Length; i++)
                {
                    bIsValidRecord = true;
                    if (dataParts[i].ToString().Length >= FiledNumberPrefixLength)
                    {
                        int ValidFieldNumber = GetNumeric(dataParts[i].ToString().Substring(0, FiledNumberPrefixLength));
                        if (ValidFieldNumber != -1)
                        {
                            String FieldValue = dataParts[i].ToString().Substring(FiledNumberPrefixLength, dataParts[i].ToString().Length - FiledNumberPrefixLength);
                            switch (ValidFieldNumber)
                            {
                                case 1:
                                    tt.MisparPnimi = Int32.Parse(FieldValue);               //מספר פנימי
                                    break;
                                case 22:
                                    tt.MisparShuraBaTnua = Int32.Parse(FieldValue);         //מספר שורה בתנועה
                                    break;
                                case 23:
                                    tt.CodeParitAlpha = FieldValue;                         //קוד פריט אלפה
                                    break;
                                case 24:
                                    tt.TeurParitAlpha = FieldValue;                         //תאור פריט אלפה
                                    break;
                                case 25:
                                    tt.Kamut1 = Int32.Parse(FieldValue);                    //כמות 1
                                    break;
                                case 26:
                                    tt.Kamut2 = Int32.Parse(FieldValue);                    //כמות 2
                                    break;
                                case 27:
                                    tt.Kamut3 = Int32.Parse(FieldValue);                    //כמות 3
                                    break;
                                case 28:
                                    tt.Kamut4 = Int32.Parse(FieldValue);                    //כמות 4
                                    break;
                                case 29:
                                    tt.KamutKlalit = Int32.Parse(FieldValue);               //כמות כללית
                                    break;
                                case 30:
                                    tt.TeurYahida = FieldValue;                             //תאור יחידה
                                    break;
                                case 31:
                                    tt.MechirYehida = Double.Parse(FieldValue);            //מחיר יחידה
                                    break;
                                case 32:
                                    tt.SchumLefniHanacha = Double.Parse(FieldValue);        //סכום לפני הנחה
                                    break;
                                case 33:
                                    tt.AhuzHanacha = Double.Parse(FieldValue);              //אחוז הנחה
                                    break;
                                case 34:
                                    tt.SchumHanacha = Double.Parse(FieldValue);             //סכום הנחה
                                    break;
                                case 35:
                                    tt.SchumShura = Double.Parse(FieldValue);      //סכום לאחר הנחה
                                    break;
                                case 36:
                                    tt.AhuzHaMaam = Double.Parse(FieldValue);               //אחוז המע"מ
                                    break;
                                case 37:
                                    tt.CodeMatbea = Int32.Parse(FieldValue);                //קוד מטבע
                                    break;
                                case 38:
                                    tt.SharMatbea = Double.Parse(FieldValue);               //שער מטבע
                                    break;
                                case 39:
                                    tt.MechirYehidaBeMatbea = Double.Parse(FieldValue);     //מחיר יחידה במטבע
                                    break;
                                case 40:
                                    tt.SchumShuraMatbeaLefniHanacha = Double.Parse(FieldValue); //סכום שורה מטבע לפני הנחה
                                    break;
                                case 41:
                                    tt.SchumHanachaBeMatbea = Double.Parse(FieldValue);     //סכום הנחה במטבע
                                    break;
                                case 42:
                                    tt.SchumShuraMatbeaLacharHanacha = Double.Parse(FieldValue);    //סכום שורה במטבע לאחר הנחה
                                    break;
                                case 43:
                                    tt.BarCodeParit = FieldValue;                             //בר קוד פריט
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            bIsValidRecord = false;
                            break;
                        }
                    }
                }

                if (bIsValidRecord)
                {
                    result.Add(tt);
                }
            }
        }

        private string[] delimitersSpace = new string[] { " " };
        private string[] delimitersColone = new string[] { ":" };
        private string[] delimitersSlash = new string[] { "/" };

        public DateTime ConvertToDDMMYYYY(String date)
        {
            date = date.ToLower().Replace(" pm", "");

            if (date.Length == 10)
            {
                try
                {
                    return new DateTime(Int32.Parse(date.ToString().Substring(6, 4)), Int32.Parse(date.ToString().Substring(0, 2)), Int32.Parse(date.ToString().Substring(3, 2)), 0, 0, 0);
                }
                catch (Exception)
                {
                    return new DateTime(Int32.Parse(date.ToString().Substring(6, 4)), Int32.Parse(date.ToString().Substring(3, 2)), Int32.Parse(date.ToString().Substring(0, 2)), 0, 0, 0);
                }
            }
            else
            {
                if (date.IndexOf(":") == -1)
                {
                    date += " 00:00:00";
                }

                string[] parts = date.Split(delimitersSpace, StringSplitOptions.None);
                string[] partsTime = parts[1].Split(delimitersColone, StringSplitOptions.None);

                string[] partsDate = parts[0].Split(delimitersSlash, StringSplitOptions.None);

                if (partsDate[0].Length == 1)
                {
                    partsDate[0] = "0" + partsDate[0];
                }

                if (partsDate[1].Length == 1)
                {
                    partsDate[1] = "0" + partsDate[1];
                }

                if (partsTime[0].Length == 1)
                {
                    partsTime[0] = "0" + partsTime[0];
                }

                if (partsTime[1].Length == 1)
                {
                    partsTime[1] = "0" + partsTime[1];
                }

                try
                {
                    return new DateTime(Int32.Parse(partsDate[2]), Int32.Parse(partsDate[0]), Int32.Parse(partsDate[1]), Int32.Parse(partsTime[0]), Int32.Parse(partsTime[1]), Int32.Parse(partsTime[2]));
                }
                catch (Exception)
                {
                    return new DateTime(Int32.Parse(partsDate[2]), Int32.Parse(partsDate[1]), Int32.Parse(partsDate[0]), Int32.Parse(partsTime[0]), Int32.Parse(partsTime[1]), Int32.Parse(partsTime[2]));
                }
            }
            //return Convert.ToDateTime(date.ToString().Substring(0, 2) + @"/" + date.ToString().Substring(3, 2) + @"/" + date.ToString().Substring(6, 4));
        }

        public int GetNumeric(String number)
        {
            try
            {
                return Convert.ToInt32(number);
            }
            catch (Exception)
            {
            }

            return -1;
        }
    }
}
