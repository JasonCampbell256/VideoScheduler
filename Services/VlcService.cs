using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace VideoScheduler.Services
{
    public enum VlcState
    {
        Disconnected,
        Playing,
        Paused,
        Stopped,
        Error,
        Unknown
    }

    public class VlcStatus
    {
        public VlcState State { get; set; } = VlcState.Disconnected;
        public int Time { get; set; } // Current seconds
        public int Length { get; set; } // Total seconds
        public string Filename { get; set; }
        public double Position { get; set; } // 0.0 to 1.0
    }

    public class VlcService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:8080/requests/status.xml";
        private const string Password = "password";

        public bool IsConnected { get; private set; }
        public string LastError { get; private set; }

        public VlcService()
        {
            _httpClient = new HttpClient();
            var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{Password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);
        }

        public async Task<VlcStatus> GetStatusAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                response.EnsureSuccessStatusCode();
                var xml = await response.Content.ReadAsStringAsync();
                
                var doc = XDocument.Parse(xml);
                var root = doc.Element("root");
                
                var stateString = root?.Element("state")?.Value ?? "unknown";
                var state = ParseVlcState(stateString);
                
                var status = new VlcStatus
                {
                    State = state,
                    Time = int.Parse(root?.Element("time")?.Value ?? "0"),
                    Length = int.Parse(root?.Element("length")?.Value ?? "0"),
                    Position = double.Parse(root?.Element("position")?.Value ?? "0")
                };

                // Try to find filename in information/category[@name='meta']/info[@name='filename']
                var meta = root?.Element("information")?.Elements("category")
                    .FirstOrDefault(c => c.Attribute("name")?.Value == "meta");
                
                if (meta != null)
                {
                    status.Filename = meta.Elements("info")
                        .FirstOrDefault(i => i.Attribute("name")?.Value == "filename")?.Value;
                }

                IsConnected = true;
                LastError = null;
                return status;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                LastError = ex.Message;
                return new VlcStatus { State = VlcState.Error };
            }
        }

        public async Task<bool> ConnectAsync()
        {
            LastError = null;
            try
            {
                // Test connection by getting status
                var response = await _httpClient.GetAsync(BaseUrl);
                response.EnsureSuccessStatusCode();
                IsConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                LastError = ex.Message;
                Console.WriteLine($"VLC HTTP Connection Error: {ex.Message}");
                return false;
            }
        }

        private async Task SendCommandAsync(string query)
        {
            try
            {
                var url = $"{BaseUrl}{query}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                IsConnected = true;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                LastError = ex.Message;
                Console.WriteLine($"VLC HTTP Send Error: {ex.Message}");
            }
        }

        public async Task Play(string filePath)
        {
            // Clear playlist first to ensure immediate playback of new file
            await SendCommandAsync("?command=pl_empty");
            
            // Add and play
            // Use AbsoluteUri to handle spaces and special characters correctly for VLC
            string uri;
            try 
            {
                uri = new Uri(filePath).AbsoluteUri;
            }
            catch
            {
                // Fallback for relative paths or weird formats, though unlikely in this app
                uri = filePath;
            }

            var encodedPath = Uri.EscapeDataString(uri).Replace("'", "%27");
            await SendCommandAsync($"?command=in_play&input={encodedPath}");
        }

        public async Task Pause()
        {
            await SendCommandAsync("?command=pl_pause");
        }

        public async Task Stop()
        {
            await SendCommandAsync("?command=pl_stop");
        }

        public async Task Seek(int seconds)
        {
            await SendCommandAsync($"?command=seek&val={seconds}");
        }

        private VlcState ParseVlcState(string stateString)
        {
            return stateString?.ToLowerInvariant() switch
            {
                "playing" => VlcState.Playing,
                "paused" => VlcState.Paused,
                "stopped" => VlcState.Stopped,
                "error" => VlcState.Error,
                _ => VlcState.Unknown
            };
        }
    }
}
