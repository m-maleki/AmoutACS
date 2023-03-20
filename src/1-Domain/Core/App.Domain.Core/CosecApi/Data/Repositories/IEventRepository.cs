using Framework.Core.Markers;

namespace App.Domain.Core.CosecApi.Data.Repositories;
public interface IEventRepository : IScopedDependency
{
    Task<int> GetTodayEventsCount(CancellationToken cancellationToken);
}
