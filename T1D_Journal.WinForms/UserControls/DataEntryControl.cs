using System;
using System.Windows.Forms;
using T1D_Journal.DAL.Repositories;
using T1D_Journal.Models.DTO;
using T1D_Journal.Models;

namespace T1D_Journal.WinForms.UserControls
{
    public partial class DataEntryControl : UserControl
    {
        public DataEntryControl()
        {
            InitializeComponent();

			// Настройка внешнего вида
			dateTimePickerReading.Font = new Font("Segoe UI", 11);
			numericUpDownGlucose.Font = new Font("Segoe UI", 11);
			comboBoxMealTag.Font = new Font("Segoe UI", 11);
			textBoxNote.Font = new Font("Segoe UI", 11);

			// Подсказки (Tooltips)
			ToolTip toolTip = new ToolTip();
			toolTip.SetToolTip(dateTimePickerReading, "Выберите дату и время замера");
			toolTip.SetToolTip(numericUpDownGlucose, "Введите уровень глюкозы (1–40 ммоль/л)");
			toolTip.SetToolTip(comboBoxMealTag, "Выберите, когда сделан замер");
			toolTip.SetToolTip(textBoxNote, "Дополнительная информация (необязательно)");
			toolTip.SetToolTip(buttonSaveReading, "Сохранить замер в базу данных");
			toolTip.SetToolTip(buttonClear, "Очистить все поля");

			this.buttonSaveReading.Click += ButtonSaveReading_Click;
            this.buttonClear.Click += ButtonClear_Click;			

			LoadData();
        }

        public void LoadData()
        {
            dateTimePickerReading.Value = DateTime.Now;
            numericUpDownGlucose.Value = 5.0m;
            if (comboBoxMealTag.Items.Count > 0)
                comboBoxMealTag.SelectedIndex = 0;
            textBoxNote.Clear();
        }

        private void ButtonSaveReading_Click(object sender, EventArgs e)
        {
            DateTime readingDateTime = dateTimePickerReading.Value;
            decimal glucoseValue = numericUpDownGlucose.Value;
            string mealTag = comboBoxMealTag.SelectedItem?.ToString() ?? "BeforeMeal";
            string note = textBoxNote.Text.Trim();

            if (glucoseValue <= 0)
            {
                MessageBox.Show("Глюкоза должна быть больше 0!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (glucoseValue < 2.0m || glucoseValue > 15.0m)
            {
                DialogResult result = MessageBox.Show(
                    $"Значение глюкозы {glucoseValue} ммоль/л выходит за пределы нормы.\n" +
                    "Вы уверены, что хотите сохранить этот замер?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;
            }

            try
            {
                var reading = new GlucoseReadingDto
                {
                    UserID = CurrentUser.ID,
                    ReadingDateTime = readingDateTime,
                    GlucoseValue = glucoseValue,
                    MealTag = mealTag,
                    Note = note
                };

                var repo = new GlucoseRepository();
                int newId = repo.Create(reading);

                MessageBox.Show(
                    $"✅ Замер успешно сохранён!\n\n" +
                    $"ID записи: {newId}\n" +
                    $"Дата: {readingDateTime:dd.MM.yyyy HH:mm}\n" +
                    $"Глюкоза: {glucoseValue:F1} ммоль/л\n" +
                    $"Когда: {mealTag}\n" +
                    $"Заметка: {(string.IsNullOrEmpty(note) ? "(нет)" : note)}",
                    "Успешно",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Ошибка при сохранении замера:\n\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}