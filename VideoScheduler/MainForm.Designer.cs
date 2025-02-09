namespace VideoScheduler
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            _buttonOpenPlayer = new System.Windows.Forms.Button();
            _listBoxQueue = new System.Windows.Forms.ListBox();
            _buttonOpenSchedule = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            _buttonChangeLibraryPath = new System.Windows.Forms.Button();
            _buttonReloadBlock = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // _buttonOpenPlayer
            // 
            _buttonOpenPlayer.Location = new System.Drawing.Point(14, 14);
            _buttonOpenPlayer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            _buttonOpenPlayer.Name = "_buttonOpenPlayer";
            _buttonOpenPlayer.Size = new System.Drawing.Size(152, 26);
            _buttonOpenPlayer.TabIndex = 0;
            _buttonOpenPlayer.Text = "Open Player";
            _buttonOpenPlayer.UseVisualStyleBackColor = true;
            _buttonOpenPlayer.Click += OnButtonClick;
            // 
            // _listBoxQueue
            // 
            _listBoxQueue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _listBoxQueue.FormattingEnabled = true;
            _listBoxQueue.HorizontalScrollbar = true;
            _listBoxQueue.Location = new System.Drawing.Point(173, 43);
            _listBoxQueue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            _listBoxQueue.Name = "_listBoxQueue";
            _listBoxQueue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            _listBoxQueue.ScrollAlwaysVisible = true;
            _listBoxQueue.Size = new System.Drawing.Size(524, 424);
            _listBoxQueue.TabIndex = 5;
            // 
            // _buttonOpenSchedule
            // 
            _buttonOpenSchedule.Location = new System.Drawing.Point(14, 49);
            _buttonOpenSchedule.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            _buttonOpenSchedule.Name = "_buttonOpenSchedule";
            _buttonOpenSchedule.Size = new System.Drawing.Size(152, 26);
            _buttonOpenSchedule.TabIndex = 1;
            _buttonOpenSchedule.Text = "Schedule View";
            _buttonOpenSchedule.UseVisualStyleBackColor = true;
            _buttonOpenSchedule.Click += OnButtonClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(173, 14);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(75, 15);
            label1.TabIndex = 4;
            label1.Text = "Video Queue";
            // 
            // _buttonChangeLibraryPath
            // 
            _buttonChangeLibraryPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            _buttonChangeLibraryPath.Location = new System.Drawing.Point(14, 414);
            _buttonChangeLibraryPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            _buttonChangeLibraryPath.Name = "_buttonChangeLibraryPath";
            _buttonChangeLibraryPath.Size = new System.Drawing.Size(152, 54);
            _buttonChangeLibraryPath.TabIndex = 3;
            _buttonChangeLibraryPath.Text = "Change Library Path";
            _buttonChangeLibraryPath.UseVisualStyleBackColor = true;
            _buttonChangeLibraryPath.Click += OnButtonClick;
            // 
            // _buttonReloadBlock
            // 
            _buttonReloadBlock.Location = new System.Drawing.Point(14, 82);
            _buttonReloadBlock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            _buttonReloadBlock.Name = "_buttonReloadBlock";
            _buttonReloadBlock.Size = new System.Drawing.Size(152, 26);
            _buttonReloadBlock.TabIndex = 2;
            _buttonReloadBlock.Text = "Reload Block";
            _buttonReloadBlock.UseVisualStyleBackColor = true;
            _buttonReloadBlock.Click += OnButtonClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(716, 484);
            Controls.Add(_buttonReloadBlock);
            Controls.Add(_buttonChangeLibraryPath);
            Controls.Add(label1);
            Controls.Add(_buttonOpenSchedule);
            Controls.Add(_listBoxQueue);
            Controls.Add(_buttonOpenPlayer);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Video Scheduler";
            Load += OnButtonClick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button _buttonOpenPlayer;
        private System.Windows.Forms.ListBox _listBoxQueue;
        private System.Windows.Forms.Button _buttonOpenSchedule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _buttonChangeLibraryPath;
        private System.Windows.Forms.Button _buttonReloadBlock;
    }
}

