using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using Pulsar.Classes;
using System.Windows.Forms;
using Pulsar.Controls;

namespace Pulsar
{
    public class ExternalDataManager
    {
        public CompanyInfo Company_Info { get; set; }
        public DBLayer dblayer { get; set; }

        private String _importEncoding = "default";

        public String ImportEncoding
        {
            get
            {
                CheckEncoder();
                return _importEncoding;
            }
            set
            {
                _importEncoding = value;
                CheckEncoder();
            }
        }

        private Encoding CheckEncoder()
        {
            if (_importEncoding == "default")
            {
                CurrentImportEncoding = Encoding.Default;
            }

            if (_importEncoding == "utf8")
            {
                CurrentImportEncoding = Encoding.UTF8;
            }

            if (_importEncoding == "unicode")
            {
                CurrentImportEncoding = Encoding.Unicode;
            }

            if (_importEncoding == "iso-8859-8")
            {
                CurrentImportEncoding = Encoding.GetEncoding("iso-8859-8");
            }

            if (_importEncoding == "iso-8859-8-i")
            {
                CurrentImportEncoding = Encoding.GetEncoding("iso-8859-8-i");
            }

            if (_importEncoding == "1255")
            {
                CurrentImportEncoding = Encoding.GetEncoding(1255);
            }

            if (_importEncoding == "DOS-862")
            {
                CurrentImportEncoding = Encoding.GetEncoding("DOS-862");
            }

            return CurrentImportEncoding;
        }

        private Encoding CurrentImportEncoding { get; set; }

        public String IniFileName { get; set; }
        public IniFile iniFile { get; set; }

        private void PrepareIniFileName()
        {
            IniFileName = Path.GetFileName(Application.ExecutablePath);

            FileInfo fi = new FileInfo(IniFileName);
            fi = new FileInfo(fi.FullName);
            IniFileName = fi.Name;

            IniFileName = IniFileName.Substring(0, IniFileName.Length - ".exe".Length) + ".ini";
            iniFile = new IniFile(Application.StartupPath + @"\" + IniFileName);
        }

        public void ImportHakladaMokup(String filename, CompanyInfo _companyinfo, DBLayer _dblayer)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;
            FiledHolder filedHolder = new FiledHolder();
            filedHolder.InitINI();
            filedHolder.Company_Info = Company_Info;
            filedHolder.Add("CountryID");
            filedHolder.Add("CompanyVAT");
            filedHolder.Add("CompanyName");
            filedHolder.Add("ActionCode");
            filedHolder.Add("MisparMismach");
            filedHolder.Add("TarichMismach");
            filedHolder.Add("TarichAcher");
            filedHolder.Add("ActionDetails");
            filedHolder.Add("Maam");
            filedHolder.Add("SchumPaturMaam");
            filedHolder.Add("SchumMaam");
            filedHolder.Add("SchumKolelMaam");
            filedHolder.Add("Attachment");
            filedHolder.LoadData();

            StreamReader sr = new StreamReader(filename, CheckEncoder());   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic
            String line = "";
            ArrayList flds = filedHolder.GetFileds();

            //filedHolder.GetFiled("ActionCode")
            dblayer.Current_Company_Info = _companyinfo;

            //bool bSkipRow = false;

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                filedHolder.CurrentDataLine = line;

                ShuratHaklada shurat_haklada = new ShuratHaklada();

                String CompanyVAT = filedHolder.GetFiledValue("CompanyVAT");
                String CompanyName = filedHolder.GetFiledValue("CompanyName");

                Company company = CompanyIdentification.Identify(dblayer, Company_Info, null, CompanyVAT, CompanyName, null);

                if (company != null)
                {
                    shurat_haklada.CompanyID = company.CompanyID;

                    shurat_haklada.ActionCode = Convert.ToInt32(filedHolder.GetFiledValue("ActionCode"));
                    shurat_haklada.MisparMismach = Convert.ToInt32(filedHolder.GetFiledValue("MisparMismach"));
                    shurat_haklada.TarichMismach = Convert.ToDateTime(filedHolder.GetFiledValue("TarichMismach"));
                    shurat_haklada.TarichAcher = Convert.ToDateTime(filedHolder.GetFiledValue("TarichAcher"));
                    shurat_haklada.ActionDetails = filedHolder.GetFiledValue("ActionDetails");
                    shurat_haklada.AhuzHaMaam = Convert.ToDouble(filedHolder.GetFiledValue("Maam"));
                    shurat_haklada.SchumPaturMaam = Convert.ToDouble(filedHolder.GetFiledValue("SchumPaturMaam"));
                    shurat_haklada.SchumMaam = Convert.ToDouble(filedHolder.GetFiledValue("SchumMaam"));
                    shurat_haklada.SchumKolelMaam = Convert.ToDouble(filedHolder.GetFiledValue("SchumKolelMaam"));
                    shurat_haklada.Attachment = filedHolder.GetFiledValue("Attachment");
                    shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID;
                    shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT;
                    dblayer.AddHakladaRecord(shurat_haklada);
                }
            }

