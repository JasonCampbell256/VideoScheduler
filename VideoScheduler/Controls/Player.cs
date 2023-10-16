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

namespace VideoScheduler
{
    public partial class Player : Form
    {
        private const int PLAYER_WIDTH = 1280;
        private const int PLAYER_HEIGHT = 720;

        private AxWindowsMediaPlayer _currentPlayer;
        private AxWindowsMediaPlayer _hiddenPlayer;

        public AxWindowsMediaPlayer GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        public AxWindowsMediaPlayer GetHiddenPlayer()
        {
            return _hiddenPlayer;
        }

        public void SwitchPlayers(bool isCommercialBreak = false)
        {
            var oldCurrent = _currentPlayer;
            var newCurrent = _hiddenPlayer;
            _currentPlayer = newCurrent;
            _hiddenPlayer = oldCurrent;

            if (isCommercialBreak)
            {
                _hiddenPlayer.Ctlcontrols.pause();
            }
            else
            {
                _hiddenPlayer.Ctlcontrols.stop();
            }
            _hiddenPlayer.Visible = false;
            _hiddenPlayer.close();
            _currentPlayer.Visible = true;
            _currentPlayer.Ctlcontrols.play();

        }

        public Player()
        {
            InitializeComponent();
            SetMediaPlayerSettings(axWindowsMediaPlayer1);
            SetMediaPlayerSettings(axWindowsMediaPlayer2);
            _currentPlayer = axWindowsMediaPlayer1;
            _hiddenPlayer = axWindowsMediaPlayer2;
        }

        private void SetMediaPlayerSettings(AxWindowsMediaPlayer player)
        {
            player.uiMode = "none";
            player.Width = PLAYER_WIDTH;
            player.Height = PLAYER_HEIGHT;
            player.Margin = new Padding(0);
            player.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            player.Location = new Point(0, 0);
            player.stretchToFit = true;
            player.settings.volume = 65;
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
    }
}
