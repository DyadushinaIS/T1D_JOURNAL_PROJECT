namespace T1D_Journal.WinForms.UserControls
{
	partial class DashboardControl
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
			labelWelcome = new Label();
			groupBoxLastReadings = new GroupBox();
			dataGridViewLastReadings = new DataGridView();
			groupBoxStats = new GroupBox();
			labelStatus = new Label();
			labelInTarget = new Label();
			labelMax = new Label();
			labelMin = new Label();
			labelAvg = new Label();
			groupBoxLastReadings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridViewLastReadings).BeginInit();
			groupBoxStats.SuspendLayout();
			SuspendLayout();
			// 
			// labelWelcome
			// 
			labelWelcome.AutoSize = true;
			labelWelcome.Dock = DockStyle.Top;
			labelWelcome.Font = new Font("Book Antiqua", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			labelWelcome.Location = new Point(0, 0);
			labelWelcome.Name = "labelWelcome";
			labelWelcome.Size = new Size(251, 23);
			labelWelcome.TabIndex = 0;
			labelWelcome.Text = "Добро пожаловать, Ирина!";
			// 
			// groupBoxLastReadings
			// 
			groupBoxLastReadings.Controls.Add(dataGridViewLastReadings);
			groupBoxLastReadings.Dock = DockStyle.Fill;
			groupBoxLastReadings.Location = new Point(0, 0);
			groupBoxLastReadings.Name = "groupBoxLastReadings";
			groupBoxLastReadings.Size = new Size(1499, 691);
			groupBoxLastReadings.TabIndex = 1;
			groupBoxLastReadings.TabStop = false;
			groupBoxLastReadings.Text = "Последние замеры:";
			// 
			// dataGridViewLastReadings
			// 
			dataGridViewLastReadings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewLastReadings.Dock = DockStyle.Fill;
			dataGridViewLastReadings.Location = new Point(3, 19);
			dataGridViewLastReadings.Name = "dataGridViewLastReadings";
			dataGridViewLastReadings.ReadOnly = true;
			dataGridViewLastReadings.Size = new Size(1493, 669);
			dataGridViewLastReadings.TabIndex = 0;
			// 
			// groupBoxStats
			// 
			groupBoxStats.Controls.Add(labelStatus);
			groupBoxStats.Controls.Add(labelInTarget);
			groupBoxStats.Controls.Add(labelMax);
			groupBoxStats.Controls.Add(labelMin);
			groupBoxStats.Controls.Add(labelAvg);
			groupBoxStats.Dock = DockStyle.Fill;
			groupBoxStats.Location = new Point(0, 0);
			groupBoxStats.Name = "groupBoxStats";
			groupBoxStats.Size = new Size(1499, 691);
			groupBoxStats.TabIndex = 1;
			groupBoxStats.TabStop = false;
			// 
			// labelStatus
			// 
			labelStatus.Location = new Point(420, 135);
			labelStatus.Name = "labelStatus";
			labelStatus.Size = new Size(350, 35);
			labelStatus.TabIndex = 4;
			labelStatus.Text = "Статус: ✅ Всё хорошо";
			// 
			// labelInTarget
			// 
			labelInTarget.Location = new Point(120, 135);
			labelInTarget.Name = "labelInTarget";
			labelInTarget.Size = new Size(250, 30);
			labelInTarget.TabIndex = 3;
			labelInTarget.Text = "В норме: --%";
			// 
			// labelMax
			// 
			labelMax.Location = new Point(500, 85);
			labelMax.Name = "labelMax";
			labelMax.Size = new Size(200, 30);
			labelMax.TabIndex = 2;
			labelMax.Text = "Макс: --";
			// 
			// labelMin
			// 
			labelMin.Location = new Point(260, 85);
			labelMin.Name = "labelMin";
			labelMin.Size = new Size(220, 30);
			labelMin.TabIndex = 1;
			labelMin.Text = "Мин: --";
			// 
			// labelAvg
			// 
			labelAvg.Location = new Point(20, 85);
			labelAvg.Name = "labelAvg";
			labelAvg.Size = new Size(220, 30);
			labelAvg.TabIndex = 0;
			labelAvg.Text = "Средняя: --";
			// 
			// DashboardControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(labelWelcome);
			Controls.Add(groupBoxStats);
			Controls.Add(groupBoxLastReadings);
			Name = "DashboardControl";
			Size = new Size(1499, 691);
			groupBoxLastReadings.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridViewLastReadings).EndInit();
			groupBoxStats.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label labelWelcome;
		private GroupBox groupBoxLastReadings;
		private GroupBox groupBoxStats;
		private DataGridView dataGridViewLastReadings;
		private Label labelMax;
		private Label labelMin;
		private Label labelAvg;
		private Label labelStatus;
		private Label labelInTarget;
	}
}
