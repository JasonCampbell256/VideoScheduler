using Avalonia;
using System;
using System.Collections.Generic;

namespace VideoScheduler.UI
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            InitializeSettings();
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

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
                    using (System.IO.File.Create(file))
                    {
                        // Do nothing, the file should be empty at this point.
                    }
                }
            }
        }
    }
}
