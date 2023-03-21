using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Services;

public interface IEventsProcessedService : IScopedDependency
{
    Task Add(EventsProcessedDto model, CancellationToken cancellationToken);
    Task<int> GetLastIndex(CancellationToken cancellationToken);
}