using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;

namespace OnlineShopProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShopRepository shopRepository;

        public HomeController(IShopRepository repo)
        {
            this.shopRepository = repo;
        }


        public ViewResult Index()
        {
            return View(this.shopRepository.GetProducts);
        }

        public IActionResult Error(string message)
        {
            message = message ?? "Unknown error";

            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = message;
                return View();
            }

            ViewData["ErrorMessage"] = message;
            return View();
        }


    }
}
