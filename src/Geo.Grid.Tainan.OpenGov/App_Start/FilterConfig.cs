using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // 必須置於第一順位
            // http://blog.miniasp.com/post/2013/03/12/ASPNET-MVC-4-and-ELMAH-Integration.aspx
            //filters.Add(new ElmahHandledErrorLoggerFilter());

            // 預設的 Exception Action Filter
            filters.Add(new HandleErrorAttribute());
        }
    }
}