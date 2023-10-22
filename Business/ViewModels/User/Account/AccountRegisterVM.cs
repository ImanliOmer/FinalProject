using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Account
{
	public class AccountRegisterVM
	{
		[Required(ErrorMessage = "Enter Username")]
		[MaxLength(50, ErrorMessage = "Username can't be longer than 50 characters")]
		public string Username { get; set; }

		[EmailAddress(ErrorMessage = "Wrong email format")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirmation is required")]
		[Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
		public string ConfirmPassword { get; set; }
	}
}
