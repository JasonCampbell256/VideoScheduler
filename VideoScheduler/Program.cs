using System;
using System.Collections.Generic;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateTempFiles();
            Application.Run(new Form1());
        }

        private static void CreateTempFiles()
        {
            List<string> files = new List<string>()
            {
"attributeTrees.json",
"blockTemplateItems.json",
"schedulableBlocks.json",
"showRuns.json",
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
    }
}
