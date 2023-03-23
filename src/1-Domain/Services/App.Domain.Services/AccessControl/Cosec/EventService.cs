using App.Domain.Core.AccessControl.Services;
using App.Domain.Core.Cosec.Data.Repositories;
using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.CosecApi.Dtos;

namespace App.Domain.Services.AccessControl.Cosec;
public class EventService : IEventService
{
    #region Fields

    private readonly IEventRepository _eventRepository;

    #endregion

    #region Ctor
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    #endregion

    #region Implementations

    public async Task<int> GetTodayEventsCount(CancellationToken cancellationToken)
        => await _eventRepository.GetTodayEventsCount(cancellationToken);

    public async Task<List<EventOutputDto>> GetAll(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        => await _eventRepository.GetAll(fromDate, toDate, cancellationToken);

    public async Task<DailyReportDto> GetDailyEvent(CancellationToken cancellationToken)
        => await _eventRepository.GetDailyEvent(cancellationToken);

    #endregion
}

