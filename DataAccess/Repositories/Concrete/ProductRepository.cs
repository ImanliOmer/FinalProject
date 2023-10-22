using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly AppDbContext _context;

		public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

       

		public async Task<IQueryable<Product>> FilterProductsByTitle(string? title)
        {
			return !string.IsNullOrEmpty(title) ?
							   _context.Products.Include(p => p.Name).Where(p => p.Name.Contains(title) && !p.IsDeleted) :
							   _context.Products.Include(p => p.Name).Where(p => !p.IsDeleted);
		}

        public List<ProductCategory> GetAllCategories()
        {
			return _context.ProductCategories.Where(p => !p.IsDeleted).ToList();
		}

        public async Task<Product> GetByNameAsync(string name)
        {
			return await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Name == name);
            
		}

        public async Task<ProductCategory> GetCategoryAsync(int id)
        {
			return await _context.ProductCategories.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id);
		}

		
		public async Task<List<Product>> GetProductsWithPhotos()
		{
			return await _context.Products.Include(x=> x.Category).Include(x=> x.ProductPhots).Where(x=> !x.IsDeleted).ToListAsync();
		}

        public async Task<Product> GetProductsWithPhotos(int id)
        {
			return await _context.Products.Include(x => x.ProductPhots).Include(x => x.Category).Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
