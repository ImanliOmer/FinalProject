using Common.Entities.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class Product : BaseEntity
	{
        public Product()
        {
			ProductPhots = new List<ProductPhot>();

		}
        [MaxLength(300)]
        public string Name { get; set; }

		[MaxLength(1000)]
		public string Description { get; set; }

		[MaxLength(1000)]
        public string TechnicalSpecifications { get; set; }

        public int CategoryId { get; set; }

		public ProductCategory Category { get; set; }
        public string? MainPhoto { get; set; }

        public bool IsMain { get; set; }

        public ICollection<Common.Entities.ProductPhot>? ProductPhots { get; set; }
        public decimal Cost { get; set; }

        public decimal DiscountPrice { get; set; }
    }
}
