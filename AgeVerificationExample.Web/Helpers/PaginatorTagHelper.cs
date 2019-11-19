using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Helpers
{
    /// <summary>
    /// A tag helper to render a pagination control
    /// </summary>
    [HtmlTargetElement("Paginator")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PaginatorTagHelper : HtmlTagHelper
    {
        /// <inheritdoc />
        public PaginatorTagHelper(IHtmlHelper html)
            : base(html)
        {
        }

        /// <summary>
        /// Gets or sets an expression representing the name of the "page" property to be posted to the server.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Gets or sets the current page
        /// </summary>
        [HtmlAttributeName("current")]
        public int Current { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        [HtmlAttributeName("pageSize")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the total number of records (number of pages = Count / PageSize + 1)
        /// </summary>
        [HtmlAttributeName("count")]
        public int Count { get; set; }

        /// <inheritdoc />
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Contract.Requires(output != null);
            await base.ProcessAsync(context, output);

            var model = new PaginatorViewModel();
            model.ButtonName = this.For.Name;

            var lastPage = (this.Count + this.PageSize - 1) / this.PageSize;

            // Do the complex math in the TagHelper and pass the model to the partial view to render
            if (lastPage <= 10)
            {
                // Only add buttons if we have more that 1 page of data to display.
                if (lastPage > 1)
                {
                    // We have less than 10 pages of data so just render a button per page
                    for (int i = 1; i <= lastPage; i++)
                    {
                        model.Buttons.Add(new PageButton { Value = i.ToString(), Active = i == Current });
                    }
                }
            }
            else
            {
                // More than 10 pages - use pagination button groups
                // Start group of buttons starts at page 1 and goes to 3
                for (int i = 1; i <= 3; i++)
                {
                    model.Buttons.Add(new PageButton { Value = i.ToString(), Active = i == Current });
                }

                if (Current > 1 && Current < lastPage)
                {
                    // Middle group starts at current - 2 and goes to current + 2 (unless overlaps with start/end)
                    int middleGroupStartPageNumber = Math.Max(Current - 2, 4);
                    if (middleGroupStartPageNumber > 4)
                    {
                        // Render a "spacer" button with the value "..." between 3 and start
                        model.Buttons.Add(new PageButton { Value = "...", Disabled = true });
                    }

                    int middleGroupEndPageNumber = Math.Min(Current + 2, lastPage - 2);
                    for (int i = middleGroupStartPageNumber; i <= middleGroupEndPageNumber; i++)
                    {
                        model.Buttons.Add(new PageButton { Value = i.ToString(), Active = i == Current });
                    }

                    if (middleGroupEndPageNumber < lastPage - 2)
                    {
                        // Render a "spacer" button with the value "..." between end and start of "end" group
                        model.Buttons.Add(new PageButton { Value = "...", Disabled = true });
                    }
                }
                else
                {
                    // Render a "spacer" button with the value "..." between start group and end group
                    model.Buttons.Add(new PageButton { Value = "...", Disabled = true });
                }

                // End group of buttons starts at (lastPage - 2) page  and goes to lastPage
                for (int i = lastPage - 2; i <= lastPage; i++)
                {
                    model.Buttons.Add(new PageButton { Value = i.ToString(), Active = i == Current });
                }
            }

            output.TagName = "div";
            var content = await Html.PartialAsync("~/Views/Shared/Templates/PaginatorPartial.cshtml", model);
            output.Content.SetHtmlContent(content);
        }
    }
}
