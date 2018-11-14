using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers.Api
{
    public class QuestionController : ApiController
    {
        private readonly IQuestionService questService;
        private readonly ICityTownService townService;

        public QuestionController(IQuestionService questService, ICityTownService townService)
        {
            this.questService = questService;
            this.townService = townService;
        }

        [HttpGet]
        [Route("api/question/changesort")]
        public IHttpActionResult ChangeSort(Guid? id, string t, bool? up = true)
        {
            if (!id.HasValue || string.IsNullOrEmpty(t) || up == null)
            {
                return BadRequest("Bad Request");
            }

            return Ok(new { Success = questService.ChangeSort(id.Value, t, up.Value) });
        }

        [HttpGet]
        [Route("api/question/deletegroup")]
        public IHttpActionResult DeleteGroup(Guid? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Bad Request");
            }

            return Ok(new { Success = questService.DeleteGroup(id.Value) });
        }

        [HttpGet]
        [Route("api/question/deletequestion")]
        public IHttpActionResult DeleteQuestion(Guid? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Bad Request");
            }

            return Ok(new { Success = questService.DeleteQuestion(id.Value) });
        }

        public IHttpActionResult GetGroups(Guid? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Bad Request");
            }

            return null;
        }

        /// <summary>
        /// 題目-跳題用
        /// </summary>
        /// <param name="id">qstId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Question/Edit/Rel/{id}")]
        public IHttpActionResult QuestionEditRel(Guid id)
        {
            #region 取題目關聯性下拉選單資料 
            //取出下拉式選單顯示題組題目
            var groups = questService.GetGroupsByQstId(id);

            //移除自己
            foreach (var gp in groups)
            {
                if (gp.MdQuestions.Any(x => x.QstId == id))
                {
                    gp.MdQuestions = gp.MdQuestions.Where(x => x.QstId != id);
                    break;
                }
            }
            //移除空題組
            groups.RemoveAll(x => !x.MdQuestions.Any());
            var data = groups.Select(x => new
            {
                GroupId = x.GroupId,
                GroupTitle = x.GroupTitle,
                MdQuestions = x.MdQuestions.Select(y => new { QstId = y.QstId, QstQuestion = y.QstQuestion })
            });
            //ViewData["ddlData"] = Newtonsoft.Json.JsonConvert.SerializeObject(groups.Select(x => new
            //{
            //    GroupId = x.GroupId,
            //    GroupTitle = x.GroupTitle,
            //    MdQuestions = x.MdQuestions.Select(y => new { QstId = y.QstId, QstQuestion = y.QstQuestion })
            //}));
            #endregion 取題目關聯性下拉選單資料 
            return Ok(data);
        }

        /// <summary>
        /// 題目-跳題用-change
        /// </summary>
        /// <param name="id">qstId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Question/Edit/RelChange/{id}")]
        public IHttpActionResult QuestionEditRelChange(Guid id)
        {
            var data = questService.GetNecessaryRel(id);
            return Ok(data);
        }

        /// <summary>
        /// 選項-儲存
        /// </summary>
        /// <param name="id">qstId</param>
        /// <param name="title">內容</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Question/Edit/SetItem/Save")]
        public IHttpActionResult QuestSetItemSave(IdName vmD)
        {
            var data = questService.QuestSetItemSave(vmD);
            return Ok(data);
        }

        /// <summary>
        /// 選項-刪除
        /// </summary>
        /// <param name="id">setId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Question/Edit/SetItem/Remove")]
        public IHttpActionResult QuestSetItemRemove([FromUri]Guid id)
        {
            var data = questService.QuestSetItemRemove(id);
            return Ok(data);
        }

        /// <summary>
        /// 問卷列表
        /// </summary>
        /// <param name="id">forumId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Question/Info/{id}")]
        public IHttpActionResult QuestionInfo(Guid id)
        {
            var data = questService.GetQuestInfo(id);
            return Ok(data);
        }

        #region 問卷結果-統計圖表

        /// <summary>
        /// 統計圖表-單筆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Question/Gather/Info/{id}")]
        public IHttpActionResult QuestionGatherInfo(Guid id)
        {
            var data = questService.GetQuestGatherInfo(id);
            return Ok(data);
        }

        /// <summary>
        /// 統計圖表
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Question/Gather/{id}")]
        public IHttpActionResult QuestionGather(Guid id)
        {
            var data = questService.GetQuestGather(id);

            return Ok(data);
        }

        /// <summary>
        /// 統計圖表-單筆
        /// </summary>
        /// <param name="id">qstId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Question/Gather/Detail/{id}")]
        public IHttpActionResult QuestionGatherDetail(Guid id)
        {
            return Ok(questService.GetQuestGatherDetail(id));
        }

        /// <summary>
        /// 填寫檢視
        /// </summary>
        /// <param name="id">infoId</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Question/Gather/Examine/Detail/{id}")]
        public IHttpActionResult QuestionGatherExamineDetail(Guid id, string userId)
        {
            return Ok(questService.GetQuestGatherExamineDetail(id, userId));
        }

        ///// <summary>
        ///// 匯出-全部+user
        ///// </summary>
        ///// <param name="id">infoId</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("Api/Question/Gather/ExcelAll/{id}")]
        //public IHttpActionResult QuestionGatherExcelAll(Guid id)
        //{
        //    return Ok(questService.GetQuestGatherExcelAll(id));
        //}

        #endregion


        #region api用
        [HttpGet]
        [Route("Api/Question")]
        public IEnumerable<ApiQuestInfoVm> GetList() {
            return questService.GetList();
        }

        [HttpGet]
        [Route("Api/Question/{id}")]
        public IEnumerable<ApiQuestGatherVm> GetDetail(Guid id) {
            return questService.GetGather(id);
        }
        #endregion
    }
}
