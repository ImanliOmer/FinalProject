using Common.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace Business.ViewModels.Admin.Product
{
	public class ProductCreateVM
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public decimal Cost { get; set; }


        public decimal DiscountPrice { get; set; }

        [Required]
		public string Description { get; set; }


		[Required]
		public string TechnicalSpecifications { get; set; }

        public IFormFile MainPhoto { get; set; }

        [Required]
		public List<IFormFile> Photos { get; set; }
		public int CategoryId { get; set; }
		public List<Common.Entities.ProductCategory>? Category { get; set; }

		public List<Common.Entities.ProductCategory>? ProductCategories { get; set; }

        [Display(Name = "Main Product")]
        public bool IsMain { get; set; }


    }
}
