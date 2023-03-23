using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.CosecApi.Services;
using Newtonsoft.Json;
using System.Threading;
using App.Domain.Core.Cosec.Data.Repositories;
using System.Net;
using Framework.Core.Configs;
using System.Globalization;
using System;
using System.Net.Sockets;
using Azure;

namespace App.Domain.Services.AccessControl.Cosec;
public class AvailabilityService : IAvailabilityService
{
    #region Fields

    private readonly SiteSettings _siteSettings;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly ICosecApiService _cosecApiService;

    #endregion

    #region Ctor

    public AvailabilityService(IAvailabilityRepository availabilityRepository,
        SiteSettings siteSettings,
        IHttpClientFactory clientFactory,
        ICosecApiService cosecApiService)
    {
        _availabilityRepository = availabilityRepository;
        _siteSettings = siteSettings;
        _clientFactory = clientFactory;
        _cosecApiService = cosecApiService;
    }

    #endregion

    #region Implementations

    public async Task<bool> AvailableCosecApi(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _cosecApiService.CallApi("", cancellationToken);

            if (response.Contains("License Key not found"))
                return false;
            else
                return true;

        }
        catch (Exception ex) 
        {
            return false;
        }
    }

    public async Task<bool> AvailableCosecDatabase(CancellationToken cancellationToken)
        => await _availabilityRepository.AvailableCosecDatabase(cancellationToken);

    public async Task<bool> AvailableACSDB(CancellationToken cancellationToken)
        => await _availabilityRepository.AvailableAcsDatabase(cancellationToken);

    #endregion
}

