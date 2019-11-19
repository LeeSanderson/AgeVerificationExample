using System;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Contracts
{
    /// <summary>
    /// The unit of work defines a transaction boundary for saving changes made to multiple repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save any changes made to multiple repositories (that are part of the same database?) 
        /// asynchrously
        /// </summary>
        Task SaveChangesAsync();
    }
}
