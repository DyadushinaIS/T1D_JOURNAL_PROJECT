namespace T1D_Journal.WinForms.UserControls
{
	partial class ReportsControl
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
			panelStats = new Panel();
			labelTarget = new Label();
			labelMax = new Label();
			labelMin = new Label();
			labelAvg = new Label();
			comboBoxPeriod = new ComboBox();
			buttonGenerate = new Button();
			cartesianChartGlucose = new LiveCharts.WinForms.CartesianChart();
			panelStats.SuspendLayout();
			SuspendLayout();
			// 
			// panelStats
			// 
			panelStats.Controls.Add(labelTarget);
			panelStats.Controls.Add(labelMax);
			panelStats.Controls.Add(labelMin);
			panelStats.Controls.Add(labelAvg);
			panelStats.Dock = DockStyle.Top;
			panelStats.Location = new Point(0, 0);
			panelStats.Name = "panelStats";
			panelStats.Size = new Size(800, 100);
			panelStats.TabIndex = 0;
			// 
			// labelTarget
			// 
			labelTarget.AutoSize = true;
			labelTarget.Location = new Point(610, 43);
			labelTarget.Name = "labelTarget";
			labelTarget.Size = new Size(79, 15);
			labelTarget.TabIndex = 3;
			labelTarget.Text = "В норме: --%";
			// 
			// labelMax
			// 
			labelMax.AutoSize = true;
			labelMax.Location = new Point(422, 43);
			labelMax.Name = "labelMax";
			labelMax.Size = new Size(105, 15);
			labelMax.TabIndex = 2;
			labelMax.Text = "Макс: -- ммоль/л";
			// 
			// labelMin
			// 
			labelMin.AutoSize = true;
			labelMin.Location = new Point(222, 43);
			labelMin.Name = "labelMin";
			labelMin.Size = new Size(101, 15);
			labelMin.TabIndex = 1;
			labelMin.Text = "Мин: -- ммоль/л";
			// 
			// labelAvg
			// 
			labelAvg.AutoSize = true;
			labelAvg.Location = new Point(33, 43);
			labelAvg.Name = "labelAvg";
			labelAvg.Size = new Size(122, 15);
			labelAvg.TabIndex = 0;
			labelAvg.Text = "Средняя: -- ммоль/л";
			// 
			// comboBoxPeriod
			// 
			comboBoxPeriod.FormattingEnabled = true;
			comboBoxPeriod.Items.AddRange(new object[] { "День", "Неделя", "Месяц" });
			comboBoxPeriod.Location = new Point(406, 121);
			comboBoxPeriod.Name = "comboBoxPeriod";
			comboBoxPeriod.Size = new Size(121, 23);
			comboBoxPeriod.TabIndex = 1;
			// 
			// buttonGenerate
			// 
			buttonGenerate.Location = new Point(610, 121);
			buttonGenerate.Name = "buttonGenerate";
			buttonGenerate.Size = new Size(152, 23);
			buttonGenerate.TabIndex = 2;
			buttonGenerate.Text = "Построить график";
			buttonGenerate.UseVisualStyleBackColor = true;
			buttonGenerate.Click += ButtonGenerate_Click;
			// 
			// cartesianChartGlucose
			// 
			cartesianChartGlucose.Dock = DockStyle.Fill;
			cartesianChartGlucose.Location = new Point(0, 100);
			cartesianChartGlucose.Name = "cartesianChartGlucose";
			cartesianChartGlucose.Size = new Size(800, 350);
			cartesianChartGlucose.TabIndex = 3;
			cartesianChartGlucose.Text = "cartesianChart1";
			// 
			// ReportsControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(buttonGenerate);
			Controls.Add(comboBoxPeriod);
			Controls.Add(cartesianChartGlucose);
			Controls.Add(panelStats);
			Name = "ReportsControl";
			Text = "ReportsControl";
			panelStats.ResumeLayout(false);
			panelStats.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelStats;
		private Label labelAvg;
		private Label labelTarget;
		private Label labelMax;
		private Label labelMin;
		private ComboBox comboBoxPeriod;
		private Button buttonGenerate;
		private LiveCharts.WinForms.CartesianChart cartesianChartGlucose;
	}
}