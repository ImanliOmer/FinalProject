using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class ProductPhot:BaseEntity
	{
        public Product Product { get; set; }
        public int ProductId { get; set; }
		public string PhotoName { get; set; }
    }
}
