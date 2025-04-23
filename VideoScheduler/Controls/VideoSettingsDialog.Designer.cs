namespace VideoScheduler.Controls
{
    partial class PlayerSettingsDialog
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
            _numericUpDownX = new System.Windows.Forms.NumericUpDown();
            _numericUpDownY = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            button2 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            _buttonBrowse = new System.Windows.Forms.Button();
            _labelOpacityLevel = new System.Windows.Forms.Label();
            _trackBarOpacity = new System.Windows.Forms.TrackBar();
            textBox1 = new System.Windows.Forms.TextBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownY).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_trackBarOpacity).BeginInit();
            SuspendLayout();
            // 
            // _numericUpDownX
            // 
            _numericUpDownX.Location = new System.Drawing.Point(75, 77);
            _numericUpDownX.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            _numericUpDownX.Name = "_numericUpDownX";
            _numericUpDownX.Size = new System.Drawing.Size(60, 23);
            _numericUpDownX.TabIndex = 0;
            _numericUpDownX.ValueChanged += OnValueChanged;
            // 
            // _numericUpDownY
            // 
            _numericUpDownY.Location = new System.Drawing.Point(75, 106);
            _numericUpDownY.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            _numericUpDownY.Name = "_numericUpDownY";
            _numericUpDownY.Size = new System.Drawing.Size(60, 23);
            _numericUpDownY.TabIndex = 1;
            _numericUpDownY.ValueChanged += OnValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 79);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(60, 15);
            label2.TabIndex = 4;
            label2.Text = "X Position";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 108);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(60, 15);
            label3.TabIndex = 5;
            label3.Text = "Y Position";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 142);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(48, 15);
            label4.TabIndex = 6;
            label4.Text = "Opacity";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 48);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(34, 15);
            label6.TabIndex = 8;
            label6.Text = "Logo";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(_buttonBrowse);
            groupBox1.Controls.Add(_labelOpacityLevel);
            groupBox1.Controls.Add(_trackBarOpacity);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(_numericUpDownX);
            groupBox1.Controls.Add(_numericUpDownY);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(366, 216);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Logo Settings";
            // 
            // button2
            // 
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            button2.Location = new System.Drawing.Point(280, 187);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(80, 23);
            button2.TabIndex = 15;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Location = new System.Drawing.Point(194, 187);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(80, 23);
            button1.TabIndex = 14;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            // 
            // _buttonBrowse
            // 
            _buttonBrowse.Location = new System.Drawing.Point(280, 48);
            _buttonBrowse.Name = "_buttonBrowse";
            _buttonBrowse.Size = new System.Drawing.Size(80, 23);
            _buttonBrowse.TabIndex = 13;
            _buttonBrowse.Text = "Browse";
            _buttonBrowse.UseVisualStyleBackColor = true;
            _buttonBrowse.Click += OnButtonClick;
            // 
            // _labelOpacityLevel
            // 
            _labelOpacityLevel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            _labelOpacityLevel.AutoSize = true;
            _labelOpacityLevel.Location = new System.Drawing.Point(307, 142);
            _labelOpacityLevel.Name = "_labelOpacityLevel";
            _labelOpacityLevel.Size = new System.Drawing.Size(48, 15);
            _labelOpacityLevel.TabIndex = 12;
            _labelOpacityLevel.Text = "Opacity";
            // 
            // _trackBarOpacity
            // 
            _trackBarOpacity.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _trackBarOpacity.Location = new System.Drawing.Point(75, 142);
            _trackBarOpacity.Maximum = 255;
            _trackBarOpacity.Name = "_trackBarOpacity";
            _trackBarOpacity.Size = new System.Drawing.Size(227, 45);
            _trackBarOpacity.TabIndex = 11;
            _trackBarOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            _trackBarOpacity.ValueChanged += OnValueChanged;
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(75, 48);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(199, 23);
            textBox1.TabIndex = 10;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(12, 22);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(91, 19);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "Enable Logo";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += OnValueChanged;
            // 
            // VideoSettingsDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(366, 216);
            ControlBox = false;
            Controls.Add(groupBox1);
            Name = "VideoSettingsDialog";
            Text = "Player Settings";
            ((System.ComponentModel.ISupportInitialize)_numericUpDownX).EndInit();
            ((System.ComponentModel.ISupportInitialize)_numericUpDownY).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_trackBarOpacity).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.NumericUpDown _numericUpDownX;
        private System.Windows.Forms.NumericUpDown _numericUpDownY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar _trackBarOpacity;
        private System.Windows.Forms.Label _labelOpacityLevel;
        private System.Windows.Forms.Button _buttonBrowse;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}