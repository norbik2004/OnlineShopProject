using System.ComponentModel.DataAnnotations;
using System.IO;

namespace OnlineShopProject.Services.ViewModels
{
    public class ImageValidationAttribute : ValidationAttribute
    {
        private readonly string[] _validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public ImageValidationAttribute() : base("Invalid file type. Allowed types: .jpg, .png, .gif")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName)?.ToLower();

                if (!_validExtensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}