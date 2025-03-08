namespace VideoScheduler
{
    partial class ShowRunControl
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
            dataGridView1 = new System.Windows.Forms.DataGridView();
            Show = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Season = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Episode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _buttonAddNew = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            _comboBoxShow = new System.Windows.Forms.ComboBox();
            _comboBoxSeason = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            _comboBoxEpisode = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            _textBoxDescription = new System.Windows.Forms.TextBox();
            _labelGUID = new System.Windows.Forms.Label();
            _buttonUseSelected = new System.Windows.Forms.Button();
            _buttonSave = new System.Windows.Forms.Button();
            _buttonEditSelected = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Show, Season, Episode, Description });
            dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dataGridView1.Location = new System.Drawing.Point(14, 14);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ShowEditingIcon = false;
            dataGridView1.Size = new System.Drawing.Size(542, 411);
            dataGridView1.TabIndex = 0;
            dataGridView1.RowEnter += OnRowEnter;
            // 
            // Show
            // 
            Show.HeaderText = "Show";
            Show.Name = "Show";
            Show.ReadOnly = true;
            // 
            // Season
            // 
            Season.HeaderText = "Season";
            Season.Name = "Season";
            Season.ReadOnly = true;
            // 
            // Episode
            // 
            Episode.HeaderText = "Episode";
            Episode.Name = "Episode";
            Episode.ReadOnly = true;
            // 
            // Description
            // 
            Description.HeaderText = "Description";
            Description.Name = "Description";
            Description.ReadOnly = true;
            // 
            // _buttonAddNew
            // 
            _buttonAddNew.Location = new System.Drawing.Point(15, 433);
            _buttonAddNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonAddNew.Name = "_buttonAddNew";
            _buttonAddNew.Size = new System.Drawing.Size(541, 27);
            _buttonAddNew.TabIndex = 1;
            _buttonAddNew.Text = "Add New";
            _buttonAddNew.UseVisualStyleBackColor = true;
            _buttonAddNew.Click += OnButtonClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(574, 70);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 15);
            label1.TabIndex = 2;
            label1.Text = "Show:";
            // 
            // _comboBoxShow
            // 
            _comboBoxShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _comboBoxShow.FormattingEnabled = true;
            _comboBoxShow.Location = new System.Drawing.Point(672, 67);
            _comboBoxShow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _comboBoxShow.Name = "_comboBoxShow";
            _comboBoxShow.Size = new System.Drawing.Size(193, 23);
            _comboBoxShow.TabIndex = 3;
            _comboBoxShow.SelectedIndexChanged += OnSelectedIndexChanged;
            // 
            // _comboBoxSeason
            // 
            _comboBoxSeason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _comboBoxSeason.FormattingEnabled = true;
            _comboBoxSeason.Location = new System.Drawing.Point(672, 98);
            _comboBoxSeason.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _comboBoxSeason.Name = "_comboBoxSeason";
            _comboBoxSeason.Size = new System.Drawing.Size(193, 23);
            _comboBoxSeason.TabIndex = 5;
            _comboBoxSeason.SelectedIndexChanged += OnSelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(574, 102);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(47, 15);
            label2.TabIndex = 4;
            label2.Text = "Season:";
            // 
            // _comboBoxEpisode
            // 
            _comboBoxEpisode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _comboBoxEpisode.FormattingEnabled = true;
            _comboBoxEpisode.Location = new System.Drawing.Point(672, 129);
            _comboBoxEpisode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _comboBoxEpisode.Name = "_comboBoxEpisode";
            _comboBoxEpisode.Size = new System.Drawing.Size(193, 23);
            _comboBoxEpisode.TabIndex = 7;
            _comboBoxEpisode.SelectedIndexChanged += OnSelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(574, 133);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(51, 15);
            label3.TabIndex = 6;
            label3.Text = "Episode:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(574, 164);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(70, 15);
            label4.TabIndex = 8;
            label4.Text = "Description:";
            // 
            // _textBoxDescription
            // 
            _textBoxDescription.Location = new System.Drawing.Point(672, 160);
            _textBoxDescription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _textBoxDescription.Multiline = true;
            _textBoxDescription.Name = "_textBoxDescription";
            _textBoxDescription.Size = new System.Drawing.Size(193, 141);
            _textBoxDescription.TabIndex = 9;
            // 
            // _labelGUID
            // 
            _labelGUID.AutoSize = true;
            _labelGUID.Location = new System.Drawing.Point(578, 27);
            _labelGUID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            _labelGUID.Name = "_labelGUID";
            _labelGUID.Size = new System.Drawing.Size(0, 15);
            _labelGUID.TabIndex = 10;
            // 
            // _buttonUseSelected
            // 
            _buttonUseSelected.Location = new System.Drawing.Point(15, 501);
            _buttonUseSelected.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonUseSelected.Name = "_buttonUseSelected";
            _buttonUseSelected.Size = new System.Drawing.Size(541, 27);
            _buttonUseSelected.TabIndex = 11;
            _buttonUseSelected.Text = "Use Selected";
            _buttonUseSelected.UseVisualStyleBackColor = true;
            _buttonUseSelected.Click += OnButtonClick;
            // 
            // _buttonSave
            // 
            _buttonSave.Location = new System.Drawing.Point(578, 330);
            _buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonSave.Name = "_buttonSave";
            _buttonSave.Size = new System.Drawing.Size(301, 27);
            _buttonSave.TabIndex = 12;
            _buttonSave.Text = "Save";
            _buttonSave.UseVisualStyleBackColor = true;
            _buttonSave.Click += OnButtonClick;
            // 
            // _buttonEditSelected
            // 
            _buttonEditSelected.Location = new System.Drawing.Point(15, 467);
            _buttonEditSelected.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonEditSelected.Name = "_buttonEditSelected";
            _buttonEditSelected.Size = new System.Drawing.Size(541, 27);
            _buttonEditSelected.TabIndex = 13;
            _buttonEditSelected.Text = "Edit Selected";
            _buttonEditSelected.UseVisualStyleBackColor = true;
            _buttonEditSelected.Click += OnButtonClick;
            // 
            // ShowRunControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(892, 554);
            Controls.Add(_buttonEditSelected);
            Controls.Add(_buttonSave);
            Controls.Add(_buttonUseSelected);
            Controls.Add(_labelGUID);
            Controls.Add(_textBoxDescription);
            Controls.Add(label4);
            Controls.Add(_comboBoxEpisode);
            Controls.Add(label3);
            Controls.Add(_comboBoxSeason);
            Controls.Add(label2);
            Controls.Add(_comboBoxShow);
            Controls.Add(label1);
            Controls.Add(_buttonAddNew);
            Controls.Add(dataGridView1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ShowRunControl";
            Text = "ShowRunControl";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Show;
        private System.Windows.Forms.DataGridViewTextBoxColumn Season;
        private System.Windows.Forms.DataGridViewTextBoxColumn Episode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Button _buttonAddNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _comboBoxShow;
        private System.Windows.Forms.ComboBox _comboBoxSeason;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _comboBoxEpisode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _textBoxDescription;
        private System.Windows.Forms.Label _labelGUID;
        private System.Windows.Forms.Button _buttonUseSelected;
        private System.Windows.Forms.Button _buttonSave;
        private System.Windows.Forms.Button _buttonEditSelected;
    }
}