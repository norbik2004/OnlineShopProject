using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Services
{
	public class Category
	{
		[Required]
		[Key]
		public int CategoryId { get; set; }

		[Required]
		public string CategoryName { get; set; } = string.Empty;

		[Required]
		public string CategoryDescription { get; set; }

		public virtual ICollection<Product> Products { get; set;} = new List<Product>();

	}
}
