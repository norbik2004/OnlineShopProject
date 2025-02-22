using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Services;
using OnlineShopProject.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Register Razor Pages and MVC
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Identity services
builder.Services.AddDbContext<OnlineShopIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"]));

// Registration of repository services
builder.Services.AddScoped<IShopRepository, ShopRepository>();

builder.Services.Configure<FormOptions>(options =>
{
	options.ValueLengthLimit = 1024 * 1024 * 10;  // 10 MB
	options.MultipartBodyLengthLimit = 1024 * 1024 * 10;  // 10 MB
});

builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<OnlineShopIdentityDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();
builder.Services.AddScoped<Cart>(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "User"};

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}


await app.RunAsync();
