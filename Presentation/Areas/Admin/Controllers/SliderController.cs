using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SliderController : Controller
	{
		private readonly ISliderService _sliderService;

		public SliderController(ISliderService sliderService)
		{
			_sliderService = sliderService;
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var model = await _sliderService.GelAllASync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateAsync(SliderCreateVM model)
		{
			var isSucceeded = await _sliderService.CreateAsync(model);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _sliderService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateAsync(SliderUpdateVM model, int id)
		{
			var isSucceeded = await _sliderService.UpdateAsync(model, id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _sliderService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return NotFound("Slider not found");
		}
	}
}
