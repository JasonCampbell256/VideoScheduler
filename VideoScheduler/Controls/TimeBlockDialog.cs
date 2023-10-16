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
            _comboBoxDayOfWeek.SelectedItem = day;
        }

        public TimeBlockDialog(TimeBlock timeBlock)
        {
            InitializeComponent();
            PopulateComboBoxes();
            TimeBlock = timeBlock;
            _comboBoxDayOfWeek.SelectedItem = TimeBlock.Day;
            _textBoxStartTime.Text = DateTime.Today.Add(TimeBlock.StartTime).ToString("hh:mm tt");
            _textBoxEndTime.Text = DateTime.Today.Add(TimeBlock.EndTime).ToString("hh:mm tt");
            existing = true;
        }

        private void PopulateComboBoxes()
        {
            _comboBoxDayOfWeek.DataSource = Enum.GetValues(typeof(DayOfWeek));
        }

        private bool ValidateTimeSpans()
        {
            if (!DateTime.TryParseExact(_textBoxStartTime.Text, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startTime))
            {
                MessageBox.Show("Invalid start time.");
                return false;
            }
            if (!DateTime.TryParseExact(_textBoxEndTime.Text, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endTime))
            {
                MessageBox.Show("Invalid end time.");
                return false;
            }
            return true;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonSave))
            {
                if (ValidateTimeSpans() == false)
                {
                    return;
                }

                var selectedDay = (DayOfWeek)_comboBoxDayOfWeek.SelectedItem;
                var startTime = DateTime.ParseExact(_textBoxStartTime.Text, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None).TimeOfDay;
                var endTime = DateTime.ParseExact(_textBoxEndTime.Text, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None).TimeOfDay;


                if (!existing)
                {
                    TimeBlock = new TimeBlock(selectedDay, startTime, endTime);
                } else
                {
                    TimeBlock.Day = selectedDay;
                    TimeBlock.StartTime = startTime;
                    TimeBlock.EndTime = endTime;
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
    }
}
