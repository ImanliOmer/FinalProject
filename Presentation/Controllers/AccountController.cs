using Business.Services.Abstract.User;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }
    }
}
