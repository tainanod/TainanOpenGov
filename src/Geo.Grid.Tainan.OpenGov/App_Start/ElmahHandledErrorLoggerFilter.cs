using System.Web.Mvc;
using Elmah;

namespace Geo.Grid.Tainan.OpenGov
{
    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
            }
        }
    }
}