using Business.ViewModels.Admin.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
	public interface IProductService
	{
		Task<ProductListVM> GetAllAsync();
		Task <ProductCreateVM> Create();
		Task<bool> CreateAsync(ProductCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<ProductUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(ProductUpdateVM model, int id);
	}
}
