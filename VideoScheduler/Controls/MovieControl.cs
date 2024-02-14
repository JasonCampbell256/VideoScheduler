using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler.Controls
{
    public partial class MovieControl : Form
    {
        public SchedulableMovie Movie { get; set; }

        private PersistenceManagers persistenceManagers;

        public MovieControl(PersistenceManagers persistenceManagers)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.persistenceManagers = persistenceManagers;
            InitializeComponent();
            _buttonSelect.Enabled = false;
            comboBox1.Items.AddRange(persistenceManagers._library.Movies.Select(m => m.FileName).ToArray());
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(_buttonSelect))
            {
                DialogResult = DialogResult.OK;
                Close();
            } else if (sender.Equals(_buttonCancel))
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedValue = comboBox1.SelectedItem.ToString();
            if (String.IsNullOrEmpty(selectedValue))
            {
                Movie = null;
                _buttonSelect.Enabled = false;
            } else
            {
                //TODO this assumes no movies will ever have the same filename, which is fine for now, but may be an incorrect assumption in the future.
                var movie = persistenceManagers._library.Movies.FirstOrDefault(m => m.FileName == selectedValue);
                if (movie != null)
                {
                    Movie = new SchedulableMovie(movie);
                    Movie.Description = movie.FileName;
                    _buttonSelect.Enabled = true;
                } else {
                    _buttonSelect.Enabled = false;
                }
            }
        }
    }
}
