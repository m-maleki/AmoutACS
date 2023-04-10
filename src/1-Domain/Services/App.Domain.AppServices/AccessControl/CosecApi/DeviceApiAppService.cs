using App.Domain.Core.CosecApi.AppServices;
using App.Domain.Core.CosecApi.Dtos;
using App.Domain.Core.CosecApi.Services;

namespace App.Domain.AppServices.AccessControl.CosecApi;
public class DeviceApiAppService : IDeviceApiAppService
{
    #region Fields

    private readonly ICosecApiService _cosecApiService;

    #endregion

    #region Ctor
    public DeviceApiAppService(ICosecApiService cosecApiService)
    {
        _cosecApiService = cosecApiService;
    }

    #endregion

    #region Implementations

    public async Task Create(CreateDeviceDto model, CancellationToken cancellationToken)
    {
        var url = $"device?action=set;name={model.DeviceName};device-id={model.DeviceId}" +
                  $";device-type={model.DeviceTypeId};mac={model.MacAddress};";
      var tt=  await _cosecApiService.CallApi(url, cancellationToken);
    }

    public async Task Delete(DeleteDeviceDto model, CancellationToken cancellationToken)
    {
        var url = $"device?action=delete;device-id={model.DeviceId};device-type={model.DeviceTypeId};";
        await _cosecApiService.CallApi(url, cancellationToken);
    }

    public async Task OpenDoor(int deviceId, int deviceType, CancellationToken cancellationToken)
    {
        var url = $"device-commands?action=command;device-id={deviceId};command-type=10;device-type={deviceType};";
        await _cosecApiService.CallApi(url, cancellationToken);
    }

    public async Task LockDoor(int deviceId, int deviceType, CancellationToken cancellationToken)
    {
        var url = $"device-commands?action=command;device-id={deviceId};command-type=3;device-type={deviceType};";
        await _cosecApiService.CallApi(url, cancellationToken);
    }

    public async Task UnLockDoor(int deviceId, int deviceType, CancellationToken cancellationToken)
    {
        var url = $"device-commands?action=command;device-id={deviceId};command-type=4;device-type={deviceType};";
        await _cosecApiService.CallApi(url, cancellationToken);
    }

    public async Task NormalizeDoor(int deviceId, int deviceType, CancellationToken cancellationToken)
    {
        var url = $"device-commands?action=command;device-id={deviceId};command-type=2;device-type={deviceType};";
        await _cosecApiService.CallApi(url, cancellationToken);
    }

    public async Task SyncCredentials(int deviceId, int deviceType, CancellationToken cancellationToken)
    {
        var url = $"device-commands?action=command;device-id={deviceId};command-type=6;device-type={deviceType};";
        await _cosecApiService.CallApi(url, cancellationToken);
    }



    #endregion

}

