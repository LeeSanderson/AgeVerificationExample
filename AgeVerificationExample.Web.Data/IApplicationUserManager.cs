using AgeVerificationExample.Web.Contracts.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data
{
    public interface IApplicationUserManager
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
    }

    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }
    }
}
