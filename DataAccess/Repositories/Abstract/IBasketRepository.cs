using Common.Constants;
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
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket> GetBasketWithProductsAsync(IdentityUser user);
        Task<Basket> GetBasketById(IdentityUser user);
        Task<BasketProduct>? GetProductByBasketProductIdAsync(int id, IdentityUser user);
    }
}
