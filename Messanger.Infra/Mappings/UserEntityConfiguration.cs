using Messanger.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Messanger.Infra.Mappings
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            ToTable("User");
            HasKey(x => x.Id);
            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
