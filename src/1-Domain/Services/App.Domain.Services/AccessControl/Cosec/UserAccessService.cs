using App.Domain.Core.Cosec.Data.Repositories;
using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.Cosec.Services;

namespace App.Domain.Services.AccessControl.Cosec;
public class UserAccessService : IUserAccessService
{
    #region Fields

    private readonly IUserAccessRepository _userAccessRepository;

    #endregion

    #region Ctor
    public UserAccessService(IUserAccessRepository repository)
    {
        _userAccessRepository = repository;
    }

    #endregion

    #region Implementations

    public async Task<List<UserAccessDto>> GetAllByUserId(string userId, CancellationToken cancellationToken)
        => await _userAccessRepository.GetAllByUserId(userId, cancellationToken);

    #endregion
}
