﻿namespace VideoScheduler
{
    partial class ScheduleControl
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
            this._buttonMonday = new System.Windows.Forms.Button();
            this._buttonTuesday = new System.Windows.Forms.Button();
            this._buttonThursday = new System.Windows.Forms.Button();
            this._buttonWednesday = new System.Windows.Forms.Button();
            this._buttonSaturday = new System.Windows.Forms.Button();
            this._buttonFriday = new System.Windows.Forms.Button();
            this._buttonSunday = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._buttonAddTimeBlock = new System.Windows.Forms.Button();
            this._buttonEditTimeBlock = new System.Windows.Forms.Button();
            this._labelDayOrDate = new System.Windows.Forms.Label();
            this._buttonCustomDate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._buttonEraseDay = new System.Windows.Forms.Button();
            this._buttonCopyDay = new System.Windows.Forms.Button();
            this._buttonCopyTimeBlockToAnotherDay = new System.Windows.Forms.Button();
            this._buttonCopyTimeBlock = new System.Windows.Forms.Button();
            this._buttonRemoveTimeBlock = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._buttonContentUp = new System.Windows.Forms.Button();
            this._buttonContentDown = new System.Windows.Forms.Button();
            this._buttonAddMovie = new System.Windows.Forms.Button();
            this._buttonAddCommercialFiller = new System.Windows.Forms.Button();
            this._buttonAddVideoWithCriteria = new System.Windows.Forms.Button();
            this._buttonAddRun = new System.Windows.Forms.Button();
            this._buttonRemoveContent = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContentDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonMonday
            // 
            this._buttonMonday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonMonday.Location = new System.Drawing.Point(12, 31);
            this._buttonMonday.Name = "_buttonMonday";
            this._buttonMonday.Size = new System.Drawing.Size(102, 23);
            this._buttonMonday.TabIndex = 0;
            this._buttonMonday.Text = "Monday";
            this._buttonMonday.UseVisualStyleBackColor = true;
            this._buttonMonday.Click += new System.EventHandler(this.OnDayButtonClick);
            // 
            // _buttonTuesday
            // 
            this._buttonTuesday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonTuesday.Location = new System.Drawing.Point(3, 31);
            this._buttonTuesday.Name = "_buttonTuesday";
            this._buttonTuesday.Size = new System.Drawing.Size(98, 23);
            this._buttonTuesday.TabIndex = 0;
            this._buttonTuesday.Text = "Tuesday";
            this._buttonTuesday.UseVisualStyleBackColor = true;
            this._buttonTuesday.Click += new System.EventHandler(this.OnDayButtonClick);
            // 
            // _buttonThursday
            // 
            this._buttonThursday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonThursday.Location = new System.Drawing.Point(3, 60);
            this._buttonThursday.Name = "_buttonThursday";
            this._buttonThursday.Size = new System.Drawing.Size(98, 23);
            this._buttonThursday.TabIndex = 1;
            this._buttonThursday.Text = "Thursday";
            this._buttonThursday.UseVisualStyleBackColor = true;
            this._buttonThursday.Click += new System.EventHandler(this.OnDayButtonClick);
            // 
            // _buttonWednesday
            // 
            this._buttonWednesday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonWednesday.Location = new System.Drawing.Point(12, 60);
            this._buttonWednesday.Name = "_buttonWednesday";
            this._buttonWednesday.Size = new System.Drawing.Size(102, 23);
            this._buttonWednesday.TabIndex = 1;
            this._buttonWednesday.Text = "Wednesday";
            this._buttonWednesday.UseVisualStyleBackColor = true;
            this._buttonWednesday.Click += new System.EventHandler(this.OnDayButtonClick);
            // 
            // _buttonSaturday
            // 
            this._buttonSaturday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonSaturday.Location = new System.Drawing.Point(3, 89);
            this._buttonSaturday.Name = "_buttonSaturday";
            this._buttonSaturday.Size = new System.Drawing.Size(98, 23);
            this._buttonSaturday.TabIndex = 2;
            this._buttonSaturday.Text = "Saturday";
            this._buttonSaturday.UseVisualStyleBackColor = true;
            this._buttonSaturday.Click += new System.EventHandler(this.OnDayButtonClick);
            // 
            // _buttonFriday
            // 
            this._buttonFriday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonFriday.Location = new System.Drawing.Point(12, 89);
            this._buttonFriday.Name = "_buttonFriday";
            this._buttonFriday.Size = new System.Drawing.Size(102, 23);
            this._buttonFriday.TabIndex = 2;
            this._buttonFriday.Text = "Friday";
            this._buttonFriday.UseVisualStyleBackColor = true;
            this._buttonFriday.Click += new System.EventHandler(this.OnDayButtonClick);
            // 
            // _buttonSunday
            // 
            this._buttonSunday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonSunday.Location = new System.Drawing.Point(12, 118);
            this._buttonSunday.Name = "_buttonSunday";
            this._buttonSunday.Size = new System.Drawing.Size(102, 23);
            this._buttonSunday.TabIndex = 3;
            this._buttonSunday.Text = "Sunday";
            this._buttonSunday.UseVisualStyleBackColor = true;
            this._buttonSunday.Click += new System.EventHandler(this.OnDayButtonClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StartTime,
            this.EndTime,
            this.Description});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(6, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(286, 372);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnRowEnter);
            // 
            // StartTime
            // 
            this.StartTime.HeaderText = "StartTime";
            this.StartTime.Name = "StartTime";
            this.StartTime.ReadOnly = true;
            this.StartTime.Width = 75;
            // 
            // EndTime
            // 
            this.EndTime.HeaderText = "EndTime";
            this.EndTime.Name = "EndTime";
            this.EndTime.ReadOnly = true;
            this.EndTime.Width = 75;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // _buttonAddTimeBlock
            // 
            this._buttonAddTimeBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonAddTimeBlock.Location = new System.Drawing.Point(6, 410);
            this._buttonAddTimeBlock.Name = "_buttonAddTimeBlock";
            this._buttonAddTimeBlock.Size = new System.Drawing.Size(285, 23);
            this._buttonAddTimeBlock.TabIndex = 2;
            this._buttonAddTimeBlock.Text = "Add Time Block";
            this._buttonAddTimeBlock.UseVisualStyleBackColor = true;
            this._buttonAddTimeBlock.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonEditTimeBlock
            // 
            this._buttonEditTimeBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonEditTimeBlock.Location = new System.Drawing.Point(6, 468);
            this._buttonEditTimeBlock.Name = "_buttonEditTimeBlock";
            this._buttonEditTimeBlock.Size = new System.Drawing.Size(285, 23);
            this._buttonEditTimeBlock.TabIndex = 4;
            this._buttonEditTimeBlock.Text = "Edit Time Block";
            this._buttonEditTimeBlock.UseVisualStyleBackColor = true;
            this._buttonEditTimeBlock.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _labelDayOrDate
            // 
            this._labelDayOrDate.AutoSize = true;
            this._labelDayOrDate.Location = new System.Drawing.Point(3, 8);
            this._labelDayOrDate.Name = "_labelDayOrDate";
            this._labelDayOrDate.Size = new System.Drawing.Size(35, 13);
            this._labelDayOrDate.TabIndex = 0;
            this._labelDayOrDate.Text = "label1";
            // 
            // _buttonCustomDate
            // 
            this._buttonCustomDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCustomDate.Enabled = false;
            this._buttonCustomDate.Location = new System.Drawing.Point(3, 118);
            this._buttonCustomDate.Name = "_buttonCustomDate";
            this._buttonCustomDate.Size = new System.Drawing.Size(98, 23);
            this._buttonCustomDate.TabIndex = 3;
            this._buttonCustomDate.Text = "Custom Date";
            this._buttonCustomDate.UseVisualStyleBackColor = true;
            this._buttonCustomDate.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._buttonEraseDay);
            this.splitContainer1.Panel1.Controls.Add(this._buttonCopyDay);
            this.splitContainer1.Panel1.Controls.Add(this._buttonCopyTimeBlockToAnotherDay);
            this.splitContainer1.Panel1.Controls.Add(this._buttonCopyTimeBlock);
            this.splitContainer1.Panel1.Controls.Add(this._buttonRemoveTimeBlock);
            this.splitContainer1.Panel1.Controls.Add(this._labelDayOrDate);
            this.splitContainer1.Panel1.Controls.Add(this._buttonEditTimeBlock);
            this.splitContainer1.Panel1.Controls.Add(this._buttonAddTimeBlock);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this._buttonAddMovie);
            this.splitContainer1.Panel2.Controls.Add(this._buttonAddCommercialFiller);
            this.splitContainer1.Panel2.Controls.Add(this._buttonAddVideoWithCriteria);
            this.splitContainer1.Panel2.Controls.Add(this._buttonAddRun);
            this.splitContainer1.Panel2.Controls.Add(this._buttonRemoveContent);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(591, 619);
            this.splitContainer1.SplitterDistance = 295;
            this.splitContainer1.TabIndex = 0;
            // 
            // _buttonEraseDay
            // 
            this._buttonEraseDay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonEraseDay.Location = new System.Drawing.Point(6, 585);
            this._buttonEraseDay.Name = "_buttonEraseDay";
            this._buttonEraseDay.Size = new System.Drawing.Size(285, 23);
            this._buttonEraseDay.TabIndex = 8;
            this._buttonEraseDay.Text = "Erase Entire Day";
            this._buttonEraseDay.UseVisualStyleBackColor = true;
            this._buttonEraseDay.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonCopyDay
            // 
            this._buttonCopyDay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCopyDay.Location = new System.Drawing.Point(6, 556);
            this._buttonCopyDay.Name = "_buttonCopyDay";
            this._buttonCopyDay.Size = new System.Drawing.Size(285, 23);
            this._buttonCopyDay.TabIndex = 7;
            this._buttonCopyDay.Text = "Copy Entire Day";
            this._buttonCopyDay.UseVisualStyleBackColor = true;
            this._buttonCopyDay.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonCopyTimeBlockToAnotherDay
            // 
            this._buttonCopyTimeBlockToAnotherDay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCopyTimeBlockToAnotherDay.Location = new System.Drawing.Point(6, 527);
            this._buttonCopyTimeBlockToAnotherDay.Name = "_buttonCopyTimeBlockToAnotherDay";
            this._buttonCopyTimeBlockToAnotherDay.Size = new System.Drawing.Size(285, 23);
            this._buttonCopyTimeBlockToAnotherDay.TabIndex = 6;
            this._buttonCopyTimeBlockToAnotherDay.Text = "Copy Time Block to Another Day";
            this._buttonCopyTimeBlockToAnotherDay.UseVisualStyleBackColor = true;
            this._buttonCopyTimeBlockToAnotherDay.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonCopyTimeBlock
            // 
            this._buttonCopyTimeBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCopyTimeBlock.Location = new System.Drawing.Point(6, 439);
            this._buttonCopyTimeBlock.Name = "_buttonCopyTimeBlock";
            this._buttonCopyTimeBlock.Size = new System.Drawing.Size(285, 23);
            this._buttonCopyTimeBlock.TabIndex = 3;
            this._buttonCopyTimeBlock.Text = "Copy Time Block";
            this._buttonCopyTimeBlock.UseVisualStyleBackColor = true;
            this._buttonCopyTimeBlock.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonRemoveTimeBlock
            // 
            this._buttonRemoveTimeBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonRemoveTimeBlock.Location = new System.Drawing.Point(6, 497);
            this._buttonRemoveTimeBlock.Name = "_buttonRemoveTimeBlock";
            this._buttonRemoveTimeBlock.Size = new System.Drawing.Size(285, 23);
            this._buttonRemoveTimeBlock.TabIndex = 5;
            this._buttonRemoveTimeBlock.Text = "Remove Time Block";
            this._buttonRemoveTimeBlock.UseVisualStyleBackColor = true;
            this._buttonRemoveTimeBlock.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(6, 438);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._buttonContentUp);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._buttonContentDown);
            this.splitContainer2.Size = new System.Drawing.Size(274, 23);
            this.splitContainer2.SplitterDistance = 136;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 16;
            // 
            // _buttonContentUp
            // 
            this._buttonContentUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonContentUp.Location = new System.Drawing.Point(0, 0);
            this._buttonContentUp.Name = "_buttonContentUp";
            this._buttonContentUp.Size = new System.Drawing.Size(136, 23);
            this._buttonContentUp.TabIndex = 0;
            this._buttonContentUp.Text = "Move Up";
            this._buttonContentUp.UseVisualStyleBackColor = true;
            this._buttonContentUp.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonContentDown
            // 
            this._buttonContentDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonContentDown.Location = new System.Drawing.Point(0, 0);
            this._buttonContentDown.Name = "_buttonContentDown";
            this._buttonContentDown.Size = new System.Drawing.Size(137, 23);
            this._buttonContentDown.TabIndex = 0;
            this._buttonContentDown.Text = "Move Down";
            this._buttonContentDown.UseVisualStyleBackColor = true;
            this._buttonContentDown.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonAddMovie
            // 
            this._buttonAddMovie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonAddMovie.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._buttonAddMovie.Location = new System.Drawing.Point(6, 525);
            this._buttonAddMovie.Name = "_buttonAddMovie";
            this._buttonAddMovie.Size = new System.Drawing.Size(274, 23);
            this._buttonAddMovie.TabIndex = 4;
            this._buttonAddMovie.Text = "Add Movie";
            this._buttonAddMovie.UseVisualStyleBackColor = true;
            this._buttonAddMovie.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonAddCommercialFiller
            // 
            this._buttonAddCommercialFiller.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonAddCommercialFiller.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._buttonAddCommercialFiller.Location = new System.Drawing.Point(6, 554);
            this._buttonAddCommercialFiller.Name = "_buttonAddCommercialFiller";
            this._buttonAddCommercialFiller.Size = new System.Drawing.Size(274, 23);
            this._buttonAddCommercialFiller.TabIndex = 5;
            this._buttonAddCommercialFiller.Text = "Add Commercial Filler";
            this._buttonAddCommercialFiller.UseVisualStyleBackColor = true;
            this._buttonAddCommercialFiller.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonAddVideoWithCriteria
            // 
            this._buttonAddVideoWithCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonAddVideoWithCriteria.Location = new System.Drawing.Point(6, 467);
            this._buttonAddVideoWithCriteria.Name = "_buttonAddVideoWithCriteria";
            this._buttonAddVideoWithCriteria.Size = new System.Drawing.Size(274, 23);
            this._buttonAddVideoWithCriteria.TabIndex = 2;
            this._buttonAddVideoWithCriteria.Text = "Add Video From Folder";
            this._buttonAddVideoWithCriteria.UseVisualStyleBackColor = true;
            this._buttonAddVideoWithCriteria.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonAddRun
            // 
            this._buttonAddRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonAddRun.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._buttonAddRun.Location = new System.Drawing.Point(6, 496);
            this._buttonAddRun.Name = "_buttonAddRun";
            this._buttonAddRun.Size = new System.Drawing.Size(274, 23);
            this._buttonAddRun.TabIndex = 3;
            this._buttonAddRun.Text = "Add Show Run";
            this._buttonAddRun.UseVisualStyleBackColor = true;
            this._buttonAddRun.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonRemoveContent
            // 
            this._buttonRemoveContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonRemoveContent.Enabled = false;
            this._buttonRemoveContent.Location = new System.Drawing.Point(6, 584);
            this._buttonRemoveContent.Name = "_buttonRemoveContent";
            this._buttonRemoveContent.Size = new System.Drawing.Size(274, 23);
            this._buttonRemoveContent.TabIndex = 6;
            this._buttonRemoveContent.Text = "Remove Content from Time Block";
            this._buttonRemoveContent.UseVisualStyleBackColor = true;
            this._buttonRemoveContent.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.ContentDescription});
            this.dataGridView2.Location = new System.Drawing.Point(6, 32);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(274, 401);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnRowEnter);
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // ContentDescription
            // 
            this.ContentDescription.HeaderText = "ContentDescription";
            this.ContentDescription.Name = "ContentDescription";
            this.ContentDescription.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Content";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer3.Size = new System.Drawing.Size(829, 619);
            this.splitContainer3.SplitterDistance = 234;
            this.splitContainer3.TabIndex = 17;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this._buttonMonday);
            this.splitContainer4.Panel1.Controls.Add(this._buttonWednesday);
            this.splitContainer4.Panel1.Controls.Add(this._buttonSunday);
            this.splitContainer4.Panel1.Controls.Add(this._buttonFriday);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this._buttonTuesday);
            this.splitContainer4.Panel2.Controls.Add(this._buttonSaturday);
            this.splitContainer4.Panel2.Controls.Add(this._buttonCustomDate);
            this.splitContainer4.Panel2.Controls.Add(this._buttonThursday);
            this.splitContainer4.Size = new System.Drawing.Size(234, 619);
            this.splitContainer4.SplitterDistance = 117;
            this.splitContainer4.TabIndex = 0;
            // 
            // ScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 619);
            this.Controls.Add(this.splitContainer3);
            this.Name = "ScheduleControl";
            this.Text = "Schedule Control";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _buttonMonday;
        private System.Windows.Forms.Button _buttonTuesday;
        private System.Windows.Forms.Button _buttonThursday;
        private System.Windows.Forms.Button _buttonWednesday;
        private System.Windows.Forms.Button _buttonSaturday;
        private System.Windows.Forms.Button _buttonFriday;
        private System.Windows.Forms.Button _buttonSunday;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button _buttonAddTimeBlock;
        private System.Windows.Forms.Button _buttonEditTimeBlock;
        private System.Windows.Forms.Label _labelDayOrDate;
        private System.Windows.Forms.Button _buttonCustomDate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Button _buttonRemoveContent;
        private System.Windows.Forms.Button _buttonRemoveTimeBlock;
        private System.Windows.Forms.Button _buttonAddRun;
        private System.Windows.Forms.Button _buttonAddVideoWithCriteria;
        private System.Windows.Forms.Button _buttonAddCommercialFiller;
        private System.Windows.Forms.Button _buttonCopyTimeBlock;
        private System.Windows.Forms.Button _buttonCopyTimeBlockToAnotherDay;
        private System.Windows.Forms.Button _buttonAddMovie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContentDescription;
        private System.Windows.Forms.Button _buttonContentDown;
        private System.Windows.Forms.Button _buttonContentUp;
        private System.Windows.Forms.Button _buttonCopyDay;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button _buttonEraseDay;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
    }
}