using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;
using OnlineShopProject.Services.ViewModels;

namespace OnlineShopProject.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
    {
        public readonly IShopRepository shopRepository;
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public AdminController(IShopRepository repo, SignInManager<Users> signManager, UserManager<Users> userManag)
        {
            this.shopRepository = repo;
            this.signInManager = signManager;
            this.userManager = userManag;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Users(int page = 1)
        {
            if (ModelState.IsValid)
            {
                int pageSize = 5;
                var allUsers = this.shopRepository.GetAllUsers();

                if (allUsers == null || !allUsers.Any())
                {
                    return View();
                }

                var usersToShow = allUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewData["CurrentPage"] = page;
                ViewData["TotalPages"] = (int)Math.Ceiling((double)allUsers.Count() / pageSize);
                ViewData["TotalUsers"] = allUsers.Count();

                return View(usersToShow);
            }
            
            return View();
        }

        [HttpGet("Admin/UserDetails/{email}")]
        public IActionResult UserDetails(string email)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new KeyNotFoundException(nameof(email));
                }

                var user = this.shopRepository.ShowUserByEmail(email);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                AccountViewModel viewModel = new()
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

            return View();
		}

        [HttpPost("Admin/DeleteUser/{email}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email cannot be null or empty");
            }

            var user = await this.userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"User with email {email} not found");
            }

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"User with email {email} has been deleted successfully";
                return RedirectToAction("Users", "Admin");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Error", result);
        }


        public IActionResult UserChangeData(string email)
        {
            return View();
        }

    }
}
