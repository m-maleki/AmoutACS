using App.Domain.Core._Providers.Scheduler.Service;
using Framework.Core.Configs;
using Framework.Core.Utilities;
using System.Text;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.QueryServices;
using App.Domain.Core.AccessControl.CosecApi.Services;
using Newtonsoft.Json;

namespace App.Domain.AppServices.AccessControl.CosecApi;
public class SyncAppService : ISyncAppService
{
    #region Fields

    private readonly ISchedulerProvider _schedulerProvider;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IEventsProcessedService _eventsProcessedService;
    private readonly IEventsQueryServices _eventsQueryService;
    private readonly IUserService _userService;
    private readonly IUserQueryServices _userQueryServices;
    private readonly IDeviceService _deviceService;
    private readonly IDeviceQueryServices _deviceQueryServices;
    #endregion

    #region Ctor

    public SyncAppService(ISchedulerProvider schedulerProvider,
        IHttpClientFactory clientFactory,
        IEventsProcessedService eventsProcessedService,
        IEventsQueryServices eventsQueryService,
        IUserService userService,
        IUserQueryServices userQueryServices, 
        IDeviceService deviceService,
        IDeviceQueryServices deviceQueryServices)
    {
        _schedulerProvider = schedulerProvider;
        _clientFactory = clientFactory;
        _eventsProcessedService = eventsProcessedService;
        _eventsQueryService = eventsQueryService;
        _userService = userService;
        _userQueryServices = userQueryServices;
        _deviceService = deviceService;
        _deviceQueryServices = deviceQueryServices;
    }

    #endregion

    #region Implementations

    public void AddRecurringJobs(SiteSettings siteSettings)
    {
        if (CanAddJobUtility.CanAddJob(1, siteSettings.HangfireJobs))
            _schedulerProvider.AddRecurringJob<ISyncAppService>(CanAddJobUtility.GetJobName(1, siteSettings.HangfireJobs), "default", siteSettings.SyncConfig.SyncEventsCron, action => action.SyncEvents(default));
        if (CanAddJobUtility.CanAddJob(2, siteSettings.HangfireJobs))
            _schedulerProvider.AddRecurringJob<ISyncAppService>(CanAddJobUtility.GetJobName(2, siteSettings.HangfireJobs), "default", siteSettings.SyncConfig.SyncUsersCron, action => action.SyncUsers(default));
        if (CanAddJobUtility.CanAddJob(3, siteSettings.HangfireJobs))
            _schedulerProvider.AddRecurringJob<ISyncAppService>(CanAddJobUtility.GetJobName(3, siteSettings.HangfireJobs), "default", siteSettings.SyncConfig.SyncDevicesCron, action => action.SyncDevices(default));

    }

    public async Task SyncEvents(CancellationToken cancellationToken)
    {
        var lastIndex = await _eventsProcessedService.GetLastIndex(cancellationToken);

        HttpContent httpContent = new StringContent("application/json");
        var httpResult = await _clientFactory
            .CreateClient("CosecAPI")
            .PostAsync($"event-ta?action=get;format=json;index={lastIndex+1}",
                httpContent, cancellationToken);

        var response = await httpResult.Content.ReadAsStringAsync(cancellationToken);
    
        if (!response.Contains("No records found"))
        {
            var responseModel = JsonConvert.DeserializeObject<EventDto>(response);

            if (responseModel.Events.Any())
            {
                await _eventsQueryService.BulkInsert(responseModel, cancellationToken);

                var processedItems = new EventsProcessedDto
                {
                    LastIndexNo = int.Parse(responseModel.Events.Last().indexno),
                    ProcessedCount = responseModel.Events.Count
                };

                await _eventsProcessedService.Add(processedItems, cancellationToken);
            }
        }

    }

    public async Task SyncUsers(CancellationToken cancellationToken)
    {
        var result = new List<UserChildDto>();

        HttpContent httpContent = new StringContent("application/json");
        var httpResult = await _clientFactory
            .CreateClient("CosecAPI")
            .PostAsync($"user?action=get;range=all;format=json",
                httpContent, cancellationToken);

        var response = await httpResult.Content.ReadAsStringAsync(cancellationToken);

        if (!response.Contains("No records found"))
        {
            await _userService.DeleteAll(cancellationToken);
            var usersInApi = JsonConvert.DeserializeObject<UserDto>(response).Users;

            if (usersInApi.Any())
                await _userQueryServices.BulkInsert(usersInApi, cancellationToken);
        }
    }

    public async Task SyncDevices(CancellationToken cancellationToken)
    {
        var result = new List<UserChildDto>();

        HttpContent httpContent = new StringContent("application/json");
        var httpResult = await _clientFactory
            .CreateClient("CosecAPI")
            .PostAsync($"device?action=list;format=json",
                httpContent, cancellationToken);

        var response = await httpResult.Content.ReadAsStringAsync(cancellationToken);

        if (!response.Contains("No records found"))
        {
            var Devices = JsonConvert.DeserializeObject<DeviceDto>(response).Devices;
            await _deviceService.DeleteAll(cancellationToken);
            await _deviceQueryServices.BulkInsert(Devices,cancellationToken);
        }
    }

    #endregion
}