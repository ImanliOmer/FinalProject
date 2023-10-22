using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
	public class SliderCreateVM
	{
		public string Title { get; set; }

		public string Description { get; set; }

		[Required]
		public IFormFile Photo { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
