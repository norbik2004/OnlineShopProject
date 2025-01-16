using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Components
{
    public class NavigationMenuAdminViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
