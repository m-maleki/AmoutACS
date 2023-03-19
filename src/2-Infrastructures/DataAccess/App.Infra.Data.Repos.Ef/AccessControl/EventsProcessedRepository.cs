﻿using App.Domain.Core.AccessControl.CosecApi.Data.Repositories;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using App.Infra.Data.Db.SqlServer.Ef.Entities.AccessControl;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.AccessControl;

public class EventsProcessedRepository : IEventsProcessedRepository
{
    #region Fields

    private readonly AppDbContext _context;

    #endregion

    #region Ctor

    public EventsProcessedRepository(AppDbContext context)
    {
        _context = context;
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

        await _context.EventsProcessed.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetLastIndex(CancellationToken cancellationToken)
    {
        var result = await _context.EventsProcessed
            .OrderByDescending(x => x.LastIndexNo)
            .FirstOrDefaultAsync(cancellationToken);

        return result?.LastIndexNo ?? 0;
    }

    #endregion
}