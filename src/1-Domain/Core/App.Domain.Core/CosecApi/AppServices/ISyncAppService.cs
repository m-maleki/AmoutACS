using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Configs;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.AppServices;

public interface ISyncAppService: IScopedDependency
{
    void AddRecurringJobs(SiteSettings siteSettings);
    Task SyncEvents(CancellationToken cancellationToken);
    Task SyncUsers(CancellationToken cancellationToken);
    Task ReSyncUser(string userId, CancellationToken cancellationToken);
    Task SyncDevices(CancellationToken cancellationToken);
    Task<UserChildDto?> GetUser(string userId,CancellationToken cancellationToken);
}
