using App.Domain.Core.Cosec.Dtos;
using App.Infra.Data.Db.SqlServer.Ef.Entities.Cosec;
using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Data.Repositories;
public interface IUserAccessRepository : IScopedDependency
{
    Task<List<UserAccessDto>> GetAll(CancellationToken cancellationToken);

    Task<List<UserAccessDto>> GetAllByUserId(int userId,CancellationToken cancellationToken);

}
