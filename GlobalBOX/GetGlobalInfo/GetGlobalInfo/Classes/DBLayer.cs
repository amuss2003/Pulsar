using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
//using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.IO;
using System.Diagnostics;
using Pulsar;
using Pulsar.Classes;
using System.Windows.Forms;
using System.Drawing;
//using System.Drawing;

namespace Pulsar
{

    public class DBLayer
    {
        public CompanyInfo Current_Company_Info { get; set; }
        private String strConnection = null;
        public OdbcConnection conn = null;

        public DBLayer(String DB_Path)
        {
            //Exclusive=1;
            strConnection = @"Driver={Microsoft Access Driver (*.mdb)};Dbq=" + DB_Path + @"LocalInfoProtocol.mdb;Uid=;Pwd=;";
            Console.WriteLine(strConnection);
        }

        public int GetDBVersion()
        {
            String SQL = "SELECT max(VersionNumber) FROM DBVersion";
            String result = SQLQueryParam(SQL);
            return ((result != null) && (result.Trim() != "")) ? Int32.Parse(result) : 0;
        }

        public ArrayList ActionsList()
        {//ActionCode, ActionTypeIN, ActionTypeOUT, Description
            string SQL = "SELECT * FROM ActionsList";
            return SQLQueryArrayList(SQL);
        }

        public int GetCompanyCountryID(string company_id)
        {
            String SQL = "SELECT CountryID FROM Companies WHERE CompanyID = " + company_id;
            String result = SQLQueryParam(SQL);
            return ((result != null) && (result.Trim() != "")) ? Int32.Parse(SQLQueryParam(SQL)) : 0;
        }

        public String GetCompanyVAT(string company_id)
        {
            String SQL = "SELECT CompanyVAT FROM Companies WHERE CompanyID = " + company_id;
            String result = SQLQueryParam(SQL);
            return ((result != null) && (result.Trim() != "")) ? SQLQueryParam(SQL) : "";
        }

        public bool CheckCompanyExist(KoteretTnua koteret_tnua)
        {
            if ((koteret_tnua.CountryIDHaSholeah == null) || (koteret_tnua.OsekMoorshehHaSholeah == null))
                return false;

            String SQL = "SELECT CompanyVAT FROM Companies WHERE CountryID = " + koteret_tnua.CountryIDHaSholeah + " AND CompanyVAT = '" + koteret_tnua.OsekMoorshehHaSholeah + "'";
            String result = SQLQueryParam(SQL);
            return ((result != null) && (result.Trim() != ""));
        }

        public bool CheckCompanyExist(WriteCodeRequest write_code_request)
        {
            if ((write_code_request.CountryIDHaSholeah == null) || (write_code_request.OsekMoorshehHaSholeah == null))
                return false;

            String SQL = "SELECT CompanyVAT FROM Companies WHERE CountryID = " + write_code_request.CountryIDHaSholeah + " AND CompanyVAT = '" + write_code_request.OsekMoorshehHaSholeah + "'";
            String result = SQLQueryParam(SQL);
            return ((result != null) && (result.Trim() != ""));
        }

        public String SQLQueryParam(String SQL)
        {
            Open();

            String result = null;
            try
            {
                OdbcCommand cmd = new OdbcCommand(SQL, conn);

                OdbcDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader[0].ToString();
                }
            }
            catch (Exception)
            {
            }

            Close();

            return result;
        }

        public ArrayList SQLQueryArrayList(String SQL)
        {
            Open();

            ArrayList result = new ArrayList();

            OdbcCommand cmd = new OdbcCommand(SQL, conn);

            OdbcDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ArrayList record = new ArrayList();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        record.Add(reader[i].ToString());
                    }

