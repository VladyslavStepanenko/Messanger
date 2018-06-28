using Messanger.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.Infra.DataContexts
{
    public class MessangerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }

        public MessangerDbContext() : base("MessangerDb")
        {

        }
    }
}
