using AxWMPLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace VideoScheduler
{
    public partial class Player : Form
    {
        private AxWindowsMediaPlayer _currentPlayer;
        private AxWindowsMediaPlayer _hiddenPlayer;

        private bool isFullScreen = false;

        public AxWindowsMediaPlayer GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        public AxWindowsMediaPlayer GetHiddenPlayer()
        {
            return _hiddenPlayer;
        }

        public Player()
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            InitializeComponent();
            SetMediaPlayerSettings(axWindowsMediaPlayer1);
            SetMediaPlayerSettings(axWindowsMediaPlayer2);
            _currentPlayer = axWindowsMediaPlayer1;
            _hiddenPlayer = axWindowsMediaPlayer2;
        }

        public void SwitchPlayers()
        {
            var oldCurrent = _currentPlayer;
            var newCurrent = _hiddenPlayer;

            _currentPlayer = newCurrent;

            _hiddenPlayer = oldCurrent;
            _hiddenPlayer.Ctlcontrols.stop();
            _hiddenPlayer.Visible = false;
            _hiddenPlayer.close();

            _currentPlayer.Dock = DockStyle.Fill;
            _currentPlayer.Visible = true;

            _currentPlayer.Width = this.ClientSize.Width;
            _currentPlayer.Height = this.ClientSize.Height;

            _currentPlayer.Ctlcontrols.play();
        }
        
        private void SetMediaPlayerSettings(AxWindowsMediaPlayer player)
        {
            player.uiMode = "none";
            player.Margin = new Padding(0);
            player.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            player.Location = new Point(0, 0);
            player.stretchToFit = true;
            player.settings.volume = 65;
            player.enableContextMenu = false;
        }

        public void ShowControls()
        {
            if (!String.Equals(_currentPlayer.uiMode, "none"))
            {
                _currentPlayer.uiMode = "none";
            }
            else
            {
                _currentPlayer.uiMode = "full";
            }
        }

        public void ToggleFullScreen()
        {
            if (isFullScreen)
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
            isFullScreen = !isFullScreen;
        }

        private void OnToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(sender.Equals(_toolStripMenuItemFullScreen))
            {
                ToggleFullScreen();
            }
        }

        private void OnMouseUp(object sender, _WMPOCXEvents_MouseUpEvent e)
        {
            // We can't override the default context menu for the axMediaPlayer controls
            // So we implement it here instead
            if (e.nButton == 2)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
    }
}
