using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class BannerService : IBannerService
    {
        private readonly IRepository<Banner> _rep;
        private readonly OpenGovContext _db;
        private string _urlName = new WebSite().GetWebSite();

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public BannerService(IRepository<Banner> rep)
        {
            this._rep = rep;
            this._db = rep.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<BannerVm> GetList()
        {
            var data = _db.Banner.OrderByDescending(x => x.IsTopEnabled).ThenByDescending(x => x.CreatedDate)
                .Where(x => !x.IsDeleted && x.IsEnabled)
                .Select(x => new BannerVm()
                {
                    BannerId = x.BannerId,
                    Title = x.Title,
                    WebUrl = x.WebUrl,
                    Target = x.Target,
                    PhotoUrl = _urlName + "/Photo/Detail/" + x.PhotoId
                });
            return data;
        }
    }
}
