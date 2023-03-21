using App.Domain.Core.CosecApi.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.Cosec.Services;
using App.Domain.Core.CosecApi.Services;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Newtonsoft.Json;
using System.Threading;

namespace App.Domain.AppServices.AccessControl.CosecApi;
public class UserApiAppService : IUserApiAppService
{
    #region Fields

    private readonly IDeviceService _deviceService;
    private readonly ICosecApiService _cosecApiService;
    private readonly ISyncAppService _syncAppService;
    private readonly IUserService _userService;

    #endregion

    #region Ctor
    public UserApiAppService(IDeviceService deviceService,
        ICosecApiService cosecApiService,
        ISyncAppService syncAppService,
        IUserService userService)
    {
        _deviceService = deviceService;
        _cosecApiService = cosecApiService;
        _syncAppService = syncAppService;
        _userService = userService;
    }

    #endregion

    #region Implementations

    public async Task AssignUser(int userId, int deviceId, CancellationToken cancellationToken)
    {
        var deviceType = await _deviceService.GetDeviceTypeWithDeviceId(deviceId, cancellationToken);

        var url = $"device?action=assign;device={addDeviceType(deviceType)}{deviceId};id={userId};";

        await _cosecApiService.CallApi(url, cancellationToken);
    }

    public async Task RevokeUser(int userId, int deviceId, CancellationToken cancellationToken)
    {
        var deviceType = await _deviceService.GetDeviceTypeWithDeviceId(deviceId, cancellationToken);

        var url = $"device?action=revoke;device={addDeviceType(deviceType)}{deviceId};id={userId};";

        await _cosecApiService.CallApi(url, cancellationToken);
    }

    public async Task ActiveUser(int userId, CancellationToken cancellationToken)
    {
        var url = $"user?action=set;id={userId};active=1";

        await _cosecApiService.CallApi(url, cancellationToken);

        await _syncAppService.ReSyncUser(userId, cancellationToken);
    }

    public async Task DeActiveUser(int userId, CancellationToken cancellationToken)
    {
        var url = $"user?action=set;id={userId};active=0";

        await _cosecApiService.CallApi(url, cancellationToken);

        await _syncAppService.ReSyncUser(userId, cancellationToken);
    }

    public async Task Create(CreateUserDto model, CancellationToken cancellationToken)
    {

        var url = $"user?action=set;id={model.Id};name={model.ShortName}" +
                  $";short-name={model.ShortName};full-name={model.ShortName} {model.FullName};";

        var response = await _cosecApiService.CallApi(url, cancellationToken);

        if (response.Contains("saved successfully"))
        {
           var user = await GetById(model.Id, cancellationToken);
           await _userService.Add(user, cancellationToken);
        }
    }

    public async Task<UserChildDto> GetById(int userId, CancellationToken cancellationToken)
    {
        var url = $"user?action=get;id={userId};format=json";

        var response = await _cosecApiService.CallApi(url, cancellationToken);

        if (!response.Contains("does not exist"))
        {
            return JsonConvert.DeserializeObject<UserDto>(response).Users.FirstOrDefault()!;
        }

        return new UserChildDto();
    }

    public async Task DeleteUser(int userId, CancellationToken cancellationToken)
    {
        var url = $"user?action=delete;id={userId}";

        await _cosecApiService.CallApi(url, cancellationToken);

        await _userService.Delete(userId, cancellationToken);
    }

    #endregion

    #region PrivateMethods

    private string addDeviceType(int deviceTypeId)
    {
        switch (deviceTypeId)
        {
            case 0:
                return "p_";
            case 6:
                return "d_";
        }

        return deviceTypeId.ToString();
    }

    private async Task<int> GetLastId(CancellationToken cancellationToken)
    {
        var url = $"user?action=get;field-name=id;format=json;";

        var response = await _cosecApiService.CallApi(url, cancellationToken);

        if (!response.Contains("does not exist"))
        {
            var id = JsonConvert.DeserializeObject<UserDto>(response).Users.FirstOrDefault()?.id;
            if (id != null) return int.Parse(id);
        }
        return 0;
    }

    #endregion
}