using Domain.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<SqlUser>
{
    public void Configure(EntityTypeBuilder<SqlUser> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).IsRequired();
        builder.Property(x => x.PhoneNumber);
        builder.Property(x => x.Avatar);
        builder.Property(x => x.BirthDay);
        builder.Property(x => x.Gender);
        builder.Property(x => x.IdNumber);
        builder.Property(x => x.Address);


        builder.HasOne(x => x.Account)
            .WithOne(x => x.User)
            .IsRequired(true);
    }
}
