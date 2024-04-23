using Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne(x => x.User)
            .WithOne(x => x.Account)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
