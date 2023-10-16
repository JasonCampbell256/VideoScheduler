using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler
{
    public partial class Form1 : Form
    {
        private readonly Player _mediaPlayerForm;
        private readonly Queue<string> _playlist = new Queue<string>();
        private readonly PersistenceManagers _persistenceManagers = new PersistenceManagers();
        private List<TimeBlock> _timeBlocks = new List<TimeBlock>();
        Timer timer;

        public void loadschedule()
        {
            _timeBlocks = _persistenceManagers.timeBlockManager.GetTimeBlocks(DateTime.Now.DayOfWeek);
        }


        public Form1()
        {
            InitializeComponent();
            _mediaPlayerForm = new Player();
            loadschedule();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += CheckTimeBlocks;
            timer.Start();
        }

        private void CheckTimeBlocks(object sender, EventArgs e)
        {
            loadschedule();
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            var truncatedCurrentTime = new TimeSpan(currentTime.Hours, currentTime.Minutes, currentTime.Seconds);

            foreach (var timeBlock in _timeBlocks)
            {
                if (truncatedCurrentTime == timeBlock.StartTime)
                {
                    var videos = _persistenceManagers._picker.GetVideosForTimeBlock(timeBlock);
                    foreach (var video in videos)
                    {
                        AddToQueue(video.FilePath);
                    }
                    Play();
                }
            }
        }

        private void Play()
        {
            if (_mediaPlayerForm == null)
            {
                OpenPlayer();
            }
            _mediaPlayerForm.GetCurrentPlayer().URL = _playlist.Dequeue();
            _listBoxQueue.Items.Remove(_mediaPlayerForm.GetCurrentPlayer().URL);

            SetPreload();
        }

        private void AddToQueue(string url)
        {
            _playlist.Enqueue(url);
            _listBoxQueue.Items.Add(url);
        }

        private void QueueNextVideo(AxWindowsMediaPlayer player)
        {
            if (_playlist.Count == 0)
            {
                return;
            }
            var url = _playlist.Dequeue();
            player.URL = url;
            player.Visible = false;

        }

        private void SetPreload()
        {
            var preloadedPlayer = _mediaPlayerForm.GetHiddenPlayer();
            QueueNextVideo(preloadedPlayer);
            System.GC.Collect();
        }

        private void Player_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (sender.Equals(_mediaPlayerForm.GetHiddenPlayer()) && e.newState == 3)
            {
                _mediaPlayerForm.GetHiddenPlayer().Ctlcontrols.pause();
            }
            else if (sender.Equals(_mediaPlayerForm.GetCurrentPlayer()) && e.newState == 8)
            {
                NextVideo();
            }
        }

        private void NextVideo()
        {
            SwitchPlayers();
            SetPreload();
        }

        private void SwitchPlayers()
        {
            _mediaPlayerForm.SwitchPlayers();
        }

        private void OpenPlayer()
        {
            _mediaPlayerForm.Show();
            _mediaPlayerForm.GetCurrentPlayer().PlayStateChange += Player_PlayStateChange;
            QueueNextVideo(_mediaPlayerForm.GetCurrentPlayer());
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonOpenPlayer))
            {
                OpenPlayer();
                _mediaPlayerForm.GetHiddenPlayer().PlayStateChange += Player_PlayStateChange;
            }
            else if (sender.Equals(_buttonOpenSchedule))
            {
                ScheduleControl scheduleControl = new ScheduleControl(_persistenceManagers);
                scheduleControl.Show();
            }
        }
    }
}
