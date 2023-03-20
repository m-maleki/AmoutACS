using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.AppServices;
public interface IDeviceAppService : IScopedDependency
{
    Task<int> GetCount(CancellationToken cancellationToken);
    Task<int> GetActiveDevicesCount(CancellationToken cancellationToken);
}
