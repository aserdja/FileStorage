using System.ComponentModel.DataAnnotations;

namespace FileStorage.WebApi.DTOs
{
    public class UserDto
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; } = (string)null;

        [Required]
        [MaxLength(20)]
        public string Login { get; set; } = (string)null;

        [Required]
        [MaxLength(62)]
        public string Email { get; set; } = (string)null;

        [Required]
        [MinLength(8)]
        [MaxLength(128)]
        public string Password { get; set; } = (string)null;
    }
}
