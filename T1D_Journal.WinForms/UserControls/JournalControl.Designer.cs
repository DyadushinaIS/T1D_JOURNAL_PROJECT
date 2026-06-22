namespace T1D_Journal.WinForms.UserControls
{
	partial class JournalControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelControl = new Panel();
            buttonRefresh = new Button();
            buttonFilter = new Button();
            dateTimePickerTo = new DateTimePicker();
            label2 = new Label();
            dateTimePickerFrom = new DateTimePicker();
            label1 = new Label();
            dataGridViewJournal = new DataGridView();
            panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewJournal).BeginInit();
            SuspendLayout();
            // 
            // panelControl
            // 
            panelControl.Controls.Add(buttonRefresh);
            panelControl.Controls.Add(buttonFilter);
            panelControl.Controls.Add(dateTimePickerTo);
            panelControl.Controls.Add(label2);
            panelControl.Controls.Add(dateTimePickerFrom);
            panelControl.Controls.Add(label1);
            panelControl.Dock = DockStyle.Top;
            panelControl.Location = new Point(0, 0);
            panelControl.Margin = new Padding(3, 4, 3, 4);
            panelControl.Name = "panelControl";
            panelControl.Size = new Size(947, 53);
            panelControl.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(773, 9);
            buttonRefresh.Margin = new Padding(3, 4, 3, 4);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(137, 40);
            buttonRefresh.TabIndex = 5;
            buttonRefresh.Text = "Сбросить фильтр";
            buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // buttonFilter
            // 
            buttonFilter.Location = new Point(611, 9);
            buttonFilter.Margin = new Padding(3, 4, 3, 4);
            buttonFilter.Name = "buttonFilter";
            buttonFilter.Size = new Size(154, 40);
            buttonFilter.TabIndex = 4;
            buttonFilter.Text = "Применить фильтр";
            buttonFilter.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.CustomFormat = "dd.MM.yyyy";
            dateTimePickerTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerTo.Location = new Point(360, 12);
            dateTimePickerTo.Margin = new Padding(3, 4, 3, 4);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(228, 27);
            dateTimePickerTo.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(310, 20);
            label2.Name = "label2";
            label2.Size = new Size(30, 20);
            label2.TabIndex = 2;
            label2.Text = "по:";
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.CustomFormat = "dd.MM.yyyy";
            dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerFrom.Location = new Point(58, 12);
            dateTimePickerFrom.Margin = new Padding(3, 4, 3, 4);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(228, 27);
            dateTimePickerFrom.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 20);
            label1.Name = "label1";
            label1.Size = new Size(21, 20);
            label1.TabIndex = 0;
            label1.Text = "С:";
            // 
            // dataGridViewJournal
            // 
            dataGridViewJournal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewJournal.Location = new Point(3, 68);
            dataGridViewJournal.Margin = new Padding(3, 4, 3, 4);
            dataGridViewJournal.Name = "dataGridViewJournal";
            dataGridViewJournal.RowHeadersWidth = 51;
            dataGridViewJournal.Size = new Size(922, 732);
            dataGridViewJournal.TabIndex = 7;
            // 
            // JournalControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelControl);
            Controls.Add(dataGridViewJournal);
            Margin = new Padding(3, 4, 3, 4);
            Name = "JournalControl";
            Size = new Size(947, 800);
            panelControl.ResumeLayout(false);
            panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewJournal).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelControl;
		private DataGridView dataGridViewJournal;
		private DateTimePicker dateTimePickerFrom;
		private Label label1;
		private Button buttonRefresh;
		private Button buttonFilter;
		private DateTimePicker dateTimePickerTo;
		private Label label2;
	}
}
