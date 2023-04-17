using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Dtos;
using App.Domain.Core.CosecApi.AppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoint.RazorPages.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly IUserApiAppService _userApiAppService;

        public bool ShowMessage { get; set; } = false;
        public List<UserOutputDto> users { get; set; }

        public IndexModel(IUserAppService userAppService,
            IUserApiAppService userApiAppService)
        {
            _userAppService = userAppService;
            _userApiAppService = userApiAppService;
        }

        public async Task OnGet(bool showMessage)
        {
            ShowMessage = showMessage;
            users = await _userAppService.GetAll(default);
        }

        public async Task<IActionResult> OnPostActive(string id)
        {
            await _userApiAppService.ActiveUser(id, default);
            return RedirectToAction("OnGet", new { showMessage = true});
        }

        public async Task<IActionResult> OnPostDeActive(string id)
        {
            await _userApiAppService.DeActiveUser(id, default);
            return RedirectToAction("OnGet", new { showMessage = true });

        }
        public async Task<IActionResult> OnPostCreate(string id, string name, string family)
        {
            var model = new CreateUserDto
            {
                Id = id,
                ShortName = name,
                FullName = family
            };

            await _userApiAppService.Create(model, default);
            return RedirectToAction("OnGet", new { showMessage = true });
        }

        public async Task<IActionResult> OnGetDelete(string id)
        {
            await _userApiAppService.DeleteUser(id, default);
            return RedirectToAction("OnGet", new { showMessage = true });
        }

    }
}
