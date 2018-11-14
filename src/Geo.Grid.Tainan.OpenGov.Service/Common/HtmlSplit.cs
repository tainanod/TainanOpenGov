using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Service.Common
{
    /// <summary>
    /// html處理
    /// </summary>
    public class HtmlSplit
    {
        /// <summary>
        /// 去除Html
        /// </summary>
        /// <param name="txt">文字</param>
        /// <returns></returns>
        public static string SplitHtml(string txt)
        {
            string split = txt;

            if (string.IsNullOrEmpty(txt))
            {
                return string.Empty;
            }

            Regex regex = new Regex("<[^>]*>");
            split = regex.Replace(split, string.Empty);
            return split;
        }

        /// <summary>
        /// 縮字
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="counts"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string SplitWord(string txt, int counts, string model)
        {
            string split = SplitHtml(txt);
            string[] strArray = new string[] { " ", "　", "\r", "\n", "\r\n", "&nbsp;", "&#12288;", "&hellip;", "&rarr;" };
            if (string.IsNullOrEmpty(split))
            {
                return string.Empty;
            }

            foreach (var item in strArray)
            {
                split = split.Replace(item, string.Empty);
            }

            Encoding utf8 = Encoding.GetEncoding("big5");
            byte[] byteArray = utf8.GetBytes(split);

            counts *= 2;
            if (byteArray.Length <= counts)
            {
                return split;
            }
            else
            {
                string res = utf8.GetString(byteArray, 0, counts);
                if (!utf8.GetString(byteArray).StartsWith(res))
                {
                    res = utf8.GetString(byteArray, 0, counts - 1);
                }
                if (string.IsNullOrEmpty(split))
                {
                    model = string.Empty;
                }
                return res + model;
            }
        }
    }
}
