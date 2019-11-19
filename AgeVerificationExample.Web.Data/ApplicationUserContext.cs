using AgeVerificationExample.Web.Contracts;
using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Contracts.Repositories;
using AgeVerificationExample.Web.Data.Repositories;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data
{
    public class ApplicationUserContext : EntityContext, IApplicationUserContext
    {
        private readonly IApplicationUserSignInManager signInManager;
        private readonly IApplicationUserManager userManager;

        public ApplicationUserContext(
            ApplicationDbContext context,
            IApplicationUserSignInManager signInManager,
            IApplicationUserManager userManager) : base(context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.LoginAttemptRepository = new LoginAttemptRepository(context);
            this.RegistrationAttemptRepository = new RegistrationAttemptRepository(context);
            this.ApplicationUserRepository = new ApplicationUserRepository(context);
        }

        /// <inheritdoc/>
        public ILoginAttemptRepository LoginAttemptRepository { get; }

        /// <inheritdoc/>
        public IRegistrationAttemptRepository RegistrationAttemptRepository { get; }

        /// <inheritdoc/>
        public IApplicationUserRepository ApplicationUserRepository { get; }

        /// <inheritdoc/>
        public async Task<SuccessOrFailureWithErrors> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            return result.Succeeded ? 
                SuccessOrFailureWithErrors.Success : 
                new SuccessOrFailureWithErrors(result.Errors.Select(e => e.Description).ToList());
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return signInManager.IsSignedIn(principal);
        }

        /// <inheritdoc/>
        public async Task<bool> PasswordSignInAsync(string userName, string password)
        {
            var user = await ApplicationUserRepository.GetByEmailAsync(userName);
            if (user != null)
            {
                // User should be valid - try a login to make sure
                var result = await signInManager.PasswordSignInAsync(userName, password);

                // Record the login result
                await LoginAttemptRepository.AddAsync(
                    new LoginAttempt
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        AttemptDate = DateTime.UtcNow,
                        Status = result.Succeeded ? LoginAttemptStatus.Success : LoginAttemptStatus.Fail
                    });

                return result.Succeeded;
            }

            // Else user name found
            return false;
        }

        /// <inheritdoc/>
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
