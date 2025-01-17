using System.Security.Policy;

namespace OnlineShopProject.Services.ViewModels.Account
{
    public class AccountViewModel
    {
        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string PhotoURL { get; set; } = string.Empty;

    }
}
