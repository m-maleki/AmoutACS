using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.QueryServices;
public interface IDeviceQueryServices : IScopedDependency
{
    Task BulkInsert(List<DeviceChildDto> model, CancellationToken cancellationToken);
}
