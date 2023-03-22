using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.DbContexts;

public class AccountDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUser>(b =>
        {
            b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
        });

        base.OnModelCreating(modelBuilder);
    }
}