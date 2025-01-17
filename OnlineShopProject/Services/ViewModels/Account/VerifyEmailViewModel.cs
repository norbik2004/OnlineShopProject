using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Services.ViewModels.Account
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
