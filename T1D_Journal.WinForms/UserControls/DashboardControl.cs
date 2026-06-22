using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace T1D_Journal.WinForms.UserControls
{
	public partial class DashboardControl : UserControl
	{
		public DashboardControl()
		{
			InitializeComponent();

			// ================================================================
			// ЖЕСТКОЕ ПЕРЕСТРОЕНИЕ ИЕРАРХИИ (исправляем вложенность)
			// ================================================================

			// 1. Запоминаем ссылки на элементы, чтобы не потерять
			var welcome = this.labelWelcome;
			var stats = this.groupBoxStats;
			var lastReadings = this.groupBoxLastReadings;
			var grid = this.dataGridViewLastReadings;

			// 2. Очищаем контрол от ВСЕХ элементов (они не удаляются, просто убираются из списка)
			this.Controls.Clear();

			// 3. Убеждаемся, что таблица привязана к своему GroupBox (она там и должна быть)
			if (!lastReadings.Controls.Contains(grid))
			{
				lastReadings.Controls.Add(grid);
			}

			// 4. Добавляем элементы обратно в ПРАВИЛЬНОМ порядке (снизу вверх)
			//    Сначала самый нижний (таблица внутри своей группы), потом статистика, потом приветствие
			this.Controls.Add(lastReadings);   // 1. Таблица (самая нижняя)
			this.Controls.Add(stats);          // 2. Статистика (посередине)
			this.Controls.Add(welcome);        // 3. Приветствие (сверху)

			// ================================================================
			// НАСТРОЙКА DOCK И РАЗМЕРОВ
			// ================================================================

			// ---- Приветствие ----
			welcome.Dock = DockStyle.Top;
			welcome.Height = 50;

			// ---- Статистика ----
			stats.Dock = DockStyle.Top;
			stats.Height = 130;

			// ---- Последние замеры (таблица) ----
			lastReadings.Dock = DockStyle.Fill;

			// ---- DataGridView (внутри своей группы) ----
			grid.Dock = DockStyle.Fill;

			// ================================================================
			// ОСТАЛЬНЫЕ НАСТРОЙКИ
			// ================================================================

			SetupDataGridView();
			LoadDashboardData();
		}

		// ================================================================
		// НАСТРОЙКА ТАБЛИЦЫ
		// ================================================================
		private void SetupDataGridView()
		{
			// Очищаем колонки
			dataGridViewLastReadings.Columns.Clear();

			// Добавляем колонки
			dataGridViewLastReadings.Columns.Add("DateTime", "Дата и время");
			dataGridViewLastReadings.Columns.Add("Glucose", "Глюкоза (ммоль/л)");
			dataGridViewLastReadings.Columns.Add("MealTag", "Когда");
			dataGridViewLastReadings.Columns.Add("Note", "Примечание");

			// Настройки
			dataGridViewLastReadings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewLastReadings.ReadOnly = true;
			dataGridViewLastReadings.AllowUserToAddRows = false;
			dataGridViewLastReadings.RowHeadersVisible = false;
			dataGridViewLastReadings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			// Ширина колонок
			if (dataGridViewLastReadings.Columns["DateTime"] != null)
				dataGridViewLastReadings.Columns["DateTime"].Width = 150;
			if (dataGridViewLastReadings.Columns["Glucose"] != null)
				dataGridViewLastReadings.Columns["Glucose"].Width = 120;
			if (dataGridViewLastReadings.Columns["MealTag"] != null)
				dataGridViewLastReadings.Columns["MealTag"].Width = 120;
			if (dataGridViewLastReadings.Columns["Note"] != null)
				dataGridViewLastReadings.Columns["Note"].Width = 300;
		}

		// ================================================================
		// ЗАГРУЗКА ТЕСТОВЫХ ДАННЫХ
		// ================================================================
		private void LoadDashboardData()
		{
			// Приветствие
			labelWelcome.Text = "Добро пожаловать! 👋\nВаш дневник самоконтроля";

			// Статистика
			var todayData = new List<double> { 5.2, 4.8, 6.1, 7.5, 5.5 };

			double avg = todayData.Average();
			double min = todayData.Min();
			double max = todayData.Max();
			int inTarget = todayData.Count(v => v >= 4.0 && v <= 7.0);
			double percentInTarget = (double)inTarget / todayData.Count * 100;

			labelAvg.Text = $"Средняя: {avg:F1} ммоль/л";
			labelMin.Text = $"Мин: {min:F1} ммоль/л";
			labelMax.Text = $"Макс: {max:F1} ммоль/л";
			labelInTarget.Text = $"В норме: {percentInTarget:F0}%";

			// Статус
			if (percentInTarget >= 70)
			{
				labelStatus.Text = "Статус: ✅ Всё хорошо";
				labelStatus.ForeColor = Color.Green;
			}
			else if (percentInTarget >= 40)
			{
				labelStatus.Text = "Статус: ⚠️ Есть отклонения";
				labelStatus.ForeColor = Color.Orange;
			}
			else
			{
				labelStatus.Text = "Статус: ❌ Требуется внимание";
				labelStatus.ForeColor = Color.Red;
			}

			// Таблица (последние замеры)
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

		// ================================================================
		// ОБНОВЛЕНИЕ ДАННЫХ
		// ================================================================
		public void RefreshData()
		{
			LoadDashboardData();
		}

		// ================================================================
		// КОД ДЛЯ БД (закомментирован)
		// ================================================================
		/*
        private void LoadDataFromDB()
        {
            try
            {
                int userId = 1; // ID текущего пользователя
                var repo = new GlucoseRepository();
                var stats = repo.GetTodayStats(userId);

                labelAvg.Text = $"Средняя: {stats.Avg:F1} ммоль/л";
                labelMin.Text = $"Мин: {stats.Min:F1} ммоль/л";
                labelMax.Text = $"Макс: {stats.Max:F1} ммоль/л";
                labelInTarget.Text = $"В норме: {stats.PercentInTarget:F0}%";

                if (stats.PercentInTarget >= 70)
                {
                    labelStatus.Text = "Статус: ✅ Всё хорошо";
                    labelStatus.ForeColor = Color.Green;
                }
                else if (stats.PercentInTarget >= 40)
                {
                    labelStatus.Text = "Статус: ⚠️ Есть отклонения";
                    labelStatus.ForeColor = Color.Orange;
                }
                else
                {
                    labelStatus.Text = "Статус: ❌ Требуется внимание";
                    labelStatus.ForeColor = Color.Red;
                }

                var lastReadings = repo.GetLastReadings(userId, 3);
                dataGridViewLastReadings.Rows.Clear();
                foreach (var r in lastReadings)
                {
                    dataGridViewLastReadings.Rows.Add(
                        r.ReadingDateTime.ToString("dd.MM.yyyy HH:mm"),
                        r.GlucoseValue.ToString("F1"),
                        r.MealTag,
                        r.Note
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }
        */
	}
}