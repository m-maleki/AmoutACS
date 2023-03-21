using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Data.Repositories;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using App.Infra.Data.Db.SqlServer.Ef.Entities.App;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.AccessControl;

public class EventsProcessedRepository : IEventsProcessedRepository
{
    #region Fields

    private readonly AppDbContext _appDbContext;

    #endregion

    #region Ctor

    public EventsProcessedRepository(AppDbContext context)
    {
        _appDbContext = context;
    }

    #endregion

    #region Implementations

    public async Task Add(EventsProcessedDto model, CancellationToken cancellationToken)
    {
        var entity = new EventsProcessedDbEntity
        {
            LastIndexNo = model.LastIndexNo,
            ProcessedCount = model.ProcessedCount,
            CreateAt = DateTime.Now,
        };

        await _appDbContext.EventsProcessed.AddAsync(entity, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetLastIndex(CancellationToken cancellationToken)
    {
        var result = await _appDbContext.EventsProcessed
            .OrderByDescending(x => x.LastIndexNo)
            .FirstOrDefaultAsync(cancellationToken);

        return result?.LastIndexNo ?? 0;
    }


    #endregion
}