            sr.Close();
        }

        public void ImportHakladaMokupINI(String filename, CompanyInfo _companyinfo, DBLayer _dblayer, string delimiter)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;

            String line = "";
            StreamReader sr = new StreamReader(filename, CurrentImportEncoding);   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                dblayer.Current_Company_Info = _companyinfo;
                //dblayer.AddCompany(CreateCompanyDelimter(line, delimiter));
                ShuratHaklada shurat_haklada = CreateShuratHakladaMokupINI(line, delimiter);

                if (shurat_haklada.Attachment == "")
                {
                    if (Company_Info.FilesSearch != null)
                    {
                        try
                        {
                            string[] files = Directory.GetFiles(Company_Info.FilesSearch, "*" + shurat_haklada.MisparMismach + "*.*");
                            if (files.Length == 1)
                            {
                                shurat_haklada.Attachment = files[0].ToString();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                String TransactionGUID = dblayer.AddHakladaRecord(shurat_haklada);
            }

            sr.Close();
        }

        //private void MoveAttachment(String TransactionGUID, String Attachmnent)
        //{
        //    string ServerPath = Application.StartupPath + @"\TransientStorage";

        //    if (!Directory.Exists(ServerPath))
        //    {
        //        Directory.CreateDirectory(ServerPath);
        //    }

        //    if (Company_Info.CompanyCountryID != 0)
        //    {
        //        if (!Directory.Exists(ServerPath + "/" + Company_Info.CompanyCountryID))
        //        {
        //            Directory.CreateDirectory(ServerPath + "/" + Company_Info.CompanyCountryID);
        //        }
        //    }

        //    if (Company_Info.CompanyVAT != null)
        //    {
        //        if (!Directory.Exists(ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT))
        //        {
        //            Directory.CreateDirectory(ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT);
        //        }
        //    }

        //    String fileName = TransactionGUID + Path.GetExtension(Attachmnent);
        //    File.Move(Attachmnent, ServerPath + "/" + Company_Info.CompanyCountryID + "/" + Company_Info.CompanyVAT + "/" + fileName);
        //}

        public void ImportHakladaMokup(String filename, CompanyInfo _companyinfo, DBLayer _dblayer, char delimiter)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;
            FiledHolder filedHolder = new FiledHolder();
            filedHolder.InitINI();
            filedHolder.Company_Info = Company_Info;
            filedHolder.Add("CountryID");
            filedHolder.Add("CompanyVAT");
            filedHolder.Add("CompanyName");
            filedHolder.Add("ActionCode");
            filedHolder.Add("MisparMismach");
            filedHolder.Add("TarichMismach");
            filedHolder.Add("TarichAcher");
            filedHolder.Add("ActionDetails");
            filedHolder.Add("Maam");
            filedHolder.Add("SchumPaturMaam");
            filedHolder.Add("SchumMaam");
            filedHolder.Add("SchumKolelMaam");
            filedHolder.Add("Attachment");
            filedHolder.LoadData();

            StreamReader sr = new StreamReader(filename, CheckEncoder());   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic
            String line = "";
            ArrayList flds = filedHolder.GetFileds();

            //filedHolder.GetFiled("ActionCode")
            dblayer.Current_Company_Info = _companyinfo;

            //bool bSkipRow = false;

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                filedHolder.CurrentDataLine = line;

                ShuratHaklada shurat_haklada = new ShuratHaklada();

                String CompanyVAT = filedHolder.GetFiledValue(filedHolder.GetFiledPosition("CompanyVAT"), delimiter);
                String CompanyName = filedHolder.GetFiledValue(filedHolder.GetFiledOrder("CompanyName"), delimiter);

                Company company = CompanyIdentification.Identify(dblayer, Company_Info, null, CompanyVAT, CompanyName, null);

                if (company != null)
                {
                    shurat_haklada.CompanyID = company.CompanyID;

                    shurat_haklada.ActionCode = Convert.ToInt32(filedHolder.GetFiledValue("ActionCode"));
                    shurat_haklada.MisparMismach = Convert.ToInt32(filedHolder.GetFiledValue("MisparMismach"));
                    shurat_haklada.TarichMismach = Convert.ToDateTime(filedHolder.GetFiledValue("TarichMismach"));
                    shurat_haklada.TarichAcher = Convert.ToDateTime(filedHolder.GetFiledValue("TarichAcher"));
                    shurat_haklada.ActionDetails = filedHolder.GetFiledValue("ActionDetails");
                    shurat_haklada.AhuzHaMaam = Convert.ToDouble(filedHolder.GetFiledValue("Maam"));
                    shurat_haklada.SchumPaturMaam = Convert.ToDouble(filedHolder.GetFiledValue("SchumPaturMaam"));
                    shurat_haklada.SchumMaam = Convert.ToDouble(filedHolder.GetFiledValue("SchumMaam"));
                    shurat_haklada.SchumKolelMaam = Convert.ToDouble(filedHolder.GetFiledValue("SchumKolelMaam"));
                    shurat_haklada.Attachment = filedHolder.GetFiledValue("Attachment");
                    shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID;
                    shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT;
                    dblayer.AddHakladaRecord(shurat_haklada);
                }
            }

            sr.Close();
        }

