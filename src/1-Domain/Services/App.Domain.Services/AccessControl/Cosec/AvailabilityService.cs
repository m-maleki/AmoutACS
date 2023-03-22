using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.CosecApi.Services;
using Newtonsoft.Json;
using System.Threading;
using App.Domain.Core.Cosec.Data.Repositories;

namespace App.Domain.Services.AccessControl.Cosec;
public class AvailabilityService : IAvailabilityService
{
    #region Fields

    private readonly ICosecApiService _cosecApiService;
    private readonly IAvailabilityRepository _availabilityRepository;

    #endregion

    #region Ctor

    public AvailabilityService(ICosecApiService cosecApiService,
        IAvailabilityRepository availabilityRepository)
    {
        _cosecApiService = cosecApiService;
        _availabilityRepository = availabilityRepository;
    }

    #endregion

    #region Implementations

    public async Task<bool> AvailableCosecApi(CancellationToken cancellationToken)
    {
        var response = await _cosecApiService.CallApi("", cancellationToken);
        return response.Contains("Hello API User");
    }

    public async Task<bool> AvailableCosecDatabase(CancellationToken cancellationToken)
        => await _availabilityRepository.AvailableCosecDatabase(cancellationToken);

    public async Task<bool> AvailableACSDB(CancellationToken cancellationToken)
        => await _availabilityRepository.AvailableAcsDatabase(cancellationToken);

    #endregion
}

