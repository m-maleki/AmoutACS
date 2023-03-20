using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.AccessControl.CosecApi.Services;

public interface IUserService : IScopedDependency
{
    Task<List<UserOutputDto>> GetAll(CancellationToken cancellationToken);
    Task DeleteAll(CancellationToken cancellationToken);
}

