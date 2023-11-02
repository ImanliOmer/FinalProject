using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.NavigationCardVM;
using Business.ViewModels.Admin.Slider;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NavigationCardController : Controller
    {
        private readonly INavigationCardService _navigationCard;

        public NavigationCardController(INavigationCardService navigationCard)
        {
            _navigationCard = navigationCard;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var model = await _navigationCard.GetAllAsync();
            return View(model);

        }
			[HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(NavigationCardCreateVM model)
        {
            var isSucceeded = await _navigationCard.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(List));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _navigationCard.UpdateAsync(id);
            if (model is null) return NotFound();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(NavigationCardUpdateVM model, int id)
        {
            var isSucceeded = await _navigationCard.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(List));

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _navigationCard.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(List));

            return NotFound("NavigationCard not found");
        }
    }
}
