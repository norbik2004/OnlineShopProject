using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;
using OnlineShopProject.Services.ViewModels.Product;

namespace OnlineShopProject.Controllers
{
	public class ProductController : Controller
	{
		private readonly IShopRepository shopRepository;
        private readonly UserManager<Users> userManager;

        public ProductController(IShopRepository repository, UserManager<Users> userManager)
        {
            this.shopRepository = repository;
			this.userManager = userManager;
        }

		public IActionResult ShowProduct(long productId)
		{
            ViewData["ProductId"] = productId;
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

		[HttpPost]
		public async Task<IActionResult> CommentPublication(CommentViewModel viewModel, long productId)
		{
			var user = await this.userManager.GetUserAsync(User);

			viewModel.UserEmail = user.Email;
			viewModel.ProductId = productId;
			viewModel.PublicationDate = DateTime.Now;

            Comments newComment = new()
			{
                ProductId = viewModel.ProductId,
                UserId = user.Id,
                Text = viewModel.Text,
                Rating = viewModel.Rating,
                PublicationDate = viewModel.PublicationDate
            };

            try
            {
                await this.shopRepository.SaveComment(newComment);
                return RedirectToAction("ShowProduct", "Product", new { productId = viewModel.ProductId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"There was an error saving your comment. Please try again later. {ex.Message}";
                return RedirectToAction("ShowProduct", "Product", new { productId = viewModel.ProductId });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(long commentId, long productId)
        {
            await this.shopRepository.DeleteComment(commentId);
            return RedirectToAction("ShowProduct", new {productId});
        }
    }
}
