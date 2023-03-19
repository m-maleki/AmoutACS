using App.Domain.Core.AccessControl.CosecApi.Data.Repositories;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.Services;

namespace App.Domain.Services.AccessControl.CosecApi;

public class UserService : IUserService
{
    #region Fields

    private readonly IUserRepository _userRepository;

    #endregion

    #region Ctor

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    #region Implementations

    public async Task<List<UserOutputDto>> GetAll(CancellationToken cancellationToken)
        => await _userRepository.GetAll(cancellationToken);

    #endregion
}

