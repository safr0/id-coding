using System.ComponentModel;

namespace Data.ViewModel
{
	public class ReportModel
	{
		public int? Disadvantage2011 { get; set; }
		public int? Disadvantage2016 { get; set; }
		[DisplayName("Compare 2016 to 2011")]
		public int Comparison
		{
			get
			{
				return Disadvantage2016.GetValueOrDefault() - Disadvantage2011.GetValueOrDefault();
			}
		}
		[DisplayName("State Name")]
		public string StateName { get; set; }
		[DisplayName("Place Name")]
		public string PlaceName { get; set; }		
        public int? MedianScore { get; set; }
    }
}
