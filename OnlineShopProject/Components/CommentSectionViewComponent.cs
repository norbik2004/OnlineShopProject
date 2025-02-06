using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services.Repository;
using OnlineShopProject.Services.ViewModels.Product;

namespace OnlineShopProject.Components
{
    public class CommentSectionViewComponent : ViewComponent
	{

		private readonly IShopRepository repository;
		public CommentSectionViewComponent(IShopRepository repository)
		{
			this.repository = repository;
		}

        public IViewComponentResult Invoke(long productId)
        {
            var comments = this.repository.GetAllComments(productId);

            List<CommentViewModel> viewModel = comments.Select(comment => new CommentViewModel
            {
                ProductId = comment.ProductId,
                UserEmail = comment.User.Email,
                Text = comment.Text,
                Rating = comment.Rating,
                PublicationDate = comment.PublicationDate
            }).ToList();

            return View(viewModel);
        }

    }
}
