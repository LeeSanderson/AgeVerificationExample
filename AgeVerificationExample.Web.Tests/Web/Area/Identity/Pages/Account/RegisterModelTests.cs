using AgeVerificationExample.Web.Areas.Identity.Pages.Account;
using AgeVerificationExample.Web.Contracts;
using AgeVerificationExample.Web.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AgeVerificationExample.Web.Tests.Web.Area.Identity.Pages.Account
{
    public class RegisterModelTests : DatabaseTestBase
    {
        private ApplicationUserContextAccessor accessor;

        [Fact]
        public async Task OnPostAsync_ReturnsPageIfModelIsInvalid()
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
        public async Task OnPostAsync_ReturnsLockedOutMessageIfUserAlreadyLockedOut()
        {
            // Given
            var page = CreatePage();
            page.Input = new RegisterInputModel { Email = "kp@example.com", Name = "KP", DateOfBirth = DateTime.Now.AddYears(-20) };
            accessor.Context.RegistrationAttemptRepository.Add(
                new RegistrationAttempt
                {
                    Id = Guid.NewGuid(),
                    Email = page.Input.Email,
                    Name = page.Input.Name,
                    Failures = 3,
                    LockedOutDate = DateTime.Now
                });
            await accessor.Context.SaveChangesAsync();

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsPageResult();
            page.ExpectModelError(string.Empty, "Your registration details have been locked out due to multiple registration failures.");
        }

        [Fact]
        public async Task OnPostAsync_ReturnsErrorIfUserUnder18()
        {
            // Given
            var page = CreatePage();
            page.Input = new RegisterInputModel { Email = "kp@example.com", Name = "KP", DateOfBirth = DateTime.Now.AddYears(-16) };

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsPageResult();
            page.ExpectModelError(string.Empty, "You must be at least 18 to register");
        }

        [Fact]
        public async Task OnPostAsync_ReturnsLockedOutMessageIfUserFailsValidation3Times()
        {
            // Given
            var page = CreatePage();
            page.Input = new RegisterInputModel { Email = "kp@example.com", Name = "KP", DateOfBirth = DateTime.Now.AddYears(-16) };
            accessor.Context.RegistrationAttemptRepository.Add(
                new RegistrationAttempt
                {
                    Id = Guid.NewGuid(),
                    Email = page.Input.Email,
                    Name = page.Input.Name,
                    Failures = 2,
                    LockedOutDate = DateTime.Now
                });
            await accessor.Context.SaveChangesAsync();

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsPageResult();
            page.ExpectModelError(string.Empty, "Your registration details have been locked out due to multiple registration failures.");
        }

        [Fact]
        public async Task OnPostAsync_RedirectsToCreatePasswordPageIfValidationSuccessful()
        {
            // Given
            var page = CreatePage();
            page.Input = new RegisterInputModel { Email = "kp@example.com", Name = "KP", DateOfBirth = DateTime.Now.AddYears(-19) };

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsRedirectToPageResult();
        }

        /// <summary>
        /// Create the page under test
        /// </summary>
        /// <returns>The page</returns>
        private RegisterModel CreatePage()
        {
            accessor = CreateInMemoryDataContext();
            var page = new RegisterModel(accessor.Context, new Mock<ILogger<RegisterModel>>().Object).EnsureHttpContext();
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(m => m.Content(It.IsAny<string>())).Returns("/");
            page.Url = mockUrlHelper.Object;
            return page;
        }
    }
}
