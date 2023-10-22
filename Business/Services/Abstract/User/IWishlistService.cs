using Business.ViewModels.User.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
    public interface IWishlistService
    {
        Task<List<WishlistVM>> IndexGetWishlist(ClaimsPrincipal user);
        Task<bool> AddAsync(ClaimsPrincipal user, int id);
        Task<bool> DeleteAsync(ClaimsPrincipal user, int id);
    }
}
