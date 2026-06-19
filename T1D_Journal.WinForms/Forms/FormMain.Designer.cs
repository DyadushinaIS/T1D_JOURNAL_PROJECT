namespace T1D_Journal.WinForms.Forms
{
	partial class FormMain
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
			tabControl = new TabControl();
			tabPageMain = new TabPage();
			tabPageDataEntry = new TabPage();
			dataEntryControl1 = new T1D_Journal.WinForms.UserControls.DataEntryControl();
			tabPageJournal = new TabPage();
			journalControl1 = new T1D_Journal.WinForms.UserControls.JournalControl();
			tabPageReports = new TabPage();
			reportsControl1 = new T1D_Journal.WinForms.UserControls.ReportsControl();
			tabControl.SuspendLayout();
			tabPageDataEntry.SuspendLayout();
			tabPageJournal.SuspendLayout();
			tabPageReports.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl
			// 
			tabControl.Controls.Add(tabPageMain);
			tabControl.Controls.Add(tabPageDataEntry);
			tabControl.Controls.Add(tabPageJournal);
			tabControl.Controls.Add(tabPageReports);
			tabControl.Dock = DockStyle.Fill;
			tabControl.Location = new Point(0, 0);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new Size(800, 450);
			tabControl.TabIndex = 0;
			tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
			// 
			// tabPageMain
			// 
			tabPageMain.Location = new Point(4, 24);
			tabPageMain.Name = "tabPageMain";
			tabPageMain.Padding = new Padding(3);
			tabPageMain.Size = new Size(792, 422);
			tabPageMain.TabIndex = 0;
			tabPageMain.Text = "Главная";
			tabPageMain.UseVisualStyleBackColor = true;
			// 
			// tabPageDataEntry
			// 
			tabPageDataEntry.Controls.Add(dataEntryControl1);
			tabPageDataEntry.Location = new Point(4, 24);
			tabPageDataEntry.Name = "tabPageDataEntry";
			tabPageDataEntry.Padding = new Padding(3);
			tabPageDataEntry.Size = new Size(792, 422);
			tabPageDataEntry.TabIndex = 1;
			tabPageDataEntry.Text = "Ввод данных";
			tabPageDataEntry.UseVisualStyleBackColor = true;
			// 
			// dataEntryControl1
			// 
			dataEntryControl1.Dock = DockStyle.Fill;
			dataEntryControl1.Location = new Point(3, 3);
			dataEntryControl1.Name = "dataEntryControl1";
			dataEntryControl1.Size = new Size(786, 416);
			dataEntryControl1.TabIndex = 0;
			// 
			// tabPageJournal
			// 
			tabPageJournal.Controls.Add(journalControl1);
			tabPageJournal.Location = new Point(4, 24);
			tabPageJournal.Name = "tabPageJournal";
			tabPageJournal.Padding = new Padding(3);
			tabPageJournal.Size = new Size(792, 422);
			tabPageJournal.TabIndex = 2;
			tabPageJournal.Text = "Журнал";
			tabPageJournal.UseVisualStyleBackColor = true;
			// 
			// journalControl1
			// 
			journalControl1.Dock = DockStyle.Fill;
			journalControl1.Location = new Point(3, 3);
			journalControl1.Name = "journalControl1";
			journalControl1.Size = new Size(786, 416);
			journalControl1.TabIndex = 0;
			// 
			// tabPageReports
			// 
			tabPageReports.Controls.Add(reportsControl1);
			tabPageReports.Location = new Point(4, 24);
			tabPageReports.Name = "tabPageReports";
			tabPageReports.Padding = new Padding(3);
			tabPageReports.Size = new Size(792, 422);
			tabPageReports.TabIndex = 3;
			tabPageReports.Text = "Отчеты";
			tabPageReports.UseVisualStyleBackColor = true;
			// 
			// reportsControl1
			// 
			reportsControl1.Dock = DockStyle.Fill;
			reportsControl1.Location = new Point(3, 3);
			reportsControl1.Name = "reportsControl1";
			reportsControl1.Size = new Size(786, 416);
			reportsControl1.TabIndex = 0;
			// 
			// FormMain
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(tabControl);
			Name = "FormMain";
			Text = "FormMain";
			FormClosed += FormMain_FormClosed;
			Load += FormMain_Load;
			tabControl.ResumeLayout(false);
			tabPageDataEntry.ResumeLayout(false);
			tabPageJournal.ResumeLayout(false);
			tabPageReports.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TabControl tabControl;
		private TabPage tabPageMain;
		private TabPage tabPageJournal;
		private TabPage tabPageReports;
		private TabPage tabPageDataEntry;
		private UserControls.DataEntryControl dataEntryControl1;
		private UserControls.JournalControl journalControl1;
		private UserControls.ReportsControl reportsControl1;
	}
}