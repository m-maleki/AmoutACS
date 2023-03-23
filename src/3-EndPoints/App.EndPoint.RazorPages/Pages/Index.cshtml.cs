using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoint.RazorPages.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly IEventAppService _eventAppService;
        private readonly IDeviceAppService _deviceAppService;
        private readonly ISyncAppService _syncAppService;

        public int UserCount { get; set; }
        public int EventCount { get; set; }
        public int DeviceCount { get; set; }
        public int ActiveDeviceCount { get; set; }
        public int DeActiveDeviceCount { get; set; }
        public List<UserOutputDto> users { get; set; }
        public List<EventOutputDto> Events { get; set; } = new List<EventOutputDto>();
        public Tuple<string, string> DailyReport { get; set; } = new Tuple<string, string>("", "");


        public IndexModel(IUserAppService userAppService,
            IEventAppService eventAppService,
            IDeviceAppService deviceAppService,
            ISyncAppService syncAppService)
        {
            _userAppService = userAppService;
            _eventAppService = eventAppService;
            _deviceAppService = deviceAppService;
            _syncAppService = syncAppService;
        }

        public async Task OnGet()
        {
            await _syncAppService.SyncEvents(default);
            await _syncAppService.SyncUsers(default);

            UserCount = await _userAppService.GetCount(default);
            EventCount = await _eventAppService.GetTodayEventsCount(default);
            DeviceCount = await _deviceAppService.GetCount(default);
            ActiveDeviceCount = await _deviceAppService.GetActiveDevicesCount(default);
            DeActiveDeviceCount = DeviceCount - ActiveDeviceCount;

            users = await _userAppService.GetAll(default);
            users = users.Take(10).ToList();

            DateTime from = DateTime.Today.AddDays(-3);
            DateTime to = DateTime.Now;

            Events = await _eventAppService.GetAll(from, to, default);
            Events = Events.OrderByDescending(x=>x.EventDateTime).Take(10).ToList();

            DailyReport = await _eventAppService.GetDailyEvent(default);
        }
    }
}