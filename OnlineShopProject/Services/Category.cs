using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set;} = new List<Product>();

	}
}
