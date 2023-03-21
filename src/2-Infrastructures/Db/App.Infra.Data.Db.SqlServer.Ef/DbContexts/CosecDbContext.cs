using App.Infra.Data.Db.SqlServer.Ef.Entities.App;
using App.Infra.Data.Db.SqlServer.Ef.Entities.Cosec;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.DbContexts;
public class CosecDbContext : DbContext
{

    public CosecDbContext(DbContextOptions<CosecDbContext> options) : base(options)
    {
    }

    public DbSet<UserAccessDbEntity> UserAccess { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserAccessDbEntityConfig());
    }
}

