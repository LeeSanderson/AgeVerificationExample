using AgeVerificationExample.Web.Areas.Identity.Pages.Account;
using AgeVerificationExample.Web.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AgeVerificationExample.Web.Tests.Web.Area.Identity.Pages.Account
{
    /// <summary>
    /// Tests for the <see cref="LoginModel"/> page
    /// </summary>
    public class LoginModelTests
    {
        private Mock<IApplicationUserContext> mockUserContext;

        [Fact]
        public void OnGet_SetsupPage()
        {
            // Given
            var page = CreatePage();

            // When
            page.OnGet();

            // Then
            Assert.Equal("/", page.ReturnUrl);
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageIfModelInvalid()
        {
            // Given
            var page = CreatePage();
            page.ModelState.AddModelError(string.Empty, "Model is invalid");

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsPageResult();
        }

        [Fact]
        public async Task OnPostAsync_RedirectIfLoginSuccessful()
        {
            // Given
            var page = CreatePage();
            page.Input = new LoginInputModel();
            mockUserContext.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsLocalRedirectResult();
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageIfLoginFails()
        {
            // Given
            var page = CreatePage();
            page.Input = new LoginInputModel();
            mockUserContext.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsPageResult();
            page.ExpectModelError(string.Empty, "Invalid login attempt.");
        }

        /// <summary>
        /// Create the page under test
        /// </summary>
        /// <returns>The page</returns>
        private LoginModel CreatePage()
        {
            mockUserContext = new Mock<IApplicationUserContext>();
            var page = new LoginModel(mockUserContext.Object, new Mock<ILogger<LoginModel>>().Object);
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(m => m.Content(It.IsAny<string>())).Returns("/");
            page.Url = mockUrlHelper.Object;
            return page;
        }
    }
}
