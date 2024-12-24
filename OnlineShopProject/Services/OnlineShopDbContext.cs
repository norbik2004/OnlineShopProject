using Microsoft.EntityFrameworkCore;

namespace OnlineShopProject.Services
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext(DbContextOptions options) : base(options)
        {
             
        }
    }
}
