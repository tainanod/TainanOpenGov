using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geo.Grid.Tainan.OpenGov.Common
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 取得可為Null日期的格式化串  預設為1911/12/01格式
        /// </summary>
        /// <param name="ori">原始日期</param>
        /// <param name="template">格式化字串</param>
        /// <returns></returns>
        public static string GetDateTimeString(this DateTime? ori,string template = "yyyy/MM/dd")
        {
            if (!ori.HasValue)
            {
                return string.Empty;
            }

            return ori.Value.ToString(template);
        }
    }
}