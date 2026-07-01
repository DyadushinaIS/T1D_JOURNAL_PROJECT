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
		public JournalControl()
		{
			InitializeComponent();

			// ============================================================
			// РАЗМЕЩАЕМ ПАНЕЛЬ СВЕРХУ, ТАБЛИЦУ ПОД НЕЙ
			// (БЕЗ ВСЯКИХ BringToFront И SetChildIndex!)
			// ============================================================

			// 1. Панель фильтров — приклеиваем к верху
			panelControl.Dock = DockStyle.Top;
			panelControl.Height = 45;
			panelControl.BackColor = Color.FromArgb(245, 245, 250);

			// 2. Таблица — занимает всё оставшееся место
			dataGridViewJournal.Dock = DockStyle.Fill;

			// ============================================================
			// ПОДПИСКИ И ЛОГИКА
			// ============================================================
			this.buttonFilter.Click += BtnFilter_Click;
			this.buttonRefresh.Click += BtnRefresh_Click;

			SetupDataGridView();

			if (CurrentUser.Login == "test")
			{
				LoadTestData();
			}
			else
			{
				LoadDataFromDB(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1));
			}
		}

		private void SetupDataGridView()
		{
			dataGridViewJournal.Columns.Clear();

			dataGridViewJournal.Columns.Add("DateTime", "Дата и время");
			dataGridViewJournal.Columns.Add("Glucose", "Глюкоза (ммоль/л)");
			dataGridViewJournal.Columns.Add("MealTag", "Когда");
			dataGridViewJournal.Columns.Add("Note", "Примечание");

			dataGridViewJournal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewJournal.ReadOnly = true;
			dataGridViewJournal.AllowUserToAddRows = false;
			dataGridViewJournal.RowHeadersVisible = false;

			dataGridViewJournal.BorderStyle = BorderStyle.None;
			dataGridViewJournal.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dataGridViewJournal.GridColor = Color.FromArgb(220, 220, 230);

			dataGridViewJournal.EnableHeadersVisualStyles = false;

			dataGridViewJournal.ColumnHeadersVisible = true;
			dataGridViewJournal.ColumnHeadersHeight = 35;
			dataGridViewJournal.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 245);
			dataGridViewJournal.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			dataGridViewJournal.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 60);

			dataGridViewJournal.RowTemplate.Height = 35;
			dataGridViewJournal.DefaultCellStyle.Font = new Font("Segoe UI", 10);
			dataGridViewJournal.DefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 60);

			dataGridViewJournal.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 230, 255);
			dataGridViewJournal.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 20, 40);

			dataGridViewJournal.BackgroundColor = Color.White;
			dataGridViewJournal.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 252);

			if (dataGridViewJournal.Columns["Note"] != null)
				dataGridViewJournal.Columns["Note"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		}

		private void LoadDataFromDB(DateTime from, DateTime to)
		{
			try
			{
				var repo = new GlucoseRepository();
				DataTable dt = repo.GetByDateRange(CurrentUser.ID, from, to);

				dataGridViewJournal.Rows.Clear();

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

		public void RefreshData()
		{
			if (CurrentUser.Login == "test")
			{
				LoadTestData();
			}
			else
			{
				DateTime today = DateTime.Now.Date;
				LoadDataFromDB(today, today.AddDays(1).AddSeconds(-1));
				dateTimePickerFrom.Value = today;
				dateTimePickerTo.Value = today.AddDays(1).AddSeconds(-1);
			}
		}

		public void LoadTestData()
		{
			dataGridViewJournal.Rows.Clear();

			var testData = new System.Collections.Generic.List<object[]>
			{
				new object[] { DateTime.Now.AddHours(-2).ToString("dd.MM.yyyy HH:mm"), "5.2", "AfterMeal", "Обед" },
				new object[] { DateTime.Now.AddHours(-6).ToString("dd.MM.yyyy HH:mm"), "4.5", "Fasting", "Натощак" },
				new object[] { DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy HH:mm"), "6.1", "BeforeMeal", "Ужин" }
			};

			foreach (var row in testData)
			{
				dataGridViewJournal.Rows.Add(row);
			}

			dateTimePickerFrom.Value = DateTime.Now.Date;
			dateTimePickerTo.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
		}

		private void BtnFilter_Click(object sender, EventArgs e)
		{
			if (CurrentUser.Login == "test")
			{
				MessageBox.Show("В тестовом режиме фильтр не работает.\nИспользуйте реальный вход для фильтрации.",
								"Информация",
								MessageBoxButtons.OK,
								MessageBoxIcon.Information);
				return;
			}

			DateTime from = dateTimePickerFrom.Value.Date;
			DateTime to = dateTimePickerTo.Value.Date.AddDays(1).AddSeconds(-1);

			if (from > to)
			{
				MessageBox.Show("Дата 'С' не может быть позже даты 'По'!",
								"Ошибка",
								MessageBoxButtons.OK,
								MessageBoxIcon.Warning);
				return;
			}

			LoadDataFromDB(from, to);
			MessageBox.Show($"✅ Фильтр применён!\nС: {from:dd.MM.yyyy}\nПо: {to:dd.MM.yyyy}",
							"Информация",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
		}

		private void BtnRefresh_Click(object sender, EventArgs e)
		{
			RefreshData();
			MessageBox.Show("✅ Данные обновлены!\nФильтр сброшен на сегодня.",
							"Информация",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
		}
	}
}