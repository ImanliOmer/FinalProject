using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.ProductCategory
{
    public class ProducCategorytListVM
    {
        public ProducCategorytListVM()
        {
            ProductCategories = new List<Common.Entities.ProductCategory>();
        }

        public List<Common.Entities.ProductCategory> ProductCategories { get; set; }
    }

}
