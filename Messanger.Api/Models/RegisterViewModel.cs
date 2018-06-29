using System.ComponentModel.DataAnnotations;

namespace Messanger.Api.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
    }
}