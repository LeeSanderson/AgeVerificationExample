using AgeVerificationExample.Web.Contracts;
using AgeVerificationExample.Web.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Data.Repositories
{
    /// <summary>
    /// Base class implementation of repository and unit of work interfaces using EF core datacontext.
    /// </summary>
    /// <typeparam name="TKey">The key type of the model</typeparam>
    /// <typeparam name="TModel">The model</typeparam>
    internal abstract class EntityRepository<TKey, TModel> : IRepository<TKey, TModel>, IUnitOfWork
        where TModel : class
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The context.
        /// </summary>
        protected readonly ApplicationDbContext Context;

        /// <summary>
        /// The entities.
        /// </summary>
        public readonly DbSet<TModel> Entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{TKey,TModel}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context - usually injected by DI container
        /// </param>
        protected EntityRepository(ApplicationDbContext context)
        {
            this.Context = context;
            this.Entities = context.Set<TModel>();
        }

        /// <inheritdoc />
        public TModel Get(TKey key)
        {
            return this.Entities.Find(key);
        }

        /// <inheritdoc />
        public IEnumerable<TModel> GetAll()
        {
            return this.Entities.AsEnumerable();
        }

        /// <inheritdoc />
        public void EnsureReferenceLoaded(TModel model, string propertyName)
        {
            this.Context.Entry(model).Reference(propertyName).Load();
        }

        /// <inheritdoc />
        public void EnsureCollectionLoaded(TModel model, string propertyName)
        {
            this.Context.Entry(model).Collection(propertyName).Load();
        }

        /// <inheritdoc />
        public void EnsureReferenceLoaded<TProperty>(TModel model, Expression<Func<TModel, TProperty>> propertyExpression)
            where TProperty : class
        {
            this.Context.Entry(model).Reference(propertyExpression).Load();
        }

        /// <inheritdoc />
        public void EnsureCollectionLoaded<TProperty>(TModel model, Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
            where TProperty : class
        {
            this.Context.Entry(model).Collection(propertyExpression).Load();
        }

        /// <inheritdoc />
        public async Task<TModel> GetAsync(TKey key)
        {
            return await this.Entities.FindAsync(key);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await this.Entities.ToListAsync();
        }
        
        /// <inheritdoc />
        public async Task EnsureReferenceLoadedAsync<TProperty>(TModel model, Expression<Func<TModel, TProperty>> propertyExpression)
            where TProperty : class
        {
            await this.Context.Entry(model).Reference(propertyExpression).LoadAsync();
        }

        /// <inheritdoc />
        public async Task EnsureCollectionLoadedAsync<TProperty>(TModel model, Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
            where TProperty : class
        {
            await this.Context.Entry(model).Collection(propertyExpression).LoadAsync();
        }

        /// <inheritdoc />
        public void Add(TModel model)
        {
            this.Entities.Add(model);
        }

        /// <inheritdoc />
        public void Update(TModel model)
        {
            this.Entities.Update(model);
        }

        /// <inheritdoc />
        public void Delete(TModel model)
        {
            this.Entities.Remove(model);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of the repository
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Don't dispose the Context - the DI framwork will do that for us
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            await this.Context.SaveChangesAsync();
        }
    }
}
