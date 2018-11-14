using System;
using System.Collections.Generic;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;

namespace Geo.Grid.Tainan.OpenGov.Service.Admin
{
    public class SharedService : ISharedService
    {
        private readonly IRepository<Menu> _menu;
        private readonly OpenGovContext _db;

        /// <summary>
        /// ShearedService
        /// </summary>
        /// <param name="menu"></param>
        public SharedService(IRepository<Menu> menu)
        {
            _menu = menu;
            _db = menu.Db;
        }

        /// <summary>
        /// 取得啟用中的選單列表
        /// </summary>
        /// <returns></returns>
        public List<MenuVm> GetSetMenu()
        {
            return _menu.GetAll().Where(x => x.IsEnabled).Select(x => new MenuVm
            {
                MenuId = x.MenuId,
                Name = x.Name,
                Controller = x.Controller,
                Action = x.Action,
                Area = x.Area,
                Sort = x.Sort,
                IsEnabled = x.AspNetRoles.Any(y=>y.Name=="Office")
            }).ToList();
        }

        /// <summary>
        /// 取得該使用者所屬的角色所能見的選單列表
        /// </summary>
        /// <returns></returns>
        public List<MenuVm> GetMenu(string userId)
        {
            return _db.AspNetUsers.FirstOrDefault(x => x.Id == userId)
                      .AspNetRoles.FirstOrDefault()
                      .Menu.Where(x => x.IsEnabled).OrderBy(x => x.Sort)
                      .Select(x => new MenuVm()
                      {
                          MenuId = x.MenuId,
                          Name = x.Name,
                          Area = x.Area,
                          Controller = x.Controller,
                          Action = x.Action,
                          Sort = x.Sort
                      }).ToList();
        }

        /// <summary>
        /// 將功能加入局處管理者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<MenuVm> AddToOffice(string id)
        {
            var result = new Result<MenuVm>(false);
            try
            {
                var menu = _menu.Get(x => x.MenuId.ToString() == id);
                if (menu == null)
                {
                    result.Message = "該選單並不存在";
                    return result;
                }

                var role = _db.AspNetRoles.FirstOrDefault(x => x.Name == "Office");
                role.Menu.Add(menu);
                result.Success = _menu.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 將功能移除局處管理者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<MenuVm> RemoveFromOffice(string id)
        {
            var result = new Result<MenuVm>(false);
            try
            {
                var menu = _menu.Get(x => x.MenuId.ToString() == id);
                if (menu == null)
                {
                    result.Message = "該選單並不存在";
                    return result;
                }

                var role = _db.AspNetRoles.FirstOrDefault(x => x.Name == "Office");
                role.Menu.Remove(menu);
                result.Success = _menu.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}