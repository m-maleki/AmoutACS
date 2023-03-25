using App.Domain.Core.Cosec.Data.Repositories;
using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.AccessControl;
public class EventRepository : IEventRepository
{
    #region Fields

    private readonly AppDbContext _appDbContext;

    #endregion

    #region Ctor

    public EventRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #endregion

    #region Implementations

    public async Task<int> GetTodayEventsCount(CancellationToken cancellationToken)
    {
        DateTime startDateTime = DateTime.Today; //Today at 00:00:00
        DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

        return await _appDbContext.Events
                    .Where(x => x.EventDateTime >= startDateTime && x.EventDateTime <= endDateTime)
                    .CountAsync(cancellationToken);
    }

    public async Task<List<EventOutputDto>> GetAll(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Events.AsNoTracking()
            .Where(x => x.EventDateTime >= fromDate && x.EventDateTime <= toDate)
            //.Include(x=>x.Device)
            .Select(x => new EventOutputDto
            {
                DoorControllerId = x.DoorControllerId,
                EntryExitType = x.EntryExitType,
                EventDateTime = x.EventDateTime,
                IDateTime = x.IDateTime,
                Id = x.Id,
                UserId = x.UserId,
                IndexNo = x.IndexNo,
                Leavedt = x.Leavedt,
                MasterControllerId = x.MasterControllerId,
                SpecialFunctionId = x.SpecialFunctionId,
                Username = x.Username,
                DeviceName = x.Device.Name
            })
            .OrderByDescending(x => x.EventDateTime)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<List<EventOutputDto>> Search(int userId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Events.AsNoTracking()
            .Where(x => x.UserId == userId &&  x.EventDateTime >= fromDate && x.EventDateTime <= toDate)
            .Include(x => x.Device)
            .Select(x => new EventOutputDto
            {
                DoorControllerId = x.DoorControllerId,
                EntryExitType = x.EntryExitType,
                EventDateTime = x.EventDateTime,
                IDateTime = x.IDateTime,
                Id = x.Id,
                UserId = x.UserId,
                IndexNo = x.IndexNo,
                Leavedt = x.Leavedt,
                MasterControllerId = x.MasterControllerId,
                SpecialFunctionId = x.SpecialFunctionId,
                Username = x.Username,
                DeviceName = x.Device.Name
            })
            .OrderByDescending(x => x.EventDateTime)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<DailyReportDto> GetDailyEvent(CancellationToken cancellationToken)
    {
        DateTime startDateTime = DateTime.Today.AddDays(-30); ; //Today at 00:00:00
        DateTime endDateTime = DateTime.Today;

        var result2 = await _appDbContext.Events
            .Where(x => x.EventDateTime >= startDateTime && x.EventDateTime <= endDateTime)
            .ToListAsync(cancellationToken);

        var result = result2
            //.OrderByDescending(x => x.EventDateTime)
            .GroupBy(p => p.EventDateTime.Date)
            .Select(g => new { date = g.Key, count = g.Count() });

        var report = new DailyReportDto
        {
            Events = new List<int>(),
            Days = new List<DateTime>()
        };

        foreach (var group in result)
        {
            report.Days.Add(group.date.Date);
            report.Events.Add(group.count);
        }

        return report;
    }

    #endregion
}

