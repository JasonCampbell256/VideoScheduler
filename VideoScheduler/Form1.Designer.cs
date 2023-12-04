namespace VideoScheduler
{
    partial class Form1
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
            this._buttonOpenPlayer = new System.Windows.Forms.Button();
            this._listBoxQueue = new System.Windows.Forms.ListBox();
            this._buttonOpenSchedule = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 419);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._buttonOpenSchedule);
            this.Controls.Add(this._listBoxQueue);
            this.Controls.Add(this._buttonOpenPlayer);
            this.Name = "Form1";
            this.Text = "Video Scheduler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonOpenPlayer;
        private System.Windows.Forms.ListBox _listBoxQueue;
        private System.Windows.Forms.Button _buttonOpenSchedule;
        private System.Windows.Forms.Label label1;
    }
}

