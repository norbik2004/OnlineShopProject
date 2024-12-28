namespace OnlineShopProject.Services.Repository
{
	public interface IShopRepository
	{
		IQueryable<Product> GetProducts { get; }

		Product ShowProductById(int productId);

	}
}
