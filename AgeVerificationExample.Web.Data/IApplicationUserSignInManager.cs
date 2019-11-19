using AgeVerificationExample.Web.Contracts.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data
{
    /// <summary>
    /// Abstraction over <see cref="SignInManager{T}"/> to allow <see cref="ApplicationUserContext"/>
    /// to be created more easily in tests.
    /// </summary>
    public interface IApplicationUserSignInManager
    {
        bool IsSignedIn(ClaimsPrincipal principal);

        Task SignOutAsync();

        Task<SignInResult> PasswordSignInAsync(string userName, string password);
    }

    public class ApplicationUserSignInManager : IApplicationUserSignInManager
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public ApplicationUserSignInManager(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;         
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return signInManager.IsSignedIn(principal);
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password)
        {
            return await signInManager.PasswordSignInAsync(userName, password, false, false);
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
