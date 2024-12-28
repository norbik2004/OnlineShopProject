
namespace OnlineShopProject.Services.Repository
{
	public class ShopRepository : IShopRepository
	{
		public OnlineShopDbContext ShopContext { get; set; }
		public ShopRepository(OnlineShopDbContext context) 
		{
			this.ShopContext = context;
		}
		public IQueryable<Product> Products => this.ShopContext.Products;
	}
}
