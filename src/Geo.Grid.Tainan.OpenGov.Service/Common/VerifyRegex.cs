using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Service.Common
{
    /// <summary>
    /// 驗證
    /// </summary>
    public class VerifyRegex
    {
        /// <summary>
        /// email驗證
        /// isMatch true:有符合
        /// </summary>
        /// <param name="code">email</param>
        /// <returns></returns>
        public static bool VerifyEmail(string code)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return regex.IsMatch(code);
        }
    }
}
