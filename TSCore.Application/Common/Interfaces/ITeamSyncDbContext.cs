using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TSCore.Domain.Tables;

namespace TSCore.Application.Common.Interfaces;

public interface ITeamSyncDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DatabaseFacade Database { get; }
    
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
}