using AgeVerificationExample.Web.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data
{
    /// <summary>
    /// Extension methods for <see cref="PagedList{TModel}"/>
    /// </summary>
    public static class PagedListExtensions
    {
        /// <summary>
        /// Apply the paging filter - getting the count of the number of rows that match the query and then
        /// executing the query with the given ordering skipping the offset and taking the pagesize.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <param name="query">The query to apply the paging filter to.</param>
        /// <param name="orderByExpression">The expression to order the results by after the count returns more that zero.</param>
        /// <param name="orderByDescending">Order the results in descending order</param>
        /// <typeparam name="TPagedList">The type of the filter</typeparam>
        /// <typeparam name="TModel">The type of the model - applied to both the filter and query</typeparam>
        /// <typeparam name="TKey">The key of the order by expression</typeparam>
        /// <returns>The <see cref="Task"/> to await the results.</returns>
        public static async Task<TPagedList> ApplyPagingFilter<TPagedList, TModel, TKey>(
            this TPagedList filter,
            IQueryable<TModel> query,
            Expression<Func<TModel, TKey>> orderByExpression,
            bool orderByDescending = false)
            where TPagedList : PagedList<TModel>
        {
            filter.Count = await query.CountAsync();

            if (filter.Count > 0)
            {
                IQueryable<TModel> orderedQuery = orderByDescending
                                                      ? query.OrderByDescending(orderByExpression)
                                                      : query.OrderBy(orderByExpression);

                filter.Page = new List<TModel>(
                    await orderedQuery.Skip(filter.Offset).Take(filter.PageSize).ToListAsync());
            }
            else
            {
                // Not rows in database so no point performing query to get back nothing :)
                filter.Page = new List<TModel>();
            }

            return filter;
        }
    }
}
