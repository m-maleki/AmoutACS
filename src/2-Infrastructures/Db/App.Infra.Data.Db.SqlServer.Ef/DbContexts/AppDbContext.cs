using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.DbContexts;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
