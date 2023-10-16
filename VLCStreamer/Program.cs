using System.Diagnostics;

namespace VLCStreamer
{
    class Program
    {
        private static Queue<string> _playlist = new Queue<string>();
        private const string streamingPort = "8080";

        static void Main(string[] args)
        {
            _playlist.Enqueue("G:\\Toonami\\Content\\Bumpers\\Toonami\\Moltar\\Toonami Intros\\Moltar Toonami Intro.mp4");
            _playlist.Enqueue("G:\\Toonami\\Content\\Bumpers\\Toonami\\Moltar\\Show Intros\\Reboot\\0002.mp4");

            StartStreamingNextVideo();

            Console.WriteLine($"Streaming on port {streamingPort}. Press any key to exit...");
            Console.ReadKey();
        }

        private static void StartStreamingNextVideo()
        {
            if ( _playlist.Count == 0 ) { 
                Console.WriteLine("all videos streamed");
                return;
            }

            string videoPath = _playlist.Dequeue();
            Console.WriteLine($"Streaming {videoPath}");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "vlc",
                //Arguments = $"dummy --dummy-quiet \"{videoPath}\" --sout \"#standard{{access=http,mux=ts,dst=:{streamingPort}}}\"",
                Arguments = $"\"{videoPath}\" --sout \"#duplicate{{dst=std{{access=http,mux=ogg,dst=localhost:8080/stream.ogg}}",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = @"C:\Program Files\VideoLAN\VLC",

            };

            startInfo.EnvironmentVariables["VLC_PLUGIN_PATH"] = @"C:\Program Files\VideoLAN\VLC\plugins";

            Process vlcProcess = new Process
            {
                StartInfo = startInfo
            };

            vlcProcess.EnableRaisingEvents = true;
            vlcProcess.Exited += (sender, args) => StartStreamingNextVideo();

            vlcProcess.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            vlcProcess.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

            vlcProcess.Start();
            vlcProcess.BeginOutputReadLine();
            vlcProcess.BeginErrorReadLine();
        }
    }
}