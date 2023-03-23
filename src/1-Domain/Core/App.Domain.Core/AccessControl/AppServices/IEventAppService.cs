using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.AppServices;
public interface IEventAppService : IScopedDependency
{
    Task<int> GetTodayEventsCount(CancellationToken cancellationToken);
    Task<List<EventOutputDto>> GetAll(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
    Task<Tuple<string, string>> GetDailyEvent(CancellationToken cancellationToken);
}

