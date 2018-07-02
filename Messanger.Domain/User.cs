using System.Collections.Generic;

namespace Messanger.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public User()
        {
            Dialogs = new HashSet<Dialog>();
            Messages = new HashSet<Message>();
        }
    }
}
