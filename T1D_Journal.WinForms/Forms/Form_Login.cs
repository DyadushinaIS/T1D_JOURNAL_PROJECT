using System;
using System.Windows.Forms;
using T1D_Journal.DAL.Helpers;
using T1D_Journal.DAL.Repositories;
using T1D_Journal.Models.DTO;
using T1D_Journal.Models;
using T1D_Journal.Helpers;

namespace T1D_Journal.WinForms.Forms
{
	public partial class Form_Login : Form
	{
		public Form_Login()
		{
			InitializeComponent();

			// Подписка на Enter
			this.textBoxLogin.KeyDown += TextBoxLogin_KeyDown;
			this.textBoxPassword.KeyDown += TextBoxPassword_KeyDown;
			this.KeyDown += Form_Login_KeyDown;

			// Фокус на поле логина при загрузке формы
			this.Load += Form_Login_Load;
		}

		// ================================================================
		// ФОКУС НА ПОЛЕ ЛОГИНА ПРИ СТАРТЕ
		// ================================================================
		private void Form_Login_Load(object sender, EventArgs e)
		{
			textBoxLogin.Focus();
		}

		// ================================================================
		// Enter в поле логина → переход на пароль
		// ================================================================
		private void TextBoxLogin_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				textBoxPassword.Focus();
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		// ================================================================
		// Enter в поле пароля → вход
		// ================================================================
		private void TextBoxPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonLogin_Click(sender, e);
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		// ================================================================
		// Enter на форме (запасной вариант)
		// ================================================================
		private void Form_Login_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && !(this.ActiveControl is TextBox))
			{
				buttonLogin_Click(sender, e);
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		// ================================================================
		// КНОПКА "ВОЙТИ"
		// ================================================================
		private void buttonLogin_Click(object sender, EventArgs e)
		{
			string login = textBoxLogin.Text.Trim();
			string password = textBoxPassword.Text;

			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Введите логин и пароль!", "Ошибка",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// ============================================================
			// ТЕСТОВЫЙ РЕЖИМ (без проверки БД!)
			// ============================================================
			if (login == "test" && password == "123")
			{
				// Заполняем CurrentUser тестовыми данными
				CurrentUser.ID = 1;
				CurrentUser.Login = "test";
				CurrentUser.FullName = "Тестовый Пользователь";

				MessageBox.Show("✅ Тестовый вход выполнен! (БД не требуется)",
								"Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Открываем главную форму
				FormMain mainForm = new FormMain();
				mainForm.Show();
				this.Hide();
				return;
			}

			// ============================================================
			// РЕАЛЬНЫЙ ВХОД (требуется БД)
			// ============================================================
			try
			{
				// Проверяем подключение к БД
				if (!DbHelper.TestConnection())
				{
					MessageBox.Show("❌ Не удалось подключиться к базе данных.\n\n" +
									"Проверьте:\n" +
									"1. Запущен ли SQL Server\n" +
									"2. Правильная ли строка подключения в App.config\n" +
									"3. Существует ли база данных DiabetesJournalDB",
									"Ошибка подключения",
									MessageBoxButtons.OK,
									MessageBoxIcon.Error);
					return;
				}

				string hashedPassword = HashHelper.GetMD5Hash(password);
				var userRepo = new UserRepository();
				var user = userRepo.Authenticate(login, hashedPassword);

				if (user != null)
				{
					CurrentUser.ID = user.UserID;
					CurrentUser.FullName = user.FullName;
					CurrentUser.Login = user.Login;

					MessageBox.Show($"Добро пожаловать, {user.FullName}!", "Успех",
									MessageBoxButtons.OK, MessageBoxIcon.Information);

					FormMain mainForm = new FormMain();
					mainForm.Show();
					this.Hide();
				}
				else
				{
					MessageBox.Show("Неверный логин или пароль!", "Ошибка",
									MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// ================================================================
		// КНОПКА "ВЫХОД"
		// ================================================================
		private void buttonExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}