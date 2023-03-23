using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.CosecApi.AppServices;
public interface IDeviceApiAppService : IScopedDependency
{
    Task Create(CreateDeviceDto model, CancellationToken cancellationToken);
    Task Delete(DeleteDeviceDto model, CancellationToken cancellationToken);
    Task OpenDoor(int deviceId, int deviceType,CancellationToken cancellationToken);
    Task LockDoor(int deviceId, int deviceType, CancellationToken cancellationToken);
    Task UnLockDoor(int deviceId, int deviceType, CancellationToken cancellationToken);
    Task NormalizeDoor(int deviceId, int deviceType, CancellationToken cancellationToken);
    Task SyncCredentials(int deviceId, int deviceType, CancellationToken cancellationToken);
}