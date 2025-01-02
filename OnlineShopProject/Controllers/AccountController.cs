using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
