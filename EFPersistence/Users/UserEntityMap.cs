using Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFPersistence.Users
{
    public class UserEntityMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.OwnsOne(user => user.Phone, phone=>
            {
                phone.Property(_ => _.CountryCallingCode).HasColumnName("CountryCallingCode");
                phone.Property(_ => _.PhoneNumber).HasColumnName("PhoneNumber");
            });
        }
    }
}
