using System.ComponentModel.DataAnnotations;

namespace urlshortener.Models
{
    public class UserLoginModel
    {
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}