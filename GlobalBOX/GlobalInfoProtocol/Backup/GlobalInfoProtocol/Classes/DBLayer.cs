using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Collections;
using System.Text;
using System.Web.UI;
using GlobalInfoProtocol.Classes;
//Publish Path \\192.168.0.250\c$\Inetpub\Adirim
namespace GlobalInfoProtocol
{
    public class DBLayer
    {
        //  SqlCheckInclude.aspx
        //
        //  Author: Yaniv Zohar
        //
        //  This is the include file to use with your asp pages to 
        //  validate input for SQL injection.

        //
        //  Below is a black list that will block certain SQL commands and 
        //  sequences used in SQL injection will help with input sanitization
        //
        //  However this is may not suffice, because:
        //  1) These might not cover all the cases (like encoded characters)
        //  2) This may disallow legitimate input
        //
        //  Creating a raw sql query strings by concatenating user input is 
        //  unsafe programming practice. It is advised that you use parameterized
        //  SQL instead. Check http://support.microsoft.com/kb/q164485/ for information
        //  on how to do this using ADO from ASP.
        //
        //  Moreover, you need to also implement a white list for your parameters.
        //  For example, if you are expecting input for a zipcode you should create
        //  a validation rule that will only allow 5 characters in [0-9].
        //        
        public String ErrorPage = "/ErrorPage.aspx";

        String[] BlackList = new String[] {"--", ";", "/*", "*/", "@@", "@",
                  "char", "nchar", "varchar", "nvarchar",
                  "alter", "begin", "cast", "create", "cursor",
                  "declare", "delete", "drop", "end", "exec",
                  "execute", "fetch", "insert", "kill", "open",
                  "select", "sys", "sysobjects", "syscolumns",
                  "table", "update", "or", "and"};

        //  Populate the error page you want to redirect to in case the 
        //  check fails.

        //////////////////////////////////////////////////////////////////////////////////////////////////////               
        //  This function does not check for encoded characters
        //  since we do not know the form of encoding your application
        //  uses. Add the appropriate logic to deal with encoded characters
        //  in here 
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool CheckStringForSQL(String str)
        {
            String lstr = "";

            // If the string is empty, return true
            if (str.Trim() == "")
            {
                return false;
            }

            lstr = str.ToLower();

            // Check if the string contains any patterns in our
            // black list
            foreach (String s in BlackList)
            {
                if (lstr.IndexOf(s) != -1)
                {
                    return true;
                }
            }

            return false;

        }

        public bool CheckSQLInjection(HttpRequest Request)
        {
            if (CheckFormsData(Request))
                return true;

            if (CheckQueryString(Request))
                return true;

            if (CheckCookies(Request))
                return true;

            return false;
        }

        public bool CheckFormsData(HttpRequest Request)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////  Check forms data
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            foreach (String s in Request.Form)
            {
                if (CheckStringForSQL(Request.Form[s].ToLower()))
                {
                    //' Redirect to an error page
                    return true;
                }
            }

            return false;
        }

