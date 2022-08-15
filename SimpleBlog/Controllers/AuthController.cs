using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Services.Email;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Controllers
{
	public class AuthController : Controller
	{
		private SignInManager<IdentityUser> _signInManager;
		private UserManager<IdentityUser> _userManager;
		private IEmailService _emailService;
		public AuthController(SignInManager<IdentityUser> signInManager, 
			UserManager<IdentityUser> userManager,
			IEmailService emailService)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_emailService = emailService;
			
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View(new LoginViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{

			var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, loginVM.RememberMe, false);

			if (!result.Succeeded)
			{
				return View(loginVM);
			}


			var user = await _userManager.FindByNameAsync(loginVM.UserName);
			bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");


			if (isAdmin)
			{
				return RedirectToAction("Index", "Panel");
			}

			return RedirectToAction("Index", "Home");


		}



		[HttpGet]
		public IActionResult Register()
		{
			return View(new RegisterViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerVM)
		{

			if (!ModelState.IsValid)
			{
				return View(registerVM);
			}

			var user = new IdentityUser
			{
				UserName = registerVM.Email,
				Email = registerVM.Email
			};
			var result = await _userManager.CreateAsync(user, registerVM.Password);

			if (result.Succeeded)
			{
				
				await _signInManager.SignInAsync(user, isPersistent: registerVM.RememberMe);
				//await _emailService.SendEmail(to: user.Email, subject: "Welcome", message: "Message");
				return RedirectToAction("Index", "Home");
			}

			return View();

		}


		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}



	}
}
