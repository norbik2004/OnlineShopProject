using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Register Razor Pages and MVC
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Registration of the DbContext Service
builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

// Registration of repository services
builder.Services.AddScoped<IShopRepository, ShopRepository>();

// Identity services
builder.Services.AddDbContext<OnlineShopIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"]));

builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<OnlineShopIdentityDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "error",
    pattern: "Error",
    defaults: new { Controller = "Home", action = "Error" });

app.MapControllerRoute(
    name: "productPage",
    pattern: "product/{productId:int}",
    defaults: new { Controller = "Product", action = "ShowProduct" });

app.MapControllerRoute(
    name: "category",
    pattern: "product/{category?}",
    defaults: new { Controller = "Home", action = "Index" });

app.MapFallbackToController("Error", "Home");

await app.RunAsync();
