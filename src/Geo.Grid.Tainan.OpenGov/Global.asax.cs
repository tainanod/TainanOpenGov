using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Quartz;
using Forloop.HtmlHelpers;
using System.Web.Security;
using System.Security.Principal;

namespace Geo.Grid.Tainan.OpenGov
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IScheduler _task = null;

        protected void Application_Start()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ScriptContext.ScriptPathResolver = System.Web.Optimization.Scripts.Render;
            
            AutofacConfig.Bootstrapper();
            App_Start.ScheduleConfig.SetupTasks();
        }

        /// <summary>
        /// 應用程式結束
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        protected void Application_End(object sender, EventArgs e)
        {
            if (_task != null)
            {
                _task.Shutdown(false);
            }
        }

        ///// <summary>
        ///// 自動測試用-指定所有角色為admin
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        // 先取得該使用者的 FormsIdentity
        //        FormsIdentity id = (FormsIdentity)User.Identity;
        //        // 再取出使用者的 FormsAuthenticationTicket
        //        FormsAuthenticationTicket ticket = id.Ticket;
        //        // 將儲存在 FormsAuthenticationTicket 中的角色定義取出，並轉成字串陣列
        //        string[] roles = ticket.UserData.Split(new char[] { ',' });
        //        // 指派角色到目前這個 HttpContext 的 User 物件去
        //        Context.User = new GenericPrincipal(Context.User.Identity, roles);
        //    }
        //}
    }
}