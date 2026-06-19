using Microsoft.Data.SqlClient;
using T1D_Journal.DAL.Helpers;
using T1D_Journal.Models.DTO;
using System;
using System.Data;

namespace T1D_Journal.DAL.Repositories
{
	public class UserRepository
	{
		// Получить пользователя по логину
		public UserDto GetUserByLogin(string login)
		{
			string query = "SELECT UserID, Login, PasswordHash, FullName, DateOfBirth, " +
						   "TargetGlucoseMin, TargetGlucoseMax, InsulinSensitivityFactor, " +
						   "CarbCoefficient, CreatedAt FROM Users WHERE Login = @Login";

			var parameters = new SqlParameter[]
			{
				new SqlParameter("@Login", login)
			};

			DataTable dt = DbHelper.ExecuteQuery(query, parameters);

			if (dt.Rows.Count == 0)
				return null;

			DataRow row = dt.Rows[0];
			return MapToUserDto(row);
		}

		// Получить пользователя по ID
		public UserDto GetUserById(int userId)
		{
			string query = "SELECT UserID, Login, PasswordHash, FullName, DateOfBirth, " +
						   "TargetGlucoseMin, TargetGlucoseMax, InsulinSensitivityFactor, " +
						   "CarbCoefficient, CreatedAt FROM Users WHERE UserID = @UserID";

			var parameters = new SqlParameter[]
			{
				new SqlParameter("@UserID", userId)
			};

			DataTable dt = DbHelper.ExecuteQuery(query, parameters);

			if (dt.Rows.Count == 0)
				return null;

			DataRow row = dt.Rows[0];
			return MapToUserDto(row);
		}

		// Проверка логина и пароля (простая версия, без хеширования пока)
		public UserDto Authenticate(string login, string password)
		{
			string query = "SELECT UserID, Login, PasswordHash, FullName, DateOfBirth, " +
						   "TargetGlucoseMin, TargetGlucoseMax, InsulinSensitivityFactor, " +
						   "CarbCoefficient, CreatedAt FROM Users WHERE Login = @Login AND PasswordHash = @PasswordHash";

			var parameters = new SqlParameter[]
			{
				new SqlParameter("@Login", login),
				new SqlParameter("@PasswordHash", password) // пока без хеширования
            };

			DataTable dt = DbHelper.ExecuteQuery(query, parameters);

			if (dt.Rows.Count == 0)
				return null;

			DataRow row = dt.Rows[0];
			return MapToUserDto(row);
		}

		// Создание нового пользователя
		public int CreateUser(UserDto user)
		{
			string query = @"INSERT INTO Users (Login, PasswordHash, FullName, DateOfBirth, 
                              TargetGlucoseMin, TargetGlucoseMax, InsulinSensitivityFactor, CarbCoefficient)
                              VALUES (@Login, @PasswordHash, @FullName, @DateOfBirth, 
                              @TargetGlucoseMin, @TargetGlucoseMax, @InsulinSensitivityFactor, @CarbCoefficient);
                              SELECT SCOPE_IDENTITY();";

			var parameters = new SqlParameter[]
			{
				new SqlParameter("@Login", user.Login),
				new SqlParameter("@PasswordHash", user.PasswordHash),
				new SqlParameter("@FullName", (object)user.FullName ?? DBNull.Value),
				new SqlParameter("@DateOfBirth", (object)user.DateOfBirth ?? DBNull.Value),
				new SqlParameter("@TargetGlucoseMin", user.TargetGlucoseMin),
				new SqlParameter("@TargetGlucoseMax", user.TargetGlucoseMax),
				new SqlParameter("@InsulinSensitivityFactor", (object)user.InsulinSensitivityFactor ?? DBNull.Value),
				new SqlParameter("@CarbCoefficient", (object)user.CarbCoefficient ?? DBNull.Value)
			};

			return Convert.ToInt32(DbHelper.ExecuteScalar(query, parameters));
		}

		// Преобразование DataRow в UserDto
		private UserDto MapToUserDto(DataRow row)
		{
			return new UserDto
			{
				UserID = Convert.ToInt32(row["UserID"]),
				Login = row["Login"].ToString(),
				PasswordHash = row["PasswordHash"].ToString(),
				FullName = row["FullName"]?.ToString(),
				DateOfBirth = row["DateOfBirth"] == DBNull.Value ? null : (DateTime?)row["DateOfBirth"],
				TargetGlucoseMin = Convert.ToDecimal(row["TargetGlucoseMin"]),
				TargetGlucoseMax = Convert.ToDecimal(row["TargetGlucoseMax"]),
				InsulinSensitivityFactor = row["InsulinSensitivityFactor"] == DBNull.Value ? null : (decimal?)row["InsulinSensitivityFactor"],
				CarbCoefficient = row["CarbCoefficient"] == DBNull.Value ? null : (decimal?)row["CarbCoefficient"],
				CreatedAt = Convert.ToDateTime(row["CreatedAt"])
			};
		}
	}
}