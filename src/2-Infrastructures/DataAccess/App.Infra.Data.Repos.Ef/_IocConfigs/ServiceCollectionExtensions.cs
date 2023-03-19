using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.Infra.Data.Repos.Ef._IocConfigs;

public static partial class ServiceCollectionExtensions
{
    public static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder => {});

    public static void Add_AppDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<AppDbContext>(options =>
        {
#if DEBUG
            options.UseLoggerFactory(DbLoggerFactory).UseSqlServer(connectionString,
                options => { options.MigrationsHistoryTable("__EFMigrationsHistory", "app"); });
#else
                 options.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsHistoryTable("__EFMigrationsHistory", "app");
                });
#endif
        });
    }
}