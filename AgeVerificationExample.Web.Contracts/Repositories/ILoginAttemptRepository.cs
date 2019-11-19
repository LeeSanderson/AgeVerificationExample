using AgeVerificationExample.Web.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Contracts.Repositories
{
    /// <summary>
    /// Repository interface for the <see cref="LoginAttempt"/> model class
    /// </summary>
    public interface ILoginAttemptRepository
    {
        /// <summary>
        /// Add a new model to the repository.
        /// </summary>
        /// <param name="model">The model to add</param>
        Task AddAsync(LoginAttempt model);

        Task<LoginAttemptPagedList> GetUserLoginAttempts(LoginAttemptPagedList filter);

        /// <summary>
        /// Get the stats for the number of logins by status
        /// </summary>
        /// <param name="days">The number of days to get stats for</param>
        /// <returns>The list of counts by status</returns>
        Task<List<LoginStatusCount>> GetLoginStatusCountsForPeriodAsync(int days);
    }
}
