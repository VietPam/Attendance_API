using Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class RoleConfiguration : IEntityTypeConfiguration<SqlRole>
{
    public void Configure(EntityTypeBuilder<SqlRole> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();


    }
}
