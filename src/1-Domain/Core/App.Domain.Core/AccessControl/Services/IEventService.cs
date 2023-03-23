using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.Services;
public interface IEventService : IScopedDependency
{
    Task<int> GetTodayEventsCount(CancellationToken cancellationToken);
    Task<List<EventOutputDto>> GetAll(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
    Task<DailyReportDto> GetDailyEvent(CancellationToken cancellationToken);
}

