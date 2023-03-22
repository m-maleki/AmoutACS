using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.AppServices;
public interface IAvailabilityAppService : IScopedDependency
{
    Task<bool> AvailableCosecApi(CancellationToken cancellationToken);
    Task<bool> AvailableCosecDatabase(CancellationToken cancellationToken);
    Task<bool> AvailableACSDB(CancellationToken cancellationToken);
}
