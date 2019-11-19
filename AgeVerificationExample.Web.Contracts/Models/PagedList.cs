using System.Collections.Generic;

namespace AgeVerificationExample.Web.Contracts.Models
{
    /// <summary>
    /// Model to represent a page of data in a paged resultset.
    /// </summary>
    /// <typeparam name="TModel">The type of object in the page</typeparam>
    public class PagedList<TModel>
    {
        /// <summary>
        /// Gets or sets the count - the total number of records in the result set.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the page number of this paged list
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets the offset based on the page size and page number
        /// </summary>
        public int Offset => (PageNumber - 1) * PageSize;

        /// <summary>
        /// Gets or sets all model objects on the current page
        /// </summary>
        public List<TModel> Page { get; set; }
    }
}