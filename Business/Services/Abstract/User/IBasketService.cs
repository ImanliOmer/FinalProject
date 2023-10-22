using Business.ViewModels.User.Basket;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
    public interface IBasketService
    {
        Task<List<BasketVM>> IndexGetBasket(ClaimsPrincipal user);
        Task<bool> AddAsync(ClaimsPrincipal user, int id);
        Task<bool> IncreaseAsync(ClaimsPrincipal user, int id);
        Task<bool> DecreaseAsync(ClaimsPrincipal user, int id);
        Task<bool> DeleteAsync(ClaimsPrincipal user, int id);

    }
}
