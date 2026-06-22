using Microsoft.Data.SqlClient;
using T1D_Journal.DAL.Helpers;
using T1D_Journal.Models.DTO;
using System;
using System.Data;

namespace T1D_Journal.DAL.Repositories
{
    public class UserRepository
    {
        // ================================================================
        // ПОЛУЧИТЬ ПОЛЬЗОВАТЕЛЯ ПО ЛОГИНУ
        // ================================================================
        public UserDto GetUserByLogin(string login)
        {
            string query = "SELECT UserID, Login, PasswordHash, FullName, DateOfBirth, " +
                           "TargetGlucoseMin, TargetGlucoseMax, CreatedAt FROM Users WHERE Login = @Login";

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

        // ================================================================
        // ПОЛУЧИТЬ ПОЛЬЗОВАТЕЛЯ ПО ID
        // ================================================================
        public UserDto GetUserById(int userId)
        {
            string query = "SELECT UserID, Login, PasswordHash, FullName, DateOfBirth, " +
                           "TargetGlucoseMin, TargetGlucoseMax, CreatedAt FROM Users WHERE UserID = @UserID";

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

        // ================================================================
        // ПРОВЕРКА ЛОГИНА И ПАРОЛЯ (АВТОРИЗАЦИЯ)
        // ================================================================
        public UserDto Authenticate(string login, string password)
        {
            string query = "SELECT UserID, Login, PasswordHash, FullName, DateOfBirth, " +
                           "TargetGlucoseMin, TargetGlucoseMax, CreatedAt FROM Users " +
                           "WHERE Login = @Login AND PasswordHash = @PasswordHash";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Login", login),
                new SqlParameter("@PasswordHash", password)
            };

            DataTable dt = DbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return MapToUserDto(row);
        }

        // ================================================================
        // СОЗДАНИЕ НОВОГО ПОЛЬЗОВАТЕЛЯ
        // ================================================================
        public int CreateUser(UserDto user)
        {
            string query = @"INSERT INTO Users (Login, PasswordHash, FullName, DateOfBirth, 
                              TargetGlucoseMin, TargetGlucoseMax)
                              VALUES (@Login, @PasswordHash, @FullName, @DateOfBirth, 
                              @TargetGlucoseMin, @TargetGlucoseMax);
                              SELECT SCOPE_IDENTITY();";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Login", user.Login),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@FullName", (object)user.FullName ?? DBNull.Value),
                new SqlParameter("@DateOfBirth", (object)user.DateOfBirth ?? DBNull.Value),
                new SqlParameter("@TargetGlucoseMin", user.TargetGlucoseMin),
                new SqlParameter("@TargetGlucoseMax", user.TargetGlucoseMax)
            };

            return Convert.ToInt32(DbHelper.ExecuteScalar(query, parameters));
        }

        // ================================================================
        // ПРЕОБРАЗОВАНИЕ DataRow В UserDto
        // ================================================================
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
                InsulinSensitivityFactor = null,  // Пока не используем
                CarbCoefficient = null,           // Пока не используем
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            };
        }
    }
}