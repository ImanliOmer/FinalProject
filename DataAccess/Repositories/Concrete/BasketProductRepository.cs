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
    public class BasketProductRepository : Repository<BasketProduct>, IBasketProductRepository
    {
        private readonly AppDbContext _context;

        public BasketProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BasketProduct> GetBasketProductByIdAsync(int id, Basket basket)
        {
            var basketProduct = await _context.BasketProducts.Where(bp => !bp.IsDeleted).FirstOrDefaultAsync(bp => bp.Id == id && bp.BasketId == basket.Id);
            return basketProduct;
        }
    }
}
