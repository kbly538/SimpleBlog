﻿using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.ViewModels
{
	public class RegisterViewModel
	{

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }


		public bool RememberMe { get; set; }


		
	}
}
