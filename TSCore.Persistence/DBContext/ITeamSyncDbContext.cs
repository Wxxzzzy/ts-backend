using Microsoft.EntityFrameworkCore;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.DBContext;

public interface ITeamSyncDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
}