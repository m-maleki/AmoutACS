using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Data.Repositories;

public interface IEventsProcessedRepository : IScopedDependency
{
    Task Add(EventsProcessedDto model, CancellationToken cancellationToken);
    Task<int> GetLastIndex(CancellationToken cancellationToken);
}
