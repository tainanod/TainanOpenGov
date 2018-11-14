using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Dal;
using System.Web.Security;

namespace Geo.Grid.Tainan.OpenGov.Service.Common
{
    /// <summary>
    /// 匿名登入
    /// </summary>
    public class IdentityProfile
    {
        private OpenGovContext _db = new OpenGovContext();

        /// <summary>
        /// 如已登入，則傳登入ID
        /// 未登入，傳匿名ID
        /// </summary>
        /// <returns></returns>
        public string GetIdentityProfile()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //已登入
                var data = _db.AspNetUsers.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                return data.Id;
            }
            return HttpContext.Current.Profile.UserName;
        }

        /// <summary>
        /// 回傳匿名id
        /// 登入後，也回傳原匿名id
        /// Session["_anonymousId"] →account/ExternalLoginCallback
        /// </summary>
        /// <returns></returns>
        public string GetIdentityProfileAnonymousId()
        {
            string anonymousId = HttpContext.Current.Profile.UserName;

            if (AnonymousIdentificationModule.Enabled)
            {
                anonymousId = HttpContext.Current.Request.AnonymousID;
            }
            return anonymousId;
        }
    }
}

