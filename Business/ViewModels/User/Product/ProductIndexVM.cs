using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Product
{
    public class ProductIndexVM
    {
        public List<Common.Entities.Product>? Products { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; }

    }
}
