namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Geo.Grid.Tainan.OpenGov.Dal.OpenGovContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Geo.Grid.Tainan.OpenGov.Dal.OpenGovContext";
        }

        protected override void Seed(Geo.Grid.Tainan.OpenGov.Dal.OpenGovContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}