using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IWishlistProductRepository : IRepository<WishlistProduct>
    {
        Task<WishlistProduct>? GetProductByWishlistProductIdAsync(int id, User user);
        Task<WishlistProduct> GetWishlistProductByIdAsync(int id, Wishlist wishlist);
        Task<List<WishlistProduct>> GetWishlistProductsByUser(User user);
        Task<bool> IsInWishlistAsync(int id, User user);
    }
}
