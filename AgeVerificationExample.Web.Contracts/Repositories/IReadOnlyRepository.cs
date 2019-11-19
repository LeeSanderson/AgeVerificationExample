using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Contracts.Repositories
{
    /// <summary>
    /// A repository mediates between the domain and data mapping layers using a collection-like interface for 
    /// accessing domain or data objects.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identifiy the model</typeparam>
    /// <typeparam name="TModel">The type of the model</typeparam>
    public interface IReadOnlyRepository<TKey, TModel>
    {
        /// <summary>
        /// Get a model object by key asynchronously
        /// </summary>
        /// <param name="key">The key of the model</param>
        /// <returns>The model</returns>
        Task<TModel> GetAsync(TKey key);

        /// <summary>
        /// Get all model objects in the table asynchronusly
        /// </summary>
        /// <returns>The models</returns>
        Task<IEnumerable<TModel>> GetAllAsync();

        /// <summary>
        /// Ensures a reference (i.e. non-collection) navigation property is loaded asynchronously
        /// Useful if lazy loading is disabled in the underlying provider.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property (required for the expression)</typeparam>
        /// <param name="model">The model to load the reference for</param>
        /// <param name="propertyExpression">The property expression that defines the reference navigation property to load</param>
        /// <returns>The task to await the load of ther navigation property</returns>
        Task EnsureReferenceLoadedAsync<TProperty>(TModel model, Expression<Func<TModel, TProperty>> propertyExpression)
            where TProperty : class;

        /// <summary>
        /// Ensures a collection navigation property is loaded asynchronously.
        /// Useful if lazy loading is disabled in the underlying provider.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property (required for the expression)</typeparam>
        /// <param name="model">The model to load the collection for</param>
        /// <param name="propertyExpression">The property expression that defines the collection navigation property to load</param>
        /// <returns>The task to await the load of ther navigation property</returns>
        Task EnsureCollectionLoadedAsync<TProperty>(TModel model, Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
            where TProperty : class;

    }
}
