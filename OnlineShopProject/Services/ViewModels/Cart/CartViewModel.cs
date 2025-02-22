namespace OnlineShopProject.Services.ViewModels
{
    public class CartViewModel
    {
        public Cart? Cart { get; set; } = new();

        public string ReturnUrl { get; set; } = "/";
    }
}
