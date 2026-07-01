using System;
using System.Drawing;
using System.Windows.Forms;
using T1D_Journal.Models;
using T1D_Journal.WinForms.UserControls;

namespace T1D_Journal.WinForms.Forms
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();

			// ================================================================
			// НАСТРОЙКА ОКНА
			// ================================================================
			this.Text = $"Дневник T1D - {CurrentUser.FullName}";
			this.WindowState = FormWindowState.Normal;
			this.Size = new Size(950, 700);
			this.MinimumSize = new Size(800, 600);
			this.MaximumSize = new Size(1200, 850);

			// ================================================================
			// НАСТРОЙКА ВКЛАДОК (КРУПНЫЕ, ЖИРНЫЕ, ЦВЕТНЫЕ)
			// ================================================================
			tabControl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			tabControl.SizeMode = TabSizeMode.Fixed;
			tabControl.ItemSize = new Size(150, 32);
			tabControl.Padding = new Point(10, 8);
			tabControl.BackColor = Color.FromArgb(245, 245, 250);

			// ================================================================
			// КРАСИВАЯ ШАПКА
			// ================================================================
			Panel headerPanel = new Panel();
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 70;
			headerPanel.BackColor = Color.White;
			headerPanel.Padding = new Padding(25, 0, 25, 0);

			// ---- Приветствие слева ----
			Label welcomeLabel = new Label();
			welcomeLabel.Text = $"👋 Здравствуйте, {CurrentUser.FullName}!";
			welcomeLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
			welcomeLabel.ForeColor = Color.FromArgb(40, 40, 60);
			welcomeLabel.AutoSize = true;
			welcomeLabel.Location = new Point(15, 22);

			// ---- Панель для кнопок справа ----
			Panel buttonPanel = new Panel();
			buttonPanel.Dock = DockStyle.Right;
			buttonPanel.Width = 400;
			buttonPanel.Height = 70;
			buttonPanel.BackColor = Color.Transparent;

			// ---- Кнопка "Сменить пользователя" ----
			Button buttonSwitchUser = new Button();
			buttonSwitchUser.Text = "🔑 Сменить пользователя";
			buttonSwitchUser.Size = new Size(220, 38);
			buttonSwitchUser.Location = new Point(0, 16);
			buttonSwitchUser.FlatStyle = FlatStyle.Flat;
			buttonSwitchUser.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 180);
			buttonSwitchUser.BackColor = Color.FromArgb(240, 248, 255);
			buttonSwitchUser.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			buttonSwitchUser.ForeColor = Color.FromArgb(50, 50, 80);
			buttonSwitchUser.Cursor = Cursors.Hand;
			buttonSwitchUser.Click += ButtonSwitchUser_Click;

			// ---- Кнопка "Выход" ----
			Button buttonExit = new Button();
			buttonExit.Text = "❌ Выход";
			buttonExit.Size = new Size(140, 38);
			buttonExit.Location = new Point(235, 16);
			buttonExit.FlatStyle = FlatStyle.Flat;
			buttonExit.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 180);
			buttonExit.BackColor = Color.FromArgb(255, 240, 240);
			buttonExit.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			buttonExit.ForeColor = Color.FromArgb(120, 40, 40);
			buttonExit.Cursor = Cursors.Hand;
			buttonExit.Click += ButtonExit_Click;

			buttonPanel.Controls.Add(buttonSwitchUser);
			buttonPanel.Controls.Add(buttonExit);

			headerPanel.Controls.Add(buttonPanel);
			headerPanel.Controls.Add(welcomeLabel);

			// ---- Тонкая линия-разделитель снизу ----
			Label lineSeparator = new Label();
			lineSeparator.Dock = DockStyle.Bottom;
			lineSeparator.Height = 2;
			lineSeparator.BackColor = Color.FromArgb(210, 210, 220);
			headerPanel.Controls.Add(lineSeparator);

			// ================================================================
			// ДОБАВЛЯЕМ ШАПКУ НА ФОРМУ
			// ================================================================
			this.Controls.Add(headerPanel);

			// ================================================================
			// ПРАВИЛЬНЫЙ ПОРЯДОК СЛОЁВ (шапка сзади, вкладки спереди)
			// ================================================================
			headerPanel.SendToBack();
			tabControl.BringToFront();

			// ================================================================
			// ВКЛАДКА "ОТЧЁТЫ" — ВРЕМЕННО ОТКЛЮЧЕНА
			// ================================================================
			tabPageReports.Enabled = false;
		}

		// ================================================================
		// ЗАГРУЗКА ФОРМЫ
		// ================================================================
		private void FormMain_Load(object sender, EventArgs e)
		{
			LoadAllTabsData();
		}

		// ================================================================
		// ЗАГРУЗКА ДАННЫХ НА ВСЕХ ВКЛАДКАХ
		// ================================================================
		private void LoadAllTabsData()
		{
			bool isTestMode = (CurrentUser.Login == "test");

			foreach (Control ctrl in tabPageDashboard.Controls)
			{
				if (ctrl is DashboardControl dashboard)
				{
					if (isTestMode)
						dashboard.LoadTestData();
					else
						dashboard.RefreshData();
					break;
				}
			}

			foreach (Control ctrl in tabPageDataEntry.Controls)
			{
				if (ctrl is DataEntryControl dataEntry)
				{
					dataEntry.LoadData();
					break;
				}
			}

			foreach (Control ctrl in tabPageJournal.Controls)
			{
				if (ctrl is JournalControl journal)
				{
					if (isTestMode)
						journal.LoadTestData();
					else
						journal.RefreshData();
					break;
				}
			}
		}

		// ================================================================
		// ПЕРЕКЛЮЧЕНИЕ ВКЛАДОК
		// ================================================================
		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool isTestMode = (CurrentUser.Login == "test");

			if (tabControl.SelectedIndex == 0)
			{
				foreach (Control ctrl in tabPageDashboard.Controls)
				{
					if (ctrl is DashboardControl dashboard)
					{
						if (isTestMode)
							dashboard.LoadTestData();
						else
							dashboard.RefreshData();
						break;
					}
				}
			}

			if (tabControl.SelectedIndex == 1)
			{
				foreach (Control ctrl in tabPageDataEntry.Controls)
				{
					if (ctrl is DataEntryControl dataEntry)
					{
						dataEntry.LoadData();
						break;
					}
				}
			}

			if (tabControl.SelectedIndex == 2)
			{
				foreach (Control ctrl in tabPageJournal.Controls)
				{
					if (ctrl is JournalControl journal)
					{
						if (isTestMode)
							journal.LoadTestData();
						else
							journal.RefreshData();
						break;
					}
				}
			}
		}

		// ================================================================
		// ОБРАБОТЧИК: СМЕНИТЬ ПОЛЬЗОВАТЕЛЯ
		// ================================================================
		private void ButtonSwitchUser_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show(
				"Вы уверены, что хотите сменить пользователя?\n\nПриложение будет перезапущено.",
				"Смена пользователя",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				System.Diagnostics.Process.Start(Application.ExecutablePath);
				Application.Exit();
			}
		}

		// ================================================================
		// ОБРАБОТЧИК: ВЫХОД ИЗ ПРИЛОЖЕНИЯ
		// ================================================================
		private void ButtonExit_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show(
				"Вы уверены, что хотите выйти из приложения?",
				"Подтверждение выхода",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				Application.Exit();
			}
		}

		// ================================================================
		// ЗАКРЫТИЕ ФОРМЫ (через крестик)
		// ================================================================
		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}
	}
}