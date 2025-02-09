using Avalonia.Controls;
using Avalonia.Threading;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Timers;
using VideoScheduler.Domain;
using VideoScheduler.UI.Models;

namespace VideoScheduler.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly LibVLC _libVLC = new LibVLC();
        private Timer timer;
        private Media? currentMedia;
        private bool checking = false;

        public MediaPlayer MediaPlayer { get; }
        public Scheduler Scheduler { get; }
        public Queue<string> videos { get; set; }
        

        public MainWindowViewModel()
        {
            //videos = new List<string>();
            MediaPlayer = new MediaPlayer(_libVLC);
            MediaPlayer.EndReached += (sender, args) =>
            {
                Dispatcher.UIThread.Post(PlayNextVideo);
            };
            Scheduler = new Scheduler();
            Scheduler.LoadSchedule();
            timer = new Timer(1000);
            timer.Elapsed += CheckTimeBlocks;
            timer.AutoReset = true;
            timer.Start();
        }

        private void CheckTimeBlocks(object sender, EventArgs e)
        {
            if (checking)
            {
                return;
            }
            checking = true;
            var nextVideos = Scheduler.GetVideosForCurrentTimeBlock();
            if (nextVideos != null && nextVideos.Count > 0)
            {
                videos = nextVideos;


                if (videos != null && videos.Count > 0)
                {
                    Dispatcher.UIThread.Post(PlayNextVideo);
                }
            }
            checking = false;
        }

        public void PlayNextVideo()
        {
            if (videos.Count > 0)
            {
                Play(videos.Dequeue());
            }
        }

        public void Play(string path)
        {
            if (Design.IsDesignMode)
            {
                return;
            }

            currentMedia?.Dispose();
            currentMedia = new Media(_libVLC, path);

            MediaPlayer.Play(currentMedia);
        }

    }
}
