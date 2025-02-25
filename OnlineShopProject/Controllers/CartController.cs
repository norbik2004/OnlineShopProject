using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
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
        public IActionResult ViewCart(string returnUrl)
        {
            return View(new CartViewModel
            {
                ReturnUrl = returnUrl ?? "/",
                Cart = this.Cart
            });
        }

        [HttpPost]
        public IActionResult AddToCart(long productId, string returnUrl)
        {
            Product? product = this.shopRepository.ShowProductById(productId);

            if (product != null)
            {
                this.Cart.AddItem(product, 1);

                return View(new CartViewModel
                {
                    Cart = this.Cart,
                    ReturnUrl = returnUrl,
                });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
