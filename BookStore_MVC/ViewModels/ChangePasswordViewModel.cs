using System.ComponentModel.DataAnnotations;

namespace BookStore_MVC.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}[\]:;<>,.?~\\/-]).{8,}$", ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm your new password.")]
        [DataType(DataType.Password)]
        //[Compare("NewPassword", ErrorMessage = "The confirmation password does not match the new password.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