        private String GetFiledValue(String line, FiledHolder filedHolder, String name)
        {
            return line.Substring(filedHolder.GetFiled(name).Pos, filedHolder.GetFiled(name).Length);
        }

        public void ImportHakladaMokup(String filename, CompanyInfo _companyinfo, DBLayer _dblayer, string delimiter)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;

            String line = "";
            StreamReader sr = new StreamReader(filename, CheckEncoder());   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                dblayer.Current_Company_Info = _companyinfo;
                dblayer.AddHakladaRecord(CreateShuratHakladaDelimter(line, delimiter));
            }

            sr.Close();
        }

        public void ImportHaklada(String filename, CompanyInfo _companyinfo, DBLayer _dblayer)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;

            String line = "";
            StreamReader sr = new StreamReader(filename, CheckEncoder());   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                dblayer.Current_Company_Info = _companyinfo;
                dblayer.AddHakladaRecord(CreateShuratHaklada(line));
            }

            sr.Close();
        }

        public void ImportHaklada(String filename, CompanyInfo _companyinfo, DBLayer _dblayer, string delimiter)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;

            String line = "";
            //MessageBox.Show("filename: " + filename);
            //CheckEncoder();
            //MessageBox.Show("_importEncoding: " + _importEncoding);
            //MessageBox.Show("CheckEncoder: " + CheckEncoder().ToString());
            StreamReader sr = new StreamReader(filename, CheckEncoder());    //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic
            //MessageBox.Show("filename ", filename);
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                dblayer.Current_Company_Info = _companyinfo;
                dblayer.AddHakladaRecord(CreateShuratHakladaDelimter(line, delimiter));
            }

            sr.Close();
        }

        public void ImportInboxHaklada(String filename, CompanyInfo _companyinfo, DBLayer _dblayer)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;

            String line = "";
            StreamReader sr = new StreamReader(filename, CheckEncoder());   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                dblayer.Current_Company_Info = _companyinfo;
                dblayer.AddHakladaInboxRecord(CreateShuratHaklada(line));
            }

            sr.Close();
        }

        public void ImportInboxHaklada(String filename, CompanyInfo _companyinfo, DBLayer _dblayer, string delimiter)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;

            String line = "";
            StreamReader sr = new StreamReader(filename, CheckEncoder());   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                dblayer.Current_Company_Info = _companyinfo;
                dblayer.AddHakladaInboxRecord(CreateShuratHakladaDelimter(line, delimiter));
            }

            sr.Close();
        }

        public void ImportCompanies(String filename, CompanyInfo _companyinfo, DBLayer _dblayer)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;

            String line = "";
            StreamReader sr = new StreamReader(filename, CheckEncoder());   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                dblayer.Current_Company_Info = _companyinfo;
                dblayer.AddCompany(CreateCompany(line));
            }

            sr.Close();
        }

        public void ImportCompanies(String filename, CompanyInfo _companyinfo, DBLayer _dblayer, string delimiter)
        {
            Company_Info = _companyinfo;
            dblayer = _dblayer;

            String line = "";
            StreamReader sr = new StreamReader(filename, CurrentImportEncoding);   //Encoding.GetEncoding("iso-8859-8") //Hebrew From Magic

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                dblayer.Current_Company_Info = _companyinfo;
                //dblayer.AddCompany(CreateCompanyDelimter(line, delimiter));
                dblayer.AddCompany(CreateCompanyDelimterINI(line, delimiter));
            }

            sr.Close();
        }

        private Company CreateCompany(string line)
        {
            Company company = new Company();
            int pos = 0;
            company.CountryID = Convert.ToInt32(line.Substring(pos, 4));
            pos += 5;
            company.CompanyName = line.Substring(pos, 75);
            pos += 76;
            company.CompanyVAT = line.Substring(pos, 9);
            pos += 10;
            company.AccountCode = line.Substring(pos, 15);
            pos += 16;
            company.WriteCode = line.Substring(pos, 9);

            company.CompamyInfoCountryID = Company_Info.CompanyCountryID;
            company.CompamyInfoVAT = Company_Info.CompanyVAT;

            return company;
        }

        private Company CreateCompanyDelimter(string line, string delimiter)
        {
            Company company = new Company();
            string[] parts = line.Split(new string[] { delimiter }, StringSplitOptions.None);

            company.CountryID = Convert.ToInt32(parts[0]);
            company.CompanyName = parts[1];
            company.CompanyVAT = parts[2];
            company.AccountCode = parts[3];
            company.WriteCode = parts[4];

            company.CompamyInfoCountryID = Company_Info.CompanyCountryID;
            company.CompamyInfoVAT = Company_Info.CompanyVAT;

            return company;
        }

        private Company CreateCompanyDelimterINI(string line, string delimiter)
        {
            PrepareIniFileName();

            Company company = new Company();
            string[] parts = line.Split(new string[] { delimiter }, StringSplitOptions.None);

            company.CountryID = GetCompanyImportIntValue("CountryID", parts);
            company.CompanyName = GetCompanyImportStringValue("CountryName", parts);
            company.CompanyVAT = GetCompanyImportStringValue("VAT", parts);
            company.AccountCode = GetCompanyImportStringValue("AccountCode", parts);
            company.WriteCode = GetCompanyImportStringValue("WriteCode", parts);

            company.CompamyInfoCountryID = Company_Info.CompanyCountryID;
            company.CompamyInfoVAT = Company_Info.CompanyVAT;

            return company;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        public int GetCompanyImportIntValue(String filed, string[] parts)
        {
            return Int32.Parse(parts[GetIniImportStructure("Company Import Structure", filed) - 1]);
        }

        public string GetCompanyImportStringValue(String filed, string[] parts)
        {
            return parts[GetIniImportStructure("Company Import Structure", filed) - 1];
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        public int GetOutboxImportIntValue(String filed, string[] parts)
        {
            return Int32.Parse(parts[GetIniImportStructure("Import Outbox Structure", filed) - 1]);
        }

        public double GetOutboxImportDoubleValue(String filed, string[] parts)
        {
            return double.Parse(parts[GetIniImportStructure("Import Outbox Structure", filed) - 1]);
        }

        public string GetOutboxImportStringValue(String filed, string[] parts)
        {
            return parts[GetIniImportStructure("Import Outbox Structure", filed) - 1];
        }

        public DateTime GetOutboxImportDateTimeValue(String filed, string[] parts)
        {
            return Convert.ToDateTime(parts[GetIniImportStructure("Import Outbox Structure", filed) - 1]);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////

        public int GetIniImportStructure(string section, String filed)
        {
            //MessageBox.Show("[" + Company_Info.CompanySerialNumber + " " + section + "]");
            //MessageBox.Show("GetIniImportStructure: " + iniFile.IniReadValue(Company_Info.CompanySerialNumber + " " + section, filed));
            //MessageBox.Show("iniFile.path: " + iniFile.path);
            return Int32.Parse(iniFile.IniReadValue(Company_Info.CompanySerialNumber + " " + section, filed));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////

        private ShuratHaklada CreateShuratHakladaMokupINI(string line, string delimiter)
        {
            PrepareIniFileName();

            ShuratHaklada shurat_haklada = new ShuratHaklada();
            string[] parts = line.Split(new string[] { delimiter }, StringSplitOptions.None);

            String CompanyVAT = GetOutboxImportStringValue("VAT", parts).ToString();
            String CompanyName = GetOutboxImportStringValue("CompanyName", parts);

            Company company = CompanyIdentification.Identify(dblayer, Company_Info, null, CompanyVAT, CompanyName, null);

            if (company != null)
            {
                CompanyVAT = company.CompanyVAT;
                CompanyName = company.CompanyName;

                shurat_haklada.CompanyID = company.CompanyID;
                shurat_haklada.CountryID = GetOutboxImportIntValue("CountryID", parts);
                shurat_haklada.CompanyVAT = CompanyVAT; // iniFile.IniReadValue(companyinfo.CompanySerialNumber + " Import Outbox Structure", "VAT");
                shurat_haklada.CompanyName = CompanyName; // iniFile.IniReadValue(companyinfo.CompanySerialNumber + " Import Outbox Structure", "CountryName");
                shurat_haklada.ActionCode = GetOutboxImportIntValue("ActionCode", parts);
                shurat_haklada.MisparMismach = GetOutboxImportIntValue("MisparMismach", parts);
                shurat_haklada.TarichMismach = GetOutboxImportDateTimeValue("TarichMismach", parts);
                shurat_haklada.TarichAcher = GetOutboxImportDateTimeValue("TarichAcher", parts);
                shurat_haklada.ActionDetails = GetOutboxImportStringValue("ActionDetails", parts);
                shurat_haklada.AhuzHaMaam = GetOutboxImportDoubleValue("Maam", parts);
                shurat_haklada.SchumPaturMaam = GetOutboxImportIntValue("SchumPaturMaam", parts);
                shurat_haklada.SchumMaam = GetOutboxImportIntValue("SchumMaam", parts);
                shurat_haklada.SchumKolelMaam = GetOutboxImportIntValue("SchumKolelMaam", parts);
                shurat_haklada.Attachment = GetOutboxImportStringValue("Attachment", parts);

                shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID;
                shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT;
            }

            return shurat_haklada;
        }

        private ShuratHaklada CreateShuratHakladaMokup(string line)
        {
            ShuratHaklada shurat_haklada = new ShuratHaklada();
            int pos = 0;
            String CountryID = Convert.ToInt32(line.Substring(pos, 4)).ToString();
            pos += 4;
            String CompanyVAT = line.Substring(pos, 9);
            pos += 9;
            String CompanyName = line.Substring(pos, 30);
            pos += 30;

            Company company = CompanyIdentification.Identify(dblayer, Company_Info, CountryID, CompanyVAT, CompanyName, null);

            if (company != null)
            {
                shurat_haklada.CompanyID = dblayer.GetCompany(CountryID, CompanyVAT).CompanyID;

                shurat_haklada.ActionCode = Convert.ToInt32(line.Substring(pos, 2));
                pos += 2;
                shurat_haklada.MisparMismach = Convert.ToInt32(line.Substring(pos, 9));
                pos += 9;
                shurat_haklada.TarichMismach = Convert.ToDateTime(line.Substring(pos, 10));
                pos += 10;
                shurat_haklada.TarichAcher = Convert.ToDateTime(line.Substring(pos, 10));
                pos += 10;
                shurat_haklada.ActionDetails = line.Substring(pos, 70);
                pos += 70;
                shurat_haklada.AhuzHaMaam = Convert.ToDouble(line.Substring(pos, 5));
                pos += 5;
                shurat_haklada.SchumPaturMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.SchumMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.SchumKolelMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.Attachment = line.Substring(pos, line.Length - pos);

                shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID;
                shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT;
            }

            return shurat_haklada;
        }

        private ShuratHaklada CreateShuratHaklada(string line)
        {
            ShuratHaklada shurat_haklada = new ShuratHaklada();
            int pos = 0;
            String CountryID = Convert.ToInt32(line.Substring(pos, 4)).ToString();
            pos += 4;
            String CompanyVAT = line.Substring(pos, 9);
            pos += 9;
            String CompanyName = line.Substring(pos, 30);
            pos += 30;
  
            Company company = CompanyIdentification.Identify(dblayer, Company_Info, CountryID, CompanyVAT, CompanyName, null);

            if (company != null)
            {
                shurat_haklada.CompanyID = dblayer.GetCompany(CountryID, CompanyVAT).CompanyID;

                shurat_haklada.ActionCode = Convert.ToInt32(line.Substring(pos, 2));
                pos += 2;
                shurat_haklada.MisparMismach = Convert.ToInt32(line.Substring(pos, 9));
                pos += 9;
                shurat_haklada.TarichMismach = Convert.ToDateTime(line.Substring(pos, 10));
                pos += 10;
                shurat_haklada.TarichAcher = Convert.ToDateTime(line.Substring(pos, 10));
                pos += 10;
                shurat_haklada.ActionDetails = line.Substring(pos, 70);
                pos += 70;
                shurat_haklada.AhuzHaMaam = Convert.ToDouble(line.Substring(pos, 5));
                pos += 5;
                shurat_haklada.SchumPaturMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.SchumMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.SchumKolelMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.Attachment = line.Substring(pos, line.Length - pos);

                shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID;
                shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT;
            }
            return shurat_haklada;
        }


        private ShuratHaklada CreateShuratHakladaHavshavshevet(string line)
        {
            ShuratHaklada shurat_haklada = new ShuratHaklada();
            int pos = 0;
            String CountryID = Convert.ToInt32(line.Substring(pos, 4)).ToString();
            pos += 4;
            String CompanyVAT = line.Substring(pos, 9);
            pos += 9;
            String CompanyName = line.Substring(pos, 30);
            pos += 30;

            Company company = CompanyIdentification.Identify(dblayer, Company_Info, CountryID, CompanyVAT, CompanyName, null);

            if (company != null)
            {
                shurat_haklada.CompanyID = dblayer.GetCompany(CountryID, CompanyVAT).CompanyID;

                shurat_haklada.ActionCode = Convert.ToInt32(line.Substring(pos, 2));
                pos += 2;
                shurat_haklada.MisparMismach = Convert.ToInt32(line.Substring(pos, 9));
                pos += 9;
                shurat_haklada.TarichMismach = Convert.ToDateTime(line.Substring(pos, 10));
                pos += 10;
                shurat_haklada.TarichAcher = Convert.ToDateTime(line.Substring(pos, 10));
                pos += 10;
                shurat_haklada.ActionDetails = line.Substring(pos, 70);
                pos += 70;
                shurat_haklada.AhuzHaMaam = Convert.ToDouble(line.Substring(pos, 5));
                pos += 5;
                shurat_haklada.SchumPaturMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.SchumMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.SchumKolelMaam = Convert.ToDouble(line.Substring(pos, 12));
                pos += 12;
                shurat_haklada.Attachment = line.Substring(pos, line.Length - pos);

                shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID;
                shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT;
            }

            return shurat_haklada;
        }

        private ShuratHaklada CreateShuratHakladaDelimter(string line, string delimiter)
        {
            ShuratHaklada shurat_haklada = null;
            string[] parts = line.Split(new string[] { delimiter }, StringSplitOptions.None);


            String CountryID = parts[0];
            String CompanyVAT = parts[1];
            String CompanyName = parts[2].Replace("'", "''");

            Company company = CompanyIdentification.Identify(dblayer, Company_Info, null, CompanyVAT, CompanyName, null);

            if (company != null)
            {
                shurat_haklada = new ShuratHaklada();
                shurat_haklada.CompanyID = company.CompanyID;// dblayer.GetCompany(CountryID, CompanyVAT).CompanyID;
                shurat_haklada.ActionCode = Convert.ToInt32(parts[3]);
                shurat_haklada.MisparMismach = Convert.ToInt32(parts[4]);

                shurat_haklada.TarichMismach = DateTime.Parse(parts[5], new System.Globalization.CultureInfo("en-AU", false)); //Convert.ToDateTime(parts[5]);
                shurat_haklada.TarichAcher = DateTime.Parse(parts[6], new System.Globalization.CultureInfo("en-AU", false)); //Convert.ToDateTime(parts[6]);

                shurat_haklada.ActionDetails = parts[7].Replace("'", "''"); ;

                if (parts[8] != "")
                {
                    shurat_haklada.AhuzHaMaam = Convert.ToDouble(parts[8]);
                }
                if (parts[9] != "")
                {
                    shurat_haklada.SchumPaturMaam = Convert.ToDouble(parts[9]);
                }
                if (parts[10] != "")
                {
                    shurat_haklada.SchumMaam = Convert.ToDouble(parts[10]);
                }
                if (parts[11] != "")
                {
                    shurat_haklada.SchumKolelMaam = Convert.ToDouble(parts[11]);
                }

                shurat_haklada.Attachment = parts[12];

                if ((parts[13] != "") && (parts[13] != "0"))
                {
                    shurat_haklada.LeTkufaMe = DateTime.Parse(parts[13], new System.Globalization.CultureInfo("en-AU", false));
                }
                else
                {
                    shurat_haklada.LeTkufaMe = shurat_haklada.TarichAcher;
                }
                //shurat_haklada.LeTkufaMe = new DateTime(shurat_haklada.LeTkufaMe.Year, shurat_haklada.LeTkufaMe.Month, 1);

                if ((parts[14] != "") && (parts[14] != "0"))
                {
                    shurat_haklada.LeTkufaUd = DateTime.Parse(parts[14], new System.Globalization.CultureInfo("en-AU", false));
                }
                else
                {
                    shurat_haklada.LeTkufaUd = shurat_haklada.TarichAcher;
                }
                //shurat_haklada.LeTkufaUd = new DateTime(shurat_haklada.LeTkufaUd.Year, shurat_haklada.LeTkufaUd.Month, 1);

                shurat_haklada.MisparProyect = parts[15];

                shurat_haklada.CompamyInfoCountryID = Company_Info.CompanyCountryID;
                shurat_haklada.CompamyInfoVAT = Company_Info.CompanyVAT;
            }

            return shurat_haklada;
        }
    }
}
