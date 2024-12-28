
using Microsoft.EntityFrameworkCore;

namespace OnlineShopProject.Services.Repository
{
	public class ShopRepository : IShopRepository
	{
		public OnlineShopDbContext ShopContext { get; set; }
		public ShopRepository(OnlineShopDbContext context) 
		{
			this.ShopContext = context;
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
    }
}
