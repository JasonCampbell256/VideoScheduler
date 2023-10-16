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
            this._buttonReloadSchedule = new System.Windows.Forms.Button();
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
            this._listBoxQueue.Location = new System.Drawing.Point(148, 12);
            this._listBoxQueue.Name = "_listBoxQueue";
            this._listBoxQueue.Size = new System.Drawing.Size(449, 381);
            this._listBoxQueue.TabIndex = 1;
            // 
            // _buttonOpenSchedule
            // 
            this._buttonOpenSchedule.Location = new System.Drawing.Point(12, 42);
            this._buttonOpenSchedule.Name = "_buttonOpenSchedule";
            this._buttonOpenSchedule.Size = new System.Drawing.Size(75, 23);
            this._buttonOpenSchedule.TabIndex = 2;
            this._buttonOpenSchedule.Text = "Schedule View";
            this._buttonOpenSchedule.UseVisualStyleBackColor = true;
            this._buttonOpenSchedule.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonReloadSchedule
            // 
            this._buttonReloadSchedule.Location = new System.Drawing.Point(12, 72);
            this._buttonReloadSchedule.Name = "_buttonReloadSchedule";
            this._buttonReloadSchedule.Size = new System.Drawing.Size(75, 23);
            this._buttonReloadSchedule.TabIndex = 3;
            this._buttonReloadSchedule.Text = "Reload Schedule";
            this._buttonReloadSchedule.UseVisualStyleBackColor = true;
            this._buttonReloadSchedule.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 419);
            this.Controls.Add(this._buttonReloadSchedule);
            this.Controls.Add(this._buttonOpenSchedule);
            this.Controls.Add(this._listBoxQueue);
            this.Controls.Add(this._buttonOpenPlayer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _buttonOpenPlayer;
        private System.Windows.Forms.ListBox _listBoxQueue;
        private System.Windows.Forms.Button _buttonOpenSchedule;
        private System.Windows.Forms.Button _buttonReloadSchedule;
    }
}

