using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSCore.Application.Common.Extensions;
using TSCore.Domain.Enums;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    private readonly Role[] _roles =
    {
        new(){Id = (int)ERoles.Admin, RoleName = ERoles.Admin.ToDescription(), Description = "Application administrator",
            CreatedAt = DateTime.Now, CreatedBy = "System"},
        new(){Id = (int)ERoles.User, RoleName = ERoles.User.ToDescription(), Description = "Common user",
            CreatedAt = DateTime.Now, CreatedBy = "System"}
    };
    
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("Roles", "TS");
        
        builder.Property(e => e.RoleName)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(255)
            .IsRequired(false);
        builder.Property(e => e.UpdatedBy)
            .IsRequired(false);

        builder.HasData(_roles);
    }
}