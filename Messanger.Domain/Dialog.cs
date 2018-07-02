using System.Collections.Generic;

namespace Messanger.Domain
{
    public class Dialog
    {
        public int DialogId { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public Dialog()
        {
            Users = new HashSet<User>();
            Messages = new HashSet<Message>();
        }
    }
}