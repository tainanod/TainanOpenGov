using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    /// <summary>
    /// 形像圖管理
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IQueryable<BannerVm> GetList();
    }
}
