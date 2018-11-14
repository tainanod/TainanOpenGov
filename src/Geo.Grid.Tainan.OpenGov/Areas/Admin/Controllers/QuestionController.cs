using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using PagedList;

namespace Geo.Grid.Tainan.OpenGov.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService questService;
        private readonly IForumService forumService;

        public QuestionController(IQuestionService questService, IForumService forumService)
        {
            this.questService = questService;
            this.forumService = forumService;
        }

        /*目前Partial View看起來是多餘的  不過未來如果擴充分頁功能可以使用*/

        /// <summary>
        /// 問卷清單
        /// </summary>
        /// <param name="id">Forum ID</param>
        /// <returns></returns>
        public ActionResult Index(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "forum");
            }

            var forum = forumService.GetForum(id.Value);

            return View(forum);
        }

        /// <summary>
        /// 問卷清單Partial View
        /// </summary>
        /// <param name="id">Forum ID</param>
        /// <returns></returns>
        public ActionResult PartialIndex(Guid id)
        {
            var qList = questService.GetQustionList(id);
            return PartialView(qList);
        }

        /// <summary>
        /// 問卷資訊-刪除
        /// 回傳ForumId
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public ActionResult QuestionInfoRemove(Guid id)
        {
            var data = questService.GetQuestionInfoRemove(id);
            return RedirectToAction("Index", new { id = data.ID });
        }

        /// <summary>
        /// 群組題目清單
        /// </summary>
        /// <param name="id">問卷ID</param>
        /// <returns></returns>
        public ActionResult GroupIndex(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index", "forum");
            }
            var qInfo = questService.GetSingleQuestInfoSimpleDetail(id.Value);
            return View(qInfo);
        }

        /// <summary>
        /// 群組題目清單Partial View
        /// </summary>
        /// <param name="id">問卷ID</param>
        /// <returns></returns>
        public ActionResult PartialGroupIndex(Guid id)
        {
            var gList = questService.GetQuestGroupSimpleDetailList(id);
            return PartialView(gList);
        }

        /// <summary>
        /// 新增/修改問卷
        /// </summary>
        /// <param name="id">問卷ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QuestionInfo(Guid? id, Guid fid)
        {
            var questInfoVm = new QuestInfoVm();
            ViewData["fid"] = fid;

            if (id.HasValue)
            {
                //update
                questInfoVm = questService.GetSingleQuestInfoSimpleDetail(id.Value);
            }
            //else create

            return View(questInfoVm);
        }

        /// <summary>
        /// 新增/修改問卷 POST
        /// </summary>
        /// <param name="ckbDataFill">資料填寫欄位</param>
        /// <param name="questinfoVm">VM</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QuestionInfo(Guid? id, Guid fid, QuestInfoVm questinfoVm, List<string> ckbDataFill)
        {
            QuestInfoVm ret = questService.UpdateQuestInfo(fid, questinfoVm, ckbDataFill);
            return RedirectToAction("QuestionInfo", new { id = ret.InfoId, fid = fid });
        }

        public ActionResult GroupCreate(Guid? id, QuestGroupVm vm)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index", "forum");
            }

            if (Request.HttpMethod == "GET")
            {
                return View(new QuestGroupVm());
            }

            vm = questService.CreateGroup(id.Value, vm);

            return RedirectToAction("GroupIndex", new { id = id });
        }

        public ActionResult GroupEdit(Guid? id, QuestGroupVm vm)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index", "forum");
            }

            if (Request.HttpMethod == "GET")
            {
                vm = questService.GetSingleGroupSimpleDetail(id.Value);
            }
            else
            {
                vm = questService.UpdateGroup(id.Value, vm);
            }

            return View(vm);
        }

        /// <summary>
        /// 題目內容新增
        /// </summary>
        /// <param name="id">群組ID</param>
        /// <returns></returns>
        public ActionResult QuestionCreate(Guid? id, QuestQuestionVm vm, List<string> setItem)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index", "forum");
            }

            ViewData["infoId"] = questService.GetSingleGroupSimpleDetail(id.Value).InfoId;

            if (Request.HttpMethod == "GET")
            {
                return View(new QuestQuestionVm());
            }

            vm = questService.CreateQuestion(id.Value, vm, setItem);

            return RedirectToAction("GroupIndex", new { id = ViewData["infoId"] });
        }

        /// <summary>
        /// 題目內容Update
        /// </summary>
        /// <param name="id">題目ID</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult QuestionEdit(Guid? id, QuestQuestionVm vm, List<Guid> setItemId, List<string> setItem, List<Guid?> needGroup, List<Guid?> needQuestion, List<QuestNecessaryRelChangeVm> needVm)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index", "forum");
            }

            if (Request.HttpMethod == "GET")
            {
                vm = questService.GetQuestQuestionSimpleDetailList(id.Value);
            }
            else
            {
                //questService.UpdateNecessaryRel(id.Value, setItemId, needGroup, needQuestion);
                var relData = questService.UpdateNecessaryRel(id.Value, needVm);
                vm = questService.UpdateQuestion(id.Value, vm, setItemId, setItem);
            }

            #region 取題目關聯性下拉選單資料 
            //取出下拉式選單顯示題組題目
            var groups = questService.GetGroupsByQstId(id.Value);
            //順便抓取返回的InfoId
            ViewData["infoId"] = groups.FirstOrDefault()?.InfoId;
            //移除自己
            foreach (var gp in groups)
            {
                if (gp.MdQuestions.Any(x => x.QstId == id.Value))
                {
                    gp.MdQuestions = gp.MdQuestions.Where(x => x.QstId != id.Value);
                    break;
                }
            }
            //移除空題組
            groups.RemoveAll(x => !x.MdQuestions.Any());
            ViewData["ddlData"] = Newtonsoft.Json.JsonConvert.SerializeObject(groups.Select(x => new
            {
                GroupId = x.GroupId,
                GroupTitle = x.GroupTitle,
                MdQuestions = x.MdQuestions.Select(y => new { QstId = y.QstId, QstQuestion = y.QstQuestion })
            }));
            #endregion 取題目關聯性下拉選單資料 

            //題目關聯性 
            ViewData["relData"] = questService.GetNecessaryRel(id.Value);

            return View(vm);
        }

        #region 問卷結果

        /// <summary>
        /// 問卷結果
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public ActionResult Gather(Guid id)
        {
            var infoData = questService.GetQuestGatherInfo(id);
            ViewBag.infoData = infoData;

            var data = questService.GetQuestGather(id);
            return View(data);
        }

        /// <summary>
        /// 檢視填寫-
        /// </summary>
        /// <param name="key">id:infoId</param>
        /// <returns></returns>
        public ActionResult Examine(SearchVm key)
        {
            ViewBag.key = key;
            var infoData = questService.GetQuestGatherInfo(key.id);
            ViewBag.infoData = infoData;

            var data = questService.GetQuestGatherExamine(key.id);
            var vmD = data.ToPagedList(key.Page, 20);

            return View(vmD);
        }

        /// <summary>
        /// 檢視填寫-ajax
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AjaxExamine()
        {
            return View();
        }

        /// <summary>
        /// 檢視填寫-刪除
        /// </summary>
        /// <param name="id">infoId</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public ActionResult ExamineRemove(Guid id, string userId)
        {
            var data = questService.GetQuestGatherExamineRemove(id, userId);
            return RedirectToAction("Examine", new { id = id });
        }

        /// <summary>
        /// 檢視填寫-單筆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public ActionResult Detail(Guid id, string userId)
        {
            var data = questService.GetQuestGatherExamineDetail(id, userId);
            return View(data);
        }

        /// <summary>
        /// 匯出
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public FileResult Excel(Guid id)
        {
            var data = questService.GetQuestGatherExcel(id);
            return File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "問卷報表.xlsx");
        }

        #endregion

    }
}