                    result.Add(record);
                }
            }

            Close();

            return result;
        }

        public bool SQLExecute(String SQL)
        {
            bool bResult = false;
            Open();

            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            try
            {
                cmd.ExecuteNonQuery();
                bResult = true;
            }
            catch (Exception ex)
            {
            }
            Close();
            return bResult;
        }

        public bool CheckCompanyHasData(Company company)
        {
            String result = "";
            String SQL = "";

            SQL = "SELECT Count(Haklada.TransactionGUID) AS CountOfTransactionGUID";
            SQL += " FROM Companies INNER JOIN Haklada ON Companies.CompanyID = Haklada.CompanyID";
            SQL += " GROUP BY Companies.CountryID, Companies.CompanyVAT";
            SQL += " HAVING (((Companies.CountryID)=" + company.CountryID + ") AND ((Companies.CompanyVAT)='" + company.CompamyInfoVAT + "'))";

            result = SQLQueryParam(SQL);

            if ((result == null) || (result == ""))
            {
                SQL = " SELECT Count(Inbox.TransactionGUID) AS CountOfTransactionGUID";
                SQL += " FROM Inbox";
                SQL += " GROUP BY Inbox.CountryIDFrom, Inbox.CompanyVATFrom";
                SQL += " HAVING (((Inbox.CountryIDFrom)=" + company.CountryID + ") AND ((Inbox.CompanyVATFrom)='" + company.CompamyInfoVAT + "'))";
                result = SQLQueryParam(SQL);
            }
            else
                return true;

            if ((result == null) || (result == ""))
            {
                SQL = " SELECT Count(Outbox.TransactionGUID) AS CountOfTransactionGUID";
                SQL += " FROM Outbox";
                SQL += " GROUP BY Outbox.CountryIDFrom, Outbox.CompanyVATFrom";
                SQL += " HAVING (((Outbox.CountryIDFrom)=" + company.CountryID + ") AND ((Outbox.CompanyVATFrom)='" + company.CompamyInfoVAT + "'))";

                result = SQLQueryParam(SQL);
            }
            else
                return true;

            if ((result == null) || (result == ""))
            {
            }
            else
                return true;

            return false;
        }

        public ArrayList FillReportData(String Year)
        {
            //String SQL = "SELECT Companies.CompanyName, Haklada.LeTkufaUd, Sum(Haklada.SchumKolelMaam) AS SumOfSchumKolelMaam, GroupsTypeList.GroupTypeName";
            //SQL += " FROM (Companies INNER JOIN Haklada ON Companies.CompanyID = Haklada.CompanyID) INNER JOIN GroupsTypeList ON Companies.GroupTypeID = GroupsTypeList.GroupTypeID";
            //SQL += " GROUP BY Companies.CompanyName, Haklada.LeTkufaUd, GroupsTypeList.GroupTypeName, Companies.GroupTypeID";
            String SQL = "SELECT Companies.CompanyName, Haklada.LeTkufaUd, Sum(Haklada.SchumKolelMaam) AS SumOfSchumKolelMaam, GroupsTypeList.GroupTypeName, Year([LeTkufaUd]) AS Expr1";
            SQL += " FROM (Companies INNER JOIN Haklada ON Companies.CompanyID = Haklada.CompanyID) INNER JOIN GroupsTypeList ON Companies.GroupTypeID = GroupsTypeList.GroupTypeID";
            SQL += " GROUP BY Companies.CompanyName, Haklada.LeTkufaUd, GroupsTypeList.GroupTypeName, Companies.GroupTypeID, Year([LeTkufaUd])";
            SQL += " HAVING Year([LeTkufaUd])= " + Year;

            return SQLQueryArrayList(SQL);
        }

        public DateTime FillReportDataMaxDate(String Year)
        {
            //String SQL = "SELECT Max(Haklada.LeTkufaUd) AS MaxOfLeTkufaUd";
            //SQL += " FROM (Companies INNER JOIN Haklada ON Companies.CompanyID = Haklada.CompanyID) INNER JOIN GroupsTypeList ON Companies.GroupTypeID = GroupsTypeList.GroupTypeID";
            String SQL = "SELECT Max(Haklada.LeTkufaUd) AS MaxOfLeTkufaUd";
            SQL += " FROM (Companies INNER JOIN Haklada ON Companies.CompanyID = Haklada.CompanyID) INNER JOIN GroupsTypeList ON Companies.GroupTypeID = GroupsTypeList.GroupTypeID";
            SQL += " HAVING Year([LeTkufaUd])= " + Year;
            
            String date = SQLQueryParam(SQL);
            if (date == "")
            {
                date = DateTime.Now.ToShortDateString();
            }
            return DateTime.Parse(date, new System.Globalization.CultureInfo("en-AU", false));
        }

        public void AddCompany(Company company)
        {
            AddCompany(company, Current_Company_Info);
        }

        public void AddCompany(Company company, CompanyInfo company_info)
        {
            String SQL = "INSERT INTO Companies (CompanyName, CountryID, CompanyVAT, WriteCode, AccountCode, Blocked, HaveCMail, CompanyType, CompanyInfoCountryID, CompanyInfoCompanyVAT, MobilePhone)";
            SQL += " VALUES ( ";
            SQL += "'" + company.CompanyName + "',";
            SQL += "'" + company.CountryID + "',";
            SQL += "'" + company.CompanyVAT + "',";
            SQL += "'" + company.WriteCode + "',";
            SQL += "'" + company.AccountCode + "',";
            SQL += "'" + (company.Blocked ? 1 : 0) + "',";
            SQL += "'" + company.HaveCMail + "',";
            SQL += "'" + company.CompanyType + "',";
            SQL += "'" + company_info.CompanyCountryID + "',";
            SQL += "'" + company_info.CompanyVAT + "',";
            SQL += "'" + company_info.MobilePhone + "'";
            SQL += ")";

            SQLExecute(SQL);
        }

        public void UpdateCompanyInfo(CompanyInfo company_info)
        {
            String SQL = "Update CompanyInfo SET ";

            SQL += "CompanyName = '" + company_info.CompanyName + "',";
            SQL += "CompanyCountryID = '" + company_info.CompanyCountryID + "',";
            SQL += "CompanyVAT = '" + company_info.CompanyVAT + "',";
            SQL += "ReadCode = '" + company_info.ReadCode + "',";
            SQL += "WriteCode = '" + company_info.WriteCode + "',";
            SQL += "EMail = '" + company_info.EMail + "',";
            SQL += "maam = '" + company_info.maam + "',";
            SQL += "FilesSearch = '" + company_info.FilesSearch + "',";
            SQL += "DataPath = '" + company_info.DataPath + "',";
            SQL += "CompanySerialNumber = '" + company_info.CompanySerialNumber + "',";
            SQL += "SystemColor = '" + company_info.SystemColor + "',";
            SQL += "MobilePhone = '" + company_info.MobilePhone + "',";
            SQL += "InformMyMobile = '" + (company_info.InformMyMobile ? 1 : 0) + "'";            

            SQL += " WHERE CompanyID = " + company_info.CompanyID;

            SQLExecute(SQL);
        }

        public bool UpdateCompany(Company company)
        {
            return UpdateCompany(company, Current_Company_Info);
        }

        public bool UpdateCompany(Company company, CompanyInfo company_info)
        {
            if (company.CompanyName.IndexOf("''") == -1)
            {
                if (company.CompanyName.IndexOf("'") != -1)
                {
                    company.CompanyName = company.CompanyName.Replace("'", "''");
                }
            }

            String SQL = "Update Companies SET ";

            SQL += "CompanyName = '" + company.CompanyName + "',";
            SQL += "CountryID = '" + company.CountryID + "',";
            SQL += "CompanyVAT = '" + company.CompanyVAT + "',";
            SQL += "WriteCode = '" + company.WriteCode + "',";
            SQL += "AccountCode = '" + company.AccountCode + "',";
            SQL += "Blocked = '" + (company.Blocked ? 1 : 0) + "',";
            SQL += "HaveCMail = '" + company.HaveCMail + "',";
            SQL += "CompanyType = '" + company.CompanyType + "',";
            SQL += "CompanyInfoCountryID = '" + company_info.CompanyCountryID + "',";
            SQL += "CompanyInfoCompanyVAT = '" + company_info.CompanyVAT + "',";
            SQL += "MobilePhone = '" + company_info.MobilePhone + "'";

            SQL += " WHERE CompanyID = " + company.CompanyID;

            return SQLExecute(SQL);
        }

        public void DeleteTochenTnuaOutboxHakladaInbox(double RawDataNumber)
        {
            String SQL = "DELETE FROM HakladatInboxTochenTnua WHERE RawDataNumber = '" + RawDataNumber + "'";

            SQLExecute(SQL);
        }

        public void DeleteTochenTnuaOutboxHaklada(double RawDataNumber)
        {
            String SQL = "DELETE FROM HakladatTochenTnua WHERE RawDataNumber = '" + RawDataNumber + "'";

            SQLExecute(SQL);
        }

        public void DeleteOutboxHaklada(String TransactionGUID)
        {
            String SQL = "DELETE FROM Haklada WHERE TransactionGUID = '" + TransactionGUID + "'";

            SQLExecute(SQL);
        }

        public void DeleteOutboxHakladaInbox(String TransactionGUID)
        {
            String SQL = "DELETE FROM HakladaInbox WHERE TransactionGUID = '" + TransactionGUID + "'";

            SQLExecute(SQL);
        }

        public void DeleteOutbox(String TransactionGUID)
        {
            String SQL = "DELETE FROM Outbox WHERE TransactionGUID = '" + TransactionGUID + "'";

            SQLExecute(SQL);
        }

        public void DeleteInbox(String TransactionGUID)
        {
            String SQL = "DELETE FROM Inbox WHERE TransactionGUID = '" + TransactionGUID + "'";

            SQLExecute(SQL);
        }

        public void DeleteCompany(int CompanyID)
        {
            String SQL = "DELETE FROM Companies WHERE CompanyID = " + CompanyID;

            SQLExecute(SQL);
        }

        public void CleanOutboxTable()
        {
            String SQL = "DELETE FROM Outbox";
            SQLExecute(SQL);
        }

        public void DeleteCompanyInfo(int CompanyID)
        {
            String SQL = "DELETE FROM CompanyInfo WHERE CompanyID = " + CompanyID;

            SQLExecute(SQL);
        }

        public string AddCompanyInfo(CompanyInfo company_info)
        {
            String CompanySerialNumber = "{" + Guid.NewGuid().ToString() + "}";
            String SQL = "INSERT INTO CompanyInfo (CompanyCountryID , CompanyName, CompanyVAT, ReadCode, WriteCode, EMail, maam, FilesSearch, DataPath, CompanySerialNumber, SystemColor, MobilePhone, InformMyMobile)";
            SQL += " VALUES ( ";
            SQL += "'" + company_info.CompanyCountryID + "',";
            SQL += "'" + company_info.CompanyName + "',";
            SQL += "'" + company_info.CompanyVAT + "',";
            SQL += "'" + company_info.ReadCode + "',";
            SQL += "'" + company_info.WriteCode + "',";
            SQL += "'" + company_info.EMail + "',";
            SQL += "'" + company_info.maam + "',";
            SQL += "'" + company_info.FilesSearch + "',";
            SQL += "'" + company_info.DataPath + "',";
            SQL += "'" + CompanySerialNumber + "',";
            SQL += "'" + company_info.SystemColor + "',";
            SQL += "'" + company_info.MobilePhone + "',";
            SQL += "'" + (company_info.InformMyMobile ? 1 : 0) + "'";            

            SQL += ")";

            return SQLExecute(SQL) ? CompanySerialNumber : "";
        }

        public string AddExistingCompanyInfo(CompanyInfo company_info)
        {
            String SQL = "INSERT INTO CompanyInfo (CompanyCountryID , CompanyName, CompanyVAT, ReadCode, WriteCode, EMail, maam, FilesSearch, DataPath, CompanySerialNumber, SystemColor, MobilePhone, InformMyMobile)";
            SQL += " VALUES ( ";
            SQL += "'" + company_info.CompanyCountryID + "',";
            SQL += "'" + company_info.CompanyName + "',";
            SQL += "'" + company_info.CompanyVAT + "',";
            SQL += "'" + company_info.ReadCode + "',";
            SQL += "'" + company_info.WriteCode + "',";
            SQL += "'" + company_info.EMail + "',";
            SQL += "'" + company_info.maam + "',";
            SQL += "'" + company_info.FilesSearch + "',";
            SQL += "'" + company_info.DataPath + "',";
            SQL += "'" + company_info.CompanySerialNumber + "',";
            SQL += "'" + company_info.SystemColor + "',";
            SQL += "'" + company_info.MobilePhone + "',";
            SQL += "'" + (company_info.InformMyMobile ? 1 : 0) + "'";            
            SQL += ")";

            return SQLExecute(SQL) ? company_info.CompanySerialNumber : "";
        }

        public String AddHakladaInboxTochenTnuaRecord(TochenTnua tochen_tnua)
        {
            if (tochen_tnua == null)
                return null;

            String SQL = "INSERT INTO HakladatInboxTochenTnua ( KTGUID, CompanyID, MisparPnimi, MisparShuraBaTnua, CodeParitNumeri, CodeParitAlpha, TeurParitAlpha, Kamut1, Kamut2, Kamut3, Kamut4,";
            SQL += " KamutKlalit, TeurYahida, MechirYehida, SchumLefniHanacha, AhuzHanacha, SchumHanacha, SchumShura, AhuzHaMaam, CodeMatbea, SharMatbea,";
            SQL += " MechirYehidaBeMatbea, SchumShuraMatbeaLefniHanacha, SchumHanachaBeMatbea, SchumShuraMatbeaLacharHanacha, BarCodeParit, Transfered)";
            SQL += " VALUES ( ";
            SQL += "'" + tochen_tnua.TransactionGUID + "',";
            SQL += "'" + tochen_tnua.CompanyID + "',";
            SQL += "'" + tochen_tnua.MisparPnimi + "',";
            SQL += "'" + tochen_tnua.MisparShuraBaTnua + "',";
            SQL += "'" + tochen_tnua.CodeParitNumeri + "',";
            SQL += "'" + tochen_tnua.CodeParitAlpha + "',";
            SQL += "'" + tochen_tnua.TeurParitAlpha + "',";

            SQL += "'" + tochen_tnua.Kamut1 + "',";
            SQL += "'" + tochen_tnua.Kamut2 + "',";
            SQL += "'" + tochen_tnua.Kamut3 + "',";
            SQL += "'" + tochen_tnua.Kamut4 + "',";
            SQL += "'" + tochen_tnua.KamutKlalit + "',";

            SQL += "'" + tochen_tnua.TeurYahida + "',";
            SQL += "'" + tochen_tnua.MechirYehida + "',";
            SQL += "'" + tochen_tnua.SchumLefniHanacha + "',";
            SQL += "'" + tochen_tnua.AhuzHanacha + "',";
            SQL += "'" + tochen_tnua.SchumHanacha + "',";
            SQL += "'" + tochen_tnua.SchumShura + "',";
            SQL += "'" + tochen_tnua.AhuzHaMaam + "',";
            SQL += "'" + tochen_tnua.CodeMatbea + "',";
            SQL += "'" + tochen_tnua.SharMatbea + "',";

            SQL += "'" + tochen_tnua.MechirYehidaBeMatbea + "',";
            SQL += "'" + tochen_tnua.SchumShuraMatbeaLefniHanacha + "',";
            SQL += "'" + tochen_tnua.SchumHanachaBeMatbea + "',";
            SQL += "'" + tochen_tnua.SchumShuraMatbeaLacharHanacha + "',";
            SQL += "'" + tochen_tnua.BarCodeParit + "',";
            SQL += "'" + (tochen_tnua.Transfered ? 1 : 0) + "'";
            SQL += ")";

            if (SQLExecute(SQL))
            {
                return tochen_tnua.TransactionGUID;
            }
            else
            {
                return null;
            }
        }
        public String AddTochenTnuaHakladaRecord(TochenTnua tochen_tnua)
        {
            if (tochen_tnua == null)
                return null;

            String SQL = "INSERT INTO HakladatTochenTnua ( KTGUID, CompanyID, MisparPnimi, MisparShuraBaTnua, CodeParitNumeri, CodeParitAlpha, TeurParitAlpha, Kamut1, Kamut2, Kamut3, Kamut4,";
            SQL += " KamutKlalit, TeurYahida, MechirYehida, SchumLefniHanacha, AhuzHanacha, SchumHanacha, SchumShura, AhuzHaMaam, CodeMatbea, SharMatbea,";
            SQL += " MechirYehidaBeMatbea, SchumShuraMatbeaLefniHanacha, SchumHanachaBeMatbea, SchumShuraMatbeaLacharHanacha, BarCodeParit, Transfered)";
            SQL += " VALUES ( ";
            SQL += "'" + tochen_tnua.TransactionGUID + "',";
            SQL += "'" + tochen_tnua.CompanyID + "',";
            SQL += "'" + tochen_tnua.MisparPnimi + "',";
            SQL += "'" + tochen_tnua.MisparShuraBaTnua + "',";
            SQL += "'" + tochen_tnua.CodeParitNumeri + "',";
            SQL += "'" + tochen_tnua.CodeParitAlpha + "',";
            SQL += "'" + tochen_tnua.TeurParitAlpha + "',";

            SQL += "'" + tochen_tnua.Kamut1 + "',";
            SQL += "'" + tochen_tnua.Kamut2 + "',";
            SQL += "'" + tochen_tnua.Kamut3 + "',";
            SQL += "'" + tochen_tnua.Kamut4 + "',";
            SQL += "'" + tochen_tnua.KamutKlalit + "',";

            SQL += "'" + tochen_tnua.TeurYahida + "',";
            SQL += "'" + tochen_tnua.MechirYehida + "',";
            SQL += "'" + tochen_tnua.SchumLefniHanacha + "',";
            SQL += "'" + tochen_tnua.AhuzHanacha + "',";
            SQL += "'" + tochen_tnua.SchumHanacha + "',";
            SQL += "'" + tochen_tnua.SchumShura + "',";
            SQL += "'" + tochen_tnua.AhuzHaMaam + "',";
            SQL += "'" + tochen_tnua.CodeMatbea + "',";
            SQL += "'" + tochen_tnua.SharMatbea + "',";

            SQL += "'" + tochen_tnua.MechirYehidaBeMatbea + "',";
            SQL += "'" + tochen_tnua.SchumShuraMatbeaLefniHanacha + "',";
            SQL += "'" + tochen_tnua.SchumHanachaBeMatbea + "',";
            SQL += "'" + tochen_tnua.SchumShuraMatbeaLacharHanacha + "',";
            SQL += "'" + tochen_tnua.BarCodeParit + "',";
            SQL += "'" + (tochen_tnua.Transfered ? 1 : 0) + "'";
            SQL += ")";

            if (SQLExecute(SQL))
            {
                return tochen_tnua.TransactionGUID;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateTochenTnuaHakladaInbox(TochenTnua tochen_tnua)
        {
            String SQL = "Update HakladatInboxTochenTnua SET ";

            SQL += "CompanyID = '" + tochen_tnua.CompanyID + "',";
            SQL += "MisparPnimi = '" + tochen_tnua.MisparPnimi + "',";
            SQL += "MisparShuraBaTnua = '" + tochen_tnua.MisparShuraBaTnua + "',";

            SQL += "CodeParitNumeri = '" + tochen_tnua.CodeParitNumeri + "',";
            SQL += "CodeParitAlpha = '" + tochen_tnua.CodeParitAlpha + "',";
            SQL += "TeurParitAlpha = '" + tochen_tnua.TeurParitAlpha + "',";

            SQL += "Kamut1 = '" + tochen_tnua.Kamut1 + "',";
            SQL += "Kamut2 = '" + tochen_tnua.Kamut2 + "',";
            SQL += "Kamut3 = '" + tochen_tnua.Kamut3 + "',";
            SQL += "Kamut4 = '" + tochen_tnua.Kamut4 + "',";
            SQL += "KamutKlalit = '" + tochen_tnua.KamutKlalit + "',";

            SQL += "TeurYahida = '" + tochen_tnua.TeurYahida + "',";
            SQL += "MechirYehida = '" + tochen_tnua.MechirYehida + "',";
            SQL += "SchumLefniHanacha = '" + tochen_tnua.SchumLefniHanacha + "',";
            SQL += "AhuzHanacha = '" + tochen_tnua.AhuzHanacha + "',";
            SQL += "SchumHanacha = '" + tochen_tnua.SchumHanacha + "',";

            SQL += "SchumShura = '" + tochen_tnua.SchumShura + "',";
            SQL += "AhuzHaMaam = '" + tochen_tnua.AhuzHaMaam + "',";
            SQL += "CodeMatbea = '" + tochen_tnua.CodeMatbea + "',";
            SQL += "SharMatbea = '" + tochen_tnua.SharMatbea + "',";
            SQL += "MechirYehidaBeMatbea = '" + tochen_tnua.MechirYehidaBeMatbea + "',";
            SQL += "SchumShuraMatbeaLefniHanacha = '" + tochen_tnua.SchumShuraMatbeaLefniHanacha + "',";
            SQL += "SchumHanachaBeMatbea = '" + tochen_tnua.SchumHanachaBeMatbea + "',";
            SQL += "SchumShuraMatbeaLacharHanacha = '" + tochen_tnua.SchumShuraMatbeaLacharHanacha + "',";
            SQL += "BarCodeParit = '" + tochen_tnua.BarCodeParit + "',";

            SQL += "Transfered = '" + (tochen_tnua.Transfered ? 1 : 0) + "'";

            SQL += " WHERE RawDataNumber = " + tochen_tnua.RawDataNumber;

            return SQLExecute(SQL);
        }

        public bool UpdateTochenTnuaHaklada(TochenTnua tochen_tnua)
        {
            String SQL = "Update HakladatTochenTnua SET ";

            SQL += "CompanyID = '" + tochen_tnua.CompanyID + "',";
            SQL += "MisparPnimi = '" + tochen_tnua.MisparPnimi + "',";
            SQL += "MisparShuraBaTnua = '" + tochen_tnua.MisparShuraBaTnua + "',";

            SQL += "CodeParitNumeri = '" + tochen_tnua.CodeParitNumeri + "',";
            SQL += "CodeParitAlpha = '" + tochen_tnua.CodeParitAlpha + "',";
            SQL += "TeurParitAlpha = '" + tochen_tnua.TeurParitAlpha + "',";

            SQL += "Kamut1 = '" + tochen_tnua.Kamut1 + "',";
            SQL += "Kamut2 = '" + tochen_tnua.Kamut2 + "',";
            SQL += "Kamut3 = '" + tochen_tnua.Kamut3 + "',";
            SQL += "Kamut4 = '" + tochen_tnua.Kamut4 + "',";
            SQL += "KamutKlalit = '" + tochen_tnua.KamutKlalit + "',";

            SQL += "TeurYahida = '" + tochen_tnua.TeurYahida + "',";
            SQL += "MechirYehida = '" + tochen_tnua.MechirYehida + "',";
            SQL += "SchumLefniHanacha = '" + tochen_tnua.SchumLefniHanacha + "',";
            SQL += "AhuzHanacha = '" + tochen_tnua.AhuzHanacha + "',";
            SQL += "SchumHanacha = '" + tochen_tnua.SchumHanacha + "',";

            SQL += "SchumShura = '" + tochen_tnua.SchumShura + "',";
            SQL += "AhuzHaMaam = '" + tochen_tnua.AhuzHaMaam + "',";
            SQL += "CodeMatbea = '" + tochen_tnua.CodeMatbea + "',";
            SQL += "SharMatbea = '" + tochen_tnua.SharMatbea + "',";
            SQL += "MechirYehidaBeMatbea = '" + tochen_tnua.MechirYehidaBeMatbea + "',";
            SQL += "SchumShuraMatbeaLefniHanacha = '" + tochen_tnua.SchumShuraMatbeaLefniHanacha + "',";
            SQL += "SchumHanachaBeMatbea = '" + tochen_tnua.SchumHanachaBeMatbea + "',";
            SQL += "SchumShuraMatbeaLacharHanacha = '" + tochen_tnua.SchumShuraMatbeaLacharHanacha + "',";
            SQL += "BarCodeParit = '" + tochen_tnua.BarCodeParit + "',";

            SQL += "Transfered = '" + (tochen_tnua.Transfered ? 1 : 0) + "'";

            SQL += " WHERE RawDataNumber = " + tochen_tnua.RawDataNumber;

            return SQLExecute(SQL);
        }

        public String AddHakladaRecord(ShuratHaklada shurat_haklada)
        {
            if (shurat_haklada == null)
                return null;

            String GUID = "{" + Guid.NewGuid().ToString() + "}";
            String SQL = "INSERT INTO Haklada (TransactionGUID, CompanyID, ActionCode, MisparMismach, TarichMismach, TarichAcher, ActionDetails, Maam, SchumPaturMaam, SchumMaam, SchumKolelMaam, Attachment, CompamyInfoCountryID, CompamyInfoVAT, DateSent, LeTkufaMe, LeTkufaUd, MisparProyect)";
            SQL += " VALUES ( ";
            SQL += "'" + GUID + "',";
            SQL += "'" + shurat_haklada.CompanyID + "',";
            SQL += "'" + shurat_haklada.ActionCode + "',";
            SQL += "'" + shurat_haklada.MisparMismach + "',";
            SQL += "'" + shurat_haklada.TarichMismach + "',";
            SQL += "'" + shurat_haklada.TarichAcher + "',";
            SQL += "'" + shurat_haklada.ActionDetails + "',";
            SQL += "'" + shurat_haklada.AhuzHaMaam + "',";
            SQL += "'" + shurat_haklada.SchumPaturMaam + "',";
            SQL += "'" + shurat_haklada.SchumMaam + "',";
            SQL += "'" + shurat_haklada.SchumKolelMaam + "',";
            SQL += (shurat_haklada.Attachment != null) ? "'" + shurat_haklada.Attachment + "'," : "'',";
            SQL += "'" + shurat_haklada.CompamyInfoCountryID + "',";
            SQL += "'" + shurat_haklada.CompamyInfoVAT + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + shurat_haklada.LeTkufaMe + "',";
            SQL += "'" + shurat_haklada.LeTkufaUd + "',";
            SQL += "'" + shurat_haklada.MisparProyect + "'";
            SQL += ")";

            if (SQLExecute(SQL))
            {
                return GUID;
            }
            else
            {
                return null;
            }
        }

        public void AddHakladaInboxRecord(ShuratHaklada shurat_haklada)
        {
            String SQL = "INSERT INTO HakladaInbox (TransactionGUID, CompanyID, ActionCode, MisparMismach, TarichMismach, TarichAcher, ActionDetails, Maam, SchumPaturMaam, SchumMaam, SchumKolelMaam, Attachment, CompamyInfoCountryID, CompamyInfoVAT, DateSent, LeTkufaMe, LeTkufaUd, MisparProyect)";
            SQL += " VALUES ( ";
            SQL += "'{" + Guid.NewGuid().ToString() + "}',";
            SQL += "'" + shurat_haklada.CompanyID + "',";
            SQL += "'" + shurat_haklada.ActionCode + "',";
            SQL += "'" + shurat_haklada.MisparMismach + "',";
            SQL += "'" + shurat_haklada.TarichMismach + "',";
            SQL += "'" + shurat_haklada.TarichAcher + "',";
            SQL += "'" + shurat_haklada.ActionDetails + "',";
            SQL += "'" + shurat_haklada.AhuzHaMaam + "',";
            SQL += "'" + shurat_haklada.SchumPaturMaam + "',";
            SQL += "'" + shurat_haklada.SchumMaam + "',";
            SQL += "'" + shurat_haklada.SchumKolelMaam + "',";
            SQL += (shurat_haklada.Attachment != null) ? "'" + shurat_haklada.Attachment + "'," : "'',";
            SQL += "'" + shurat_haklada.CompamyInfoCountryID + "',";
            SQL += "'" + shurat_haklada.CompamyInfoVAT + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + shurat_haklada.LeTkufaMe + "',";
            SQL += "'" + shurat_haklada.LeTkufaUd + "',";
            SQL += "'" + shurat_haklada.MisparProyect + "'";
            SQL += ")";

            SQLExecute(SQL);
        }

        public void AddCreateRecord(TochenTnua tt, String write_code, String attachment)
        {
            String SQL = "INSERT INTO Outbox (TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, TimeStampWrite, WriteCode, Attachmnent, CompamyInfoCountryID, CompamyInfoVAT)";
            SQL += " VALUES ( ";
            SQL += "'" + tt.TransactionGUID + "',";
            SQL += "'" + tt.CountryIDFrom + "',";
            SQL += "'" + tt.VatFrom + "',";
            SQL += "'" + tt.CountryIDTo + "',";
            SQL += "'" + tt.VatTo + "',";
            SQL += "'" + tt.Data + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + write_code + "',";
            SQL += (attachment != null) ? "'" + attachment + "'," : "'',";
            SQL += "'" + Current_Company_Info.CompanyCountryID + "',";
            SQL += "'" + Current_Company_Info.CompanyVAT + "'";
            SQL += ")";

            SQLExecute(SQL);
        }

        public void AddCreateRecord(KoteretTnua kt, String write_code, String attachment)
        {
            String SQL = "INSERT INTO Outbox (TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, TimeStampWrite, WriteCode, Attachmnent, CompamyInfoCountryID, CompamyInfoVAT)";
            SQL += " VALUES ( ";
            SQL += "'" + kt.TransactionGUID + "',";
            SQL += "'" + kt.CountryIDFrom + "',";
            SQL += "'" + kt.VatFrom + "',";
            SQL += "'" + kt.CountryIDTo + "',";
            SQL += "'" + kt.VatTo + "',";
            SQL += "'" + kt.Data + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + write_code + "',";
            SQL += (attachment != null) ? "'" + attachment + "'," : "'',";
            SQL += "'" + Current_Company_Info.CompanyCountryID + "',";
            SQL += "'" + Current_Company_Info.CompanyVAT + "'";
            SQL += ")";

            SQLExecute(SQL);
        }

        public void AddCreateRecord(WriteCodeRequest wr, String write_code, String attachment)
        {
            String SQL = "INSERT INTO Outbox (TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, TimeStampWrite, WriteCode, Attachmnent, CompamyInfoCountryID, CompamyInfoVAT)";
            SQL += " VALUES ( ";
            SQL += "'" + wr.TransactionGUID + "',";
            SQL += "'" + wr.CountryIDFrom + "',";
            SQL += "'" + wr.VatFrom + "',";
            SQL += "'" + wr.CountryIDTo + "',";
            SQL += "'" + wr.VatTo + "',";
            SQL += "'" + wr.Data + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + write_code + "',";
            SQL += (attachment != null) ? "'" + attachment + "'," : "'',";
            SQL += "'" + Current_Company_Info.CompanyCountryID + "',";
            SQL += "'" + Current_Company_Info.CompanyVAT + "'";
            SQL += ")";

            SQLExecute(SQL);
        }

        public void AddCreateInboxRecord(WriteCodeRequest wr, String write_code, String attachment)
        {
            String SQL = "INSERT INTO Inbox (TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, TimeStampWrite, TimeStampRead, WriteCode, Attachmnent, CompamyInfoCountryID, CompamyInfoVAT, Confirm0, Confirm1, Confirm2)";
            SQL += " VALUES ( ";
            SQL += "'" + wr.TransactionGUID + "',";
            SQL += "'" + wr.CountryIDFrom + "',";
            SQL += "'" + wr.VatFrom + "',";
            SQL += "'" + wr.CountryIDTo + "',";
            SQL += "'" + wr.VatTo + "',";
            SQL += "'" + wr.Data + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + write_code + "',";
            SQL += (attachment != null) ? "'" + attachment + "'," : "'',";
            SQL += "'" + Current_Company_Info.CompanyCountryID + "',";
            SQL += "'" + Current_Company_Info.CompanyVAT + "',";
            SQL += "'0',";
            SQL += "'0',";
            SQL += "'0'";
            SQL += ")";

            SQLExecute(SQL);
        }

        public bool AddCreateInboxRecord(KoteretTnua kt, String write_code, String attachment)
        {
            String SQL = "INSERT INTO Inbox (TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, TimeStampWrite, TimeStampRead, WriteCode, Attachmnent, CompamyInfoCountryID, CompamyInfoVAT, Confirm0, Confirm1, Confirm2)";
            SQL += " VALUES ( ";
            SQL += "'" + kt.TransactionGUID + "',";
            SQL += "'" + kt.CountryIDFrom + "',";
            SQL += "'" + kt.VatFrom + "',";
            SQL += "'" + kt.CountryIDTo + "',";
            SQL += "'" + kt.VatTo + "',";
            SQL += "'" + kt.Data + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + write_code + "',";
            SQL += (attachment != null) ? "'" + attachment + "'," : "'',";
            SQL += "'" + Current_Company_Info.CompanyCountryID + "',";
            SQL += "'" + Current_Company_Info.CompanyVAT + "',";
            SQL += "'0',";
            SQL += "'0',";
            SQL += "'0'";
            SQL += ")";

            return SQLExecute(SQL);
        }

        public void AddCreateInboxRecord(TochenTnua tt, String write_code, String attachment)
        {
            String SQL = "INSERT INTO Inbox (TransactionGUID, CountryIDFrom, CompanyVATFrom, CountryIDTo, CompanyVATTo, Data, TimeStampWrite, TimeStampRead, WriteCode, Attachmnent, CompamyInfoCountryID, CompamyInfoVAT, Confirm0, Confirm1, Confirm2)";
            SQL += " VALUES ( ";
            SQL += "'" + tt.TransactionGUID + "',";
            SQL += "'" + tt.CountryIDFrom + "',";
            SQL += "'" + tt.VatFrom + "',";
            SQL += "'" + tt.CountryIDTo + "',";
            SQL += "'" + tt.VatTo + "',";
            SQL += "'" + tt.Data + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + DateTime.Now + "',";
            SQL += "'" + write_code + "',";
            SQL += (attachment != null) ? "'" + attachment + "'," : "'',";
            SQL += "'" + Current_Company_Info.CompanyCountryID + "',";
            SQL += "'" + Current_Company_Info.CompanyVAT + "',";
            SQL += "'0',";
            SQL += "'0',";
            SQL += "'0'";
            SQL += ")";

            SQLExecute(SQL);
        }

        public void ReadShuratHakladaList(ListView lvw, String CountryID, String CompanyVAT, Dictionary<int, SugTnua> ActionsListDictionary)
        {
            lvw.Items.Clear();
            DataTable dataTable = new DataTable("ShuratHakladaList");
            //combobox.Items.Clear();
            Open();
            OdbcCommand cmd = new OdbcCommand("SELECT * FROM Haklada WHERE Transfered = 0 AND CompamyInfoVAT = '" + CompanyVAT + "' AND CompamyInfoCountryID = " + CountryID, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            bool bHasColumns = lvw.Columns.Count > 0;

            if (reader.HasRows)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataTable.Columns.Add(reader.GetName(i), typeof(String));
                    if (!bHasColumns)
                    {
                        lvw.Columns.Add(reader.GetName(i).ToString());
                    }
                }
            }

            while (reader.Read())
            {
                String field = reader.GetValue(1).ToString();

                SugTnua sug_tnua = ActionsListDictionary[Convert.ToInt32(reader.GetValue(2).ToString())];

                ListViewItem lvi = lvw.Items.Add("");
                if (Convert.ToBoolean(reader.GetValue(15).ToString()))
                {
                    lvi.ImageIndex = 4;
                }
                else
                {
                    lvi.ImageIndex = Convert.ToBoolean(reader.GetValue(12).ToString()) ? 3 : 2;
                }
                lvi.SubItems.Add(sug_tnua.ActionTypeIN);
                lvi.SubItems.Add(reader.GetValue(3).ToString());
                lvi.SubItems.Add(Convert.ToDateTime(reader.GetValue(4).ToString()).ToString("dd/MM/yy"));
                lvi.SubItems.Add(Convert.ToDateTime(reader.GetValue(5).ToString()).ToString("dd/MM/yy"));
                Company company = GetCompany(reader.GetValue(1).ToString());
                if (company == null)
                {
                    lvi.SubItems.Add("company id err");
                }
                else
                {
                    lvi.SubItems.Add(company.CompanyName);
                }
                lvi.SubItems.Add(reader.GetValue(6).ToString());
                double db = (Convert.ToDouble(reader.GetValue(8).ToString()) + Convert.ToDouble(reader.GetValue(10).ToString()));
                lvi.SubItems.Add(db.ToString("0.00"));
                //lvi.SubItems.Add(Convert.ToBoolean(reader.GetValue(12).ToString()) ? dt.ToString("dd/MM/yy") : "");
                lvi.SubItems.Add("");
                ShuratHaklada shurat_haklada = CreateShuratHaklada(reader);
                lvi.Tag = shurat_haklada;
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close();
        }

        public void ReadShuratHakladaInboxList(ListView lvw, String CountryID, String CompanyVAT, Dictionary<int, SugTnua> ActionsListDictionary)
        {
            lvw.Items.Clear();
            DataTable dataTable = new DataTable("ShuratHakladaList");
            //combobox.Items.Clear();
            Open();
            OdbcCommand cmd = new OdbcCommand("SELECT * FROM HakladaInbox WHERE Transfered = 0 AND CompamyInfoVAT = '" + CompanyVAT + "' AND CompamyInfoCountryID = " + CountryID, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            bool bHasColumns = lvw.Columns.Count > 0;

            if (reader.HasRows)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataTable.Columns.Add(reader.GetName(i), typeof(String));
                    if (!bHasColumns)
                    {
                        lvw.Columns.Add(reader.GetName(i).ToString());
                    }
                }
            }

            while (reader.Read())
            {
                String field = reader.GetValue(1).ToString();

                SugTnua sug_tnua = ActionsListDictionary[Convert.ToInt32(reader.GetValue(2).ToString())];

                ListViewItem lvi = lvw.Items.Add("");
                if (Convert.ToBoolean(reader.GetValue(15).ToString()))
                {
                    lvi.ImageIndex = 4;
                }
                else
                {
                    lvi.ImageIndex = Convert.ToBoolean(reader.GetValue(12).ToString()) ? 3 : 2;
                }
                lvi.SubItems.Add(sug_tnua.ActionTypeOUT);
                lvi.SubItems.Add(reader.GetValue(3).ToString());
                lvi.SubItems.Add(Convert.ToDateTime(reader.GetValue(4).ToString()).ToString("dd/MM/yy"));
                lvi.SubItems.Add(Convert.ToDateTime(reader.GetValue(5).ToString()).ToString("dd/MM/yy"));
                Company company = GetCompany(reader.GetValue(1).ToString());
                lvi.SubItems.Add(company.CompanyName);
                lvi.SubItems.Add(reader.GetValue(6).ToString());
                double db = (Convert.ToDouble(reader.GetValue(8).ToString()) + Convert.ToDouble(reader.GetValue(10).ToString()));
                lvi.SubItems.Add(db.ToString("0.00"));
                //lvi.SubItems.Add(Convert.ToBoolean(reader.GetValue(12).ToString()) ? dt.ToString("dd/MM/yy") : "");
                lvi.SubItems.Add("");
                ShuratHaklada shurat_haklada = CreateShuratHaklada(reader);
                lvi.Tag = shurat_haklada;
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close();
        }

        public void IsTransactionReaden(Parser parser, ListView lvwHaklada)
        {
            for (int i = 0; i < lvwHaklada.Items.Count; i++)
            {
                ShuratHaklada shurat_haklada = (ShuratHaklada)lvwHaklada.Items[i].Tag;
                if (!shurat_haklada.Readen)
                {
                    lvwHaklada.Items[i].ImageIndex = (shurat_haklada.Transfered ? 3 : 2);
                    if (parser.IsTransactionReaden(shurat_haklada.TransactionGUID))
                    {
                        lvwHaklada.Items[i].ImageIndex = 4;
                        shurat_haklada.Readen = true;
                        if (!UpdateShuratHaklada(shurat_haklada))
                        {
                            MessageBox.Show("UpdateShuratHaklada!", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        public void ReadKotertTnuaListMain(Parser parser, ListView lvw, String CountryID, String CompanyVAT, Dictionary<int, SugTnua> ActionsListDictionary, DateTime dtFrom, DateTime dtTo)
        {
            lvw.Items.Clear();

            Open();
            String SQL = "SELECT * FROM Inbox WHERE CompamyInfoVAT= '" + CompanyVAT + "' AND CompamyInfoCountryID = " + CountryID;
            SQL += " AND (TimeStampRead >= #" + dtFrom.ToString("MM/dd/yyy") + " 00:00:00# AND TimeStampRead <= #" + dtTo.ToString("MM/dd/yyy") + " 23:59:59#)";

            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            String AllRowsData = null;

            while (reader.Read())
            {
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    AllRowsData += reader[j].ToString() + "|$";
                }
                AllRowsData = AllRowsData.Substring(0, AllRowsData.Length - 2);
                AllRowsData += Environment.NewLine;
            }

            lvw.Items.Clear();

            ArrayList AllData = parser.GetInternalData(AllRowsData);
            foreach (var item in AllData)
            {
                if (item.GetType() == typeof(KoteretTnua))
                {
                    KoteretTnua kt = (KoteretTnua)item;
                    SugTnua sug_tnua = ActionsListDictionary[Convert.ToInt32(kt.SugTnua.ToString())];

                    String TransactionID = kt.TransactionGUID;
                    DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "/TransientStorage/" + CountryID + "/" + CompanyVAT + "/");
                    FileInfo[] fi = di.GetFiles(kt.TransactionGUID + ".*");

                    String FileName = "";
                    if (fi.Length > 0)
                    {
                        FileName = Application.StartupPath + "/TransientStorage/" + CountryID + "/" + CompanyVAT + "/" + fi[0];
                    }

                    ListViewItem lvi = lvw.Items.Add(""); //kt.SugTnua

                    Color color = Color.Pink;

                    if (kt.Confirm2)
                        color = Color.FromArgb(255, 255, 192);

                    if (kt.Confirm1)
                        color = Color.FromArgb(255, 224, 192);

                    if (kt.Confirm0)
                        color = Color.FromArgb(192, 255, 192);

                    lvi.BackColor = color;

                    if (File.Exists(FileName))
                    {
                        lvi.ImageIndex = 5;
                    }

                    //ListViewItem lvi = lvwInData.Items.Add("");                    
                    lvi.Tag = kt; // kt.TransactionGUID;
                    //lvi.SubItems.Add(kt.SugTnua.ToString());
                    lvi.SubItems.Add(sug_tnua.ActionTypeOUT);
                    lvi.SubItems.Add(kt.MisparMismach.ToString());
                    //lvi.SubItems.Add(kt.TarichMismach.ToShortDateString());                    
                    lvi.SubItems.Add(kt.TarichMismach.ToString("dd/MM/yy"));
                    lvi.SubItems.Add(kt.ShemHaSholeah);
                    lvi.SubItems.Add(kt.OsekMoorshehHaSholeah);
                    lvi.SubItems.Add(kt.MeidaNosaf.ToString());
                    //lvi.SubItems.Add(kt.SchumLifneMaam.ToString());
                    //lvi.SubItems.Add(kt.SchumHaMaam.ToString());
                    double db = kt.SchumKolelMaam + kt.SchumPaturMeMaam;
                    lvi.SubItems.Add(db.ToString("0.00"));
                    lvi.SubItems.Add(kt.TimeStampRead.ToString("dd/MM/yy"));

                    //company not exist
                    //lvi.BackColor = CheckCompanyExist(kt) ? Color.White : Color.Violet;
                }
                //else if (item.GetType() == typeof(WriteCodeRequest))
                //{
                //    WriteCodeRequest wr = (WriteCodeRequest)item;

                //    String TransactionID = wr.TransactionGUID;
                //    DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "/TransientStorage/" + CountryID + "/" + CompanyVAT + "/");
                //    FileInfo[] fi = di.GetFiles(wr.TransactionGUID + ".*");

                //    String FileName = "";
                //    if (fi.Length > 0)
                //    {
                //        FileName = Application.StartupPath + "/TransientStorage/" + CountryID + "/" + CompanyVAT + "/" + fi[0];
                //    }

                //    ListViewItem lvi = lvw.Items.Add(""); //kt.SugTnua

                //    if (File.Exists(FileName))
                //    {
                //        lvi.ImageIndex = 5;
                //    }
                //    lvi.ImageIndex = 6;

                //    //ListViewItem lvi = lvwInData.Items.Add("");            
                //    lvi.Tag = wr; // kt.TransactionGUID;                               
                //    lvi.SubItems.Add("");
                //    lvi.SubItems.Add("");
                //    lvi.SubItems.Add("");
                //    lvi.SubItems.Add(wr.ShemHaSholeah);
                //    lvi.SubItems.Add(wr.OsekMoorshehHaSholeah);
                //    lvi.SubItems.Add(wr.Message);
                //    lvi.SubItems.Add("");
                //    lvi.SubItems.Add(wr.TimeStampRead.ToString("dd/MM/yy"));

                //    lvi.BackColor = CheckCompanyExist(wr) ? Color.White : Color.Violet;
                //}

                if ((reader != null) && (!reader.IsClosed))
                {
                    reader.Close();
                }
            }

            Close();
            lvw.Invalidate();
        }

        public void ReadRequestsList(Parser parser, ListView lvw, String CountryID, String CompanyVAT, DateTime dtFrom, DateTime dtTo)
        {
            lvw.Items.Clear();

            Open();
            String SQL = "SELECT * FROM Inbox WHERE CompamyInfoVAT= '" + CompanyVAT + "' AND CompamyInfoCountryID = " + CountryID;
            SQL += " AND (TimeStampRead >= #" + dtFrom.ToString("MM/dd/yyy") + " 00:00:00# AND TimeStampRead <= #" + dtTo.ToString("MM/dd/yyy") + " 23:59:59#)";

            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            String AllRowsData = null;

            while (reader.Read())
            {
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    AllRowsData += reader[j].ToString() + "|$";
                }
                AllRowsData = AllRowsData.Substring(0, AllRowsData.Length - 2);
                AllRowsData += Environment.NewLine;
            }

            lvw.Items.Clear();

            ArrayList AllData = parser.GetInternalData(AllRowsData);
            foreach (var item in AllData)
            {
                if (item.GetType() == typeof(WriteCodeRequest))
                {
                    WriteCodeRequest wr = (WriteCodeRequest)item;

                    String TransactionID = wr.TransactionGUID;
                    DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "/TransientStorage/" + CountryID + "/" + CompanyVAT + "/");
                    ListViewItem lvi = lvw.Items.Add(""); //kt.SugTnua

                    if (di.Exists)
                    {
                        FileInfo[] fi = di.GetFiles(wr.TransactionGUID + ".*");

                        String FileName = "";
                        if (fi.Length > 0)
                        {
                            FileName = Application.StartupPath + "/TransientStorage/" + CountryID + "/" + CompanyVAT + "/" + fi[0];
                        }

                        if (File.Exists(FileName))
                        {
                            lvi.ImageIndex = 5;
                        }
                    }

                    lvi.ImageIndex = 6;

                    //ListViewItem lvi = lvwInData.Items.Add("");            
                    lvi.Tag = wr; // kt.TransactionGUID;                               
                    lvi.SubItems.Add(wr.ShemHaSholeah);
                    lvi.SubItems.Add(wr.OsekMoorshehHaSholeah);
                    if (wr.Message != null)
                    {
                        lvi.SubItems.Add(wr.Message);
                    }
                    else
                    {
                        lvi.SubItems.Add(wr.Answer);
                    }
                    lvi.SubItems.Add(wr.TimeStampRead.ToString("dd/MM/yy"));

                    lvi.BackColor = CheckCompanyExist(wr) ? Color.White : Color.Violet;
                }

                if ((reader != null) && (!reader.IsClosed))
                {
                    reader.Close();
                }
            }

            Close();
            lvw.Invalidate();
        }

        public void ReadShuratHakladaListMain(ListView lvw, String CountryID, String CompanyVAT, bool bWaiting, bool bTransfered, bool bReaden, Dictionary<int, SugTnua> ActionsListDictionary, DateTime dtFrom, DateTime dtTo)
        {
            lvw.Items.Clear();

            if (!bWaiting && !bTransfered && !bReaden)
                return;

            Open();
            String SQL = "SELECT * FROM Haklada WHERE CompamyInfoVAT= '" + CompanyVAT + "' AND CompamyInfoCountryID = " + CountryID;

            if (bWaiting || bTransfered || bReaden)
            {
                SQL += " AND (";

                if (bWaiting && bTransfered && !bReaden)
                {
                    SQL += " Readen = False"; //Transfered = True OR Transfered = False  AND 
                    SQL += ")";
                }
                else if (!bWaiting && bTransfered && !bReaden)
                {
                    SQL += " Transfered = True AND Readen = False";
                    SQL += ")";
                }
                else
                {
                    if (bWaiting)
                    {
                        SQL += " Transfered = False OR";
                    }

                    if (bTransfered)
                    {
                        SQL += " Transfered = True OR";
                    }

                    if (bReaden)
                    {
                        SQL += " Readen = True OR";
                    }
                }
            }

            if (SQL.Substring(SQL.Length - 3, 3) == " OR")
            {
                SQL = SQL.Substring(0, SQL.Length - 3);
                SQL += ")";
            }

            //SQL += " AND (DateSent >= #" + dtFrom.ToShortDateString() + " 00:00:00# AND DateSent <= #" + dtTo.ToShortDateString() + " 23:59:59#)";
            SQL += " AND (DateSent >= #" + dtFrom.ToString("MM/dd/yyy") + " 00:00:00# AND DateSent <= #" + dtTo.ToString("MM/dd/yyy") + " 23:59:59#)";

            //SELECT *
            //FROM Haklada
            //WHERE (((Haklada.CompamyInfoVAT)='028753390') AND ((Haklada.CompamyInfoCountryID)=117)
            // AND ((Haklada.DateSent)>=#6/1/2013# And (Haklada.DateSent)<=#12/1/2013 23:59:59#)
            // AND ((Haklada.Readen)=True)) OR (((Haklada.Readen)=False));

            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DateTime dt = (System.DateTime)(reader.GetValue(16));
                    dt = Convert.ToDateTime(dt.ToShortDateString());
                    if ((dt >= Convert.ToDateTime(dtFrom.ToShortDateString())) && (dt <= Convert.ToDateTime(dtTo.ToShortDateString())))
                    {
                        SugTnua sug_tnua = ActionsListDictionary[Convert.ToInt32(reader[2].ToString())];

                        ListViewItem lvi = lvw.Items.Add("");
                        ShuratHaklada shurat_haklada = CreateShuratHaklada(reader);
                        lvi.Tag = shurat_haklada;

                        if (Convert.ToBoolean(reader.GetValue(15).ToString()))
                        {
                            lvi.ImageIndex = 4;
                        }
                        else
                        {
                            lvi.ImageIndex = Convert.ToBoolean(reader.GetValue(12).ToString()) ? 3 : 2;
                        }
                        lvi.SubItems.Add(sug_tnua.ActionTypeIN);
                        lvi.SubItems.Add(reader.GetValue(3).ToString());
                        lvi.SubItems.Add(Convert.ToDateTime(reader.GetValue(4).ToString()).ToString("dd/MM/yy"));
                        lvi.SubItems.Add(Convert.ToDateTime(reader.GetValue(5).ToString()).ToString("dd/MM/yy"));
                        Company company = GetCompany(reader.GetValue(1).ToString());
                        lvi.SubItems.Add(company.CompanyName);                        
                        lvi.SubItems.Add(reader.GetValue(6).ToString());
                        double db = (Convert.ToDouble(reader.GetValue(8).ToString()) + Convert.ToDouble(reader.GetValue(10).ToString()));
                        lvi.SubItems.Add(db.ToString("0.00"));
                        lvi.SubItems.Add(Convert.ToBoolean(reader.GetValue(12).ToString()) ? dt.ToString("dd/MM/yy") : "");
                    }
                }
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close();
        }

        public void ReadTochenTnuaForShuratHakladaInboxListMain(ListView lvw, String TransactionGUID)
        {
            lvw.Items.Clear();

            Open();
            String SQL = "SELECT * FROM HakladatInboxTochenTnua WHERE KTGUID = '" + TransactionGUID + "'";

            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TochenTnua tochen_tnua = CreateTochenTnua(reader);

                    ListViewItem lvi = lvw.Items.Add(tochen_tnua.MisparShuraBaTnua.ToString());
                    lvi.Tag = tochen_tnua;
                    //lvi.ImageIndex = 4;

                    lvi.SubItems.Add(tochen_tnua.CodeParitAlpha);
                    lvi.SubItems.Add(tochen_tnua.TeurParitAlpha);
                    lvi.SubItems.Add(tochen_tnua.KamutKlalit.ToString("0.00"));
                    lvi.SubItems.Add(tochen_tnua.MechirYehida.ToString("0.00"));
                    lvi.SubItems.Add(tochen_tnua.SchumShura.ToString("0.00"));
                }
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close();
        }

        public void ReadTochenTnuaForShuratHakladaListMain(ListView lvw, String TransactionGUID)
        {
            lvw.Items.Clear();

            Open();
            String SQL = "SELECT * FROM HakladatTochenTnua WHERE KTGUID = '" + TransactionGUID + "'";

            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TochenTnua tochen_tnua = CreateTochenTnua(reader);

                    ListViewItem lvi = lvw.Items.Add(tochen_tnua.MisparShuraBaTnua.ToString());
                    lvi.Tag = tochen_tnua;
                    //lvi.ImageIndex = 4;

                    lvi.SubItems.Add(tochen_tnua.CodeParitAlpha);
                    lvi.SubItems.Add(tochen_tnua.TeurParitAlpha);
                    lvi.SubItems.Add(tochen_tnua.KamutKlalit.ToString("0.00"));
                    lvi.SubItems.Add(tochen_tnua.MechirYehida.ToString("0.00"));
                    lvi.SubItems.Add(tochen_tnua.SchumShura.ToString("0.00"));
                }
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close();
        }
  
        public BuisnessInfo ReadBusinessList(ListView lvw, String company_filter, CompanyInfo company_info)
        {
            BuisnessInfo buisnessInfo = new BuisnessInfo();

            lvw.Items.Clear();

            Open();
            String SQL = "SELECT CompanyID, CountryID, CompanyName, CompanyVAT, WriteCode, AccountCode, Blocked, HaveCMail, CompanyType, CompanyInfoCountryID, CompanyInfoCompanyVAT, MobilePhone FROM Companies";
            SQL += " WHERE CompanyInfoCountryID = " + company_info.CompanyCountryID + " AND CompanyInfoCompanyVAT = '" + company_info.CompanyVAT + "'";
            SQL += company_filter;
            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Company company = CreateCompany(reader);
                ListViewItem lvi = lvw.Items.Add(company.CompanyID.ToString());
                lvi.Tag = company;

                //lvi.SubItems.Add(company.CountryID.ToString());
                lvi.SubItems.Add(company.CompanyName);
                lvi.SubItems.Add(company.CompanyVAT);
                lvi.SubItems.Add(company.WriteCode);
                lvi.SubItems.Add(CompanyType.types[company.CompanyType]);
                lvi.SubItems.Add(company.AccountCode);

                lvi.ImageIndex = company.HaveCMail;

                if (company.HaveCMail == 0)
                    buisnessInfo.Active++;
                else if (company.HaveCMail == 1)
                    buisnessInfo.NotActive++;
                else if (company.HaveCMail == 2)
                    buisnessInfo.NotExist++;

                switch (company.CompanyType)
                {
                    case 0:
                        lvi.BackColor = Color.LightGreen;
                        break;
                    case 1:
                        lvi.BackColor = Color.LightPink;
                        break;
                    case 2:
                        lvi.BackColor = Color.LightSteelBlue;
                        break;
                    default:
                        break;
                }

                if (company.Blocked)
                {
                    buisnessInfo.Blocked++;
                    lvi.ImageIndex = 3;
                    lvi.BackColor = Color.Red;
                }

                buisnessInfo.Total++;
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close();

            return buisnessInfo;
        }

        public Company GetCompanySQL(String SQL)
        {
            Company company = null;

            OdbcConnection conn = null;
            conn = Open(conn);

            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    company = CreateCompany(reader);
                }
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close(conn);

            return company;
        }

        public Company GetCompany(String company_id) //, CompanyInfo company_info
        {
            String SQL = "SELECT CompanyID, CountryID, CompanyName, CompanyVAT, WriteCode, AccountCode, Blocked, HaveCMail, CompanyType, CompanyInfoCountryID, CompanyInfoCompanyVAT, MobilePhone  FROM Companies WHERE CompanyID = " + company_id;

            return GetCompanySQL(SQL);
        }

        public Company GetCompany(int CountryID, String CompanyVAT)
        {
            return GetCompany(CountryID.ToString(), CompanyVAT);
        }

        public Company GetCompanyByName(String CompanyName)
        {
            String SQL = "SELECT CompanyID, CountryID, CompanyName, CompanyVAT, WriteCode, AccountCode, Blocked, HaveCMail, CompanyType, CompanyInfoCountryID, CompanyInfoCompanyVAT, MobilePhone FROM Companies WHERE CompanyName = '" + CompanyName + "'";

            return GetCompanySQL(SQL);
        }

        public Company GetCompanyByAccountCode(String AccountCode)
        {
            String SQL = "SELECT CompanyID, CountryID, CompanyName, CompanyVAT, WriteCode, AccountCode, Blocked, HaveCMail, CompanyType, CompanyInfoCountryID, CompanyInfoCompanyVAT, MobilePhone FROM Companies WHERE AccountCode = '" + AccountCode + "'";

            return GetCompanySQL(SQL);
        }

        public Company GetCompanyByVAT(String CompanyVAT)
        {
            String SQL = "SELECT CompanyID, CountryID, CompanyName, CompanyVAT, WriteCode, AccountCode, Blocked, HaveCMail, CompanyType, CompanyInfoCountryID, CompanyInfoCompanyVAT, MobilePhone FROM Companies WHERE CompanyVAT = '" + CompanyVAT + "'";

            return GetCompanySQL(SQL);
        }

        public Company GetCompany(String CountryID, String CompanyVAT)
        {
            String SQL = "SELECT CompanyID, CountryID, CompanyName, CompanyVAT, WriteCode, AccountCode, Blocked, HaveCMail, CompanyType, CompanyInfoCountryID, CompanyInfoCompanyVAT, MobilePhone FROM Companies WHERE CountryID = " + CountryID + " AND CompanyVAT = '" + CompanyVAT + "'";

            return GetCompanySQL(SQL);
        }

        public ArrayList ReadCompaniesList()
        {
            return ReadCompaniesList(Current_Company_Info);
        }

        public ArrayList ReadCompaniesList(CompanyInfo company_info)
        {
            string SQL = "SELECT * FROM Companies";
            SQL += " WHERE CompanyInfoCountryID = " + company_info.CompanyCountryID + " AND CompanyInfoCompanyVAT = '" + company_info.CompanyVAT + "'";

            return SQLQueryArrayList(SQL);
        }

        public void ReadCompaniesInfoList(Parser parser, ListView lvw)
        {
            lvw.Items.Clear();

            Open();
            OdbcCommand cmd = new OdbcCommand("SELECT * FROM CompanyInfo", conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CompanyInfo company_info = CreateCompanyInfo(reader);

                    ListViewItem lvi = lvw.Items.Add(company_info.CompanyID.ToString());
                    lvi.ImageIndex = 0;
                    lvi.BackColor = Color.FromArgb(company_info.SystemColor == 0 ? Color.White.ToArgb() : company_info.SystemColor);
                    lvi.SubItems.Add(company_info.CompanyName);
                    lvi.SubItems.Add(company_info.CompanyVAT);
                    lvi.SubItems.Add(company_info.EMail);

                    lvi.Tag = company_info;

                    String result = parser.GetNotPaid(company_info);
                    if (result != null)
                    {
                        string[] parts = result.Split(new string[] { "|" }, StringSplitOptions.None);

                        if (parts.Length == 2)
                        {
                            lvi.SubItems.Add(parts[0]);
                            lvi.SubItems.Add(parts[1]);
                        }
                    }
                }
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close();
        }

        public CompanyInfo GetCompanyInfo(int CompanyID)
        {
            return GetCompanyInfo("SELECT * FROM CompanyInfo WHERE CompanyID = " + CompanyID);
        }

        public CompanyInfo GetCompanyInfo(String countryID, String companyVAT)
        {
            return GetCompanyInfo("SELECT * FROM CompanyInfo WHERE CompanyCountryID = " + countryID + " AND CompanyVAT = '" + companyVAT + "'");
        }

        public CompanyInfo GetCompanyInfo(String SQL)
        {
            CompanyInfo company_info = null;
            OdbcConnection conn = null;
            conn = Open(conn);
            OdbcCommand cmd = new OdbcCommand(SQL, conn);
            OdbcDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    company_info = CreateCompanyInfo(reader);
                }
            }

            if ((reader != null) && (!reader.IsClosed))
            {
                reader.Close();
            }

            Close(conn);

            return company_info;
        }

        private Company CreateCompany(OdbcDataReader reader)
        {
            Company company = new Company();
            company.CompanyID = (int)reader[0];
            company.CountryID = (int)reader[1];
            company.CompanyName = SafeString(reader[2]);
            company.CompanyVAT = SafeString(reader[3]);
            company.WriteCode = SafeString(reader[4]);
            company.AccountCode = SafeString(reader[5]);
            company.Blocked = Convert.ToBoolean(reader[6]);
            company.HaveCMail = (int)reader[7];

            if (reader[8].ToString() != "")
            {
                company.CompanyType = (int)reader[8];
            }

            company.CompamyInfoCountryID = (int)reader[9];
            company.CompamyInfoVAT = reader[10].ToString();
            company.MobilePhone = reader[11].ToString();

            return company;
        }

        private CompanyInfo CreateCompanyInfo(OdbcDataReader reader)
        {
            CompanyInfo company_info = new CompanyInfo();

            company_info.CompanyID = (int)reader[0];
            company_info.CompanyName = SafeString(reader[1]);
            company_info.CompanyCountryID = (int)reader[2];

            company_info.CompanyVAT = SafeString(reader[3]);
            company_info.ReadCode = SafeString(reader[4]);
            company_info.WriteCode = SafeString(reader[5]);

            company_info.EMail = SafeString(reader[6]);
            company_info.maam = (double)reader[7];
            company_info.FilesSearch = SafeString(reader[8]);

            company_info.DataPath = SafeString(reader[9]);
            company_info.CompanySerialNumber = SafeString(reader[10]);
            company_info.SystemColor = (int)reader[11];
            company_info.MobilePhone = reader[12].ToString();
            company_info.InformMyMobile = Convert.ToBoolean(reader[13]);

            return company_info;
        }

        private ShuratHaklada CreateShuratHaklada(OdbcDataReader reader)
        {
            ShuratHaklada shurat_haklada = new ShuratHaklada();
            shurat_haklada.TransactionGUID = SafeString(reader[0].ToString());
            shurat_haklada.CompanyID = (int)reader[1];
            shurat_haklada.ActionCode = (int)reader[2];

            shurat_haklada.MisparMismach = (int)reader[3];
            shurat_haklada.TarichMismach = SafeDate(reader[4]);
            shurat_haklada.TarichAcher = SafeDate(reader[5]);

            shurat_haklada.ActionDetails = SafeString(reader[6]);
            shurat_haklada.AhuzHaMaam = (double)reader[7];
            shurat_haklada.SchumPaturMaam = (double)reader[8];

            shurat_haklada.SchumMaam = (double)reader[9];
            shurat_haklada.SchumKolelMaam = (double)reader[10];
            shurat_haklada.Attachment = SafeString(reader[11]);

            shurat_haklada.Transfered = Convert.ToBoolean(reader[12].ToString());

            shurat_haklada.CompamyInfoCountryID = (int)reader[13];
            shurat_haklada.CompamyInfoVAT = SafeString(reader[14].ToString());

            shurat_haklada.Readen = Convert.ToBoolean(reader[15].ToString());

            Company company = GetCompany(shurat_haklada.CompanyID.ToString());
            if (company != null)
            {
                shurat_haklada.CompanyVAT = company.CompanyVAT;
                shurat_haklada.CompanyName = company.CompanyName;
                shurat_haklada.WriteCode = company.WriteCode;
            }

            shurat_haklada.DateSent = SafeDate(reader[16].ToString());

            shurat_haklada.LeTkufaMe = SafeDate(reader[17].ToString());
            shurat_haklada.LeTkufaUd = SafeDate(reader[18].ToString());
            shurat_haklada.MisparProyect = SafeString(reader[19].ToString());

            return shurat_haklada;
        }

        private TochenTnua CreateTochenTnua(OdbcDataReader reader)
        {
            TochenTnua tochen_tnua = new TochenTnua();
            tochen_tnua.TransactionGUID = SafeString(reader[0]);

            tochen_tnua.CompanyID = (int)reader[1];
            tochen_tnua.MisparPnimi = (int)reader[2];

            tochen_tnua.MisparShuraBaTnua = (int)reader[3];

            tochen_tnua.CodeParitNumeri = (int)reader[4];
            tochen_tnua.CodeParitAlpha = SafeString(reader[5]);
            tochen_tnua.TeurParitAlpha = SafeString(reader[6]);

            tochen_tnua.Kamut1 = (int)reader[7];
            tochen_tnua.Kamut2 = (int)reader[8];
            tochen_tnua.Kamut3 = (int)reader[9];
            tochen_tnua.Kamut4 = (int)reader[10];
            tochen_tnua.KamutKlalit = (int)reader[11];

            tochen_tnua.TeurYahida = SafeString(reader[12]);
            tochen_tnua.MechirYehida = (double)reader[13];

            tochen_tnua.SchumLefniHanacha = (double)reader[14];
            tochen_tnua.AhuzHanacha = (double)reader[15];
            tochen_tnua.SchumHanacha = (double)reader[16];
            tochen_tnua.SchumShura = (double)reader[17];

            tochen_tnua.AhuzHaMaam = (double)reader[18];
            tochen_tnua.CodeMatbea = (int)reader[19];
            tochen_tnua.SharMatbea = (double)reader[20];

            tochen_tnua.MechirYehidaBeMatbea = (double)reader[21];
            tochen_tnua.SchumShuraMatbeaLefniHanacha = (double)reader[22];
            tochen_tnua.SchumHanachaBeMatbea = (double)reader[23];
            tochen_tnua.SchumShuraMatbeaLacharHanacha = (double)reader[24];

            tochen_tnua.BarCodeParit = SafeString(reader[25]);

            tochen_tnua.Transfered = Convert.ToBoolean(reader[26].ToString());

            tochen_tnua.RawDataNumber = (int)reader[27];
            return tochen_tnua;
        }


        public bool UpdateShuratHaklada(ShuratHaklada shurat_haklada)
        {
            String SQL = "Update Haklada SET ";

            SQL += "CompanyID = '" + shurat_haklada.CompanyID + "',";
            SQL += "ActionCode = '" + shurat_haklada.ActionCode + "',";
            SQL += "MisparMismach = '" + shurat_haklada.MisparMismach + "',";

            SQL += "TarichMismach = '" + shurat_haklada.TarichMismach + "',";
            SQL += "TarichAcher = '" + shurat_haklada.TarichAcher + "',";
            SQL += "ActionDetails = '" + shurat_haklada.ActionDetails + "',";

            SQL += "Maam = '" + shurat_haklada.AhuzHaMaam + "',";
            SQL += "SchumPaturMaam = '" + shurat_haklada.SchumPaturMaam + "',";
            SQL += "SchumMaam = '" + shurat_haklada.SchumMaam + "',";

            SQL += "SchumKolelMaam = '" + shurat_haklada.SchumKolelMaam + "',";
            SQL += "Attachment = '" + shurat_haklada.Attachment + "',";

            SQL += "CompamyInfoCountryID = '" + shurat_haklada.CompamyInfoCountryID + "',";
            SQL += "CompamyInfoVAT = '" + shurat_haklada.CompamyInfoVAT + "',";

            SQL += "Readen = '" + (shurat_haklada.Readen ? 1 : 0) + "',";

            SQL += "LeTkufaMe = '" + shurat_haklada.LeTkufaMe + "',";
            SQL += "LeTkufaUd = '" + shurat_haklada.LeTkufaUd + "',";
            SQL += "MisparProyect = '" + shurat_haklada.MisparProyect + "'";

            SQL += " WHERE TransactionGUID = '" + shurat_haklada.TransactionGUID + "'";

            return SQLExecute(SQL);
        }

        public bool UpdateInboxConfirm(KoteretTnua koteret_tnua)
        {
            String SQL = "Update Inbox SET ";

            SQL += "Confirm0 = '" + (koteret_tnua.Confirm0 ? 1 : 0) + "',";
            SQL += "Confirm1 = '" + (koteret_tnua.Confirm1 ? 1 : 0) + "',";
            SQL += "Confirm2 = '" + (koteret_tnua.Confirm2 ? 1 : 0) + "'";

            SQL += " WHERE TransactionGUID = '" + koteret_tnua.TransactionGUID + "'";

            return SQLExecute(SQL);
        }

        public bool UpdateShuratHakladaInbox(ShuratHaklada shurat_haklada)
        {
            String SQL = "Update HakladaInbox SET ";

            SQL += "CompanyID = '" + shurat_haklada.CompanyID + "',";
            SQL += "ActionCode = '" + shurat_haklada.ActionCode + "',";
            SQL += "MisparMismach = '" + shurat_haklada.MisparMismach + "',";

            SQL += "TarichMismach = '" + shurat_haklada.TarichMismach + "',";
            SQL += "TarichAcher = '" + shurat_haklada.TarichAcher + "',";
            SQL += "ActionDetails = '" + shurat_haklada.ActionDetails + "',";

            SQL += "Maam = '" + shurat_haklada.AhuzHaMaam + "',";
            SQL += "SchumPaturMaam = '" + shurat_haklada.SchumPaturMaam + "',";
            SQL += "SchumMaam = '" + shurat_haklada.SchumMaam + "',";

            SQL += "SchumKolelMaam = '" + shurat_haklada.SchumKolelMaam + "',";
            SQL += "Attachment = '" + shurat_haklada.Attachment + "',";

            SQL += "CompamyInfoCountryID = '" + shurat_haklada.CompamyInfoCountryID + "',";
            SQL += "CompamyInfoVAT = '" + shurat_haklada.CompamyInfoVAT + "', ";

            SQL += "Readen = '" + (shurat_haklada.Readen ? 1 : 0) + "',";

            SQL += "LeTkufaMe = '" + shurat_haklada.LeTkufaMe + "', ";
            SQL += "LeTkufaUd = '" + shurat_haklada.LeTkufaUd + "', ";
            SQL += "MisparProyect = '" + shurat_haklada.MisparProyect + "'";

            SQL += " WHERE TransactionGUID = '" + shurat_haklada.TransactionGUID + "'";

            return SQLExecute(SQL);
        }

        public void TransferedShuratHaklada(String TransactionGUID, bool Transfered)
        {
            String SQL = "Update Haklada SET ";
            SQL += "Transfered = '" + (Transfered ? 1 : 0) + "'";

            if (Transfered)
            {
                SQL += ", DateSent = '" + DateTime.Now + "'";
            }

            SQL += " WHERE TransactionGUID = '" + TransactionGUID + "'";

            SQLExecute(SQL);
        }

        public void TransferedShuratHakladaTochenTnua(String TransactionGUID, bool Transfered)
        {
            String SQL = "Update HakladatTochenTnua SET ";
            SQL += "Transfered = '" + (Transfered ? 1 : 0) + "'";

            if (Transfered)
            {
                SQL += ", DateSent = '" + DateTime.Now + "'";
            }

            SQL += " WHERE TransactionGUID = '" + TransactionGUID + "'";

            SQLExecute(SQL);
        }

        public void TransferedTochenTnuaInbox(String TransactionGUID, bool Transfered)
        {
            String SQL = "Update HakladatInboxTochenTnua SET ";
            SQL += "Transfered = '" + (Transfered ? 1 : 0) + "'";

            if (Transfered)
            {
                SQL += ", DateSent = '" + DateTime.Now + "'";
            }

            SQL += " WHERE TransactionGUID = '" + TransactionGUID + "'";

            SQLExecute(SQL);
        }

        public void TransferedShuratHakladaInbox(String TransactionGUID, bool Transfered)
        {
            String SQL = "Update HakladaInbox SET ";
            SQL += "Transfered = '" + (Transfered ? 1 : 0) + "'";

            if (Transfered)
            {
                SQL += ", DateSent = '" + DateTime.Now + "'";
            }

            SQL += " WHERE TransactionGUID = '" + TransactionGUID + "'";

            SQLExecute(SQL);
        }

        public String SafeString(object obj)
        {
            return (obj != null) ? obj.ToString() : "";
        }

        public DateTime SafeDate(object obj)
        {
            return ((obj != null) && (obj.ToString() != "")) ? Convert.ToDateTime(obj.ToString()) : DateTime.MinValue;
        }

        private string GetDocumentAndSettingsFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop).Substring(0, Environment.GetFolderPath(Environment.SpecialFolder.Desktop).Length - "Desktop".Length);
        }

        public OdbcConnection Open(OdbcConnection conn)
        {
            if (conn == null)
            {
                conn = new OdbcConnection(strConnection);
                conn.Open();
            }
            else if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn;
        }

        public void Close(OdbcConnection conn)
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void Open()
        {
            if (conn == null)
            {
                conn = new OdbcConnection(strConnection);
                conn.Open();
            }
            else if (conn.State == ConnectionState.Closed)
            {
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
    }
}