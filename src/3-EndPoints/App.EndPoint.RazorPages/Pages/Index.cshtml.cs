using App.Domain.Core.AccessControl.AppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoint.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly IEventAppService _eventAppService;
        private readonly IDeviceAppService _deviceAppService;

        public int UserCount { get; set; }
        public int EventCount { get; set; }
        public int DeviceCount { get; set; }
        public int ActiveDeviceCount { get; set; }
        public int DeActiveDeviceCount { get; set; }



        public IndexModel(IUserAppService userAppService,
            IEventAppService eventAppService,
            IDeviceAppService deviceAppService)
        {
            _userAppService = userAppService;
            _eventAppService = eventAppService;
            _deviceAppService = deviceAppService;
        }

        public async Task OnGet()
        {
            UserCount = await _userAppService.GetCount(default);
            EventCount = await _eventAppService.GetTodayEventsCount(default);
            DeviceCount = await _deviceAppService.GetCount(default);
            ActiveDeviceCount = await _deviceAppService.GetActiveDevicesCount(default);
            DeActiveDeviceCount = DeviceCount - ActiveDeviceCount;

        }
    }
}