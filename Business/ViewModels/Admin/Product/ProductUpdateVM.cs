using Common.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business.ViewModels.Admin.Product
{
	public class ProductUpdateVM
	{
		
		public string Name { get; set; }

		
		public decimal Cost { get; set; }

		public string Description { get; set; }

        public string? MainPhotoPath { get; set; }
        public IFormFile? MainPhoto { get; set; }

        [Display(Name = "Main Product")]
        public bool IsMain { get; set; }
		public string TechnicalSpecifications { get; set; }

        public decimal DiscountPrice { get; set; }

        public List<Common.Entities.ProductCategory>? Category { get; set; }

		public List<Common.Entities.ProductCategory>? ProductCategories { get; set; }

        public List<IFormFile>?  PhotoFiles { get; set; }

        public List<ProductPhot>? Photos { get; set; }

        public int CategoryId { get; set; }
	}
}
