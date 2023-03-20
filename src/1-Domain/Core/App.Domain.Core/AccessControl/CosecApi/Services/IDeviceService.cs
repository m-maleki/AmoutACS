using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.Services;

public interface IDeviceService : IScopedDependency
{
    Task DeleteAll(CancellationToken cancellationToken);
}
