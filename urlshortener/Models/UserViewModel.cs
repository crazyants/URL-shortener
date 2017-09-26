using System.ComponentModel.DataAnnotations;

namespace urlshortener.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Nickname is required")]
        [RegularExpression(@"^[\S]*$", ErrorMessage = "Invalid nickname")]
        [MinLength(3, ErrorMessage = "Nickname can't be shorter than 3 characters")]
        [MaxLength(15, ErrorMessage = "Nickname can't be longer than 15 characters")]
        public string Nickname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}