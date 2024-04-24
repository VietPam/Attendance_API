using Domain.Entities.Accounts;
using Domain.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class AccountConfiguration : IEntityTypeConfiguration<SqlAccount>
{
    public void Configure(EntityTypeBuilder<SqlAccount> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne(x => x.User)
            .WithOne(x => x.Account)
            .HasForeignKey<SqlUser>(x => x.AccountId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Accounts)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
