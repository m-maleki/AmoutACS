using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.CosecApi.Services;

namespace App.Domain.AppServices.AccessControl;
public class AvailabilityAppService : IAvailabilityAppService
{
    #region Fields

    private readonly IAvailabilityService _availabilityService;

    #endregion

    #region Ctor
    public AvailabilityAppService(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    #endregion

    #region Implementations
    public async Task<bool> AvailableCosecApi(CancellationToken cancellationToken)
        => await _availabilityService.AvailableCosecApi(cancellationToken);

    public async Task<bool> AvailableCosecDatabase(CancellationToken cancellationToken)
        => await _availabilityService.AvailableCosecDatabase(cancellationToken);

    public async Task<bool> AvailableACSDB(CancellationToken cancellationToken)
        => await _availabilityService.AvailableACSDB(cancellationToken);

    #endregion
}
