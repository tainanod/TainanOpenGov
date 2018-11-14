using System.Data.Entity.Validation;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Web.Routing;
using Geo.Grid.Tainan.OpenGov.Models;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Geo.Grid.Tainan.OpenGov.Dal;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Session["salt"] = "salt";
            ViewBag.ReturnUrl = returnUrl;

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        ////
        //// POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // 這不會計算為帳戶鎖定的登入失敗
        //    // 若要啟用密碼失敗來觸發帳戶鎖定，請變更為 shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            var user = UserManager.FindByName(model.Email);

        //            return RedirectToLocal(returnUrl);

        //        case SignInStatus.LockedOut:
        //            return View("Lockout");

        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "登入嘗試失試。");
        //            return View(model);
        //    }
        //}

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 要求重新導向至外部登入提供者
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            Session["_anonymousId"] = System.Web.HttpContext.Current.Profile.UserName;
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login", new { ReturnUrl = returnUrl });
            }

            //if (loginInfo.Login.LoginProvider == "Facebook")
            //{
            //    var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
            //    var accessToken = identity.FindFirstValue("FacebookAccessToken");
            //    var fb = new FacebookClient(accessToken);
            //    dynamic myInfo = fb.Get("/me?fields=email,first_name,last_name,gender"); // specify the email field
            //}

            if (loginInfo.Email == null)
            {
                TempData["LoginEmailError"] = string.Format("無法透過 {0}，請改用其他社群帳號登入。", loginInfo.Login.LoginProvider);
                return RedirectToAction("Login", new { ReturnUrl = returnUrl });
            }

            // 若使用者已經有登入資料，請使用此外部登入提供者登入使用者
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:

                    if (returnUrl.Contains("admin")) {
                        var user = await UserManager.FindAsync(loginInfo.Login);
                        var roles = user.Roles.Select(x => x.RoleId).ToList();
                        var dbModel = new OpenGovContext();
                        var menu = dbModel.AspNetRoles.Include(x => x.Menu).AsNoTracking()
                            .Where(x => roles.Contains(x.Id)).SelectMany(x => x.Menu).OrderBy(x=>x.Sort).FirstOrDefault();

                        if (menu != null)
                        {
                            return RedirectToAction(menu.Action, menu.Controller, new {
                                area = menu.Area,
                            });
                        }
                    }

                    return RedirectToLocal(returnUrl);

                //case SignInStatus.LockedOut:
                //    return View("Lockout");

                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });

                //case SignInStatus.Failure:
                default:
                    // 若使用者沒有帳戶，請提示使用者建立帳戶
                    var imageUrl = GetImageUrl(loginInfo.Login.LoginProvider, loginInfo.Login.ProviderKey);

                    // 寫入資訊
                    await ExternalLoginConfirmation(new ExternalLoginConfirmationViewModel
                    {
                        Email = loginInfo.Email,
                        ImageUrl = imageUrl,
                        NickName = loginInfo.DefaultUserName,
                        LoginProvider = loginInfo.Login.LoginProvider
                    },
                        returnUrl);
                    return RedirectToLocal(returnUrl);
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // 從外部登入提供者處取得使用者資訊
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NickName = model.NickName,
                    ImageUrl = model.ImageUrl
                };

                //var result = await UserManager.CreateAsync(user);

                try
                {
                    // 若透過外部登入驗證成功卻發現User重覆.
                    var existUser = UserManager.FindByEmail(info.Email);
                    if (existUser != null)
                    {
                        var otherExistUser = UserManager.AddLogin(existUser.Id, info.Login);
                        if (otherExistUser.Succeeded)
                        {
                            SignInManager.SignIn(existUser, isPersistent: false, rememberBrowser: false);
                            return RedirectToLocal(returnUrl);
                        }
                    }

                    var result = UserManager.Create(user);
                    if (result.Succeeded)
                    {
                        //result = await UserManager.AddLoginAsync(user.Id, info.Login);
                        result = UserManager.AddLogin(user.Id, info.Login);
                        if (result.Succeeded)
                        {
                            //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    AddErrors(result);
                }
                catch (DbEntityValidationException es)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var failure in es.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }



        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Session 快取 Login UserInfo
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult LoginPartial()
        {
            //ApplicationUser user = User.Identity.IsAuthenticated
            //    ? UserManager.FindById(User.Identity.GetUserId())
            //    : null;
            ApplicationUser user = null;
            if (Session["LOGIN_USER"] == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    user = UserManager.FindById(User.Identity.GetUserId());
                    Session["LOGIN_USER"] = user;
                }
            }
            else
            {
                user = Session["LOGIN_USER"] as ApplicationUser;
            }
            return PartialView(user);
        }

        /// <summary>
        /// 帳號登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var returnUrl = (TempData["returnUrl"] != null) ? TempData["returnUrl"].ToString() : null;
            AuthenticationManager.SignOut();
            Session["LOGIN_USER"] = null;
            return RedirectToLocal(returnUrl);
        }

        #region Helper

        // 新增外部登入時用來當做 XSRF 保護
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Forum");
        }

        private static string GetImageUrl(string loginProvider, string providerKey)
        {
            var imageUrl = string.Empty;
            switch (loginProvider.ToLower())
            {
                case "facebook":
                    imageUrl = string.Format("http://graph.facebook.com/{0}/picture", providerKey);
                    break;

                case "google":
                    imageUrl = Service.Common.GoogleApiHelper.GetGoogleUserImageUrl(providerKey);
                    break;

                case "yahoo":
                    imageUrl = "~/Content/geo/img/default_photo.png";
                    break;
            }
            return imageUrl;
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion Helper

        /// <summary>
        /// 自動化測試用
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> AutoTestLogin()
        {
            var result = await SignInManager.PasswordSignInAsync("geoAutoTest@geo.com.tw", "geogeo", false, shouldLockout: false);
            return RedirectToAction("Index", "Forum", new { area = "Admin" });
        }
    }
}