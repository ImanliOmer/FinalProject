using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
	public class SliderUpdateVM
	{
		[MaxLength(20)]
		public string Title { get; set; }

		[MaxLength(50)]
		public string Description { get; set; }

		[Display(Name = "New Photo")]
		public IFormFile? NewPhoto { get; set; }
		public string? Photo { get; set; }
		public DateTime ModifiedAt { get; set; }
	}
}
