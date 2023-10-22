using Business.ViewModels.Admin.Product;
using Business.ViewModels.Admin.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IProductCategoryService
    {
        Task<ProducCategorytListVM> GetAllAsync();
        Task<bool> CreateAsync(ProductCategoryCreateVM model);
        Task<ProductCategoryCreateVM> CreateCategoryCreateVMAsync();
		Task<bool> DeleteAsync(int id);
        Task<ProductCategoryUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(ProductCategoryUpdateVM model, int id);
    }
}
