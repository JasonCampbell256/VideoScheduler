using System;
using System.Globalization;
using System.Windows.Forms;
using VideoScheduler.Domain;

namespace VideoScheduler
{
    public partial class TimeBlockDialog : Form
    {
        private bool existing = false;

        public TimeBlock TimeBlock { get; private set; }

        public TimeBlockDialog()
        {
            InitializeComponent();
            PopulateComboBoxes();
            _comboBoxDayOfWeek.SelectedItem = DayOfWeek.Monday;
        }

        public TimeBlockDialog(DayOfWeek day)
        {
            InitializeComponent();
            PopulateComboBoxes();
            _dateTimePickerStart.Value = DateTime.Today;
            _dateTimePickerEnd.Value = DateTime.Today + new TimeSpan(0, 30, 0);
            _comboBoxDayOfWeek.SelectedItem = day;
        }

        public TimeBlockDialog(TimeBlock timeBlock)
        {
            InitializeComponent();
            PopulateComboBoxes();
            TimeBlock = timeBlock;
            _comboBoxDayOfWeek.SelectedItem = TimeBlock.Day;
            _dateTimePickerStart.Value = DateTime.Today.Add(TimeBlock.StartTime);
            _dateTimePickerEnd.Value = DateTime.Today.Add(TimeBlock.EndTime);
            _textBoxDescription.Text = TimeBlock.Description;
            existing = true;
        }

        public TimeBlockDialog(TimeBlock timeBlock, bool isCopy, string description = "")
        {
            InitializeComponent();
            PopulateComboBoxes();
            TimeBlock = timeBlock;
            _comboBoxDayOfWeek.SelectedItem = TimeBlock.Day;
            var oldStartTime = TimeBlock.StartTime;
            var oldEndTime = TimeBlock.EndTime;
            _dateTimePickerStart.Value = DateTime.Today.Add(oldEndTime);
            _dateTimePickerEnd.Value = DateTime.Today.Add(oldEndTime - oldStartTime + oldEndTime);
            _textBoxDescription.Text = description;
            existing = true;
        }

        private void PopulateComboBoxes()
        {
            _comboBoxDayOfWeek.DataSource = Enum.GetValues(typeof(DayOfWeek));
        }

        private bool ValidateTimeSpans()
        {
            if (_dateTimePickerStart.Value.TimeOfDay < _dateTimePickerEnd.Value.TimeOfDay)
            {
                return true;
            }

            return false;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonSave))
            {
                if (ValidateTimeSpans() == false)
                {
                    MessageBox.Show("Start time must be before end time!", "Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var selectedDay = (DayOfWeek)_comboBoxDayOfWeek.SelectedItem;
                var startTime = _dateTimePickerStart.Value.TimeOfDay;
                var endTime = _dateTimePickerEnd.Value.TimeOfDay;
                var description = _textBoxDescription.Text;

                if (!existing)
                {
                    TimeBlock = new TimeBlock(selectedDay, startTime, endTime, null, description);
                }
                else
                {
                    TimeBlock.Day = selectedDay;
                    TimeBlock.StartTime = startTime;
                    TimeBlock.EndTime = endTime;
                    TimeBlock.Description = description;
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (sender.Equals(_buttonCancel))
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void OnDateTimePickerValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(_dateTimePickerEnd))
            {
                if (_dateTimePickerEnd.Value.TimeOfDay == TimeSpan.Zero)
                {
                    _dateTimePickerEnd.Value = DateTime.Today + new TimeSpan(23, 59, 59);
                }
            }
        }
    }
}
