using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using T1D_Journal.DAL.Repositories;
using T1D_Journal.Models;

namespace T1D_Journal.WinForms.UserControls
{
    public partial class ReportsControl : UserControl
    {
        // ================================================================
        // КОНСТРУКТОР
        // Вызывается при создании контрола (один раз)
        // ================================================================
        public ReportsControl()
        {
            InitializeComponent();

            // --- Настройка внешнего вида ---
            // Панель статистики приклеивается к верху
            panelStats.Dock = DockStyle.Top;
            panelStats.Height = 100;

            // График занимает всё оставшееся место
            cartesianChartGlucose.Dock = DockStyle.Fill;

            // --- Добавляем пункты в выпадающий список (если их нет) ---
            if (comboBoxPeriod.Items.Count == 0)
            {
                comboBoxPeriod.Items.Add("День");
                comboBoxPeriod.Items.Add("Неделя");
                comboBoxPeriod.Items.Add("Месяц");
            }
            // По умолчанию выбираем "Неделя" (индекс 1)
            comboBoxPeriod.SelectedIndex = 1;

            // --- Подписываемся на событие кнопки ---
            // Когда пользователь нажимает "Построить график" — вызывается метод ButtonGenerate_Click
            buttonGenerate.Click += ButtonGenerate_Click;

            // --- Загружаем реальные данные из БД при запуске ---
            LoadChartData();
        }

