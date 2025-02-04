using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShopProject.Services
{
    public class Comments
    {
        [Required]
        [Key]
        public long CommentId { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public DateTime PublicationDate { get; set; }

        public int Rating { get; set; }

        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;

        public Product Product { get; set; }

        public Users User { get; set; }

    }
}
