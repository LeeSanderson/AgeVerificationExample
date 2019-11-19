using AgeVerificationExample.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;

namespace AgeVerificationExample.Web.Tests
{
    /// <summary>
    /// Base class for tests that require access to an in memory database for testing purposes
    /// </summary>
    public class DatabaseTestBase
    {
        protected ApplicationUserContextAccessor CreateInMemoryDataContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            var dataContext = new ApplicationDbContext(optionsBuilder.Options);

            var mockSignInManager = new Mock<IApplicationUserSignInManager>();
            var mockUserManager = new Mock<IApplicationUserManager>();

            return new ApplicationUserContextAccessor(
                new ApplicationUserContext(dataContext, mockSignInManager.Object, mockUserManager.Object),
                mockSignInManager,
                mockUserManager);
        }
    }
}
