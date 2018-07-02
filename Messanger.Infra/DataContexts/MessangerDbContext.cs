using Messanger.Domain;
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
            base.OnModelCreating(modelBuilder);
        }
    }

    public class MessangerDbContextInializer : DropCreateDatabaseIfModelChanges<MessangerDbContext>
    {
        protected override void Seed(MessangerDbContext context)
        {
            User user1 = new User { UserId = 1, Name = "User1", Password = "pas1", AvatarUrl = "ava1" };
            User user2 = new User { UserId = 2, Name = "User2", Password = "pas2", AvatarUrl = "ava2" };

            Message message1 = new Message { MessageId = 1, Text = "Hello" };
            message1.User = user1;
            message1.UserId = user1.UserId;
            Message message2 = new Message { MessageId = 2, Text = "Hi" };
            message2.User = user2;
            message2.UserId = user2.UserId;

            user1.Messages.Add(message1);
            user2.Messages.Add(message2);

            Dialog dialog = new Dialog { DialogId = 1 };
            dialog.Users.Add(user1);
            dialog.Users.Add(user2);
            dialog.Messages.Add(message1);
            dialog.Messages.Add(message2);

            message1.Dialog = dialog;
            message1.DialogId = dialog.DialogId;
            message2.Dialog = dialog;
            message2.DialogId = dialog.DialogId;

            user1.Dialogs.Add(dialog);
            user2.Dialogs.Add(dialog);

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
