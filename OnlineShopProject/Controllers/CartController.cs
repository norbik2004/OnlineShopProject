using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Controllers
{
    public class CartController : Controller
    {
        public IActionResult ViewCart()
        {
            return View();
        }
    }
}
