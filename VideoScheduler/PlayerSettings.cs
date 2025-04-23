using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler
{
    public class PlayerSettings
    {
        public bool LogoEnabled { get; set; }
        public int LogoX { get; set; }
        public int LogoY { get; set; }
        public int LogoOpacity { get; set; }

        public PlayerSettings(MediaPlayer mediaPlayer)
        {
            LogoEnabled = mediaPlayer.LogoInt(VideoLogoOption.Enable) > 0;
            LogoX = mediaPlayer.LogoInt(VideoLogoOption.X);
            LogoY = mediaPlayer.LogoInt(VideoLogoOption.Y);
            LogoOpacity = mediaPlayer.LogoInt(VideoLogoOption.Opacity);
        }

        public void ApplySettings(MediaPlayer mediaPlayer)
        {
            mediaPlayer.SetLogoInt(VideoLogoOption.Enable, LogoEnabled ? 1 : 0);
            mediaPlayer.SetLogoInt(VideoLogoOption.X, LogoX);
            mediaPlayer.SetLogoInt(VideoLogoOption.Y, LogoY);
            mediaPlayer.SetLogoInt(VideoLogoOption.Opacity, LogoOpacity);
        }
    }
}
