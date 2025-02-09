using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler.Controls
{
    public partial class CommercialForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CommercialFiller CommercialFiller { get; private set; }

        private readonly PersistenceManagers persistenceManagers;

        public CommercialForm(PersistenceManagers persistenceManagers)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.persistenceManagers = persistenceManagers;
            InitializeComponent();
            EnableFields(false);
            FillExistingCommercialFillers();
        }

        private void FillExistingCommercialFillers()
        {
            foreach (var commercialFiller in persistenceManagers.commercialFillerManager.GetCommercialFillers())
            {
                AddCommercialFillerRow(commercialFiller);
            }
        }

        private CommercialFiller GetSelectedCommercialFiller()
        {
            if (dataGridView1.CurrentRow == null)
            {
                return null;
            }
            CommercialFiller = (CommercialFiller)dataGridView1.CurrentRow.Tag;
            return CommercialFiller;
        }

        private void AddCommercialFillerRow(CommercialFiller commercialFiller)
        {
            var rowIndex = dataGridView1.Rows.Add(commercialFiller.Description);
            dataGridView1.Rows[rowIndex].Tag = commercialFiller;
        }

        private void ResetFields()
        {
            ClearFields();
            EnableFields(false);
        }

        private void ClearFields()
        {
            _textBoxDescription.Text = "";
        }

        private void EnableFields(bool enable)
        {
            _textBoxDescription.Enabled = enable;
            _buttonSave.Enabled = enable;
            _buttonBrowse.Enabled = enable;
        }

        private void OnRowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.Equals(dataGridView1))
            {
                ResetFields();

                if (GetSelectedCommercialFiller() != null)
                {
                    _buttonUseSelected.Enabled = true;
                }
                else
                {
                    _buttonUseSelected.Enabled = false;
                }
            }
        }

        private void OnButtonClick(object sender, System.EventArgs e)
        {
            if (sender.Equals(_buttonAddNew))
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
                            files.AddRange(Directory.GetFiles(path, "*.mkv", SearchOption.AllDirectories));

                            if (files.Count > 0)
                            {
                                var attributeTree = persistenceManagers._picker.GetVideoByPath(files[0]).AttributeTree;

                                if (attributeTree != null)
                                {
                                    var commercialFiller = new CommercialFiller();
                                    if (persistenceManagers.attributeTreeManager.GetAttributeTree(attributeTree.Guid) == null)
                                    {
                                        persistenceManagers.attributeTreeManager.AddNewTree(attributeTree);
                                    }

                                    commercialFiller.Attributes.Add(attributeTree.Guid);
                                    persistenceManagers.commercialFillerManager.AddOrUpdateCommercial(commercialFiller);

                                    int index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Tag = commercialFiller;
                                    dataGridView1.Rows[index].Selected = true;
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
            else if (sender.Equals(_buttonEditSelected))
            {
                EnableFields(true);
                if (GetSelectedCommercialFiller() == null)
                {
                    return;
                }
                _textBoxDescription.Text = GetSelectedCommercialFiller().Description;
            }
            else if (sender.Equals(_buttonUseSelected))
            {
                CommercialFiller = GetSelectedCommercialFiller();
                if (CommercialFiller != null)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                } else
                {
                    MessageBox.Show("Error retrieving commercial filler");
                }
            }
            else if (sender.Equals(_buttonBrowse))
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
                            files.AddRange(Directory.GetFiles(path, "*.mkv", SearchOption.AllDirectories));

                            if (files.Count > 0)
                            {
                                var attributeTree = persistenceManagers._picker.GetVideoByPath(files[0]).AttributeTree;
                                
                                if (attributeTree != null)
                                {
                                    if (persistenceManagers.attributeTreeManager.GetAttributeTree(attributeTree.Guid) == null)
                                    {
                                        persistenceManagers.attributeTreeManager.AddNewTree(attributeTree);
                                    }

                                    CommercialFiller.Attributes.Clear();
                                    CommercialFiller.Attributes.Add(attributeTree.Guid);

                                    persistenceManagers.commercialFillerManager.AddOrUpdateCommercial(CommercialFiller);

                                    dataGridView1.CurrentRow.Tag = CommercialFiller;
                                    dataGridView1.CurrentRow.Selected = true;
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
            else if (sender.Equals(_buttonSave))
            {
                CommercialFiller.Description = _textBoxDescription.Text;
                dataGridView1.CurrentRow.Tag = CommercialFiller;
                dataGridView1.CurrentRow.SetValues(CommercialFiller.Description);
                persistenceManagers.commercialFillerManager.AddOrUpdateCommercial(CommercialFiller);
                EnableFields(false);
            }
        }
    }
}
