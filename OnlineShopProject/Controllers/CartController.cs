using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;
using OnlineShopProject.Services.ViewModels;

namespace OnlineShopProject.Controllers
{
    public class CartController : Controller
    {
        private readonly IShopRepository shopRepository;
        public CartController(IShopRepository repository, Cart cart) 
        { 
            this.shopRepository = repository;
            this.Cart = cart;
        }

        public Cart Cart { get; set; }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                ReturnUrl = returnUrl ?? "/",
                Cart = this.Cart
            });
        }

        public IActionResult AddToCart(long productId)
        {
            return View();
        }
    }
}
