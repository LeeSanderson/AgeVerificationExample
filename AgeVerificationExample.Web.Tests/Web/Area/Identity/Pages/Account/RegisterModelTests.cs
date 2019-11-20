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

        [Theory]
        [InlineData(-18, -1, "Birthday tomorrorw")]
        [InlineData(-17, 0, "Only 17")]
        public async Task OnPostAsync_ReturnsErrorIfUserUnder18(int yearOffset, int dayOffset, string testDescription)
        {
            // Given
            var page = CreatePage();
            page.Input = new RegisterInputModel
            {
                Email = "kp@example.com",
                Name = "KP",
                DateOfBirth = GetDateOfBirthFromOffset(yearOffset, dayOffset)
            };

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            page.ExpectModelError(string.Empty, "You must be at least 18 to register");
            actionResult.IsPageResult();
            Assert.NotNull(testDescription);
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

        [Theory]
        [InlineData(-18, 0, "Birthday today")]
        [InlineData(-18, 1, "Over 18 by 1 day")]
        [InlineData(-19, 0, "Over 18")]
        public async Task OnPostAsync_RedirectsToCreatePasswordPageIfValidationSuccessful(
            int yearOffset, int dayOffset, string testDescription)
        {
            // Given
            var page = CreatePage();
            page.Input = new RegisterInputModel
                            {
                                Email = "kp@example.com",
                                Name = "KP",
                                DateOfBirth = GetDateOfBirthFromOffset(yearOffset, dayOffset)
                            };

            // When
            var actionResult = await page.OnPostAsync();

            // Then
            actionResult.IsRedirectToPageResult();
            Assert.NotNull(testDescription);
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

        /// <summary>
        /// Date of birth helper
        /// </summary>
        /// <param name="yearOffset">The year offset from today</param>
        /// <param name="dayOffset">The number of days to add after calculating the date of birth using the year offset</param>
        /// <returns>The date of the date of birth</returns>
        private DateTime GetDateOfBirthFromOffset(int yearOffset, int dayOffset)
        {
            var today = DateTime.Today;
            return new DateTime(today.Year + yearOffset, today.Month, today.Day).AddDays(dayOffset);
        }    
    }
}
