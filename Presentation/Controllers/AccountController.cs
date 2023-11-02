using Business.Services.Abstract.User;
using Business.ViewModels.User.Account;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAccountService _accountService;
        private readonly Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary _modelState;


        public AccountController(IUserAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            var isSucceeded = await _accountService.Register(model);
            if (isSucceeded) return RedirectToAction(nameof(Login));

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceeded = await _accountService.Login(model);
            if (isSucceeded) return RedirectToAction(nameof(Index), "home");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var isSucceeded = await _accountService.ConfirmEmail(token, email);
            if (isSucceeded) return RedirectToAction(nameof(Login));

            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(AccountForgotPasswordVM model)
        {
            var isSucceeded = await _accountService.ForgotPassword(model);
            if (isSucceeded) return RedirectToAction(nameof(Login));
            
            return View("AccountErrorPage");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = _accountService.ResetPassword(token, email);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(AccountResetPasswordVM model)
        {
            var isSucceeded = await _accountService.ResetPassword(model);
            if (isSucceeded) return View("Success");

            return View("Error");
        }

    }
}
