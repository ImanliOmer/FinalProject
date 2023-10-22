using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class ProductCategory: BaseEntity
	{
		[MaxLength(50)]
        public string? CategoryName { get; set; }

        public int? ParentCategoryId { get; set; }

		public List<ProductCategory>? Children { get; set; }

		public string? Photo { get; set; }

		public ICollection<Product>? Products { get; set; }


		public ProductCategory? ParentCategory { get; set; }
    }
}
