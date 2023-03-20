using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.Services;

namespace App.Domain.AppServices.AccessControl;
public class UserAppService : IUserAppService
{
    #region Fields

    private readonly IUserService _userService;

    #endregion

    #region Ctor

    public UserAppService(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    #region Implementations

    public async Task<List<UserOutputDto>> GetAll(CancellationToken cancellationToken)
        => await _userService.GetAll(cancellationToken);

    public async Task<int> GetCount(CancellationToken cancellationToken)
        => await _userService.GetCount(cancellationToken);

    #endregion
}