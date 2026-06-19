using System;

namespace T1D_Journal.Models.DTO
{
	public class GlucoseReadingDto
	{
		public int ReadingID { get; set; }
		public int UserID { get; set; }
		public DateTime ReadingDateTime { get; set; }
		public decimal GlucoseValue { get; set; }
		public string MealTag { get; set; }  // BeforeMeal, AfterMeal, Fasting, Night
		public string Note { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}