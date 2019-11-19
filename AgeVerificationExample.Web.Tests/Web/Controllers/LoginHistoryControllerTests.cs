using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Contracts.Repositories;
using AgeVerificationExample.Web.Controllers;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgeVerificationExample.Web.Tests.Web.Controllers
{
    /// <summary>
    /// Tests for the <see cref="LoginHistoryController"/>
    /// </summary>
    public class LoginHistoryControllerTests
    {
        private Mock<ILoginAttemptRepository> mockLoginAttempRepository;

        [Fact]
        public async Task Index_ReturnsView()
        {
            // Given
            var controller = CreateController();
            mockLoginAttempRepository.Setup(m => m.GetUserLoginAttempts(It.IsAny<LoginAttemptPagedList>()))
                .ReturnsAsync(new LoginAttemptPagedList());

            // When
            var actionResult = await controller.Index();

            // Then
            actionResult.IsViewResult().WithModel<LoginAttemptPagedList>();
        }

        [Fact]
        public async Task List_ReturnsView()
        {
            // Given
            var controller = CreateController();
            mockLoginAttempRepository.Setup(m => m.GetUserLoginAttempts(It.IsAny<LoginAttemptPagedList>()))
                .ReturnsAsync(new LoginAttemptPagedList());

            // When
            var actionResult = await controller.List(new LoginAttemptPagedList());

            // Then
            actionResult.IsViewResult().WithModel<LoginAttemptPagedList>();
        }

        [Fact]
        public async Task Chart_ReturnsView()
        {
            // Given
            var controller = CreateController();
            mockLoginAttempRepository.Setup(m => m.GetLoginStatusCountsForPeriodAsync(30))
                .ReturnsAsync(new List<LoginStatusCount>());

            // When
            var actionResult = await controller.Chart();

            // Then
            actionResult.IsViewResult().WithModel<List<LoginStatusCount>>();
        }

        /// <summary>
        /// Helper to create controller
        /// </summary>
        /// <returns>The controller (and as a side-effect also creates the <see cref="mockLoginAttempRepository"/>)</returns>
        private LoginHistoryController CreateController()
        {
            this.mockLoginAttempRepository = new Mock<ILoginAttemptRepository>();
            var controller = new LoginHistoryController(this.mockLoginAttempRepository.Object);
            controller.SetupUser();

            return controller;
        }
    }
}
