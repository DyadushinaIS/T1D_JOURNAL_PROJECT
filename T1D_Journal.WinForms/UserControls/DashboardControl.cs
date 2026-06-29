using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T1D_Journal.DAL.Repositories;
using T1D_Journal.Models;

namespace T1D_Journal.WinForms.UserControls
{
	public partial class DashboardControl : UserControl
	{
		public DashboardControl()
		{
			InitializeComponent();

			// ЖЕСТКОЕ ПЕРЕСТРОЕНИЕ ИЕРАРХИИ
			var welcome = this.labelWelcome;
			var stats = this.groupBoxStats;
			var lastReadings = this.groupBoxLastReadings;
			var grid = this.dataGridViewLastReadings;

			this.Controls.Clear();

			if (!lastReadings.Controls.Contains(grid))
			{
				lastReadings.Controls.Add(grid);
			}

			this.Controls.Add(lastReadings);
			this.Controls.Add(stats);
			this.Controls.Add(welcome);

			welcome.Dock = DockStyle.Top;
			welcome.Height = 50;

			stats.Dock = DockStyle.Top;
			stats.Height = 130;

			lastReadings.Dock = DockStyle.Fill;
			grid.Dock = DockStyle.Fill;

			SetupDataGridView();

			// ============================================================
			// ПРОВЕРКА: если тестовый режим — не лезем в БД
			// ============================================================
			if (CurrentUser.Login == "test")
			{
				LoadTestData();
			}
			else
			{
				LoadDashboardData();
			}
		}

		private void SetupDataGridView()
		{
			dataGridViewLastReadings.Columns.Clear();

			dataGridViewLastReadings.Columns.Add("DateTime", "Дата и время");
			dataGridViewLastReadings.Columns.Add("Glucose", "Глюкоза (ммоль/л)");
			dataGridViewLastReadings.Columns.Add("MealTag", "Когда");
			dataGridViewLastReadings.Columns.Add("Note", "Примечание");

			dataGridViewLastReadings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewLastReadings.ReadOnly = true;
			dataGridViewLastReadings.AllowUserToAddRows = false;
			dataGridViewLastReadings.RowHeadersVisible = false;
			dataGridViewLastReadings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			if (dataGridViewLastReadings.Columns["DateTime"] != null)
				dataGridViewLastReadings.Columns["DateTime"].Width = 150;
			if (dataGridViewLastReadings.Columns["Glucose"] != null)
				dataGridViewLastReadings.Columns["Glucose"].Width = 120;
			if (dataGridViewLastReadings.Columns["MealTag"] != null)
				dataGridViewLastReadings.Columns["MealTag"].Width = 120;
			if (dataGridViewLastReadings.Columns["Note"] != null)
				dataGridViewLastReadings.Columns["Note"].Width = 300;
		}

