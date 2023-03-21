using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Services;

public interface IDeviceService : IScopedDependency
{
    Task DeleteAll(CancellationToken cancellationToken);
    Task<int> GetCount(CancellationToken cancellationToken);
    Task<int> GetActiveDevicesCount(CancellationToken cancellationToken);
    Task<List<DeviceOutputDto>> GetAll(CancellationToken cancellationToken);
    Task<int> GetDeviceTypeWithDeviceId(int deviceId, CancellationToken cancellationToken);
}
