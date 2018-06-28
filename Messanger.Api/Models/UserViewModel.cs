using System.Collections.Generic;

namespace Messanger.Api.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public IEnumerable<DialogViewModel> Dialogs { get; set; }
    }
}