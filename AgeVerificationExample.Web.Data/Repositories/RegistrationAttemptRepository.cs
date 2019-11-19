using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data.Repositories
{
    internal class RegistrationAttemptRepository : EntityRepository<Guid, RegistrationAttempt>, IRegistrationAttemptRepository
    {
        public RegistrationAttemptRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<RegistrationAttempt> GetByEmailAndNameAsync(string email, string name)
        {
            return await this.Entities.AsQueryable().Where(e => e.Email == email && e.Name == name).FirstOrDefaultAsync();
        }
    }
}
