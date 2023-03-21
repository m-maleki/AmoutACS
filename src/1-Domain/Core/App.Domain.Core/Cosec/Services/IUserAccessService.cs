using App.Domain.Core.Cosec.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Services;
public interface IUserAccessService : IScopedDependency
{
    Task<List<UserAccessDto>> GetAllByUserId(int userId, CancellationToken cancellationToken);
}
