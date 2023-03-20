using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.Services;

public interface IDeviceService : IScopedDependency
{
    Task DeleteAll(CancellationToken cancellationToken);
    Task<int> GetCount(CancellationToken cancellationToken);
    Task<int> GetActiveDevicesCount(CancellationToken cancellationToken);
}
