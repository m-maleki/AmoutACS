using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Data.Repositories;

public interface IUserRepository : IScopedDependency
{
    Task<List<UserOutputDto>> GetAll(CancellationToken cancellationToken);
    Task DeleteAll(CancellationToken cancellationToken);
    Task<int> GetCount(CancellationToken cancellationToken);
    Task<UserOutputDto?> GetById(int id, CancellationToken cancellationToken);
    Task Update(UserChildDto model , CancellationToken cancellationToken);
    Task Add(UserChildDto model, CancellationToken cancellationToken);
    Task Delete(int userId , CancellationToken cancellationToken);
}
