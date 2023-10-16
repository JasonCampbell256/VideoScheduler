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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Show = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Season = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Episode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._buttonAddNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._comboBoxShow = new System.Windows.Forms.ComboBox();
            this._comboBoxSeason = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._comboBoxEpisode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._textBoxDescription = new System.Windows.Forms.TextBox();
            this._labelGUID = new System.Windows.Forms.Label();
            this._buttonUseSelected = new System.Windows.Forms.Button();
            this._buttonSave = new System.Windows.Forms.Button();
            this._buttonEditSelected = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Show,
            this.Season,
            this.Episode,
            this.Description});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(465, 356);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnRowEnter);
            // 
            // Show
            // 
            this.Show.HeaderText = "Show";
            this.Show.Name = "Show";
            this.Show.ReadOnly = true;
            // 
            // Season
            // 
            this.Season.HeaderText = "Season";
            this.Season.Name = "Season";
            this.Season.ReadOnly = true;
            // 
            // Episode
            // 
            this.Episode.HeaderText = "Episode";
            this.Episode.Name = "Episode";
            this.Episode.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // _buttonAddNew
            // 
            this._buttonAddNew.Location = new System.Drawing.Point(13, 375);
            this._buttonAddNew.Name = "_buttonAddNew";
            this._buttonAddNew.Size = new System.Drawing.Size(464, 23);
            this._buttonAddNew.TabIndex = 1;
            this._buttonAddNew.Text = "Add New";
            this._buttonAddNew.UseVisualStyleBackColor = true;
            this._buttonAddNew.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(492, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Show:";
            // 
            // _comboBoxShow
            // 
            this._comboBoxShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxShow.FormattingEnabled = true;
            this._comboBoxShow.Location = new System.Drawing.Point(576, 58);
            this._comboBoxShow.Name = "_comboBoxShow";
            this._comboBoxShow.Size = new System.Drawing.Size(166, 21);
            this._comboBoxShow.TabIndex = 3;
            this._comboBoxShow.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // _comboBoxSeason
            // 
            this._comboBoxSeason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSeason.FormattingEnabled = true;
            this._comboBoxSeason.Location = new System.Drawing.Point(576, 85);
            this._comboBoxSeason.Name = "_comboBoxSeason";
            this._comboBoxSeason.Size = new System.Drawing.Size(166, 21);
            this._comboBoxSeason.TabIndex = 5;
            this._comboBoxSeason.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(492, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Season:";
            // 
            // _comboBoxEpisode
            // 
            this._comboBoxEpisode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxEpisode.FormattingEnabled = true;
            this._comboBoxEpisode.Location = new System.Drawing.Point(576, 112);
            this._comboBoxEpisode.Name = "_comboBoxEpisode";
            this._comboBoxEpisode.Size = new System.Drawing.Size(166, 21);
            this._comboBoxEpisode.TabIndex = 7;
            this._comboBoxEpisode.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(492, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Episode:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(492, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Description:";
            // 
            // _textBoxDescription
            // 
            this._textBoxDescription.Location = new System.Drawing.Point(576, 139);
            this._textBoxDescription.Multiline = true;
            this._textBoxDescription.Name = "_textBoxDescription";
            this._textBoxDescription.Size = new System.Drawing.Size(166, 123);
            this._textBoxDescription.TabIndex = 9;
            // 
            // _labelGUID
            // 
            this._labelGUID.AutoSize = true;
            this._labelGUID.Location = new System.Drawing.Point(495, 23);
            this._labelGUID.Name = "_labelGUID";
            this._labelGUID.Size = new System.Drawing.Size(0, 13);
            this._labelGUID.TabIndex = 10;
            // 
            // _buttonUseSelected
            // 
            this._buttonUseSelected.Location = new System.Drawing.Point(13, 434);
            this._buttonUseSelected.Name = "_buttonUseSelected";
            this._buttonUseSelected.Size = new System.Drawing.Size(464, 23);
            this._buttonUseSelected.TabIndex = 11;
            this._buttonUseSelected.Text = "Use Selected";
            this._buttonUseSelected.UseVisualStyleBackColor = true;
            this._buttonUseSelected.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonSave
            // 
            this._buttonSave.Location = new System.Drawing.Point(495, 286);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(258, 23);
            this._buttonSave.TabIndex = 12;
            this._buttonSave.Text = "Save";
            this._buttonSave.UseVisualStyleBackColor = true;
            this._buttonSave.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonEditSelected
            // 
            this._buttonEditSelected.Location = new System.Drawing.Point(13, 405);
            this._buttonEditSelected.Name = "_buttonEditSelected";
            this._buttonEditSelected.Size = new System.Drawing.Size(464, 23);
            this._buttonEditSelected.TabIndex = 13;
            this._buttonEditSelected.Text = "Edit Selected";
            this._buttonEditSelected.UseVisualStyleBackColor = true;
            this._buttonEditSelected.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // ShowRunControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 480);
            this.Controls.Add(this._buttonEditSelected);
            this.Controls.Add(this._buttonSave);
            this.Controls.Add(this._buttonUseSelected);
            this.Controls.Add(this._labelGUID);
            this.Controls.Add(this._textBoxDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._comboBoxEpisode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._comboBoxSeason);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._comboBoxShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._buttonAddNew);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ShowRunControl";
            this.Text = "ShowRunControl";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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