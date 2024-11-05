using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BL.Models
{
	public class UserAuthentication
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		[StringLength(24, MinimumLength = 8, ErrorMessage = "The \"Password\" must contain from 8 to 24 characters!")]
		public string Password { get; set; } = string.Empty;
	}
}
