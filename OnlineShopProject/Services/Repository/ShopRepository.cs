
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopProject.Services.Repository
{
	public class ShopRepository : IShopRepository
	{
		public OnlineShopDbContext ShopContext { get; set; }
		public OnlineShopIdentityDbContext IdentityContext { get; set; }

		public ShopRepository(OnlineShopDbContext context, OnlineShopIdentityDbContext idContext) 
		{
			this.ShopContext = context;
			this.IdentityContext = idContext;
		}
		public IQueryable<Product> GetProducts => this.ShopContext.Products.Include(p => p.Category)
                    .ThenInclude(p => p.Products);

        public Product ShowProductById(int productId)
        {
			if (productId < 0)
			{
				throw new KeyNotFoundException($"Product with ID {productId} not found.");
			}

			var product = this.ShopContext.Products
                .Include(p => p.Category)
					.ThenInclude(p => p.Products)
                .FirstOrDefault(p => p.ProductId == productId);

			if (product == null)
			{
				throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

			return product;
        }

		public Users ShowUserByEmail(string email)
		{
			var user = this.IdentityContext.Users.FirstOrDefault(p => p.Email == email);

			if (user == null)
			{
				throw new KeyNotFoundException("User not found");
			}

			return user;

		}

		public IEnumerable<Users> GetAllUsers()
		{
			IEnumerable<Users> users = this.IdentityContext.Users;

			if (users == null || !users.Any())
			{
				throw new KeyNotFoundException("Users not found");
			}

			return users;
		}

    }
}
