using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
	{
		private readonly AppDbContext _context;

		public ProductCategoryRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<List<ProductCategory>> GetAll()
		{
			return await _context.ProductCategories.Where(x => !x.IsDeleted).ToListAsync();
		}
		public async Task<List<ProductCategory>> GetAllWithChildren()
		{
			return await _context.ProductCategories.Include(x=> x.Children).Where(x => !x.IsDeleted && x.ParentCategoryId == null).ToListAsync();
		}


		public async Task<ProductCategory>? GetById(int id)
		{
			return await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
		}
		public async Task<ProductCategory>? GetById_IncludeChildren(int id)
		{
			var children =await _context.ProductCategories.Where(x => x.ParentCategoryId == id).ToListAsync() ;
			var category = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
			 category.Children = children ; 
			return category ;
		}

		public async Task<ProductCategory> GetByNameAsync(string name)
        {
            return await _context.ProductCategories.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.CategoryName.ToLower().Trim() == name.ToLower().Trim());
        }

		public async Task<List<ProductCategory>> GetProductsWithPhotos()
		{
			return await _context.ProductCategories.Where(x => !x.IsDeleted).ToListAsync();
		}

	}
}
