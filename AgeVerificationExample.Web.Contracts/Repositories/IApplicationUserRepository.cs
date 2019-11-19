using AgeVerificationExample.Web.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Contracts.Repositories
{
    public interface IApplicationUserRepository : IReadOnlyRepository<Guid, ApplicationUser>
    {
        /// <summary>
        /// Lookup an application user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user</param>
        /// <returns>The user or null if no user exists with a matching email address</returns>
        Task<ApplicationUser> GetByEmailAsync(string email);
    }
}
