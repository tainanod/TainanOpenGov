using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using IAdmin = Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Office")]
    public class UserController : Controller
    {
        private readonly int pageSize = 10;
        private readonly IUserService service;
        private readonly IAdmin.ISharedService _sharedService;
        private readonly IAdmin.IDataSetService _dataSetService;

        public UserController(IUserService service, IAdmin.ISharedService sharedService, IAdmin.IDataSetService dataSetService)
        {
            this.service = service;
            _sharedService = sharedService;
            _dataSetService = dataSetService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialList(int page = 1, SysRoles role = 0, string keyword = null)
        {
            var currentPage = page < 1 ? 1 : page;
            IQueryable<AspNetUsers> videos = service.QueryUser(role, keyword).OrderBy(x => x.UserName);
            var result = videos.ToPagedList(currentPage, pageSize);
            return PartialView(result);
        }

        public JsonResult AddAdminRole(string id)
        {
            Result<AspNetUsers> result = service.AddAdminRole(id);
            result.Data = null;
            return Json(result);
        }

        public JsonResult RemoveAdminRole(string id)
        {
            Result<AspNetUsers> result = service.RemoveAdminRole(id);
            result.Data = null;
            return Json(result);
        }

        public ActionResult OfficeRole()
        {
            var menu = _sharedService.GetSetMenu();
            return View(menu);
        }

        public JsonResult AddToOffice(string id)
        {
            return Json(_sharedService.AddToOffice(id));
        }

        public JsonResult RemoveFromOffice(string id)
        {
           return Json(_sharedService.RemoveFromOffice(id));
        }

        public ActionResult ChoseRole(string id)
        {
            var model = new RoleVm {
                UserId = id
            };
            var roles = service.GetRoles().ToList().Select(x=>new PageNameVm {
                PageId = x.Name,
                PageName = GetRoleName(x.Name)
            }).ToList();
            var offices = _dataSetService.GetUnitList().Select(x=>new PageNameVm {
                PageId = x.PageGuid.ToString(),
                PageName = x.PageName
            });

            ViewData["RoleName"] = new SelectList(roles, "PageId", "PageName");
            ViewData["UnitId"] = new SelectList(offices, "PageId", "PageName");
        
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChoseRole(RoleVm model)
        {
            if (!ModelState.IsValid) {
                return ChoseRole(model.UserId);
            }
            //如果是局處管理者，先更新使用者的UnitId
            if (model.RoleName == "Office")
            {
                if (model.UnitId == null)
                {
                    ModelState.AddModelError("err", "若角色為局處管理者，必須選擇局處。");
                    return ChoseRole(model.UserId);
                }
                service.UpdateUserDataSetUnitId(model.UserId, model.UnitId.ToString());
            }
            ViewBag.Result = service.AddAdminRole(model.UserId, model.RoleName);
            return View();
        }

        private string GetRoleName(string name)
        {
            return name.Equals("admin", StringComparison.OrdinalIgnoreCase) ? "系統管理者" : "局處管理者";
        }
    }
}