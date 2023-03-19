using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.Data.Repositories;

public interface IUserRepository : IScopedDependency
{
    Task<List<UserOutputDto>> GetAll(CancellationToken cancellationToken);
}
