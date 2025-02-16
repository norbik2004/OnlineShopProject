namespace OnlineShopProject.Services.ViewModels.Cart
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? TotalQuantity { get; set; }
    }
}
