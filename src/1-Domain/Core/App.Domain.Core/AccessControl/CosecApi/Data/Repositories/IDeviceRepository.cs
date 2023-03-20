using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.Data.Repositories;

public interface IDeviceRepository : IScopedDependency
{
    Task DeleteAll(CancellationToken cancellationToken);
}
