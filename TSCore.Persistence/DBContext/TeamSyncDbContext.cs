using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.DBContext;

public class TeamSyncDbContext : DbContext, ITeamSyncDbContext
{
    public TeamSyncDbContext()
    {
    }
    
    public TeamSyncDbContext(DbContextOptions<TeamSyncDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamSyncDbContext).Assembly);
    }
}