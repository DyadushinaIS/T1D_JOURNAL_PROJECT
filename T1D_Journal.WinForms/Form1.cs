using System;
using System.Windows.Forms;
// РАСКОММЕНТИРОВАТЬ КОГДА ПОЯВИТСЯ БД:
// using T1D_Journal.DAL.Repositories;
// using Microsoft.Data.SqlClient;
// using T1D_Journal.Models.DTO;

namespace T1D_Journal.WinForms
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			this.Load += Form1_Load;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// ============================================================
			// ТЕСТОВЫЙ РЕЖИМ (работает прямо сейчас, без БД)
			// ============================================================
			MessageBox.Show("Форма загружена!\n\nСейчас работаем без подключения к БД.\nИнтерфейс можно разрабатывать спокойно.",
							"Тестовый режим",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);

			// ============================================================
			// НИЖЕ ЗАКОММЕНТИРОВАН КОД ДЛЯ РАБОТЫ С БД
			// РАСКОММЕНТИРОВАТЬ КОГДА БД БУДЕТ ГОТОВА
			// ============================================================
			/*
            try
            {
                var userRepo = new UserRepository();
                var user = userRepo.Authenticate("patient1", "202cb962ac59075b964b07152d234b70");
                
                if (user != null)
                {
                    MessageBox.Show($"Добро пожаловать, {user.FullName}!\n" +
                                    $"Целевой диапазон: {user.TargetGlucoseMin} - {user.TargetGlucoseMax} ммоль/л",
                                    "Успех",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.\n\nПроверьте:\n1. Существует ли БД DiabetesJournalDB\n2. Есть ли там пользователь patient1\n3. Правильный ли пароль",
                                    "Информация",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Не удалось подключиться к базе данных.\n\nОшибка: {ex.Message}\n\nПроверьте:\n1. Запущен ли SQL Server\n2. Правильная ли строка подключения в app.config\n3. Существует ли БД DiabetesJournalDB",
                                "Ошибка подключения",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            */
		}
	}
}