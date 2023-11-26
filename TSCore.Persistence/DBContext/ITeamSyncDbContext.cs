using Microsoft.EntityFrameworkCore;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.DBContext;

public interface ITeamSyncDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
}