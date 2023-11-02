using Common.Entities;
using DataAccess.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IWishlistProductRepository : IRepository<WishlistProduct>
    {
        Task<WishlistProduct>? GetProductByWishlistProductIdAsync(int id, IdentityUser user);
        Task<WishlistProduct> GetWishlistProductByIdAsync(int id, Wishlist wishlist);
        Task<List<WishlistProduct>> GetWishlistProductsByUser(IdentityUser user);
        Task<bool> IsInWishlistAsync(int id, IdentityUser user);
    }
}
