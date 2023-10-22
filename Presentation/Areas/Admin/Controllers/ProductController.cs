using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var model = await _productService.GetAllAsync();
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult>  Create()
		{
			var model = await _productService.Create();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(ProductCreateVM model)
		{
			var isSucceeded = await _productService.CreateAsync(model);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _productService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(ProductUpdateVM model, int id)
		{
			var isSucceeded = await _productService.UpdateAsync(model, id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _productService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return NotFound();
		}
	}
}
