using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services;
using OnlineShopProject.Services.ViewModels;

namespace OnlineShopProject.Controllers
{
    public class AccountController : Controller
    {
		private readonly SignInManager<Users> signInManager;
		private readonly UserManager<Users> userManager;

		public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		public IActionResult Login()
        {
            return View();
        }
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid) {
				Users users = new Users
				{
					FullName = model.Name,
					Email = model.Email,
					UserName = model.Email,
				};

				var result = await userManager.CreateAsync(users, model.Password);

				if (result.Succeeded)
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}

					return View(model);
				}
			}

			return View(model);
		}
		public IActionResult VerifyEmail()
		{
			return View();
		}

		public IActionResult ChangePassword()
		{
			return View();
		}
	}
}
