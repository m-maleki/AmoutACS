using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.AppServices;
public interface IDeviceAppService : IScopedDependency
{
    Task<int> GetCount(CancellationToken cancellationToken);
    Task<int> GetActiveDevicesCount(CancellationToken cancellationToken);
    Task<List<DeviceOutputDto>> getDevicesWithUserAccess(int userId, CancellationToken cancellationToken);
    Task<List<DeviceOutputDto>> GetAll(CancellationToken cancellationToken);
    Task<int> GetDeviceTypeWithDeviceId(int deviceId, CancellationToken cancellationToken);
}
