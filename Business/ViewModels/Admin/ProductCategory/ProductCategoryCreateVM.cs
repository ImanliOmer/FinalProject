using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.ProductCategory
{
    public class ProductCategoryCreateVM
    {
        [Required]
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }

		public IFormFile? Photo { get; set; }

		public List<Common.Entities.ProductCategory>? Children { get; set; }


    }
}
