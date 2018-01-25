using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pulsar
{
    public partial class frmHelp : Form
    {
        //CountryIDFrom|$VatFrom|$CountryIDTo|$VatTo|$two letters of pkuda|[00 -99] command##value|....[00 -99] command##value|$time stamp date time|$|$False|$WriteCode|$
        //2|$117|$513638346|$117|$513638346|$KT|027|0303/12/2012|0503/12/2012|0603/12/2012|07adirim|08513638346|09eee|101000|110|12170|131170|14hm|$12/3/2012 3:07:41 PM|$|$False|$123456789|$
        //3|$117|$513638346|$117|$513638346|$KT|0211|0303/12/2012|0503/12/2012|0603/12/2012|07adirim|08513638346|09ss|1094.87|111|1216.13|13111|14hm|$12/3/2012 3:20:00 PM|$|$False|$123456789|$

        public enum eHelpType
        {
            ImportCompany = 1,
            ImportKotertTnua = 2,
            ImportOutbox = 3
        }

        public eHelpType HelpType { get; set; }

        public frmHelp()
        {
            InitializeComponent();
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void ShowHelp()
        {
            switch (HelpType)
            {
                case eHelpType.ImportKotertTnua:
                    ShowKoterTnuaImportHelp();
                    break;
                case eHelpType.ImportCompany:
                    ShowCompanyImportHelp();
                    break;
                case eHelpType.ImportOutbox:
                    ShowOutboxImportHelp();
                    break;
                default:
                    break;
            }
        }

        private void ShowKoterTnuaImportHelp()
        {
            lstPreview.Items.Add("main delimiter => |$");
            lstPreview.Items.Add("internal pkuda delimiter => |");

            lstPreview.Items.Add("columns structure");
            lstPreview.Items.Add("CountryIDFrom|$VatFrom|$CountryIDTo|$VatTo|$two letters of pkuda|[00 -99] command##value|....[00 -99] command##value|$time stamp date time|$|$False|$WriteCode|$");
            lstPreview.Items.Add("");

            lstPreview.Items.Add("e.g: pkuda structure");
            lstPreview.Items.Add("two letters of pkuda|[00 -99] command##value|....[00 -99] command##value");
            lstPreview.Items.Add("two letters of pkuda => KT");
            lstPreview.Items.Add("comand 02 Value 7  => 027");
            lstPreview.Items.Add("comand 03 Value 03/12/2012  => 0303/12/2012");
            lstPreview.Items.Add("comand 05 Value 03/12/2012  => 0503/12/2012");
            lstPreview.Items.Add("comand 06 Value 03/12/2012  => 0603/12/2012");
            lstPreview.Items.Add("comand 07 Value adirim  => 07adirim");
            lstPreview.Items.Add("comand 08 Value 513638346  => 08513638346");
            lstPreview.Items.Add("comand 09 Value 1000  => 101000");
            lstPreview.Items.Add("comand 10 Value 0 => 110");
            lstPreview.Items.Add("comand 12 Value 170  => 12170");
            lstPreview.Items.Add("comand 13 Value 1170  => 131170");
            lstPreview.Items.Add("comand 14 Value hm  => 14hm");
            lstPreview.Items.Add("record result");
            lstPreview.Items.Add("KT|027|0303/12/2012|0503/12/2012|0603/12/2012|07adirim|08513638346|09eee|101000|110|12170|131170|14hm");
            lstPreview.Items.Add("full record result");
            lstPreview.Items.Add("117|$513638346|$117|$513638346|$KT|027|0303/12/2012|0503/12/2012|0603/12/2012|07adirim|08513638346|09eee|101000|110|12170|131170|14hm|$12/3/2012 3:07:41 PM|$|$False|$123456789|$");

            lstPreview.Items.Add("");
            lstPreview.Items.Add("e.g:");
            lstPreview.Items.Add("117|$513638346|$117|$513638346|$KT|027|0303/12/2012|0503/12/2012|0603/12/2012|07adirim|08513638346|09eee|101000|110|12170|131170|14hm|$12/3/2012 3:07:41 PM|$|$False|$123456789|$");
            lstPreview.Items.Add("117|$513638346|$117|$513638346|$KT|0211|0303/12/2012|0503/12/2012|0603/12/2012|07adirim|08513638346|09ss|1094.87|111|1216.13|13111|14hm|$12/3/2012 3:20:00 PM|$|$False|$123456789|$");
        }


        private void ShowCompanyImportHelp()
        {
            lstPreview.Items.Clear();
            lstPreview.Items.Add("Import Company File Structure.");
            lstPreview.Items.Add("|=======================================================|");
            lstPreview.Items.Add("|Filed Name       | Pos   	| Length| Type          |");
            lstPreview.Items.Add("|=======================================================|");
            lstPreview.Items.Add("|CountryID        | 0    	| 4  	| Numeric       |");
            lstPreview.Items.Add("|CompanyName      | 5    	| 75	| AlphaNumeric  |");
            lstPreview.Items.Add("|CompanyVAT       | 81  	| 9  	| Numeric       |");
            lstPreview.Items.Add("|AccountCode      | 91  	| 15	| Numeric       |");
            lstPreview.Items.Add("|WriteCode        | 107         | 9  	| AlphaNumeric  |");
            lstPreview.Items.Add("|=======================================================|");
            lstPreview.Items.Add("");
            lstPreview.Items.Add("Row Data Sample:");
            lstPreview.Items.Add("e.g:");
            lstPreview.Items.Add("0117 abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcd 123456789 123451234512345 987654321");
        }

        private void ShowOutboxImportHelp()
        {
            lstPreview.Items.Clear();
            lstPreview.Items.Add("Outbox Import File Structure.");
            lstPreview.Items.Add("CountryID");
            lstPreview.Items.Add("CompanyVAT");
            lstPreview.Items.Add("CompanyName");
            lstPreview.Items.Add("ActionCode");
            lstPreview.Items.Add("MisparMismach");
            lstPreview.Items.Add("TarichMismach");
            lstPreview.Items.Add("TarichAcher");
            lstPreview.Items.Add("ActionDetails");
            lstPreview.Items.Add("Maam");
            lstPreview.Items.Add("SchumPaturMaam");
            lstPreview.Items.Add("SchumMaam");
            lstPreview.Items.Add("SchumKolelMaam");
            lstPreview.Items.Add("Attachment");            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
