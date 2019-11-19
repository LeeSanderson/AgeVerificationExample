using AgeVerificationExample.Web.Areas.Identity.Pages.Account;
using AgeVerificationExample.Web.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AgeVerificationExample.Web.Tests.Web.Area.Identity.Pages.Account
{
    public class LogoutModelTests
    {
        private Mock<IApplicationUserContext> mockUserContext;

        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageIfReturnUrlIsNull()
        {
            // Given
            var page = CreatePage();

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsRedirectToPageResult();
            mockUserContext.Verify(m => m.SignOutAsync(), Times.Once);
        }

        [Fact]
        public async Task OnPostAsync_ReturnsLocalRedirectIfReturnUrlIsNotNull()
        {
            // Given
            var page = CreatePage();

            // When
            var actionResult = await page.OnPostAsync("/");

            // Then
            actionResult.IsLocalRedirectResult();
            mockUserContext.Verify(m => m.SignOutAsync(), Times.Once);
        }

        /// <summary>
        /// Create the page under test
        /// </summary>
        /// <returns>The page</returns>
        private LogoutModel CreatePage()
        {
            mockUserContext = new Mock<IApplicationUserContext>();
            var page = new LogoutModel(mockUserContext.Object, new Mock<ILogger<LogoutModel>>().Object);
            return page;
        }
    }
}
