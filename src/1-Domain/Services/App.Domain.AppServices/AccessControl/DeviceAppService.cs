using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Services;
using App.Domain.Core.CosecApi.Dtos;

namespace App.Domain.AppServices.AccessControl;
public class DeviceAppService : IDeviceAppService
{
    #region Fields

    private readonly IDeviceService _deviceService;
    private readonly IUserAccessService _userAccessService;

    #endregion

    #region Ctor
    public DeviceAppService(IDeviceService deviceService,
        IUserAccessService userAccessService)
    {
        _deviceService = deviceService;
        _userAccessService = userAccessService;
    }

    #endregion

    #region Implementations

    public async Task<int> GetCount(CancellationToken cancellationToken)
        => await _deviceService.GetCount(cancellationToken);

    public async Task<int> GetActiveDevicesCount(CancellationToken cancellationToken)
        => await _deviceService.GetActiveDevicesCount(cancellationToken);

    public async Task<List<DeviceOutputDto>> getDevicesWithUserAccess(string userId,CancellationToken cancellationToken)
    {
        var devices = await _deviceService.GetAll(cancellationToken);
        var AccessUsers = await _userAccessService.GetAllByUserId(userId, cancellationToken);

        foreach (var item in devices)
        {
            var record = AccessUsers
                .Where(x => x.UserId == userId.ToString())
                .Where(x => x.MID == item.DeviceId)
                .OrderByDescending(x => x.EventDT)
                .FirstOrDefault();

            if (record != null)
                if (record.Action == 1)
                    item.UserAccess = true;
                else
                    item.UserAccess = false;
        }
        return devices;
    }

    public async Task<List<DeviceOutputDto>> GetAll(CancellationToken cancellationToken)
        => await _deviceService.GetAll(cancellationToken);

    public async Task<int> GetDeviceTypeWithDeviceId(int deviceId, CancellationToken cancellationToken)
        => await _deviceService.GetDeviceTypeWithDeviceId(deviceId, cancellationToken);

    #endregion
}
