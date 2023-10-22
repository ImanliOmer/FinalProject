using Business.Services.Abstract.User;
using Business.ViewModels.User.Wishlist;
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
    public class WishlistService : IWishlistService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataAccess.Repositories.Abstract.IProductRepository _productRepository;
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IWishlistProductRepository _wishlistProductRepository;

        public WishlistService(UserManager<User> userManager,
                               IUnitOfWork unitOfWork,
                               DataAccess.Repositories.Abstract.IProductRepository productRepository,
                               IWishlistRepository wishlistRepository,
                               IWishlistProductRepository wishlistProductRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _wishlistRepository = wishlistRepository;
            _wishlistProductRepository = wishlistProductRepository;
        }

        public async Task<List<WishlistVM>> IndexGetWishlist(ClaimsPrincipal user)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser is null)
            {
                return null;
            }

            var wishlist = await _wishlistRepository.GetWishlistWithProductsAsync(authUser);

            var model = new List<WishlistVM>();

            if (wishlist is null)
            {
                wishlist = new Wishlist();
            }

            if (wishlist.WishlistProducts is null)
            {
                return model;
            }

            foreach (var wishlistProduct in wishlist.WishlistProducts.Where(wp => !wp.IsDeleted && wp.IsInWishlist))
            {
                if (wishlistProduct is null)
                {
                    return model;
                }
                var wishlistItem = new WishlistVM
                {
                    Id = wishlistProduct.Id,
                    PhotoName = wishlistProduct.Product.MainPhoto,
                    Price = wishlistProduct.Product.Cost,
                    NewPrice = wishlistProduct.Product.DiscountPrice,
                    Title = wishlistProduct.Product.Name,
                    ProductId = wishlistProduct.ProductId,
                 
                };
                model.Add(wishlistItem);
            }

            return model;
        }
        public async Task<bool> AddAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser is null)
            {
                return false;
            }

            var wishlist = await _wishlistRepository.GetWishlistById(authUser);

            if (wishlist is null)
            {
                wishlist = new Wishlist
                {
                    UserId = authUser.Id,
                };
                await _wishlistRepository.CreateAsync(wishlist);
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return false;

            var wishlistProduct = await _wishlistProductRepository.GetProductByWishlistProductIdAsync(id, authUser);

            if (wishlistProduct is null)
            {
                wishlistProduct = new WishlistProduct
                {
                    Wishlist = wishlist,
                    ProductId = product.Id,
                    Count = 1,
                    IsInWishlist = true
                };

                await _wishlistProductRepository.CreateAsync(wishlistProduct);
            }

            else if (wishlistProduct.IsDeleted)
            {
                wishlistProduct.IsDeleted = false;
                wishlistProduct.Count = 1;
            }
            else
            {
                wishlistProduct.Count++;
                _wishlistProductRepository.Update(wishlistProduct);
            }

            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser is null)
            {
                return false;
            }

            var wishlist = await _wishlistRepository.GetWishlistById(authUser);
            if (wishlist is null) return false;

            var wishlistProduct = await _wishlistProductRepository.GetWishlistProductByIdAsync(id, wishlist);
            if (wishlistProduct is null) return false;

            var product = await _productRepository.GetByIdAsync(wishlistProduct.ProductId);
            if (product is null) return false;

            _wishlistProductRepository.SoftDelete(wishlistProduct);
            wishlistProduct.IsInWishlist = false;
            wishlistProduct.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return true;
        }

        //public async Task<bool> IsInWishlistAsync(ClaimsPrincipal user, int id)
        //{
        //    var authUser = await _userManager.GetUserAsync(user);
        //    if (authUser is null)
        //    {
        //        return false;
        //    }

        //    var wishlist = await _wishlistRepository.GetWishlistById(authUser);
        //    if (wishlist is null) return false;

        //    var wishlistProduct = await _wishlistProductRepository.IsInWishlistAsync(id, authUser);
        //    if (wishlistProduct is false) return false;
        //}
    }
}
