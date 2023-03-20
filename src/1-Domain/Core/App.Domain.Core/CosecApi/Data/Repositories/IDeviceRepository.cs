using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.Data.Repositories;

public interface IDeviceRepository : IScopedDependency
{
    Task DeleteAll(CancellationToken cancellationToken);
    Task<int> GetCount(CancellationToken cancellationToken);
    Task<int> GetActiveDevicesCount(CancellationToken cancellationToken);
}
