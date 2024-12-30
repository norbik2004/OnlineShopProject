using Microsoft.AspNetCore.Identity;

namespace OnlineShopProject.Services
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