        // ================================================================
        // ЗАГРУЗКА ДАННЫХ ДЛЯ ГРАФИКА ИЗ БАЗЫ ДАННЫХ
        // Определяет период (день/неделя/месяц) и загружает данные
        // ================================================================
        private void LoadChartData()
        {
            try
            {
                // --- ШАГ 1: Определяем период ---
                // Смотрим, что выбрано в выпадающем списке
                string period = comboBoxPeriod.SelectedItem?.ToString() ?? "Неделя";

                // Начальная дата зависит от выбранного периода
                DateTime from;
                DateTime to = DateTime.Now; // Конечная дата — сегодня

                switch (period)
                {
                    case "День":
                        from = DateTime.Now.Date; // Начало сегодняшнего дня (00:00)
                        break;
                    case "Неделя":
                        from = DateTime.Now.AddDays(-6).Date; // 6 дней назад (всего 7 дней)
                        break;
                    case "Месяц":
                        from = DateTime.Now.AddDays(-29).Date; // 29 дней назад (всего 30 дней)
                        break;
                    default:
                        from = DateTime.Now.AddDays(-6).Date;
                        break;
                }

                // --- ШАГ 2: Получаем данные из БД ---
                // Создаём репозиторий для работы с замерами
                var repo = new GlucoseRepository();

                // Вызываем метод, который возвращает DataTable с датами и значениями глюкозы
                DataTable dt = repo.GetChartData(CurrentUser.ID, from, to);

                // --- ШАГ 3: Проверяем, есть ли данные ---
                if (dt.Rows.Count == 0)
                {
                    // Если данных нет — показываем сообщение
                    MessageBox.Show("Нет данных для построения графика за выбранный период.",
                                    "Информация",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // Очищаем график, чтобы не показывать старые данные
                    cartesianChartGlucose.Series.Clear();
                    cartesianChartGlucose.AxisX.Clear();
                    cartesianChartGlucose.AxisY.Clear();

                    // Очищаем статистику
                    labelAvg.Text = "Средняя: --";
                    labelMin.Text = "Мин: --";
                    labelMax.Text = "Макс: --";
                    labelTarget.Text = "В норме: --%";
                    return;
                }

                // --- ШАГ 4: Строим график по данным из БД ---
                BuildChart(dt);

                // --- ШАГ 5: Обновляем статистику ---
                UpdateStatistics(dt);
            }
            catch (Exception ex)
            {
                // Если произошла ошибка (нет БД, ошибка SQL и т.д.)
                MessageBox.Show($"❌ Ошибка загрузки данных для графика:\n\n{ex.Message}",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // ================================================================
        // ПОСТРОЕНИЕ ГРАФИКА
        // Принимает DataTable с данными из БД и отображает их на графике
        // ================================================================
        private void BuildChart(DataTable data)
        {
            // --- ШАГ 1: Очищаем старый график ---
            cartesianChartGlucose.Series.Clear();
            cartesianChartGlucose.AxisX.Clear();
            cartesianChartGlucose.AxisY.Clear();

            // --- ШАГ 2: Извлекаем данные из DataTable ---
            // Список дат (для оси X)
            var dates = data.AsEnumerable()
                            .Select(r => Convert.ToDateTime(r["ReadingDateTime"]))
                            .ToList();

            // Список значений глюкозы (для оси Y)
            var values = data.AsEnumerable()
                             .Select(r => Convert.ToDouble(r["GlucoseValue"]))
                             .ToList();

            // --- ШАГ 3: Создаём серию данных для графика ---
            // LineSeries — это линия на графике
            var series = new LineSeries
            {
                Title = "Глюкоза (ммоль/л)",      // Название серии (отображается в легенде)
                Values = new ChartValues<double>(), // Значения (будут добавлены ниже)
                LineSmoothness = 0.5,              // Плавность линии (0 = ломаная, 1 = максимально плавная)
                PointGeometrySize = 15             // Размер точек на графике
            };

            // Добавляем значения глюкозы в серию
            foreach (var v in values)
            {
                series.Values.Add(v);
            }

            // Добавляем серию на график
            cartesianChartGlucose.Series.Add(series);

            // --- ШАГ 4: Настройка оси X (даты) ---
            // Ось X будет показывать даты в формате "день.месяц"
            cartesianChartGlucose.AxisX.Add(new Axis
            {
                Title = "Дата",
                Labels = dates.Select(d => d.ToString("dd.MM")).ToList(),
                Separator = new Separator { Step = 1 } // Шаг между делениями = 1 день
            });

            // --- ШАГ 5: Настройка оси Y (глюкоза) и целевой зоны ---
            // Создаём ось Y с диапазоном от 0 до 15 ммоль/л
            var yAxis = new Axis
            {
                Title = "Глюкоза (ммоль/л)",
                MinValue = 0,
                MaxValue = 15,
                Separator = new Separator { Step = 1 }
            };

            // --- ДОБАВЛЯЕМ ЦЕЛЕВУЮ ЗОНУ (4.0 - 7.0 ммоль/л) ---
            // Это зелёная полоса на графике, показывающая целевой диапазон
            yAxis.Sections.Add(new AxisSection
            {
                FromValue = 4.0,   // Нижняя граница нормы
                ToValue = 7.0,     // Верхняя граница нормы
                Fill = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromArgb(50, 0, 255, 0)), // Полупрозрачный зелёный
                Stroke = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromArgb(100, 0, 100, 0)), // Тёмно-зелёная граница
                StrokeThickness = 1
            });

            // Добавляем ось Y на график
            cartesianChartGlucose.AxisY.Add(yAxis);
        }

        // ================================================================
        // ОБНОВЛЕНИЕ СТАТИСТИКИ
        // Вычисляет и отображает: среднюю, минимум, максимум, процент в норме
        // ================================================================
        private void UpdateStatistics(DataTable data)
        {
            // --- ШАГ 1: Извлекаем все значения глюкозы из DataTable ---
            var values = data.AsEnumerable()
                             .Select(r => Convert.ToDouble(r["GlucoseValue"]))
                             .ToList();

            // Если данных нет — показываем прочерки
            if (values.Count == 0)
            {
                labelAvg.Text = "Средняя: --";
                labelMin.Text = "Мин: --";
                labelMax.Text = "Макс: --";
                labelTarget.Text = "В норме: --%";
                return;
            }

            // --- ШАГ 2: Вычисляем статистику ---
            // Среднее арифметическое
            double avg = values.Average();

            // Минимальное значение
            double min = values.Min();

            // Максимальное значение
            double max = values.Max();

            // Количество значений в целевом диапазоне (4.0 - 7.0)
            int inTarget = values.Count(v => v >= 4.0 && v <= 7.0);

            // Процент значений в норме
            double percentInTarget = (double)inTarget / values.Count * 100;

            // --- ШАГ 3: Отображаем статистику на форме ---
            labelAvg.Text = $"Средняя: {avg:F1} ммоль/л";
            labelMin.Text = $"Мин: {min:F1} ммоль/л";
            labelMax.Text = $"Макс: {max:F1} ммоль/л";
            labelTarget.Text = $"В норме: {percentInTarget:F0}%";
        }

        // ================================================================
        // ОБРАБОТЧИК КНОПКИ "ПОСТРОИТЬ ГРАФИК"
        // Вызывается, когда пользователь нажимает кнопку
        // ================================================================
        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            // Просто перезагружаем данные за выбранный период
            LoadChartData();
        }

        // ================================================================
        // ОБНОВЛЕНИЕ ДАННЫХ (вызывается из FormMain при переключении вкладки)
        // ================================================================
        public void RefreshData()
        {
            // При переходе на вкладку показываем данные за неделю
            comboBoxPeriod.SelectedIndex = 1; // "Неделя"

            // Загружаем данные
            LoadChartData();
        }
    }
}