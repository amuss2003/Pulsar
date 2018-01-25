using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pulsar
{
    public class ActiveLayout
    {
        public const uint KLF_ACTIVATE = 1; //activate the layout
        public const int KL_NAMELENGTH = 9; // length of the keyboard buffer
        public const string LANG_EN_US = "00000409";
        public const string LANG_HE_IL = "0000040d";

        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(System.Text.StringBuilder pwszKLID);
        //[out] string that receives the name of the locale identifier);

        // For set:
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);

        // For get:
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(
            [In] IntPtr hWnd,
            [Out, Optional] IntPtr lpdwProcessId
            );

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern ushort GetKeyboardLayout(
            [In] int idThread
            );

        static public ushort GetKeyboardLayout()
        {
            return GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero));
        }

        static public long SetKeyboardLayout(String LayoutToLoad) // "00000409" or "00000419"
        {
            long HKL_Layout = LoadKeyboardLayout(LayoutToLoad, 1);
            // 00000429
            //Где-то в коде
            //0x0050 - код сообщения WM_INPUTLANGCHANGEREQUEST
            PostMessage(GetForegroundWindow(), 0x0050, 2, 0);   // Layout changed. You think anyone checks TO WHAT?! just switch triggering
            return HKL_Layout;
        }

        static public string getName()
        {
            StringBuilder name = new System.Text.StringBuilder(KL_NAMELENGTH);
            GetKeyboardLayoutName(name);
            return name.ToString();
        }

        static public bool bHebrew { get; set; }

        static public void Hebrew()
        {
            LoadKeyboardLayout(LANG_HE_IL, KLF_ACTIVATE);
            bHebrew = true;
        }

        public static void English()
        {
            LoadKeyboardLayout(LANG_EN_US, KLF_ACTIVATE);
            bHebrew = false;
        }

        static public uint AttemptNo = 0;
        static public int SwitchTo(uint LayoutID) // 0x409 for ENG
        {
            if (ActiveLayout.GetKeyboardLayout() != LayoutID)  // If not english ( 0x409 ) - switch 
            {
                bool Found = false;
                foreach (InputLanguage c in InputLanguage.InstalledInputLanguages)
                {
                    if (c.Culture.LCID != LayoutID)
                        continue;
                    Found = true;
                }
                if (!Found)
                {
                    AttemptNo = 0;
                    return -2;
                }
                if (AttemptNo > InputLanguage.InstalledInputLanguages.Count)
                {
                    AttemptNo = 0;
                    return -1;
                }
                String HexString = LayoutID.ToString("X");
                HexString = "00000000".Substring(HexString.Length) + HexString;
                ActiveLayout.SetKeyboardLayout(HexString);
                // Console.WriteLine(HexString);
                AttemptNo++;
            }
            else
            {
                AttemptNo = 0;
                return 1;
            }
            return 0;
            // Sent request, result not known yet
        }
    }

}
