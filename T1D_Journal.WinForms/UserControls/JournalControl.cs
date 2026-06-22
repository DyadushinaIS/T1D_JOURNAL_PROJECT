using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T1D_Journal.DAL.Repositories;
using T1D_Journal.Models;

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

            // Подписываемся на события кнопок
            this.buttonFilter.Click += BtnFilter_Click;
            this.buttonRefresh.Click += BtnRefresh_Click;

            // Настройка внешнего вида таблицы
            SetupDataGridView();

            // Загружаем данные за сегодня при запуске
            LoadDataFromDB(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1));
        }

        // ================================================================
        // НАСТРОЙКА ВНЕШНЕГО ВИДА ТАБЛИЦЫ (DataGridView)
        // ================================================================
        private void SetupDataGridView()
        {
            // Очищаем старые колонки, если они есть
            dataGridViewJournal.Columns.Clear();

            // Создаём колонки с заголовками
            dataGridViewJournal.Columns.Add("DateTime", "Дата и время");
            dataGridViewJournal.Columns.Add("Glucose", "Глюкоза (ммоль/л)");
            dataGridViewJournal.Columns.Add("MealTag", "Когда");
            dataGridViewJournal.Columns.Add("Note", "Примечание");

            // Настройка внешнего вида таблицы
            dataGridViewJournal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewJournal.ReadOnly = true;
            dataGridViewJournal.AllowUserToAddRows = false;
            dataGridViewJournal.RowHeadersVisible = false;
            dataGridViewJournal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Настройка заголовков
            dataGridViewJournal.ColumnHeadersVisible = true;
            dataGridViewJournal.ColumnHeadersHeight = 35;
            dataGridViewJournal.EnableHeadersVisualStyles = false;
            dataGridViewJournal.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewJournal.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            // Настройка ширины колонок
            if (dataGridViewJournal.Columns["DateTime"] != null)
                dataGridViewJournal.Columns["DateTime"].Width = 150;
            if (dataGridViewJournal.Columns["Glucose"] != null)
                dataGridViewJournal.Columns["Glucose"].Width = 120;
            if (dataGridViewJournal.Columns["MealTag"] != null)
                dataGridViewJournal.Columns["MealTag"].Width = 120;
            if (dataGridViewJournal.Columns["Note"] != null)
                dataGridViewJournal.Columns["Note"].Width = 300;
        }

        // ================================================================
        // ЗАГРУЗКА ДАННЫХ ИЗ БАЗЫ ДАННЫХ ЗА ПЕРИОД
        // ================================================================
        private void LoadDataFromDB(DateTime from, DateTime to)
        {
            try
            {
                // 1. Создаём репозиторий для работы с замерами
                var repo = new GlucoseRepository();

                // 2. Получаем данные из БД за указанный период
                DataTable dt = repo.GetByDateRange(CurrentUser.ID, from, to);

                // 3. Очищаем таблицу перед загрузкой
                dataGridViewJournal.Rows.Clear();

                // 4. Добавляем строки в таблицу
                foreach (DataRow row in dt.Rows)
                {
                    dataGridViewJournal.Rows.Add(
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

        // ================================================================
        // ОБНОВЛЕНИЕ ДАННЫХ (вызывается из FormMain при переключении вкладки)
        // ================================================================
        public void RefreshData()
        {
            // Загружаем данные за сегодня
            DateTime today = DateTime.Now.Date;
            LoadDataFromDB(today, today.AddDays(1).AddSeconds(-1));

            // Обновляем фильтры
            dateTimePickerFrom.Value = today;
            dateTimePickerTo.Value = today.AddDays(1).AddSeconds(-1);
        }

        // ================================================================
        // ОБРАБОТЧИК КНОПКИ "ПРИМЕНИТЬ ФИЛЬТР"
        // ================================================================
        private void BtnFilter_Click(object sender, EventArgs e)
        {
            // Получаем выбранные даты из DateTimePicker
            DateTime from = dateTimePickerFrom.Value.Date;
            DateTime to = dateTimePickerTo.Value.Date.AddDays(1).AddSeconds(-1);

            // Проверяем, что начальная дата не позже конечной
            if (from > to)
            {
                MessageBox.Show("Дата 'С' не может быть позже даты 'По'!",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Загружаем данные
            LoadDataFromDB(from, to);

            // Показываем сообщение о применении фильтра
            MessageBox.Show($"✅ Фильтр применён!\n\nС: {from:dd.MM.yyyy}\nПо: {to:dd.MM.yyyy}",
                            "Информация",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        // ================================================================
        // ОБРАБОТЧИК КНОПКИ "ОБНОВИТЬ"
        // ================================================================
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтр на сегодня
            DateTime today = DateTime.Now.Date;
            dateTimePickerFrom.Value = today;
            dateTimePickerTo.Value = today.AddDays(1).AddSeconds(-1);

            // Перезагружаем данные
            LoadDataFromDB(today, today.AddDays(1).AddSeconds(-1));

            // Сообщение об обновлении
            MessageBox.Show("✅ Данные обновлены!\nФильтр сброшен на сегодня.",
                            "Информация",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }
}