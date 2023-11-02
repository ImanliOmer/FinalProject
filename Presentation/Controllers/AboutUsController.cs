using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
