using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Data.Repositories;

public interface IDeviceRepository : IScopedDependency
{
    Task DeleteAll(CancellationToken cancellationToken);
    Task<int> GetCount(CancellationToken cancellationToken);
    Task<int> GetActiveDevicesCount(CancellationToken cancellationToken);
    Task<List<DeviceOutputDto>> GetAll(CancellationToken cancellationToken);
    Task<int> GetDeviceTypeWithDeviceId(int deviceId, CancellationToken cancellationToken);
}
