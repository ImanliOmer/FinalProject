using Business.Services.Abstract.User;
using Business.ViewModels.User.Basket;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concret.Users
{
    public class BasketService : IBasketService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly IWishlistService _wishlistService;
        private readonly IWishlistProductRepository _wishlistProductRepository;

        public BasketService(UserManager<User> userManager,
                             IUnitOfWork unitOfWork,
                             IBasketRepository basketRepository,
                             IProductRepository productRepository,
                             IBasketProductRepository basketProductRepository,
                             IWishlistService wishlistService,
                             IWishlistProductRepository wishlistProductRepository)

        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _basketProductRepository = basketProductRepository;
            _wishlistService = wishlistService;
            _wishlistProductRepository = wishlistProductRepository;
        }

        public async Task<List<BasketVM>> IndexGetBasket(ClaimsPrincipal user)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return null;
            }

            var basket = await _basketRepository.GetBasketWithProductsAsync(authUser);

            var model = new List<BasketVM>();

            if (basket is null)
            {
                return null;
            }

            foreach (var basketProduct in basket.BasketProducts.Where(bp => !bp.IsDeleted))
            {
                var basketItem = new BasketVM
                {
                    Id = basketProduct.Id,
                    Count = basketProduct.Count,
                    PhotoName = basketProduct.Product.MainPhoto,
                    Title = basketProduct.Product.Name,
                    Price = basketProduct.Product.Cost,

                };
                model.Add(basketItem);
            }

            return model;
        }
        public async Task<bool> AddAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return false;
            }

            var basket = await _basketRepository.GetBasketById(authUser);

            if (basket is null)
            {
                basket = new Basket
                {
                    UserId = authUser.Id,
                };
                await _basketRepository.CreateAsync(basket);
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return false;

            var basketProduct = await _basketRepository.GetProductByBasketProductIdAsync(id, authUser);

            if (basketProduct is null)
            {
                basketProduct = new BasketProduct
                {
                    Basket = basket,
                    ProductId = product.Id,
                    Count = 1
                };
                await _basketProductRepository.CreateAsync(basketProduct);
            }
            else if (basketProduct.IsDeleted)
            {
                basketProduct.IsDeleted = false;
                basketProduct.Count = 1;
            }

            else
            {
                basketProduct.Count++;
                _basketProductRepository.Update(basketProduct);
            }

            var wishlistProducts = await _wishlistProductRepository.GetWishlistProductsByUser(authUser);
            var wishlistProduct = wishlistProducts.FirstOrDefault(wp => wp.ProductId == id);
            wishlistProduct.IsInWishlist = false;
            _wishlistService.DeleteAsync(user, wishlistProduct.Id);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> IncreaseAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return false;
            }

            var basket = await _basketRepository.GetBasketById(authUser);
            if (basket == null) return false;

            var basketProduct = await _basketProductRepository.GetBasketProductByIdAsync(id, basket);
            if (basketProduct == null)
            {
                return false;
            }
            var product = await _productRepository.GetByIdAsync(basketProduct.ProductId);
            if (product == null)
            {
                return false;
            }



            basketProduct.Count++;

            _basketProductRepository.Update(basketProduct);
            await _unitOfWork.CommitAsync();

            return true;

        }
        public async Task<bool> DecreaseAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return false;
            }

            var basket = await _basketRepository.GetBasketById(authUser);
            if (basket == null) return false;

            var basketProduct = await _basketProductRepository.GetBasketProductByIdAsync(id, basket);
            if (basketProduct == null)
            {
                return false;
            }

            if (basketProduct.Count == 0)
            {
                basketProduct.IsDeleted = true;
                return false;
            }

            basketProduct.Count--;

            _basketProductRepository.Update(basketProduct);
            await _unitOfWork.CommitAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return false;
            }

            var basket = await _basketRepository.GetBasketById(authUser);
            if (basket == null) return false;

            var basketProduct = await _basketProductRepository.GetBasketProductByIdAsync(id, basket);
            if (basketProduct == null)
            {
                return false;
            }

            var product = await _productRepository.GetByIdAsync(basketProduct.ProductId);
            if (product == null)
            {
                return false;
            }

            _basketProductRepository.SoftDelete(basketProduct);
            basketProduct.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return true;
        }


    }
}
