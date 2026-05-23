#if WINDOWS
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace VideoScheduler
{
    public class TrayApplicationContext : ApplicationContext
    {
        private NotifyIcon _notifyIcon;
        private string _url;

        public TrayApplicationContext(string url)
        {
            _url = url;
            _notifyIcon = new NotifyIcon
            {
                Icon = new Icon("icon.ico"),
                Visible = true,
                Text = "VideoScheduler"
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Open VideoScheduler", null, (s, e) => OpenBrowser());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("Exit", null, (s, e) => Exit());

            _notifyIcon.ContextMenuStrip = contextMenu;
            _notifyIcon.DoubleClick += (s, e) => OpenBrowser();

            OpenBrowser();
        }

        private void OpenBrowser()
        {
            try
            {
                Process.Start(new ProcessStartInfo(_url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening browser: {ex.Message}");
            }
        }

        private void Exit()
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
#endif
