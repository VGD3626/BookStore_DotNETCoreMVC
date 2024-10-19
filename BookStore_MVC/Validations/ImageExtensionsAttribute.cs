using System.ComponentModel.DataAnnotations;
using System.Collections;


namespace BookStore_MVC.Validations
{
    public class ImageExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public ImageExtensionsAttribute()
        {
            _extensions = new string[] { ".jpg", ".png", ".jpeg" };
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is ICollection)
            {
                var files = value as ICollection;
                foreach (IFormFile file in files)
                {
                    if (!validExtension(Path.GetExtension(file.FileName)))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }
            else
            {
                var file = value as IFormFile;
                if (file != null)
                {
                    if (!validExtension(Path.GetExtension(file.FileName)))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }
            return ValidationResult.Success;
        }

        private bool validExtension(string extension)
        {
            return _extensions.Contains(extension.ToLower());
        }

        private string GetErrorMessage()
        {
            return ErrorMessage ?? "Images Extension should be jpg or png";
        }
    }
}
