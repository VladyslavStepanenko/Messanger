using Messanger.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Messanger.Infra.Mappings
{
    public class DialogEntityConfiguration : EntityTypeConfiguration<Dialog>
    {
        public DialogEntityConfiguration()
        {
            ToTable("Dialog");
            HasKey(x => x.Id);
            HasRequired(d => d.User)
                .WithMany(u => u.Dialogs)
                .HasForeignKey(d => d.UserId);
        }
    }
}
