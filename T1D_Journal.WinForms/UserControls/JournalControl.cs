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

			this.buttonFilter.Click += BtnFilter_Click;
			this.buttonRefresh.Click += BtnRefresh_Click;

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
			dataGridViewJournal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			dataGridViewJournal.ColumnHeadersVisible = true;
			dataGridViewJournal.ColumnHeadersHeight = 35;
			dataGridViewJournal.EnableHeadersVisualStyles = false;
			dataGridViewJournal.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
			dataGridViewJournal.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

			if (dataGridViewJournal.Columns["DateTime"] != null)
				dataGridViewJournal.Columns["DateTime"].Width = 150;
			if (dataGridViewJournal.Columns["Glucose"] != null)
				dataGridViewJournal.Columns["Glucose"].Width = 120;
			if (dataGridViewJournal.Columns["MealTag"] != null)
				dataGridViewJournal.Columns["MealTag"].Width = 120;
			if (dataGridViewJournal.Columns["Note"] != null)
				dataGridViewJournal.Columns["Note"].Width = 300;
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