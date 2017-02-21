//-----------------------------------------------------------------------
// <copyright file="RepositoryDbMigrationsConfiguration.cs" company="YuGuan Corporation">
//     Copyright (c) YuGuan Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DevLib.Repository.EntityFramework
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Class RepositoryDbMigrationsConfiguration.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigrationsConfiguration{TContext}" />
    public class RepositoryDbMigrationsConfiguration<TContext> : DbMigrationsConfiguration<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDbMigrationsConfiguration{TContext}"/> class.
        /// </summary>
        public RepositoryDbMigrationsConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }
    }
}
