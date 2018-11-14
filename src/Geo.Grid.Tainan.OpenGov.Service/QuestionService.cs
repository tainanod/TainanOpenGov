using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using OfficeOpenXml;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class QuestionService : Interface.IQuestionService
    {
        private IRepository<MdQuestInfo> repository;
        private OpenGovContext db;
        private string _userId = new IdentityProfile().GetIdentityProfile();
        private string _anonymousId = new IdentityProfile().GetIdentityProfileAnonymousId();
        private string _urlName = new WebSite().GetWebSite();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mdQuestInfoRepository"></param>
        public QuestionService(IRepository<MdQuestInfo> mdQuestInfoRepository)
        {
            repository = mdQuestInfoRepository;
            db = repository.Db;
        }

        public QuestionService() : base()
        {
        }

        /// <summary>
        /// 取得問卷資訊
        /// </summary>
        /// <param name="infoId">問卷ID</param>
        /// <param name="fillUserId">填寫人ID 不傳就取題目</param>
        /// <returns></returns>
        public QuestInfoVm GetQuestDetail(Guid infoId, Guid? fillUserId = null)
        {
            var ret = db.MdQuestInfo.Include("MdQuestionGroups").Include("MdQuestionGroups.MdQuestions").Include("MdQuestionGroups.MdQuestions.MdQuestSetItems").Include("MdQuestionGroups.MdQuestions.MdFillQuests").Where(i => i.InfoId == infoId && i.IsEnable && i.InfoValid).Select(i => new QuestInfoVm
            {
                InfoDesc = i.InfoDesc,
                InfoFooter = i.InfoFooter,
                InfoId = i.InfoId,
                InfoTitle = i.InfoTitle,
                VerifyType = i.VerifyType,
                QuestGroupVms = i.MdQuestionGroups.Where(g => g.IsEnable).OrderBy(g => g.GroupSort).Select(g => new QuestGroupVm
                {
                    GroupDesc = g.GroupDesc,
                    GroupId = g.GroupId,
                    GroupSort = g.GroupSort,
                    GroupTitle = g.GroupTitle,
                    MdQuestions = g.MdQuestions.Where(q => q.IsEnable).OrderBy(q => q.QstSorting).Select(q => new QuestQuestionVm
                    {
                        QstAnsType = q.QstAnsType,
                        IsRequired = q.IsRequired,
                        QstId = q.QstId,
                        QstQuestion = q.QstQuestion,
                        QstSorting = q.QstSorting,
                        QuestSetItemVms = q.MdQuestSetItems.Where(s => s.IsEnable).OrderBy(s => s.SetSorting).Select(s => new QuestSetItemVm
                        {
                            SetSorting = s.SetSorting,
                            SetScore = s.SetScore,
                            SetNote = s.SetNote,
                            SetDesc = s.SetDesc,
                            SetId = s.SetId,
                            MdQuestNecessaryRel = s.MdQuestNecessaryRel
                        })
                    })
                })
            }).Single();


            return ret;
        }

        /// <summary>
        /// 填寫問卷
        /// </summary>
        /// <param name="infoId">問卷ID</param>
        /// <param name="userId">填寫人ID</param>
        /// <param name="vm">填寫選項VM</param>
        /// <returns></returns>
        public bool FillQuest(Guid infoId, Guid userId, List<PostQuestFillVm> vm)
        {
            if (vm == null || !vm.Any())
            {
                return false;
            }

            var filledIds = vm.Select(x => x.SetId);

            var setInfo = db.MdQuestSetItem.Where(x => filledIds.Contains(x.SetId)).ToList();

            var fillSet = db.MdFillQuest;

            var deleteSet = fillSet.Where(x => x.MdFillUserId == userId && x.InfoId == infoId).AsEnumerable();

            for (int i = deleteSet.Count() - 1; i >= 0; i--)
            {
                fillSet.Remove(deleteSet.ElementAt(i));
            }

            foreach (var fillvm in vm)
            {
                fillSet.Add(new MdFillQuest
                {
                    FillDesc = fillvm.FillDesc,
                    FillId = Guid.NewGuid(),
                    InfoId = infoId,
                    MdFillUserId = userId,
                    SetId = fillvm.SetId,
                    QstId = setInfo.Single(x => x.SetId == fillvm.SetId).QstId
                });
            }

            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 檢查是否已經填過問卷(暫時用)
        /// </summary>
        /// <param name="infoId">問卷Ｉd</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckUserFilled(Guid infoId, Guid userId)
        {
            return db.MdFillQuest.FirstOrDefault(x => x.InfoId == infoId && x.MdFillUserId == userId) != null;
        }

        #region 問卷管理-填寫-New

        /// <summary>
        /// 問卷列表-是否有填寫過
        /// </summary>
        /// <param name="forumId">公民論壇ID</param>
        /// <returns></returns>
        public IQueryable<QuestInfoVm> GetQuestInfo(Guid forumId)
        {
            var data = (from x in db.MdQuestInfo
                        where x.ForumId == forumId && x.InfoValid && x.IsEnable
                        join f in db.MdFillQuest on new { InfoId = x.InfoId, MdFillUserId = new Guid(_userId) } equals new { f.InfoId, f.MdFillUserId } into pf
                        join h in db.MdCheck on new { InfoId = x.InfoId, UserId = _anonymousId } equals new { h.InfoId, h.UserId } into ph
                        orderby x.EditDate descending
                        select new QuestInfoVm()
                        {
                            InfoId = x.InfoId,
                            InfoTitle = x.InfoTitle,
                            InfoDateSt = x.InfoDateSt,
                            InfoDateEnd = x.InfoDateEnd,
                            ForumId = x.ForumId,
                            EditDate = x.EditDate,
                            InfoValid = x.InfoValid,
                            IsGather = x.IsGather,
                            IsComplete = pf.FirstOrDefault() != null || ph.FirstOrDefault() != null
                        });
            return data;
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public QuestInfoVm GetQuestDetailNew(Guid id)
        {
            var fillData = db.MdFillQuest.Where(x => x.InfoId == id && x.MdFillUserId.ToString() == _userId);

            var data = db.MdQuestInfo.Include("MdQuestionGroups").Include("MdQuestionGroups.MdQuestions")
                .Include("MdQuestionGroups.MdQuestions.MdQuestSetItems")
                .Include("MdQuestionGroups.MdQuestions.MdFillQuests")
                .Where(x => x.InfoId == id && x.IsEnable && x.InfoValid)
                .Select(x => new QuestInfoVm()
                {
                    InfoId = x.InfoId,
                    InfoTitle = x.InfoTitle,
                    InfoDesc = x.InfoDesc,
                    InfoFooter = x.InfoFooter,
                    InfoDateSt = x.InfoDateSt,
                    InfoDateEnd = x.InfoDateEnd,
                    VerifyType = x.VerifyType,
                    IsGather = x.IsGather,
                    IsComplete = fillData.FirstOrDefault() != null,
                    QuestGroupVms = x.MdQuestionGroups.OrderBy(c => c.GroupSort).Where(c => c.IsEnable).Select(c => new QuestGroupVm()
                    {
                        GroupId = c.GroupId,
                        GroupTitle = c.GroupTitle,
                        GroupDesc = c.GroupDesc,
                        GroupSort = c.GroupSort,
                        MdQuestions = c.MdQuestions.OrderBy(q => q.QstSorting).Where(q => q.IsEnable).Select(q => new QuestQuestionVm()
                        {
                            QstId = q.QstId,
                            QstQuestion = q.QstQuestion,
                            QstAnsType = q.QstAnsType,
                            QstSorting = q.QstSorting,
                            IsRequired = q.IsRequired,
                            QuestSetItemVms = q.MdQuestSetItems.OrderBy(s => s.SetSorting).Where(s => s.IsEnable).Select(s => new QuestSetItemVm()
                            {
                                SetId = s.SetId,
                                SetDesc = s.SetDesc,
                                SetNote = s.SetNote,
                                SetSorting = s.SetSorting,
                                SetScore = s.SetScore,
                                MdQuestNecessaryRel = s.MdQuestNecessaryRel
                            })
                        })
                    })
                });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 檢查-單筆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <param name="userMail">userMail</param>
        /// <returns></returns>
        public Result<string> GetQuestDetailCheckNew(Guid id, string userMail)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var info = db.MdQuestInfo.Find(id);
                if (info == null)
                {
                    result.Message = "請重新操作";
                    return result;
                }
                userMail = (info.VerifyType == 2 ? userMail : _userId);
                switch (info.VerifyType)
                {
                    case 1:
                        break;
                    case 2:
                        bool verify = GetVerifyCheck(id);
                        if (verify)
                        {
                            result.Message = "您已填寫過了";
                            return result;
                        }
                        break;
                    case 3:
                        if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            result.Message = "請登入會員";
                            return result;
                        }
                        break;
                }

                //待處理
                var today = DateTime.Today;
                var data = (from x in db.MdQuestInfo
                            where x.InfoId == id && x.IsEnable && x.InfoValid
                            //&& (x.InfoDateSt <= today || !x.InfoDateSt.HasValue)
                            //&& (x.InfoDateEnd >= today || !x.InfoDateEnd.HasValue)
                            join f in db.MdFillQuest on new { InfoId = x.InfoId, MdFillUserEmail = userMail } equals new { f.InfoId, f.MdFillUserEmail } into pf
                            select new QuestInfoVm()
                            {
                                InfoId = x.InfoId,
                                InfoDateSt = x.InfoDateSt,
                                InfoDateEnd = x.InfoDateEnd,
                                IsComplete = pf.FirstOrDefault() != null
                            }).FirstOrDefault();

                if (data == null)
                {
                    result.Message = "請重新操作!";
                    return result;
                }

                if (!(data.InfoDateSt <= today || !data.InfoDateSt.HasValue) || !(data.InfoDateEnd >= today || !data.InfoDateEnd.HasValue))
                {
                    result.Message = "已超過填寫時間";
                    result.Data = "已超過填寫時間";
                    return result;
                }

                if (data.IsComplete)
                {
                    result.Message = "您已填過了";
                    result.Data = "您已填過了";
                    return result;
                }

                result.Success = true;

            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 填寫問卷-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetQuestFillCreateNew(QuestSaveVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                DateTime today = DateTime.Today;
                if (vmD != null)
                {
                    //確認狀態
                    var check = GetQuestDetailCheckNew(vmD.InfoId, vmD.UserMail);
                    if (!check.Success)
                    {
                        result.Message = check.Message;
                        return result;
                    }

                    var data = db.MdQuestInfo.Find(vmD.InfoId);

                    //email驗證
                    if (data.VerifyType == 2)
                    {
                        if (string.IsNullOrEmpty(vmD.UserMail))
                        {
                            result.Message = "請確認Email是否正確";
                            return result;
                        }

                        var verify = GetVerifyCheck(data.InfoId, vmD.UserMail);
                        if (verify)
                        {
                            result.Message = "您已填過了";
                            return result;
                        }
                        
                    }

                    vmD.UserMail = (data.VerifyType == 2 ? vmD.UserMail : _userId);

                    db.MdCheck.Add(new MdCheck()
                    {
                        InfoId = vmD.InfoId,
                        UserEmail = vmD.UserMail,
                        UserId = _anonymousId
                    });

                    if (vmD.SetItemArray != null)
                    {
                        //取得正確的相關id
                        var setData = GetQuestFillCreateCheck(vmD.SetItemArray.ToList());
                        foreach (var item in vmD.SetItemArray)
                        {
                            var set = setData.Data.FirstOrDefault(x => x.SetId == item.SetId);
                            db.MdFillQuest.Add(new MdFillQuest()
                            {
                                FillId = Guid.NewGuid(),
                                InfoId = set.InfoId,
                                MdFillUserId = new Guid(_userId),
                                MdFillUserEmail = vmD.UserMail,
                                QstId = set.QstId,
                                SetId = item.SetId,
                                FillDesc = item.FillDesc,
                                FillScore = 0,
                                EditDate = DateTime.Now,
                                IsEnable = true
                            });
                        }
                    }
                    db.SaveChanges();
                    if (data.VerifyType == 2)
                    {
                        result.Message = "您己完成問卷，請至mail信箱點選驗證信。";
                    }
                    else
                    {
                        result.Message = "您已填寫完畢，感謝您的填寫。";
                    }
                    result.Success = true;
                    result.ID = vmD.InfoId;

                    if (data.VerifyType == 2)
                    {
                        GetVerifyCheckEmailOne(data.InfoId);
                    }

                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        ///// <summary>
        ///// 填寫問卷
        ///// </summary>
        ///// <param name="vmD">參數</param>
        ///// <returns></returns>
        //public Result<string> GetQuestFillCreateNew(List<PostQuestFillVm> vmD)
        //{
        //    Result<string> result = new Result<string>(false);
        //    try
        //    {
        //        DateTime today = DateTime.Today;
        //        //取得正確的相關id
        //        var setData = GetQuestFillCreateCheck(vmD);

        //        //確認是否為同一份問卷
        //        if (setData.Success)
        //        {
        //            var infoId = setData.ID;
        //            var infoData = db.MdQuestInfo.Where(x => x.InfoValid && x.IsEnable && x.InfoId == infoId
        //            && (x.InfoDateSt <= today || x.InfoDateSt == null)
        //            && (x.InfoDateEnd >= today || x.InfoDateEnd == null)).FirstOrDefault();
        //            if (infoData != null)
        //            {
        //                //檢查是否已填過
        //                var fillData = db.MdFillQuest.FirstOrDefault(x => x.MdFillUserEmail.ToString() == _userId && x.InfoId == infoId);
        //                if (fillData != null)
        //                {
        //                    result.Message = "您已填寫過了";
        //                    return result;
        //                }

        //                //開始儲存
        //                foreach (var item in vmD)
        //                {
        //                    var set = setData.Data.FirstOrDefault(x => x.SetId == item.SetId);
        //                    db.MdFillQuest.Add(new MdFillQuest()
        //                    {
        //                        FillId = Guid.NewGuid(),
        //                        InfoId = set.InfoId,
        //                        MdFillUserId = new Guid(_userId),
        //                        MdFillUserEmail = _userId,
        //                        QstId = set.QstId,
        //                        SetId = set.SetId,
        //                        FillDesc = item.FillDesc,
        //                        FillScore = 0,
        //                        EditDate = DateTime.Now,
        //                        Editer = new Guid(_userId),
        //                        IsEnable = true
        //                    });
        //                }
        //                db.SaveChanges();
        //                result.Success = true;
        //                result.ID = infoId;
        //            }
        //        }
        //        else
        //        {
        //            result.Message = "請確認是否為同一份問卷";
        //            return result;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        result.Message = e.Message;
        //    }
        //    return result;
        //}

        /// <summary>
        /// 填寫問卷-檢查回傳的set是否為正確
        /// </summary>
        /// <param name="vmD">post set</param>
        /// <returns></returns>
        private Result<IQueryable<QuestCreateSetItemVm>> GetQuestFillCreateCheck(List<PostQuestFillVm> vmD)
        {
            Result<IQueryable<QuestCreateSetItemVm>> result = new Result<IQueryable<QuestCreateSetItemVm>>(false);
            try
            {
                if (vmD != null)
                {
                    Guid[] guidArray = vmD.Select(x => x.SetId).ToArray();
                    //取得正確的相關id
                    var data = db.MdQuestSetItem.Where(x => guidArray.Contains(x.SetId)).Select(x => new QuestCreateSetItemVm()
                    {
                        SetId = x.SetId,
                        QstId = x.QstId,
                        GroupId = x.MdQuestion.GroupId,
                        InfoId = x.MdQuestion.MdQuestionGroup.InfoId
                    });

                    result.Data = data;
                    result.Success = data.Select(c => c.InfoId).Distinct().Count() == 1;
                    result.ID = data.FirstOrDefault().InfoId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        #endregion

        #region 第二階段驗證

        /// <summary>
        /// 檢查是否已填過
        /// true:已填過
        /// </summary>
        /// <param name="infoId">infoId</param>
        /// <returns></returns>
        private bool GetVerifyCheck(Guid infoId)
        {
            var data = db.MdCheck.FirstOrDefault(x => x.InfoId == infoId && x.UserId == _anonymousId);
            return data != null;
        }

        /// <summary>
        /// 檢查是否已填過
        /// true:已填過
        /// </summary>
        /// <param name="infoId">問卷編號</param>
        /// <param name="userMail">userMail</param>
        /// <returns></returns>
        private bool GetVerifyCheck(Guid infoId, string userMail)
        {
            if (!VerifyRegex.VerifyEmail(userMail))
            {
                return false;
            }

            var data = db.MdCheck.FirstOrDefault(x => x.InfoId == infoId && (x.UserId == _anonymousId || x.UserEmail == userMail));
            return data != null;
        }

        /// <summary>
        /// 傳送Email驗證信件
        /// </summary>
        /// <param name="infoId">問卷id</param>
        /// <returns></returns>
        private Result<string> GetVerifyCheckEmailOne(Guid infoId)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = db.MdCheck.FirstOrDefault(x => x.InfoId == infoId && x.UserId == _anonymousId);
                if (data != null)
                {
                    string urlName = _urlName;
                    string mailBody = FileToHtml.GetFileHtml("~/App_Data/ForumQuestionEmail.html").Data;
                    mailBody = mailBody.Replace(@"{{urlName}}", urlName + "/Question/_Check/" + infoId);

                    var mailData = new WaitingMail();
                    mailData.TypeName = "公民論壇-問卷管理-第二階段驗證Email";
                    mailData.ToName = _anonymousId;
                    mailData.ToEmail = data.UserEmail;
                    mailData.Subject = "臺南開放政府-您有一封問卷驗證信件" + DateTime.Today.ToString("(yyyy/MM/dd)");
                    mailData.MailContent = mailBody;

                    db.WaitingMail.Add(mailData);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 回傳Email驗證是否為有效
        /// </summary>
        /// <param name="infoId">infoId</param>
        /// <returns></returns>
        public Result<string> GetVerifyCheckEmailTwo(Guid infoId)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = db.MdCheck.FirstOrDefault(x => x.InfoId == infoId && x.UserId == _anonymousId);
                if (data != null)
                {
                    data.IsEnabled = true;
                    db.SaveChanges();

                    result.Success = true;
                    result.ID = infoId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        #endregion

        #region Admin後台

        /// <summary>
        /// 取得開放論壇底下的問卷清單
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        public List<QuestInfoVm> GetQustionList(Guid forumId)
        {
            var ret = db.MdQuestInfo.Where(x => x.ForumId == forumId && x.IsEnable).Select(x => new QuestInfoVm
            {
                InfoId = x.InfoId,
                InfoTitle = x.InfoTitle,
                EditDate = x.EditDate,
                InfoDateSt = x.InfoDateSt,
                InfoDateEnd = x.InfoDateEnd,
                IsGather = x.IsGather,
                InfoValid = x.InfoValid
            }).ToList();

            return ret;
        }

        /// <summary>
        /// 問卷資訊-刪除
        /// 回傳ForumId
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public Result<string> GetQuestionInfoRemove(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = db.MdQuestInfo.Find(id);
                if (data != null)
                {
                    data.IsEnable = false;
                    db.SaveChanges();

                    result.Success = true;
                    result.ID = data.ForumId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 取得單一問卷資訊(不包含底下的群組題目)
        /// </summary>
        /// <param name="infoID"></param>
        /// <returns></returns>
        public QuestInfoVm GetSingleQuestInfoSimpleDetail(Guid infoID)
        {
            var ret = db.MdQuestInfo.Where(x => x.InfoId == infoID && x.IsEnable).OrderBy(x => x.EditDate).Select(x => new QuestInfoVm
            {
                InfoId = x.InfoId,
                InfoTitle = x.InfoTitle,
                EditDate = x.EditDate,
                InfoDateSt = x.InfoDateSt,
                InfoDateEnd = x.InfoDateEnd,
                InfoDesc = x.InfoDesc,
                InfoFooter = x.InfoFooter,
                ForumId = x.ForumId,
                InfoValid = x.InfoValid,
                IsGather = x.IsGather,
                VerifyType = x.VerifyType
            }).Single();

            return ret;
        }

        /// <summary>
        /// 取得問卷群組題目(不包含選項)
        /// </summary>
        /// <param name="infoID">InfoID</param>
        /// <returns></returns>
        public List<QuestGroupVm> GetQuestGroupSimpleDetailList(Guid infoID)
        {
            var ret = db.MdQuestionGroup.Where(x => x.InfoId == infoID && x.IsEnable).OrderBy(x => x.GroupSort).Select(x => new QuestGroupVm
            {
                GroupId = x.GroupId,
                GroupDesc = x.GroupDesc,
                GroupSort = x.GroupSort,
                GroupTitle = x.GroupTitle,
                MdQuestions = x.MdQuestions.Where(y => y.IsEnable).OrderBy(y => y.QstSorting).Select(y => new QuestQuestionVm
                {
                    QstQuestion = y.QstQuestion,
                    QstId = y.QstId,
                    QstSorting = y.QstSorting
                })
            }).ToList();

            return ret;
        }

        /// <summary>
        /// 取得單一題組資訊(不含題目選項)
        /// </summary>
        /// <param name="groupId">群組ID</param>
        /// <returns></returns>
        public QuestGroupVm GetSingleGroupSimpleDetail(Guid groupId)
        {
            return db.MdQuestionGroup.Where(x => x.GroupId == groupId && x.IsEnable).Select(x => new QuestGroupVm
            {
                InfoId = x.InfoId,
                GroupDesc = x.GroupDesc,
                GroupId = x.GroupId,
                GroupSort = x.GroupSort,
                GroupTitle = x.GroupTitle,
            }).Single();
        }

        /// <summary>
        /// 新增題組
        /// </summary>
        /// <param name="infoId">題組所屬問卷ID</param>
        /// <param name="vm">題組VM</param>
        /// <returns></returns>
        public QuestGroupVm CreateGroup(Guid infoId, QuestGroupVm vm)
        {
            var questInfo = db.MdQuestInfo.Single(x => x.InfoId == infoId && x.IsEnable);

            var maxSort = questInfo.MdQuestionGroups.Any() ? questInfo.MdQuestionGroups.Max(x => x.GroupSort) : 0;

            vm.GroupId = Guid.NewGuid();

            questInfo.MdQuestionGroups.Add(new MdQuestionGroup
            {
                GroupId = vm.GroupId,
                EditDate = DateTime.Now,
                GroupDesc = vm.GroupDesc,
                GroupSort = maxSort + 1,
                IsEnable = true,
                GroupTitle = vm.GroupTitle,
            });

            db.SaveChanges();
            return vm;
        }

        /// <summary>
        /// 修改題組
        /// </summary>
        /// <param name="groupId">題組ID</param>
        /// <param name="vm">題組VM</param>
        /// <returns></returns>
        public QuestGroupVm UpdateGroup(Guid groupId, QuestGroupVm vm)
        {
            var entity = db.MdQuestionGroup.Single(x => x.GroupId == groupId && x.IsEnable);
            entity.EditDate = DateTime.Now;
            entity.GroupDesc = vm.GroupDesc;
            entity.GroupTitle = vm.GroupTitle;

            db.SaveChanges();
            return vm;
        }

        /// <summary>
        /// 刪除群組
        /// </summary>
        /// <param name="gourpId">題組ID</param>
        /// <returns></returns>
        public bool DeleteGroup(Guid groupId)
        {
            var entity = db.MdQuestionGroup.Single(x => x.GroupId == groupId && x.IsEnable);
            entity.GroupSort = -1;
            entity.IsEnable = false;
            entity.EditDate = DateTime.Now;

            //取出所有的有效題組 重刷Sort
            var groups = entity.MdQuestInfo.MdQuestionGroups.Where(x => x.IsEnable).OrderBy(x => x.GroupSort);
            if (groups.Any())
            {
                for (var i = 0; i < groups.Count(); groups.ElementAt(i).GroupSort = ++i) ;
            }

            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 新增題目
        /// </summary>
        /// <param name="groupId">題組ID</param>
        /// <param name="vm">問題VM</param>
        /// <param name="setItem">選項字串</param>
        /// <returns></returns>
        public QuestQuestionVm CreateQuestion(Guid groupId, QuestQuestionVm vm, List<string> setItem)
        {
            var now = DateTime.Now;

            var group = db.MdQuestionGroup.Single(x => x.GroupId == groupId && x.IsEnable);
            var qst = new MdQuestion
            {
                EditDate = now,
                IsEnable = true,
                IsRequired = vm.IsRequired,
                QstAnsType = vm.QstAnsType,
                QstId = Guid.NewGuid(),
                QstQuestion = vm.QstQuestion,
                QstSorting = group.MdQuestions.Any(x => x.IsEnable) ? group.MdQuestions.Where(x => x.IsEnable).Max(x => x.QstSorting) + 1 : 1
            };

            //新增問題選項
            AddSetItem(qst, setItem, now);
            group.MdQuestions.Add(qst);
            db.SaveChanges();
            vm.QstId = qst.QstId;
            vm.QuestSetItemVms = qst.MdQuestSetItems.Select(x => new QuestSetItemVm { SetNote = x.SetNote });
            return vm;
        }

        /// <summary>
        /// 新增問題選項
        /// </summary>
        /// <param name="question">問題 Entity</param>
        /// <param name="setItem">選項文字集合</param>
        /// <param name="now">現在時間</param>
        private void AddSetItem(MdQuestion question, List<string> setItem, DateTime now)
        {
            int i = 1;

            switch (question.QstAnsType)
            {
                case 1:
                case 2:

                    //單選是非
                    //複選
                    question.MdQuestSetItems = setItem.Select(x => new MdQuestSetItem
                    {
                        EditDate = now,
                        IsEnable = true,
                        SetDesc = x,
                        SetId = Guid.NewGuid(),
                        SetNote = false, //之後勢必又要改  o其他______沒有設想到
                        SetSorting = i++,
                    }).ToList();
                    break;
                case 3:
                    //問答題 (只加一個選項)
                    question.MdQuestSetItems.Add(new MdQuestSetItem
                    {
                        EditDate = now,
                        IsEnable = true,
                        SetId = Guid.NewGuid(),
                        SetNote = true,
                        SetSorting = 1
                    });
                    break;
                case 4:
                    //多重問答

                    question.MdQuestSetItems = setItem.Select(x => new MdQuestSetItem
                    {
                        EditDate = now,
                        IsEnable = true,
                        SetDesc = x,
                        SetId = Guid.NewGuid(),
                        SetNote = true,
                        SetSorting = i++,
                    }).ToList();
                    break;
            }
        }

        /// <summary>
        /// 刪除題目
        /// </summary>
        /// <param name="qstId">題目ID</param>
        /// <returns></returns>
        public bool DeleteQuestion(Guid qstId)
        {
            var entity = db.MdQuestion.Single(x => x.QstId == qstId && x.IsEnable);

            entity.IsEnable = false;
            entity.EditDate = DateTime.Now;
            entity.QstSorting = -1;

            //取出所有的有效題目 重刷Sort
            var qsts = entity.MdQuestionGroup.MdQuestions.Where(x => x.IsEnable).OrderBy(x => x.QstSorting);
            if (qsts.Any())
            {
                for (var i = 0; i < qsts.Count(); qsts.ElementAt(i).QstSorting = ++i) ;
            }

            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 新增/修改問卷資訊
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="questinfoVm"></param>
        /// <param name="ckbDataFill"></param>
        /// <returns></returns>
        public QuestInfoVm UpdateQuestInfo(Guid forumId, QuestInfoVm questinfoVm, List<string> ckbDataFill)
        {
            var now = DateTime.Now;

            if (questinfoVm.InfoId == Guid.Empty)
            {
                //create
                questinfoVm.InfoId = Guid.NewGuid();
                var questInfoEntity = new MdQuestInfo
                {
                    EditDate = now,
                    ForumId = forumId,
                    InfoDateEnd = questinfoVm.InfoDateEnd,
                    InfoDateSt = questinfoVm.InfoDateSt,
                    InfoDesc = questinfoVm.InfoDesc,
                    InfoFooter = questinfoVm.InfoFooter,
                    InfoId = questinfoVm.InfoId,
                    InfoTitle = questinfoVm.InfoTitle,
                    IsEnable = true,
                    InfoValid = questinfoVm.InfoValid,
                    IsGather = questinfoVm.IsGather,
                    VerifyType = questinfoVm.VerifyType
                };

                if (ckbDataFill != null && ckbDataFill.Any())
                {
                    var group = new MdQuestionGroup
                    {
                        EditDate = now,
                        GroupId = Guid.NewGuid(),
                        GroupSort = 1,
                        GroupTitle = "個人基本資料",
                        IsEnable = true,
                    };

                    //快速新增個人資料
                    AddPersonDataQuest(group, ckbDataFill);

                    questInfoEntity.MdQuestionGroups.Add(group);
                }

                db.MdQuestInfo.Add(questInfoEntity);
            }
            else
            {
                var entity = db.MdQuestInfo.Single(x => x.InfoId == questinfoVm.InfoId && x.IsEnable);

                entity.EditDate = now;
                entity.InfoDateEnd = questinfoVm.InfoDateEnd;
                entity.InfoDateSt = questinfoVm.InfoDateSt;
                entity.InfoDesc = questinfoVm.InfoDesc;
                entity.InfoFooter = questinfoVm.InfoFooter;
                entity.InfoTitle = questinfoVm.InfoTitle;
                //entity.VerifyType = questinfoVm.VerifyType;
                entity.InfoValid = questinfoVm.InfoValid;
                entity.IsGather = questinfoVm.IsGather;
            }

            db.SaveChanges();
            questinfoVm.EditDate = now;
            return questinfoVm;
        }

        #region 快速新增個人資料
        private void AddPersonDataQuest(MdQuestionGroup group, List<string> ckbDataFill)
        {
            MdQuestion question;
            var now = DateTime.Now;
            ckbDataFill.ForEach(x =>
            {
                switch (x)
                {
                    case "0":
                        {
                            question = new MdQuestion
                            {
                                EditDate = now,
                                IsEnable = true,
                                IsRequired = true,
                                QstAnsType = (int)QstAnsType.單選題,
                                QstId = Guid.NewGuid(),
                                QstQuestion = "性別",
                                MdQuestSetItems = new List<MdQuestSetItem>
                                {
                                    NewSetItem(now, "男", 1),
                                    NewSetItem(now, "女", 2)
                                }
                            };
                            break;
                        }
                    case "1":
                        {
                            question = new MdQuestion
                            {
                                EditDate = now,
                                IsEnable = true,
                                IsRequired = true,
                                QstAnsType = (int)QstAnsType.問答題,
                                QstId = Guid.NewGuid(),
                                QstQuestion = "年齡",
                                MdQuestSetItems = new List<MdQuestSetItem>
                                {
                                    NewSetItem(now, null, 1, true)
                                }
                            };
                            break;
                        }
                    case "2":
                        {
                            question = new MdQuestion
                            {
                                EditDate = now,
                                IsEnable = true,
                                IsRequired = true,
                                QstAnsType = (int)QstAnsType.單選題,
                                QstId = Guid.NewGuid(),
                                QstQuestion = "職業",
                                MdQuestSetItems = new List<MdQuestSetItem>
                                {
                                    NewSetItem(now, "學生", 1),
                                    NewSetItem(now, "軍公教", 2),
                                    NewSetItem(now, "金融業", 3),
                                    NewSetItem(now, "商業", 4),
                                    NewSetItem(now, "製造業", 5),
                                    NewSetItem(now, "營造業", 6),
                                    NewSetItem(now, "電子業", 7),
                                    NewSetItem(now, "服務業", 8),
                                    NewSetItem(now, "大眾傳播業", 9),
                                    NewSetItem(now, "運輸業", 10),
                                    NewSetItem(now, "資訊業", 11),
                                    NewSetItem(now, "通信業", 12),
                                    NewSetItem(now, "農林漁牧業", 13),
                                    NewSetItem(now, "醫務人員", 14),
                                    NewSetItem(now, "自由業", 15),
                                    NewSetItem(now, "家管", 16),
                                    NewSetItem(now, "待業中", 17),
                                    NewSetItem(now, "已退休", 18),
                                    NewSetItem(now, "其他", 19, true)
                                }
                            };
                            break;
                        }
                    case "3":
                        {
                            question = new MdQuestion
                            {
                                EditDate = now,
                                IsEnable = true,
                                IsRequired = true,
                                QstAnsType = (int)QstAnsType.單選題,
                                QstId = Guid.NewGuid(),
                                QstQuestion = "學歷",
                                MdQuestSetItems = new List<MdQuestSetItem>
                                {
                                    NewSetItem(now, "國小(肄)畢業或以下", 1),
                                    NewSetItem(now, "國(初)中", 2),
                                    NewSetItem(now, "高中/職", 3),
                                    NewSetItem(now, "專科", 4),
                                    NewSetItem(now, "大學", 5),
                                    NewSetItem(now, "碩士", 6),
                                    NewSetItem(now, "博士", 7)
                                }
                            };
                            break;
                        }
                    case "4":
                        {
                            question = new MdQuestion
                            {
                                EditDate = now,
                                IsEnable = true,
                                IsRequired = true,
                                QstAnsType = (int)QstAnsType.單選題,
                                QstId = Guid.NewGuid(),
                                QstQuestion = "家庭年總所得",
                                MdQuestSetItems = new List<MdQuestSetItem>
                                {
                                    NewSetItem(now, "無工作收入", 1),
                                    NewSetItem(now, "50 萬元以下", 2),
                                    NewSetItem(now, "高於 50 萬~100 萬元以下", 3),
                                    NewSetItem(now, "高於 100 萬元~150 萬元以下", 4),
                                    NewSetItem(now, "高於 150 萬元~200 萬元以下", 5),
                                    NewSetItem(now, "高於 200 萬元", 6),
                                }
                            };
                            break;
                        }
                    case "5":
                        {
                            //居住地(台灣縣市/鄉鎮)  未實作
                            question = new MdQuestion
                            {
                                EditDate = now,
                                IsEnable = true,
                                IsRequired = true,
                                QstAnsType = (int)QstAnsType.全台地址,
                                QstId = Guid.NewGuid(),
                                QstQuestion = "居住地(台灣縣市/鄉鎮)",
                                MdQuestSetItems = new List<MdQuestSetItem>
                                {
                                    NewSetItem(now, "", 1, true),
                                }
                            };
                            break;
                        }
                    case "6":
                        {
                            //居住地(限台南 / 縣市)
                            question = new MdQuestion
                            {
                                EditDate = now,
                                IsEnable = true,
                                IsRequired = true,
                                QstAnsType = (int)QstAnsType.台南地址,
                                QstId = Guid.NewGuid(),
                                QstQuestion = "居住地(限台南/縣市)",
                                MdQuestSetItems = new List<MdQuestSetItem>
                                {
                                    NewSetItem(now, "", 1, true),
                                }
                            };
                            break;
                        }
                    case "7":
                        {
                            //填寫地址詳細欄位
                            question = new MdQuestion
                            {
                                EditDate = now,
                                IsEnable = true,
                                IsRequired = true,
                                QstAnsType = (int)QstAnsType.問答題,
                                QstId = Guid.NewGuid(),
                                QstQuestion = "填寫地址詳細欄位",
                                MdQuestSetItems = new List<MdQuestSetItem>
                                {
                                    NewSetItem(now, "", 1, true),
                                }
                            };
                            break;
                        }
                    default:
                        {
                            question = null;
                            break;
                        }
                }
                group.MdQuestions.Add(question);
            });

            for (int i = 0; i < group.MdQuestions.Count(); group.MdQuestions.ElementAt(i).QstSorting = ++i) ;

        }

        private MdQuestSetItem NewSetItem(DateTime now, string setDesc, int setSorting, bool setNote = false, int setScore = 0)
        {
            return new MdQuestSetItem
            {
                EditDate = now,
                IsEnable = true,
                SetDesc = setDesc,
                SetId = Guid.NewGuid(),
                SetNote = setNote,
                SetScore = setScore,
                SetSorting = setSorting
            };
        }
        # endregion 快速新增個人資料

        /// <summary>
        /// 移動題目 題組順序
        /// </summary>
        /// <param name="id">題目/題組ID</param>
        /// <param name="type">題目:"qst" 題組:"grp"</param>
        /// <param name="up">true向上移 false向下移</param>
        /// <returns></returns>
        public bool ChangeSort(Guid id, string type, bool up)
        {
            if (type.Equals("qst", StringComparison.OrdinalIgnoreCase))
            {
                //題目
                //先取出自己
                var self = db.MdQuestion.Single(x => x.QstId == id);
                //判斷是否可以移動
                if (!CheckSort(self, up))
                {
                    return false;
                }

                //開始排序
                var dest = up ? self.MdQuestionGroup.MdQuestions.Single(x => x.QstSorting == self.QstSorting - 1) : self.MdQuestionGroup.MdQuestions.Single(x => x.QstSorting == self.QstSorting + 1);
                self.QstSorting += up ? -1 : 1;
                dest.QstSorting += up ? 1 : -1;
                return db.SaveChanges() > 0;
            }
            else if (type.Equals("grp", StringComparison.OrdinalIgnoreCase))
            {
                //題組
                //先取出自己
                var self = db.MdQuestionGroup.Single(x => x.GroupId == id);
                //判斷是否可以移動
                if (!CheckSort(self, up))
                {
                    return false;
                }

                //開始排序
                var dest = up ? self.MdQuestInfo.MdQuestionGroups.Single(x => x.GroupSort == self.GroupSort - 1) : self.MdQuestInfo.MdQuestionGroups.Single(x => x.GroupSort == self.GroupSort + 1);
                self.GroupSort += up ? -1 : 1;
                dest.GroupSort += up ? 1 : -1;
                return db.SaveChanges() > 0;
            }
            else if (type.Equals("item", StringComparison.OrdinalIgnoreCase))
            {
                //選項
            }

            return false;
        }

        /// <summary>
        /// 檢查是否可以移動Sort
        /// </summary>
        /// <returns></returns>
        private bool CheckSort<T>(T self, bool up) where T : class
        {
            if (typeof(T).Name == "MdQuestion")
            {
                var question = self as MdQuestion;
                if (up && question.QstSorting == 1)
                {
                    //向上但已經是第一個
                    return false;
                }
                else if (!up && (question.QstSorting == question.MdQuestionGroup.MdQuestions.Where(x => x.IsEnable).Max(x => x.QstSorting)))
                {
                    //向下但已經是最後一個
                    return false;
                }
                return true;
            }
            else if (typeof(T).Name == "MdQuestionGroup")
            {
                var group = self as MdQuestionGroup;
                if (up && group.GroupSort == 1)
                {
                    //向上但已經是第一個
                    return false;
                }
                else if (!up && (group.GroupSort == group.MdQuestInfo.MdQuestionGroups.Where(x => x.IsEnable).Max(x => x.GroupSort)))
                {
                    //向下但已經是最後一個
                    return false;
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// 取得題目選項資訊
        /// </summary>
        /// <param name="qstId">題目ID</param>
        /// <returns></returns>
        public QuestQuestionVm GetQuestQuestionSimpleDetailList(Guid qstId)
        {
            return db.MdQuestion.Include(x => x.MdQuestSetItems).Where(x => x.QstId == qstId && x.IsEnable).Select(x => new QuestQuestionVm
            {
                IsRequired = x.IsRequired,
                QstAnsType = x.QstAnsType,
                QstId = x.QstId,
                QstSorting = x.QstSorting,
                QstQuestion = x.QstQuestion,
                QuestSetItemVms = x.MdQuestSetItems.Where(y => y.IsEnable).OrderBy(y => y.SetSorting).Select(y => new QuestSetItemVm
                {
                    SetId = y.SetId,
                    SetDesc = y.SetDesc,
                    SetNote = y.SetNote,
                    SetScore = y.SetScore,
                    SetSorting = y.SetSorting
                })
            }).Single();
        }

        /// <summary>
        /// 修改問題與選項
        /// </summary>
        /// <param name="qstId">問題ID</param>
        /// <param name="vm">問題VM</param>
        /// <param name="setItemId">選項ID</param>
        /// <param name="setItem">選項文字</param>
        /// <returns></returns>
        public QuestQuestionVm UpdateQuestion(Guid qstId, QuestQuestionVm vm, List<Guid> setItemId, List<string> setItem)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    var now = DateTime.Now;
                    var qst = db.MdQuestion.Include("MdQuestSetItems").Include("MdQuestSetItems.MdQuestNecessaryRel")
                        .Single(x => x.QstId == qstId && x.IsEnable);

                    var necessary = db.MdQuestNecessaryRel.FirstOrDefault(x => x.MdQuestSetItem.MdQuestion.QstId == qstId);

                    qst.EditDate = now;
                    qst.IsRequired = (necessary != null ? true : vm.IsRequired);

                    qst.QstAnsType = vm.QstAnsType;
                    qst.QstQuestion = vm.QstQuestion;

                    ////刪除移除的選項
                    //var deleteIds = qst.MdQuestSetItems.Where(x => !setItemId.Contains(x.SetId) && x.IsEnable).Select(x => x.SetId).ToList();
                    //DeleteItemSet(qst, deleteIds, now);

                    //for (int i = 0; i < setItemId.Count(); i++)
                    //{
                    //    if (setItemId[i] == Guid.Empty)
                    //    {
                    //        //new item
                    //        qst.MdQuestSetItems.Add(new MdQuestSetItem
                    //        {
                    //            EditDate = now,
                    //            IsEnable = true,
                    //            SetDesc = setItem[i],
                    //            SetId = Guid.NewGuid(),
                    //            SetSorting = qst.MdQuestSetItems.Where(x => x.IsEnable).Max(x => x.SetSorting) + 1
                    //        });

                    //        continue;
                    //    }

                    //    var item = qst.MdQuestSetItems.Single(x => x.SetId == setItemId[i] && x.IsEnable);
                    //    item.EditDate = now;
                    //    item.SetDesc = setItem[i];
                    //}

                    //db.SaveChanges();

                    ////同時刪除已經填過該選項的資料
                    //var deletes = db.MdQuestion.Single(x => x.QstId == qstId).MdFillQuests.Where(x => deleteIds.Contains(x.SetId));
                    //for (var i = deletes.Count() - 1; i >= 0; i--)
                    //{
                    //    db.MdFillQuest.Remove(deletes.ElementAt(i));
                    //}

                    db.SaveChanges();
                    tran.Complete();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return GetQuestQuestionSimpleDetailList(qstId);
        }

        private void DeleteItemSet(MdQuestion question, IEnumerable<Guid> deleteIds, DateTime now)
        {
            foreach (var id in deleteIds)
            {
                var item = question.MdQuestSetItems.Single(x => x.SetId == id);
                item.SetSorting = -1;
                item.IsEnable = false;
                item.EditDate = now;
            }

            var tmp = question.MdQuestSetItems.Where(x => x.IsEnable).OrderBy(x => x.SetSorting);
            for (int i = 0; i < tmp.Count(); tmp.ElementAt(i).SetSorting = ++i) ;
        }

        /// <summary>
        /// 由題目ID取得所在問卷底下的所有群組題目
        /// <param name="qstId"></param>
        /// </summary>
        public List<QuestGroupVm> GetGroupsByQstId(Guid qstId)
        {
            var self = db.MdQuestion.Single(x => x.QstId == qstId && x.IsEnable);
            return self.MdQuestionGroup.MdQuestInfo.MdQuestionGroups.Where(x => x.IsEnable).OrderBy(x => x.GroupSort).Select(x => new QuestGroupVm
            {
                InfoId = x.InfoId,
                GroupTitle = x.GroupTitle,
                GroupId = x.GroupId,
                MdQuestions = x.MdQuestions.Where(y => y.IsEnable).OrderBy(y => y.QstSorting).Select(y => new QuestQuestionVm
                {
                    QstQuestion = y.QstQuestion,
                    QstId = y.QstId
                })
            }).ToList();
        }

        /// <summary>
        /// 跳題
        /// </summary>
        /// <param name="id">qstId</param>
        /// <param name="vmD">跳題</param>
        /// <returns></returns>
        public Result<string> UpdateNecessaryRel(Guid id, List<QuestNecessaryRelChangeVm> vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    //先刪除
                    var data = db.MdQuestSetItem.Where(x => x.QstId == id).Select(x => x.SetId).ToList();
                    var relData = db.MdQuestNecessaryRel.Where(x => data.Contains(x.SetId)).ToList();
                    db.MdQuestNecessaryRel.RemoveRange(relData);
                    //db.SaveChanges();

                    //後新增
                    if (vmD != null)
                    {
                        foreach (var item in vmD)
                        {
                            if (item.GroupId.HasValue)
                            {
                                db.MdQuestNecessaryRel.Add(new MdQuestNecessaryRel()
                                {
                                    SetId = item.SetId,
                                    TargetType = (item.NeedId != null ? 2 : 1),
                                    TargetId = (item.NeedId != null ? item.NeedId.Value : item.GroupId.Value)
                                });
                            }
                            db.SaveChanges();
                        }

                        result.Success = true;
                    }

                    tran.Complete();
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public void UpdateNecessaryRel(Guid qstId, List<Guid> setIds, List<Guid?> needGourpIds, List<Guid?> needQuestionIds)
        {
            for (var i = 0; i < setIds.Count(); i++)
            {
                var setId = setIds[i];
                var entity = db.MdQuestNecessaryRel.SingleOrDefault(x => x.SetId == setId);
                if (entity == null)
                {
                    //add
                    if (!needGourpIds[i].HasValue && !needQuestionIds[i].HasValue)
                    {
                        continue;
                    }

                    db.MdQuestNecessaryRel.Add(new MdQuestNecessaryRel
                    {
                        SetId = setIds[i],
                        TargetType = needQuestionIds[i].HasValue ? 2 : 1,
                        TargetId = needQuestionIds[i].HasValue ? needQuestionIds[i].Value : needGourpIds[i].Value
                    });
                }
                else
                {
                    if (!needGourpIds[i].HasValue && !needQuestionIds[i].HasValue)
                    {
                        //delete 刪除關聯性(選項還在 只是把關聯取消)
                        db.MdQuestNecessaryRel.Remove(entity);
                    }
                    else
                    {
                        //update
                        entity.TargetType = needQuestionIds[i].HasValue ? 2 : 1;
                        entity.TargetId = needQuestionIds[i].HasValue ? needQuestionIds[i].Value : needGourpIds[i].Value;
                    }
                }
            }
            //刪除移除的選項的關聯性
            var tmp = db.MdQuestSetItem.Where(x => x.QstId == qstId && !setIds.Contains(x.SetId));
            foreach (var t in tmp)
            {
                if (t.MdQuestNecessaryRel != null)
                {
                    db.MdQuestNecessaryRel.Remove(t.MdQuestNecessaryRel);
                }
            }

            db.SaveChanges();
        }

        public List<QuestNecessaryRelVm> GetNecessaryRel(Guid qstId)
        {
            var ret = new List<QuestNecessaryRelVm>();

            var tmp = db.MdQuestSetItem.Where(x => x.QstId == qstId && x.IsEnable).Select(x => new
            {
                SetId = x.SetId,
                NecessaryRel = x.MdQuestNecessaryRel
            }).ToList();

            tmp.ForEach(x =>
            {
                if (x.NecessaryRel == null)
                {
                    ret.Add(new QuestNecessaryRelVm { SetId = x.SetId, GroupId = null, QuestionId = null });
                }
                else
                {
                    switch (x.NecessaryRel.TargetType)
                    {
                        case 1:
                            ret.Add(new QuestNecessaryRelVm { SetId = x.SetId, GroupId = x.NecessaryRel.TargetId, QuestionId = null });
                            break;
                        case 2:
                            ret.Add(new QuestNecessaryRelVm
                            {
                                SetId = x.SetId,
                                GroupId = db.MdQuestion.Single(y => y.QstId == x.NecessaryRel.TargetId).GroupId,
                                QuestionId = x.NecessaryRel.TargetId
                            });
                            break;
                    }
                }
            });

            return ret;
        }

        /// <summary>
        /// 選項-儲存
        /// </summary>
        /// <param name="vmD">id:qstId、name:setDesc</param>
        /// <returns></returns>
        public Result<string> QuestSetItemSave(IdName vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                if (vmD != null)
                {
                    var setData = db.MdQuestSetItem.OrderByDescending(x => x.SetSorting).Where(x => x.IsEnable && x.QstId == new Guid(vmD.Id)).FirstOrDefault().SetSorting;
                    var data = new MdQuestSetItem();
                    data.SetId = Guid.NewGuid();
                    data.QstId = new Guid(vmD.Id);
                    data.SetDesc = vmD.Name;
                    data.SetSorting = setData + 1;
                    data.SetNote = false;
                    data.EditDate = DateTime.Now;
                    data.IsEnable = true;

                    db.MdQuestSetItem.Add(data);
                    db.SaveChanges();

                    result.Success = true;
                    result.ID = data.SetId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 選項-刪除
        /// </summary>
        /// <param name="id">setId</param>
        /// <returns></returns>
        public Result<string> QuestSetItemRemove(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = db.MdQuestSetItem.Find(id);
                if (data != null)
                {
                    data.IsEnable = false;
                    db.SaveChanges();

                    result.Success = true;
                    result.ID = data.SetId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        #endregion Admin後台

        #region 問卷結果-統計表

        /// <summary>
        /// 問卷資訊-單筆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public QuestGatherInfoVm GetQuestGatherInfo(Guid id)
        {
            var data = (from x in db.MdQuestInfo
                        join f in db.MdFillQuest on x.InfoId equals f.InfoId into pf
                        where x.InfoId == id
                        select new QuestGatherInfoVm()
                        {
                            ForumId = x.ForumId,
                            InfoId = x.InfoId,
                            Title = x.InfoTitle,
                            Counts = pf.Where(c => c.IsEnable).Select(c => c.MdFillUserId).Distinct().Count()
                        });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 統計圖表
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public IQueryable<QuestGatherVm> GetQuestGather(Guid id)
        {
            var data = GetQuestGatherAll();
            data = data.Where(x => x.InfoId == id);
            return data;
        }

        /// <summary>
        /// 統計圖表-單筆
        /// </summary>
        /// <param name="id">qstId</param>
        /// <returns></returns>
        public IQueryable<QuestGatherVm> GetQuestGatherDetail(Guid id)
        {
            var data = GetQuestGatherAll();
            data = data.Where(x => x.QstId == id);

            return data;
        }

        /// <summary>
        /// 統計圖表-全部
        /// </summary>
        /// <returns></returns>
        private IQueryable<QuestGatherVm> GetQuestGatherAll()
        {
            int[] intArray = new int[] { 1, 2 };
            var data = (from x in db.MdQuestion
                        join g in db.MdQuestionGroup on x.GroupId equals g.GroupId into pg
                        from g in pg.DefaultIfEmpty()
                        join s in db.MdQuestSetItem on x.QstId equals s.QstId into ps
                        join f in db.MdFillQuest on g.InfoId equals f.InfoId into pf
                        orderby g.GroupSort, x.QstSorting
                        where g.IsEnable
                        && x.IsEnable && intArray.Contains(x.QstAnsType)
                        select new QuestGatherVm()
                        {
                            InfoId = g.InfoId,
                            QstId = x.QstId,
                            Title = x.QstQuestion,
                            IsGather = g.MdQuestInfo.IsGather,
                            Counts = pf.Where(c => c.IsEnable).Select(c => c.MdFillUserEmail).Distinct().Count(),
                            SetCounts = pf.Where(c => c.IsEnable && c.QstId == x.QstId).Select(c => c.MdFillUserEmail).Distinct().Count(),
                            Categories = ps.OrderBy(c => c.SetSorting).Where(c => c.IsEnable).Select(c => c.SetDesc),
                            Series = ps.OrderBy(c => c.SetSorting).Where(c => c.IsEnable).Select(c => new QuestGatherChartsSeriesVm()
                            {
                                PageName = c.SetDesc,
                                y = pf.Where(yy => yy.IsEnable && yy.SetId == c.SetId).Count()
                            })
                        });
            return data;
        }

        #endregion

        #region 問卷結果-填寫檢視

        /// <summary>
        /// 填寫檢視
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public IQueryable<QuestGatherExamineVm> GetQuestGatherExamine(Guid id)
        {
            var data = GetQuestGatherExamineAll(id);
            return data;
        }

        /// <summary>
        /// 填寫檢視-個人
        /// </summary>
        /// <param name="infoId">infoId</param>
        /// <param name="fillUserId">userId</param>
        /// <returns></returns>
        public QuestInfoVm GetQuestGatherExamineDetail(Guid infoId, string fillUserId)
        {
            var data = GetQuestionInfoAll(infoId, fillUserId);

            return data;
        }

        /// <summary>
        /// 填寫檢視-刪除
        /// 回傳infoId
        /// </summary>
        /// <param name="id">infoId</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public Result<string> GetQuestGatherExamineRemove(Guid id, string userId)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = db.MdFillQuest.Where(x => x.InfoId == id && x.MdFillUserEmail == userId).ToList();

                foreach (var item in data)
                {
                    item.IsEnable = false;
                    db.SaveChanges();
                }
                result.Success = true;
                result.ID = id;

            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 填寫檢視-全部
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        private IQueryable<QuestGatherExamineVm> GetQuestGatherExamineAll(Guid id)
        {
            //var data = (from x in db.MdFillQuest
            //            join u in db.AspNetUsers on x.MdFillUserId.ToString() equals u.Id into pu
            //            from u in pu.DefaultIfEmpty()
            //            where x.InfoId == id && x.IsEnable
            //            group x by new { x.MdFillUserId, u.NickName, u.Email } into g
            //            orderby g.Max(x => x.EditDate) descending
            //            select new QuestGatherExamineVm()
            //            {
            //                InfoId = id,
            //                MdFillUserId = g.Key.MdFillUserId,
            //                UserNickName = g.Key.NickName,
            //                UserEmail = g.Key.Email,
            //                CreateTime = g.Max(x => x.EditDate)
            //            });
            var question = db.MdQuestInfo.Find(id);

            int[] intArray = new int[] { 1, 2 };
            var data = (from x in db.MdFillQuest
                        where x.InfoId == id && x.IsEnable
                        group x by x.MdFillUserEmail into g
                        join u in db.AspNetUsers on g.Key equals u.Id into pu
                        from u in pu.DefaultIfEmpty()
                        join f in db.MdFillQuest on id equals f.InfoId into pf
                        select new QuestGatherExamineVm()
                        {
                            InfoId = id,
                            MdFillUserEmail = intArray.Contains(question.VerifyType) ? g.Key : u.Id,
                            UserEmail = intArray.Contains(question.VerifyType) ? g.Key : u.Email,
                            UserNickName = intArray.Contains(question.VerifyType) ? g.Key : u.NickName,
                            CreateTime = g.Max(x => x.EditDate)
                        });

            return data.OrderBy(x => x.CreateTime);
        }

        #endregion

        #region 問卷結果-匯出

        /// <summary>
        /// 全部匯出
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        public Result<byte[]> GetQuestGatherExcel(Guid id)
        {
            Result<byte[]> result = new Result<byte[]>(false);
            try
            {
                var data = GetQuestGatherAll();
                var vmD = data.Where(x => x.InfoId == id).ToList();
                if (vmD.FirstOrDefault() != null)
                {
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        #region 第一批

                        int ii = 0;
                        foreach (var item in vmD)
                        {
                            ii = vmD.IndexOf(item);
                            var sheet = excel.Workbook.Worksheets.Add((ii + 1).ToString());

                            if (item.Categories.Count() > 0)
                            {
                                //問題     
                                sheet.Cells[1, 1].Value = "問題";
                                for (int i = 0; i < item.Categories.Count(); i++)
                                {
                                    sheet.Cells[1, i + 2].Value = item.Categories.Skip(i).Take(1);
                                }
                                sheet.Cells[1, item.Categories.Count() + 2].Value = "未作答";

                                //數量
                                int i1 = 0;
                                sheet.Cells[2, 1].Value = item.Title;
                                sheet.Column(1).AutoFit(80);
                                foreach (var item2 in item.Series)
                                {
                                    i1 = item.Series.ToList().IndexOf(item2);
                                    sheet.Cells[2, i1 + 2].Value = item2.y;
                                }

                                sheet.Cells[2, i1 + 3].Value = item.Counts - item.SetCounts;
                            }
                            else
                            {
                                sheet.Cells[1, 1].Value = item.Title;
                            }
                        }

                        #endregion

                        #region 全部回覆

                        var sheet2 = excel.Workbook.Worksheets.Add("全部回覆");
                        sheet2.Cells[1, 1].Value = "eMail";
                        sheet2.Cells[1, 2].Value = "填寫時間";

                        //選項 & 填寫人
                        var optionData = GetQuestGatherExcelAll(id).ToList();
                        var userData = GetQuestFillUserExcel(id).ToList();
                        foreach (var item in optionData)
                        {
                            int ii2 = optionData.IndexOf(item) + 3;
                            sheet2.Cells[1, ii2].Value = item.QstQuestion;
                            foreach (var itemUser in userData)
                            {
                                int ii3 = userData.IndexOf(itemUser) + 2;
                                sheet2.Cells[ii3, 1].Value = itemUser.UserEmail;
                                sheet2.Cells[ii3, 2].Value = itemUser.UserCreate.ToString("yyyy/MM/dd hh:mm");

                                var option = itemUser.OptionArray.FirstOrDefault(x => x.PageGuid == item.QstId);
                                sheet2.Cells[ii3, ii2].Value = option != null ? option.PageName : string.Empty;
                            }
                        }

                        sheet2.Cells.AutoFitColumns(0, 15);

                        #endregion

                        result.Data = excel.GetAsByteArray();
                        result.Success = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 全部
        /// </summary>
        /// <param name="id">infoId</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        private QuestInfoVm GetQuestionInfoAll(Guid id, string userId)
        {
            var data = db.MdQuestInfo.Include("MdQuestionGroups")
                .Include("MdQuestionGroups.MdQuestions")
                .Include("MdQuestionGroups.MdQuestions.MdQuestSetItems")
                .Include("MdQuestionGroups.MdQuestions.MdFillQuests")
                .Where(i => i.InfoId == id && i.IsEnable && i.InfoValid).Select(i => new QuestInfoVm
                {
                    InfoDesc = i.InfoDesc,
                    InfoFooter = i.InfoFooter,
                    InfoId = i.InfoId,
                    InfoTitle = i.InfoTitle,
                    QuestGroupVms = i.MdQuestionGroups.Where(g => g.IsEnable).OrderBy(g => g.GroupSort).Select(g => new QuestGroupVm
                    {
                        GroupDesc = g.GroupDesc,
                        GroupId = g.GroupId,
                        GroupSort = g.GroupSort,
                        GroupTitle = g.GroupTitle,
                        MdQuestions = g.MdQuestions.Where(q => q.IsEnable).OrderBy(q => q.QstSorting).Select(q => new QuestQuestionVm
                        {
                            QstAnsType = q.QstAnsType,
                            IsRequired = q.IsRequired,
                            QstId = q.QstId,
                            QstQuestion = q.QstQuestion,
                            QstSorting = q.QstSorting,
                            QuestSetItemVms = q.MdQuestSetItems.Where(s => s.IsEnable).OrderBy(s => s.SetSorting).Select(s => new QuestSetItemVm
                            {
                                SetSorting = s.SetSorting,
                                SetScore = s.SetScore,
                                SetNote = s.SetNote,
                                SetDesc = s.SetDesc,
                                SetId = s.SetId,
                                SetChoose = q.MdFillQuests.Where(c => c.IsEnable && c.SetId == s.SetId && c.MdFillUserEmail == userId).Select(c => new QuestSetItemChooseVm()
                                {
                                    SetId = c.SetId,
                                    MdFillUserId = c.MdFillUserId,
                                    FillDesc = c.FillDesc
                                }).FirstOrDefault(),
                                SetChooseList = q.MdFillQuests.Where(c => c.IsEnable && c.SetId == s.SetId).Select(c => new QuestSetItemChooseVm()
                                {
                                    SetId = c.SetId,
                                    MdFillUserId = c.MdFillUserId,
                                    FillDesc = c.FillDesc
                                }),
                                MdQuestNecessaryRel = s.MdQuestNecessaryRel
                            })
                        })
                    })
                }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 匯出-共用
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        private IQueryable<QuestGatherExcelVm> GetQuestGatherExcelAll(Guid id)
        {
            var data = (from x in db.MdQuestion
                        join g in db.MdQuestionGroup on x.GroupId equals g.GroupId into pg
                        from g in pg.DefaultIfEmpty()
                        orderby g.GroupSort, x.QstSorting
                        where g.IsEnable && x.IsEnable
                        && g.InfoId == id
                        select new QuestGatherExcelVm()
                        {
                            QstId = x.QstId,
                            QstQuestion = x.QstQuestion
                        });
            return data;
        }

        /// <summary>
        /// 填寫人回覆
        /// </summary>
        /// <param name="id">infoId</param>
        /// <returns></returns>
        private IQueryable<QuestGatherExcelFillVm> GetQuestGatherExcelFillAll(Guid id)
        {
            var data = (from x in db.MdFillQuest
                        join s in db.MdQuestSetItem on x.SetId equals s.SetId into ps
                        from s in ps.DefaultIfEmpty()
                        join u in db.AspNetUsers on x.MdFillUserId.ToString() equals u.Id into pu
                        from u in pu.DefaultIfEmpty()
                        where x.IsEnable && s.IsEnable
                        select new QuestGatherExcelFillVm()
                        {
                            InfoId = x.InfoId,
                            MdFillUserId = x.MdFillUserId,
                            UserNickName = u.NickName,
                            UserEmail = u.Email,
                            QstId = x.QstId,
                            QstAnsType = x.MdQuestion.QstAnsType,
                            SetId = x.SetId,
                            SetDesc = s.SetDesc,
                            FillDesc = x.FillDesc
                        });
            return data;
        }

        /// <summary>
        /// 所有回覆-user回答
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private IQueryable<QuestFillUserExcelVm> GetQuestFillUserExcel(Guid id)
        {
            var info = db.MdQuestInfo.Find(id);

            //未登入
            int[] intArray = new int[] { 1, 2 };

            //選項狀態
            int[] ansTypeArray = new int[] { 1, 2 };
            var data = (from x in db.MdFillQuest
                        where x.InfoId == id && x.IsEnable
                        group x by new { x.MdFillUserId, x.MdFillUserEmail } into g
                        join u in db.AspNetUsers on g.Key.MdFillUserEmail equals u.Id into pu
                        from u in pu.DefaultIfEmpty()
                        join o in db.MdFillQuest on id equals o.InfoId into po
                        select new QuestFillUserExcelVm()
                        {
                            UserId = intArray.Contains(info.VerifyType) ? g.Key.MdFillUserEmail : u.Id,
                            UserNickName = intArray.Contains(info.VerifyType) ? g.Key.MdFillUserEmail : u.NickName,
                            UserEmail = intArray.Contains(info.VerifyType) ? g.Key.MdFillUserEmail : u.Email,
                            UserCreate = g.Max(x => x.EditDate),
                            OptionArray = po.Where(c => c.MdFillUserEmail == (intArray.Contains(info.VerifyType) ? g.Key.MdFillUserEmail : u.Id)).Select(c => new GuidNameVm()
                            {
                                PageGuid = c.QstId,
                                PageName = (ansTypeArray.Contains(c.MdQuestion.QstAnsType) ? c.MdQuestion.MdQuestSetItems.FirstOrDefault(xx => xx.SetId == c.SetId).SetDesc : c.FillDesc)
                            })
                        });
            return data.OrderBy(x => x.UserCreate);
        }

        #endregion

        #region Api用
        public IEnumerable<ApiQuestInfoVm> GetList(){
            return repository.Where(x => x.IsEnable)
                             .Select(x => new ApiQuestInfoVm {
                                 Id = x.Forum.ForumId,
                                 Name = x.Forum.Subject,
                                 QiId = x.InfoId,
                                 QiName = x.InfoTitle
                             });
        }

        public IEnumerable<ApiQuestGatherVm> GetGather(Guid QiId)
        {
            return db.MdQuestion.Where(x => x.MdQuestionGroup.MdQuestInfo.InfoId == QiId
                                          && x.MdQuestionGroup.IsEnable).AsNoTracking()
                                .Select(x => new ApiQuestGatherVm {
                                    Id = x.MdQuestionGroup.MdQuestInfo.Forum.ForumId,
                                    Name = x.MdQuestionGroup.MdQuestInfo.Forum.Subject,
                                    QiId= x.MdQuestionGroup.MdQuestInfo.InfoId,
                                    QiName = x.MdQuestionGroup.MdQuestInfo.InfoTitle,
                                    QstName = x.QstQuestion,
                                    QstCount = x.MdFillQuests.Where(q=>q.IsEnable).Select(q=>q.MdFillUserEmail).Distinct().Count()
                                });
        }
        #endregion
    }
}
