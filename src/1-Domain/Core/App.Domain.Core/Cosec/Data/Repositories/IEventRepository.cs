using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Data.Repositories;
public interface IEventRepository : IScopedDependency
{
    Task<int> GetTodayEventsCount(CancellationToken cancellationToken);
}
