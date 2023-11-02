using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Account;
using Business.ViewModels.User.Account;
using Common.Constants;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Business.Services.Concret.Admin
{
    public class AccountService : Business.Services.Abstract.Admin.IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ModelStateDictionary _modelState;

		public AccountService(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                IActionContextAccessor contextAccessor)
        {
            _modelState = contextAccessor.ActionContext.ModelState;
            _userManager = userManager;
            _signInManager = signInManager;
        }

       

        public async Task<bool> Login(AdminAccountLoginVM model)
        {
            if (!_modelState.IsValid) return false;

            var userName = await _userManager.FindByNameAsync(model.Username);
            if (userName is null)
            {
                _modelState.AddModelError(string.Empty, "Username or password is incorrect");
                return false;
            }

            

			var result = await _signInManager.PasswordSignInAsync(userName, model.Password, false, false);
            if (!result.Succeeded)
            {
                _modelState.AddModelError(string.Empty, "Username or password is incorrect");
                return false;
            }

            if (!await HasAccessToAdminPanelAsync(userName))
            {
                _modelState.AddModelError(string.Empty, "You don't have permission to admin panel");
                return false;
            }

            return true;
        }

        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        private async Task<bool> HasAccessToAdminPanelAsync(IdentityUser user)
        {
            if (await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()) ||
                await _userManager.IsInRoleAsync(user, UserRoles.SuperAdmin.ToString()) ||
                await _userManager.IsInRoleAsync(user, UserRoles.Manager.ToString()) ||
                await _userManager.IsInRoleAsync(user, UserRoles.HR.ToString()))
            {
                return true;
            }
            return false;
        }
    }
}
