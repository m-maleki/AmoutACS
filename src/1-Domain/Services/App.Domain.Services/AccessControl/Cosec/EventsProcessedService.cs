﻿using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Data.Repositories;
using App.Domain.Core.Cosec.Services;

namespace App.Domain.Services.AccessControl.Cosec;

public class EventsProcessedService : IEventsProcessedService
{
    #region Fields

    private readonly IEventsProcessedRepository _eventsProcessedRepository;

    #endregion

    #region Ctor

    public EventsProcessedService(IEventsProcessedRepository eventsProcessedRepository)
    {
        _eventsProcessedRepository = eventsProcessedRepository;
    }

    #endregion

    #region Implementations

    public async Task Add(EventsProcessedDto model, CancellationToken cancellationToken)
        => await _eventsProcessedRepository.Add(model, cancellationToken);

    public async Task<int> GetLastIndex(CancellationToken cancellationToken)
        => await _eventsProcessedRepository.GetLastIndex(cancellationToken);

    #endregion
}