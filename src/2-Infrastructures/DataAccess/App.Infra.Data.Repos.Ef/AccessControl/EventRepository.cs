using App.Domain.Core.Cosec.Data.Repositories;
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
                    .Where(x=>x.EventDateTime >= startDateTime && x.EventDateTime <= endDateTime)
                    .CountAsync(cancellationToken);
    }
        

    #endregion
}

