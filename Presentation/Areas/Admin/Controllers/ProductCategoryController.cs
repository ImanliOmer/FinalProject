using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
        [Area("admin")]
    public class ProductCategoryController : Controller
    {

        private readonly IProductCategoryService _categoryService;

        public ProductCategoryController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var model = await _categoryService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult>  Create()
        {
            var model =await _categoryService.CreateCategoryCreateVMAsync();


			return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductCategoryCreateVM model)
        {
			var test =  await _categoryService.CreateCategoryCreateVMAsync();
            model.Children = test.Children;

			var isSucceeded = await _categoryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Create),model);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _categoryService.UpdateAsync(id);
            if (model is null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(ProductCategoryUpdateVM model, int id)
        {
            var isSucceeded = await _categoryService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(List));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _categoryService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(List));

            return NotFound();
        }
    }
}
