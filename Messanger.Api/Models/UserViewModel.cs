using System.ComponentModel.DataAnnotations;

namespace Messanger.Api.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
    }
}