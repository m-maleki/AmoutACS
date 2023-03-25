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

    public async Task<List<EventOutputDto>> Search(int userId, string fromDate, string toDate,
        CancellationToken cancellationToken)
    {
        DateTime startDateTime;
        var endDateTime = DateTime.Now;

        if (userId == 0 & fromDate == null && toDate == null)
        {
            startDateTime = DateTime.Today; //Today at 00:00:00
            endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
            return await _eventService.GetAll(startDateTime, endDateTime, cancellationToken);
        }

        if (fromDate == null && toDate == null)
        {
            startDateTime = DateTime.Today.AddDays(-30); //Today at 00:00:00
            endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
            return await _eventService.Search(userId, startDateTime, endDateTime, cancellationToken);
        }

        startDateTime = CosecDate.DateFormat(fromDate);
        endDateTime = CosecDate.DateFormat(toDate).AddDays(1).AddTicks(-1);

        if (userId == 0)
            return await _eventService.GetAll(startDateTime, endDateTime, cancellationToken);
        else
            return await _eventService.Search(userId, startDateTime, endDateTime, cancellationToken);
    }

    public async Task<Tuple<string, string>> GetDailyEvent(CancellationToken cancellationToken)
    {
        var result = await _eventService.GetDailyEvent(cancellationToken);

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

