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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "/",
	defaults: new { Controller = "Home", action = "Index" });

app.Run();
