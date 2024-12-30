using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopProject.Services
{
    public class OnlineShopIdentityDbContext : IdentityDbContext<Users>
    {
        public OnlineShopIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
