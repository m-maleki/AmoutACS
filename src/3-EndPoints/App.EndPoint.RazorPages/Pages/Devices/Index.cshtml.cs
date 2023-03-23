using App.Domain.AppServices.AccessControl.CosecApi;
using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.Cosec.Enums;
using App.Domain.Core.CosecApi.AppServices;
using App.Domain.Core.CosecApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoint.RazorPages.Pages.Devices
{
    public class IndexModel : PageModel
    {
        public bool ShowMessage { get; set; } = false;

        private readonly IDeviceAppService _deviceAppService;
        private readonly IDeviceApiAppService _deviceApiAppService;
        private readonly ISyncAppService _syncAppService;

        public IndexModel(IDeviceAppService deviceAppService,
            IDeviceApiAppService deviceApiAppService,
            ISyncAppService syncAppService)
        {
            _deviceAppService = deviceAppService;
            _deviceApiAppService = deviceApiAppService;
            _syncAppService = syncAppService;
        }

        public List<DeviceOutputDto> Devices { get; set; } = new List<DeviceOutputDto>();

        public async Task OnGet(bool showMessage)
        {
            await _syncAppService.SyncDevices(default);
            Devices = await _deviceAppService.GetAll(default);
            ShowMessage = showMessage;
        }

        public async Task<IActionResult> OnGetDelete(int deviceId, int deviceTypeId)
        {
            var model = new DeleteDeviceDto
            {
                DeviceId = deviceId,
                DeviceTypeId = deviceTypeId,
            };

            await _deviceApiAppService.Delete(model, default);
            await _syncAppService.SyncDevices(default);

            return RedirectToAction("OnGet", new { showMessage = true });
        }


        public async Task<IActionResult> OnPostCreate(int id, string name, int devicetype, string mac)
        {
            var model = new CreateDeviceDto
            {
                DeviceId = id,
                DeviceName = name,
                DeviceTypeId = devicetype,
                MacAddress = mac
            };

            await _deviceApiAppService.Create(model, default);
            await _syncAppService.SyncDevices(default);

            return RedirectToAction("OnGet", new { showMessage = true });
        }

        public async Task<IActionResult> OnGetOpenDoor(int deviceId, int deviceTypeId)
        {
            await _deviceApiAppService.OpenDoor(deviceId, deviceTypeId, default);
            return RedirectToAction("OnGet", new { showMessage = true });
        }

        public async Task<IActionResult> OnGetLockDoor(int deviceId, int deviceTypeId)
        {
            await _deviceApiAppService.LockDoor(deviceId, deviceTypeId, default);
            return RedirectToAction("OnGet", new { showMessage = true });
        }

        public async Task<IActionResult> OnGetUnLockDoor(int deviceId, int deviceTypeId)
        {
            await _deviceApiAppService.UnLockDoor(deviceId, deviceTypeId, default);
            return RedirectToAction("OnGet", new { showMessage = true });
        }

        public async Task<IActionResult> OnGetNormalizeDoor(int deviceId, int deviceTypeId)
        {
            await _deviceApiAppService.NormalizeDoor(deviceId, deviceTypeId, default);
            return RedirectToAction("OnGet", new { showMessage = true });
        }
    }
}
