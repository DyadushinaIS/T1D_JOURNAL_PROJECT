using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T1D_Journal.WinForms.Forms
{
	public partial class Form_Login : Form
	{
		public Form_Login()
		{
			InitializeComponent();
		}

		// ОБРАБОТЧИК КНОПКИ "ВОЙТИ" - вызывается при клике на кнопку
		private void buttonLogin_Click(object sender, EventArgs e)
		{
			// Получаем введённые данные из текстовых полей
			string login = textBoxLogin.Text.Trim();      // Trim() убирает пробелы в начале и конце
			string password = textBoxPassword.Text;       // Пароль без Trim(), т.к. пробелы могут быть частью пароля

			// ПРОВЕРКА: заполнены ли поля
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
			{
				// Если логин или пароль пустые - показываем предупреждение
				MessageBox.Show("Введите логин и пароль!", "Ошибка",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return; // Выходим из метода, дальше не идём
			}

			// ============================================================
			// ТЕСТОВЫЙ РЕЖИМ (без БД) - ДЛЯ РАЗРАБОТКИ ИНТЕРФЕЙСА
			// ============================================================
			// Пока БД нет, используем тестовую пару логин/пароль
			// Потом этот блок нужно будет УДАЛИТЬ (не закомментировать, а удалить)

			if (login == "test" && password == "123")
			{
				// Тестовый вход успешен
				MessageBox.Show("Тестовый вход выполнен!", "Успех",
								MessageBoxButtons.OK, MessageBoxIcon.Information);

				// СОЗДАЁМ ГЛАВНУЮ ФОРМУ
				// FormMain - это основное окно программы (пока пустое, заполним позже)
				FormMain mainForm = new FormMain();

				// ПОКАЗЫВАЕМ ГЛАВНУЮ ФОРМУ
				mainForm.Show();

				// СКРЫВАЕМ ФОРМУ ЛОГИНА (не закрываем, а прячем)
				// Если закрыть - приложение завершится
				this.Hide();

				return; // Выходим, код ниже не выполняется
			}

			// ============================================================
			// КОД С БД - РАБОТАЕТ, КОГДА ПОЯВИТСЯ БАЗА ДАННЫХ
			// СЕЙЧАС ЗАКОММЕНТИРОВАН, ПОТОМ РАСКОММЕНТИРОВАТЬ
			// И УДАЛИТЬ ТЕСТОВЫЙ БЛОК ВЫШЕ
			// ============================================================
			/*
            try
            {
                // 1. СОЗДАЁМ РЕПОЗИТОРИЙ - объект, который работает с БД
                var userRepo = new UserRepository();
                
                // 2. ПЫТАЕМСЯ НАЙТИ ПОЛЬЗОВАТЕЛЯ В БД
                // Метод Authenticate проверяет логин и пароль в таблице Users
                var user = userRepo.Authenticate(login, password);
                
                // 3. ПРОВЕРЯЕМ, НАЙДЕН ЛИ ПОЛЬЗОВАТЕЛЬ
                if (user != null) // если не null - пользователь существует
                {
                    // Успешный вход - приветствуем пользователя
                    MessageBox.Show($"Добро пожаловать, {user.FullName}!", "Успех",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Открываем главную форму
                    FormMain mainForm = new FormMain();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    // Пользователь не найден - ошибка авторизации
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Если что-то пошло не так (нет БД, ошибка подключения и т.д.)
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
		}

		// ОБРАБОТЧИК КНОПКИ "ВЫХОД" - закрывает приложение
		private void buttonExit_Click(object sender, EventArgs e)
		{
			// Application.Exit() - корректно завершает всё приложение
			// В отличие от this.Close(), который закроет только форму логина,
			// но приложение продолжит работать в фоне
			Application.Exit();
		}
	}
}
