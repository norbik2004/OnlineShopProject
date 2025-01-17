using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Services.ViewModels.Account
{
    public class ChangeDataViewModel
    {
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name can only contain letters and spaces.")]
        public string FullName { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Phone number must be in the format xxx-xxx-xxx with exactly 9 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street can only contain letters and spaces.")]
        public string Street { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]
        [DataType(DataType.Text)]
        public string City { get; set; } = string.Empty;

        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code can only contain letters and spaces, (xx-xxx) format.")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]
        [DataType(DataType.Text)]
        public string Country { get; set; } = string.Empty;

        [ImageValidation(ErrorMessage = "Only .jpg, .png, and .gif images are allowed.")]
        public IFormFile? Photo { get; set; }

        public string? PhotoPath { get; set; }
    }
}
