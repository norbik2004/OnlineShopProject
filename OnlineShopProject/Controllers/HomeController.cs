using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services.Repository;

namespace OnlineShopProject.Controllers
{
    public class HomeController : Controller
    {
        private IShopRepository shopRepository;

        public HomeController(IShopRepository repo)
        {
            this.shopRepository = repo;
        }


        public ViewResult Index()
        {
            return View(this.shopRepository.Products);
        }
    }
}
