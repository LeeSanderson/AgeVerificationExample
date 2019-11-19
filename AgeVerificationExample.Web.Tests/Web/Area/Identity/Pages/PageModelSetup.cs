using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeVerificationExample.Web.Tests.Web.Area.Identity.Pages
{
    public static class PageModelSetup
    {
        /// <summary>
        /// Ensure the page has a valid HTTP context.
        /// </summary>
        /// <param name="pageModel">The page to be configured.</param>
        /// <typeparam name="TPageModel">The type of page</typeparam>
        /// <returns>
        /// The <see cref="TPageModel"/> that was originally pass into the function as the <see cref="pageModel"/> parameter
        /// so that we can chain setup methods.
        /// </returns>
        public static TPageModel EnsureHttpContext<TPageModel>(this TPageModel pageModel)
            where TPageModel : PageModel
        {
            var context = pageModel.EnsureContext().PageContext;
            if (context.HttpContext == null)
            {
                context.HttpContext = new DefaultHttpContext();
            }

            pageModel.TempData = new TempDataDictionary(context.HttpContext, Mock.Of<ITempDataProvider>());
            pageModel.Url = (new Mock<IUrlHelper>()).Object;

            return pageModel;
        }

        /// <summary>
        /// Ensure the page model has a valid context.
        /// </summary>
        /// <param name="pageModel">The page to be configured.</param>
        /// <typeparam name="TPageModel">The type of page </typeparam>
        /// <returns>
        /// The <see cref="TPageModel"/> that was originally pass into the function as the <see cref="pageModel"/> parameter
        /// so that we can chain setup methods.
        /// </returns>
        public static TPageModel EnsureContext<TPageModel>(this TPageModel pageModel)
            where TPageModel : PageModel
        {
            if (pageModel.PageContext == null)
            {
                pageModel.PageContext = new PageContext();
                pageModel.PageContext.ActionDescriptor = new CompiledPageActionDescriptor();
            }

            // For chaining setup 
            return pageModel;
        }

    }
}
