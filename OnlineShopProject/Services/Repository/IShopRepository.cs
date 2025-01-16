using Microsoft.AspNetCore.Identity;

namespace OnlineShopProject.Services.Repository
{
	public interface IShopRepository
	{
		IQueryable<Product> GetProducts { get; }

		Product ShowProductById(int productId);

		Users ShowUserByEmail(string email);

		IEnumerable<Users> GetAllUsers();

    }
}
