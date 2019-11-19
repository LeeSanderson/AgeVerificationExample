using AgeVerificationExample.Web.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data
{
    /// <summary>
    /// Base class for bound context classes
    /// </summary>
    public abstract class EntityContext : IUnitOfWork
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityContext"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected EntityContext(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.DetachAll();
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Detach all attached entries
        /// </summary>
        public void DetachAll()
        {
            foreach (EntityEntry entityEntry in this.context.ChangeTracker.Entries().ToArray())
            {
                if (entityEntry.Entity != null)
                {
                    entityEntry.State = EntityState.Detached;
                }
            }
        }


        /// <summary>
        /// Dispose of the repository
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Don't dispose the context - the DI framwork will do that for us
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Execute a non-query sql command (INSERT, UPDATE, DELETE etc.) 
        /// </summary>
        /// <param name="sql">The formatted sql string.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The task to await the result of the command. The number of rows affected is returned.
        /// </returns>
        public async Task<int> ExecuteSqlCommandAsync(FormattableString sql, CancellationToken cancellationToken = default)
        {
            return await this.context.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);
        }
    }
}
