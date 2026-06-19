namespace T1D_Journal.WinForms.UserControls
{
	partial class DataEntryControl
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
			labelDateTime = new Label();
			labelGlucose = new Label();
			labelMealTag = new Label();
			labelNote = new Label();
			dateTimePickerReading = new DateTimePicker();
			numericUpDownGlucose = new NumericUpDown();
			comboBoxMealTag = new ComboBox();
			textBoxNote = new TextBox();
			buttonSaveReading = new Button();
			buttonClear = new Button();
			((System.ComponentModel.ISupportInitialize)numericUpDownGlucose).BeginInit();
			SuspendLayout();
			// 
			// labelDateTime
			// 
			labelDateTime.AutoSize = true;
			labelDateTime.Location = new Point(76, 73);
			labelDateTime.Name = "labelDateTime";
			labelDateTime.Size = new Size(82, 15);
			labelDateTime.TabIndex = 0;
			labelDateTime.Text = "Дата и время:";
			// 
			// labelGlucose
			// 
			labelGlucose.AutoSize = true;
			labelGlucose.Location = new Point(76, 119);
			labelGlucose.Name = "labelGlucose";
			labelGlucose.Size = new Size(118, 15);
			labelGlucose.TabIndex = 1;
			labelGlucose.Text = "Глюкоза (ммоль/л):";
			// 
			// labelMealTag
			// 
			labelMealTag.AutoSize = true;
			labelMealTag.Location = new Point(76, 165);
			labelMealTag.Name = "labelMealTag";
			labelMealTag.Size = new Size(41, 15);
			labelMealTag.TabIndex = 2;
			labelMealTag.Text = "Когда:";
			// 
			// labelNote
			// 
			labelNote.AutoSize = true;
			labelNote.Location = new Point(76, 211);
			labelNote.Name = "labelNote";
			labelNote.Size = new Size(81, 15);
			labelNote.TabIndex = 3;
			labelNote.Text = "Примечание:";
			// 
			// dateTimePickerReading
			// 
			dateTimePickerReading.CustomFormat = "dd.MM.yyyy HH:mm";
			dateTimePickerReading.Format = DateTimePickerFormat.Custom;
			dateTimePickerReading.Location = new Point(288, 73);
			dateTimePickerReading.Name = "dateTimePickerReading";
			dateTimePickerReading.Size = new Size(200, 23);
			dateTimePickerReading.TabIndex = 4;
			// 
			// numericUpDownGlucose
			// 
			numericUpDownGlucose.DecimalPlaces = 1;
			numericUpDownGlucose.Location = new Point(288, 118);
			numericUpDownGlucose.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
			numericUpDownGlucose.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numericUpDownGlucose.Name = "numericUpDownGlucose";
			numericUpDownGlucose.Size = new Size(200, 23);
			numericUpDownGlucose.TabIndex = 5;
			numericUpDownGlucose.Value = new decimal(new int[] { 55, 0, 0, 65536 });
			// 
			// comboBoxMealTag
			// 
			comboBoxMealTag.FormattingEnabled = true;
			comboBoxMealTag.Items.AddRange(new object[] { "BeforeMeal", "AfterMMeal", "Fasting", "Night" });
			comboBoxMealTag.Location = new Point(288, 163);
			comboBoxMealTag.Name = "comboBoxMealTag";
			comboBoxMealTag.Size = new Size(200, 23);
			comboBoxMealTag.TabIndex = 6;
			// 
			// textBoxNote
			// 
			textBoxNote.Location = new Point(288, 208);
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new Size(200, 23);
			textBoxNote.TabIndex = 7;
			// 
			// buttonSaveReading
			// 
			buttonSaveReading.Location = new Point(76, 269);
			buttonSaveReading.Name = "buttonSaveReading";
			buttonSaveReading.Size = new Size(200, 46);
			buttonSaveReading.TabIndex = 8;
			buttonSaveReading.Text = "Сохранить замер";
			buttonSaveReading.UseVisualStyleBackColor = true;			
			// 
			// buttonClear
			// 
			buttonClear.Location = new Point(288, 269);
			buttonClear.Name = "buttonClear";
			buttonClear.Size = new Size(200, 46);
			buttonClear.TabIndex = 9;
			buttonClear.Text = "Очистить";
			buttonClear.UseVisualStyleBackColor = true;
			
			// 
			// DataEntryControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(buttonClear);
			Controls.Add(buttonSaveReading);
			Controls.Add(textBoxNote);
			Controls.Add(comboBoxMealTag);
			Controls.Add(numericUpDownGlucose);
			Controls.Add(dateTimePickerReading);
			Controls.Add(labelNote);
			Controls.Add(labelMealTag);
			Controls.Add(labelGlucose);
			Controls.Add(labelDateTime);
			Name = "DataEntryControl";
			Size = new Size(750, 387);
			((System.ComponentModel.ISupportInitialize)numericUpDownGlucose).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label labelDateTime;
		private Label labelGlucose;
		private Label labelMealTag;
		private Label labelNote;
		private DateTimePicker dateTimePickerReading;
		private NumericUpDown numericUpDownGlucose;
		private ComboBox comboBoxMealTag;
		private TextBox textBoxNote;
		private Button buttonSaveReading;
		private Button buttonClear;
	}
}
