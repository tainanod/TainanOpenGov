using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IQuestionService
    {
        QuestInfoVm GetQuestDetail(Guid infoId, Guid? fillUserId = null);
        bool FillQuest(Guid infoId, Guid userId, List<PostQuestFillVm> vm);
        bool CheckUserFilled(Guid infoId, Guid fillUserId);

        #region Admin
        List<QuestInfoVm> GetQustionList(Guid forumId);

        /// <summary>
        /// 問卷資訊-刪除
        /// 回傳ForumId
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        Result<string> GetQuestionInfoRemove(Guid id);

        QuestInfoVm GetSingleQuestInfoSimpleDetail(Guid id);
        List<QuestGroupVm> GetQuestGroupSimpleDetailList(Guid infoID);
        bool ChangeSort(Guid id, string type, bool up);
        QuestInfoVm UpdateQuestInfo(Guid forumId, QuestInfoVm questinfoVm, List<string> ckbDataFill);
        QuestGroupVm CreateGroup(Guid infoId, QuestGroupVm vm);
        QuestGroupVm GetSingleGroupSimpleDetail(Guid groupId);
        QuestGroupVm UpdateGroup(Guid groupId, QuestGroupVm vm);
        bool DeleteGroup(Guid groupId);
        bool DeleteQuestion(Guid qstId);
        QuestQuestionVm CreateQuestion(Guid groupId, QuestQuestionVm vm, List<string> setItem);
        QuestQuestionVm GetQuestQuestionSimpleDetailList(Guid qstId);
        QuestQuestionVm UpdateQuestion(Guid qstId, QuestQuestionVm vm, List<Guid> setItemId, List<string> setItem);
        List<QuestNecessaryRelVm> GetNecessaryRel(Guid qstId);
        void UpdateNecessaryRel(Guid qstId, List<Guid> setIds, List<Guid?> needGourpIds, List<Guid?> needQuestionIds);

        /// <summary>
        /// 跳題
        /// </summary>
        /// <param name="id">qstId</param>
        /// <param name="vmD">跳題</param>
        /// <returns></returns>
        Result<string> UpdateNecessaryRel(Guid id, List<QuestNecessaryRelChangeVm> vmD);

        /// <summary>
        /// 選項-儲存
        /// </summary>
        /// <param name="vmD">id:qstId、name:setDesc</param>
        /// <returns></returns>
        Result<string> QuestSetItemSave(IdName vmD);

        /// <summary>
        /// 選項-刪除
        /// </summary>
        /// <param name="id">setId</param>
        /// <returns></returns>
        Result<string> QuestSetItemRemove(Guid id);

        List<QuestGroupVm> GetGroupsByQstId(Guid qstId);

        #endregion Admin

        #region 問卷管理-填寫-New

        /// <summary>
        /// 問卷列表-是否有填寫過
        /// </summary>
        /// <param name="forumId">公民論壇ID</param>
        /// <returns></returns>
        IQueryable<QuestInfoVm> GetQuestInfo(Guid forumId);

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        QuestInfoVm GetQuestDetailNew(Guid id);

        /// <summary>
        /// 檢查-單筆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <param name="userMail">userMail</param>
        /// <returns></returns>
        Result<string> GetQuestDetailCheckNew(Guid id, string userMail);

        /// <summary>
        /// 填寫問卷
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        Result<string> GetQuestFillCreateNew(QuestSaveVm vmD);

        ///// <summary>
        ///// 填寫問卷
        ///// </summary>
        ///// <param name = "vmD" > 參數 </ param >
        ///// < returns ></ returns >
        //Result < string > GetQuestFillCreateNew(List < PostQuestFillVm > vmD);

        #endregion

        /// <summary>
        /// 回傳Email驗證是否為有效
        /// </summary>
        /// <param name="infoId">infoId</param>
        /// <returns></returns>
        Result<string> GetVerifyCheckEmailTwo(Guid infoId);

        #region 問卷結果-統計圖表

        /// <summary>
        /// 問卷資訊-單筆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        QuestGatherInfoVm GetQuestGatherInfo(Guid id);

        /// <summary>
        /// 統計圖表-列表
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        IQueryable<QuestGatherVm> GetQuestGather(Guid id);

        /// <summary>
        /// 統計圖片-單筆
        /// </summary>
        /// <param name="id">qstId</param>
        /// <returns></returns>
        IQueryable<QuestGatherVm> GetQuestGatherDetail(Guid id);

        #endregion

        #region 問卷結果-填寫檢視

        /// <summary>
        /// 填寫檢視
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        IQueryable<QuestGatherExamineVm> GetQuestGatherExamine(Guid id);

        /// <summary>
        /// 填寫檢視-個人
        /// </summary>
        /// <param name="infoId">infoId</param>
        /// <param name="fillUserId">userId</param>
        /// <returns></returns>
        QuestInfoVm GetQuestGatherExamineDetail(Guid infoId, string fillUserId);

        /// <summary>
        /// 填寫檢視-刪除
        /// 回傳infoId
        /// </summary>
        /// <param name="id">infoId</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        Result<string> GetQuestGatherExamineRemove(Guid id, string userId);

        #endregion

        #region 問卷結果-匯出

        /// <summary>
        /// 全部匯出
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        Result<byte[]> GetQuestGatherExcel(Guid id);

        ///// <summary>
        ///// 匯出-共用
        ///// </summary>
        ///// <param name="id">infoId</param>
        ///// <returns></returns>
        //IQueryable<QuestGatherExcelVm> GetQuestGatherExcelAll(Guid id);

        #endregion

        #region Api用
        IEnumerable<ApiQuestInfoVm> GetList();
        IEnumerable<ApiQuestGatherVm> GetGather(Guid QiId);
        #endregion
    }
}
