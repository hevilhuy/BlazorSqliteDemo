using BlazorSqlite.Services;
using Microsoft.EntityFrameworkCore;

namespace SqliteDAL.DAL;

public class BlazorContext : DbContext
{
    private readonly DatabasePersistenceService _databasePersistenceService;

    public BlazorContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
}
