using Business.Services.Abstract.User;
using Business.ViewModels.Admin.Slider;
using Business.ViewModels.User.Basket;
using Business.ViewModels.User.Product;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concret.Users
{
    public class ShopService : IShopService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ShopService(IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<List<ProductCategory>> GetCategoryAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            return categories;
        }

        public async Task<List<Product>> GetProductsAsync(ProductCategory productCategory)
        {
            var products = await _productRepository.GetAllAsync();
            foreach (var product in products)
            {
                if (productCategory.Children is not null && product.CategoryId == productCategory.ParentCategoryId)
                {

                    return products;
                }
            }
            return products;
        }


        public async Task<ProductIndexVM> IndexGetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productCategories = await _productCategoryRepository.GetAllAsync();


            var model = new ProductIndexVM
            {
                Products = products,
                ProductCategories = productCategories
            };

            return model;
        }
    }
}
