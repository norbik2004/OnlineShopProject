using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services.Repository;

namespace OnlineShopProject.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
    {
        public readonly IShopRepository shopRepository;
        public AdminController(IShopRepository repo)
        {
            this.shopRepository = repo;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Users(int page = 1)
        {
            int pageSize = 3;
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

        public IActionResult UserDetails(int userId)
        {
            return View();
        }
    }
}
