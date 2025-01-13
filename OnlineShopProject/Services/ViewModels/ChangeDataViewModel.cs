using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Services.ViewModels
{
	public class ChangeDataViewModel
	{
		[DataType(DataType.Text)]
		[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name can only contain letters and spaces.")]
		public string FullName { get; set; } = string.Empty;

		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Phone number must be in the format xxx-xxx-xxx with exactly 9 digits.")]
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