        public bool CheckQueryString(HttpRequest Request)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////  Check forms data
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            foreach (String s in Request.QueryString)
            {
                if (s != null)
                {
                    if (CheckStringForSQL(Request.QueryString[s].ToLower()))
                    {
                        //' Redirect to an error page
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckCookies(HttpRequest Request)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////  Check forms data
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            //foreach (HttpCookieCollection cookie in Request.Cookies)
            //{
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                for (int j = 0; j < Request.Cookies[i].Values.Count; j++)
                {
                    //subkeyName = Server.HtmlEncode(aCookie.Values.AllKeys[j]);
                    //String subkeyValue = Uri.EscapeUriString(cookie.Values[j]); //.ToString();
                    String subkeyValue = Request.Cookies[i].Values[j].ToString().ToLower();

                    if (CheckStringForSQL(subkeyValue))
                    {
                        //' Redirect to an error page
                        return true;
                    }
                }
            }
            //}

            return false;
        }

        private static OleDbConnection m_conn = null;
        private static OleDbConnection m_user_conn = null;
        public string connectionString = "";
        public string DBName { get; set; }

        public String SafeData(String text)
        {
            return text == null ? "" : text.Trim().Replace("'", "''");
        }

        public HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        //public HttpSessionState Session()
        //{            
        //    return HttpContext.Current.Session;
        //}
        //UserLogin.aspx?LoginKey=xezp3avnniqyjf45wso0ot45&UserName=adirim&Password=79315XY&SMSCount=1
        public void CreateConnectionString(String server_path)
        {
            if (server_path.Substring(0, 1) != @"\")
            {
                server_path = server_path + @"\";
            }
            //~/App_Data/SMSServer.mdb
            //D:\wwwroot\wwwroot1\isphost\misradit\misradit.info\www\SMSServer\

            //if (server_path.Substring(0, 1).ToLower() == "I".ToLower())
            //{
            //connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + server_path + @"\app_data\GlobalInfoProtocol.mdb;User Id=;Password=;";
            connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + server_path + @"app_data\GlobalInfoProtocol.mdb;User Id=;Password=;";
            DBName = server_path + @"app_data\GlobalInfoProtocol.mdb";
            //}
            //else
            //{
            //    connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\wwwroot\wwwroot1\isphost\misradit\misradit.info\www\app_data\GlobalInfoProtocol.mdb;User Id=;Password=;";
            //}

            Session["connectionString"] = connectionString;
        }

        public void CreateDynamicConnectionString(String server_path, String DBNAME)
        {
            if (server_path.Substring(0, 1) != @"\")
            {
                server_path = server_path + @"\";
            }

            if (Session["DBName"] != null)
            {
                connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + server_path + @"\app_data\" + DBNAME + ".mdb;User Id=;Password=;";
                Session["connectionString"] = connectionString;
                DBName = server_path + @"\app_data\" + DBNAME + ".mdb";
            }
        }


        public bool ExecuteNonQuery2(string SQL)
        {
            bool bRetVal = false;
            try
            {
                OdbcCommand command = new OdbcCommand(SQL);
                Open();
                command.Connection = conn;
                int result = command.ExecuteNonQuery();
                Close();
                bRetVal = true;
            }
            catch (Exception ex)
            {
                AddErr(ex.Message);
                Console.WriteLine(ex.Message);
                Close();
            }

            return bRetVal;
        }

        private String strConnection = null;
        private OdbcConnection conn = null;

        public void Open()
        {
            if ((conn == null) || (conn.State != ConnectionState.Open))
            {
                conn = new OdbcConnection(strConnection);
                conn.Open();
            }
        }

        public void Close()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void ClearDBConn()
        {
            if (m_conn != null)
            {
                if (m_conn.State == ConnectionState.Open)
                {
                    m_conn.Close();
                }
            }

            m_conn = null;
        }

        public void CloseDB()
        {
            if (m_conn != null)
            {
                if (m_conn.State == ConnectionState.Open)
                {
                    m_conn.Close();
                }
            }
        }

        public void OpenDB()
        {
            try
            {
                if (m_conn == null)
                {
                    m_conn = new OleDbConnection(connectionString);
                    m_conn.Open();
                }
                else if (m_conn.State == ConnectionState.Closed)
                {
                    m_conn.Open();
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
        }

        public void CloseUserDB()
        {
            if (m_user_conn != null)
            {
                if (m_user_conn.State == ConnectionState.Open)
                {
                    m_user_conn.Close();
                }
            }
        }

        public void OpenUserDB()
        {
            try
            {
                if ((m_user_conn == null) || (m_user_conn.State == ConnectionState.Closed))
                {
                    m_user_conn = new OleDbConnection(connectionString);
                    m_user_conn.Open();
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
        }

        public OleDbConnection GetNewConnection()
        {
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection(connectionString);
                conn.Open();

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return conn;
        }

        public String GetSQLString(String SQL, OleDbConnection conn)
        {
            OleDbCommand cmd = new OleDbCommand(SQL, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            String retVal = null;

            while (reader.Read())
            {
                retVal = reader[0].ToString();
            }

            reader.Close();

            return retVal;
        }

        public String GetSQLString(String SQL)
        {
            OleDbDataReader reader = returnData(SQL, connectionString);
            String retVal = null;

            while (reader.Read())
            {
                retVal = reader[0].ToString();
            }

            reader.Close();

            return retVal;
        }

        public ArrayList GetSQLArrayList(String SQL)
        {
            ArrayList arraylist = new ArrayList();
            OleDbDataReader reader = null;

            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                OleDbCommand cmd = new OleDbCommand(SQL, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    arraylist.Add(reader[0].ToString());
                }
                reader.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                conn.Close();
            }

            return arraylist;
        }

        public ArrayList GetSQLArrayList(String SQL, OleDbConnection conn)
        {
            ArrayList arraylist = new ArrayList();
            OleDbDataReader reader = null;

            try
            {
                OleDbCommand cmd = new OleDbCommand(SQL, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    arraylist.Add(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return arraylist;
        }

        public ArrayList GetSQLArrayList(String SQL, int ColumnIndex)
        {
            ArrayList arraylist = new ArrayList();
            OleDbDataReader reader = null;

            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                OleDbCommand cmd = new OleDbCommand(SQL, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    arraylist.Add(reader[ColumnIndex].ToString());
                }
                reader.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return arraylist;
        }

        public String ErrorList { get; set; }

        public void AddErr(String err)
        {
            ErrorList += err + "</br>";
        }

        ///////////////////////////////////////////////////////////////
        ///////////////      Blocking
        ///////////////////////////////////////////////////////////////
        public void AddCompanyBlocking(String CountryIDBlocked, String CompanyVATBlocked, String CountryIDRequest, String CompanyVATRequest)
        {
            String SQL = "INSERT INTO BlockedRequestList ( ";
            SQL += " CountryIDBlocked, CompanyVATBlocked, CountryIDRequest, CompanyVATRequest";
            SQL += "  ) VALUES (";

            SQL += "'" + CountryIDBlocked + "',";
            SQL += "'" + CompanyVATBlocked + "',";
            SQL += "'" + CountryIDRequest + "',";
            SQL += "'" + CompanyVATRequest + "'";

            SQL += ")";

            ExecuteNonQuery(SQL);
        }

        //public void UpdateBlocking(String CountryIDBlocked, String CompanyVATBlocked, String CountryIDRequest, String CompanyVATRequest)
        //{
        //    String SQL = "UPDATE BlockedRequestList SET ";

        //    SQL += " CountryIDBlocked = '" + CountryIDBlocked + "',";
        //    SQL += " CompanyVATBlocked = '" + CompanyVATBlocked + "',";
        //    SQL += " CountryIDRequest = '" + CountryIDRequest + "',";
        //    SQL += " CompanyVATRequest = '" + CompanyVATRequest + "'";

        //    ExecuteNonQuery(SQL);
        //}

        public bool IsCompanyBlocked(String CountryIDBlocked, String CompanyVATBlocked, String CountryIDRequest, String CompanyVATRequest)
        {
            String SQL = "SELECT * FROM BlockedRequestList";
            SQL += " WHERE CountryIDBlocked = " + CountryIDBlocked + "";
            SQL += " AND CompanyVATBlocked = '" + CompanyVATBlocked + "'";
            SQL += " AND CountryIDRequest = " + CountryIDRequest + "";
            SQL += " AND CompanyVATRequest = '" + CompanyVATRequest + "'";

            return GetSQLArrayList(SQL).Count > 0;
        }

        public void DeleteCompanyBlocking(String CountryIDBlocked, String CompanyVATBlocked, String CountryIDRequest, String CompanyVATRequest)
        {
            String SQL = "DELETE FROM BlockedRequestList";
            SQL += " WHERE CountryIDBlocked = " + CountryIDBlocked + "";
            SQL += " AND CompanyVATBlocked = '" + CompanyVATBlocked + "'";
            SQL += " AND CountryIDRequest = " + CountryIDRequest + "";
            SQL += " AND CompanyVATRequest = '" + CompanyVATRequest + "'";

            ExecuteNonQuery(SQL);
        }

        ///////////////////////////////////////////////////////////////
        //  Billing
        ///////////////////////////////////////////////////////////////

        private Billing CreateBilling(OleDbDataReader reader)
        {
            Billing billing = new Billing();
            billing.CompanySerialNumber = reader[0].ToString();
            billing.DateMonth = Convert.ToDateTime(reader[1].ToString());
            billing.InCounter = Int32.Parse(reader[2].ToString());
            billing.OutCounter = Int32.Parse(reader[3].ToString());
            billing.Paid = Convert.ToBoolean(reader[4].ToString());

            return billing;
        }

        //SELECT Billing.CompanySerialNumber, Sum(Billing.InCounter) AS SumOfInCounter, Sum(Billing.OutCounter) AS SumOfOutCounter
        //FROM Billing
        //GROUP BY Billing.CompanySerialNumber
        //HAVING (((Billing.CompanySerialNumber)={guid {6155ABB0-1E60-4028-9E11-B70B7D526596}}));

        public String GetNotPaidBilling(String CompanySerialNumber)
        {
            //String SQL = "SELECT CompanySerialNumber, Sum(InCounter) AS SumOfInCounter, Sum(OutCounter) AS SumOfOutCounter FROM Billing";
            //SQL += " GROUP BY CompanySerialNumber";
            //SQL += " HAVING CompanySerialNumber = '" + CompanySerialNumber + "'";
            //SQL += " AND Paid = False";

            //CompanySerialNumber, 
            String SQL = "SELECT Sum(InCounter) AS SumOfInCounter, Sum(OutCounter) AS SumOfOutCounter";
            SQL += " FROM Billing";
            SQL += " GROUP BY CompanySerialNumber, Paid";
            SQL += " HAVING CompanySerialNumber='" + CompanySerialNumber + "' AND Paid=False";

            //AddErr(SQL);
            OpenUserDB();
            //AddErr(connectionString);

            OleDbDataReader reader = returnUserData(SQL, connectionString);
            String billing = null;

            if (reader != null)
            {
                if (reader.HasRows)
                {
                    //AddErr("HasRows");
                    reader.Read();
                    billing = reader[0] + "|" + reader[1];
                    AddErr(billing);
                }

                reader.Close();
            }
            CloseUserDB();

            return billing;
        }

        public Billing GetBilling(String CompanySerialNumber, DateTime DateMonth)
        {
            String SQL = "select * From Billing";
            SQL += " WHERE CompanySerialNumber = " + CompanySerialNumber + "";
            SQL += " AND (DateMonth >= #" + DateMonth.ToString("MM/dd/yyy") + " 00:00:00# AND DateMonth <= #" + DateMonth.ToString("MM/dd/yyy") + " 23:59:59#)";

            AddErr(SQL);
            OpenUserDB();
            AddErr(connectionString);

            OleDbDataReader reader = returnUserData(SQL, connectionString);
            Billing billing = null;

            if (reader != null)
            {
                if (reader.HasRows)
                {
                    AddErr("HasRows");
                    reader.Read();
                    billing = CreateBilling(reader);
                }

                reader.Close();
            }
            CloseUserDB();

            return billing;
        }

        public Billing GetBillingForThisMonth(String CompanySerialNumber)
        {
            String SQL = "select * From Billing";
            SQL += " WHERE CompanySerialNumber = " + CompanySerialNumber + "";
            SQL += " AND (DateMonth >= #" + (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("MM/dd/yyy") + " 00:00:00#";
            SQL += " AND DateMonth <= #" + (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)).ToString("MM/dd/yyy") + " 23:59:59#)";

            AddErr(SQL);
            OpenUserDB();
            AddErr(connectionString);

            OleDbDataReader reader = returnUserData(SQL, connectionString);
            Billing billing = null;

            if (reader != null)
            {
                if (reader.HasRows)
                {
                    //AddErr("HasRows");
                    reader.Read();
                    billing = CreateBilling(reader);
                }

                reader.Close();
            }
            CloseUserDB();

            return billing;
        }

        public void AddBilling(Billing billing)
        {
            String SQL = "INSERT INTO Billing ( ";
            SQL += " CompanySerialNumber, DateMonth, InCounter, OutCounter, Paid";
            SQL += "  ) VALUES (";

            SQL += "'" + billing.CompanySerialNumber + "',";
            SQL += "'" + billing.DateMonth + "',";
            SQL += "'" + billing.InCounter + "',";
            SQL += "'" + billing.OutCounter + "',";
            SQL += "'" + (billing.Paid ? 1 : 0) + "'";

            SQL += ")";

            ExecuteNonQuery(SQL);
        }

        public bool UpdateBilling(Billing billing, DateTime DateMonth)
        {
            String SQL = "UPDATE Billing SET ";

            SQL += " CompanySerialNumber = '" + billing.CompanySerialNumber + "',";
            SQL += " DateMonth = '" + billing.DateMonth + "',";
            SQL += " InCounter = '" + billing.InCounter + "',";
            SQL += " OutCounter = '" + billing.OutCounter + "',";
            SQL += " Paid = '" + (billing.Paid ? 1 : 0) + "' ";

            SQL += " WHERE CompanySerialNumber = '" + billing.CompanySerialNumber + "'";
            SQL += " AND (DateMonth >= #" + DateMonth.ToString("MM/dd/yyy") + " 00:00:00# AND DateMonth <= #" + DateMonth.ToString("MM/dd/yyy") + " 23:59:59#)";

            return ExecuteNonQuery(SQL);
        }

        public void DeleteBilling(Billing billing)
        {
            String SQL = "DELETE FROM Billing";
            SQL += " WHERE CompanySerialNumber = '" + billing.CompanySerialNumber + "'";
            SQL += " AND DateMonth = '" + billing.DateMonth + "'";

            ExecuteNonQuery(SQL);
        }

        ///////////////////////////////////////////////////////////////
        //  CMailBox
        ///////////////////////////////////////////////////////////////
        private CMailBox CreateCMailBox(OleDbDataReader reader)
        {
            CMailBox cMailBox = new CMailBox();
            cMailBox.CMailBoxInstallID = reader[0].ToString();
            cMailBox.CommercialUse = Convert.ToBoolean(reader[1].ToString());

            return cMailBox;
        }

        //Update ServiceStatusLog Table
        //CompanySerialNumber
        //ActionDate
        //Status
        //CommercialUse
        public bool AddStatusLog(Company company, String Action)
        {
            String SQL = "INSERT INTO ServiceStatusLog ( ";
            SQL += " [CompanySerialNumber], [ActionDate], [Action], [CommercialUse]";
            SQL += "  ) VALUES (";

            SQL += "'" + company.CompanySerialNumber + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + Action + "',";
            SQL += "'" + (company.CommercialUse ? 1 : 0) + "'";

            SQL += ")";

            return ExecuteNonQuery(SQL);
        }

        ///////////////////////////////////////////////////////////////
        //  Company
        ///////////////////////////////////////////////////////////////
        private Company CreateCompany(OleDbDataReader reader)
        {
            Company company = new Company();
            company.CompanyID = Int32.Parse(reader[0].ToString());
            company.CompanyName = reader[1].ToString();
            company.CountryID = Int32.Parse(reader[2].ToString());
            company.CompanyVAT = reader[3].ToString();
            company.CompanyLogo = reader[4].ToString();
            company.ReadCode = reader[5].ToString();
            company.WriteCode = reader[6].ToString();
            company.MAC = reader[7].ToString();
            company.UserName = reader[8].ToString();
            company.Password = reader[9].ToString();
            company.EMail = reader[10].ToString();
            company.Active = Convert.ToBoolean(reader[11].ToString());
            company.CompanySerialNumber = reader[12].ToString();
            company.Payment = reader[13].ToString();
            company.CommercialUse = Convert.ToBoolean(reader[14].ToString());
            company.Paid = Convert.ToBoolean(reader[15].ToString());

            return company;
        }

        public bool UpdateCompany(Company company)
        {
            String SQL = "UPDATE Companies SET ";

            SQL += " CompanyName = '" + company.CompanyName + "',";
            SQL += " CountryID = '" + company.CountryID + "',";
            SQL += " CompanyVAT = '" + company.CompanyVAT + "',";

            SQL += " UserName = '" + company.UserName + "',";
            SQL += " [Password] = '" + company.Password + "',";

            SQL += " ReadCode = '" + company.ReadCode + "',";
            SQL += " WriteCode = '" + company.WriteCode + "',";

            SQL += " MAC = '" + company.MAC + "',";

            SQL += " EMail = '" + company.EMail + "',";
            SQL += " CompanyLogo = '" + company.CompanyLogo + "',";

            SQL += " Active = '" + (company.Active ? 1 : 0) + "', ";
            //SQL += " TransactionCounter = '" + company.TransactionCounter + "',";

            SQL += " CommercialUse = '" + (company.CommercialUse ? 1 : 0) + "',";
            SQL += " Paid = '" + (company.Paid ? 1 : 0) + "',";
            SQL += " StopService = '" + company.StopService + "',";
            SQL += " MobilePhone = '" + company.MobilePhone + "',";
            SQL += " InformMyMobile = '" + (company.InformMyMobile ? 1 : 0) + "'";
            
            SQL += " WHERE CountryID = " + company.CountryID;
            SQL += " AND CompanyVAT = '" + company.CompanyVAT + "'";

            return ExecuteNonQuery(SQL);
        }

        public Company GetCompany(String CountryID, String VAT)
        {
            String SQL = "select * From Companies";
            SQL += " WHERE CountryID = " + CountryID + "";
            SQL += " AND CompanyVAT = '" + VAT + "'";
            SQL += " AND Active = True";

            AddErr(SQL);
            OpenUserDB();
            AddErr(connectionString);

            OleDbDataReader reader = returnUserData(SQL, connectionString);
            Company company = null;

            if (reader != null)
            {
                if (reader.HasRows)
                {
                    AddErr("HasRows");
                    reader.Read();
                    company = CreateCompany(reader);
                }

                reader.Close();
            }
            CloseUserDB();

            return company;
        }

        public Company GetCompanyReadable(String CountryID, String VAT, String ReadCode)
        {
            String SQL = "select * From Companies";
            SQL += " WHERE CountryID = " + CountryID + "";
            SQL += " AND CompanyVAT = '" + VAT + "'";
            SQL += " AND ReadCode = '" + ReadCode + "'";
            SQL += " AND Active = True";

            AddErr(SQL);
            OpenUserDB();
            AddErr(connectionString);

            OleDbDataReader reader = returnUserData(SQL, connectionString);
            Company company = null;

            if (reader != null)
            {
                if (reader.HasRows)
                {
                    AddErr("HasRows");
                    reader.Read();
                    company = CreateCompany(reader);
                }

                reader.Close();
            }
            CloseUserDB();

            return company;
        }

        public Company GetCompanyReadable(String CountryID, String MAC, String VAT, String ReadCode)
        {
            String SQL = "select * From Companies";
            SQL += " WHERE CountryID = " + CountryID + "";
            //SQL += " AND MAC = '" + MAC + "'";
            SQL += " AND CompanyVAT = '" + VAT + "'";
            SQL += " AND ReadCode = '" + ReadCode + "'";
            SQL += " AND Active = True";

            AddErr(SQL);
            OpenUserDB();
            AddErr(connectionString);
            OleDbDataReader reader = returnUserData(SQL, connectionString);
            Company company = null;
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    AddErr("HasRows");
                    reader.Read();
                    company = CreateCompany(reader);
                }

                reader.Close();
            }
            CloseUserDB();

            return company;
        }

        public Company GetCompanyByKey(String EMail, String MAC, String VAT)
        {
            String SQL = "select * From Companies";
            SQL += " WHERE EMail = '" + EMail + "'";
            SQL += " AND MAC = '" + MAC + "'";
            SQL += " AND CompanyVAT = '" + VAT + "'";

            AddErr(SQL);
            OpenUserDB();

            OleDbDataReader reader = returnData(SQL, connectionString);
            Company company = null;
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    company = CreateCompany(reader);
                }

                reader.Close();
            }
            CloseUserDB();

            return company;
        }

        public bool AddCMailBox(CMailBox cMailBox)
        {
            String SQL = "INSERT INTO CMailBoxMangement ( ";
            SQL += " CMailBoxInstallID, CommercialUse";
            SQL += "  ) VALUES (";

            SQL += "'" + cMailBox.CMailBoxInstallID + "',";
            SQL += "'" + (cMailBox.CommercialUse ? 1 : 0) + "'";

            SQL += ")";

            return ExecuteNonQuery(SQL);
        }

        public bool AddCompany(Company company)
        {
            String SQL = "INSERT INTO Companies ( ";
            SQL += " CompanyName, CountryID, CompanyVAT, ReadCode, WriteCode , MAC , UserName, [Password], EMail, CompanyLogo, Active, CompanySerialNumber, Payment, CommercialUse, Paid, CreationDate, MobilePhone, InformMyMobile";
            SQL += "  ) VALUES (";

            SQL += "'" + company.CompanyName + "',";
            SQL += "'" + company.CountryID + "',";
            SQL += "'" + company.CompanyVAT + "',";

            SQL += "'" + company.ReadCode + "',";
            SQL += "'" + company.WriteCode + "',";

            SQL += "'" + company.MAC + "',";

            SQL += "'" + company.UserName + "',";
            SQL += "'" + company.Password + "',";

            SQL += "'" + company.EMail + "',";
            SQL += "'" + company.CompanyLogo + "',";

            SQL += "'" + (company.Active ? 1 : 0) + "',";

            SQL += "'" + company.CompanySerialNumber + "',";
            SQL += "'" + company.Payment + "', ";

            SQL += "'" + (company.CommercialUse ? 1 : 0) + "',";

            SQL += "'" + (company.Paid ? 1 : 0) + "',";

            SQL += "'" + company.CreationDate + "',";
            SQL += "'" + company.MobilePhone + "',";
            SQL += "'" + (company.InformMyMobile ? 1 : 0) + "'";
            
            SQL += ")";

            return ExecuteNonQuery(SQL);
        }

        //SendData
        public void DeleteData(long CountryID, String CompanyVAT, String TimeStamp)
        {
            String SQL = "DELETE FROM SendData";
            SQL += " WHERE CountryIDTo = " + CountryID;
            SQL += " AND CompanyVATTo = '" + CompanyVAT + "'";
            SQL += " AND [TimeStamp] = '" + TimeStamp + "'";

            ExecuteNonQuery(SQL);
        }

        public bool AddData(String TransactionGUID, String CountryIDFrom, String CompanyVATFrom, String CountryIDTo, String CompanyVATTo, String Data, String WriteCode)
        {
            String SQL = "INSERT INTO SendData ( ";
            SQL += " TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, WriteCode, [TimeStampWrite]";
            SQL += "  ) VALUES (";

            SQL += "'" + TransactionGUID + "',";
            SQL += "'" + CountryIDFrom + "',";
            SQL += "'" + CompanyVATFrom + "',";
            SQL += "'" + CountryIDTo + "',";
            SQL += "'" + CompanyVATTo + "',";
            SQL += "'" + Data + "',";
            SQL += "'" + WriteCode + "',";
            SQL += "'" + DateTime.Now + "'";

            SQL += ")";

            AddErr(SQL);
            return ExecuteNonQuery(SQL);
        }

        //public bool AddRequestData(String TransactionGUID, String CountryIDFrom, String CompanyVATFrom, String CountryIDTo, String CompanyVATTo, String Data)
        //{
        //    String SQL = "INSERT INTO SendData ( ";
        //    SQL += " TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, [TimeStampWrite]";
        //    SQL += "  ) VALUES (";

        //    SQL += "'" + TransactionGUID + "',";
        //    SQL += "'" + CountryIDFrom + "',";
        //    SQL += "'" + CompanyVATFrom + "',";
        //    SQL += "'" + CountryIDTo + "',";
        //    SQL += "'" + CompanyVATTo + "',";
        //    SQL += "'" + Data + "',";            
        //    SQL += "'" + DateTime.Now + "'";

        //    SQL += ")";

        //    AddErr(SQL);
        //    return ExecuteNonQuery(SQL);
        //}

        public CMailBox GetCMailBox(String CMailBoxInstallID)
        {
            CMailBox retCMailBox = null;
            String SQL = "";
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
            SQL = "select * from CMailBoxMangement";
            SQL += " where CMailBoxInstallID = '" + CMailBoxInstallID + "'";
            AddErr(SQL);
            accessdatasource.SelectCommand = SQL;
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            using (OleDbDataReader reader = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    retCMailBox = CreateCMailBox(reader);
                    reader.Close();
                }
            }

            return retCMailBox;
        }
        public CMailBox GetCMailBox(CMailBox cMailBox)
        {
            CMailBox retCMailBox = null;
            String SQL = "";
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
            SQL = "select * from CMailBoxMangement";
            SQL += " where CMailBoxInstallID = '" + cMailBox.CMailBoxInstallID + "'";
            AddErr(SQL);
            accessdatasource.SelectCommand = SQL;
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            using (OleDbDataReader reader = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    retCMailBox = CreateCMailBox(reader);
                    reader.Close();
                }
            }

            return retCMailBox;
        }

        public Company IsComapnyExist(Company company_info)
        {
            Company company = null;
            String SQL = "";
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
            SQL = "select * from companies";
            SQL += " where CountryID = " + company_info.CountryID;
            SQL += " AND CompanyVAT = '" + company_info.CompanyVAT + "'";
            SQL += " AND CompanyName = '" + company_info.CompanyName + "'";
            SQL += " AND ReadCode = '" + company_info.ReadCode + "'";
            SQL += " AND WriteCode = '" + company_info.WriteCode + "'";
            SQL += " AND EMail = '" + company_info.EMail + "'";
            SQL += " AND Active = " + company_info.Active;
            AddErr(SQL);
            //try
            //{
            accessdatasource.SelectCommand = SQL;
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode            
            using (OleDbDataReader reader = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    company = CreateCompany(reader);
                    reader.Close();
                }
            }
            //}
            //catch (Exception)
            //{
            //}

            return company;
        }

        public Company IsComapnyExist(string CountryID, string CompanyVAT)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
            accessdatasource.SelectCommand = "select * from companies where CountryID = " + CountryID + " AND CompanyVAT = '" + CompanyVAT + "'";
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode            
            Company company = null;

            using (OleDbDataReader reader = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    company = CreateCompany(reader);
                    reader.Close();
                }
            }

            return company;
        }

        public void returnDataMultipleResults(String SQL, String connectionString)
        {//static OleDbDataReader
            OleDbDataReader dr = null;

            //SqlConnection
            m_conn = new OleDbConnection(connectionString);

            try
            {
                OleDbCommand cmd = new OleDbCommand(SQL, m_conn);
                m_conn.Open();

                dr = cmd.ExecuteReader();
                while (dr.HasRows)
                {
                    Console.WriteLine("\t{0}\t{1}", dr.GetName(0),
                        dr.GetName(1));

                    while (dr.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}", dr[0].ToString(), dr.GetString(1));
                    }
                    dr.NextResult();
                }

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
        }

        public String GetComapnyData(Int32 CompanyID)
        {
            String SQL = "SELECT [SMSQuantity], [SMSUsed] ,[SMSRequest]";
            SQL += " FROM Companies";
            SQL += " WHERE [CompanyID] = " + CompanyID + " AND Active = true";
            OpenUserDB();

            OleDbDataReader reader = returnData(SQL, connectionString);
            String retVal = "0|0|0";

            //while (reader.Read())
            if (reader.HasRows)
            {
                reader.Read();
                retVal = reader[0].ToString() + "|" + reader[1].ToString() + "|" + reader[2].ToString();
            }

            reader.Close();
            CloseUserDB();

            return retVal;
        }

        private void CheckConnection(OleDbConnection conn)
        {
            if (conn == null)
            {
                conn = GetNewConnection();
            }
            else if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public ArrayList GetProfitSubTotal(String TeurSugHaged)
        {
            OpenUserDB();

            //To Be Filter By Date 
            String SQL = "SELECT Sum(Tikim.[סכום חובה מקומי]) AS [SumOfסכום חובה מקומי], Sum(Tikim.[סכום זכות מקומי]) AS [SumOfסכום זכות מקומי]";
            SQL += " FROM Tikim";
            SQL += " GROUP BY Tikim.[תאור סוג אגד], Tikim.[תאור סוג האגד]";
            SQL += " HAVING [תאור סוג האגד]='" + TeurSugHaged + "' AND [תאור סוג אגד] = 'תוצאתי'";

            ArrayList result = GetKartisTotals(SQL);

            CloseUserDB();

            return result;
        }

        public ArrayList GetAgadimNamesAndNumber(String SQL)
        {
            ArrayList total = new ArrayList();

            try
            {
                OleDbDataReader reader = returnUserData(SQL, connectionString);

                while (reader.Read())
                {
                    if (reader[0].ToString() == "")
                    {
                        total.Add("0");
                        total.Add("");
                        reader.Close();
                        //m_conn.Close();
                        return total;
                    }
                    total.Add(reader[0].ToString());
                    total.Add(reader[1].ToString());

                }
                reader.Close();
                //m_conn.Close();
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return total;
        }

        public ArrayList GetKartisTotals(String SQL, bool closeConnectionOnExit)
        {
            ArrayList result = GetKartisTotals(SQL);
            if (closeConnectionOnExit)
            {
                m_user_conn.Close();
            }
            return result;
        }

        public ArrayList GetKartisTotals(String SQL)
        {
            ArrayList total = new ArrayList();

            try
            {
                OleDbDataReader reader = returnUserData(SQL, connectionString);

                while (reader.Read())
                {
                    if (reader[0].ToString() == "")
                    {
                        total.Add(Convert.ToDouble("0"));
                        total.Add(Convert.ToDouble("0"));
                        reader.Close();
                        //m_conn.Close();
                        return total;
                    }
                    total.Add(Convert.ToDouble(reader[0].ToString()));
                    total.Add(Convert.ToDouble(reader[1].ToString()));

                }
                reader.Close();
                //m_conn.Close();
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return total;
        }

        public ArrayList GetKartisTotals(String SQL, OleDbConnection conn)
        {
            ArrayList total = new ArrayList();

            try
            {
                OleDbCommand cmd = new OleDbCommand(SQL, conn);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[0].ToString() == "")
                    {
                        total.Add(Convert.ToDouble("0"));
                        total.Add(Convert.ToDouble("0"));
                        reader.Close();
                        conn.Close();
                        return total;
                    }
                    total.Add(Convert.ToDouble(reader[0].ToString()));
                    total.Add(Convert.ToDouble(reader[1].ToString()));

                }
                reader.Close();
                //conn.Close();
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return total;
        }

        public int GetNumeric(string Value)
        {
            try
            {
                Decimal Number = Decimal.Parse(Value);
                return (int)Number;
            }

            catch (Exception)
            {
                return 0;
            }
        }

        public double GetDouble(string Value)
        {
            try
            {
                Decimal Number = Decimal.Parse(Value);
                return (double)Number;
            }

            catch (Exception)
            {
                return 0;
            }
        }

        public String GetCompanyData(String CountryID, String CompanyVAT, String WriteCode)
        {
            String SQL = "SELECT *";
            SQL += " FROM SendData";
            //SQL += " WHERE (CountryIDTo = " + CountryID + ") AND (CompanyVATTo = '" + CompanyVAT + "') AND (WriteCode = '" + WriteCode + "')";
            SQL += " WHERE (CountryIDTo = " + CountryID + ") AND (CompanyVATTo = '" + CompanyVAT + "') AND (WriteCode = '" + WriteCode + "') AND (Readen = False)";
            AddErr(SQL);
            return ExpoertSQLLines(SQL);
        }

        public String ExpoertSQLLines(String SQL)
        {
            OleDbDataReader reader = returnData(SQL, connectionString);
            String line = "";
            //int counter = 0;
            while (reader.Read())
            {
                //counter++;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    line += reader[i].ToString() + "|$";
                }
                line += Environment.NewLine; // @"\r\n";
            }
            reader.Close();
            CloseDB();
            //line = counter + Environment.NewLine + line;

            return line;
        }

        public String ExpoertTableLines(String TableName)
        {
            String SQL = "SELECT * From " + TableName;

            OleDbDataReader reader = returnData(SQL, connectionString);
            String line = "";
            int counter = 0;
            while (reader.Read())
            {
                counter++;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    line += reader[i].ToString() + "|";
                }
                line += Environment.NewLine; // @"\r\n";
            }
            reader.Close();

            line = counter + Environment.NewLine + line;

            return line;
        }

        public String ShowTableLines(String TableName)
        {
            //ORDER BY column_name(s) ASC|DESC            
            String line = "";
            String SQL = "SELECT * From " + TableName;

            if (Session["OrderBy"] != null)
            {
                SQL += " ORDER BY " + Session["Column"].ToString() + " " + Session["OrderBy"].ToString();
            }

            line += SQL;

            OleDbDataReader reader = returnData(SQL, connectionString);

            if (reader == null)
            {
                m_conn.Close();
                return null;
            }


            int counter = 0;
            line += "<table border=1 cellpadding=0 cellspacing=0>";
            line += "<tr>";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                line += "<td align=center  bgcolor=lightblue>";
                line += "<FONT FACE=\"Tahoma, sans-serif\" SIZE=\"2\">";
                String str = reader.GetName(i);
                line += (i + 1).ToString();
                line += "<FONT>";
                line += "</td>";
            }
            line += "</tr>";

            line += "<tr>";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                line += "<td bgcolor=lightyellow>";
                line += "<FONT FACE=\"Tahoma, sans-serif\" SIZE=\"2\"><b>";
                String str = reader.GetName(i);
                line += @"<a href=""http://80.179.222.155/Adirim/GetOrders.aspx?TableName=" + TableName + @"&LoginKey=xezp3avnniqyjf45wso0ot45&OrderBy=ASC&Column=" + str + @""">";
                line += (((str == null) || (str == "")) ? "&nbsp;" : str);
                line += "</a>";
                line += "</b><FONT>";
                line += "</td>";
            }
            line += "</tr>";

            while (reader.Read())
            {
                line += "<tr>";
                counter++;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    line += "<td>";
                    line += "<FONT FACE=\"Tahoma, sans-serif\" SIZE=\"2\">";
                    String str = reader[i].ToString();
                    line += (((str == null) || (str == "")) ? "&nbsp;" : str);
                    line += "<FONT>";
                    line += "</td>";
                }
                line += "</tr>";
            }
            reader.Close();
            m_conn.Close();

            line += "</table>";

            line += "<br>Number Of Records #" + (counter);
            return line;
        }

        public string LogedInDB { get; set; }

        public bool Login(String UserName, String Password)
        {
            String SQL = "SELECT * From Users Where UserName= '" + UserName + "' and [Password] = '" + Password + "' AND Admin = True";

            OleDbDataReader reader = returnData(SQL, connectionString);
            bool retVal = false;

            while (reader.Read())
            {
                //LogedInDB = reader[0].ToString() + "_" + reader[1].ToString();
                retVal = true;
            }

            reader.Close();

            CloseDB();
            //if (!retVal)
            //{
            //    m_conn.Close();
            //}

            return retVal;
        }

        //SqlDataReader
        public static OleDbDataReader returnData(String SQL, String connectionString)
        {
            //"server=(local);database=[somedatabase];user id=sa;password=sa"

            //SqlDataReader
            OleDbDataReader dr = null;

            //SqlConnection
            if (m_conn == null)
            {
                m_conn = new OleDbConnection(connectionString);
            }

            if (m_conn.State != ConnectionState.Open)
            {
                m_conn.Open();
            }

            try
            {
                //SqlCommand
                OleDbCommand cmd = new OleDbCommand(SQL, m_conn);

                //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); return dr;
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return dr;
        }

        //SqlDataReader
        public static OleDbDataReader returnUserData(String SQL, String connectionString)
        {
            //"server=(local);database=[somedatabase];user id=sa;password=sa"

            //SqlDataReader
            OleDbDataReader dr = null;

            //SqlConnection
            if (m_user_conn == null)
            {
                m_user_conn = new OleDbConnection(connectionString);
            }

            if (m_user_conn.State != ConnectionState.Open)
            {
                m_user_conn.Open();
            }

            try
            {
                //SqlCommand
                OleDbCommand cmd = new OleDbCommand(SQL, m_user_conn);

                //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); return dr;
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return dr;
        }

        public String dbERR { get; set; }
        //INSERT INTO Companies (CompanyName, UserName, Password, EMail, SMSQuantity, CompanyLogo, Active, DateStart, DateEnd) VALUES ('a', 'b', 'c','d', '1', 'x', '1', #05/07/2012#, #05/07/2013#)
        public bool ExecuteNonQuery(String SQL)
        {
            bool bResult = false;

            AddErr(SQL);
            try
            {
                //OleDbConnection conn = new OleDbConnection(connectionString);
                OpenDB();
                OleDbCommand cmd = new OleDbCommand(SQL, m_conn);
                //conn.Open();                
                int i = cmd.ExecuteNonQuery();
                //conn.Close();
                m_conn.Close();
                bResult = (i > 0);//true;
                //dbERR = i.ToString();
                AddErr(i.ToString());
            }
            catch (Exception ex)
            {
                //dbERR = ex.Message;
                AddErr(ex.Message);
                System.Console.WriteLine(ex.Message);
                if (m_conn != null)
                {
                    m_conn.Close();
                }
                bResult = false;
            }

            return bResult;
        }

        public String GetWaitingMails(string CountryID,string CompanyVAT)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
            accessdatasource.SelectCommand = "select count(*) from SendData where CompanyVATTo = '" + CompanyVAT + "' AND CountryIDTo = " + CountryID + " AND Readen = False"; 
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode
            String MailCounter = "0";

            using (OleDbDataReader rdrAccess = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                if (rdrAccess.HasRows)
                {
                    rdrAccess.Read();
                    MailCounter = rdrAccess[0].ToString();
                }
            }

            return MailCounter;
        }

        public bool IsCompanyHasCC(string CompanySerialNumber)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
            accessdatasource.SelectCommand = "select Paid from companies where CompanySerialNumber = '" + CompanySerialNumber + "'";
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode
            bool bCompanyHasCC = false;

            using (OleDbDataReader rdrAccess = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                bCompanyHasCC = rdrAccess.HasRows;
            }

            return bCompanyHasCC;
        }

        public bool IsComapnyUsedSMS(int pCompanyID)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/SMSServer.mdb";
            accessdatasource.SelectCommand = "select * from companies where CompanyID = " + pCompanyID + " and SMSUsed = 0";
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode
            bool bCompanyUsedSMS = true;

            using (OleDbDataReader rdrAccess = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                bCompanyUsedSMS = !rdrAccess.HasRows;
            }

            return bCompanyUsedSMS;
        }

        public bool IsUserUsedSMS(int pUserID)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/SMSServer.mdb";
            accessdatasource.SelectCommand = "select * from users where UserID = " + pUserID + " and SMSUsed = 0";
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode
            bool bUserUsedSMS = true;

            using (OleDbDataReader rdrAccess = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                bUserUsedSMS = !rdrAccess.HasRows;
            }

            return bUserUsedSMS;
        }

        public bool IsComapnyNameExist(string pCompanyName, int pCompanyID)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/SMSServer.mdb";
            accessdatasource.SelectCommand = "select * from companies where CompanyName = '" + pCompanyName + "'";
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            if (pCompanyID != -1)
            {
                accessdatasource.SelectCommand += " and CompanyID <> " + pCompanyID;
            }

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode
            bool bCompanyExist = false;

            using (OleDbDataReader rdrAccess = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                bCompanyExist = rdrAccess.HasRows;
            }

            return bCompanyExist;
        }

        public bool IsUserNameExist(string pUserName, int pUserID)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/SMSServer.mdb";
            accessdatasource.SelectCommand = "select * from users where UserName = '" + pUserName + "'";
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            if (pUserID != -1)
            {
                accessdatasource.SelectCommand += " and UserID <> " + pUserID;
            }

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode
            bool bCompanyExist = false;

            using (OleDbDataReader rdrAccess = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                bCompanyExist = rdrAccess.HasRows;
            }

            return bCompanyExist;
        }

        public bool IsComapnyNameExist(string pCompanyName)
        {
            return IsComapnyNameExist(pCompanyName, -1);
        }

        public bool IsUserNameExist(string pUserName)
        {
            return IsUserNameExist(pUserName, -1);
        }

        public void DeleteCompany(Company company)
        {
            String SQL = "DELETE FROM Companies";
            SQL += " WHERE CountryID = " + company.CountryID;
            SQL += " AND CompanyVAT = '" + company.CompanyVAT + "'";
            SQL += " AND ReadCode = '" + company.ReadCode + "'";
            SQL += " AND WriteCode = '" + company.WriteCode + "'";
            SQL += " AND EMail = '" + company.EMail + "'";
            AddErr(SQL);
            DeleteSQL(SQL);
        }

        public void DeleteCompany(int CountryID, String CompanyVAT, String ReadCode, String WriteCode, String EMail)
        {
            String SQL = "DELETE FROM Companies";
            SQL += " WHERE CountryID = " + CountryID;
            SQL += " AND CompanyVAT = '" + CompanyVAT + "'";
            SQL += " AND ReadCode = '" + ReadCode + "'";
            SQL += " AND WriteCode = '" + WriteCode + "'";
            SQL += " AND EMail = '" + EMail + "'";
            AddErr(SQL);
            DeleteSQL(SQL);
        }

        public void DeleteUser(int pUserID)
        {
            DeleteSQL("Delete from users where UserID = " + pUserID);
        }

        public void DeleteSQL(String SQL)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
            accessdatasource.DeleteCommand = SQL;
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataSet;

            accessdatasource.DataBind();

            accessdatasource.Delete();
        }

        //public void UpdatePayment(string TransactionGUID, string WriteCode)
        //{
        //    String SQL = "UPDATE Companies SET";
        //    SQL += " Paid = 1,";
        //    SQL += " TimeStampRead = #" + DateTime.Now + "#";
        //    SQL += " WHERE CompanySerialNumber = '" + TransactionGUID + "'";
        //    SQL += " AND WriteCode = '" + WriteCode + "'";

        //    Update(SQL);
        //}

        public String UpdateTransaction(String TransactionGUID, string Readen)
        {
            String SQL = "UPDATE SendData SET";
            SQL += " Readen ='1'";
            SQL += " WHERE TransactionGUID = '" + TransactionGUID + "'";

            return Update(SQL);
        }

        public bool IsTransactionReaden(string TransactionGUID)
        {
            AccessDataSource accessdatasource = new AccessDataSource();
            accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
            accessdatasource.SelectCommand = "select * from SendData where TransactionGUID = '" + TransactionGUID + "' AND Readen = True";
            accessdatasource.DataSourceMode = SqlDataSourceMode.DataReader;

            accessdatasource.DataBind();

            //AccessDataSource DataReader Mode
            bool bTransactionReaden = false;

            using (OleDbDataReader rdrAccess = (OleDbDataReader)accessdatasource.Select(DataSourceSelectArguments.Empty))
            {
                bTransactionReaden = rdrAccess.HasRows;
            }

            return bTransactionReaden;
        }

        public String Update(string SQL)
        {
            try
            {
                AccessDataSource accessdatasource = new AccessDataSource();
                accessdatasource.DataFile = "~/App_Data/GlobalInfoProtocol.mdb";
                accessdatasource.UpdateCommand = SQL;
                accessdatasource.DataSourceMode = SqlDataSourceMode.DataSet;

                accessdatasource.DataBind();

                accessdatasource.Update();
            }
            catch (Exception ex)
            {
                return ex.Message + Environment.NewLine + SQL;
            }

            return SQL;
        }

        ///////////////////////////////////////////////////////////////////////

        public int PageSize { get; set; }

        public String ShowTableFileds(String TableName)
        {
            //ORDER BY column_name(s) ASC|DESC            
            String line = "";
            String SQL = "SELECT * From [" + TableName + "]";

            line += SQL;
            line += "</br>" + GetSQLString("SELECT count(*) From [" + TableName + "]") + " Records";

            OleDbDataReader reader = returnData(SQL, connectionString);

            if (reader == null)
            {
                m_conn.Close();
                return null;
            }

            if (!reader.IsClosed)
            {
                line += ", " + (reader.FieldCount) + " Fileds";
                line += "</br>";
                line += "</br>";

                line += "<table border=1 cellpadding=0 cellspacing=0>";

                //for (int i = 0; i < reader.FieldCount; i++)
                //{
                //    line += "<tr>";
                //    line += "<td align=center  bgcolor=lightblue>";
                //    line += "<FONT FACE=\"Tahoma, sans-serif\" SIZE=\"2\">";
                //    String str = reader.GetName(i);
                //    line += (i + 1).ToString();
                //    line += "<FONT>";
                //    line += "</td>";
                //    line += "</tr>";
                //}
                ArrayList PKTables = GetPKTable(TableName); // GetTablePK(TableName);

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    line += "<tr>";
                    line += "<td align=center  bgcolor=lightblue>";
                    line += "<FONT FACE=\"Tahoma, sans-serif\" SIZE=\"2\">";
                    String FieldName = reader.GetName(i);
                    line += (i + 1).ToString();
                    line += "<FONT>";
                    line += "</td>";
                    line += "<td bgcolor=lightyellow>";
                    line += "<FONT FACE=\"Tahoma, sans-serif\" SIZE=\"2\"><b>";
                    //line += @"<a href=""http://adirim.info/GetOrders.aspx?TableName=" + TableName + @"&LoginKey=xezp3avnniqyjf45wso0ot45&OrderBy=ASC&Column=" + str + @""">";
                    //line += @"<a href=""http://adirim.info/ShowTable.aspx?TableName=" + TableName + @"&LoginKey=xezp3avnniqyjf45wso0ot45&OrderBy=ASC&Column=" + FieldName + @""">";
                    line += @"<a href=""http://adirim.info/GlobalInfoProtocol/ShowTable.aspx?TableName=" + TableName + "&PageSize=" + PageSize + @"&LoginKey=xezp3avnniqyjf45wso0ot45&OrderBy=ASC&Column=" + FieldName + @""">";
                    //line += @"<a href=""http://localhost:44964/ShowTable.aspx?TableName=" + TableName + @"&LoginKey=xezp3avnniqyjf45wso0ot45&OrderBy=ASC&Column=" + FieldName + @""">";
                    line += (((FieldName == null) || (FieldName == "")) ? "&nbsp;" : FieldName);
                    line += "</a>";
                    line += "</b><FONT>";
                    line += "</td>";

                    //5 ***** DataType = System.String 
                    //9 ***** IsReadOnly = False 
                    //11 ***** IsUnique = False 
                    //12 ***** IsKey = False
                    //13 ***** IsAutoIncrement = False 


                    ArrayList FiledsInfo = GetFieldInformation(TableName, FieldName, Session["connectionString"].ToString());

                    line += "<td>";
                    line += (FiledsInfo[13].ToString() == "IsAutoIncrement = True") ? "<img src=\"/GlobalInfoProtocol/images/icon_add.png\"/>" : "<img src=\"/GlobalInfoProtocol/images/spacer.png\"/>";
                    line += "</td>";

                    line += "<td>";
                    String key_name = "";

                    foreach (String PKField in PKTables)
                    {
                        if (FieldName == PKField)
                        {
                            key_name = (FieldName == PKField) ? "<img src=\"/GlobalInfoProtocol/images/unique_icon.png\"/>" : "<img src=\"/GlobalInfoProtocol/images/spacer.png\"/>";
                            break;
                        }
                    }

                    line += (key_name == "" ? "<img src=\"/GlobalInfoProtocol/images/spacer.png\"/>" : key_name);
                    line += "</td>";

                    line += "<td>";
                    line += (FiledsInfo[12].ToString() == "IsKey = True") ? "<img src=\"/GlobalInfoProtocol/images/Key-icon-16.png\"/>" : "<img src=\"/GlobalInfoProtocol/images/spacer.png\"/>";
                    line += "</td>";

                    //line += "<td>";
                    //line += (FiledsInfo[11].ToString() == "IsUnique = True") ? "<img src=\"/images/unique_icon.png\"/>" : "&nbsp;";
                    //line += "</td>";

                    line += "<td>";
                    line += FiledsInfo[5].ToString().Replace("DataType = System.", "");
                    line += "</td>";

                    line += "<td>";

                    String FieldSize = FiledsInfo[2].ToString().Replace("ColumnSize = ", "");

                    line += (FieldSize == "536870910") ? "<img src=\"/GlobalInfoProtocol/images/Memo.png\"/>" : FieldSize;

                    line += "</td>";

                    line += "<td>";
                    line += (FiledsInfo[9].ToString() == "IsReadOnly = True") ? "<img src=\"/GlobalInfoProtocol/images/action_cancel_small.png\"/>" : "<img src=\"/GlobalInfoProtocol/images/spacer.png\"/>";
                    line += "</td>";


                    //foreach (String PKTable in PKTables)
                    //{
                    //    line += "<td>";
                    //    line += PKTable;
                    //    line += "</td>";
                    //}

                    //foreach (String FiledInfo in FiledsInfo)
                    //{
                    //    line += "<td>";
                    //    line += FiledInfo;
                    //    line += "</td>";
                    //}

                    line += "</tr>";
                }

