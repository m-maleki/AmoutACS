using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.CosecApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace App.EndPoint.RazorPages.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAvailabilityAppService _availabilityAppService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;


        public string CosecApi { get; set; } = "danger";
        public string CosecDB { get; set; } = "danger";
        public string AcsDB { get; set; } = "danger";

        public LoginModel(IAvailabilityAppService availabilityAppService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _availabilityAppService = availabilityAppService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGet()
        {
            if (await _availabilityAppService.AvailableCosecApi(default))
                CosecApi = "success";

            if (await _availabilityAppService.AvailableCosecDatabase(default))
                CosecDB = "success";

            if (await _availabilityAppService.AvailableACSDB(default))
                AcsDB = "success";

        }

        public async Task<IActionResult> OnPostLogin(string email, string password)
        {
           var result =  await _signInManager.PasswordSignInAsync(email, password, true, false);
           if (result.Succeeded)
           {
               return RedirectToPage("/Index");
           }
           return RedirectToAction("Account/Login");
        }
    }
}
