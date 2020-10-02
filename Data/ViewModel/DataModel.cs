using System.ComponentModel;

namespace Data.ViewModel
{
	public class DataModel
	{
		[DisplayName("Place Name")]
		public string PlaceName { get; set; }
		[DisplayName("State Name")]
		public string StateName { get; set; }
		public int Year { get; set; }
		[DisplayName("Disadvantage")]
		public int? DisadvantageScore { get; set; }
		[DisplayName("Advantage")]
		public int? AdvantageDisadvantageScore { get; set; }

		public int? MedianScore { get; set; }
	}
}
