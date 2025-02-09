namespace OnlineShopProject.Services.ViewModels.Product
{
    public class CommentViewModel
    {
        public long ProductId { get; set; }
        public long CommentId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Text { get; set; } = string.Empty;

        public string PhotoURL { get; set; } = string.Empty;

    }
}
