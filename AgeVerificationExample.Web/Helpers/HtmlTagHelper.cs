using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Helpers
{
    /// <summary>
    /// Base class for tag helpers that using <see cref="IHtmlHelper"/> to render partial views.
    /// </summary>
    public abstract class HtmlTagHelper : TagHelper
    {
        /// <summary>
        /// Gets the html helper.
        /// </summary>
        protected IHtmlHelper Html { get; }

        /// <summary>
        /// Gets or sets the view context of the currently executing page (magically set up by ASP.NET).
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTagHelper"/> class.
        /// </summary>
        /// <param name="html">The html helper - injected by the DI framework.</param>
        protected HtmlTagHelper(IHtmlHelper html)
        {
            this.Html = html;
        }

        /// <inheritdoc />
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            ((IViewContextAware)this.Html).Contextualize(this.ViewContext);
            await base.ProcessAsync(context, output);
        }
    }
}