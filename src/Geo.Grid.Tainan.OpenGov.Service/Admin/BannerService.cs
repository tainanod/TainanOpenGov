using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;
using Geo.Grid.Tainan.OpenGov.Entity.Dictionary;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;

namespace Geo.Grid.Tainan.OpenGov.Service.Admin
{
    public class BannerService : IBannerService
    {
        private readonly IRepository<Banner> _banner;
        private readonly OpenGovContext _db;

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public BannerService(IRepository<Banner> banner)
        {
            this._banner = banner;
            this._db = banner.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public IQueryable<BannerVm> GetList(SearchVm key)
        {
            var data = _db.Banner.OrderByDescending(x => x.IsTopEnabled).ThenByDescending(x => x.CreatedDate)
                .Where(x => !x.IsDeleted)
                .Select(x => new BannerVm()
                {
                    BannerId = x.BannerId,
                    Title = x.Title,
                    IsTopEnabled = x.IsTopEnabled,
                    IsEnabled = x.IsEnabled,
                    PhotoId = x.PhotoId,
                    CreatedDate = x.CreatedDate
                });
            return data;
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetCreate(BannerVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = new Banner();
                data.Title = vmD.Title;
                data.WebUrl = vmD.WebUrl;
                data.Target = vmD.Target;
                data.IsTopEnabled = vmD.IsTopEnabled;
                data.IsDeleted = false;
                data.PhotoId = vmD.PhotoId;
                data.IsEnabled = vmD.IsEnabled;

                result.Success = _banner.Create(data) > 0;
                result.ID = data.BannerId;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public BannerVm GetDetail(Guid id)
        {
            var data = _db.Banner.Where(x => x.BannerId == id).Select(x => new BannerVm()
            {
                BannerId = x.BannerId,
                Title = x.Title,
                WebUrl = x.WebUrl,
                Target = x.Target,
                PhotoId = x.PhotoId,
                IsTopEnabled = x.IsTopEnabled,
                IsEnabled = x.IsEnabled
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetEdit(BannerVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.Banner.Find(vmD.BannerId);
                if (data != null)
                {
                    data.Title = vmD.Title;
                    data.WebUrl = vmD.WebUrl;
                    data.Target = vmD.Target;
                    data.PhotoId = vmD.PhotoId;
                    data.IsTopEnabled = vmD.IsTopEnabled;
                    data.IsEnabled = vmD.IsEnabled;

                    result.Success = _banner.SaveChanges() > 0;
                    result.ID = data.BannerId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 刪除-儲存
        /// </summary>
        /// <param name="id">參數</param>
        /// <returns></returns>
        public Result<string> GetRemove(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.Banner.Find(id);
                if (data != null)
                {
                    data.IsDeleted = true;
                    result.Success = _banner.SaveChanges() > 0;
                    result.ID = data.BannerId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
    }
}
