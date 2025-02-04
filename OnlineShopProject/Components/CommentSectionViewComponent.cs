using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Components
{
    public class CommentSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
