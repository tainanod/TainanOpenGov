using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Forum", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}