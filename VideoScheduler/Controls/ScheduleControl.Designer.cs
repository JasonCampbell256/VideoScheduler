namespace VideoScheduler
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
            _buttonMonday = new System.Windows.Forms.Button();
            _buttonTuesday = new System.Windows.Forms.Button();
            _buttonThursday = new System.Windows.Forms.Button();
            _buttonWednesday = new System.Windows.Forms.Button();
            _buttonSaturday = new System.Windows.Forms.Button();
            _buttonFriday = new System.Windows.Forms.Button();
            _buttonSunday = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _buttonAddTimeBlock = new System.Windows.Forms.Button();
            _buttonEditTimeBlock = new System.Windows.Forms.Button();
            _labelDayOrDate = new System.Windows.Forms.Label();
            _buttonCustomDate = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            _buttonEraseDay = new System.Windows.Forms.Button();
            _buttonCopyDay = new System.Windows.Forms.Button();
            _buttonCopyTimeBlockToAnotherDay = new System.Windows.Forms.Button();
            _buttonCopyTimeBlock = new System.Windows.Forms.Button();
            _buttonRemoveTimeBlock = new System.Windows.Forms.Button();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            _buttonContentUp = new System.Windows.Forms.Button();
            _buttonContentDown = new System.Windows.Forms.Button();
            _buttonAddMovie = new System.Windows.Forms.Button();
            _buttonAddCommercialFiller = new System.Windows.Forms.Button();
            _buttonAddVideoWithCriteria = new System.Windows.Forms.Button();
            _buttonAddRun = new System.Windows.Forms.Button();
            _buttonRemoveContent = new System.Windows.Forms.Button();
            dataGridView2 = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            splitContainer4 = new System.Windows.Forms.SplitContainer();
            Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ContentDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            SuspendLayout();
            // 
            // _buttonMonday
            // 
            _buttonMonday.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonMonday.Location = new System.Drawing.Point(14, 36);
            _buttonMonday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonMonday.Name = "_buttonMonday";
            _buttonMonday.Size = new System.Drawing.Size(119, 27);
            _buttonMonday.TabIndex = 0;
            _buttonMonday.Text = "Monday";
            _buttonMonday.UseVisualStyleBackColor = true;
            _buttonMonday.Click += OnDayButtonClick;
            // 
            // _buttonTuesday
            // 
            _buttonTuesday.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonTuesday.Location = new System.Drawing.Point(4, 36);
            _buttonTuesday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonTuesday.Name = "_buttonTuesday";
            _buttonTuesday.Size = new System.Drawing.Size(112, 27);
            _buttonTuesday.TabIndex = 0;
            _buttonTuesday.Text = "Tuesday";
            _buttonTuesday.UseVisualStyleBackColor = true;
            _buttonTuesday.Click += OnDayButtonClick;
            // 
            // _buttonThursday
            // 
            _buttonThursday.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonThursday.Location = new System.Drawing.Point(4, 69);
            _buttonThursday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonThursday.Name = "_buttonThursday";
            _buttonThursday.Size = new System.Drawing.Size(112, 27);
            _buttonThursday.TabIndex = 1;
            _buttonThursday.Text = "Thursday";
            _buttonThursday.UseVisualStyleBackColor = true;
            _buttonThursday.Click += OnDayButtonClick;
            // 
            // _buttonWednesday
            // 
            _buttonWednesday.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonWednesday.Location = new System.Drawing.Point(14, 69);
            _buttonWednesday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonWednesday.Name = "_buttonWednesday";
            _buttonWednesday.Size = new System.Drawing.Size(119, 27);
            _buttonWednesday.TabIndex = 1;
            _buttonWednesday.Text = "Wednesday";
            _buttonWednesday.UseVisualStyleBackColor = true;
            _buttonWednesday.Click += OnDayButtonClick;
            // 
            // _buttonSaturday
            // 
            _buttonSaturday.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonSaturday.Location = new System.Drawing.Point(4, 103);
            _buttonSaturday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonSaturday.Name = "_buttonSaturday";
            _buttonSaturday.Size = new System.Drawing.Size(112, 27);
            _buttonSaturday.TabIndex = 2;
            _buttonSaturday.Text = "Saturday";
            _buttonSaturday.UseVisualStyleBackColor = true;
            _buttonSaturday.Click += OnDayButtonClick;
            // 
            // _buttonFriday
            // 
            _buttonFriday.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonFriday.Location = new System.Drawing.Point(14, 103);
            _buttonFriday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonFriday.Name = "_buttonFriday";
            _buttonFriday.Size = new System.Drawing.Size(119, 27);
            _buttonFriday.TabIndex = 2;
            _buttonFriday.Text = "Friday";
            _buttonFriday.UseVisualStyleBackColor = true;
            _buttonFriday.Click += OnDayButtonClick;
            // 
            // _buttonSunday
            // 
            _buttonSunday.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonSunday.Location = new System.Drawing.Point(14, 136);
            _buttonSunday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonSunday.Name = "_buttonSunday";
            _buttonSunday.Size = new System.Drawing.Size(119, 27);
            _buttonSunday.TabIndex = 3;
            _buttonSunday.Text = "Sunday";
            _buttonSunday.UseVisualStyleBackColor = true;
            _buttonSunday.Click += OnDayButtonClick;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { StartTime, EndTime, Description });
            dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dataGridView1.Location = new System.Drawing.Point(7, 37);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(334, 429);
            dataGridView1.TabIndex = 1;
            dataGridView1.RowEnter += OnRowEnter;
            // 
            // StartTime
            // 
            StartTime.HeaderText = "StartTime";
            StartTime.Name = "StartTime";
            StartTime.ReadOnly = true;
            StartTime.Width = 75;
            // 
            // EndTime
            // 
            EndTime.HeaderText = "EndTime";
            EndTime.Name = "EndTime";
            EndTime.ReadOnly = true;
            EndTime.Width = 75;
            // 
            // Description
            // 
            Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            Description.HeaderText = "Description";
            Description.Name = "Description";
            Description.ReadOnly = true;
            // 
            // _buttonAddTimeBlock
            // 
            _buttonAddTimeBlock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonAddTimeBlock.Location = new System.Drawing.Point(7, 473);
            _buttonAddTimeBlock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonAddTimeBlock.Name = "_buttonAddTimeBlock";
            _buttonAddTimeBlock.Size = new System.Drawing.Size(332, 27);
            _buttonAddTimeBlock.TabIndex = 2;
            _buttonAddTimeBlock.Text = "Add Time Block";
            _buttonAddTimeBlock.UseVisualStyleBackColor = true;
            _buttonAddTimeBlock.Click += OnButtonClick;
            // 
            // _buttonEditTimeBlock
            // 
            _buttonEditTimeBlock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonEditTimeBlock.Location = new System.Drawing.Point(7, 540);
            _buttonEditTimeBlock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonEditTimeBlock.Name = "_buttonEditTimeBlock";
            _buttonEditTimeBlock.Size = new System.Drawing.Size(332, 27);
            _buttonEditTimeBlock.TabIndex = 4;
            _buttonEditTimeBlock.Text = "Edit Time Block";
            _buttonEditTimeBlock.UseVisualStyleBackColor = true;
            _buttonEditTimeBlock.Click += OnButtonClick;
            // 
            // _labelDayOrDate
            // 
            _labelDayOrDate.AutoSize = true;
            _labelDayOrDate.Location = new System.Drawing.Point(4, 9);
            _labelDayOrDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            _labelDayOrDate.Name = "_labelDayOrDate";
            _labelDayOrDate.Size = new System.Drawing.Size(38, 15);
            _labelDayOrDate.TabIndex = 0;
            _labelDayOrDate.Text = "label1";
            // 
            // _buttonCustomDate
            // 
            _buttonCustomDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonCustomDate.Enabled = false;
            _buttonCustomDate.Location = new System.Drawing.Point(4, 136);
            _buttonCustomDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonCustomDate.Name = "_buttonCustomDate";
            _buttonCustomDate.Size = new System.Drawing.Size(112, 27);
            _buttonCustomDate.TabIndex = 3;
            _buttonCustomDate.Text = "Custom Date";
            _buttonCustomDate.UseVisualStyleBackColor = true;
            _buttonCustomDate.Click += OnButtonClick;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_buttonEraseDay);
            splitContainer1.Panel1.Controls.Add(_buttonCopyDay);
            splitContainer1.Panel1.Controls.Add(_buttonCopyTimeBlockToAnotherDay);
            splitContainer1.Panel1.Controls.Add(_buttonCopyTimeBlock);
            splitContainer1.Panel1.Controls.Add(_buttonRemoveTimeBlock);
            splitContainer1.Panel1.Controls.Add(_labelDayOrDate);
            splitContainer1.Panel1.Controls.Add(_buttonEditTimeBlock);
            splitContainer1.Panel1.Controls.Add(_buttonAddTimeBlock);
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Panel2.Controls.Add(_buttonAddMovie);
            splitContainer1.Panel2.Controls.Add(_buttonAddCommercialFiller);
            splitContainer1.Panel2.Controls.Add(_buttonAddVideoWithCriteria);
            splitContainer1.Panel2.Controls.Add(_buttonAddRun);
            splitContainer1.Panel2.Controls.Add(_buttonRemoveContent);
            splitContainer1.Panel2.Controls.Add(dataGridView2);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Size = new System.Drawing.Size(690, 714);
            splitContainer1.SplitterDistance = 344;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // _buttonEraseDay
            // 
            _buttonEraseDay.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonEraseDay.Location = new System.Drawing.Point(7, 675);
            _buttonEraseDay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonEraseDay.Name = "_buttonEraseDay";
            _buttonEraseDay.Size = new System.Drawing.Size(332, 27);
            _buttonEraseDay.TabIndex = 8;
            _buttonEraseDay.Text = "Erase Entire Day";
            _buttonEraseDay.UseVisualStyleBackColor = true;
            _buttonEraseDay.Click += OnButtonClick;
            // 
            // _buttonCopyDay
            // 
            _buttonCopyDay.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonCopyDay.Location = new System.Drawing.Point(7, 642);
            _buttonCopyDay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonCopyDay.Name = "_buttonCopyDay";
            _buttonCopyDay.Size = new System.Drawing.Size(332, 27);
            _buttonCopyDay.TabIndex = 7;
            _buttonCopyDay.Text = "Copy Entire Day";
            _buttonCopyDay.UseVisualStyleBackColor = true;
            _buttonCopyDay.Click += OnButtonClick;
            // 
            // _buttonCopyTimeBlockToAnotherDay
            // 
            _buttonCopyTimeBlockToAnotherDay.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonCopyTimeBlockToAnotherDay.Location = new System.Drawing.Point(7, 608);
            _buttonCopyTimeBlockToAnotherDay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonCopyTimeBlockToAnotherDay.Name = "_buttonCopyTimeBlockToAnotherDay";
            _buttonCopyTimeBlockToAnotherDay.Size = new System.Drawing.Size(332, 27);
            _buttonCopyTimeBlockToAnotherDay.TabIndex = 6;
            _buttonCopyTimeBlockToAnotherDay.Text = "Copy Time Block to Another Day";
            _buttonCopyTimeBlockToAnotherDay.UseVisualStyleBackColor = true;
            _buttonCopyTimeBlockToAnotherDay.Click += OnButtonClick;
            // 
            // _buttonCopyTimeBlock
            // 
            _buttonCopyTimeBlock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonCopyTimeBlock.Location = new System.Drawing.Point(7, 507);
            _buttonCopyTimeBlock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonCopyTimeBlock.Name = "_buttonCopyTimeBlock";
            _buttonCopyTimeBlock.Size = new System.Drawing.Size(332, 27);
            _buttonCopyTimeBlock.TabIndex = 3;
            _buttonCopyTimeBlock.Text = "Copy Time Block";
            _buttonCopyTimeBlock.UseVisualStyleBackColor = true;
            _buttonCopyTimeBlock.Click += OnButtonClick;
            // 
            // _buttonRemoveTimeBlock
            // 
            _buttonRemoveTimeBlock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonRemoveTimeBlock.Location = new System.Drawing.Point(7, 573);
            _buttonRemoveTimeBlock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonRemoveTimeBlock.Name = "_buttonRemoveTimeBlock";
            _buttonRemoveTimeBlock.Size = new System.Drawing.Size(332, 27);
            _buttonRemoveTimeBlock.TabIndex = 5;
            _buttonRemoveTimeBlock.Text = "Remove Time Block";
            _buttonRemoveTimeBlock.UseVisualStyleBackColor = true;
            _buttonRemoveTimeBlock.Click += OnButtonClick;
            // 
            // splitContainer2
            // 
            splitContainer2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer2.Location = new System.Drawing.Point(7, 505);
            splitContainer2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(_buttonContentUp);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(_buttonContentDown);
            splitContainer2.Size = new System.Drawing.Size(319, 27);
            splitContainer2.SplitterDistance = 157;
            splitContainer2.SplitterWidth = 1;
            splitContainer2.TabIndex = 16;
            // 
            // _buttonContentUp
            // 
            _buttonContentUp.Dock = System.Windows.Forms.DockStyle.Fill;
            _buttonContentUp.Location = new System.Drawing.Point(0, 0);
            _buttonContentUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonContentUp.Name = "_buttonContentUp";
            _buttonContentUp.Size = new System.Drawing.Size(157, 27);
            _buttonContentUp.TabIndex = 0;
            _buttonContentUp.Text = "Move Up";
            _buttonContentUp.UseVisualStyleBackColor = true;
            _buttonContentUp.Click += OnButtonClick;
            // 
            // _buttonContentDown
            // 
            _buttonContentDown.Dock = System.Windows.Forms.DockStyle.Fill;
            _buttonContentDown.Location = new System.Drawing.Point(0, 0);
            _buttonContentDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonContentDown.Name = "_buttonContentDown";
            _buttonContentDown.Size = new System.Drawing.Size(161, 27);
            _buttonContentDown.TabIndex = 0;
            _buttonContentDown.Text = "Move Down";
            _buttonContentDown.UseVisualStyleBackColor = true;
            _buttonContentDown.Click += OnButtonClick;
            // 
            // _buttonAddMovie
            // 
            _buttonAddMovie.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonAddMovie.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            _buttonAddMovie.Location = new System.Drawing.Point(7, 606);
            _buttonAddMovie.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonAddMovie.Name = "_buttonAddMovie";
            _buttonAddMovie.Size = new System.Drawing.Size(319, 27);
            _buttonAddMovie.TabIndex = 4;
            _buttonAddMovie.Text = "Add Movie";
            _buttonAddMovie.UseVisualStyleBackColor = true;
            _buttonAddMovie.Click += OnButtonClick;
            // 
            // _buttonAddCommercialFiller
            // 
            _buttonAddCommercialFiller.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonAddCommercialFiller.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            _buttonAddCommercialFiller.Location = new System.Drawing.Point(7, 639);
            _buttonAddCommercialFiller.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonAddCommercialFiller.Name = "_buttonAddCommercialFiller";
            _buttonAddCommercialFiller.Size = new System.Drawing.Size(319, 27);
            _buttonAddCommercialFiller.TabIndex = 5;
            _buttonAddCommercialFiller.Text = "Add Commercial Filler";
            _buttonAddCommercialFiller.UseVisualStyleBackColor = true;
            _buttonAddCommercialFiller.Click += OnButtonClick;
            // 
            // _buttonAddVideoWithCriteria
            // 
            _buttonAddVideoWithCriteria.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonAddVideoWithCriteria.Location = new System.Drawing.Point(7, 539);
            _buttonAddVideoWithCriteria.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonAddVideoWithCriteria.Name = "_buttonAddVideoWithCriteria";
            _buttonAddVideoWithCriteria.Size = new System.Drawing.Size(319, 27);
            _buttonAddVideoWithCriteria.TabIndex = 2;
            _buttonAddVideoWithCriteria.Text = "Add Video From Folder";
            _buttonAddVideoWithCriteria.UseVisualStyleBackColor = true;
            _buttonAddVideoWithCriteria.Click += OnButtonClick;
            // 
            // _buttonAddRun
            // 
            _buttonAddRun.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonAddRun.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            _buttonAddRun.Location = new System.Drawing.Point(7, 572);
            _buttonAddRun.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonAddRun.Name = "_buttonAddRun";
            _buttonAddRun.Size = new System.Drawing.Size(319, 27);
            _buttonAddRun.TabIndex = 3;
            _buttonAddRun.Text = "Add Show Run";
            _buttonAddRun.UseVisualStyleBackColor = true;
            _buttonAddRun.Click += OnButtonClick;
            // 
            // _buttonRemoveContent
            // 
            _buttonRemoveContent.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _buttonRemoveContent.Enabled = false;
            _buttonRemoveContent.Location = new System.Drawing.Point(7, 674);
            _buttonRemoveContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _buttonRemoveContent.Name = "_buttonRemoveContent";
            _buttonRemoveContent.Size = new System.Drawing.Size(319, 27);
            _buttonRemoveContent.TabIndex = 6;
            _buttonRemoveContent.Text = "Remove Content from Time Block";
            _buttonRemoveContent.UseVisualStyleBackColor = true;
            _buttonRemoveContent.Click += OnButtonClick;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Type, ContentDescription });
            dataGridView2.Location = new System.Drawing.Point(7, 37);
            dataGridView2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new System.Drawing.Size(319, 463);
            dataGridView2.TabIndex = 1;
            dataGridView2.RowEnter += OnRowEnter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 9);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Content";
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.Location = new System.Drawing.Point(0, 0);
            splitContainer3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(splitContainer1);
            splitContainer3.Size = new System.Drawing.Size(967, 714);
            splitContainer3.SplitterDistance = 272;
            splitContainer3.SplitterWidth = 5;
            splitContainer3.TabIndex = 17;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer4.Location = new System.Drawing.Point(0, 0);
            splitContainer4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(_buttonMonday);
            splitContainer4.Panel1.Controls.Add(_buttonWednesday);
            splitContainer4.Panel1.Controls.Add(_buttonSunday);
            splitContainer4.Panel1.Controls.Add(_buttonFriday);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(_buttonTuesday);
            splitContainer4.Panel2.Controls.Add(_buttonSaturday);
            splitContainer4.Panel2.Controls.Add(_buttonCustomDate);
            splitContainer4.Panel2.Controls.Add(_buttonThursday);
            splitContainer4.Size = new System.Drawing.Size(272, 714);
            splitContainer4.SplitterDistance = 136;
            splitContainer4.SplitterWidth = 5;
            splitContainer4.TabIndex = 0;
            // 
            // Type
            // 
            Type.HeaderText = "Type";
            Type.Name = "Type";
            Type.ReadOnly = true;
            // 
            // ContentDescription
            // 
            ContentDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            ContentDescription.HeaderText = "ContentDescription";
            ContentDescription.Name = "ContentDescription";
            ContentDescription.ReadOnly = true;
            // 
            // ScheduleControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(967, 714);
            Controls.Add(splitContainer3);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ScheduleControl";
            Text = "Schedule Control";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            ResumeLayout(false);
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
        private System.Windows.Forms.Button _buttonContentDown;
        private System.Windows.Forms.Button _buttonContentUp;
        private System.Windows.Forms.Button _buttonCopyDay;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button _buttonEraseDay;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContentDescription;
    }
}