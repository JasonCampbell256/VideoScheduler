using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace VideoScheduler
{
    public partial class DayOfWeekPicker : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<DayOfWeek> SelectedDays { get; set; }
        private Dictionary<DayOfWeek, CheckBox> DayCheckboxes { get; set; }

        public DayOfWeekPicker(DayOfWeek? sourceDay = null)
        {
            InitializeComponent();
            SelectedDays = new List<DayOfWeek>();
            DayCheckboxes = new Dictionary<DayOfWeek, CheckBox>()
            {
                { DayOfWeek.Monday, _checkBoxMonday },
                { DayOfWeek.Tuesday, _checkBoxTuesday },
                { DayOfWeek.Wednesday, _checkBoxWednesday },
                { DayOfWeek.Thursday, _checkBoxThursday },
                { DayOfWeek.Friday, _checkBoxFriday },
                { DayOfWeek.Saturday, _checkBoxSaturday },
                { DayOfWeek.Sunday, _checkBoxSunday }
            };
            if (sourceDay != null)
            {
                DayCheckboxes[sourceDay.Value].Enabled = false;
            }
        }


        private void UpdateSelectedDays()
        {
            SelectedDays.Clear();

            foreach (DayOfWeek day in DayCheckboxes.Keys)
            {
                if (DayCheckboxes[day].Checked && DayCheckboxes[day].Enabled)
                {
                    SelectedDays.Add(day);
                }
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonOK))
            {
                UpdateSelectedDays();  
                DialogResult = DialogResult.OK;
            }
            else if (sender.Equals(_buttonCancel))
            {
                SelectedDays.Clear();
                DialogResult = DialogResult.Cancel;
            }
            else if (sender.Equals(_buttonSelectAll))
            {
                foreach(var checkbox in DayCheckboxes.Values)
                {
                    if (checkbox.Enabled)
                    {
                        checkbox.Checked = true;
                    }
                }
            }
            else if (sender.Equals(_buttonDeselectAll))
            {
                foreach (var checkbox in DayCheckboxes.Values)
                {
                    if (checkbox.Enabled)
                    {
                        checkbox.Checked = false;
                    }
                }
            }
        }
    }
}