                line += "</table>";
            }
            reader.Close();
            m_conn.Close();

            return line;
        }

        public String GetTableFileds(String TableName)
        {
            String line = "";
            String SQL = "SELECT * From [" + TableName + "] where 1 = 1";

            OleDbDataReader reader = returnData(SQL, connectionString);

            if (reader == null)
            {
                m_conn.Close();
                return null;
            }

            for (int i = 0; i < reader.FieldCount; i++)
            {
                String str = reader.GetName(i);
                line += (str + Environment.NewLine);
            }


            reader.Close();
            m_conn.Close();

            return line;
        }


        public String GetTablesNames()
        {
            //String restrictions() = {Nothing, Nothing, "my table name"};

            String line = "";
            m_conn = new OleDbConnection(connectionString);
            m_conn.Open();
            DataTable dt = m_conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //DataTable dt1 = m_conn.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, new object[] { null, null, "IndexColumns" });
            //DataTable dt2 = m_conn.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new object[] { null, null, null, "TABLE" });
            //DataTable dt3 = m_conn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, null, null, "TABLE" });
            //DataTable dt4 = m_conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, null, "TABLE" });
            //m_conn.GetSchema("INDEXES", restrictions);



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String tableName = dt.Rows[i]["TABLE_NAME"].ToString();
                line += tableName + Environment.NewLine;
            }
            m_conn.Close();

            return line;
        }

        private ArrayList GetFieldInformation(String TableName, String FiledName, String connectionString)
        {
            //***** DataType = System.String 
            ////AllowDBNull = True 
            //***** IsReadOnly = False 
            //***** IsUnique = False 
            //***** IsKey = False
            //***** IsAutoIncrement = False 
            ArrayList FiledsInfo = new ArrayList();

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string sSql = string.Empty;
                    sSql = "SELECT [" + FiledName + "] FROM [" + TableName + "]";

                    OleDbCommand cmd = new OleDbCommand(sSql, conn);
                    conn.Open();

                    OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.KeyInfo);


                    DataTable schemaTable = rdr.GetSchemaTable();
                    //StringBuilder sb = new StringBuilder();
                    foreach (DataRow myField in schemaTable.Rows)
                    {
                        foreach (DataColumn myProperty in schemaTable.Columns)
                        {
                            FiledsInfo.Add(myProperty.ColumnName + " = " + myField[myProperty].ToString());
                            //sb.Append(myProperty.ColumnName + " = " + myField[myProperty].ToString() + Environment.NewLine);
                        }

                        // burn the reader
                        rdr.Close();

                        // exit 
                        //return sb.ToString();
                        return FiledsInfo;
                    }
                }
            }
            catch
            {
                //MessageBox.Show("Unable to attach to this table with current user; check database security permissions.", "Field Information");
            }

            return FiledsInfo;


        }

        private ArrayList GetTablePK(String TableName)
        {
            ArrayList FiledsInfo = new ArrayList();

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string sSql = string.Empty;
                    sSql = "SELECT * FROM [" + TableName + "]";

                    OleDbCommand cmd = new OleDbCommand(sSql, conn);
                    conn.Open();

                    OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.KeyInfo);

                    //DataTable schemaTable = rdr.GetSchemaTable();
                    DataTable PKTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new object[] { null, null, TableName });
                    //StringBuilder sb = new StringBuilder();
                    //lvwFiledInfo.Items.Clear();
                    foreach (DataRow myField in PKTable.Rows)
                    {
                        foreach (DataColumn myProperty in PKTable.Columns)
                        {
                            FiledsInfo.Add(myProperty.ColumnName + " = " + myField[myProperty].ToString());
                        }

                        // burn the reader
                        rdr.Close();

                        return FiledsInfo;
                    }
                }
            }
            catch
            {
            }

            return FiledsInfo;
        }

        public ArrayList GetPKTable(String TableName)
        {
            ArrayList FiledsInfo = new ArrayList();
            try
            {
                bool PK = false;

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {

                    //Create OleDbDataReader and OleDbCommand to return all data from selected table..
                    OleDbDataReader reader;
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM " + TableName, conn);
                    conn.Open();
                    reader = cmd.ExecuteReader();

                    //Return primary key tables..
                    DataTable PKTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new object[] { null, null, TableName });

                    //Create schemaTable
                    DataTable schemaTable = reader.GetSchemaTable();
                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        PK = false;
                        for (int j = 0; j < PKTable.Rows.Count; j++)
                        {
                            if (schemaTable.Rows[i][0].ToString() == PKTable.Rows[j][3].ToString())
                            {
                                PK = true;
                                break;
                            }
                        }
                        // schemaTable.Rows[i][0] --> Column Name..
                        if (PK)
                        {
                            //listBox2.Items.Add(schemaTable.Rows[i][0].ToString() + ": PrimaryKey");
                            //FiledsInfo.Add(schemaTable.Rows[i][0].ToString() + ": PrimaryKey");
                            FiledsInfo.Add(schemaTable.Rows[i][0].ToString());
                        }
                        //else
                        //{
                        //    //listBox2.Items.Add(schemaTable.Rows[i][0].ToString());
                        //    FiledsInfo.Add(schemaTable.Rows[i][0].ToString()); 
                        //}
                    }

                    //Close connection
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                FiledsInfo.Add(ex.Message);
                //MessageBox.Show(ex.Message);
            }

            return FiledsInfo;
        }
    }
}