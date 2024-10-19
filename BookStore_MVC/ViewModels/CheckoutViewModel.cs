// ViewModels/CheckoutViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace BookStore_MVC.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal, etc.

        public List<BookViewModel> CartItems { get; set; } = new List<BookViewModel>();
    }

}
