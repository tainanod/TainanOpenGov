using System;
using System.Collections.Generic;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;
using Newtonsoft.Json.Linq;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IParticipationService
    {
        /// <summary>
        /// 取得列表資料
        /// </summary>
        /// <returns></returns>
        IQueryable<Participation> GetQuery();

        /// <summary>
        /// 建立 資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<Participation> Create(Participation model);
        
        /// <summary>
        /// 取得 市政參與 資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Participation GetData(Guid id);

        /// <summary>
        /// 更新 市政參與議題
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<Participation> UpdateData(Participation model);

        /// <summary>
        /// 移除 資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<Participation> RemoveData(Guid id);

        /// <summary>
        /// 封存或取消封存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<Participation> MothballData(Guid id);

        /// <summary>
        /// 新增 圖片關聯
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        bool addPhoto(Guid id, Photo data);

        /// <summary>
        /// 取得 附件 資訊
        /// </summary>
        /// <param name="id"></param>
        /// <param name="docType"></param>
        /// <returns></returns>
        IQueryable<ParticipationDocument> GetDocumentWithType(Guid id, DocType docType);

        /// <summary>
        /// 取得 超連結 資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<ParticipationHyperlink> GetHyperlink(Guid id);

        /// <summary>
        /// 建立超連結資料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>

        Result<ParticipationHyperlink> CreateHyperlink(Guid id, ParticipationHyperlink model);

        /// <summary>
        /// 市政資料 論壇匯出
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Result<JObject> GetForumDiscuss(Guid id, DateTime startDate, DateTime endDate);

        #region 前台

        /// <summary>
        /// 前台 取得 市政參與 資料
        /// </summary>
        /// <returns></returns>
        IQueryable<Participation> GetDataByType();


        IQueryable<ParticipationDiscuss> GetDiscuss(Guid id);


        /// <summary>
        /// Restful-Api用
        /// 取得公民論壇之清單
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiParticipationVm> GetList();

        /// <summary>
        /// Restful-Api用
        /// 取得單一公民論壇之欄位資料內容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiParticipationDetailVm GetDetail(Guid id);

        Result<ParticipationVm> GetDataAndRelations(Guid forumId);

        #endregion
        
        //Forum GetForumWithRelation(Guid id);
    }
}