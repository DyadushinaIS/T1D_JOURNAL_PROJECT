using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace T1D_Journal.WinForms.UserControls
{
	public partial class ReportsControl : UserControl
	{
		// ================================================================
		// КОНСТРУКТОР
		// ================================================================
		public ReportsControl()
		{
			InitializeComponent();

			// Настройка панели статистики
			panelStats.Dock = DockStyle.Top;
			panelStats.Height = 100;

			// Настройка графика
			cartesianChartGlucose.Dock = DockStyle.Fill;

			// Подписка на события
			buttonGenerate.Click += ButtonGenerate_Click;

			// Загрузка тестовых данных
			LoadTestData();
		}

		// ================================================================
		// ЗАГРУЗКА ТЕСТОВЫХ ДАННЫХ
		// ================================================================
		private void LoadTestData()
		{
			// Тестовые данные: дата и уровень глюкозы
			var testData = new List<KeyValuePair<DateTime, double>>
			{
				new KeyValuePair<DateTime, double>(DateTime.Now.AddDays(-6), 5.2),
				new KeyValuePair<DateTime, double>(DateTime.Now.AddDays(-5), 7.8),
				new KeyValuePair<DateTime, double>(DateTime.Now.AddDays(-4), 4.5),
				new KeyValuePair<DateTime, double>(DateTime.Now.AddDays(-3), 6.1),
				new KeyValuePair<DateTime, double>(DateTime.Now.AddDays(-2), 8.2),
				new KeyValuePair<DateTime, double>(DateTime.Now.AddDays(-1), 5.0),
				new KeyValuePair<DateTime, double>(DateTime.Now, 6.3)
			};

			BuildChart(testData);
			UpdateStatistics(testData);
		}

		// ================================================================
		// ПОСТРОЕНИЕ ГРАФИКА
		// ================================================================
		private void BuildChart(List<KeyValuePair<DateTime, double>> data)
		{
			// Очищаем старый график
			cartesianChartGlucose.Series.Clear();
			cartesianChartGlucose.AxisX.Clear();
			cartesianChartGlucose.AxisY.Clear();

			// Создаём серию данных для графика
			var series = new LineSeries
			{
				Title = "Глюкоза (ммоль/л)",
				Values = new ChartValues<double>(),
				LineSmoothness = 0.5,  // Плавная линия
				PointGeometrySize = 15 // Размер точек
			};

			// Добавляем значения
			foreach (var point in data)
			{
				series.Values.Add(point.Value);
			}

			// Добавляем серию на график
			cartesianChartGlucose.Series.Add(series);

			// Настройка оси X (даты)
			cartesianChartGlucose.AxisX.Add(new Axis
			{
				Title = "Дата",
				Labels = data.ConvertAll(p => p.Key.ToString("dd.MM")),
				Separator = new Separator { Step = 1 }
			});

			// Настройка оси Y (глюкоза)
			var yAxis = new Axis
			{
				Title = "Глюкоза (ммоль/л)",
				MinValue = 0,
				MaxValue = 15,
				Separator = new Separator { Step = 1 }
			};

			// Целевая зона (4.0 - 7.0 ммоль/л)
			yAxis.Sections.Add(new AxisSection
			{
				FromValue = 4.0,
				ToValue = 7.0,
				Fill = new System.Windows.Media.SolidColorBrush(
					System.Windows.Media.Color.FromArgb(50, 0, 255, 0)),
				Stroke = new System.Windows.Media.SolidColorBrush(
					System.Windows.Media.Color.FromArgb(100, 0, 100, 0)),
				StrokeThickness = 1
			});

			cartesianChartGlucose.AxisY.Add(yAxis);
		}

		// ================================================================
		// ОБНОВЛЕНИЕ СТАТИСТИКИ
		// ================================================================
		private void UpdateStatistics(List<KeyValuePair<DateTime, double>> data)
		{
			if (data.Count == 0)
			{
				labelAvg.Text = "Средняя: --";
				labelMin.Text = "Мин: --";
				labelMax.Text = "Макс: --";
				labelTarget.Text = "В норме: --%";
				return;
			}

			// Извлекаем значения глюкозы
			var values = data.ConvertAll(p => p.Value);

			// Вычисляем статистику
			double avg = values.Average();
			double min = values.Min();
			double max = values.Max();
			int inTarget = values.Count(v => v >= 4.0 && v <= 7.0);
			double percentInTarget = (double)inTarget / values.Count * 100;

			// Отображаем
			labelAvg.Text = $"Средняя: {avg:F1} ммоль/л";
			labelMin.Text = $"Мин: {min:F1} ммоль/л";
			labelMax.Text = $"Макс: {max:F1} ммоль/л";
			labelTarget.Text = $"В норме: {percentInTarget:F0}%";
		}

		// ================================================================
		// ОБРАБОТЧИК КНОПКИ "ПОСТРОИТЬ ГРАФИК"
		// ================================================================
		private void ButtonGenerate_Click(object sender, EventArgs e)
		{
			// Получаем выбранный период
			string period = comboBoxPeriod.SelectedItem?.ToString() ?? "Неделя";

			// В зависимости от периода генерируем разные данные
			// Пока просто тестовые данные с разным количеством точек

			var testData = new List<KeyValuePair<DateTime, double>>();

			switch (period)
			{
				case "День":
					// Данные за день (каждый час)
					for (int i = 0; i < 8; i++)
					{
						double value = 4.0 + new Random().NextDouble() * 5.0;
						testData.Add(new KeyValuePair<DateTime, double>(
							DateTime.Now.AddHours(-7 + i), value));
					}
					break;

				case "Неделя":
					// Данные за неделю (каждый день)
					for (int i = 0; i < 7; i++)
					{
						double value = 4.0 + new Random().NextDouble() * 5.0;
						testData.Add(new KeyValuePair<DateTime, double>(
							DateTime.Now.AddDays(-6 + i), value));
					}
					break;

				case "Месяц":
					// Данные за месяц (каждые 2-3 дня)
					for (int i = 0; i < 14; i++)
					{
						double value = 4.0 + new Random().NextDouble() * 5.0;
						testData.Add(new KeyValuePair<DateTime, double>(
							DateTime.Now.AddDays(-28 + i * 2), value));
					}
					break;

				default:
					LoadTestData();
					return;
			}

			BuildChart(testData);
			UpdateStatistics(testData);
		}

		// ================================================================
		// ОБНОВЛЕНИЕ ДАННЫХ (вызывается из FormMain)
		// ================================================================
		public void RefreshData()
		{
			// При переходе на вкладку показываем данные за неделю
			comboBoxPeriod.SelectedIndex = 1; // "Неделя"
			ButtonGenerate_Click(null, null);
		}
	}
}