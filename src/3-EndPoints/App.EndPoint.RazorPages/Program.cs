using App.Domain.Core._Providers.Scheduler.Service;
using App.EndPoint.RazorPages.Infrastructures;
using App.Infra._3rdParties.Scheduler.hangfire._IocConfigs;
using App.Infra._3rdParties.Scheduler.hangfire.Dto;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

var siteSettings = builder.Services.Add_SettingAndConfig(configuration);

builder.Services.Add_SchedulerProviders(siteSettings.ConnectionStrings.SqlDBHangFire);

builder.Services.Add_SchedulerServer(new[] { "ACS", Environment.MachineName, "default" });

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
}



app.Run();
