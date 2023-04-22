using App.Domain.Core._Providers.Scheduler.Service;
using Framework.Core.Configs;
using Framework.Core.Utilities;
using System.Text;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.QueryServices;
using Newtonsoft.Json;
using App.Domain.Core.Cosec.Services;
using App.Domain.Core.CosecApi.Services;
using System.Threading;

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
    private readonly ICosecApiService _cosecApiService;
    #endregion

    #region Ctor

    public SyncAppService(ISchedulerProvider schedulerProvider,
        IHttpClientFactory clientFactory,
        IEventsProcessedService eventsProcessedService,
        IEventsQueryServices eventsQueryService,
        IUserService userService,
        IUserQueryServices userQueryServices, 
        IDeviceService deviceService,
        IDeviceQueryServices deviceQueryServices, ICosecApiService cosecApiService)
    {
        _schedulerProvider = schedulerProvider;
        _clientFactory = clientFactory;
        _eventsProcessedService = eventsProcessedService;
        _eventsQueryService = eventsQueryService;
        _userService = userService;
        _userQueryServices = userQueryServices;
        _deviceService = deviceService;
        _deviceQueryServices = deviceQueryServices;
        _cosecApiService = cosecApiService;
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

        var url = $"event-ta?action=get;format=json;index={lastIndex + 1}";

        var response = await _cosecApiService.CallApi(url, cancellationToken);

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
        const string url = $"user?action=get;range=all;format=json";

        var response = await _cosecApiService.CallApi(url, cancellationToken);

        if (!response.Contains("No records found"))
        {
            await _userService.DeleteAll(cancellationToken);
            Thread.Sleep(100);
            var usersInApi = JsonConvert.DeserializeObject<UserDto>(response).Users;

            if (usersInApi.Any())
                await _userQueryServices.BulkInsert(usersInApi, cancellationToken);
        }
    }

    public async Task ReSyncUser(string userId,CancellationToken cancellationToken)
    {
        var url = $"user?action=get;id={userId};format=json";

        var response = await _cosecApiService.CallApi(url, cancellationToken);

        if (!response.Contains("does not exist"))
        {
            var user = JsonConvert.DeserializeObject<UserDto>(response).Users.FirstOrDefault();

            if (user != null)
                await _userService.Update(user, cancellationToken);
        }
    }

    public async Task SyncDevices(CancellationToken cancellationToken)
    {
        const string url = $"device?action=list;format=json";

        var response = await _cosecApiService.CallApi(url, cancellationToken);

        if (!response.Contains("No records found"))
        {
            var devices = JsonConvert.DeserializeObject<DeviceDto>(response).Devices;

            //foreach (var device in devices)
            //{
            //    if (device.DeviceType == "16")
            //        device.DeviceType = "6";
            //}

            await _deviceService.DeleteAll(cancellationToken);
            Thread.Sleep(100);
            await _deviceQueryServices.BulkInsert(devices, cancellationToken);
        }
    }

    public async Task<UserChildDto?> GetUser(string userId, CancellationToken cancellationToken)
    {
        var url = $"user?action=get;id={userId};format=json";

        var response = await _cosecApiService.CallApi(url, cancellationToken);

        if (!response.Contains("does not exist"))
        {
            var user = JsonConvert.DeserializeObject<UserDto>(response).Users.FirstOrDefault();
            return user;
        }
        return new UserChildDto();
    }

    #endregion
}