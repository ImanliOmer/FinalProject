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
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        Task<Wishlist> GetWishlistWithProductsAsync(IdentityUser user);
        Task<Wishlist> GetWishlistById(IdentityUser user);
    }
}
