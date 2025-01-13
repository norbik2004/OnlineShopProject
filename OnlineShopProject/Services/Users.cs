using Microsoft.AspNetCore.Identity;

namespace OnlineShopProject.Services
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string PhotoPath {  get; set; } = string.Empty;
	}
}
