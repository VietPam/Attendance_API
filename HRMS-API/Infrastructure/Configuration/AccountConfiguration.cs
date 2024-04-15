using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne(x => x.AccountInfo)
            .WithOne(x => x.Account)
            .HasForeignKey<AccountInfo>(x => x.AccountId)
            .IsRequired(false);
        //.OnDelete(DeleteBehavior.Restrict);
    }
}
