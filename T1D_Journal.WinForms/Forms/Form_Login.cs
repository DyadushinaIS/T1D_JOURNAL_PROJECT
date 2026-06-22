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

            // ================================================================
            // ПОДПИСКА НА СОБЫТИЯ КЛАВИШ Enter
            // ================================================================

            // Когда пользователь нажимает Enter в поле логина
            this.textBoxLogin.KeyDown += TextBoxLogin_KeyDown;

            // Когда пользователь нажимает Enter в поле пароля
            this.textBoxPassword.KeyDown += TextBoxPassword_KeyDown;

            // Когда пользователь нажимает Enter на самой форме (глобально)
            this.KeyDown += Form_Login_KeyDown;
        }

        // ================================================================
        // ОБРАБОТЧИК НАЖАТИЯ КЛАВИШ В ПОЛЕ ЛОГИНА
        // Если нажат Enter → переходим на поле пароля
        // ================================================================
        private void TextBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Переключаем фокус на поле пароля
                textBoxPassword.Focus();

                // Говорим системе, что мы обработали нажатие
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // ================================================================
        // ОБРАБОТЧИК НАЖАТИЯ КЛАВИШ В ПОЛЕ ПАРОЛЯ
        // Если нажат Enter → вызываем вход
        // ================================================================
        private void TextBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Вызываем метод входа
                buttonLogin_Click(sender, e);

                // Говорим системе, что мы обработали нажатие
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // ================================================================
        // ОБРАБОТЧИК НАЖАТИЯ КЛАВИШ НА ВСЕЙ ФОРМЕ (запасной вариант)
        // Если Enter нажат где-то на форме — вызываем вход
        // ================================================================
        private void Form_Login_KeyDown(object sender, KeyEventArgs e)
        {
            // Если нажат Enter и активное поле — не поле ввода (чтобы не было двойного срабатывания)
            if (e.KeyCode == Keys.Enter &&
                !(this.ActiveControl is TextBox))
            {
                buttonLogin_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // ================================================================
        // ОБРАБОТЧИК КНОПКИ "ВОЙТИ"
        // ================================================================
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Получаем введённые данные
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text;

            // Проверка: заполнены ли поля
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ============================================================
            // ТЕСТОВЫЙ РЕЖИМ (для разработки)
            // ============================================================
            if (login == "test" && password == "123")
            {
                bool connected = DbHelper.TestConnection();

                if (connected)
                {
                    MessageBox.Show("✅ Подключение к БД успешно!",
                                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("❌ Не удалось подключиться к БД.\nПроверьте строку подключения в App.config.",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                FormMain mainForm = new FormMain();
                mainForm.Show();
                this.Hide();
                return;
            }

            // ============================================================
            // РЕАЛЬНАЯ АВТОРИЗАЦИЯ ЧЕРЕЗ БАЗУ ДАННЫХ
            // ============================================================
            try
            {
                // Хешируем введённый пароль
                string hashedPassword = HashHelper.GetMD5Hash(password);

                var userRepo = new UserRepository();
                var user = userRepo.Authenticate(login, hashedPassword);

                if (user != null)
                {
                    // Сохраняем данные вошедшего пользователя
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
        // ОБРАБОТЧИК КНОПКИ "ВЫХОД"
        // ================================================================
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}