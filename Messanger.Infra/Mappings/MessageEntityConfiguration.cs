using Messanger.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Messanger.Infra.Mappings
{
    public class MessageEntityConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageEntityConfiguration()
        {
            ToTable("Message");
            HasKey(m => m.MessageId);
            Property(m => m.Text)
                .HasMaxLength(50)
                .IsRequired();
            HasRequired(m => m.Dialog)
                .WithMany(d => d.Messages)
                .HasForeignKey(m => m.DialogId);
        }
    }
}
