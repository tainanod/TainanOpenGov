using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using Geo.Grid.Tainan.OpenGov.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.Yahoo;

namespace Geo.Grid.Tainan.OpenGov
{
    public partial class Startup
    {
        // 如需設定驗證的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // 設定資料庫內容、使用者管理員和登入管理員，以針對每個要求使用單一執行個體
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // 讓應用程式使用 Cookie 儲存已登入使用者的資訊
            // 並使用 Cookie 暫時儲存使用者利用協力廠商登入提供者登入的相關資訊；
            // 在 Cookie 中設定簽章
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // 讓應用程式在使用者登入時驗證安全性戳記。
                    // 這是您變更密碼或將外部登入新增至帳戶時所使用的安全性功能。
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 讓應用程式在雙因素驗證程序中驗證第二個因素時暫時儲存使用者資訊。
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // 讓應用程式記住第二個登入驗證因素 (例如電話或電子郵件)。
            // 核取此選項之後，將會在用來登入的裝置上記住登入程序期間的第二個驗證步驟。
            // 這類似於登入時的 RememberMe 選項。
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // 註銷下列各行以啟用利用協力廠商登入提供者登入
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            var facebook = new FacebookAuthenticationOptions
            {
                //AppId = "500502193431314",
                //AppSecret = "1e4a190be6b3357883003a106b5ae5a9"
                AppId = WebConfigurationManager.AppSettings["FacebookId"],
                AppSecret = WebConfigurationManager.AppSettings["FacebookSecret"],
                BackchannelHttpHandler = new FacebookBackChannelHandler(),
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name,location"
            };
            facebook.Scope.Add("email");
            app.UseFacebookAuthentication(facebook);

            //app.UseFacebookAuthentication(new FacebookAuthenticationOptions {
            //    AppId = WebConfigurationManager.AppSettings["FacebookId"],
            //    AppSecret = WebConfigurationManager.AppSettings["FacebookSecret"],
            //    Scope = { "email"},
            //    Provider=new FacebookAuthenticationProvider {
            //        OnAuthenticated = context => {
            //            context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
            //            return Task.FromResult(true);
            //        }
            //    }
            //});


            var google = new GoogleOAuth2AuthenticationOptions
            {
                //ClientId = "520268624248-gbhqfet71t9nat9akj287qm73u7sa72s.apps.googleusercontent.com",
                //ClientSecret = "ShYiD4iAtakapwc7vqBXg3KE"
                ClientId = WebConfigurationManager.AppSettings["GoogleId"],
                ClientSecret = WebConfigurationManager.AppSettings["GooglePw"]
            };
            app.UseGoogleAuthentication(google);

            app.UseYahooAuthentication(new YahooAuthenticationOptions
            {
                //ConsumerKey = "dj0yJmk9cGtpVU1vWHdWSXBMJmQ9WVdrOVNHWkVhVFZoTm0wbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD01Yg--",
                //ConsumerSecret = "9e6d01a7d5a3911918f54bedbd5dab1b40b37093"
                ConsumerKey = WebConfigurationManager.AppSettings["YahooKey"],
                ConsumerSecret = WebConfigurationManager.AppSettings["YahooSecret"]
            });
        }
    }

    public class FacebookBackChannelHandler : HttpClientHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            // Replace the RequestUri so it's not malformed
            if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
            {
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}