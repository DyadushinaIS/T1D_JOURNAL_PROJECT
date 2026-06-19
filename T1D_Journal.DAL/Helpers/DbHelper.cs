using Microsoft.Data.SqlClient;
using System.Data;

namespace T1D_Journal.DAL.Helpers
{
	public static class DbHelper
	{
		private static string GetConnectionString()
		{
			return ConfigHelper.GetConnectionString("DiabetesDB");
		}

		public static SqlConnection GetConnection()
		{
			return new SqlConnection(GetConnectionString());
		}

		// Выполнение запроса с возвратом DataTable
		public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
		{
			using (SqlConnection conn = GetConnection())
			using (SqlCommand cmd = new SqlCommand(query, conn))
			{
				if (parameters != null)
					cmd.Parameters.AddRange(parameters);

				using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
				{
					DataTable dt = new DataTable();
					adapter.Fill(dt);
					return dt;
				}
			}
		}

		// Выполнение запроса без возврата (INSERT, UPDATE, DELETE)
		public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
		{
			using (SqlConnection conn = GetConnection())
			using (SqlCommand cmd = new SqlCommand(query, conn))
			{
				if (parameters != null)
					cmd.Parameters.AddRange(parameters);

				conn.Open();
				return cmd.ExecuteNonQuery();
			}
		}

		// Выполнение запроса с возвратом одного значения
		public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
		{
			using (SqlConnection conn = GetConnection())
			using (SqlCommand cmd = new SqlCommand(query, conn))
			{
				if (parameters != null)
					cmd.Parameters.AddRange(parameters);

				conn.Open();
				return cmd.ExecuteScalar();
			}
		}
	}
}