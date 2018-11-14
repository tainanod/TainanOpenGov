using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Service.Common
{
    /// <summary>
    /// 網址
    /// </summary>
    public class WebSite
    {
        /// <summary>
        /// 網址切換
        /// </summary>
        /// <returns></returns>
        public string GetWebSite()
        {
            string urlName = "http://" + HttpContext.Current.Request.Url.Authority;
            if (urlName.Contains("demo2"))
            {
                urlName += "/OpenGov";
            }
            return urlName;
        }
    }
}
