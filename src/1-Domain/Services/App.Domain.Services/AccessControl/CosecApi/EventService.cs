using App.Domain.Core.AccessControl.Services;
using App.Domain.Core.CosecApi.Data.Repositories;

namespace App.Domain.Services.AccessControl.CosecApi;
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

    #endregion
}

