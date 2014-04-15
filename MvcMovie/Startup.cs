using Microsoft.Owin;
using MvcMovie.Models.db;
using Owin;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

[assembly: OwinStartupAttribute(typeof(MvcMovie.Startup))]
namespace MvcMovie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext,MvcMovie.Migrations.ApplicationDbContextMigrations.Configuration>());
            var context = new ApplicationDbContext();
            context.Database.Initialize(true);


            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieDbContext, MvcMovie.Migrations.MovieDbContextMigrations.Configuration>());
            var context2 = new MovieDbContext();
            context2.Database.Initialize(true);
        }
    }
}
