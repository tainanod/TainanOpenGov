using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterNameAttribute : ActionFilterAttribute
    {
        public string ViewParameterName { get; set; }
        public string ActionParameterName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.ActionParameters.Add("editormdImageFile", "editormd-image-file");
            //if (filterContext.ActionParameters.ContainsKey(ViewParameterName))
            //{
            //    var parameterValue = filterContext.ActionParameters[ViewParameterName];
            //    filterContext.ActionParameters.Add(ActionParameterName, parameterValue);
            //}
        }
    }
}