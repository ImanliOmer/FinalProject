using Business.Services.Abstract.User;
using Business.ViewModels.User.Wishlist;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Presentation.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var x = await _wishlistService.IndexGetWishlist(User);
            return View(x);
        }

        [HttpGet]
        public async Task<IActionResult> AddAsync(WishlistVM model)
        {
            if (!User.IsInRole(UserRoles.User.ToString()))
            {

                return RedirectToAction(nameof(Index), "Account");
            }
            var isSucceded = await _wishlistService.AddAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction("Index", "NotFound", new { area = "" });

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(WishlistVM model)
        {
            var isSucceded = await _wishlistService.DeleteAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction("Index", "NotFound", new { area = "" });

            return RedirectToAction(nameof(Index));
        }


    }
}
