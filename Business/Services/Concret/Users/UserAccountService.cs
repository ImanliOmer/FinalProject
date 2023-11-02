using Business.Services.Abstract.User;
using Business.Utilities.EmailService.EmailSender.Abstract;
using Business.Utilities.EmailService;
using Business.ViewModels.User.Account;
using Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Business.Services.Concret.Users
{
	public class UserAccountService : IUserAccountService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;
		private readonly IActionContextAccessor _contextAccessor;
		private readonly IUrlHelperFactory _urlHelperFactory;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ModelStateDictionary _modelState;

		public UserAccountService(UserManager<IdentityUser> userManager,
								SignInManager<IdentityUser> signInManager,
								RoleManager<IdentityRole> roleManager,
								IEmailSender emailSender,
								IActionContextAccessor contextAccessor,
								IUrlHelperFactory urlHelperFactory,
								IHttpContextAccessor httpContextAccessor)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_emailSender = emailSender;
			_contextAccessor = contextAccessor;
			_urlHelperFactory = urlHelperFactory;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<bool> Register(AccountRegisterVM model)
		{
			if (!_modelState.IsValid) return false;

			var user = new Common.Entities.User
			{
				UserName = model.Username,
				Email = model.Email,
				
				PhoneNumber = model.PhoneNumber,
				
			};

			var password = isPassword(model.Password);
			

			if (!password)
			{
				_modelState.AddModelError("Password", "Password must contain Minimum eight characters, at least one letter, one number and one special character");
			
				return false;
			}

			if (model.Password != model.ConfirmPassword)
			{
				_modelState.AddModelError("ConfirmPassword", "Confirme Password is not same pasword ");
			}

			var result = await _userManager.CreateAsync(user, model.Password);
			
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
			
					_modelState.AddModelError(string.Empty, error.Description);

				return false;
			}

			var urlHelper = _urlHelperFactory.GetUrlHelper(_contextAccessor.ActionContext);
			
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			
			var confirmationLink = urlHelper.Action(nameof(ConfirmEmail), "account", new { token, email = user.Email }, _httpContextAccessor.HttpContext.Request.Scheme);

			var message = new Message(new string[] { user.Email }, "P331 Email Confirmation", confirmationLink);
			
			_emailSender.SendEmail(message);

			await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
			
			return true;
		}

		public async Task<bool> Login(AccountLoginVM model)
		{
			if (!_modelState.IsValid) return false;

			var user = await _userManager.FindByNameAsync(model.Username);
			
			if (user is null)
			{
				_modelState.AddModelError("Username", "Username or password is incorrect");
				
				return false;
			}
			if (!user.EmailConfirmed)
			{
				_modelState.AddModelError("Email", "Email address must be confirmed to Login");
				
				return false;
			}
			var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
			if (!result.Succeeded)
			{
				_modelState.AddModelError(string.Empty, "Username or password is incorrect");
			
				return false;
			}

			var urlHelper = _urlHelperFactory.GetUrlHelper(_contextAccessor.ActionContext);
			
			if (!string.IsNullOrEmpty(model.ReturnUrl) && urlHelper.IsLocalUrl(model.ReturnUrl))
				return false;

			return true;
		}

		public async Task<bool> Logout()
		{
			await _signInManager.SignOutAsync();
			
			return true;
		}

		public async Task<bool> ConfirmEmail(string token, string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user is null)

				return false;

			var result = await _userManager.ConfirmEmailAsync(user, token);

			if (!result.Succeeded) return false;

			return true;
		}

		public async Task<bool> ForgotPassword(AccountForgotPasswordVM model)
		{
			if (!_modelState.IsValid) return false;

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null) return false;
			
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			
			var urlHelper = _urlHelperFactory.GetUrlHelper(_contextAccessor.ActionContext);
			
			var resetLink = urlHelper.Action(nameof(ResetPassword), "account", new { token, email = user.Email }, _httpContextAccessor.HttpContext.Request.Scheme);
			
			var message = new Message(new string[] { user.Email }, "Reset password", resetLink);
			
			_emailSender.SendEmail(message);

			return true;
		}
		
		public AccountResetPasswordVM ResetPassword(string token, string email)
		{
			var model = new AccountResetPasswordVM
			{
				Email = email,
				Token = token
			};
			return model;
		}
		
		public async Task<bool> ResetPassword(AccountResetPasswordVM model)
		{
			if (!_modelState.IsValid) return false;

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null) return false;

			var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					_modelState.AddModelError(string.Empty, error.Description);
				}
				return false;
			}

			return true;
		}


		private static bool isPassword(string pswrd)
		{
			return Regex.IsMatch(pswrd, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
		}

	}
}
