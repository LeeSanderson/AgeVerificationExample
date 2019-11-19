using System.Collections.Generic;

namespace AgeVerificationExample.Web.Helpers
{
    /// <summary>
    /// View model to inform the paginator template how to render the pagination control.
    /// </summary>
    public class PaginatorViewModel
    {
        /// <summary>
        /// Gets or sets the name to be assigned as the form name of the submit buttons responsible for paging
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// Gets the buttons to be rendered
        /// </summary>
        public List<PageButton> Buttons { get; } = new List<PageButton>();
    }
}