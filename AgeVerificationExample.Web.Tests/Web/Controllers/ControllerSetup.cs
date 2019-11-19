using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AgeVerificationExample.Web.Tests.Web.Controllers
{
    /// <summary>
    /// Common setup methods for configuring controllers under test
    /// </summary>
    public static class ControllerSetup
    {
        /// <summary>
        /// The default user id used by <see cref="SetupUser{TController}"/> when no other value is specified.
        /// </summary>
        public static readonly Guid SetupUserId = Guid.Parse("9730C598-5EF2-4962-BA2F-08D5B01B5B92");

        /// <summary>
        /// Ensure the controller has a valid controller context.
        /// </summary>
        /// <param name="controller">The controller to be configured.</param>
        /// <typeparam name="TController">The type of controller</typeparam>
        /// <returns>
        /// The <see cref="TController"/> that was originally pass into the function as the <see cref="controller"/> parameter
        /// so that we can chain setup methods.
        /// </returns>
        public static TController EnsureContext<TController>(this TController controller)
            where TController : Controller
        {
            if (controller.ControllerContext == null)
            {
                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
            }

            // For chaining setup 
            return controller;
        }

        /// <summary>
        /// Ensure the controller has a valid HTTP context.
        /// </summary>
        /// <param name="controller">The controller to be configured.</param>
        /// <typeparam name="TController">The type of controller</typeparam>
        /// <returns>
        /// The <see cref="TController"/> that was originally pass into the function as the <see cref="controller"/> parameter
        /// so that we can chain setup methods.
        /// </returns>
        public static TController EnsureHttpContext<TController>(this TController controller)
            where TController : Controller
        {
            var context = controller.EnsureContext().ControllerContext;
            if (context.HttpContext == null)
            {
                context.HttpContext = new DefaultHttpContext();
            }

            controller.TempData = new TempDataDictionary(context.HttpContext, Mock.Of<ITempDataProvider>());
            controller.Url = (new Mock<IUrlHelper>()).Object;

            return controller;
        }

        /// <summary>
        /// Setup the <see cref="Controller.User"/> in the given controller.
        /// </summary>
        /// <param name="controller">The controller to be configured.</param>
        /// <param name="isAuthenticated">
        /// Is the user authenticated?
        /// If false then all other parameters will be ignored. Defaults to true
        /// </param>
        /// <param name="userId">The user id of the user. If not specified then <see cref="SetupUserId"/> will be used</param>
        /// <param name="emailAddress">The email address of the user If not specified then "katy.perry@example.com" will be used.</param>
        /// <typeparam name="TController">The type of controller</typeparam>
        /// <returns>
        /// The <see cref="TController"/> that was originally pass into the function as the <see cref="controller"/> parameter
        /// so that we can chain setup methods.
        /// </returns>
        public static TController SetupUser<TController>(
            this TController controller,
            bool isAuthenticated = true,
            Guid? userId = null,
            string emailAddress = "katy.perry@example.com")
            where TController : Controller
        {
            controller.EnsureHttpContext().ControllerContext.HttpContext.User =
                CreateUser(
                    isAuthenticated,
                    userId,
                    emailAddress);
            return controller;
        }
        
        /// <summary>
        /// Create a LENA user
        /// </summary>
        /// <param name="isAuthenticated">
        /// Is the user authenticated?
        /// If false then all other parameters will be ignored. Defaults to true
        /// </param>
        /// <param name="userId">The user id of the user. If not specified then <see cref="SetupUserId"/> will be used</param>
        /// <param name="emailAddress">The email address of the user If not specified then "katy.perry@example.com" will be used.</param>
        /// <returns>
        /// The user
        /// </returns>
        public static ClaimsPrincipal CreateUser(
            bool isAuthenticated = true,
            Guid? userId = null,
            string emailAddress = "katy.perry@example.com")
        {
            ClaimsPrincipal user;
            if (!isAuthenticated)
            {
                user = new ClaimsPrincipal(new ClaimsIdentity());
            }
            else
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, (userId ?? SetupUserId).ToString()));

                claims.Add(new Claim(ClaimTypes.Name, emailAddress));

                user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Application.Identity"));
            }

            return user;
        }
    }
}
