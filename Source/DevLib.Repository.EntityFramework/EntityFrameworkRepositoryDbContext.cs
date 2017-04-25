//-----------------------------------------------------------------------
// <copyright file="EntityFrameworkRepositoryDbContext.cs" company="YuGuan Corporation">
//     Copyright (c) YuGuan Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DevLib.Repository.EntityFramework
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;

    /// <summary>
    /// Class EntityFrameworkRepositoryDbContext.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class EntityFrameworkRepositoryDbContext<TEntity> : DbContext where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepositoryDbContext{TEntity}" /> class.
        /// </summary>
        public EntityFrameworkRepositoryDbContext()
            : base()
        {
            this.Configuration.ProxyCreationEnabled = false;

            if (!this.CompatibleWithModel<TEntity>())
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntityFrameworkRepositoryDbContext<TEntity>, EntityFrameworkRepositoryDbMigrationsConfiguration<EntityFrameworkRepositoryDbContext<TEntity>>>(true));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepositoryDbContext{TEntity}" /> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public EntityFrameworkRepositoryDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.ProxyCreationEnabled = false;

            if (!this.CompatibleWithModel<TEntity>())
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntityFrameworkRepositoryDbContext<TEntity>, EntityFrameworkRepositoryDbMigrationsConfiguration<EntityFrameworkRepositoryDbContext<TEntity>>>(true));
            }
        }

        /// <summary>
        /// Gets or sets the table for the given entity type.
        /// </summary>
        public DbSet<TEntity> Table
        {
            get;
            set;
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized,
        /// but before the model has been locked down and used to initialize the context.
        /// The default implementation of this method does nothing,
        /// but it can be overridden in a derived class such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(i => i.GetTypes())
                .Where(type =>
                    !string.IsNullOrWhiteSpace(type.Namespace)
                    && type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                );

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
