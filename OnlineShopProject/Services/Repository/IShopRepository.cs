using Microsoft.AspNetCore.Identity;

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
    }
}
