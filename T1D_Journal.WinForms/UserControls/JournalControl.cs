using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace T1D_Journal.WinForms.UserControls
{
	public partial class JournalControl : UserControl
	{
		// ================================================================
		// КОНСТРУКТОР
		// ================================================================
		public JournalControl()
		{
			InitializeComponent();

			//Подписка на события кнопок
			this.buttonFilter.Click += new EventHandler(ButtonFilter_Click);
			this.buttonRefresh.Click += new EventHandler(ButtonRefresh_Click);

			// Настройка DataGridView
			SetupDataGridView();

			// Загрузка тестовых данных
			LoadTestData();
		}

		// ================================================================
		// НАСТРОЙКА ВНЕШНЕГО ВИДА ТАБЛИЦЫ
		// ================================================================
		private void SetupDataGridView()
		{
			// Очищаем все колонки, если они есть
			dataGridViewJournal.Columns.Clear();

			// Создаём колонки
			dataGridViewJournal.Columns.Add("DateTime", "Дата и время");
			dataGridViewJournal.Columns.Add("Glucose", "Глюкоза (ммоль/л)");
			dataGridViewJournal.Columns.Add("MealTag", "Когда");
			dataGridViewJournal.Columns.Add("Note", "Примечание");			
		}







		// ================================================================
		// ЗАГРУЗКА ТЕСТОВЫХ ДАННЫХ (без БД)
		// ================================================================
		private void LoadTestData()
		{
			// Очищаем таблицу
			dataGridViewJournal.Rows.Clear();

			// Создаём тестовые данные
			var testData = new List<object[]>
			{
				new object[] { DateTime.Now.AddDays(-2).ToString("dd.MM.yyyy HH:mm"), "5.2", "BeforeMeal", "Завтрак" },
				new object[] { DateTime.Now.AddDays(-2).ToString("dd.MM.yyyy HH:mm"), "7.8", "AfterMeal", "Обед" },
				new object[] { DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy HH:mm"), "4.5", "Fasting", "Натощак" },
				new object[] { DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy HH:mm"), "6.1", "BeforeMeal", "Ужин" },
				new object[] { DateTime.Now.ToString("dd.MM.yyyy HH:mm"), "5.0", "BeforeMeal", "Завтрак" },
				new object[] { DateTime.Now.ToString("dd.MM.yyyy HH:mm"), "8.2", "AfterMeal", "Обед" }
			};

			// Добавляем строки в таблицу
			foreach (var row in testData)
			{
				dataGridViewJournal.Rows.Add(row);
			}

			// Настраиваем фильтры на сегодня
			dateTimePickerFrom.Value = DateTime.Now.Date;
			dateTimePickerTo.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);  // Конец сегодняшнего дня
		}

		// ================================================================
		// ОБНОВЛЕНИЕ ДАННЫХ (вызывается из FormMain при переключении вкладки)
		// ================================================================
		public void RefreshData()
		{
			// Пока просто перезагружаем тестовые данные
			// Позже здесь будет загрузка из БД с учётом фильтров
			LoadTestData();
		}

		// ================================================================
		// ПРИМЕНЕНИЕ ФИЛЬТРА ПО ДАТЕ
		// ================================================================
		private void ApplyFilter()
		{
			// В тестовом режиме просто показываем сообщение
			// Позже здесь будет реальная фильтрация данных из БД
			MessageBox.Show($"Фильтр применён!\nС: {dateTimePickerFrom.Value:dd.MM.yyyy}\nПо: {dateTimePickerTo.Value:dd.MM.yyyy}",
							"Информация",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);

			// В реальном режиме здесь будет загрузка данных из БД с фильтром
			// LoadDataFromDB(dtpFrom.Value, dtpTo.Value);
		}

		// ================================================================
		// ОБРАБОТЧИКИ КНОПОК
		// ================================================================

		// Кнопка "Применить фильтр"
		private void ButtonFilter_Click(object sender, EventArgs e)
		{
			ApplyFilter();
		}

		// Кнопка "Обновить" (сброс фильтра)
		private void ButtonRefresh_Click(object sender, EventArgs e)
		{
			// Сбрасываем фильтр на сегодня
			dateTimePickerFrom.Value = DateTime.Now.Date;
			dateTimePickerTo.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

			// Перезагружаем данные
			RefreshData();

			MessageBox.Show("Данные обновлены!\nФильтр сброшен на сегодня.", "Информация",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void dataGridViewJournal_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		// ================================================================
		// КОД С БД (закомментирован, раскомментировать когда появится БД)
		// ================================================================
		/*
        private void LoadDataFromDB(DateTime from, DateTime to)
        {
            try
            {
                var repo = new GlucoseRepository();
                var data = repo.GetByDateRange(CurrentUser.ID, from, to);
                
                dgvJournal.Rows.Clear();
                foreach (var reading in data)
                {
                    dgvJournal.Rows.Add(
                        reading.ReadingDateTime.ToString("dd.MM.yyyy HH:mm"),
                        reading.GlucoseValue.ToString("0.0"),
                        reading.MealTag,
                        reading.Note
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
	}
}