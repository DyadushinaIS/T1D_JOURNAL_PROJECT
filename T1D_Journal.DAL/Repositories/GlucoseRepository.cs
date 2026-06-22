using Microsoft.Data.SqlClient;
using T1D_Journal.DAL.Helpers;
using T1D_Journal.Models.DTO;
using System;
using System.Data;

namespace T1D_Journal.DAL.Repositories
{
    public class GlucoseRepository
    {
        // ================================================================
        // СОХРАНЕНИЕ НОВОГО ЗАМЕРА В БАЗУ ДАННЫХ
        // ================================================================
        public int Create(GlucoseReadingDto reading)
        {
            string query = @"INSERT INTO GlucoseReadings (UserID, ReadingDateTime, GlucoseValue, MealTag, Note)
                             VALUES (@UserID, @ReadingDateTime, @GlucoseValue, @MealTag, @Note);
                             SELECT SCOPE_IDENTITY();";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", reading.UserID),
                new SqlParameter("@ReadingDateTime", reading.ReadingDateTime),
                new SqlParameter("@GlucoseValue", reading.GlucoseValue),
                new SqlParameter("@MealTag", (object)reading.MealTag ?? DBNull.Value),
                new SqlParameter("@Note", (object)reading.Note ?? DBNull.Value)
            };

            // Выполняем запрос и получаем ID нового замера
            object result = DbHelper.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result);
        }

        // ================================================================
        // ПОЛУЧИТЬ ВСЕ ЗАМЕРЫ ПОЛЬЗОВАТЕЛЯ ЗА ПЕРИОД
        // ================================================================
        public DataTable GetByDateRange(int userId, DateTime from, DateTime to)
        {
            string query = @"SELECT ReadingID, ReadingDateTime, GlucoseValue, MealTag, Note, CreatedAt
                             FROM GlucoseReadings
                             WHERE UserID = @UserID
                               AND ReadingDateTime >= @From
                               AND ReadingDateTime <= @To
                             ORDER BY ReadingDateTime DESC";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@From", from),
                new SqlParameter("@To", to)
            };

            return DbHelper.ExecuteQuery(query, parameters);
        }

        // ================================================================
        // ПОЛУЧАЕМ ПОСЛЕДНИЕ N ЗАМЕРОВ ПОЛЬЗОВАТЕЛЯ
        // ================================================================
        public DataTable GetLastReadings(int userId, int count)
        {
            string query = @"SELECT TOP (@Count) ReadingID, ReadingDateTime, GlucoseValue, MealTag, Note, CreatedAt
                             FROM GlucoseReadings
                             WHERE UserID = @UserID
                             ORDER BY ReadingDateTime DESC";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Count", count)
            };

            return DbHelper.ExecuteQuery(query, parameters);
        }

        // ================================================================
        // ПОЛУЧИТЬ СТАТИСТИКУ ЗА СЕГОДНЯ
        // Возвращает: среднюю, мин, макс, процент в норме, количество замеров
        // ================================================================
        public (double Avg, double Min, double Max, double PercentInTarget, int Total) GetTodayStats(int userId)
        {
            string query = @"SELECT 
                        AVG(CAST(GlucoseValue AS FLOAT)) AS AvgGlucose,
                        MIN(GlucoseValue) AS MinGlucose,
                        MAX(GlucoseValue) AS MaxGlucose,
                        SUM(CASE WHEN GlucoseValue >= 4.0 AND GlucoseValue <= 7.0 THEN 1 ELSE 0 END) AS InTarget,
                        COUNT(*) AS Total
                     FROM GlucoseReadings
                     WHERE UserID = @UserID
                       AND CAST(ReadingDateTime AS DATE) = CAST(GETDATE() AS DATE)";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@UserID", userId)
            };

            DataTable dt = DbHelper.ExecuteQuery(query, parameters);

            // Проверяем, есть ли данные
            if (dt.Rows.Count == 0 || dt.Rows[0]["Total"] == DBNull.Value || Convert.ToInt32(dt.Rows[0]["Total"]) == 0)
            {
                // Если данных нет — возвращаем нули и Total = 0
                return (0, 0, 0, 0, 0);
            }

            DataRow row = dt.Rows[0];
            double avg = Convert.ToDouble(row["AvgGlucose"]);
            double min = Convert.ToDouble(row["MinGlucose"]);
            double max = Convert.ToDouble(row["MaxGlucose"]);
            int inTarget = Convert.ToInt32(row["InTarget"]);
            int total = Convert.ToInt32(row["Total"]);
            double percentInTarget = (double)inTarget / total * 100;

            return (avg, min, max, percentInTarget, total);
        }



        // ================================================================
        // ПОЛУЧАЕМ ДАННЫЕ ДЛЯ ГРАФИКА ЗА ПЕРИОД
        // Возвращает список (дата, значение глюкозы) для построения графика
        // ================================================================
        public DataTable GetChartData(int userId, DateTime from, DateTime to)
        {
            string query = @"SELECT ReadingDateTime, GlucoseValue
                     FROM GlucoseReadings
                     WHERE UserID = @UserID
                       AND ReadingDateTime >= @From
                       AND ReadingDateTime <= @To
                     ORDER BY ReadingDateTime ASC";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@UserID", userId),
        new SqlParameter("@From", from),
        new SqlParameter("@To", to)
            };

            return DbHelper.ExecuteQuery(query, parameters);
        }
    }
}