		private void LoadDashboardData()
		{
			try
			{
				// ============================================================
				// 1. ЗАГОЛОВОК
				// ============================================================
				labelWelcome.Text = "📊 Ваш дневник самоконтроля";
				labelWelcome.Font = new Font("Segoe UI", 22, FontStyle.Bold);
				labelWelcome.ForeColor = Color.FromArgb(30, 30, 60);
				labelWelcome.AutoSize = true;
				labelWelcome.Location = new Point(20, 25);

				// ============================================================
				// 2. СТАТИСТИКА ЗА СЕГОДНЯ
				// ============================================================
				var repo = new GlucoseRepository();
				var stats = repo.GetTodayStats(CurrentUser.ID);

				// Шрифт для значений
				Font valueFont = new Font("Segoe UI", 14, FontStyle.Bold);

				if (stats.Total == 0)
				{
					labelAvg.Text = "Средняя: --";
					labelAvg.Font = valueFont;
					labelMin.Text = "Мин: --";
					labelMin.Font = valueFont;
					labelMax.Text = "Макс: --";
					labelMax.Font = valueFont;
					labelInTarget.Text = "В норме: --%";
					labelInTarget.Font = valueFont;
					labelStatus.Text = "Статус: ⏳ Нет данных за сегодня";
					labelStatus.Font = new Font("Segoe UI", 13, FontStyle.Bold);
					labelStatus.ForeColor = Color.Gray;
					dataGridViewLastReadings.Rows.Clear();
					return;
				}

				// Заполняем значения
				labelAvg.Text = $"📉 Средняя: {stats.Avg:F1} ммоль/л";
				labelAvg.Font = valueFont;
				labelAvg.ForeColor = Color.FromArgb(20, 80, 20);

				labelMin.Text = $"📉 Мин: {stats.Min:F1} ммоль/л";
				labelMin.Font = valueFont;
				labelMin.ForeColor = Color.FromArgb(200, 100, 0);

				labelMax.Text = $"📈 Макс: {stats.Max:F1} ммоль/л";
				labelMax.Font = valueFont;
				labelMax.ForeColor = Color.FromArgb(200, 30, 30);

				labelInTarget.Text = $"✅ В норме: {stats.PercentInTarget:F0}%";
				labelInTarget.Font = valueFont;
				labelInTarget.ForeColor = Color.FromArgb(0, 100, 200);

				// Статус
				if (stats.PercentInTarget >= 70)
				{
					labelStatus.Text = "Статус: ✅ Всё хорошо";
					labelStatus.Font = new Font("Segoe UI", 13, FontStyle.Bold);
					labelStatus.ForeColor = Color.Green;
				}
				else if (stats.PercentInTarget >= 40)
				{
					labelStatus.Text = "Статус: ⚠️ Есть отклонения";
					labelStatus.Font = new Font("Segoe UI", 13, FontStyle.Bold);
					labelStatus.ForeColor = Color.Orange;
				}
				else
				{
					labelStatus.Text = "Статус: ❌ Требуется внимание";
					labelStatus.Font = new Font("Segoe UI", 13, FontStyle.Bold);
					labelStatus.ForeColor = Color.Red;
				}

				// ============================================================
				// 3. ПОСЛЕДНИЕ 3 ЗАМЕРА
				// ============================================================
				DataTable dt = repo.GetLastReadings(CurrentUser.ID, 3);
				dataGridViewLastReadings.Rows.Clear();

				foreach (DataRow row in dt.Rows)
				{
					dataGridViewLastReadings.Rows.Add(
						Convert.ToDateTime(row["ReadingDateTime"]).ToString("dd.MM.yyyy HH:mm"),
						Convert.ToDecimal(row["GlucoseValue"]).ToString("F1"),
						row["MealTag"].ToString(),
						row["Note"].ToString()
					);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"❌ Ошибка загрузки данных:\n\n{ex.Message}",
								"Ошибка",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}
		}

		public void RefreshData()
		{
			if (CurrentUser.Login == "test")
				LoadTestData();
			else
				LoadDashboardData();
		}

		public void LoadTestData()
		{
			// ============================================================
			// 1. ЗАГОЛОВОК
			// ============================================================
			labelWelcome.Text = "📊 Ваш дневник самоконтроля (тестовый режим)";
			labelWelcome.Font = new Font("Segoe UI", 22, FontStyle.Bold);
			labelWelcome.ForeColor = Color.FromArgb(30, 30, 60);
			labelWelcome.AutoSize = true;
			labelWelcome.Location = new Point(20, 25);

			// ============================================================
			// 2. ТЕСТОВАЯ СТАТИСТИКА
			// ============================================================
			var todayData = new List<double> { 5.2, 4.8, 6.1, 7.5, 5.5 };

			double avg = todayData.Average();
			double min = todayData.Min();
			double max = todayData.Max();
			int inTarget = todayData.Count(v => v >= 4.0 && v <= 7.0);
			double percentInTarget = (double)inTarget / todayData.Count * 100;

			Font valueFont = new Font("Segoe UI", 14, FontStyle.Bold);

			labelAvg.Text = $"📉 Средняя: {avg:F1} ммоль/л";
			labelAvg.Font = valueFont;
			labelAvg.ForeColor = Color.FromArgb(20, 80, 20);

			labelMin.Text = $"📉 Мин: {min:F1} ммоль/л";
			labelMin.Font = valueFont;
			labelMin.ForeColor = Color.FromArgb(200, 100, 0);

			labelMax.Text = $"📈 Макс: {max:F1} ммоль/л";
			labelMax.Font = valueFont;
			labelMax.ForeColor = Color.FromArgb(200, 30, 30);

			labelInTarget.Text = $"✅ В норме: {percentInTarget:F0}%";
			labelInTarget.Font = valueFont;
			labelInTarget.ForeColor = Color.FromArgb(0, 100, 200);

			// Статус
			if (percentInTarget >= 70)
			{
				labelStatus.Text = "Статус: ✅ Всё хорошо";
				labelStatus.Font = new Font("Segoe UI", 13, FontStyle.Bold);
				labelStatus.ForeColor = Color.Green;
			}
			else if (percentInTarget >= 40)
			{
				labelStatus.Text = "Статус: ⚠️ Есть отклонения";
				labelStatus.Font = new Font("Segoe UI", 13, FontStyle.Bold);
				labelStatus.ForeColor = Color.Orange;
			}
			else
			{
				labelStatus.Text = "Статус: ❌ Требуется внимание";
				labelStatus.Font = new Font("Segoe UI", 13, FontStyle.Bold);
				labelStatus.ForeColor = Color.Red;
			}

			// ============================================================
			// 3. ТЕСТОВЫЕ ПОСЛЕДНИЕ ЗАМЕРЫ
			// ============================================================
			dataGridViewLastReadings.Rows.Clear();

			var lastReadings = new List<object[]>
	{
		new object[] { DateTime.Now.AddHours(-2).ToString("dd.MM.yyyy HH:mm"), "5.2", "AfterMeal", "Обед" },
		new object[] { DateTime.Now.AddHours(-6).ToString("dd.MM.yyyy HH:mm"), "4.5", "Fasting", "Натощак" },
		new object[] { DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy HH:mm"), "6.1", "BeforeMeal", "Ужин" }
	};

			foreach (var row in lastReadings)
			{
				dataGridViewLastReadings.Rows.Add(row);
			}
		}
	}
}