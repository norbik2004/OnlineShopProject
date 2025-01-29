﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopProject.Services.Repository
{
	public class ShopRepository : IShopRepository
	{
		public OnlineShopDbContext ShopContext { get; set; }
		public OnlineShopIdentityDbContext IdentityContext { get; set; }

		public ShopRepository(OnlineShopDbContext context, OnlineShopIdentityDbContext idContext) 
		{
			this.ShopContext = context;
			this.IdentityContext = idContext;
		}
		public IQueryable<Product> GetProducts()
		{
			return this.ShopContext.Products.Include(p => p.Category)
					.ThenInclude(p => p.Products);
		}

        public Product ShowProductById(long productId)
        {
			if (productId < 0)
			{
				throw new KeyNotFoundException($"Product with ID {productId} not found.");
			}

			var product = this.ShopContext.Products
                .Include(p => p.Category)
					.ThenInclude(p => p.Products)
                .FirstOrDefault(p => p.ProductId == productId);

			if (product == null)
			{
				throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

			return product;
        }

		public Users ShowUserByEmail(string email)
		{
			var user = this.IdentityContext.Users.FirstOrDefault(p => p.Email == email);

			if (user == null)
			{
				throw new KeyNotFoundException("User not found");
			}

			return user;

		}

		public IEnumerable<Users> GetAllUsers()
		{
			IEnumerable<Users> users = this.IdentityContext.Users;

			if (users == null || !users.Any())
			{
				throw new KeyNotFoundException("Users not found");
			}

			return users;
		}

        public async Task<bool> DeleteProductAsync(long productId)
        {
			if (productId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(productId), "product id below 0");
			}

			var product = await this.ShopContext.Products.FindAsync(productId);

			if (product == null)
			{
				throw new KeyNotFoundException("product not found");
			}

            if (!string.IsNullOrEmpty(product.IMGFileLink))
            {
                DeleteImage(product.IMGFileLink);
            }

            this.ShopContext.Products.Remove(product);

			await this.ShopContext.SaveChangesAsync();

			return true;
        }

        private void DeleteImage(string imagePath)
        {
            string basePath = AppContext.BaseDirectory;

            string rootPath = basePath.Substring(0, basePath.IndexOf("bin", StringComparison.Ordinal));

            string fullPath = Path.Combine(rootPath, "wwwroot", "images", imagePath);

			fullPath += ".png";

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

		public List<Category> GetAllCategories()
		{
			List<Category> categories = this.ShopContext.Categories.ToList();

			return categories;
		}

		public Category GetCategoryById(int categoryId)
		{
			if(categoryId < 0 || categoryId == 0)
			{
                throw new KeyNotFoundException("Bad ID");
            }

			Category category = this.ShopContext.Categories.FirstOrDefault(p => p.CategoryId == categoryId);

			if (category == null)
			{
				throw new KeyNotFoundException("Bad ID");
			}
			return category;
		}

        public void UpdateProduct(Product product)
        {
            this.ShopContext.Products.Update(product);
        }

        public async Task SaveChangesAsync()
        {
            await this.ShopContext.SaveChangesAsync();
        }
    }
}
