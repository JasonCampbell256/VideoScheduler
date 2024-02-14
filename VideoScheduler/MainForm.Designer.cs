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
            this._buttonOpenPlayer = new System.Windows.Forms.Button();
            this._listBoxQueue = new System.Windows.Forms.ListBox();
            this._buttonOpenSchedule = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._buttonChangeLibraryPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _buttonOpenPlayer
            // 
            this._buttonOpenPlayer.Location = new System.Drawing.Point(12, 12);
            this._buttonOpenPlayer.Name = "_buttonOpenPlayer";
            this._buttonOpenPlayer.Size = new System.Drawing.Size(75, 23);
            this._buttonOpenPlayer.TabIndex = 0;
            this._buttonOpenPlayer.Text = "Open Player";
            this._buttonOpenPlayer.UseVisualStyleBackColor = true;
            this._buttonOpenPlayer.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _listBoxQueue
            // 
            this._listBoxQueue.FormattingEnabled = true;
            this._listBoxQueue.Location = new System.Drawing.Point(148, 25);
            this._listBoxQueue.Name = "_listBoxQueue";
            this._listBoxQueue.Size = new System.Drawing.Size(449, 368);
            this._listBoxQueue.TabIndex = 3;
            // 
            // _buttonOpenSchedule
            // 
            this._buttonOpenSchedule.Location = new System.Drawing.Point(12, 42);
            this._buttonOpenSchedule.Name = "_buttonOpenSchedule";
            this._buttonOpenSchedule.Size = new System.Drawing.Size(75, 23);
            this._buttonOpenSchedule.TabIndex = 1;
            this._buttonOpenSchedule.Text = "Schedule View";
            this._buttonOpenSchedule.UseVisualStyleBackColor = true;
            this._buttonOpenSchedule.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Video Queue";
            // 
            // _buttonChangeLibraryPath
            // 
            this._buttonChangeLibraryPath.Location = new System.Drawing.Point(12, 346);
            this._buttonChangeLibraryPath.Name = "_buttonChangeLibraryPath";
            this._buttonChangeLibraryPath.Size = new System.Drawing.Size(75, 47);
            this._buttonChangeLibraryPath.TabIndex = 4;
            this._buttonChangeLibraryPath.Text = "Change Library Path";
            this._buttonChangeLibraryPath.UseVisualStyleBackColor = true;
            this._buttonChangeLibraryPath.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 419);
            this.Controls.Add(this._buttonChangeLibraryPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._buttonOpenSchedule);
            this.Controls.Add(this._listBoxQueue);
            this.Controls.Add(this._buttonOpenPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Video Scheduler";
            this.Load += new System.EventHandler(this.OnButtonClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonOpenPlayer;
        private System.Windows.Forms.ListBox _listBoxQueue;
        private System.Windows.Forms.Button _buttonOpenSchedule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _buttonChangeLibraryPath;
    }
}

