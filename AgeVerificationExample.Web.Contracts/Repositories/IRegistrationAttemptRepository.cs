using AgeVerificationExample.Web.Contracts.Models;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Contracts.Repositories
{
    public interface IRegistrationAttemptRepository
    {
        /// <summary>
        /// Get a registration attempt by email address and name
        /// </summary>
        /// <param name="email">The email address of the registration attempt</param>
        /// <param name="name">The name of the user registering</param>
        /// <returns>The registration attempt if the user has attempted to register before</returns>
        Task<RegistrationAttempt> GetByEmailAndNameAsync(string email, string name);

        /// <summary>
        /// Add a new model to the repository.
        /// Changes are not applied until <see cref="IUnitOfWork.SaveChangesAsync"/> is called.
        /// </summary>
        /// <param name="model">The model to add</param>
        void Add(RegistrationAttempt model);
    }
}
