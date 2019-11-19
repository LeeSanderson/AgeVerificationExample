using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Contracts.Repositories;
using AgeVerificationExample.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Controllers
{
    /// <summary>
    /// Controller to view login history
    /// </summary>
    [Authorize]
    public class LoginHistoryController : Controller
    {
        private readonly ILoginAttemptRepository loginAttemptRepository;

        public LoginHistoryController(ILoginAttemptRepository loginAttemptRepository)
        {
            this.loginAttemptRepository = loginAttemptRepository;
        }

        /// <summary>
        /// List the first page of login attempts by the user
        /// </summary>
        /// <returns>The view containing the list</returns>
        public async Task<IActionResult> Index()
        {
            return await List(new LoginAttemptPagedList());
        }

        public async Task<IActionResult> List(LoginAttemptPagedList model)
        {
            Contract.Requires(model != null);

            // Always set the user parameters of the model on each page (so we don't need to store this sensitive data in the view)
            model.UserId = this.User.GetUserId();
            model = await this.loginAttemptRepository.GetUserLoginAttempts(model);
            return this.View(nameof(this.Index), model);
        }

        public async Task<IActionResult> Chart()
        {
            var model = await this.loginAttemptRepository.GetLoginStatusCountsForPeriodAsync(30);
            return this.View(model);
        }
    }
}