using System.Reflection;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Geo.Grid.Tainan.OpenGov
{
    public class AutofacConfig
    {
        public static void Bootstrapper()
        {
            // 容器建立者
            var builder = new ContainerBuilder();

            // 註冊Controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // 註冊DbContextFactory
            var connectionString = WebConfigurationManager.ConnectionStrings["OpenGovDB"].ConnectionString;
            builder.RegisterType<Dal.DbContextFactory>()
                .WithParameter("cs", connectionString)
                .As<Dal.Interface.IDbContextFactory>()
                .InstancePerRequest();

            // 註冊 Repository UnitOfWork
            builder.RegisterGeneric(typeof(Dal.GenericRepository<>))
                .As(typeof(Dal.Interface.IRepository<>));

            // 註冊Services
            var services = Assembly.Load("Geo.Grid.Tainan.OpenGov.Service");
            builder.RegisterAssemblyTypes(services).AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(services)
            //       .Where(t => t.Name.EndsWith("Service"))
            //       .AsImplementedInterfaces();

            builder.RegisterFilterProvider();

            // 建立容器
            var container = builder.Build();

            // 解析容器內的型別
            var resolverApi = new AutofacWebApiDependencyResolver(container);

            // 建立相依解析器
            GlobalConfiguration.Configuration.DependencyResolver = resolverApi;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}