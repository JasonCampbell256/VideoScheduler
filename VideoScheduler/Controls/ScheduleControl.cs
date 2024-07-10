using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VideoScheduler.Controls;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler
{
    public partial class ScheduleControl : Form
    {
        private PersistenceManagers PersistenceManagers;
        private DayOfWeek dayOfWeek = DateTime.Today.DayOfWeek;

        public ScheduleControl(PersistenceManagers persistenceManagers)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.PersistenceManagers = persistenceManagers;
            InitializeComponent();
            ChangeDay(dayOfWeek);
        }

        private void ChangeDay(DayOfWeek day)
        {
            dayOfWeek = day;
            _labelDayOrDate.Text = dayOfWeek.ToString();
            LoadTimeBlocksForDay(dayOfWeek);
            _buttonMonday.Enabled = true;
            _buttonTuesday.Enabled = true;
            _buttonWednesday.Enabled = true;
            _buttonThursday.Enabled = true;
            _buttonFriday.Enabled = true;
            _buttonSaturday.Enabled = true;
            _buttonSunday.Enabled = true;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    _buttonMonday.Enabled = false;
                    break;
                case DayOfWeek.Tuesday:
                    _buttonTuesday.Enabled = false;
                    break;
                case DayOfWeek.Wednesday:
                    _buttonWednesday.Enabled = false;
                    break;
                case DayOfWeek.Thursday:
                    _buttonThursday.Enabled = false;
                    break;
                case DayOfWeek.Friday:
                    _buttonFriday.Enabled = false;
                    break;
                case DayOfWeek.Saturday:
                    _buttonSaturday.Enabled = false;
                    break;
                case DayOfWeek.Sunday:
                    _buttonSunday.Enabled = false;
                    break;
            }
        }

        private void LoadTimeBlocksForDay(DayOfWeek day)
        {
            ClearTimeBlocksRows();
            var timeBlocks = PersistenceManagers.timeBlockManager.GetTimeBlocks(day);
            foreach (var timeBlock in timeBlocks)
            {
                AddTimeBlockRow(timeBlock);
            }
        }

        private void ClearTimeBlocksRows()
        {
            dataGridView1.Rows.Clear();
        }

        private void AddTimeBlockRow(TimeBlock timeBlock)
        {
            var startTime = DateTime.Today.Add(timeBlock.StartTime).ToString("hh:mm tt");
            var endTime = DateTime.Today.Add(timeBlock.EndTime).ToString("hh:mm tt");
            int rowIndex = dataGridView1.Rows.Add(startTime, endTime, timeBlock.Description);
            dataGridView1.Rows[rowIndex].Tag = timeBlock;
        }

        private void LoadContent(TimeBlock timeBlock)
        {
            ClearContent();
            var contentRepository = new ContentRepository(PersistenceManagers._library);
            foreach (var content in timeBlock.ContentGuids)
            {
                var contentObject = contentRepository.GetContent(content);
                if (contentObject != null)
                {
                    var contentType = contentObject.GetType().Name;
                    if (contentObject.GetType() == typeof(AttributeNode))
                    {
                        contentType = "From Folder";
                    }
                    else if (contentObject.GetType() == typeof(ShowRun))
                    {
                        contentType = "Show";
                    }
                    else if (contentObject.GetType() == typeof(SchedulableMovie))
                    {
                        contentType = "Movie";
                    }
                    else if (contentObject.GetType() == typeof(CommercialFiller))
                    {
                        contentType = "Commercials";
                    }

                    var rowIndex = dataGridView2.Rows.Add(contentType, contentObject.Description);
                    dataGridView2.Rows[rowIndex].Tag = contentObject;
                }
            }
        }

        private void ClearContent()
        {
            dataGridView2.Rows.Clear();
        }

        #region Event Handlers
        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonRemoveTimeBlock))
            {
                var currentRow = dataGridView1.CurrentRow;
                if (currentRow.Tag is TimeBlock)
                {
                    // open a new dialog to ask the user if they are sure they want to delete the time block

                    var messageBox = MessageBox.Show("Are you sure you want to delete this time block?", "Delete Time Block", MessageBoxButtons.YesNo);
                    if (messageBox.Equals(DialogResult.Yes))
                    {
                        PersistenceManagers.timeBlockManager.RemoveTimeBlock((TimeBlock)currentRow.Tag);
                        dataGridView1.Rows.Remove(currentRow);
                        ClearContent();
                    }
                }
            }
            else if (sender.Equals(_buttonAddTimeBlock))
            {
                var dialog = new TimeBlockDialog(dayOfWeek);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(dialog.TimeBlock);
                    if (dialog.TimeBlock.Day == dayOfWeek)
                    {
                        LoadTimeBlocksForDay(dayOfWeek);
                    }
                }
            }
            else if (sender.Equals(_buttonEditTimeBlock))
            {
                var currentRow = dataGridView1.CurrentRow;
                var timeBlock = (TimeBlock)currentRow.Tag;
                if (timeBlock != null)
                {
                    var dialog = new TimeBlockDialog(timeBlock);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(dialog.TimeBlock);
                        LoadTimeBlocksForDay(dayOfWeek);
                    }
                }
            }
            else if (sender.Equals(_buttonCopyTimeBlock))
            {
                var currentRow = dataGridView1.CurrentRow;
                var timeBlock = (TimeBlock)currentRow.Tag;
                if (timeBlock != null)
                {
                    var newBlock = timeBlock;
                    newBlock.Guid = Guid.NewGuid();
                    var dialog = new TimeBlockDialog(newBlock, true);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(newBlock);
                        LoadTimeBlocksForDay(dayOfWeek);
                    }
                }
            }
            else if (sender.Equals(_buttonCopyTimeBlockToAnotherDay))
            {
                var currentRow = dataGridView1.CurrentRow;
                var timeBlock = (TimeBlock)currentRow.Tag;
                if (timeBlock != null)
                {
                    var dayPicker = new DayOfWeekPicker();
                    if (dayPicker.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var day in dayPicker.SelectedDays)
                        {
                            if (!day.Equals(dayOfWeek))
                            {
                                var newBlock = timeBlock;
                                newBlock.Guid = Guid.NewGuid();
                                newBlock.Day = day;
                                PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(newBlock);
                            }
                        }
                    }
                }
            }
            else if (sender.Equals(_buttonAddVideoWithCriteria))
            {
                //TODO this is a terrible way to do this, but I'm too lazy to do it another way at the moment. Revist this later.
                //Instead of allowing the user to select attributes, for now we're allowing them to pick a folder,
                //and we'll just extract an attribute tree from the first video file we find
                var timeBlock = (TimeBlock)dataGridView1.CurrentRow.Tag;
                if (timeBlock != null)
                {
                    using (var folderDialog = new FolderBrowserDialog())
                    {
                        folderDialog.Description = "Select a folder:";
                        folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            var path = folderDialog.SelectedPath;

                            try
                            {
                                var files = Directory.GetFiles(path, "*.mp4", SearchOption.AllDirectories).ToList();
                                files.AddRange( Directory.GetFiles(path, "*.mkv", SearchOption.AllDirectories));

                                if (files.Count > 0)
                                {
                                    var attributeTree = PersistenceManagers._picker.GetVideoByPath(files[0]).AttributeTree;
                                    
                                    if (attributeTree != null)
                                    {
                                        if (PersistenceManagers.attributeTreeManager.GetAttributeTree(attributeTree.Guid) == null)
                                        {
                                            PersistenceManagers.attributeTreeManager.AddNewTree(attributeTree);
                                        }
                                        timeBlock.ContentGuids.Add(attributeTree.Guid);
                                        PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(timeBlock);
                                        dataGridView1.CurrentRow.Tag = timeBlock;
                                        LoadContent(timeBlock);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error reading files from folder");
                            }
                        }
                    }

                }
            }
            else if (sender.Equals(_buttonAddRun))
            {
                var timeBlock = (TimeBlock)dataGridView1.CurrentRow.Tag;
                var dialog = new ShowRunControl(PersistenceManagers);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (timeBlock != null)
                    {
                        timeBlock.ContentGuids.Add(dialog.ShowRun.Guid);
                        PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(timeBlock);
                        dataGridView1.CurrentRow.Tag = timeBlock;       
                    }
                }
                LoadContent(timeBlock);
            }
            else if (sender.Equals(_buttonAddMovie))
            {
                var timeBlock = (TimeBlock)dataGridView1.CurrentRow.Tag;
                var dialog = new MovieControl(PersistenceManagers);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (timeBlock != null)
                    {
                        timeBlock.ContentGuids.Add(dialog.Movie.Guid);
                        PersistenceManagers.movieManager.AddOrUpdateMovieRun(dialog.Movie);
                        PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(timeBlock);
                        dataGridView1.CurrentRow.Tag = timeBlock;
                    }
                }
                LoadContent(timeBlock);
            }
            else if (sender.Equals(_buttonAddCommercialFiller))
            {
                //TODO this is a terrible way to do this, but I'm too lazy to do it another way at the moment. Revist this later.
                //Instead of allowing the user to select attributes, for now we're allowing them to pick a folder,
                //and we'll just extract an attribute tree from the first video file we find
                var timeBlock = (TimeBlock)dataGridView1.CurrentRow.Tag;
                if (timeBlock != null)
                {
                    var dialog = new CommercialForm(PersistenceManagers);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (dialog.CommercialFiller != null)
                        {
                            timeBlock.ContentGuids.Add(dialog.CommercialFiller.Guid);
                            PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(timeBlock);
                            dataGridView1.CurrentRow.Tag = timeBlock;
                            LoadContent(timeBlock);
                        }
                    }
                    //using (var folderDialog = new FolderBrowserDialog())
                    //{
                    //    folderDialog.Description = "Select a folder:";
                    //    folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                    //    if (folderDialog.ShowDialog() == DialogResult.OK)
                    //    {
                    //        var path = folderDialog.SelectedPath;

                    //        try
                    //        {
                    //            var files = Directory.GetFiles(path, "*.mp4", SearchOption.AllDirectories).ToList();
                    //            files.AddRange(Directory.GetFiles(path, "*.mkv", SearchOption.AllDirectories));

                    //            if (files.Count > 0)
                    //            {
                    //                var attributeTree = PersistenceManagers._picker.GetVideoByPath(files[0]).AttributeTree;

                    //                if (attributeTree != null)
                    //                {
                    //                    var commercialFiller = new CommercialFiller();
                    //                    if (PersistenceManagers.attributeTreeManager.GetAttributeTree(attributeTree.Guid) == null)
                    //                    {
                    //                        PersistenceManagers.attributeTreeManager.AddNewTree(attributeTree);
                    //                    }

                    //                    commercialFiller.Attributes.Add(attributeTree.Guid);
                    //                    PersistenceManagers.commercialFillerManager.AddOrUpdateCommercial(commercialFiller);
                    //                    timeBlock.ContentGuids.Add(commercialFiller.Guid);
                    //                    PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(timeBlock);
                    //                    dataGridView1.CurrentRow.Tag = timeBlock;
                    //                    LoadContent(timeBlock);
                    //                }
                    //            }
                    //        }
                    //        catch (Exception)
                    //        {
                    //            MessageBox.Show("Error reading files from folder");
                    //        }
                    //    }
                    //}

                }
            }
            else if (sender.Equals(_buttonRemoveContent))
            {
                var timeBlock = (TimeBlock)dataGridView1.CurrentRow.Tag;
                if (timeBlock != null)
                {
                    if (dataGridView2.CurrentRow.Tag is ISchedulableContent)
                    {
                        var messageBox = MessageBox.Show("Are you sure you want to remove this content?", "Remove Content", MessageBoxButtons.YesNo);
                        if (messageBox == DialogResult.No)
                        {
                            return;
                        }
                        var guid = (dataGridView2.CurrentRow.Tag as ISchedulableContent).Guid;
                        timeBlock.ContentGuids.Remove(guid);
                        PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(timeBlock);
                        dataGridView1.CurrentRow.Tag = timeBlock;
                    }
                }
                LoadContent(timeBlock);
            } else if (sender.Equals(_buttonContentUp))
            {
                var timeBlock = (TimeBlock)dataGridView1.CurrentRow.Tag;
                if (timeBlock != null)
                {
                    if (dataGridView2.CurrentRow.Tag is ISchedulableContent)
                    {
                        var guid = (dataGridView2.CurrentRow.Tag as ISchedulableContent).Guid;
                        var contentIndex = timeBlock.ContentGuids.IndexOf(guid);
                        
                        if (contentIndex != 0)
                        {
                            timeBlock.ContentGuids.RemoveAt(contentIndex);
                            timeBlock.ContentGuids.Insert(contentIndex -1, guid);
                        }
                        PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(timeBlock);
                        dataGridView1.CurrentRow.Tag = timeBlock;
                        LoadContent(timeBlock);
                        var newGridViewIndex = dataGridView2.Rows.Cast<DataGridViewRow>().Where(x => (x.Tag as ISchedulableContent).Guid == guid).First().Index;
                        dataGridView2.CurrentCell = dataGridView2.Rows[newGridViewIndex].Cells[0];
                    }
                }
            } else if (sender.Equals(_buttonContentDown))
            {
                var timeBlock = (TimeBlock)dataGridView1.CurrentRow.Tag;
                if (timeBlock != null)
                {
                    if (dataGridView2.CurrentRow.Tag is ISchedulableContent)
                    {
                        var guid = (dataGridView2.CurrentRow.Tag as ISchedulableContent).Guid;
                        var contentIndex = timeBlock.ContentGuids.IndexOf(guid);

                        if (contentIndex < timeBlock.ContentGuids.Count -1)
                        {
                            timeBlock.ContentGuids.RemoveAt(contentIndex);
                            timeBlock.ContentGuids.Insert(contentIndex + 1, guid);
                        }
                        PersistenceManagers.timeBlockManager.AddOrUpdateTimeBlock(timeBlock);
                        dataGridView1.CurrentRow.Tag = timeBlock;
                        LoadContent(timeBlock);
                        var newGridViewIndex = dataGridView2.Rows.Cast<DataGridViewRow>().Where(x => (x.Tag as ISchedulableContent).Guid == guid).First().Index;
                        dataGridView2.CurrentCell = dataGridView2.Rows[newGridViewIndex].Cells[0];
                    }
                }
            }
        }

        private void OnDayButtonClick(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();
            if (sender.Equals(_buttonMonday))
            {
                ChangeDay(DayOfWeek.Monday);
            }
            else if (sender.Equals(_buttonTuesday))
            {
                ChangeDay(DayOfWeek.Tuesday);
            }
            else if (sender.Equals(_buttonWednesday))
            {
                ChangeDay(DayOfWeek.Wednesday);
            }
            else if (sender.Equals(_buttonThursday))
            {
                ChangeDay(DayOfWeek.Thursday);
            }
            else if (sender.Equals(_buttonFriday))
            {
                ChangeDay(DayOfWeek.Friday);
            }
            else if (sender.Equals(_buttonSaturday))
            {
                ChangeDay(DayOfWeek.Saturday);
            }
            else if (sender.Equals(_buttonSunday))
            {
                ChangeDay(DayOfWeek.Sunday);
            }
            else if (sender.Equals(_buttonCustomDate))
            {
                //TODO
            }
        }

        private void OnRowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.Equals(dataGridView1))
            {
                var timeBlock = (TimeBlock)dataGridView1.Rows[e.RowIndex].Tag;
                if (timeBlock != null)
                {
                    LoadContent(timeBlock);
                }
            } else if (sender.Equals(dataGridView2))
            {
                var content = (ISchedulableContent)dataGridView2.Rows[e.RowIndex].Tag;
                if (content != null)
                {
                    _buttonRemoveContent.Enabled = true;
                }
            }
        }
        #endregion
    }
}
