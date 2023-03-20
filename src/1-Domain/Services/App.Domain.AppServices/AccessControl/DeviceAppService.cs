using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Services;

namespace App.Domain.AppServices.AccessControl;
public class DeviceAppService : IDeviceAppService
{
    #region Fields

    private readonly IDeviceService _deviceService;

    #endregion

    #region Ctor
    public DeviceAppService(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    #endregion

    #region Implementations

    public async Task<int> GetCount(CancellationToken cancellationToken)
        => await _deviceService.GetCount(cancellationToken);

    public async Task<int> GetActiveDevicesCount(CancellationToken cancellationToken)
        => await _deviceService.GetActiveDevicesCount(cancellationToken);

    #endregion
}
