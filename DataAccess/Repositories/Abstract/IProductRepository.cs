using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IProductRepository : IRepository<Product>
	{
		List<ProductCategory> GetAllCategories();
		Task<Product> GetByNameAsync(string name);
		Task<ProductCategory> GetCategoryAsync(int id);
        Task<Product> GetByIdCustom(int id);

        Task<List<Product>> GetProductsForTheirId(int id);
		Task<List<Product>> GetProductsWithPhotos();
		Task<IQueryable<Product>> FilterProductsByTitle(string? title);
		Task<Product> GetProductsWithPhotos(int id);
		

    }
}
