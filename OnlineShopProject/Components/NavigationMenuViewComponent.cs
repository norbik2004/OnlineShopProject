using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;

namespace OnlineShopProject.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IShopRepository shopRepository;

        public NavigationMenuViewComponent(IShopRepository repo)
        {
            this.shopRepository = repo;
        }

        public IViewComponentResult Invoke(string category)
        {
            var categories = this.shopRepository.GetProducts
                .Select(x => x.Category.CategoryName)
                .Distinct()
                .OrderBy(x => x)
            .ToList();

            ViewData["SelectedCategory"] = category;

            return View(categories);
        }
    }
}
