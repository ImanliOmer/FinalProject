using Business.Services.Abstract.User;
using Business.ViewModels.User.Basket;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _basketService.IndexGetBasket(User));
        }

        [HttpGet]
        public async Task<IActionResult> AddAsync(BasketVM model)
        {
            if (!User.IsInRole(UserRoles.User.ToString()))
            {
                
                return RedirectToAction(nameof(Index), "Account");
            }
            var isSucceded = await _basketService.AddAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Increase(BasketVM model)
        {
            var isSucceded = await _basketService.IncreaseAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Decrease(BasketVM model)
        {
            var isSucceded = await _basketService.DecreaseAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(BasketVM model)
        {
            var isSucceded = await _basketService.DeleteAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }
    }

}
