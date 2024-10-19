using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using BookStore_MVC.Validations;

namespace BookStore_MVC.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a language")]
        [Display(Name = "Language")]
        public int LanguageId { get; set; } // Keep as int to match the Book model

        [JsonProperty("Language")]
        public string LanguageName { get; set; }

        [Required]
        [Range(100, 1000, ErrorMessage = "Pages number should be in range of 100 and 1000")]
        [Display(Name = "Number of Pages")]
        public int? TotalPages { get; set; }

        [Required(ErrorMessage = "Please select one or more categories")]
        [Display(Name = "Category")]
        public IEnumerable<int> CategoryIds { get; set; }

        //[Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price.")]
        public decimal? Price { get; set; }

        [JsonProperty("Categories")]
        public IEnumerable<string> CategoryNames { get; set; }

        [Required]
        [ImageExtensions] // Custom validation attribute
        [Display(Name = "Choose Cover Photo")]
        public IFormFile CoverPhoto { get; set; }

        [JsonProperty("Cover Photo")]
        public string CoverPhotoPath { get; set; }

        [Required]
        [ImageExtensions] // Custom validation attribute
        [Display(Name = "Gallery")]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryViewModel> Gallery { get; set; }

        [Required]
        [Display(Name = "Upload book in pdf format")]
        [PdfExtension] // Custom validation attribute
        public IFormFile Pdf { get; set; }

        [JsonProperty("Pdf")]
        public string PdfPath { get; set; }

        public string UserId { get; set; }
    }
}
