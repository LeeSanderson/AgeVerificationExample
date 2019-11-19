using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data.Repositories
{
    internal class ApplicationUserRepository : EntityRepository<Guid, ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await this.Entities.AsQueryable().Where(e => e.Email == email).FirstOrDefaultAsync();
        }
    }
}
