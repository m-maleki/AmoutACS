using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Markers;

namespace App.Domain.Core.CosecApi.AppServices;
public interface IUserApiAppService : IScopedDependency
{
    Task AssignUser(string userId, int deviceId, CancellationToken cancellationToken);
    Task RevokeUser(string userId, int deviceId, CancellationToken cancellationToken);
    Task ActiveUser(string userId, CancellationToken cancellationToken);
    Task DeActiveUser(string userId, CancellationToken cancellationToken);
    Task Create(CreateUserDto model, CancellationToken cancellationToken);
    Task<UserChildDto> GetById(string userId, CancellationToken cancellationToken);
    Task DeleteUser(string userId, CancellationToken cancellationToken);
    Task EnrollmentUser(EnrollmentUserDto model, CancellationToken cancellationToken);
}
