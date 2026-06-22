using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
// Подключаем репозиторий для работы с базой данных
using T1D_Journal.DAL.Repositories;
// Подключаем класс для хранения данных о текущем пользователе
using T1D_Journal.Models;

namespace T1D_Journal.WinForms.UserControls
{
    public partial class DashboardControl : UserControl
    {
        // ================================================================
        // КОНСТРУКТОР
        // Вызывается при создании контрола (один раз, при загрузке формы)
        // ================================================================
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

            // Загружаем реальные данные из БД
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
        // ЗАГРУЗКА ДАННЫХ ДЛЯ ГЛАВНОЙ ВКЛАДКИ ИЗ БАЗЫ ДАННЫХ
        // Здесь мы получаем:
        // 1. Приветствие с именем пользователя
        // 2. Статистику за сегодня (средняя, мин, макс, процент в норме)
        // 3. Индикатор состояния (зелёный/жёлтый/красный)
        // 4. Последние 3 замера в таблице
        // ================================================================
        private void LoadDashboardData()
        {
            try
            {
                // ============================================================
                // 1. ПРИВЕТСТВИЕ
                // ============================================================
                // Берем имя пользователя из статического класса CurrentUser
                // Это имя было сохранено при входе в систему
                labelWelcome.Text = $"Добро пожаловать, {CurrentUser.FullName}! 👋\n" +
                                    "Ваш дневник самоконтроля";

                // ============================================================
                // 2. СТАТИСТИКА ЗА СЕГОДНЯ
                // ============================================================
                // Создаём репозиторий — объект, который умеет работать с БД
                var repo = new GlucoseRepository();

                // Получаем статистику за сегодня для текущего пользователя
                // Метод возвращает кортеж: (Avg, Min, Max, PercentInTarget, Total)
                var stats = repo.GetTodayStats(CurrentUser.ID);

                // --- Проверяем, есть ли данные за сегодня ---
                if (stats.Total == 0)
                {
                    // Если сегодня нет ни одного замера — показываем прочерки
                    labelAvg.Text = "Средняя: --";
                    labelMin.Text = "Мин: --";
                    labelMax.Text = "Макс: --";
                    labelInTarget.Text = "В норме: --%";
                    labelStatus.Text = "Статус: ⏳ Нет данных за сегодня";
                    labelStatus.ForeColor = Color.Gray;

                    // Очищаем таблицу последних замеров
                    dataGridViewLastReadings.Rows.Clear();
                    return; // Выходим из метода, дальше не идём
                }

                // --- Отображаем статистику ---
                // F1 — формат с одной цифрой после запятой (например, 5.2)
                labelAvg.Text = $"Средняя: {stats.Avg:F1} ммоль/л";
                labelMin.Text = $"Мин: {stats.Min:F1} ммоль/л";
                labelMax.Text = $"Макс: {stats.Max:F1} ммоль/л";
                labelInTarget.Text = $"В норме: {stats.PercentInTarget:F0}%";

                // --- Индикатор состояния (цветной статус) ---
                // Если процент в норме >= 70% — зелёный (всё хорошо)
                // Если 40-69% — жёлтый (есть отклонения)
                // Если < 40% — красный (требуется внимание)
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

                // ============================================================
                // 3. ПОСЛЕДНИЕ 3 ЗАМЕРА
                // ============================================================
                // Получаем из БД последние 3 замера для текущего пользователя
                DataTable dt = repo.GetLastReadings(CurrentUser.ID, 3);

                // Очищаем таблицу на форме (чтобы не было старых данных)
                dataGridViewLastReadings.Rows.Clear();

                // Проходим по всем строкам из БД и добавляем их в таблицу на форме
                foreach (DataRow row in dt.Rows)
                {
                    // Преобразуем данные из БД в формат для отображения:
                    // - Дата и время: из объекта DateTime в строку "дд.ММ.гггг ЧЧ:мм"
                    // - Глюкоза: из decimal в строку с одной цифрой после запятой
                    // - Остальные поля — как есть
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
                // Если произошла ошибка (нет БД, ошибка SQL и т.д.)
                // Показываем сообщение с текстом ошибки
                MessageBox.Show($"❌ Ошибка загрузки данных для главной страницы:\n\n{ex.Message}",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // ================================================================
        // ОБНОВЛЕНИЕ ДАННЫХ (вызывается из FormMain при переключении вкладки)
        // Когда пользователь переходит на вкладку "Главная", мы обновляем данные
        // ================================================================
        public void RefreshData()
        {
            // Просто перезагружаем все данные
            LoadDashboardData();
        }
    }
}