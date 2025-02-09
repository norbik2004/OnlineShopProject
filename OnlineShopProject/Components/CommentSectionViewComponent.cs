using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services;
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

        public IViewComponentResult Invoke(long productId, int page = 1)
        {
            page = Convert.ToInt32(HttpContext.Request.Query["page"]);
            double avgRating = this.repository.GetRatingOfComments(productId);

            ViewData["avgRating"] = avgRating;

            page = page == 0 ? 1 : page;
            int pageSize = 5;
            var comments = this.repository.GetAllComments(productId);

            var commentsToShow = comments.Skip((page - 1) * pageSize).Take(pageSize);

            List<CommentViewModel> viewModel = commentsToShow.Select(commentsToShow => new CommentViewModel
            {
                ProductId = commentsToShow.ProductId,
                CommentId = commentsToShow.CommentId,
                UserEmail = commentsToShow.User.Email,
                Text = commentsToShow.Text,
                Rating = commentsToShow.Rating,
                PublicationDate = commentsToShow.PublicationDate,
                PhotoURL = commentsToShow.User.PhotoPath,
            }).ToList();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)comments.Count / pageSize);
            ViewData["TotalProducts"] = comments.Count;

            return View(viewModel);
        }

    }
}
