using Microsoft.AspNetCore.Identity;
using OnlineShopProject.Services.ViewModels.Product;

namespace OnlineShopProject.Services.Repository
{
	public interface IShopRepository
	{
		IQueryable<Product> GetProducts();

		Product ShowProductById(long productId);

		Users ShowUserByEmail(string email);

		IEnumerable<Users> GetAllUsers();

		Task<bool> DeleteProductAsync(long productId);

		List<Category> GetAllCategories();

		Category GetCategoryById(int categoryId);

		void UpdateProduct(Product product);

		Task SaveChangesAsync();

		List<Comments> GetAllComments(long productId);

		Task SaveComment(Comments comment);

		double GetRatingOfComments(long productId);
	}
}
