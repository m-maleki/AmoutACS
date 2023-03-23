using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.Services;
using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Utilities;

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

    public async Task<List<EventOutputDto>> GetAll(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        => await _eventService.GetAll(fromDate, toDate, cancellationToken);

    public async Task<Tuple<string, string>> GetDailyEvent(CancellationToken cancellationToken)
    {
        var result = await _eventService.GetDailyEvent(cancellationToken);

        result.Events = result.Events.Take(10).ToList();
        result.Days = result.Days.Take(10).ToList();

        if (result.Events.Any())
        {
            var days = result.Days.Aggregate("", (current, item) => current + $"'{item.Date.ToPersianString()}',");

            var counts = result.Events.Aggregate("", (current, item) => current + $"{item.ToString()},");

            if (!string.IsNullOrEmpty(days))
            {
                days = days.Remove(days.Length - 1);
                counts = counts.Remove(counts.Length - 1);
            }
            return new Tuple<string, string>(days, counts);
        }

        return new Tuple<string, string>("", "");
    }


    #endregion
}

