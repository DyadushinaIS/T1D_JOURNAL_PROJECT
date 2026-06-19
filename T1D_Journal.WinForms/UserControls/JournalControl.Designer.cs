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
			panelControl.Name = "panelControl";
			panelControl.Size = new Size(829, 40);
			panelControl.TabIndex = 0;
			// 
			// buttonRefresh
			// 
			buttonRefresh.Location = new Point(676, 7);
			buttonRefresh.Name = "buttonRefresh";
			buttonRefresh.Size = new Size(120, 30);
			buttonRefresh.TabIndex = 5;
			buttonRefresh.Text = "Обновить";
			buttonRefresh.UseVisualStyleBackColor = true;
			// 
			// buttonFilter
			// 
			buttonFilter.Location = new Point(535, 7);
			buttonFilter.Name = "buttonFilter";
			buttonFilter.Size = new Size(135, 30);
			buttonFilter.TabIndex = 4;
			buttonFilter.Text = "Применить фильтр";
			buttonFilter.UseVisualStyleBackColor = true;
			// 
			// dateTimePickerTo
			// 
			dateTimePickerTo.CustomFormat = "dd.MM.yyyy";
			dateTimePickerTo.Format = DateTimePickerFormat.Custom;
			dateTimePickerTo.Location = new Point(315, 9);
			dateTimePickerTo.Name = "dateTimePickerTo";
			dateTimePickerTo.Size = new Size(200, 23);
			dateTimePickerTo.TabIndex = 3;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(271, 15);
			label2.Name = "label2";
			label2.Size = new Size(24, 15);
			label2.TabIndex = 2;
			label2.Text = "по:";
			// 
			// dateTimePickerFrom
			// 
			dateTimePickerFrom.CustomFormat = "dd.MM.yyyy";
			dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
			dateTimePickerFrom.Location = new Point(51, 9);
			dateTimePickerFrom.Name = "dateTimePickerFrom";
			dateTimePickerFrom.Size = new Size(200, 23);
			dateTimePickerFrom.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(13, 15);
			label1.Name = "label1";
			label1.Size = new Size(18, 15);
			label1.TabIndex = 0;
			label1.Text = "С:";
			// 
			// dataGridViewJournal
			// 
			dataGridViewJournal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewJournal.Location = new Point(3, 51);
			dataGridViewJournal.Name = "dataGridViewJournal";
			dataGridViewJournal.Size = new Size(807, 549);
			dataGridViewJournal.TabIndex = 7;
			dataGridViewJournal.CellContentClick += dataGridViewJournal_CellContentClick;
			// 
			// JournalControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(panelControl);
			Controls.Add(dataGridViewJournal);
			Name = "JournalControl";
			Size = new Size(829, 600);
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
