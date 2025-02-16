using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services.Repository;

namespace OnlineShopProject.Controllers
{
    public class CartController : Controller
    {
        private readonly IShopRepository shopRepository;
        public CartController(IShopRepository repository) 
        { 
            this.shopRepository = repository;
        }
        public IActionResult ViewCart()
        {
            return View();
        }

        public IActionResult AddToCart(long productId)
        {
            return View();
        }
    }
}
