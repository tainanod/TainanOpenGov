using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    /// <summary>
    /// 論壇活動
    /// </summary>
    public interface IActivityService
    {
        /// <summary>
        /// 移除論壇活動
        /// </summary>
        /// <param name="id">活動ID</param>
        /// <returns></returns>
        Result<Activity> Remove(Guid id);

        /// <summary>
        /// 依照活動類別取得論壇活動
        /// </summary>
        /// <param name="id">論壇ID</param>
        /// <param name="type">論壇活動 OR 工作坊</param>
        /// <returns></returns>
        IQueryable<Activity> GetForumActivitiesByType(Guid id, ActivityType type);

        /// <summary>
        /// 新增論壇活動
        /// </summary>
        /// <param name="id">論壇ID</param>
        /// <param name="model">要新增論壇活動的實體</param>
        /// <returns></returns>
        Result<Activity> CreateActivity(Guid id, Activity model);

        /// <summary>
        /// 取得論壇活動
        /// </summary>
        /// <param name="id">活動ID</param>
        /// <returns></returns>
        Activity GetActivity(Guid id);

        /// <summary>
        /// 更新論壇活動
        /// </summary>
        /// <param name="id">活動ID</param>
        /// <param name="model">要更新的論壇活動實體</param>
        /// <returns></returns>
        Result<Activity> UpdateActivity(Guid id, Activity model);

        /// <summary>
        /// 取得與論壇活動相關的附件
        /// </summary>
        /// <param name="id">活動ID</param>
        /// <returns></returns>
        IQueryable<Document> GetDocument(Guid id);

        /// <summary>
        /// 取得論壇活動ViewModel
        /// </summary>
        /// <param name="id">活動ID</param>
        /// <returns>論壇活動ViewModel，附件僅有ID及名稱</returns>
        ActivityVm GetActivityById(Guid id);
    }
}