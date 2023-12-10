using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("Teams", "TS");

        builder.Property(e => e.TeamName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(255)
            .IsRequired(false);
        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);
        
        builder.HasOne(e => e.User)
            .WithMany(e => e.Teams)
            .HasForeignKey(e => e.Owner);
    }
}