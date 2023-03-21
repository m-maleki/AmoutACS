using App.Domain.Core.Cosec.Data.Repositories;
using App.Domain.Core.Cosec.Services;
using App.Domain.Core.CosecApi.Dtos;

namespace App.Domain.Services.AccessControl.Cosec;
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

    public async Task<List<DeviceOutputDto>> GetAll(CancellationToken cancellationToken)
        => await _deviceRepository.GetAll(cancellationToken);

    public async Task<int> GetDeviceTypeWithDeviceId(int deviceId, CancellationToken cancellationToken)
        => await _deviceRepository.GetDeviceTypeWithDeviceId(deviceId, cancellationToken);

    #endregion
}

