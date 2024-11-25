using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler
{
    public partial class MainForm : Form
    {
        private Player _mediaPlayerForm;
        private readonly Queue<string> _playlist = new Queue<string>();
        private readonly PersistenceManagers _persistenceManagers;
        private List<TimeBlock> _timeBlocks = new List<TimeBlock>();
        private TimeBlock currentTimeBlock = null;
        Timer timer;

        public void loadschedule()
        {
            _timeBlocks = _persistenceManagers.timeBlockManager.GetTimeBlocks(DateTime.Now.DayOfWeek);
        }


        public MainForm()
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            InitializeComponent();
            var libraryPath = PersistenceManagers.GetFilePath();

            if (string.IsNullOrEmpty(libraryPath) )
            {
                MessageBox.Show("Please select your library folder.");
                var folderBrowserDialog = new FolderBrowserDialog();
                var result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PersistenceManagers.SetFilePath(folderBrowserDialog.SelectedPath);
                } else
                {
                    NotifyAndExitForPathNotFound();
                }
            }

            if (!Directory.Exists(libraryPath))
            {
                var pathNotFoundMessageBox = MessageBox.Show("The path to the library could not be found. Would you like to change the path?", "Library Path Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (pathNotFoundMessageBox == DialogResult.Yes)
                {
                    ChangeLibraryPath();
                }
                libraryPath = PersistenceManagers.GetFilePath();
                if (!Directory.Exists(libraryPath))
                {
                    NotifyAndExitForPathNotFound();
                }
            }
            _persistenceManagers = new PersistenceManagers();
            _mediaPlayerForm = new Player();
            loadschedule();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += CheckTimeBlocks;
            timer.Start();
        }

        private void NotifyAndExitForPathNotFound()
        {
            MessageBox.Show("Path to library could not be found. The application will now close.", "Path not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }

        private void ChangeLibraryPath()
        {
            var confirmationDialog = MessageBox.Show("Updating the library may cause data to be lost! Are you sure you want to update the library path?", "You Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmationDialog == DialogResult.No)
            {
                return;
            }
            var folderBrowserDialog = new FolderBrowserDialog();
            var result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                PersistenceManagers.SetFilePath(folderBrowserDialog.SelectedPath);
                Application.Restart();
            }
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
                if (truncatedCurrentTime >= timeBlock.StartTime && truncatedCurrentTime < timeBlock.EndTime)
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
                    var firstVideoFound = false;

                    foreach (var video in videos)
                    {
                        var videoDuration = VideoPicker.GetDuration(video.FilePath);
                        timespanCounter += videoDuration;
                        if (timespanCounter >= timeElapsed)
                        {
                            AddToQueue(video.FilePath);
                            if (!firstVideoFound)
                            {
                                firstVideoFound = true;
                                timeSpanToStart = timeElapsed - (timespanCounter - videoDuration);
                            }
                        }
                    }
                    
                    Play(timeSpanToStart.Duration());
                    break;
                }


            }
        }

        private void ClearQueue()
        {
            currentTimeBlock = null;
            _listBoxQueue.Items.Clear();
            _playlist.Clear();
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
            if (_mediaPlayerForm == null || _mediaPlayerForm.IsDisposed || !_mediaPlayerForm.Visible)
            {
                _mediaPlayerForm = new Player();
            }
            ClearQueue();
            _mediaPlayerForm.Show();
            _mediaPlayerForm.GetCurrentPlayer().PlayStateChange += Player_PlayStateChange;
            QueueNextVideo(_mediaPlayerForm.GetCurrentPlayer());
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonOpenPlayer))
            {
                if (_mediaPlayerForm.Visible)
                {
                    return;
                }
                OpenPlayer();
                _mediaPlayerForm.GetHiddenPlayer().PlayStateChange += Player_PlayStateChange;
            }
            else if (sender.Equals(_buttonOpenSchedule))
            {
                ScheduleControl scheduleControl = new ScheduleControl(_persistenceManagers);
                scheduleControl.Show();
            } else if (sender.Equals(_buttonChangeLibraryPath))
            {
                ChangeLibraryPath();
            } else if (sender.Equals(_buttonReloadBlock))
            {
                ClearQueue();
                loadschedule();
            }
        }
    }
}
