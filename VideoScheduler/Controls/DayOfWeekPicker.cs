using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoScheduler
{
    public partial class DayOfWeekPicker : Form
    {
        public List<DayOfWeek> SelectedDays { get; set; }

        public DayOfWeekPicker()
        {
            InitializeComponent();
            SelectedDays = new List<DayOfWeek>();
        }


        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonOK))
            {
                SelectedDays.Clear();
                if (_checkBoxMonday.Checked)
                {
                    SelectedDays.Add(DayOfWeek.Monday);
                }
                if (_checkBoxTuesday.Checked)
                {
                    SelectedDays.Add(DayOfWeek.Tuesday);
                }
                if (_checkBoxWednesday.Checked)
                {
                    SelectedDays.Add(DayOfWeek.Wednesday);
                }
                if (_checkBoxThursday.Checked)
                {
                    SelectedDays.Add(DayOfWeek.Thursday);
                }
                if (_checkBoxFriday.Checked)
                {
                    SelectedDays.Add(DayOfWeek.Friday);
                }
                if (_checkBoxSaturday.Checked)
                {
                    SelectedDays.Add(DayOfWeek.Saturday);
                }
                if (_checkBoxSunday.Checked)
                {
                    SelectedDays.Add(DayOfWeek.Sunday);
                }
                DialogResult = DialogResult.OK;
            }
            else if (sender.Equals(_buttonCancel))
            {
                SelectedDays.Clear();
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
