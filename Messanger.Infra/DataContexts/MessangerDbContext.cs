using Messanger.Domain;
using Messanger.Infra.Mappings;
using System.Data.Entity;

namespace Messanger.Infra.DataContexts
{
    public class MessangerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }

        public MessangerDbContext() : base("MessangerDb")
        {
            Database.SetInitializer(new MessangerDbContextInializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new DialogEntityConfiguration());
            modelBuilder.Configurations.Add(new MessageEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class MessangerDbContextInializer : DropCreateDatabaseIfModelChanges<MessangerDbContext>
    {
        protected override void Seed(MessangerDbContext context)
        {
            User user1 = new User { Id = 1, Name = "User1", Password = "pas1", AvatarUrl = "ava1" };
            User user2 = new User { Id = 2, Name = "User2", Password = "pas2", AvatarUrl = "ava2" };

            Message message1 = new Message { Id = 1, Text = "Hello", DialogId = 1, SenderId = 1, ReceiverId = 2 };
            Message message2 = new Message { Id = 2, Text = "Hi", DialogId = 1, SenderId = 2, ReceiverId = 1 };
            Dialog dialog = new Dialog { Id = 1, UserId = 1 };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();

            context.Dialogs.Add(dialog);
            context.SaveChanges();

            context.Messages.Add(message1);
            context.Messages.Add(message2);
            context.SaveChanges();
        }
    }
}
