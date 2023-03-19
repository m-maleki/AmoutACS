using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.QueryServices;

public interface IUserQueryServices : IScopedDependency
{
    Task BulkInsert(List<UserChildDto> model, CancellationToken cancellationToken);

}
