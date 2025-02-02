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
        private bool _playing = false;
        private TransparentPanel _transparentPanel;
        private BlackPanel _blackPanel;

        public VlcPlayer(MainForm mainForm)
        {
            InitializeComponent();

            _mainForm = mainForm;

            LibVLCSharp.Shared.Core.Initialize();

            _transparentPanel = new TransparentPanel();
            AddPanels(_transparentPanel);
            _blackPanel = new BlackPanel();
            AddPanels(_blackPanel);
            SendPanelToBackSafe(_blackPanel);
        }

        private void AddPanels(Panel panel)
        {
            this.Controls.Add(panel);
            this.Dock = DockStyle.Fill;
            this.DoubleClick += (sender, args) => OnDoubleClick(sender, args);
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

        public void PlayVideo(string filePath, long? position)
        {
            if (_playing)
            {
                //In this case, we are loading a new timeblock or reloading the current one
                //We want to dispose of the player here so it won't continue playing
                //But we only want to do it in this case, because more dipose logic
                //Occurs later in this method.
                foreach (Control control in this.Controls)
                {
                    if (control is VideoView videoView && videoView.MediaPlayer != null)
                    {
                        videoView.MediaPlayer.Stop();
                        videoView.MediaPlayer.Dispose();
                    }
                }
            }
            
            //These debug lines are left in because it's really easy to mess up the threading here
            Console.WriteLine($"PlayVideo method called. Playing {filePath}");
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    return;
                }
                Console.WriteLine($"File found: {filePath}");

                // Ensure cross-thread safety when accessing controls
                this.Invoke(new Action(() =>
                {
                    // Cleanup any existing instances
                    foreach (Control control in this.Controls)
                    {
                        if (control is VideoView existingView)
                        {
                            Console.WriteLine("Removing existing VideoView");
                            this.Controls.Remove(existingView);
                            existingView.Dispose();
                            break;
                        }
                    }
                }));

                // Create LibVLC instance
                using (var libVlc = new LibVLC())
                {
                    // Create a new VideoView instance
                    var videoView = new VideoView
                    {
                        Dock = DockStyle.Fill
                    };
                    this.Invoke(new Action(() =>
                    {
                        this.Controls.Add(videoView);
                        Console.WriteLine("New VideoView created and added to form");
                    }));

                    // Create a new MediaPlayer
                    var mediaPlayer = new MediaPlayer(libVlc);
 
                    this.Invoke(new Action(() =>
                    {
                        videoView.MediaPlayer = mediaPlayer;
                        Console.WriteLine("New MediaPlayer created and bound to VideoView");
                    }));

                    // Subscribe to events
                    mediaPlayer.Playing += (sender, args) => { SendPanelToBackSafe(_blackPanel); Console.WriteLine("Playback started."); _playing = true; };
                    mediaPlayer.EndReached += (sender, args) => { BringPanelToFrontSafe(_blackPanel); _playing = false; PlayVideo(_mainForm.GetNextVideo(), null); };
                    mediaPlayer.EncounteredError += (sender, args) => { _playing = false; Console.WriteLine("Playback error occurred."); };

                    // Attach media and start playback
                    using (var media = new Media(libVlc, filePath, FromType.FromPath))
                    {
                        mediaPlayer.Media = media;

                        Console.WriteLine("Starting playback...");
                        if (!mediaPlayer.Play())
                        {
                            Console.WriteLine("Playback failed to start.");
                            return;
                        }

                        Console.WriteLine($"Playing: {filePath}");

                        this.Invoke(new Action(() =>
                        {
                            Console.WriteLine($"Seeking to position: {position ?? 0}");
                            mediaPlayer.Time = position ?? 0;
                        }));
                        BringPanelToFrontSafe(_transparentPanel);
                    }
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing video: {ex.Message}");
            }
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
