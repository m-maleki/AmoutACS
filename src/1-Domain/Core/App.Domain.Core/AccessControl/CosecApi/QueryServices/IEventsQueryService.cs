using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.QueryServices;

public interface IEventsQueryService : IScopedDependency
{
    Task BulkInsert(EventDto model, CancellationToken cancellationToken);
}
