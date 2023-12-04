namespace VideoScheduler
{
    partial class TimeBlockDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._comboBoxDayOfWeek = new System.Windows.Forms.ComboBox();
            this._textBoxStartTime = new System.Windows.Forms.TextBox();
            this._textBoxEndTime = new System.Windows.Forms.TextBox();
            this._buttonSave = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._textBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Day";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "End Time";
            // 
            // _comboBoxDayOfWeek
            // 
            this._comboBoxDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxDayOfWeek.FormattingEnabled = true;
            this._comboBoxDayOfWeek.Location = new System.Drawing.Point(122, 27);
            this._comboBoxDayOfWeek.Name = "_comboBoxDayOfWeek";
            this._comboBoxDayOfWeek.Size = new System.Drawing.Size(68, 21);
            this._comboBoxDayOfWeek.TabIndex = 3;
            // 
            // _textBoxStartTime
            // 
            this._textBoxStartTime.Location = new System.Drawing.Point(122, 57);
            this._textBoxStartTime.Name = "_textBoxStartTime";
            this._textBoxStartTime.Size = new System.Drawing.Size(68, 20);
            this._textBoxStartTime.TabIndex = 4;
            // 
            // _textBoxEndTime
            // 
            this._textBoxEndTime.Location = new System.Drawing.Point(122, 84);
            this._textBoxEndTime.Name = "_textBoxEndTime";
            this._textBoxEndTime.Size = new System.Drawing.Size(68, 20);
            this._textBoxEndTime.TabIndex = 5;
            // 
            // _buttonSave
            // 
            this._buttonSave.Location = new System.Drawing.Point(60, 176);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(75, 23);
            this._buttonSave.TabIndex = 6;
            this._buttonSave.Text = "Save";
            this._buttonSave.UseVisualStyleBackColor = true;
            this._buttonSave.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Location = new System.Drawing.Point(153, 175);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 7;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _textBoxDescription
            // 
            this._textBoxDescription.Location = new System.Drawing.Point(122, 110);
            this._textBoxDescription.Multiline = true;
            this._textBoxDescription.Name = "_textBoxDescription";
            this._textBoxDescription.Size = new System.Drawing.Size(159, 59);
            this._textBoxDescription.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Description";
            // 
            // TimeBlockDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 236);
            this.Controls.Add(this._textBoxDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonSave);
            this.Controls.Add(this._textBoxEndTime);
            this.Controls.Add(this._textBoxStartTime);
            this.Controls.Add(this._comboBoxDayOfWeek);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TimeBlockDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Add or Edit Time Block";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _comboBoxDayOfWeek;
        private System.Windows.Forms.TextBox _textBoxStartTime;
        private System.Windows.Forms.TextBox _textBoxEndTime;
        private System.Windows.Forms.Button _buttonSave;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.TextBox _textBoxDescription;
        private System.Windows.Forms.Label label4;
    }
}