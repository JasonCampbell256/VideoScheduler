using Microsoft.Extensions.Logging;
using VideoScheduler.Data;

namespace VideoScheduler;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<YamlRepository>(sp =>
        {
            try
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, "Data");
                return new YamlRepository(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to init YamlRepository: {ex}");
                // Return a dummy or throw? If we return null, DI fails.
                // Let's return a repo with a temp path to avoid crash
                return new YamlRepository(Path.Combine(Path.GetTempPath(), "VideoScheduler_Fallback"));
            }
        });
        builder.Services.AddSingleton<LibraryScanner>();
        builder.Services.AddSingleton<VlcService>();
        builder.Services.AddSingleton<SchedulePlanner>();
        builder.Services.AddSingleton<AutomationService>();

        return builder.Build();
    }
}
