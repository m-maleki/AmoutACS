using App.Domain.Core.AccessControl.CosecApi.Data.Repositories;
using App.Domain.Core.AccessControl.CosecApi.Services;

namespace App.Domain.Services.AccessControl.CosecApi;
public class DeviceService : IDeviceService
{
    #region Fields

    private readonly IDeviceRepository _deviceRepository;

    #endregion

    #region Ctor
    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    #endregion

    #region Implementations

    public async Task DeleteAll(CancellationToken cancellationToken)
        => await _deviceRepository.DeleteAll(cancellationToken);

    public async Task<int> GetCount(CancellationToken cancellationToken)
        => await _deviceRepository.GetCount(cancellationToken);

    public async Task<int> GetActiveDevicesCount(CancellationToken cancellationToken)
        => await _deviceRepository.GetActiveDevicesCount(cancellationToken);

    #endregion
}

