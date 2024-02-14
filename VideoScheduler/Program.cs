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
                    using (System.IO.File.Create(file));
                }
            }
        }

        // Handle exceptions from all threads in the AppDomain
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogException((Exception)e.ExceptionObject);
            // Here, you could add more code to handle the exception, e.g., display a dialog box
        }

        // Handle exceptions from Windows Forms threads
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogException(e.Exception);
            // Here, you could add more code to handle the exception, e.g., display a dialog box
        }

        private static void LogException(Exception e)
        {
            string logFilePath = @"Logs\log.txt";
            Directory.CreateDirectory("Logs");  // Ensure the directory exists
            string logText = $"[{DateTime.Now}] Unhandled Exception: {e.ToString()}\n";
            File.AppendAllText(logFilePath, logText);
        }

    }
}
