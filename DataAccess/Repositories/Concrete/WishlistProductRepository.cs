using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class WishlistProductRepository : Repository<WishlistProduct>, IWishlistProductRepository
    {
        private readonly AppDbContext _context;

        public WishlistProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<WishlistProduct> GetWishlistProductByIdAsync(int id, Wishlist wishlist)
        {
            var wishlistProduct = await _context.WishlistProducts.Where(wp => !wp.IsDeleted).FirstOrDefaultAsync(wp => wp.Id == id && wp.WishlistId == wishlist.Id);
            return wishlistProduct;
        }

        public Task<WishlistProduct>? GetProductByWishlistProductIdAsync(int id, IdentityUser user)
        {
            var wishlistproduct = _context.WishlistProducts.Where(wp => !wp.IsDeleted).FirstOrDefaultAsync(wp => wp.ProductId == id && wp.Wishlist.UserId == user.Id);
            return wishlistproduct;
        }

        public async Task<List<WishlistProduct>> GetWishlistProductsByUser(IdentityUser user)
        {
            var wishlistproducts = await _context.WishlistProducts.Where(wp => !wp.IsDeleted && wp.Wishlist.UserId == user.Id).ToListAsync();

            return wishlistproducts;
        }

        public async Task<bool> IsInWishlistAsync(int id, IdentityUser user)
        {
            var wishlistproduct = await _context.WishlistProducts.Where(wp => wp.IsInWishlist).FirstOrDefaultAsync(wp => wp.ProductId == id && wp.Wishlist.UserId == user.Id);
            if (wishlistproduct is not null)
            {
                return true;
            }
            return false;
        }
    }
}
