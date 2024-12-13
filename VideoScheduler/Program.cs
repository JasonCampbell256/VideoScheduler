using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoScheduler
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitializeSettings();
            Application.Run(new MainForm());
        }

        private static void InitializeSettings()
        {
            List<string> files = new List<string>()
            {
"attributeTrees.json",
"blockTemplateItems.json",
"schedulableBlocks.json",
"showRuns.json",
"movieRuns.json",
"timeBlocks.json",
"settings.json"
            };
            foreach (var file in files)
            {
                if (!System.IO.File.Exists(file))
                {
                    System.IO.File.Create(file);
                }
            }
        }

        // Handle exceptions from all threads in the AppDomain
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Logger.LogException(ex);
            }
            else
            {
                Logger.LogMessage("An unknown unhandled exception occurred.");
            }
        }

        // Handle exceptions from Windows Forms threads
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.LogException(e.Exception);
        }

    }
}
