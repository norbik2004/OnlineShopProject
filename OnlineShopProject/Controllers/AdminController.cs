using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.AspNetCore.Rewrite;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;
using OnlineShopProject.Services.ViewModels.Account;
using OnlineShopProject.Services.ViewModels.Admin;

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

        [HttpPost]
        public async Task<IActionResult> UserChangeData(string email, AdminUserChangeDataViewModel model)
        {

            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            user.PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) ? "" : model.PhoneNumber;
            user.FullName = string.IsNullOrEmpty(model.FullName) ? "" : model.FullName;
            user.Street = string.IsNullOrEmpty(model.Street) ? "" : model.Street;
            user.City = string.IsNullOrEmpty(model.City) ? "" : model.City;
            user.PostalCode = string.IsNullOrEmpty(model.PostalCode) ? "" : model.PostalCode;
            user.Country = string.IsNullOrEmpty(model.Country) ? "" : model.Country;

            var result = await this.userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("UserDetails", "Admin", new { email = model.Email });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);

        }

        public IActionResult UserChangeData(string email)
        {
            var user = this.shopRepository.ShowUserByEmail(email);

            if (user == null)
            {
                return BadRequest("NO USER FOUND");
            }

            AdminUserChangeDataViewModel model = new AdminUserChangeDataViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Street = user.Street,
                City = user.City,
                PostalCode = user.PostalCode,
                Country = user.Country,
                FullName = user.FullName,
            };

            return View(model);
        }

        public IActionResult ViewProducts(int page = 1)
        {
			if (ModelState.IsValid)
			{
				int pageSize = 5;
                IQueryable<Product> products = this.shopRepository.GetProducts();

                IQueryable<Product> productsToShow = products.Skip((page - 1) * pageSize).Take(pageSize);

                ViewData["CurrentPage"] = page;
				ViewData["TotalPages"] = (int)Math.Ceiling((double)products.Count() / pageSize);
				ViewData["TotalProducts"] = products.Count();

				return View(productsToShow);
			}

			return View();
		}

        [HttpGet("Admin/ProductDetails/{productId}")]
        public async Task<IActionResult> ViewProductDetails(int productId)
        {
            Product product = this.shopRepository.ShowProductById(productId);

            AdminProductDetailsViewModel viewModel = new()
            {
                ProductId = product.ProductId,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                CategoryId = product.CategoryId,
                IMGFileLink = product.IMGFileLink,
                Name = product.Name,
            };

            return View(viewModel);
        }

        [HttpPost("{productId}")]
        [ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            try
            {
                var result = await this.shopRepository.DeleteProductAsync(productId);

                if (result)
                {
                    return RedirectToAction("ViewProducts", "Admin");
                }

                return BadRequest("Product could not be deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
		public IActionResult ProductChangeDetails(long productId)
        {
            Product product = this.shopRepository.ShowProductById(productId);
            List<Category> categories = this.shopRepository.GetAllCategories();

			AdminProductChangeDataViewModel
                viewModel = new()
			{
				ProductId = product.ProductId,
				Price = product.Price,
				Description = product.Description,
				Category = product.Category,
				CategoryId = product.CategoryId,
				IMGFileLink = product.IMGFileLink,
				Name = product.Name,
                Categories = categories,
			};

            return View(viewModel);
		}

        [HttpPost]
        public async Task<IActionResult> ProductChangeDetails(long productId, AdminProductChangeDataViewModel model)
        {
            var product = this.shopRepository.ShowProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            Category modelCategory = this.shopRepository.GetCategoryById(model.CategoryId);

            model.Category = modelCategory;
            model.Categories = this.shopRepository.GetAllCategories();

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Category = model.Category;
            product.CategoryId = model.CategoryId;
            
            this.shopRepository.UpdateProduct(product);
            await this.shopRepository.SaveChangesAsync();

            return RedirectToAction("ViewProductDetails", "Admin", new { productId = model.ProductId });
        }

	}
}
