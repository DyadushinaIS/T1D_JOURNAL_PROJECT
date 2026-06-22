using System;
using System.Windows.Forms;
// Подключаем пространство имен с UserControl-ами, чтобы использовать их в коде
using T1D_Journal.WinForms.UserControls;

namespace T1D_Journal.WinForms.Forms
{
	public partial class FormMain : Form
	{
		// ================================================================
		// КОНСТРУКТОР - вызывается при создании главной формы
		// ================================================================
		public FormMain()
		{
			// Инициализация всех компонентов (кнопки, вкладки и т.д.)
			// Этот метод автоматически генерируется в FormMain.Designer.cs
			InitializeComponent();

			// Настройка внешнего вида формы
			this.WindowState = FormWindowState.Maximized;  // Открывать на весь экран
			this.Text = "Дневник самоконтроля T1D";        // Заголовок окна
		}

		// ================================================================
		// ОБРАБОТЧИК СОБЫТИЯ ЗАГРУЗКИ ФОРМЫ
		// Вызывается один раз, когда форма загружается и становится видимой
		// ================================================================
		private void FormMain_Load(object sender, EventArgs e)
		{
			// Загружаем начальные данные для вкладки "Ввод данных"
			// Ищем на вкладке наш DataEntryControl и вызываем его метод LoadData()
			foreach (Control ctrl in tabPageDataEntry.Controls)
			{
				// Проверяем, является ли контрол именно DataEntryControl
				if (ctrl is DataEntryControl dataEntry)
				{
					// Вызываем метод загрузки данных (устанавливает дату, очищает поля)
					dataEntry.LoadData();
					break; // Нашли - выходим из цикла
				}
			}

			// Можно также добавить приветствие пользователя или другую логику
		}

		// ================================================================
		// ОБРАБОТЧИК ПЕРЕКЛЮЧЕНИЯ ВКЛАДОК
		// Вызывается каждый раз, когда пользователь кликает на другую вкладку
		// ================================================================
		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Вкладка "Главная" (индекс 0)
			if (tabControl.SelectedIndex == 0)
			{
				foreach (Control ctrl in tabPageDashboard.Controls)
				{
					if (ctrl is DashboardControl dashboard)
					{
						dashboard.RefreshData();
						break;
					}
				}
			}

			// Вкладка "Ввод данных" (индекс 1)
			if (tabControl.SelectedIndex == 1)
			{
				// При переходе на вкладку "Ввод данных" обновляем данные
				foreach (Control ctrl in tabPageDataEntry.Controls)
				{
					if (ctrl is DataEntryControl dataEntry)
					{
						// Очищаем поля для нового ввода
						dataEntry.LoadData();
						break;
					}
				}
			}
			// Вкладка "Журнал" (индекс 2)
			if (tabControl.SelectedIndex == 2)
			{
				foreach (Control ctrl in tabPageJournal.Controls)
				{
					if (ctrl is JournalControl journal)
					{
						journal.RefreshData();
						break;
					}
				}
			}
			// Вкладка "Отчеты" (индекс 3)
			if (tabControl.SelectedIndex == 3)
			{
				foreach (Control ctrl in tabPageReports.Controls)
				{
					if (ctrl is ReportsControl reports)
					{
						reports.RefreshData();
						break;
					}
				}
			}

			// ================================================================
			// ДЛЯ БУДУЩИХ ВКЛАДОК (добавим позже)
			// ================================================================
			/*
            if (tabControl.SelectedIndex == 0)
            {
                // Вкладка "Главная" - обновить дашборд
            }
            else if (tabControl.SelectedIndex == 2)
            {
                // Вкладка "Журнал" - обновить таблицу
            }
            else if (tabControl.SelectedIndex == 3)
            {
                // Вкладка "Отчёты" - обновить графики
            }
            */
		}

		// ================================================================
		// ОБРАБОТЧИК ЗАКРЫТИЯ ФОРМЫ
		// Вызывается, когда форма уже закрылась
		// ================================================================
		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			// Завершаем приложение полностью
			// Если просто закрыть форму, приложение может остаться в фоне
			Application.Exit();
		}
	}
}