using System;
using System.Windows.Forms;

namespace T1D_Journal.WinForms.UserControls
{
	public partial class DataEntryControl : UserControl
	{
		// ============================================================
		// КОНСТРУКТОР
		// ============================================================
		public DataEntryControl()
		{
			InitializeComponent();

			// Подписываемся на события кнопок
			this.buttonSaveReading.Click += ButtonSaveReading_Click;
			this.buttonClear.Click += ButtonClear_Click;
		}

		// ============================================================
		// МЕТОД ЗАГРУЗКИ ДАННЫХ (вызывается при открытии вкладки)
		// ============================================================
		public void LoadData()
		{
			dateTimePickerReading.Value = DateTime.Now;
			numericUpDownGlucose.Value = 5.5m;
			comboBoxMealTag.SelectedIndex = 0;
			textBoxNote.Clear();
		}

		// ============================================================
		// ОБРАБОТЧИК КНОПКИ "СОХРАНИТЬ ЗАМЕР"
		// ============================================================
		private void ButtonSaveReading_Click(object sender, EventArgs e)
		{
			// Собираем данные с формы
			DateTime readingDateTime = dateTimePickerReading.Value;
			decimal glucoseValue = numericUpDownGlucose.Value;
			string mealTag = comboBoxMealTag.SelectedItem?.ToString() ?? "BeforeMeal";
			string note = textBoxNote.Text.Trim();

			// ВАЛИДАЦИЯ (проверка данных)
			if (glucoseValue <= 0)
			{
				MessageBox.Show("Глюкоза должна быть больше 0!", "Ошибка",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// ============================================================
			// ТЕСТОВЫЙ РЕЖИМ (без БД)
			// ============================================================
			MessageBox.Show($"Замер сохранён (тестовый режим)!\n\n" +
							$"Дата: {readingDateTime:dd.MM.yyyy HH:mm}\n" +
							$"Глюкоза: {glucoseValue} ммоль/л\n" +
							$"Когда: {mealTag}\n" +
							$"Примечание: {note}",
							"Успешно",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);

			// Очищаем поля для следующего ввода
			LoadData();

			/*
            // ============================================================
            // КОД С БД - РАСКОММЕНТИРОВАТЬ КОГДА ПОЯВИТСЯ БД
            // ============================================================
            try
            {
                var reading = new GlucoseReadingDto
                {
                    UserID = 1, // TODO: брать ID текущего пользователя
                    ReadingDateTime = readingDateTime,
                    GlucoseValue = glucoseValue,
                    MealTag = mealTag,
                    Note = note
                };

                var repo = new GlucoseRepository();
                int newId = repo.Create(reading);

                MessageBox.Show($"Замер сохранён! ID: {newId}", "Успешно",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
		}

		// ============================================================
		// ОБРАБОТЧИК КНОПКИ "ОЧИСТИТЬ"
		// ============================================================
		private void ButtonClear_Click(object sender, EventArgs e)
		{
			LoadData();
		}
	}
}