using System;

namespace T1D_Journal.Models.DTO
{
	public class UserDto
	{
		public int UserID { get; set; }
		public string Login { get; set; }
		public string PasswordHash { get; set; }
		public string FullName { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public decimal TargetGlucoseMin { get; set; }
		public decimal TargetGlucoseMax { get; set; }
		public decimal? InsulinSensitivityFactor { get; set; }
		public decimal? CarbCoefficient { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}