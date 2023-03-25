using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.CosecApi.AppServices;
using App.Domain.Core.CosecApi.Dtos;
using Framework.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace App.EndPoint.RazorPages.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly IEventAppService _eventAppService;
        private readonly ISyncAppService _syncAppService;
        private readonly IUserAppService _userAppService;

        public IndexModel(IEventAppService eventAppService,
            ISyncAppService syncAppService,
            IUserAppService userAppService)
        {
            _eventAppService = eventAppService;
            _syncAppService = syncAppService;
            _userAppService = userAppService;
            FromDate = DateTime.Now.ToPersianString("yyyy/MM/dd").ToString();
            ToDate = FromDate = DateTime.Now.ToPersianString("yyyy/MM/dd").ToString();
        }


        public List<EventOutputDto> Events { get; set; } = new List<EventOutputDto>();
        public List<SelectListItem> UserList { get; set; } = new List<SelectListItem>();
        public string FromDate { get; set; } 
        public string ToDate { get; set; } 
        public int userId { get; set; }

        public async Task OnGet(int userId, string fromDate, string toDate)
        {
            await _syncAppService.SyncEvents(default);

            var users = await _userAppService.GetAll(default);

            UserList = users.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            Events = await _eventAppService.Search(userId, fromDate, toDate, default);

            if (userId != 0 & fromDate == null && toDate == null)
            {
                FromDate = CosecDate.DateFormatString(DateTime.Now.AddDays(-30).ToPersianString());
            }

            if (fromDate != null && toDate != null)
            {
                FromDate = CosecDate.DateFormatString(fromDate);
                ToDate = CosecDate.DateFormatString(toDate);
            }
        }

        public async Task<IActionResult> OnPostSearch(int userId, string fromDate, string toDate)
        {
            return RedirectToAction("OnGet",new { userId = userId , fromDate = fromDate , toDate = toDate });
        }

    }
}
