using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.Dashboard.BasicAuthorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core._Providers.Scheduler.Service;
using App.Infra._3rdParties.Scheduler.hangfire.Dto;
using App.Infra._3rdParties.Scheduler.hangfire.HangfireProvider;
using Hangfire.Heartbeat;
using Hangfire.SqlServer;


namespace App.Infra._3rdParties.Scheduler.hangfire._IocConfigs
{
    public static class ServiceCollectionExtensions
    {
        public static void Add_SchedulerProviders(this IServiceCollection services, string sqlHangfireConnectionString)
        {
            services.AddScoped<ISchedulerProvider, SchedulerProvider>();
            services.AddHangfire(config =>
            {
                config
                    .UseDefaultTypeSerializer()
                    .UseSimpleAssemblyNameTypeSerializer()
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseHeartbeatPage(TimeSpan.FromSeconds(1))
                    .UseSqlServerStorage(sqlHangfireConnectionString, new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
            });

            services.AddHangfireServer();

        }

        public static void Add_SchedulerServer(this IServiceCollection services, string[] queues)
        {
            services.AddHangfireServer(options =>
            {
                options.Queues = queues.ToList().Select(x => x.ToLower()).ToArray();

            });
        }

        public static void Use_SchedulerProviders(this IApplicationBuilder app, BasicCredentialDto credential)
        {
            app.UseHangfireDashboard("/dashboard", new DashboardOptions
            {
                DashboardTitle = "ACS HangFire Dashboard",
                AppPath = "https://localhost",
                IsReadOnlyFunc = _ => false,
                DisplayStorageConnectionString = true,
                IgnoreAntiforgeryToken = true,
                Authorization = new[]
                {
                new BasicAuthAuthorizationFilter(
                    new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = false,
                        SslRedirect = false,
                        LoginCaseSensitive = true,
                        Users = new[]
                        {
                            new BasicAuthAuthorizationUser
                                {Login = credential.Username, PasswordClear = credential.Password}
                        }
                    })
            }
            });
        }
    }
}
