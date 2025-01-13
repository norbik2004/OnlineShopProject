using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;
using OnlineShopProject.Services.ViewModels;

namespace OnlineShopProject.Controllers
{
    public class AccountController : Controller
    {
		private readonly SignInManager<Users> signInManager;
		private readonly UserManager<Users> userManager;
		private readonly IShopRepository shopRepository;

		public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager, IShopRepository repo)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.shopRepository = repo;
		}

		public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Invalid Data");
					return View(model);
				}

			}
			return View(model);
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
                    var roleResult = await userManager.AddToRoleAsync(users, "User");
                    if (!roleResult.Succeeded)
					{
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", $"Role assignment failed: {error.Description}");
                        }

                        await userManager.DeleteAsync(users);

                        return View(model);
                    }

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

		[HttpPost]
		public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByNameAsync(model.Email);

				if (user != null)
				{
					return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
				}
				else
				{
					ModelState.AddModelError("", "Invalid Data");
					return View(model);
				}
			}
			return View(model);
		}

		public IActionResult ChangePassword(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				return RedirectToAction("VerifyEmail", "Account");
			}
			return View(new ChangePasswordViewModel { Email = username });
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByNameAsync(model.Email);

				if (user != null)
				{
					var result = await userManager.RemovePasswordAsync(user);
					if (result.Succeeded)
					{
						await userManager.AddPasswordAsync(user, model.NewPassword);
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
				else
				{
					ModelState.AddModelError("", "Email not found");
					return View(model);
				}
			}
			else
			{
				ModelState.AddModelError("", "Something went wrong");
				return View(model);
			}
		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public IActionResult ViewAccount()
		{
			string email = HttpContext.User.Identity?.Name ?? string.Empty;
			
			if (string.IsNullOrEmpty(email))
			{
				return Unauthorized("Please Log In");
			}

			var user = this.shopRepository.ShowUserByEmail(email);

			if (user == null)
			{
				return NotFound("User not found.");
			}

			AccountViewModel viewModel = new AccountViewModel
			{
				FullName = user.FullName,
				PhoneNumber = user.PhoneNumber,
				Email = user.Email,
				Street = user.Street,
				PostalCode = user.PostalCode,
				Country = user.Country,
				City = user.City,
				PhotoURL = user.PhotoPath,
			};


			return View(viewModel);

		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> ChangeData(ChangeDataViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await this.userManager.GetUserAsync(User);

			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}

			user.PhoneNumber = model.PhoneNumber;
			user.FullName = model.FullName;
			user.Street = model.Street;
			user.City = model.City;
			user.PostalCode = model.PostalCode;
			user.Country = model.Country;

			if (model.Photo != null)
			{
                var extension = Path.GetExtension(model.Photo.FileName)?.ToLower();

                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif")
                {
                    ModelState.AddModelError("Photo", "Only .jpg, .png, and .gif files are allowed.");
                    return View(model);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads"));

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await model.Photo.CopyToAsync(fileStream);
				}

				user.PhotoPath = "/uploads/" + fileName;
            }

			var result = await this.userManager.UpdateAsync(user);

			if (result.Succeeded)
			{
				return RedirectToAction("ViewAccount", "Account");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}

		[Authorize]
		public IActionResult ChangeData()
		{
			try
			{
				string email = HttpContext.User.Identity?.Name ?? string.Empty;
				var user = this.shopRepository.ShowUserByEmail(email);

				ChangeDataViewModel viewModel = new ChangeDataViewModel
				{
					FullName = user.FullName,
					PhoneNumber = user.PhoneNumber,
					Street = user.Street,
					City = user.City,
					PostalCode = user.PostalCode,
					Country = user.Country,
					PhotoPath = user.PhotoPath, 
				};

				return View(viewModel);
			}
			catch (KeyNotFoundException ex)
			{
				return RedirectToAction("Error", "Home", new { message = ex.Message });
			}
		}


		

	}
}
