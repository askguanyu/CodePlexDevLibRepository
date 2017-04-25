//-----------------------------------------------------------------------
// <copyright file="EntityFrameworkRepositoryDbMigrationsConfiguration.cs" company="YuGuan Corporation">
//     Copyright (c) YuGuan Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DevLib.Repository.EntityFramework
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Class EntityFrameworkRepositoryDbMigrationsConfiguration.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigrationsConfiguration{TContext}" />
    public class EntityFrameworkRepositoryDbMigrationsConfiguration<TContext> : DbMigrationsConfiguration<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepositoryDbMigrationsConfiguration{TContext}"/> class.
        /// </summary>
        public EntityFrameworkRepositoryDbMigrationsConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }
    }
}
