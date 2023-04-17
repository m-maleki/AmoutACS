using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.CosecApi.AppServices;
using App.Domain.Core.CosecApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoint.RazorPages.Pages.Access
{
    public class IndexModel : PageModel
    {
        public List<DeviceOutputDto> UserDevices { get; set; }
        public UserOutputDto? User { get; set; }
        public bool ShowMessage { get; set; } = false;

        private readonly IDeviceAppService _deviceAppService;
        private readonly IUserAppService _userAppService;
        private readonly IUserApiAppService _userApiAppService;

        public IndexModel(IDeviceAppService deviceAppService,
            ISyncAppService syncAppService,
            IUserApiAppService userApiAppService, IUserAppService userAppService)
        {
            _deviceAppService = deviceAppService;
            _userApiAppService = userApiAppService;
            _userAppService = userAppService;
        }

        public async Task OnGet(string id, bool showMessage)
        {
            UserDevices = await _deviceAppService.getDevicesWithUserAccess(id, default);
            User = await _userAppService.GetById(id, default);
            ShowMessage = showMessage;
        }

        public async Task<IActionResult> OnPostAssign(string userId, int DeviceId)
        {
            await _userApiAppService.AssignUser(userId, DeviceId, default);
            return RedirectToAction("OnGet", new { id = userId, showMessage = true });
        }

        public async Task<IActionResult> OnPostRevoke(string userId, int DeviceId)
        {
            await _userApiAppService.RevokeUser(userId, DeviceId, default);
            return RedirectToAction("OnGet", new { id = userId, showMessage = true });
        }

    }
}
