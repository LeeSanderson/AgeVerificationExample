using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Contracts.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Contracts
{
    /// <summary>
    /// Context for data consumers that require access to application users
    /// (a bit like a DDD bounded context)
    /// </summary>
    public interface IApplicationUserContext : IUnitOfWork
    {
        /// <summary>
        /// Sign in the user
        /// </summary>
        /// <param name="userName">The username/email address of the user</param>
        /// <param name="password">The users password</param>
        /// <returns>The sign in result - true if sign in successful, false otherwise</returns>
        Task<bool> PasswordSignInAsync(string userName, string password);

        /// <summary>
        /// Is there user currently signed in?
        /// </summary>
        /// <param name="principal">The users claim</param>
        /// <returns>True if the user is logged in</returns>
        bool IsSignedIn(ClaimsPrincipal principal);

        /// <summary>
        /// Sign out the current user
        /// </summary>
        /// <returns>Task to await completion of the operation</returns>
        Task SignOutAsync();

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">The user details</param>
        /// <param name="password">The users password</param>
        /// <returns>A success or a failure with errors</returns>
        Task<SuccessOrFailureWithErrors> CreateUserAsync(ApplicationUser user, string password);

        /// <summary>
        /// Get the repository for login attempts
        /// </summary>
        ILoginAttemptRepository LoginAttemptRepository { get; }

        /// <summary>
        /// Get the repository for registration attempts
        /// </summary>
        IRegistrationAttemptRepository RegistrationAttemptRepository { get; }

        /// <summary>
        /// Gets the repositry for application users
        /// </summary>
        IApplicationUserRepository ApplicationUserRepository { get; }
    }
}
