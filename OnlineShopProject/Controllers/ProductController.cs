using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services.Repository;

namespace OnlineShopProject.Controllers
{
	public class ProductController : Controller
	{
		private readonly IShopRepository shopRepository;

        public ProductController(IShopRepository repository)
        {
            this.shopRepository = repository;
        }

		public IActionResult ShowProduct(int productId)
		{
			try
			{
				var product = this.shopRepository.ShowProductById(productId);

				if (!ModelState.IsValid)
				{
					return View(product);
				}

				return View(product);
			}
			catch (KeyNotFoundException ex)
			{
				return RedirectToAction("Error", "Home", new { message = ex.Message});
			}
		}
	}
}
