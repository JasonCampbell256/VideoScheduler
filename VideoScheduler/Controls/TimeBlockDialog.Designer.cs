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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            _comboBoxDayOfWeek = new System.Windows.Forms.ComboBox();
            _buttonSave = new System.Windows.Forms.Button();
            _buttonCancel = new System.Windows.Forms.Button();
            _textBoxDescription = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            _dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            _dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(104, 31);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(27, 15);
            label1.TabIndex = 0;
            label1.Text = "Day";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(70, 66);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(60, 15);
            label2.TabIndex = 1;
            label2.Text = "Start Time";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(74, 97);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(56, 15);
            label3.TabIndex = 2;
            label3.Text = "End Time";
            // 
            // _comboBoxDayOfWeek
            // 
            _comboBoxDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _comboBoxDayOfWeek.FormattingEnabled = true;
            _comboBoxDayOfWeek.Location = new System.Drawing.Point(142, 31);
            _comboBoxDayOfWeek.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _comboBoxDayOfWeek.Name = "_comboBoxDayOfWeek";
            _comboBoxDayOfWeek.Size = new System.Drawing.Size(79, 23);
            _comboBoxDayOfWeek.TabIndex = 3;
            // 
            // _buttonSave
            // 
            _buttonSave.Location = new System.Drawing.Point(70, 203);
            _buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonSave.Name = "_buttonSave";
            _buttonSave.Size = new System.Drawing.Size(88, 27);
            _buttonSave.TabIndex = 6;
            _buttonSave.Text = "Save";
            _buttonSave.UseVisualStyleBackColor = true;
            _buttonSave.Click += OnButtonClick;
            // 
            // _buttonCancel
            // 
            _buttonCancel.Location = new System.Drawing.Point(178, 202);
            _buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonCancel.Name = "_buttonCancel";
            _buttonCancel.Size = new System.Drawing.Size(88, 27);
            _buttonCancel.TabIndex = 7;
            _buttonCancel.Text = "Cancel";
            _buttonCancel.UseVisualStyleBackColor = true;
            _buttonCancel.Click += OnButtonClick;
            // 
            // _textBoxDescription
            // 
            _textBoxDescription.Location = new System.Drawing.Point(142, 127);
            _textBoxDescription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _textBoxDescription.Multiline = true;
            _textBoxDescription.Name = "_textBoxDescription";
            _textBoxDescription.Size = new System.Drawing.Size(185, 67);
            _textBoxDescription.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(64, 127);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(67, 15);
            label4.TabIndex = 8;
            label4.Text = "Description";
            // 
            // _dateTimePickerStart
            // 
            _dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            _dateTimePickerStart.Location = new System.Drawing.Point(142, 66);
            _dateTimePickerStart.Name = "_dateTimePickerStart";
            _dateTimePickerStart.ShowUpDown = true;
            _dateTimePickerStart.Size = new System.Drawing.Size(124, 23);
            _dateTimePickerStart.TabIndex = 10;
            // 
            // _dateTimePickerEnd
            // 
            _dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            _dateTimePickerEnd.Location = new System.Drawing.Point(142, 95);
            _dateTimePickerEnd.Name = "_dateTimePickerEnd";
            _dateTimePickerEnd.ShowUpDown = true;
            _dateTimePickerEnd.Size = new System.Drawing.Size(124, 23);
            _dateTimePickerEnd.TabIndex = 11;
            _dateTimePickerEnd.ValueChanged += OnDateTimePickerValueChanged;
            // 
            // TimeBlockDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(342, 272);
            Controls.Add(_dateTimePickerEnd);
            Controls.Add(_dateTimePickerStart);
            Controls.Add(_textBoxDescription);
            Controls.Add(label4);
            Controls.Add(_buttonCancel);
            Controls.Add(_buttonSave);
            Controls.Add(_comboBoxDayOfWeek);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "TimeBlockDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Add or Edit Time Block";
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.DateTimePicker _dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker _dateTimePickerEnd;
    }
}