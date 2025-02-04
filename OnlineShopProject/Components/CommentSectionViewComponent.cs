using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Services.Repository;

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
			return View(comments);
		}
	}
}
