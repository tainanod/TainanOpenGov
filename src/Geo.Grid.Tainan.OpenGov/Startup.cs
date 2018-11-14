using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Geo.Grid.Tainan.OpenGov.Startup))]

namespace Geo.Grid.Tainan.OpenGov
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}