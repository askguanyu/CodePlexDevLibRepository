//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="YuGuan Corporation">
//     Copyright (c) YuGuan Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DevLib.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface IRepository with common repository methods.
    /// </summary>
    /// <typeparam name="TEntity">The type the entity.</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Gets or sets a value indicating whether throw exception on any error.
        /// </summary>
        bool ThrowOnError
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this property to log action.
        /// For example, to log to the console, set this property to System.Console.Write(System.String).
        /// </summary>
        Action<string> Log
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the timeout value, in seconds, for all context operations.
        /// The default value is null, where null indicates that the default value of the underlying provider will be used.
        /// </summary>
        int? Timeout
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new entity.
        /// </summary>
        /// <returns>TEntity instance.</returns>
        TEntity Create();

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>Inserted TEntity instance.</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts the given collection of entities.
        /// </summary>
        /// <param name="entities">The collection of entities to insert.</param>
        /// <returns>The collection of entities.</returns>
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts the specified entity, if already exist then update.
        /// </summary>
        /// <param name="entity">The entity to insert or update.</param>
        /// <returns>Inserted TEntity instance.</returns>
        TEntity InsertOrUpdate(TEntity entity);

        /// <summary>
        /// Inserts the given collection of entities, if already exist then update.
        /// </summary>
        /// <param name="entities">The collection of entities to insert or update.</param>
        /// <returns>The collection of entities.</returns>
        IEnumerable<TEntity> InsertOrUpdate(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes an entity with the given primary key values.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        TEntity Delete(params object[] keyValues);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The entity found, or null.</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes the given collection of entities.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        /// <returns>The collection of entities.</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the given collection of entities.
        /// </summary>
        /// <param name="predicate">A function to test entity for a condition.</param>
        /// <returns>The collection of entities.</returns>
        IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes all entities.
        /// </summary>
        /// <returns>The number of entries deleted.</returns>
        int DeleteAll();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The entity found, or null.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates the given collection of entities.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        /// <returns>The collection of entities.</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Checks whether the specified entity exists or not.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>true if exists; otherwise, false.</returns>
        bool Exists(params object[] keyValues);

        /// <summary>
        /// Checks whether the specified entity exists or not.
        /// </summary>
        /// <param name="predicate">A function to test entity for a condition.</param>
        /// <returns>true if exists; otherwise, false.</returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds an entity with the given primary key values.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        TEntity Select(params object[] keyValues);

        /// <summary>
        /// Selects the entities based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test entity for a condition.</param>
        /// <returns>The collection of entities.</returns>
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Selects all entities.
        /// </summary>
        /// <returns>The collection of entities as IQueryable{T}.</returns>
        IQueryable<TEntity> SelectAll();
    }
}
