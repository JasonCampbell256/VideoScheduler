namespace VideoScheduler.Controls
{
    partial class CommercialForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this._buttonSave = new System.Windows.Forms.Button();
            this._textBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._buttonEditSelected = new System.Windows.Forms.Button();
            this._buttonUseSelected = new System.Windows.Forms.Button();
            this._buttonAddNew = new System.Windows.Forms.Button();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._buttonBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(517, 244);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnRowEnter);
            // 
            // _buttonSave
            // 
            this._buttonSave.Location = new System.Drawing.Point(541, 186);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(258, 23);
            this._buttonSave.TabIndex = 15;
            this._buttonSave.Text = "Save";
            this._buttonSave.UseVisualStyleBackColor = true;
            this._buttonSave.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _textBoxDescription
            // 
            this._textBoxDescription.Location = new System.Drawing.Point(622, 12);
            this._textBoxDescription.Multiline = true;
            this._textBoxDescription.Name = "_textBoxDescription";
            this._textBoxDescription.Size = new System.Drawing.Size(166, 92);
            this._textBoxDescription.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(538, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Description:";
            // 
            // _buttonEditSelected
            // 
            this._buttonEditSelected.Location = new System.Drawing.Point(12, 292);
            this._buttonEditSelected.Name = "_buttonEditSelected";
            this._buttonEditSelected.Size = new System.Drawing.Size(517, 23);
            this._buttonEditSelected.TabIndex = 18;
            this._buttonEditSelected.Text = "Edit Selected";
            this._buttonEditSelected.UseVisualStyleBackColor = true;
            this._buttonEditSelected.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonUseSelected
            // 
            this._buttonUseSelected.Location = new System.Drawing.Point(12, 321);
            this._buttonUseSelected.Name = "_buttonUseSelected";
            this._buttonUseSelected.Size = new System.Drawing.Size(517, 23);
            this._buttonUseSelected.TabIndex = 17;
            this._buttonUseSelected.Text = "Use Selected";
            this._buttonUseSelected.UseVisualStyleBackColor = true;
            this._buttonUseSelected.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // _buttonAddNew
            // 
            this._buttonAddNew.Location = new System.Drawing.Point(12, 262);
            this._buttonAddNew.Name = "_buttonAddNew";
            this._buttonAddNew.Size = new System.Drawing.Size(517, 23);
            this._buttonAddNew.TabIndex = 16;
            this._buttonAddNew.Text = "Add New";
            this._buttonAddNew.UseVisualStyleBackColor = true;
            this._buttonAddNew.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // _buttonBrowse
            // 
            this._buttonBrowse.Location = new System.Drawing.Point(541, 110);
            this._buttonBrowse.Name = "_buttonBrowse";
            this._buttonBrowse.Size = new System.Drawing.Size(258, 23);
            this._buttonBrowse.TabIndex = 19;
            this._buttonBrowse.Text = "Browse";
            this._buttonBrowse.UseVisualStyleBackColor = true;
            this._buttonBrowse.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // CommercialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._buttonBrowse);
            this.Controls.Add(this._buttonEditSelected);
            this.Controls.Add(this._buttonUseSelected);
            this.Controls.Add(this._buttonAddNew);
            this.Controls.Add(this._buttonSave);
            this.Controls.Add(this._textBoxDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CommercialForm";
            this.Text = "CommercialForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button _buttonSave;
        private System.Windows.Forms.TextBox _textBoxDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button _buttonEditSelected;
        private System.Windows.Forms.Button _buttonUseSelected;
        private System.Windows.Forms.Button _buttonAddNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Button _buttonBrowse;
    }
}