using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public ViewResult Index(string? category, int page = 1)
        {
            ViewData["SelectedCategory"] = category;

            if (ModelState.IsValid)
            {
                int pageSize = 9;
                IQueryable<Product> products = this.shopRepository.GetProducts().Where(p => category == null || p.Category.CategoryName == category);

                IQueryable<Product> productsToShow = products.Skip((page - 1) * pageSize).Take(pageSize);

                ViewData["CurrentPage"] = page;
                ViewData["TotalPages"] = (int)Math.Ceiling((double)products.Count() / pageSize);
                ViewData["TotalProducts"] = products.Count();

                return View(productsToShow);
            }

            return View();
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
