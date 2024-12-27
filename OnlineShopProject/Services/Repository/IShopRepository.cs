namespace OnlineShopProject.Services.Repository
{
	public interface IShopRepository
	{
		IQueryable<Product> Products { get; }

	}
}
