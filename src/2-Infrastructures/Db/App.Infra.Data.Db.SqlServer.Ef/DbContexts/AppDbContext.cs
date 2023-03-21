using App.Infra.Data.Db.SqlServer.Ef.Entities.App;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EventDbEntity> Events { get; set; }
    public DbSet<EventsProcessedDbEntity> EventsProcessed { get; set; }
    public DbSet<UserDbEntity> Users { get; set; }
    public DbSet<DeviceDbEntity> Devices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EventDbEntityConfig());
        modelBuilder.ApplyConfiguration(new EventsProcessedDbEntityConfig());
        modelBuilder.ApplyConfiguration(new UserDbEntityConfig());
        modelBuilder.ApplyConfiguration(new DeviceDbEntityConfig());
    }
}