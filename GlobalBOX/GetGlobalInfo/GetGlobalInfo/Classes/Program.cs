﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Pulsar
{
    static class Program
    {
        public static bool IsServerOnline;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //FirstRun();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain(args));
        }

    }
}
