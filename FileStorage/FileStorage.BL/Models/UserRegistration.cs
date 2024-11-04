using System.ComponentModel.DataAnnotations;

namespace FileStorage.BL.Models
{
    public class UserRegistration
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The \"Name\" must contain from 3 to 20 characters!")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The \"Login\" must contain from 3 to 20 characters!")]
        public string Login { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(24, MinimumLength = 8, ErrorMessage = "The \"Password\" must contain from 8 to 24 characters!")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Value does not match password!")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}