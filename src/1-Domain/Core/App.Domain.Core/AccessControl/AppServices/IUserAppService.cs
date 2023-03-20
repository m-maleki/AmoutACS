using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.AppServices;
public interface IUserAppService : IScopedDependency
{
    Task<List<UserOutputDto>> GetAll(CancellationToken cancellationToken);
    Task<int> GetCount(CancellationToken cancellationToken);
}
