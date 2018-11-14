using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface.Admin
{
    /// <summary>
    /// 公民論壇-討論區
    /// </summary>
    public interface IDiscussService
    {
        /// <summary>
        /// 公民論壇-單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        ForumVm GetForumDetail(Guid id);

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">id:公民論壇編號</param>
        /// <returns></returns>
        IQueryable<DiscussVm> GetList(SearchVm key);

        /// <summary>
        /// 置頂/非置頂
        /// </summary>
        /// <param name="id">discussId</param>
        /// <returns></returns>
        Result<string> GetUpdateTop(Guid id);
    }
}
