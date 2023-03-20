using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.Services;
public interface IEventService : IScopedDependency
{
    Task<int> GetTodayEventsCount(CancellationToken cancellationToken);
}

