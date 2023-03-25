using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.CosecApi.AppServices;
using App.Domain.Core.CosecApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoint.RazorPages.Pages.Access
{
    public class CreateModel : PageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly IDeviceAppService _deviceAppService;
        private readonly IUserApiAppService _userApiAppService;

        public UserOutputDto User { get; set; } = new UserOutputDto();
        public List<SelectListItem> DeviceList { get; set; } = new List<SelectListItem>();
        public int DeviceId { get; set; }
        public int UserId { get; set; }

        public CreateModel(IUserAppService userAppService,
            IDeviceAppService deviceAppService,
            IUserApiAppService userApiAppService)
        {
            _userAppService = userAppService;
            _deviceAppService = deviceAppService;
            _userApiAppService = userApiAppService;
        }


        public async Task OnGet(int userId)
        {
            User = await _userAppService.GetById(userId,default);

            var devices = await _deviceAppService.GetAll(default);

            DeviceList = devices.Select(x => new SelectListItem
            {
                Value = x.DeviceId.ToString(),
                Text = x.Name
            }).ToList();

        }

        public async Task<IActionResult> OnPostEnrollment(int deviceId, int userId, string EnrollmentType, int count)
        {
            var deviceTypeId = await _deviceAppService.GetDeviceTypeWithDeviceId(deviceId, default);

            var model = new EnrollmentUserDto
            {
                EnrollmentType = EnrollmentType,
                DeviceId = deviceId,
                UserId = userId,
                DeviceTypeId = deviceTypeId,
                EnrollmentCount = count
            };

            await _userApiAppService.EnrollmentUser(model, default);
            return RedirectToAction("OnGet");
        }
    }
}
