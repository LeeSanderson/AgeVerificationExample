namespace AgeVerificationExample.Web.Contracts.Repositories
{
    /// <summary>
    /// A repository mediates between the domain and data mapping layers using a collection-like interface for 
    /// accessing domain or data objects.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identifiy the model</typeparam>
    /// <typeparam name="TModel">The type of the model</typeparam>
    public interface IRepository<TKey, TModel> : IReadOnlyRepository<TKey, TModel>
    {
        /// <summary>
        /// Add a new model to the repository.
        /// Changes are not applied until <see cref="IUnitOfWork.SaveChanges"/> is called.
        /// </summary>
        /// <param name="model">The model to add</param>
        void Add(TModel model);

        /// <summary>
        /// Update an existing model in the repository.
        /// Changes are not applied until <see cref="IUnitOfWork.SaveChanges"/> is called.
        /// </summary>
        /// <param name="model">The model to update</param>
        void Update(TModel model);

        /// <summary>
        /// Delete an existing model in the repository
        /// Changes are not applied until <see cref="IUnitOfWork.SaveChanges"/> is called.
        /// </summary>
        /// <param name="model">The model to update</param>
        void Delete(TModel model);
    }
}
