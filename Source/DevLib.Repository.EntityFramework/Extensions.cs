﻿//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="YuGuan Corporation">
//     Copyright (c) YuGuan Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DevLib.Repository.EntityFramework
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Core.Mapping;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Class for extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <param name="source">The context.</param>
        /// <param name="type">The type of entity.</param>
        /// <returns>The name of table.</returns>
        public static string GetTableName(this DbContext source, Type type)
        {
            var metadata = ((IObjectContextAdapter)source).ObjectContext.MetadataWorkspace;

            var objectItemCollection = (ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace);

            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            var table = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            return (string)table.MetadataProperties["Table"].Value ?? table.Name;
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="source">The context.</param>
        /// <returns>The name of table.</returns>
        public static string GetTableName<T>(this DbContext source)
        {
            return source.GetTableName(typeof(T));
        }

        /// <summary>
        /// Gets the value of the specified field name.
        /// </summary>
        /// <param name="source">The IDataRecord to read.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The object value.</returns>
        public static object GetValue(this IDataRecord source, string name)
        {
            int ordinal = source.GetOrdinal(name);

            if (source.IsDBNull(ordinal))
            {
                return null;
            }

            return source.GetValue(ordinal);
        }

        /// <summary>
        /// Visits the specified expression.
        /// </summary>
        /// <typeparam name="TExpression">The type of the expression.</typeparam>
        /// <param name="source">The expression.</param>
        /// <param name="visitor">The visitor.</param>
        /// <returns>Expression instance.</returns>
        public static Expression Visit<TExpression>(this Expression source, Func<TExpression, Expression> visitor)
            where TExpression : Expression
        {
            return ExpressionVisitor<TExpression>.Visit(source, visitor);
        }

        /// <summary>
        /// Visits the specified expression.
        /// </summary>
        /// <typeparam name="TExpression">The type of the expression.</typeparam>
        /// <typeparam name="TReturn">The type of the return value.</typeparam>
        /// <param name="source">The expression.</param>
        /// <param name="visitor">The visitor.</param>
        /// <returns>TReturn instance.</returns>
        public static TReturn Visit<TExpression, TReturn>(this TReturn source, Func<TExpression, Expression> visitor)
            where TExpression : Expression
            where TReturn : Expression
        {
            return (TReturn)ExpressionVisitor<TExpression>.Visit(source, visitor);
        }

        /// <summary>
        /// Visits the specified expression.
        /// </summary>
        /// <typeparam name="TExpression">The type of the expression.</typeparam>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        /// <param name="source">The expression.</param>
        /// <param name="visitor">The visitor.</param>
        /// <returns>Expression{TDelegate} instance.</returns>
        public static Expression<TDelegate> Visit<TExpression, TDelegate>(this Expression<TDelegate> source, Func<TExpression, Expression> visitor)
            where TExpression : Expression
        {
            return ExpressionVisitor<TExpression>.Visit(source, visitor);
        }

        /// <summary>
        /// Starts a database transaction from the database provider connection.
        /// </summary>
        /// <param name="source">The <see cref="ObjectContext" /> to get the database connection from.</param>
        /// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
        /// <returns>An object representing the new transaction.</returns>
        public static DbTransaction BeginTransaction(this ObjectContext source, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (source == null)
            {
                throw new ArgumentNullException("ObjectContext");
            }

            if (source.Connection.State != ConnectionState.Open)
            {
                source.Connection.Open();
            }

            return source.Connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The context.</param>
        /// <returns>EntitySetBase instance.</returns>
        public static EntitySetBase GetEntitySet<TEntity>(this ObjectContext source)
        {
            string name = typeof(TEntity).FullName;
            return GetEntitySet(source, name);
        }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <param name="source">The context.</param>
        /// <param name="elementTypeName">Name of the element type.</param>
        /// <returns>EntitySetBase instance.</returns>
        public static EntitySetBase GetEntitySet(this ObjectContext source, string elementTypeName)
        {
            var container = source.MetadataWorkspace.GetEntityContainer(source.DefaultContainerName, DataSpace.CSpace);
            return container.BaseEntitySets.FirstOrDefault(item => item.ElementType.FullName.Equals(elementTypeName));
        }
    }
}
