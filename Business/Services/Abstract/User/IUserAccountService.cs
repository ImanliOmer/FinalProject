using Business.ViewModels.User.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
	public interface IUserAccountService
	{
		Task<bool> Login(AccountLoginVM model);
		Task<bool> Logout();
		Task<bool> Register(AccountRegisterVM model);
		Task<bool> ConfirmEmail(string token, string email);
		Task<bool> ForgotPassword(AccountForgotPasswordVM model);
		AccountResetPasswordVM ResetPassword(string token, string email);
		Task<bool> ResetPassword(AccountResetPasswordVM model);
	}
}
