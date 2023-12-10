using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.Configurations;

public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
{
    public void Configure(EntityTypeBuilder<UserTeam> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => new { e.UserId, e.TeamId });
        builder.ToTable("UserTeams", "TS");

        builder.HasOne(e => e.User)
            .WithMany(e => e.BelongTeams)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Team)
            .WithMany(e => e.TeamMembers)
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}