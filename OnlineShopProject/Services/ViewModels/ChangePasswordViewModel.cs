using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Services.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "The {0} must be at {2} and at max {1} characters")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password are not the same")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
