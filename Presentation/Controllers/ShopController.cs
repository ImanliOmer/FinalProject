using Business.Services.Abstract.Admin;
using Business.Services.Abstract.User;
using Business.Services.Concret.Admin;
using Business.Services.Concret.Users;
using Business.ViewModels.User.Product;
using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Migrations;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Crmf;

namespace Presentation.Controllers
{
	public class ShopController : Controller
	{

        private readonly IShopService _shopService;
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly AppDbContext _context;

        public ShopController(IShopService shopService, IProductService productService, IProductCategoryRepository categoryRepository , IProductRepository productRepository, AppDbContext context)
        {
            _shopService = shopService;
           _productService = productService;
           _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _context = context;


        }

        public async Task<IActionResult> Index(int? id)
        {
            var model = await _shopService.IndexGetAllAsync();
          if(id!=null)
            {
                model.Products = model.Products?.Where(x => x.CategoryId == id).ToList();
            }
            return View(model);

        }

        public async Task<IActionResult> Details(int id)
        {
            // Ürünü veritabanından çekmek veya başka bir kaynaktan almak için gerekli kodu buraya ekleyin.
            var productFromDatabase = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            var model = new ProductIndexVM
            {
                Products = new List<Common.Entities.Product> { productFromDatabase }
                // Diğer model özelliklerini de doldurabilirsiniz, eğer varsa.
            };

            if (model.Products == null)
            {
                // Eğer model.Products null ise, burada gerekli bir işlem yapabilirsiniz.
                // Örneğin, uygun bir hata mesajı gösterebilir veya kullanıcıyı başka bir sayfaya yönlendirebilirsiniz.
                return NotFound(); // 404 Not Found hatası döndürme örneği
            }

            return View(model);
        }
       






    }
}
