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
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Comments> Comments => Set<Comments>();
    }
}
