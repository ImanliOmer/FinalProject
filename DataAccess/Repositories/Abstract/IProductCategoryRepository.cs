using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IProductCategoryRepository : IRepository<ProductCategory>
	{
		Task<ProductCategory> GetById(int id);
		Task<List<ProductCategory>> GetAll();
        Task<ProductCategory> GetByNameAsync(string name);
		Task<List<ProductCategory>> GetProductsWithPhotos();
		Task<List<ProductCategory>> GetAllWithChildren();
		Task<ProductCategory> GetById_IncludeChildren(int id);
    }
}
