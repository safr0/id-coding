﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.ViewModel
{
	public class Location
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LocationId { get; set; }
		public int? Code { get; set; }
		[DisplayName("Place name")]
		public string PlaceName { get; set; }
		public State State { get; set; }
	}
}
