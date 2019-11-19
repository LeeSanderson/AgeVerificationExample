using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data.Repositories
{
    internal class LoginAttemptRepository : EntityRepository<Guid, LoginAttempt>, ILoginAttemptRepository
    {
        public LoginAttemptRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task AddAsync(LoginAttempt model)
        {
            this.Entities.Add(model);
            await this.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<LoginStatusCount>> GetLoginStatusCountsForPeriodAsync(int days)
        {
            var minDate = DateTime.UtcNow.AddDays(-1 * days);
            return await this.Entities.AsQueryable()
                .Where(e => e.AttemptDate > minDate)
                .GroupBy(e => e.Status)
                .Select(e => new LoginStatusCount { Status = e.Key, Count = e.Count() })
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<LoginAttemptPagedList> GetUserLoginAttempts(LoginAttemptPagedList filter)
        {
            var query = this.Entities.AsQueryable().Where(e => e.UserId == filter.UserId);
            return await filter.ApplyPagingFilter(query, e => e.AttemptDate, true);
        }
    }
}
