namespace VideoScheduler.Controls
{
    partial class VlcPlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            _toolStripMenuItemFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            logoSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _toolStripMenuItemFullScreen, logoSettingsToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(170, 48);
            // 
            // _toolStripMenuItemFullScreen
            // 
            _toolStripMenuItemFullScreen.Name = "_toolStripMenuItemFullScreen";
            _toolStripMenuItemFullScreen.Size = new System.Drawing.Size(169, 22);
            _toolStripMenuItemFullScreen.Text = "Toggle Full Screen";
            _toolStripMenuItemFullScreen.Click += OnToolStripMenuItemClick;
            // 
            // logoSettingsToolStripMenuItem
            // 
            logoSettingsToolStripMenuItem.Name = "logoSettingsToolStripMenuItem";
            logoSettingsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            logoSettingsToolStripMenuItem.Text = "Logo Settings";
            logoSettingsToolStripMenuItem.Click += OnToolStripMenuItemClick;
            // 
            // VlcPlayer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.Black;
            ClientSize = new System.Drawing.Size(1264, 681);
            ContextMenuStrip = contextMenuStrip1;
            Margin = new System.Windows.Forms.Padding(2);
            Name = "VlcPlayer";
            Text = "VlcPlayer";
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemFullScreen;
        #endregion

        private System.Windows.Forms.ToolStripMenuItem logoSettingsToolStripMenuItem;
    }
}