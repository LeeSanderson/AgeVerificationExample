using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Xunit;

namespace AgeVerificationExample.Web.Tests
{
    /// <summary>
    /// Fluent assertions for <see cref="IActionResult"/> and subclasses.
    /// </summary>
    public static class ActionResultAssertions
    {
        /// <summary>
        /// Checks an action resul is a view result.
        /// </summary>
        /// <param name="actionResult">The action result.</param>
        /// <returns>The <see cref="ViewResult"/>.</returns>
        public static ViewResult IsViewResult(this IActionResult actionResult)
        {
            Assert.NotNull(actionResult);
            return Assert.IsType<ViewResult>(actionResult);
        }

        /// <summary>
        /// Checks an action result is a page result.
        /// </summary>
        /// <param name="actionResult">The action result.</param>
        /// <returns>The <see cref="PageResult"/>.</returns>
        public static PageResult IsPageResult(this IActionResult actionResult)
        {
            Assert.NotNull(actionResult);
            return Assert.IsType<PageResult>(actionResult);
        }

        /// <summary>
        /// Checks an action result is a local redirect result.
        /// </summary>
        /// <param name="actionResult">The action result.</param>
        /// <returns>The <see cref="LocalRedirectResult"/>.</returns>
        public static LocalRedirectResult IsLocalRedirectResult(this IActionResult actionResult)
        {
            Assert.NotNull(actionResult);
            return Assert.IsType<LocalRedirectResult>(actionResult);
        }

        /// <summary>
        /// Checks an action result is a redirect to page result.
        /// </summary>
        /// <param name="actionResult">The action result.</param>
        /// <returns>The <see cref="RedirectToPageResult"/>.</returns>
        public static RedirectToPageResult IsRedirectToPageResult(this IActionResult actionResult)
        {
            Assert.NotNull(actionResult);
            return Assert.IsType<RedirectToPageResult>(actionResult);
        }

        /// <summary>
        /// Checks a view result has a model with the expected type.
        /// </summary>
        /// <param name="viewResult">The view result.</param>
        /// <typeparam name="TModel">The expected type of the model</typeparam>
        /// <returns>The <see cref="TModel"/>.</returns>
        public static TModel WithModel<TModel>(this ViewResult viewResult)
        {
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.Model);
            return Assert.IsType<TModel>(viewResult.Model);
        }

        /// <summary>
        /// Check whether a controller contains the given error
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="key">The key.</param>
        /// <param name="expectedModelError">The expected model error.</param>
        /// <returns>
        /// The <see cref="Controller"/>.
        /// </returns>
        public static Controller ExpectModelError(this Controller controller, string key, string expectedModelError)
        {
            ModelStateEntry value;
            if (!controller.ModelState.TryGetValue(key, out value))
            {
                Assert.True(false, $"Expected model error with key '{key}' and error '{expectedModelError}' but key does not exist");
            }

            // ReSharper disable once SimplifyLinqExpression
            if (!value.Errors.Any(m => m.ErrorMessage == expectedModelError))
            {
                string errorMessages = string.Join(", ", value.Errors.Select(m => m.ErrorMessage));
                Assert.True(
                    false,
                    // ReSharper disable once StyleCop.SA1118
                    $"Expected model error with key '{key}' and error '{expectedModelError}' but the error was not found. " +
                    $"The model state contains the following errors: {errorMessages} ");
            }

            return controller;
        }

        public static PageModel ExpectModelError(this PageModel page, string key, string expectedModelError)
        {
            ModelStateEntry value;
            if (!page.ModelState.TryGetValue(key, out value))
            {
                Assert.True(false, $"Expected model error with key '{key}' and error '{expectedModelError}' but key does not exist");
            }

            // ReSharper disable once SimplifyLinqExpression
            if (!value.Errors.Any(m => m.ErrorMessage == expectedModelError))
            {
                string errorMessages = string.Join(", ", value.Errors.Select(m => m.ErrorMessage));
                Assert.True(
                    false,
                    // ReSharper disable once StyleCop.SA1118
                    $"Expected model error with key '{key}' and error '{expectedModelError}' but the error was not found. " +
                    $"The model state contains the following errors: {errorMessages} ");
            }

            return page;
        }

        /// <summary>The has no model errors.</summary>
        /// <param name="controller">The controller.</param>
        /// <returns>The <see cref="Controller"/>.</returns>
        public static Controller HasNoModelErrors(this Controller controller)
        {
            Assert.False(controller.ModelState.Any());

            return controller;
        }
    }
}
