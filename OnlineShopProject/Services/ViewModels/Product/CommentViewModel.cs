namespace OnlineShopProject.Services.ViewModels.Product
{
    public class CommentViewModel
    {
        public long ProductId { get; set; }
        public string UserEmail { get; set; }
        public int Rating { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Text { get; set; } = string.Empty;

    }
}
