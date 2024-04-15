using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class AccountInfoConfiguration : IEntityTypeConfiguration<AccountInfo>
{
    public void Configure(EntityTypeBuilder<AccountInfo> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired(false);
        builder.Property(x => x.Avatar).IsRequired(false);
        builder.Property(x => x.BirthDay).IsRequired(false);
        builder.Property(x => x.Gender).IsRequired(false);
        builder.Property(x => x.IdNumber).IsRequired(false);
        builder.Property(x => x.Address).IsRequired(false);


        builder.HasOne(x => x.Account)
            .WithOne(x => x.AccountInfo)
            .HasForeignKey<AccountInfo>(x => x.AccountId)
            .IsRequired(true);
    }
}
