using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.Infra.Data.Repos.Ef._IocConfigs;

//

public static partial class ServiceCollectionExtensions_AccountDbContext
{
    public static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder => { });
    public static void Add_AccountDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<AccountDbContext>(options =>
        {
#if DEBUG
            options.UseLoggerFactory(DbLoggerFactory).UseSqlServer(connectionString,
                options => { options.MigrationsHistoryTable("__EFMigrationsHistory", "dbo"); });
#else
                 options.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
                });
#endif
        });
    }
}