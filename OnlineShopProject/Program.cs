using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Registration of the DbContext Service
builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

// Registration of repository services

builder.Services.AddScoped<IShopRepository, ShopRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


// routes
app.MapControllerRoute(
	name: "default",
	pattern: "/",
	defaults: new { Controller = "Home", action = "Index" });

app.MapControllerRoute(
    "error",
    "Error",
    defaults: new { Controller = "Home", action = "Error" });

app.MapControllerRoute(
    name: "productPage",
    pattern: "product/{productId:int}",
    defaults: new { Controller = "Product", action = "ShowProduct" });

app.MapFallbackToController("Error", "Home");


await app.RunAsync();
