using Framework.Core.Configs;

namespace App.EndPoint.RazorPages.Infrastructures;
public static class ServiceCollectionExtensions
{
    public static SiteSettings Add_SettingAndConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

        services.AddSingleton(siteSettings);

        return siteSettings;
    }
}