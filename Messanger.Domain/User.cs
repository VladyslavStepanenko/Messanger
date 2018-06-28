using System.Collections.Generic;

namespace Messanger.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }

        public virtual ICollection<Dialog> Dialogs { get; set; }

        public User()
        {
            Dialogs = new HashSet<Dialog>();
        }
    }
}
