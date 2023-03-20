using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoint.RazorPages.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserAppService _userAppService;

        public List<UserOutputDto> users { get; set; }

        public IndexModel(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task OnGet()
        {
            users = await _userAppService.GetAll(default);
        }
    }
}
