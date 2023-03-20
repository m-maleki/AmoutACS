using App.Domain.Core._Providers.Scheduler.Service;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using Framework.Core.Configs;
using Framework.Core.Utilities;
using System.Text;
using Newtonsoft.Json;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.QueryServices;
using App.Domain.Core.AccessControl.CosecApi.Services;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.AppServices.AccessControl.CosecApi;

public class SyncAppService : ISyncAppService
{
    #region Fields

    private readonly ISchedulerProvider _schedulerProvider;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IEventsProcessedService _eventsProcessedService;
    private readonly IEventsQueryService _eventsQueryService;
    private readonly IUserService _userService;
    private readonly IUserQueryServices _userQueryServices;
    #endregion

    #region Ctor

    public SyncAppService(ISchedulerProvider schedulerProvider,
        IHttpClientFactory clientFactory,
        IEventsProcessedService eventsProcessedService,
        IEventsQueryService eventsQueryService,
        IUserService userService,
        IUserQueryServices userQueryServices)
    {
        _schedulerProvider = schedulerProvider;
        _clientFactory = clientFactory;
        _eventsProcessedService = eventsProcessedService;
        _eventsQueryService = eventsQueryService;
        _userService = userService;
        _userQueryServices = userQueryServices;
    }

    #endregion

    #region Implementations

    public void AddRecurringJobs(SiteSettings siteSettings)
    {
        if (CanAddJobUtility.CanAddJob(1, siteSettings.HangfireJobs))
            _schedulerProvider.AddRecurringJob<ISyncAppService>(CanAddJobUtility.GetJobName(1, siteSettings.HangfireJobs), "default", siteSettings.SyncConfig.SyncEventsCron, action => action.SyncEvents(default));
        if (CanAddJobUtility.CanAddJob(2, siteSettings.HangfireJobs))
            _schedulerProvider.AddRecurringJob<ISyncAppService>(CanAddJobUtility.GetJobName(2, siteSettings.HangfireJobs), "default", siteSettings.SyncConfig.SyncUsersCron, action => action.SyncUsers(default));
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

    #endregion
}