using Business.Services.Abstract.Admin;
using Business.Services.Abstract.User;
using Business.Services.Concret.Admin;
using Business.Services.Concret.Users;
using Business.ViewModels.User.Product;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class ShopController : Controller
	{

        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _shopService.IndexGetAllAsync();
            return View(model);

        }




    }
}
