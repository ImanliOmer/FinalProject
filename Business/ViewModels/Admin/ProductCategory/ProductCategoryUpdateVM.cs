using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.ProductCategory
{
    public class ProductCategoryUpdateVM
    {
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
		public List<Common.Entities.ProductCategory>? Children { get; set; }

		[Display(Name = "New Photo")]
		public IFormFile? NewPhoto { get; set; }
		public string? Photo { get; set; }
	}
}
