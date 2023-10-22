using Business.ViewModels.Admin.Product;
using Business.ViewModels.User.Product;
using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
    public interface IShopService
    {
        Task<List<ProductCategory>> GetCategoryAsync();
        Task<List<Product>> GetProductsAsync(ProductCategory productCategory);

        Task<ProductIndexVM> IndexGetAllAsync();

    }
}
