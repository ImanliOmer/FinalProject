using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Account
{
	public class AccountForgotPasswordVM
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
