using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoScheduler.Controls
{
    public partial class PlayerSettingsDialog : Form
    {
        private MediaPlayer _mediaPlayer;
        private bool _supressEvents = false;
        public PlayerSettings PlayerSettings { get; set; }


        public PlayerSettingsDialog(MediaPlayer mediaPlayer)
        {
            InitializeComponent();
            _mediaPlayer = mediaPlayer;
            PlayerSettings = new PlayerSettings(mediaPlayer);

            _supressEvents = true;
            checkBox1.Checked = _mediaPlayer.LogoInt(VideoLogoOption.Enable) > 0;
            _numericUpDownX.Value = _mediaPlayer.LogoInt(VideoLogoOption.X) > 0 ? _mediaPlayer.LogoInt(VideoLogoOption.X) : 0;
            _numericUpDownY.Value = _mediaPlayer.LogoInt(VideoLogoOption.Y) > 0 ? _mediaPlayer.LogoInt(VideoLogoOption.Y) : 0;
            _trackBarOpacity.Value = _mediaPlayer.LogoInt(VideoLogoOption.Opacity);

            UpdateSettings();
            _supressEvents = false;
        }

        private void UpdateSettings()
        {
            _labelOpacityLevel.Text = _trackBarOpacity.Value.ToString();

            PlayerSettings.LogoEnabled = checkBox1.Checked;
            PlayerSettings.LogoX = _numericUpDownX.Value > 0 ? (int)_numericUpDownX.Value : 0;
            PlayerSettings.LogoY = _numericUpDownY.Value > 0 ? (int)_numericUpDownY.Value : 0;
            PlayerSettings.LogoOpacity = _trackBarOpacity.Value > 0 ? (int)_trackBarOpacity.Value : 0;
            PlayerSettings.ApplySettings(_mediaPlayer);
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            if (_supressEvents)
                return;
            UpdateSettings();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {

        }
    }

}
