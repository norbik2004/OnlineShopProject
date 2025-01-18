using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Services.ViewModels.Admin
{
    public class AdminProductDetailsViewModel
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string IMGFileLink { get; set; } = string.Empty;
        public Category Category { get; set; }
    }
}
