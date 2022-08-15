﻿using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.ViewModels
{
	public class LoginViewModel
	{
		public string UserName { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }

		
	}
}
