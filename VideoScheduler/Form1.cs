using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib.Asf;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler
{
    public partial class Form1 : Form
    {
        private readonly Player _mediaPlayerForm;
        private readonly Queue<string> _playlist = new Queue<string>();
        private readonly List<string> _urls = new List<string>();
        private readonly PersistenceManagers _persistenceManagers = new PersistenceManagers();
        private List<TimeBlock> _timeBlocks = new List<TimeBlock>();
        private List<IVideo> videoQueue = new List<IVideo>();
        Timer timer;

        private IVideo brbScreen;

        public void foo()
        {
            var tom2Drawing = new AttributeNode("Bumpers");
            tom2Drawing = AttributeNode.AddAttribute(tom2Drawing, "Toonami");
            tom2Drawing = AttributeNode.AddAttribute(tom2Drawing, "TOM2");
            tom2Drawing = AttributeNode.AddAttribute(tom2Drawing, "Drawings");
            _persistenceManagers.attributeTreeManager.AddNewTree(tom2Drawing);

            var toonamiIntro = new AttributeNode("Bumpers");
            toonamiIntro = AttributeNode.AddAttribute(toonamiIntro, "Toonami");
            toonamiIntro = AttributeNode.AddAttribute(toonamiIntro, "Moltar");
            toonamiIntro = AttributeNode.AddAttribute(toonamiIntro, "Toonami Intros");
            _persistenceManagers.attributeTreeManager.AddNewTree(toonamiIntro);


            var dbz = _persistenceManagers._library.GetShowEpisode("Dragon Ball Z", 1, 2);
            var dbzRun = new ShowRun("Dragon Ball Z", dbz);
            _persistenceManagers.runManager.AddOrUpdateShowRun(dbzRun);
            var sailorMoon = _persistenceManagers._library.GetShowEpisode("Sailor Moon", 1, 3);
            var sailorMoonRun = new ShowRun("Sailor Moon", sailorMoon);
            _persistenceManagers.runManager.AddOrUpdateShowRun(sailorMoonRun);

            var genericShowIntroBumperAttributes = new List<string>
{
    "Bumpers",
    "Toonami",
    "Moltar",
    "Show Intros"
};

            var dbzScheduledProgram = new BlockTemplateItem(dbzRun);
            var dbzIntroBumper = new AttributeNode(genericShowIntroBumperAttributes);
            dbzIntroBumper = AttributeNode.AddAttribute(dbzIntroBumper, "Dragon Ball Z");
            _persistenceManagers.attributeTreeManager.AddNewTree(dbzIntroBumper);
            dbzScheduledProgram.BeforeBumpersAttributeTrees.Add(dbzIntroBumper.Guid);
            _persistenceManagers.blockTemplateItemManager.AddOrUpdateBlockTemplateItem(dbzScheduledProgram);


            var sailorMoonScheduledProgram = new BlockTemplateItem(sailorMoonRun);
            var sailorMoonIntroBumper = new AttributeNode(genericShowIntroBumperAttributes);
            sailorMoonIntroBumper = AttributeNode.AddAttribute(sailorMoonIntroBumper, "Sailor Moon");
            _persistenceManagers.attributeTreeManager.AddNewTree(sailorMoonIntroBumper);
            sailorMoonScheduledProgram.BeforeBumpersAttributeTrees.Add(sailorMoonIntroBumper.Guid);
            _persistenceManagers.blockTemplateItemManager.AddOrUpdateBlockTemplateItem(sailorMoonScheduledProgram);

            var videos = new List<IVideo>();

            TimeBlock toonamiTimeBlock = new TimeBlock(DayOfWeek.Tuesday, new TimeSpan(0, 0, 0), new TimeSpan(0, 30, 00));
            SchedulableBlock toonamiBlock = new SchedulableBlock();
            toonamiBlock.Description = "Toonami";
            toonamiBlock.AddContent(toonamiIntro);
            toonamiBlock.AddContent(dbzScheduledProgram);
            toonamiBlock.AddContent(tom2Drawing);
            toonamiBlock.AddContent(sailorMoonScheduledProgram);
            _persistenceManagers.schedulableBlockManager.AddOrUpdateSchedulableBlock(toonamiBlock);
            

            toonamiTimeBlock.ContentGuids.Add(toonamiBlock.Guid);
            _persistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(toonamiTimeBlock);

            videos = _persistenceManagers._picker.GetVideos(toonamiBlock);


            _persistenceManagers.attributeTreeManager.SaveTrees();
            foreach (var video in videos)
            {
                //Console.WriteLine(video.FilePath);
                _urls.Add(video.FilePath);
            }
            // Console.ReadLine();
        }

        public void loadschedule()
        {
            _timeBlocks = _persistenceManagers.timeBlockManager.GetTimeBlocks(DateTime.Now.DayOfWeek);
        }


        public Form1()
        {
            InitializeComponent();
            _mediaPlayerForm = new Player();
            var brbNode = new AttributeNode("BRB");
            brbScreen = _persistenceManagers._picker.GetVideos(brbNode).First();
            AddToQueue(brbScreen.FilePath);
            Play();
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
                AddToQueue(brbScreen.FilePath);
            }
            var url = _playlist.Dequeue();
            player.URL = url;
            player.Visible = false;

        }

        private void UpdateTitleLabels()
        {
            //_labelTitle.Text = _mediaPlayerForm.GetCurrentPlayer().URL;
            //_labelNextTitle.Text = _mediaPlayerForm.GetHiddenPlayer().URL;
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
            else if (e.newState == 3)
            {
                UpdateTitleLabels();
            }
            else if (sender.Equals(_mediaPlayerForm.GetCurrentPlayer()) && e.newState == 8)
            {
                /*if (_commercialBreak)
                {
                    _commercialBreak = false;
                    _mediaPlayerForm.GetCurrentPlayer().Ctlcontrols.pause();
                    _mediaPlayerForm.GetCurrentPlayer().Ctlcontrols.currentPosition = 0;
                    _mediaPlayerForm.SwitchPlayers();
                    _mediaPlayerForm.GetCurrentPlayer().Ctlcontrols.play();
                }*/
                NextVideo();
            }
            UpdateTitleLabels();
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
            UpdateTitleLabels();
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
            else if (sender.Equals(_buttonReloadSchedule))
            {
                loadschedule();
            }
        }
    }
}
