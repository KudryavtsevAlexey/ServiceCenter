using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel lvm)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(lvm.Email);

				if (user != null)
				{
					var result = await _signInManager.PasswordSignInAsync(user, lvm.Password, false, false);

					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}

			return View(lvm);
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel rvm)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = rvm.FirstName + rvm.LastName,
					FirstName = rvm.FirstName,
					LastName = rvm.LastName,
					Email = rvm.Email,
				};

				var identityResult = await _userManager.CreateAsync(user, rvm.Password);

				if (identityResult.Succeeded)
				{
					await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Client"));
					var signInResult = await _signInManager.PasswordSignInAsync(user, rvm.Password, true, false);

					if (signInResult.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}

			return View(rvm);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

	}
}
