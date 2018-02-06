using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Pulsar.Classes;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Pulsar
{
    public static class SendOutboxData
    {
        public static CompanyInfo Company_Info { get; set; }
        public static Parser parser { get; set; }
        public static DBLayer dblayer { get; set; }

        public static void Send(CompanyInfo _company_info, Parser _parser, DBLayer _dblayer)
        {
            Company_Info = _company_info;
            parser = _parser;
            dblayer = _dblayer;

            OutboxFromHaklada();
            ApplyTransaction(_company_info, _parser, _dblayer);
        }

        public static void SendRequest(CompanyInfo _company_info, Company _company, Parser _parser, DBLayer _dblayer, string _message)
        {
            Company_Info = _company_info;
            parser = _parser;
            dblayer = _dblayer;

            OutboxRequest(_company_info, _company, _message);
            ApplyRequestTransaction(_company_info, _parser, _dblayer);
        }

        public static void SendAnswer(CompanyInfo _company_info, Company _company, Parser _parser, DBLayer _dblayer, string _answer)
        {
            Company_Info = _company_info;
            parser = _parser;
            dblayer = _dblayer;

            String TransactionGUID = OutboxAnswer(_company_info, _company, _answer);
            ApplyRequestTransaction(_company_info, _parser, _dblayer);
            dblayer.DeleteOutbox(TransactionGUID);
        }

        private static void ApplyTransaction(CompanyInfo _company_info, Parser _parser, DBLayer _dblayer)
        {
            String SQL = "SELECT * from Outbox";
            ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

            foreach (ArrayList record in all_records)
            {
                CompanyInfo Company_Info_To = dblayer.GetCompanyInfo(record[1].ToString(), record[2].ToString());
                Company Company_To = dblayer.GetCompany(record[3].ToString(), record[4].ToString());
                //parser.AddData(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, record[3].ToString(), record[4].ToString(), record[9].ToString(), record[5].ToString());
                //parser.AddData(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, Company_To.CountryID.ToString(), Company_To.CompanyVAT, Company_To.WriteCode, record[5].ToString(), Company_Info.CompanySerialNumber);
                String FileName = record[12].ToString();
                if (File.Exists(FileName))
                    parser.AddData(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, Company_To.CountryID.ToString(), Company_To.CompanyVAT, Company_To.WriteCode, record[5].ToString(), Company_Info.CompanySerialNumber, FileName);
                else
                    parser.AddData(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, Company_To.CountryID.ToString(), Company_To.CompanyVAT, Company_To.WriteCode, record[5].ToString(), Company_Info.CompanySerialNumber);
                //if (File.Exists(FileName))
                //{
                //    UploadFileFTP(FileName, record[0].ToString(), record[3].ToString(), record[4].ToString());
                //    parser.TransFTPToDB(Company_Info, FileName);
                //}
            }
        }

        private static void ApplyRequestTransaction(CompanyInfo _company_info, Parser _parser, DBLayer _dblayer)
        {
            String SQL = "SELECT * from Outbox";
            ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

            foreach (ArrayList record in all_records)
            {
                //CompanyInfo Company_Info_To = dblayer.GetCompanyInfo(record[3].ToString(), record[4].ToString());
                Company Company_To = dblayer.GetCompany(record[3].ToString(), record[4].ToString());
                //parser.AddData(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, record[3].ToString(), record[4].ToString(), record[9].ToString(), record[5].ToString());
                String FileName = record[12].ToString();
                if (File.Exists(FileName))
                    parser.AddRequest(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, Company_To.CountryID.ToString(), Company_To.CompanyVAT, record[5].ToString(), Company_Info.CompanySerialNumber, FileName);
                else
                    parser.AddRequest(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, Company_To.CountryID.ToString(), Company_To.CompanyVAT, record[5].ToString(), Company_Info.CompanySerialNumber);
                
                //if (File.Exists(FileName))
                //{
                //    UploadFileFTP(FileName, record[0].ToString(), record[3].ToString(), record[4].ToString());
                //    parser.TransFTPToDB(Company_Info, FileName);
                //}
            }

            dblayer.CleanOutboxTable();
        }


        public static void Get(CompanyInfo _company_info, Parser _parser, DBLayer _dblayer)
        {
            Company_Info = _company_info;
            parser = _parser;
            dblayer = _dblayer;

            InboxFromHaklada();

            String SQL = "SELECT * from Outbox";
            ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

            //foreach (ArrayList record in all_records)
            //{
            //    CompanyInfo Company_Info_To = dblayer.GetCompanyInfo(record[3].ToString(), record[4].ToString());
            //    //parser.AddData(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, record[3].ToString(), record[4].ToString(), record[9].ToString(), record[5].ToString());
            //    parser.AddData(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, Company_Info_To.CompanyCountryID.ToString(), Company_Info_To.CompanyVAT, Company_Info_To.WriteCode, record[5].ToString());
            //    if (File.Exists(record[12].ToString()))
            //    {
            //        UploadFileFTP(record[12].ToString(), record[0].ToString(), record[3].ToString(), record[4].ToString());
            //    }
            //}
        }

        private static void UploadFileFTP(string filename, String TransactionGuid, string countryIDTo, string companyVatTo)
        {
            String FileExt = Path.GetExtension(filename);
            String FileName = Path.GetFileName(filename);
            String FilePath = Path.GetDirectoryName(filename);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.FileName = Application.StartupPath + @"\FTPTrans.exe";
            //TODO: adirim.info
            startInfo.Arguments = "\"/ftpServerIP:10.9.10.250\" \"/ftpUserID:adi\" \"/ftpPassword:9363\" \"/filename:" + FileName + "\" \"/cmd:upload\" \"/path:" + FilePath + "/\" \"/targetpath:GlobalInfoTransfer/\" \"/targetfilename:" + TransactionGuid + FileExt + "\" \"/countryid:" + countryIDTo + "\" \"/companyvat:" + companyVatTo + "\"";
            Process.Start(startInfo);
        }

        private static void OutboxFromHaklada()
        {
            String SQL = "SELECT * from Haklada WHERE Transfered = 0";
            ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

            foreach (ArrayList record in all_records)
            {
                Company company = dblayer.GetCompany(record[1].ToString());

                if ((company.WriteCode != null) && (company.WriteCode.Trim() != ""))
                {
                    KoteretTnua koteret_tnua = new KoteretTnua();
                    koteret_tnua.TransactionGUID = record[0].ToString();
                    koteret_tnua.CountryIDFrom = Company_Info.CompanyCountryID;
                    koteret_tnua.CountryIDTo = company.CountryID;
                    koteret_tnua.VatTo = company.CompanyVAT;
                    koteret_tnua.VatFrom = Company_Info.CompanyVAT;

                    CompanyInfo company_info = dblayer.GetCompanyInfo(record[13].ToString(), record[14].ToString());
                    //KT|011|021231|0301/01/2012|0401/02/2012|0503/01/2012|0604/01/2012|07ClientName|08123456789|09bla bla|10100.20|1110.5|1216.5|13116.5|141|15CompanyFrom|16OsekMoorshehHaSholeah|17CountryIDHaSholeah

                    //01: kt.MisparPnimi           //מספר פנימי         -
                    //02: kt.MisparMismach         //מספר מסמך          *
                    //03: kt.TarichMismach         //תאריך מסמך         *
                    //04: kt.TarichKovea_Divuch    //תאריך קובע/דיווח  -
                    //05: kt.TarichMishloah        //תאריך משלוח        *
                    //06: kt.TarichAher            //תאריך אחר          *
                    //07: kt.ShemHaLakoh           //שם הלקוח           *
                    //08: kt.OsekMoorshehLakoh     //עוסק מורשה לקוח   *
                    //09: kt.MeidaNosaf            //מידע נוסף          *
                    //10: kt.SchumLifneMaam        //סכום לפני מע"מ     *
                    //11: kt.SchumPaturMeMaam      //סכום פטור ממע"מ    *
                    //12: kt.SchumHaMaam           //סכום המע"מ         *
                    //13: kt.SchumKolelMaam        //סכום כולל מע"מ     *
                    //14: kt.SugTnua               //סוג תנועה           *                
                    //15: kt.ShemHaSholeah         //שם השולח            *
                    //16: kt.OsekMoorshehHaSholeah //עוסק מורשה השולח   *
                    //17: kt.CountryIDHaSholeah    //קוד מדינת השולח    *
                    //18: kt.CountryIDHaSholeah    //אחוז המע"מ          *

                    String kt = "KT|";
                    kt += cmd("02", record[3].ToString());
                    kt += cmd("03", record[4].ToString());
                    kt += cmd("05", DateTime.Now.ToShortDateString());
                    kt += cmd("06", record[5].ToString());
                    kt += cmd("07", company.CompanyName);
                    kt += cmd("08", company.CompanyVAT);
                    kt += cmd("09", record[6].ToString());
                    kt += cmd("10", (double.Parse(record[10].ToString()) - double.Parse(record[10].ToString())).ToString()); //txtLefniMaam
                    kt += cmd("11", record[8].ToString());
                    kt += cmd("12", record[9].ToString());
                    kt += cmd("13", record[10].ToString());
                    kt += cmd("14", record[2].ToString());
                    kt += cmd("15", company_info.CompanyName); //koteret_tnua.ShemHaSholeah
                    kt += cmd("16", company_info.CompanyVAT); //koteret_tnua.OsekMoorshehHaSholeah //record[14].ToString()
                    kt += cmd("17", company_info.CompanyCountryID.ToString()); //koteret_tnua.CountryIDHaSholeah //record[13].ToString()
                    kt += cmd("18", record[7].ToString()); //koteret_tnua.CountryIDHaSholeah //record[13].ToString()

                    koteret_tnua.Data = kt.Substring(0, kt.Length - 1);
                    koteret_tnua.Data = koteret_tnua.Data.Replace("\"", "\"\"");
                    koteret_tnua.Data = koteret_tnua.Data.Replace("'", "''");
                    dblayer.Current_Company_Info = Company_Info;
                    dblayer.AddCreateRecord(koteret_tnua, company.WriteCode, record[11].ToString());
                    dblayer.TransferedShuratHaklada(record[0].ToString(), true);
                }
            }
        }

        private static void InboxFromHaklada()
        {
            String SQL = "SELECT * from HakladaInbox WHERE Transfered = 0";
            ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

            foreach (ArrayList record in all_records)
            {
                Company company = dblayer.GetCompany(record[1].ToString());
                CompanyInfo company_info = dblayer.GetCompanyInfo(record[13].ToString(), record[14].ToString());

                KoteretTnua koteret_tnua = new KoteretTnua();
                koteret_tnua.TransactionGUID = record[0].ToString();
                //koteret_tnua.CountryIDFrom = Company_Info.CompanyCountryID;
                //koteret_tnua.CountryIDTo = company.CountryID;
                //koteret_tnua.VatTo = company.CompanyVAT;
                //koteret_tnua.VatFrom = Company_Info.CompanyVAT;

                koteret_tnua.CountryIDFrom = company.CountryID;
                koteret_tnua.CountryIDTo = Company_Info.CompanyCountryID;
                koteret_tnua.VatTo = Company_Info.CompanyVAT;
                koteret_tnua.VatFrom = company.CompanyVAT;

                //CompanyInfo company_info = dblayer.GetCompanyInfo(record[13].ToString(), record[14].ToString());

                //KT|011|021231|0301/01/2012|0401/02/2012|0503/01/2012|0604/01/2012|07ClientName|08123456789|09bla bla|10100.20|1110.5|1216.5|13116.5|141|15CompanyFrom|16OsekMoorshehHaSholeah|17CountryIDHaSholeah

                //01: kt.MisparPnimi           //מספר פנימי         -
                //02: kt.MisparMismach         //מספר מסמך          *
                //03: kt.TarichMismach         //תאריך מסמך         *
                //04: kt.TarichKovea_Divuch    //תאריך קובע/דיווח  -
                //05: kt.TarichMishloah        //תאריך משלוח        *
                //06: kt.TarichAher            //תאריך אחר          *
                //07: kt.ShemHaLakoh           //שם הלקוח           *
                //08: kt.OsekMoorshehLakoh     //עוסק מורשה לקוח   *
                //09: kt.MeidaNosaf            //מידע נוסף          *
                //10: kt.SchumLifneMaam        //סכום לפני מע"מ     *
                //11: kt.SchumPaturMeMaam      //סכום פטור ממע"מ    *
                //12: kt.SchumHaMaam           //סכום המע"מ         *
                //13: kt.SchumKolelMaam        //סכום כולל מע"מ     *
                //14: kt.SugTnua               //סוג תנועה           *                
                //15: kt.ShemHaSholeah         //שם השולח            *
                //16: kt.OsekMoorshehHaSholeah //עוסק מורשה השולח   *
                //17: kt.CountryIDHaSholeah    //קוד מדינת השולח    *
                //18: kt.CountryIDHaSholeah    //אחוז המע"מ          *

                String kt = "KT|";
                kt += cmd("02", record[3].ToString());
                kt += cmd("03", record[4].ToString());
                kt += cmd("05", DateTime.Now.ToShortDateString());
                kt += cmd("06", record[5].ToString());
                kt += cmd("07", company_info.CompanyName); //company.CompanyName
                kt += cmd("08", company_info.CompanyVAT); //company.CompanyVAT
                kt += cmd("09", record[6].ToString());
                kt += cmd("10", (double.Parse(record[10].ToString()) - double.Parse(record[10].ToString())).ToString()); //txtLefniMaam
                kt += cmd("11", record[8].ToString());
                kt += cmd("12", record[9].ToString());
                kt += cmd("13", record[10].ToString());
                kt += cmd("14", record[2].ToString());
                kt += cmd("15", company.CompanyName); //koteret_tnua.ShemHaSholeah //company_info.CompanyName
                kt += cmd("16", company.CompanyVAT); //koteret_tnua.OsekMoorshehHaSholeah //record[14].ToString() //company_info.CompanyVAT
                kt += cmd("17", company.CountryID.ToString()); //koteret_tnua.CountryIDHaSholeah //record[13].ToString() //company_info.CompanyCountryID.ToString()
                kt += cmd("18", record[7].ToString()); //koteret_tnua.CountryIDHaSholeah //record[13].ToString()

                koteret_tnua.Data = kt.Substring(0, kt.Length - 1);
                dblayer.Current_Company_Info = Company_Info;
                dblayer.AddCreateInboxRecord(koteret_tnua, company.WriteCode, record[11].ToString());
                dblayer.TransferedShuratHakladaInbox(record[0].ToString(), true);
                if ((record[11].ToString() != null) && (record[11].ToString().Trim()) != "")
                {
                    MoveAttachment(koteret_tnua.TransactionGUID, record[11].ToString());
                }
            }
        }
        
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        private static void OutboxFromHakladatTochenTnua()
        {
            String SQL = "SELECT * from HakladatTochenTnua WHERE Transfered = 0";
            ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

            foreach (ArrayList record in all_records)
            {
                Company company = dblayer.GetCompany(record[1].ToString());

                if ((company.WriteCode != null) && (company.WriteCode.Trim() != ""))
                {
                    TochenTnua tochen_tnua = new TochenTnua();
                    tochen_tnua.TransactionGUID = record[0].ToString();
                    tochen_tnua.CountryIDFrom = Company_Info.CompanyCountryID;
                    tochen_tnua.CountryIDTo = company.CountryID;
                    tochen_tnua.VatTo = company.CompanyVAT;
                    tochen_tnua.VatFrom = Company_Info.CompanyVAT;
                    
                    //TT|011|021231|0301/01/2012|0401/02/2012|0503/01/2012|0604/01/2012|07ClientName|08123456789|09bla bla|10100.20|1110.5|1216.5|13116.5|141|15CompanyFrom|16OsekMoorshehHaSholeah|17CountryIDHaSholeah
                    
                    //מספר תנועה חיצוני	// מספר פנימי
                    //01: kt.MisparPnimi           //מספר פנימי         -   

                    //22: מספר שורה בתנועה
                    //23: קוד פריט נומרי חיצוני
                    //24: קוד פריט אלפה
                    //25: תאור פריט אלפה	
                    //26: כמות 1	
                    //27: כמות 2	
                    //28: כמות 3	
                    //28: כמות 4	
                    //29: כמות כללית	
                    //30: תאור יחידה	
                    //31: מחיר יחידה	
                    //32: סכום לפני הנחה	
                    //33: אחוז הנחה	
                    //34: סכום הנחה	
                    //35: סכום לאחר הנחה	
                    //36: אחוז המע"מ	
                    //37: קוד מטבע	
                    //38: שער מטבע	
                    //39: מחיר יחידה במטבע	
                    //40: סכום שורה מטבע לפני הנחה	
                    //41: סכום הנחה במטבע	
                    //42: סכום שורה במטבע לאחר הנחה	
                    //43: בר קוד פריט

                    String tt = "TT|";
                    tt += cmd("01", record[2].ToString());
                    tt += cmd("22", record[3].ToString());
                    tt += cmd("23", record[4].ToString());
                    tt += cmd("24", record[5].ToString());
                    tt += cmd("25", record[5].ToString());
                    tt += cmd("26", record[6].ToString());
                    tt += cmd("27", record[7].ToString());
                    tt += cmd("28", record[8].ToString());
                    tt += cmd("29", record[9].ToString());
                    tt += cmd("30", record[10].ToString());
                    tt += cmd("31", record[11].ToString());
                    tt += cmd("32", record[12].ToString());
                    tt += cmd("33", record[13].ToString());
                    tt += cmd("34", record[14].ToString());
                    tt += cmd("35", record[15].ToString());
                    tt += cmd("36", record[16].ToString()); 
                    tt += cmd("37", record[17].ToString());
                    tt += cmd("38", record[18].ToString());
                    tt += cmd("39", record[19].ToString());
                    tt += cmd("40", record[20].ToString());
                    tt += cmd("41", record[21].ToString());
                    tt += cmd("42", record[22].ToString());
                    tt += cmd("43", record[23].ToString());

                    tochen_tnua.Data = tt.Substring(0, tt.Length - 1);
                    dblayer.Current_Company_Info = Company_Info;
                    // TODO: == > 
                    dblayer.AddCreateRecord(tochen_tnua, company.WriteCode, record[11].ToString());
                    dblayer.TransferedShuratHaklada(record[0].ToString(), true);
                }
            }
        }

        private static void InboxFromHakladatTochenTnua()
        {
            String SQL = "SELECT * from HakladatTochenTnuaInbox WHERE Transfered = 0";
            ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

            foreach (ArrayList record in all_records)
            {
                Company company = dblayer.GetCompany(record[1].ToString());
                CompanyInfo company_info = dblayer.GetCompanyInfo(record[13].ToString(), record[14].ToString());

                TochenTnua tochen_tnua = new TochenTnua();
                tochen_tnua.TransactionGUID = record[0].ToString();
                tochen_tnua.CountryIDFrom = Company_Info.CompanyCountryID;
                tochen_tnua.CountryIDTo = company.CountryID;
                tochen_tnua.VatTo = company.CompanyVAT;
                tochen_tnua.VatFrom = Company_Info.CompanyVAT;

                //TT|011|021231|0301/01/2012|0401/02/2012|0503/01/2012|0604/01/2012|07ClientName|08123456789|09bla bla|10100.20|1110.5|1216.5|13116.5|141|15CompanyFrom|16OsekMoorshehHaSholeah|17CountryIDHaSholeah

                //מספר תנועה חיצוני	// מספר פנימי
                //01: kt.MisparPnimi           //מספר פנימי         -   

                //22: מספר שורה בתנועה
                //23: קוד פריט נומרי חיצוני
                //24: קוד פריט אלפה
                //25: תאור פריט אלפה	
                //26: כמות 1	
                //27: כמות 2	
                //28: כמות 3	
                //28: כמות 4	
                //29: כמות כללית	
                //30: תאור יחידה	
                //31: מחיר יחידה	
                //32: סכום לפני הנחה	
                //33: אחוז הנחה	
                //34: סכום הנחה	
                //35: סכום לאחר הנחה	
                //36: אחוז המע"מ	
                //37: קוד מטבע	
                //38: שער מטבע	
                //39: מחיר יחידה במטבע	
                //40: סכום שורה מטבע לפני הנחה	
                //41: סכום הנחה במטבע	
                //42: סכום שורה במטבע לאחר הנחה	
                //43: בר קוד פריט

                String tt = "TT|";
                tt += cmd("01", record[2].ToString());
                tt += cmd("22", record[3].ToString());
                tt += cmd("23", record[4].ToString());
                tt += cmd("24", record[5].ToString());
                tt += cmd("25", record[5].ToString());
                tt += cmd("26", record[6].ToString());
                tt += cmd("27", record[7].ToString());
                tt += cmd("28", record[8].ToString());
                tt += cmd("29", record[9].ToString());
                tt += cmd("30", record[10].ToString());
                tt += cmd("31", record[11].ToString());
                tt += cmd("32", record[12].ToString());
                tt += cmd("33", record[13].ToString());
                tt += cmd("34", record[14].ToString());
                tt += cmd("35", record[15].ToString());
                tt += cmd("36", record[16].ToString());
                tt += cmd("37", record[17].ToString());
                tt += cmd("38", record[18].ToString());
                tt += cmd("39", record[19].ToString());
                tt += cmd("40", record[20].ToString());
                tt += cmd("41", record[21].ToString());
                tt += cmd("42", record[22].ToString());
                tt += cmd("43", record[23].ToString());

                tochen_tnua.Data = tt.Substring(0, tt.Length - 1);                
                dblayer.Current_Company_Info = Company_Info;

                dblayer.AddCreateInboxRecord(tochen_tnua, company.WriteCode, record[11].ToString());
                dblayer.TransferedShuratHakladaInbox(record[0].ToString(), true);
            }
        }
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////


        private static void MoveAttachment(String TransactionGUID, String Attachmnent)
        {
            string ServerPath = Application.StartupPath + @"\TransientStorage";

            if (!Directory.Exists(ServerPath))
            {
                Directory.CreateDirectory(ServerPath);
            }

            if (Company_Info.CompanyCountryID != 0)
            {
                if (!Directory.Exists(ServerPath + "/" + Company_Info.CompanyCountryID))
                {
                    Directory.CreateDirectory(ServerPath + "/" + Company_Info.CompanyCountryID);
                }
            }

            if (Company_Info.CompanyVAT != null)
            {
                if (!Directory.Exists(ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT))
                {
                    Directory.CreateDirectory(ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT);
                }
            }

            String fileName = TransactionGUID + Path.GetExtension(Attachmnent);
            File.Move(Attachmnent, ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT + "/" + fileName);
        }

        private static String cmd(String command_number, String txt)
        {
            return command_number + txt + "|";
        }

        private static String cmd(String command_number, ComboBox cmbbox)
        {
            return command_number + cmbbox.Text + "|";
        }

        private static String cmd(String command_number, TextBox txtbox)
        {
            return command_number + txtbox.Text + "|";
        }

        private static String cmd(String command_number, DateTimePicker dtpicker)
        {
            return command_number + dtpicker.Value.ToShortDateString() + "|";
        }

        private static void OutboxRequest(CompanyInfo _company_info, Company _company, string _message)
        {
            //Company company = dblayer.GetCompany(record[1].ToString());
            String TransactionGUID = "{" + Guid.NewGuid().ToString() + "}";
            WriteCodeRequest write_code_request = new WriteCodeRequest();
            write_code_request.TransactionGUID = TransactionGUID;
            write_code_request.CountryIDFrom = Company_Info.CompanyCountryID;
            write_code_request.CountryIDTo = _company.CountryID;
            write_code_request.VatTo = _company.CompanyVAT;
            write_code_request.VatFrom = Company_Info.CompanyVAT;

            //CompanyInfo company_info = dblayer.GetCompanyInfo(record[13].ToString(), record[14].ToString());

            String kt = "WR|";
            kt += cmd("07", _company.CompanyName);
            kt += cmd("08", _company.CompanyVAT);
            kt += cmd("15", _company_info.CompanyName);
            kt += cmd("16", _company_info.CompanyVAT);
            kt += cmd("17", _company_info.CompanyCountryID.ToString());
            kt += cmd("99", _message);

            write_code_request.Data = kt.Substring(0, kt.Length - 1);
            dblayer.Current_Company_Info = Company_Info;
            dblayer.AddCreateRecord(write_code_request, _company.WriteCode, "" /*record[11].ToString()*/);
            dblayer.TransferedShuratHaklada(TransactionGUID, true);
            //if ((record[11].ToString() != null) && (record[11].ToString().Trim()) != "")
            //{
            //    MoveAttachment(write_code_request.TransactionGUID, record[11].ToString());
            //}
        }

        private static String OutboxAnswer(CompanyInfo _company_info, Company _company, string _answer)
        {
            //Company company = dblayer.GetCompany(record[1].ToString());
            String TransactionGUID = "{" + Guid.NewGuid().ToString() + "}";
            WriteCodeRequest write_code_request = new WriteCodeRequest();
            write_code_request.TransactionGUID = TransactionGUID;
            write_code_request.CountryIDFrom = Company_Info.CompanyCountryID;
            write_code_request.CountryIDTo = _company.CountryID;
            write_code_request.VatTo = _company.CompanyVAT;
            write_code_request.VatFrom = Company_Info.CompanyVAT;

            //CompanyInfo company_info = dblayer.GetCompanyInfo(record[13].ToString(), record[14].ToString());

            String kt = "WR|";
            kt += cmd("07", _company.CompanyName);
            kt += cmd("08", _company.CompanyVAT);
            kt += cmd("15", _company_info.CompanyName);
            kt += cmd("16", _company_info.CompanyVAT);
            kt += cmd("17", _company_info.CompanyCountryID.ToString());
            kt += cmd("98", _answer);

            write_code_request.Data = kt.Substring(0, kt.Length - 1);
            dblayer.Current_Company_Info = Company_Info;
            dblayer.AddCreateRecord(write_code_request, _company.WriteCode, "" /*record[11].ToString()*/);
            dblayer.TransferedShuratHaklada(TransactionGUID, true);
            //if ((record[11].ToString() != null) && (record[11].ToString().Trim()) != "")
            //{
            //    MoveAttachment(write_code_request.TransactionGUID, record[11].ToString());
            //}

            return TransactionGUID;
        }
    }
}



//public SendOutboxData(CompanyInfo _company_info, Parser _parser, DBLayer _dblayer)
//{
//    Company_Info = _company_info;
//    parser = _parser;
//    dblayer = _dblayer;

//    OutboxFromHaklada();

//    String SQL = "SELECT * from Outbox";
//    ArrayList all_records = dblayer.SQLQueryArrayList(SQL);

//    foreach (ArrayList record in all_records)
//    {
//        parser.AddData(record[0].ToString(), Company_Info.CompanyCountryID.ToString(), Company_Info.CompanyVAT, record[3].ToString(), record[4].ToString(), record[9].ToString(), record[5].ToString());
//        if (File.Exists(record[12].ToString()))
//        {
//            UploadFileFTP(record[12].ToString(), record[0].ToString(), record[3].ToString(), record[4].ToString());
//        }
//    }       
//}