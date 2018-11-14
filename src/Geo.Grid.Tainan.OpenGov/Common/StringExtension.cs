using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geo.Grid.Tainan.OpenGov.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// 縮短顯示文字，後面有..
        /// </summary>
        /// <param name="ori">原始字串</param>
        /// <param name="length">要顯示的長度</param>
        /// <param name="leftStr">縮短後後面的字，預設[..]</param>
        /// <returns></returns>
        public static string ShortStr(this string ori, int length, string leftStr = "..")
        {
            if (ori.Length <= length)
            {
                return ori;
            }

            return ori.Substring(0, length) + leftStr;
        }

        public static System.Web.Mvc.MvcHtmlString GetHtmlStr(this string input)
        {
            return new System.Web.Mvc.MvcHtmlString(input);
        }

    }
}