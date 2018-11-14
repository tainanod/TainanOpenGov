using System;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;

namespace Geo.Grid.Tainan.OpenGov.Dal
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly string connectionString;
        private OpenGovContext context;

        public DbContextFactory(string cs)
        {
            this.connectionString = cs;
        }

        public OpenGovContext GetDbContext()
        {
            if (this.context == null)
            {
                context = (OpenGovContext)Activator.CreateInstance(typeof(OpenGovContext), connectionString);
            }
            return context;
        }
    }
}