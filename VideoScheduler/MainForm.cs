using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VideoScheduler.Controls;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler
{
    public partial class MainForm : Form
    {
        private VlcPlayer _VlcPlayerForm;
        private readonly Queue<string> _playlist = new Queue<string>();
        private readonly PersistenceManagers _persistenceManagers;
        private List<TimeBlock> _timeBlocks = new List<TimeBlock>();
        private TimeBlock currentTimeBlock = null;
        Timer timer;

        public void loadschedule()
        {
            _timeBlocks = _persistenceManagers.timeBlockManager.GetTimeBlocks(DateTime.Now.DayOfWeek);
        }

        public string GetNextVideo()
        {
            if (_playlist.Count > 0)
            {
                return _playlist.Dequeue();
            }
            return String.Empty;
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
                    libraryPath = PersistenceManagers.GetFilePath();
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
            _VlcPlayerForm = new VlcPlayer(this);
            loadschedule();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += CheckTimeBlocks;
            timer.Start();

            // If the user goes through the folder select flow the MainForm may render behind other open windows
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
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
            if (!_VlcPlayerForm.Visible)
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
                    
                    PlayVlc((long)timeSpanToStart.Duration().TotalMilliseconds);
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

        private void PlayVlc(long? startTime)
        {
            if (_playlist.Count != 0)
            {
                if (_VlcPlayerForm == null && _playlist.Count > 0)
                {
                    OpenVlcPlayer();
                }
                var url = _playlist.Dequeue();
                _VlcPlayerForm.PlayVideo(url, startTime);
                _listBoxQueue.Items.RemoveAt(0);
            }
        }

        private void AddToQueue(string url)
        {
            _playlist.Enqueue(url);
            _listBoxQueue.Items.Add(url);
        }

        private void UpdateListBox()
        {
            _listBoxQueue.Items.Clear();
            foreach (var item in _playlist)
            {
                _listBoxQueue.Items.Add(item);
            }
        }

        private void OpenVlcPlayer()
        {
            if (_VlcPlayerForm == null || _VlcPlayerForm.IsDisposed || !_VlcPlayerForm.Visible)
            {
                _VlcPlayerForm = new VlcPlayer(this);
            }
            ClearQueue();
            _VlcPlayerForm.Show();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonOpenPlayer))
            {
                if (_VlcPlayerForm.Visible)
                {
                    return;
                }
                OpenVlcPlayer();
            }
            else if (sender.Equals(_buttonOpenSchedule))
            {
                ScheduleControl scheduleControl = new ScheduleControl(_persistenceManagers);
                scheduleControl.Show();
            }
            else if (sender.Equals(_buttonChangeLibraryPath))
            {
                ChangeLibraryPath();
            }
            else if (sender.Equals(_buttonReloadBlock))
            {
                ClearQueue();
                loadschedule();
                OpenVlcPlayer();
            }
        }
    }
}
