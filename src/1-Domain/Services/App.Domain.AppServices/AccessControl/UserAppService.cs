using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Services;
using Newtonsoft.Json;
using System.Threading;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.CosecApi.Services;
using App.Domain.Core.Cosec.Enums;
using App.Domain.AppServices.AccessControl.CosecApi;
using App.Domain.Core.Cosec.Dtos;

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

    public async Task<UserOutputDto?> GetById(string id, CancellationToken cancellationToken)
        => await _userService.GetById(id, cancellationToken);
    
    #endregion

}