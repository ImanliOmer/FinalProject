using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Account
{
	public class AccountLoginVM
	{

		[Required]
		public string Username { get; set; }

		[Required]
        public string Email { get; set; }

        [Required]
		public string Password { get; set; }

		public string? ReturnUrl { get; set; }
	}
}
