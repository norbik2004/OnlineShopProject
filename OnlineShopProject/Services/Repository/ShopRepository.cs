
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Services.ViewModels.Product;

namespace OnlineShopProject.Services.Repository
{
	public class ShopRepository : IShopRepository
	{
		public OnlineShopIdentityDbContext IdentityContext { get; set; }

		public ShopRepository(OnlineShopIdentityDbContext idContext) 
		{
			this.IdentityContext = idContext;
		}
		public IQueryable<Product> GetProducts()
		{
			return this.IdentityContext.Products.Include(p => p.Category)
					.ThenInclude(p => p.Products);
		}

        public Product ShowProductById(long productId)
        {
			if (productId < 0)
			{
				throw new KeyNotFoundException($"Product with ID {productId} not found.");
			}

			var product = this.IdentityContext.Products
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

			var product = await this.IdentityContext.Products.FindAsync(productId);

			if (product == null)
			{
				throw new KeyNotFoundException("product not found");
			}

            if (!string.IsNullOrEmpty(product.IMGFileLink))
            {
                DeleteImage(product.IMGFileLink);
            }

            this.IdentityContext.Products.Remove(product);

			await this.IdentityContext.SaveChangesAsync();

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
			List<Category> categories = this.IdentityContext.Categories.ToList();

			return categories;
		}

		public Category GetCategoryById(int categoryId)
		{
			if(categoryId < 0 || categoryId == 0)
			{
                throw new KeyNotFoundException("Bad ID");
            }

			Category category = this.IdentityContext.Categories.FirstOrDefault(p => p.CategoryId == categoryId);

			if (category == null)
			{
				throw new KeyNotFoundException("Bad ID");
			}
			return category;
		}

        public void UpdateProduct(Product product)
        {
            this.IdentityContext.Products.Update(product);
        }

        public async Task SaveChangesAsync()
        {
            await this.IdentityContext.SaveChangesAsync();
        }

		public List<Comments> GetAllComments(long productId)
		{
            return this.IdentityContext.Comments
				.Where(p => p.ProductId == productId)
				.Include(c => c.User)
				.OrderByDescending(p => p.PublicationDate)
				.ToList();

        }

		public async Task SaveComment(Comments comment)
		{
            Comments model = new()
			{
				ProductId = comment.ProductId,
				UserId = comment.UserId,
				Text = comment.Text,
				Rating = comment.Rating,
				PublicationDate = comment.PublicationDate
			};

			await this.IdentityContext.AddAsync(model);
			await this.IdentityContext.SaveChangesAsync();
		}
    }
}
