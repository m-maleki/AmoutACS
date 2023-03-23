using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.CosecApi.AppServices;
public interface IUserApiAppService : IScopedDependency
{
    Task AssignUser(int userId, int deviceId, CancellationToken cancellationToken);
    Task RevokeUser(int userId, int deviceId, CancellationToken cancellationToken);
    Task ActiveUser(int userId, CancellationToken cancellationToken);
    Task DeActiveUser(int userId, CancellationToken cancellationToken);
    Task Create(CreateUserDto model, CancellationToken cancellationToken);
    Task<UserChildDto> GetById(int userId, CancellationToken cancellationToken);
    Task DeleteUser(int userId, CancellationToken cancellationToken);
    Task EnrollmentUser(EnrollmentUserDto model, CancellationToken cancellationToken);
}
