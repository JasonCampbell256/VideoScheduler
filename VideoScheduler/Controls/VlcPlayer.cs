using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace VideoScheduler.Controls
{

    public partial class VlcPlayer : Form
    {
        private MainForm _mainForm;
        private bool _isFullscreen = false;
        private TransparentPanel _transparentPanel;
        private BlackPanel _blackPanel;
        private LibVLC _libVLC; 
        private MediaPlayer _mediaPlayer;
        private Media _currentMedia;
        private Media _hiddenMedia;

        public VlcPlayer(MainForm mainForm)
        {
            InitializeComponent();

            _mainForm = mainForm;

            LibVLCSharp.Shared.Core.Initialize();
            _libVLC = new LibVLC();

            _mediaPlayer = new MediaPlayer(_libVLC);
            //TODO retrieve logo options from settings
            _mediaPlayer.SetLogoString(VideoLogoOption.File, Path.Combine(Application.StartupPath, "logo2.png"));
            _mediaPlayer.SetLogoInt(VideoLogoOption.Enable, 1);

            _mediaPlayer.Playing += (sender, args) => { SendPanelToBackSafe(_blackPanel); };
            _mediaPlayer.EndReached += (sender, args) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    BringPanelToFrontSafe(_blackPanel);
                     PlayNextVideo();
                }));
            };

            var videoView = new VideoView
            {
                Dock = DockStyle.Fill,
                MediaPlayer = _mediaPlayer
            };

            this.Controls.Add(videoView);

            _transparentPanel = new TransparentPanel();
            AddPanels(_transparentPanel);
            _blackPanel = new BlackPanel();
            AddPanels(_blackPanel);
            SendPanelToBackSafe(_blackPanel);
        }

        private void SwitchMedia()
        {
            var oldCurrent = _currentMedia;
            var newCurrent = _hiddenMedia;

            _currentMedia = newCurrent;
            _hiddenMedia = oldCurrent;
        }

        public void PreloadNextVideo()
        {
            var nextVideo = _mainForm.GetNextVideo();
            if (!string.IsNullOrEmpty(nextVideo))
            {
                _hiddenMedia = new Media(_libVLC, nextVideo);
            }
        }

        public void PlayFirstVideo(string filePath, long? position)
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Media?.Dispose();
            _currentMedia = new Media(_libVLC, filePath, FromType.FromPath);
            _mediaPlayer.Media = _currentMedia;
            _mediaPlayer.Play();

            this.Invoke(new Action(() =>
            {
                _mediaPlayer.Time = position ?? 0;
            }));

            _mainForm.UpdateListBox();
            BringPanelToFrontSafe(_transparentPanel);
            PreloadNextVideo();
        }

        public void PlayNextVideo()
        {
            _mediaPlayer.Stop();
            if (_currentMedia == null) return;
            SwitchMedia();
            _hiddenMedia?.Dispose();
            _mediaPlayer.Media = _currentMedia;
            _mediaPlayer.Play();
            _mainForm.UpdateListBox();
            BringPanelToFrontSafe(_transparentPanel);
            PreloadNextVideo();
        }

        private void AddPanels(Panel panel)
        {
            this.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
            panel.DoubleClick += (sender, args) => OnDoubleClick(sender, args);
        }

        private void BringPanelToFrontSafe(Panel panel)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new Action(() => panel.BringToFront()));
            }
            else
            {
                panel.BringToFront();
            }
        }

        private void SendPanelToBackSafe(Panel panel)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new Action(() => panel.SendToBack()));
            }
            else
            {
                panel.SendToBack();
            }
        }


        public void ToggleFullScreen()
        {
            if (_isFullscreen)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState |= FormWindowState.Maximized;
            }
            _isFullscreen = !_isFullscreen;
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            ToggleFullScreen();
        }

        private void OnToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (sender.Equals(_toolStripMenuItemFullScreen))
            {
                ToggleFullScreen();
            }
            else if (sender.Equals(logoSettingsToolStripMenuItem))
            {
                var playerSettings = new PlayerSettings(_mediaPlayer);
                var VideoSettingsDialog = new PlayerSettingsDialog(_mediaPlayer);
                if (VideoSettingsDialog.ShowDialog() == DialogResult.OK)
                {
                    // TODO save current settings
                }
                else
                {
                    // Revert player to previous settings
                    playerSettings.ApplySettings(_mediaPlayer);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            foreach (Control control in this.Controls)
            {
                if (control is VideoView videoView && videoView.MediaPlayer != null)
                {
                    videoView.MediaPlayer.Stop();
                    videoView.MediaPlayer.Dispose();
                }
            }
        }
    }

    /// <summary>
    /// This 'nearly' transparent panel allows us to handle click events.
    /// It won't work if the panel is 100% transparent, but this should be imperceptible.
    /// </summary>
    public class TransparentPanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT

                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e) =>
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
    }

    /// <summary>
    /// Covers the screen when a video ends so it isn't hanging at the last frame
    /// </summary>
    public class BlackPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e) =>
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), this.ClientRectangle);
    }
}
