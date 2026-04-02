using VideoScheduler.Components;
using VideoScheduler.Data;
using VideoScheduler.Services;
#if WINDOWS
using System.Windows.Forms;
#endif

var builder = WebApplication.CreateBuilder(args);

var configuredPort = builder.Configuration.GetValue<int>("Server:Port", 5050);
if (configuredPort < 1 || configuredPort > 65535)
{
    throw new InvalidOperationException($"Invalid Server:Port value '{configuredPort}'. Expected a value between 1 and 65535.");
}

var appUrl = $"http://localhost:{configuredPort}";
builder.WebHost.UseUrls(appUrl);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<YamlRepository>(sp =>
{
    // Use a local "Data" folder in the current directory for portability
    var path = Path.Combine(Directory.GetCurrentDirectory(), "DataFiles");
    return new YamlRepository(path);
});

builder.Services.AddSingleton<LibraryScanner>();
builder.Services.AddSingleton<VlcService>();
builder.Services.AddSingleton<SchedulePlanner>();
builder.Services.AddSingleton<AutomationService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<AutomationService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

#if WINDOWS
// Start the Web Server in the background
await app.StartAsync();

// Start the System Tray Icon (WinForms)
var uiThread = new Thread(() =>
{
    ApplicationConfiguration.Initialize();
    Application.Run(new VideoScheduler.TrayApplicationContext(appUrl));
});
uiThread.SetApartmentState(ApartmentState.STA);
uiThread.Start();
uiThread.Join();

// Stop the server when UI exits
await app.StopAsync();
#else
app.Run();
#endif