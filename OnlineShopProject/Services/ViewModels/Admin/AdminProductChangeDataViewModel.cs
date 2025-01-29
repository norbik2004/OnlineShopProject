namespace OnlineShopProject.Services.ViewModels.Admin
{
	public class AdminProductChangeDataViewModel
	{
		public long ProductId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public string IMGFileLink { get; set; } = string.Empty;
		public IFormFile? Photo { get; set; }
		public Category Category { get; set; }
		public List<Category> Categories { get; set; }
	}
}
