using System.ComponentModel.DataAnnotations;

namespace BookStore_MVC.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // You can add additional properties as needed
        // For example, you might want to include:
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Login Date")]
        public DateTime LastLoginDate { get; set; }

        // Add other properties that you might want to display in the user list or detail view
    }
}
