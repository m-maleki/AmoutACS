using Framework.Core.Markers;

namespace App.Domain.Core.CosecApi.Services;
public interface IAvailabilityService : IScopedDependency
{
    Task<bool> AvailableCosecApi(CancellationToken cancellationToken);
    Task<bool> AvailableCosecDatabase(CancellationToken cancellationToken);
    Task<bool> AvailableACSDB(CancellationToken cancellationToken);
}