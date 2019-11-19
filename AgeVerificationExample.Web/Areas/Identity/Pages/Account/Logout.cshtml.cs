using System.Threading.Tasks;
using AgeVerificationExample.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AgeVerificationExample.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly IApplicationUserContext applicationUserContext;
        private readonly ILogger<LogoutModel> logger;

        public LogoutModel(IApplicationUserContext applicationUserContext, ILogger<LogoutModel> logger)
        {
            this.applicationUserContext = applicationUserContext;
            this.logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            await applicationUserContext.SignOutAsync();
            logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
