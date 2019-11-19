using AgeVerificationExample.Web.Contracts;
using AgeVerificationExample.Web.Data;
using Moq;

namespace AgeVerificationExample.Web.Tests
{
    /// <summary>
    /// Accessor for the <see cref="IApplicationUserContext"/> and various mock object used in it's construction
    /// </summary>
    public class ApplicationUserContextAccessor
    {
        public ApplicationUserContextAccessor(
            IApplicationUserContext context,
            Mock<IApplicationUserSignInManager> mockSignInManager, 
            Mock<IApplicationUserManager> mockUserManager)
        {
            this.Context = context;
            MockSignInManager = mockSignInManager;
            MockUserManager = mockUserManager;
        }

        public IApplicationUserContext Context { get; }

        public Mock<IApplicationUserSignInManager> MockSignInManager { get; }

        public Mock<IApplicationUserManager> MockUserManager { get;  }
    }
}
