using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.Dictionary
{
    /// <summary>
    /// 驗證方式
    /// </summary>
    public class VerifyType
    {
        public static readonly IDictionary<int, string>
            VoteVerifyType = new Dictionary<int, string>
            {
                { 1,"第一階段：毋須登入即可填寫（無驗證）。"},
                { 2,"第二階段：毋須登入，惟須透過電子郵件進行資料驗證。"},
                { 3,"第三階段：民眾以帳號（MAIL）登入驗證。"}
            };
    }
}
