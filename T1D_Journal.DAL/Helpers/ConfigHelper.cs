using System.Configuration;

namespace T1D_Journal.DAL.Helpers
{
	public static class ConfigHelper
	{
		public static string GetConnectionString(string name)
		{
			var connectionString = ConfigurationManager.ConnectionStrings[name]?.ConnectionString;

			if (string.IsNullOrEmpty(connectionString))
				throw new Exception($"Строка подключения '{name}' не найдена");

			return connectionString;
		}
	}
}