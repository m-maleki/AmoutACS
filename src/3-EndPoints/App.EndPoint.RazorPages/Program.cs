using App.Domain.Core._Providers.Scheduler.Service;
using App.EndPoint.RazorPages.Infrastructures;
using App.Infra._3rdParties.Scheduler.hangfire._IocConfigs;
using App.Infra._3rdParties.Scheduler.hangfire.Dto;
using System.Text;
using App.Domain.AppServices;
using App.Domain.Core;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Services;
using App.Infra.Data.Db.SqlServer.Ef;
using App.Infra.Data.QueryServices.SqlServer.Dapper;
using App.Infra.Data.Repos.Ef;
using App.Infra.Data.Repos.Ef._IocConfigs;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Framework.Core.Markers;
using sFramework.Core;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((builder, containerBuilder) =>
    {
        var assemblies = new[]
        {
            // Domain
            typeof(IAssemblyMarker_AppDomain_Core).Assembly,
            typeof(IAssemblyMarker_AppDomain_AppServices).Assembly,
            typeof(IAssemblyMarker_AppDomain_Services).Assembly,

            // Infra
            typeof(IAssemblyMarker_AppInfra_DataQuerySqlDapper).Assembly,
            typeof(IAssemblyMarker_AppInfra_DataDbSqlServerEf).Assembly,
            typeof(IAssemblyMarker_AppInfra_DataReposEf).Assembly,

            //s_Framework
            typeof(IAssemblyMarker_Framework_Core).Assembly,
        };

        containerBuilder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<IScopedDependency>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        containerBuilder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<ITransientDependency>()
            .AsImplementedInterfaces()
            .InstancePerDependency();

        containerBuilder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<ISingletonDependency>()
            .AsImplementedInterfaces()
            .SingleInstance();

    });

var siteSettings = builder.Services.Add_SettingAndConfig(configuration);
builder.Services.Add_AppDbContext(siteSettings.ConnectionStrings.AppDb);

builder.Services.Add_SchedulerProviders(siteSettings.ConnectionStrings.SqlDBHangFire);

builder.Services.Add_SchedulerServer(new[] { "ACS", Environment.MachineName, "default" });

builder.Services.AddHttpClient("CosecAPI", client =>
{
    var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{siteSettings.CosecApi.UserName}:{siteSettings.CosecApi.Password}"));
    client.BaseAddress = new Uri(siteSettings.CosecApi.BaseAddress);
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    app.Use_SchedulerProviders(new BasicCredentialDto
    {
        Username = "admin",
        Password = "25915491"
    });

    scope.ServiceProvider.GetRequiredService<ISchedulerProvider>().RemoveAllRecurringJobs();
    scope.ServiceProvider.GetRequiredService<ISyncAppService>().AddRecurringJobs(siteSettings);
}



app.Run();
