using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.AppServices;
public interface IEventAppService : IScopedDependency
{
    Task<int> GetTodayEventsCount(CancellationToken cancellationToken);
}

