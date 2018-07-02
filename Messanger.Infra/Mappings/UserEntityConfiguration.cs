using Messanger.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Messanger.Infra.Mappings
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            ToTable("User");
            HasKey(x => x.UserId);
            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
            HasMany(u => u.Dialogs)
                .WithMany(d => d.Users)
                .Map(ud =>
                {
                    ud.MapLeftKey("UserRefId");
                    ud.MapRightKey("DialogRefId");
                    ud.ToTable("UserDialog");
                });
        }
    }
}
