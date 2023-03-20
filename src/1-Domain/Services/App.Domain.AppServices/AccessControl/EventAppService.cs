using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.Services;

namespace App.Domain.AppServices.AccessControl;
public class EventAppService : IEventAppService
{
    #region Fields

    private readonly IEventService _eventService;

    #endregion

    #region Ctor
    public EventAppService(IEventService eventService)
    {
        _eventService = eventService;
    }

    #endregion

    #region Implementations

    public async Task<int> GetTodayEventsCount(CancellationToken cancellationToken)
        => await _eventService.GetTodayEventsCount(cancellationToken);

    #endregion
}

