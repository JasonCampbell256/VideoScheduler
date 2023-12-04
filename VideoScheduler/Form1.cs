using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler
{
    public partial class Form1 : Form
    {
        private readonly Player _mediaPlayerForm;
        private readonly Queue<string> _playlist = new Queue<string>();
        private readonly PersistenceManagers _persistenceManagers;
        private List<TimeBlock> _timeBlocks = new List<TimeBlock>();
        private TimeBlock currentTimeBlock = null;
        Timer timer;

        public void loadschedule()
        {
            _timeBlocks = _persistenceManagers.timeBlockManager.GetTimeBlocks(DateTime.Now.DayOfWeek);
        }


        public Form1()
        {
            InitializeComponent();
            if (PersistenceManagers.GetFilePath() == null)
            {
                MessageBox.Show("Please select your library folder.");
                var folderBrowserDialog = new FolderBrowserDialog();
                var result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PersistenceManagers.SetFilePath(folderBrowserDialog.SelectedPath);
                } else
                {
                    MessageBox.Show("No folder selected. The application will now close");
                }
            }
            _persistenceManagers = new PersistenceManagers();
            _mediaPlayerForm = new Player();
            loadschedule();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += CheckTimeBlocks;
            //OpenPlayer();
            timer.Start();
        }

        private void CheckTimeBlocks(object sender, EventArgs e)
        {
            if (!_mediaPlayerForm.Visible)
            {
                return;
            }
            loadschedule();
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            var truncatedCurrentTime = new TimeSpan(currentTime.Hours, currentTime.Minutes, currentTime.Seconds);

            foreach (var timeBlock in _timeBlocks)
            {
                if (truncatedCurrentTime == timeBlock.StartTime)
                {
                    currentTimeBlock = timeBlock;
                    var videos = _persistenceManagers._picker.GetVideosForTimeBlock(timeBlock);
                    foreach (var video in videos)
                    {
                        AddToQueue(video.FilePath);
                    }
                    Play();
                    break;
                } else if (truncatedCurrentTime > timeBlock.StartTime && truncatedCurrentTime < timeBlock.EndTime)
                {
                    if (currentTimeBlock != null && currentTimeBlock.Guid == timeBlock.Guid)
                    {
                        return;
                    }
                    currentTimeBlock = timeBlock;
                    var videos = _persistenceManagers._picker.GetVideosForTimeBlock(timeBlock);
                    var timeElapsed = truncatedCurrentTime - timeBlock.StartTime;
                    //find the video that should be playing
                    var timespanCounter = new TimeSpan(0, 0, 0);
                    var timeSpanToStart = new TimeSpan(0, 0, 0);
                    foreach (var video in videos)
                    {
                        var duration = VideoPicker.GetDuration(video.FilePath);
                        timespanCounter += duration;
                        var firstVideoFound = false;
                        if (timespanCounter >= timeElapsed)
                        {
                            AddToQueue(video.FilePath);
                            if (!firstVideoFound)
                            {
                                firstVideoFound = true;
                                timeSpanToStart = timeElapsed - (timespanCounter - duration);
                            }
                        }
                    }
                    
                    Play(timeSpanToStart.Duration());
                    break;
                }


            }
        }

        private void Play(TimeSpan startTime)
        {
            if (_playlist.Count != 0)
            {
                if (_mediaPlayerForm == null && _playlist.Count > 0)
                {
                    OpenPlayer();
                }
                _mediaPlayerForm.GetCurrentPlayer().URL = _playlist.Dequeue();
                _mediaPlayerForm.GetCurrentPlayer().Ctlcontrols.currentPosition = startTime.TotalSeconds;
                _listBoxQueue.Items.Remove(_mediaPlayerForm.GetCurrentPlayer().URL);

                SetPreload();
            }
            //StartStreaming();
        }

        private void StartStreaming()
        {
            if (_playlist.Count > 0)
            {
                string nextVideo = _playlist.Dequeue();
                //isStreaming = true;
                StreamVideo(nextVideo);
            }
            //else
            //{
            //    isStreaming = false;
            //}
        }

        private void StreamVideo(string videoPath)
        {
            videoPath = "\"" + videoPath + "\"";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-re -i {videoPath} -c:v libx264 -b:v 800k -c:a aac -b:a 128k -f mpegts udp://127.0.0.1:8080",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error: " + error);
                }
            }
            StartStreaming();
        }

        private void Play()
        {
            if (_playlist.Count != 0)
            {
                if (_mediaPlayerForm == null && _playlist.Count > 0)
                {
                    OpenPlayer();
                }
                _mediaPlayerForm.GetCurrentPlayer().URL = _playlist.Dequeue();
                _listBoxQueue.Items.Remove(_mediaPlayerForm.GetCurrentPlayer().URL);

                SetPreload();
            }
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

        private void UpdateListBox()
        {
            _listBoxQueue.Items.Clear();
            foreach (var item in _playlist)
            {
                _listBoxQueue.Items.Add(item);
            }
        }

        private void SetPreload()
        {
            var preloadedPlayer = _mediaPlayerForm.GetHiddenPlayer();
            QueueNextVideo(preloadedPlayer);
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
            UpdateListBox();
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
