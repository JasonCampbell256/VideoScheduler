using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler
{
    public partial class ShowRunControl : Form
    {
        public ShowRun ShowRun { get; private set; }

        private PersistenceManagers PersistenceManagers;

        public ShowRunControl(PersistenceManagers persistenceManagers)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.PersistenceManagers = persistenceManagers;
            InitializeComponent();
            EnableFields(false);
            _comboBoxShow.Items.AddRange(PersistenceManagers._library.Shows.Select(s => s.Title).ToArray());
            FillExistingRuns();
        }

        private void FillExistingRuns()
        {
            foreach (var run in PersistenceManagers.runManager.GetRuns())
            {
                AddShowRunRow(run);
            }
        }

        private ShowRun GetSelectedRun() 
        {
            if (dataGridView1.CurrentRow == null)
            {
                return null;
            }
            return (ShowRun)dataGridView1.CurrentRow.Tag;
        }

        private ShowRun GetSelectedRun(int index)
        {
            return (ShowRun)dataGridView1.Rows[index].Tag;
        }

        private void AddShowRunRow(ShowRun showRun)
        {
            var rowIndex = dataGridView1.Rows.Add(showRun.GetShowTitle(), showRun.NextEpisode.Season.ToString(), showRun.NextEpisode, showRun.Description);
            dataGridView1.Rows[rowIndex].Tag = showRun;
        }

        private void FillSeasonComboBox()
        {
            _comboBoxSeason.Items.Clear();
            if (_comboBoxShow.SelectedItem == null)
            {
                _comboBoxSeason.Enabled = false;
                return;
            }
            _comboBoxSeason.Enabled = true;

            var show = PersistenceManagers._library.GetShow(_comboBoxShow.SelectedItem.ToString());
            foreach (var season in show.Seasons)
            {
                _comboBoxSeason.Items.Add(season.SeasonNumber);
            }

            _comboBoxSeason.SelectedIndex = 0;
            FillEpisodeComboBox();
        }

        private void FillEpisodeComboBox()
        {
            _comboBoxEpisode.Items.Clear();
            _comboBoxEpisode.Enabled = true;
            var show = PersistenceManagers._library.GetShow(_comboBoxShow.SelectedItem.ToString());
            var season = show.Seasons.Where(s => s.SeasonNumber == (int)_comboBoxSeason.SelectedItem).FirstOrDefault();

            _comboBoxEpisode.Items.AddRange(season.Episodes.ToArray());
            if (_comboBoxEpisode.Items.Count > 0)
            {
                if (GetSelectedRun() != null)
                {
                    _comboBoxEpisode.SelectedItem = GetSelectedRun().NextEpisode;
                } else
                {
                    _comboBoxEpisode.SelectedIndex = 0;
                }
            }
        }

        private void ResetFields()
        {
            ClearFields();
            EnableFields(false);
        }

        private void ClearFields()
        {
            _comboBoxShow.SelectedItem = null;
            _comboBoxEpisode.Items.Clear();
            _comboBoxSeason.Items.Clear();
            _textBoxDescription.Text = "";
        }

        private void EnableFields(bool enable)
        {
            _comboBoxShow.Enabled = enable;
            _comboBoxSeason.Enabled = enable;
            _comboBoxEpisode.Enabled = enable;
            _textBoxDescription.Enabled = enable;
        }

        private void OnRowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.Equals(dataGridView1))
            {
                ResetFields();
                
                if (GetSelectedRun(e.RowIndex) != null)
                {
                    _buttonUseSelected.Enabled = true;
                } else
                {
                    _buttonUseSelected.Enabled = false;
                }
            }
        }

        private bool ValidateFields()
        {
            if (_comboBoxShow.SelectedIndex == -1 || _comboBoxSeason.SelectedIndex == -1 || _comboBoxEpisode.SelectedIndex == -1)
            {
                return false;
            }

            var show = PersistenceManagers._library.GetShow(_comboBoxShow.SelectedItem.ToString());
            if (show == null)
            {
                return false;
            }

            var season = show.Seasons[_comboBoxSeason.SelectedIndex];
            if (season == null)
            {
                return false;
            }

            var episode = season.Episodes[_comboBoxEpisode.SelectedIndex];
            if (episode == null)
            {
                return false;
            }

            var newRun = new ShowRun(_textBoxDescription.Text, episode);
            newRun.Description = _textBoxDescription.Text;
            if (GetSelectedRun() != null)
            {
                newRun.Guid = GetSelectedRun().Guid;
            }
            dataGridView1.CurrentRow.Tag = newRun;
            dataGridView1.CurrentRow.SetValues(newRun.GetShowTitle(), newRun.NextEpisode.Season.ToString(), newRun.NextEpisode.EpisodeNumber, newRun.Description);
            PersistenceManagers.runManager.AddOrUpdateShowRun(newRun);
            return true;
        }

        private void OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender.Equals(_comboBoxShow))
            {
                FillSeasonComboBox();
            }
            else if (sender.Equals(_comboBoxSeason))
            {
                FillEpisodeComboBox();
            }
        }

        private void EditSelected()
        {
            ClearFields();
            EnableFields(true);
            if (GetSelectedRun() == null)
            {
                return;
            }
            _comboBoxShow.SelectedItem = GetSelectedRun().NextEpisode.Season.Show.Title;
            _comboBoxSeason.SelectedItem = GetSelectedRun().NextEpisode.Season.SeasonNumber;
        }

        private void OnButtonClick(object sender, System.EventArgs e)
        {
            if (sender.Equals(_buttonAddNew))
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.CurrentCell = dataGridView1[0, index];
                EditSelected();
            }
            else if (sender.Equals(_buttonEditSelected))
            {
                EditSelected();
            }
            else if (sender.Equals(_buttonUseSelected))
            {
                ShowRun = GetSelectedRun();
                if (ShowRun != null)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                } else
                {
                    MessageBox.Show("Error retrieving run");
                }
            }
            else if (sender.Equals(_buttonSave))
            {
                if (!ValidateFields())
                {
                    MessageBox.Show("Error saving run");
                } else
                {
                    EnableFields(false);
                }
            }
        }
    }
}
