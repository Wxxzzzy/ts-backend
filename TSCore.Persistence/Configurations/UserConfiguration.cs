using TSCore.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TSCore.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Username);

        builder.ToTable("Users", "TS");
        
        builder.Property(e => e.Username)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.PasswordHash)
            .HasMaxLength(1024)
            .IsRequired();
        builder.Property(e => e.Salt)
            .HasMaxLength(1024)
            .IsRequired();
        builder.Property(e => e.UpdatedBy)
            .IsRequired(false);

        builder.HasOne(e => e.Role)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.RoleId);

        builder.HasMany(e => e.CreatedTickets)
            .WithOne(e => e.TicketCreator)
            .HasForeignKey(e => e.TicketCreatorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.AssignedTickets)
            .WithOne(e => e.AssignedTo)
            .HasForeignKey(e => e.AssignedToId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}