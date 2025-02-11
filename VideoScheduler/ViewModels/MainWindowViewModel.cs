using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Threading;
using LibVLCSharp.Shared;

namespace VideoScheduler.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";
        public MediaPlayer MediaPlayer { get; }
        private Media? _currentMedia;
        private readonly LibVLC _libVlc;

        public MainWindowViewModel()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                /*
                TODO we should bundle this in with the app when distributing it.
                This path may not be accurate on all systems
                */
                //var libVlcPath = "/Applications/VLC.app/Contents/MacOS/lib/libvlc.dylib";
                //_libVlc = new LibVLC(libVlcPath);
                _libVlc = new LibVLC();
            }
            else
            {
                _libVlc = new LibVLC();
            }
            MediaPlayer = new MediaPlayer(_libVlc);
            MediaPlayer.EndReached += (sender, args) =>
            {
                //Dispatcher.UIThread.Post(PlayNextVideo);
            };
            Dispatcher.UIThread.Post(Play);
        }

        
        public void Play()
        {
            if (Design.IsDesignMode)
            {
                return;
            }

            _currentMedia?.Dispose();
            _currentMedia = new Media(_libVlc, "https://sample-videos.com/video123/mp4/720/big_buck_bunny_720p_1mb.mp4");

            MediaPlayer.Play(_currentMedia);
        }
    }
}
