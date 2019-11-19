using AgeVerificationExample.Web.Controllers;
using AgeVerificationExample.Web.Models;
using Xunit;

namespace AgeVerificationExample.Web.Tests.Web.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_RendersView()
        {
            // Given / When / Then
            new HomeController().Index().IsViewResult();
        }

        [Fact]
        public void Privacy_RendersView()
        {
            // Given / When / Then
            new HomeController().Privacy().IsViewResult();
        }

        [Fact]
        public void Error_RendersViewWithErrorModel()
        {
            // Given / When / Then
            new HomeController().EnsureHttpContext().Error().IsViewResult().WithModel<ErrorViewModel>();
        }
    }
}
