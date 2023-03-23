using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.AppServices;
using App.Domain.Core.CosecApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoint.RazorPages.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly IEventAppService _eventAppService;
        private readonly ISyncAppService _syncAppService;

        public List<EventOutputDto> Events { get; set; } = new List<EventOutputDto>();

        public IndexModel(IEventAppService eventAppService, 
            ISyncAppService syncAppService)
        {
            _eventAppService = eventAppService;
            _syncAppService = syncAppService;
        }

        public async Task OnGet()
        {
            await _syncAppService.SyncEvents(default);

            DateTime from = DateTime.Today.AddDays(-7);
            DateTime to = DateTime.Now;

            Events = await _eventAppService.GetAll(from,to,default);

        }
    }
}
