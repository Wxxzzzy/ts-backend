using Microsoft.EntityFrameworkCore;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.DBContext;

public class TeamSyncDbContext : DbContext, ITeamSyncDbContext
{
    public TeamSyncDbContext(DbContextOptions<TeamSyncDbContext> options) : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}