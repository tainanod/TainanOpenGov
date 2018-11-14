using System;
using System.Collections.Generic;
using System.IO;
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
using System.Text.RegularExpressions;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    /// <summary>
    /// 投票管理
    /// </summary>
    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> _vote;
        private readonly OpenGovContext _db;
        private string _userId = new IdentityProfile().GetIdentityProfile();
        private string _anonymousId = new IdentityProfile().GetIdentityProfileAnonymousId();
        private string _urlName = new WebSite().GetWebSite();

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public VoteService(IRepository<Vote> vote)
        {
            this._vote = vote;
            this._db = vote.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="id">公民論壇編號</param>
        /// <returns></returns>
        public IQueryable<VoteVm> GetList(Guid id)
        {
            var today = DateTime.Today;
            var data = (from x in _db.Vote
                        where x.IsPublish && !x.IsEnabled && x.ForumId == id //&& (x.StartDate <= today && x.EndDate >= today)
                        join o in _db.VoteFillOption on new { VoteId = x.VoteId, UserId = _userId } equals new { o.VoteId, o.UserId } into po
                        join b in _db.VoteFillBasic on new { VoteId = x.VoteId, UserId = _userId } equals new { b.VoteId, b.UserId } into pb
                        join h in _db.VoteCheck on new { VoteId = x.VoteId, UserId = _anonymousId } equals new { h.VoteId, h.UserId } into ph
                        orderby x.StartDate descending, x.CreatedDate descending
                        select new VoteVm()
                        {
                            ForumId = x.ForumId,
                            VoteId = x.VoteId,
                            Title = x.Title,
                            Info = x.Info,
                            CanVote = x.CanVote,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            VerifyType = x.VerifyType,
                            IsVote = (po.FirstOrDefault() != null || pb.FirstOrDefault() != null || ph.FirstOrDefault() != null)
                        });
            return data;
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public VoteVm GetDetail(Guid id)
        {
            var today = DateTime.Today;
            var data = _db.Vote.Where(x => x.IsPublish && !x.IsEnabled && x.VoteId == id)
                .Select(x => new VoteVm()
                {
                    VoteId = x.VoteId,
                    ForumId = x.ForumId,
                    Title = x.Title,
                    Info = x.Info,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    VerifyType = x.VerifyType,
                    SelectNumber = x.SelectNumber,
                    CanVote = x.CanVote,
                    OptionArray = x.VoteOption.OrderBy(c => c.Sort).Where(c => c.IsEnabled).Select(c => new GuidNameVm()
                    {
                        PageGuid = c.OptionId,
                        PageName = c.Title
                    }),
                    BasicArray = x.VoteBasicGroups.OrderBy(c => c.Sort).Where(c => c.IsEnabled).Select(c => new VoteBasicGroupVm()
                    {
                        GroupId = c.GroupId,
                        Name = c.Name,
                        VoteBasicArray = c.VoteBasic.OrderBy(b => b.Sort).Where(b => b.IsEnabled).Select(b => new VoteBasicVm()
                        {
                            BasicId = b.BasicId,
                            Name = b.Name,
                            IsExplain = b.IsExplain
                        })
                    })
                });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetCreate(VoteSaveVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                DateTime today = DateTime.Today;
                if (vmD != null)
                {
                    //確認狀態
                    var check = GetDetailCheck(vmD.VoteId, vmD.UserMail);
                    if (!check.Success)
                    {
                        result.Message = check.Message;
                        return result;
                    }

                    var data = _db.Vote.Find(vmD.VoteId);

                    //email驗證
                    if (data.VerifyType == 2)
                    {
                        if (string.IsNullOrEmpty(vmD.UserMail))
                        {
                            result.Message = "請確認Email是否正確";
                            return result;
                        }

                        var verify = GetVerifyCheck(data.VoteId, vmD.UserMail);
                        if (verify)
                        {
                            result.Message = "您已填過了";
                            return result;
                        }
                        _userId = vmD.UserMail;
                    }

                    _db.VoteCheck.Add(new VoteCheck()
                    {
                        VoteId = vmD.VoteId,
                        UserEmail = _userId,
                        UserId = _anonymousId
                    });

                    //儲存
                    #region 選項

                    var fillOption = new VoteFillOption();
                    if (data.VoteOption.FirstOrDefault() != null)
                    {
                        //選項
                        if (vmD.OptionArray != null)
                        {
                            foreach (var item in vmD.OptionArray)
                            {
                                var vote = data.VoteOption.FirstOrDefault(x => x.OptionId == item.OptionId);
                                _db.VoteFillOption.Add(new VoteFillOption()
                                {
                                    UserId = _userId,
                                    OptionId = item.OptionId,
                                    VoteId = vote.VoteId,
                                    CreatedDate = DateTime.Now
                                });
                            }
                        }
                    }

                    #endregion

                    #region 個資

                    if (data.VoteBasicGroups.FirstOrDefault() != null)
                    {
                        var basic = _db.VoteBasic.Where(x => x.IsEnabled);
                        int[] intArray = new int[] { 6, 7 };
                        //個資
                        if (vmD.BasicArray != null)
                        {
                            foreach (var item in vmD.BasicArray)
                            {
                                var group = basic.FirstOrDefault(x => x.BasicId == item.BasicId);
                                if (group != null)
                                {
                                    _db.VoteFillBasic.Add(new VoteFillBasic()
                                    {
                                        UserId = _userId,
                                        BasicId = item.BasicId,
                                        VoteId = vmD.VoteId,
                                        BasicDesc = group.IsExplain || intArray.Contains(group.GroupId) ? item.BasicDesc : group.Name,
                                        GroupId = group.GroupId,
                                        CreatedDate = DateTime.Now
                                    });
                                }
                            }
                        }
                    }

                    #endregion

                    _db.SaveChanges();
                    if (data.VerifyType == 2)
                    {
                        result.Message = "您己完成投票，請至mail信箱點選驗證信。";
                    }
                    else
                    {
                        result.Message = "您已填寫完畢，感謝您的填寫。";
                    }
                    result.Success = true;
                    result.ID = vmD.VoteId;

                    if (data.VerifyType == 2)
                    {
                        GetVerifyCheckEmailOne(data.VoteId);
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
        /// 檢查是否為有效投票
        /// </summary>
        /// <param name="voteId">voteId</param>
        /// <returns></returns>
        public Result<string> GetVerifyCheckSave(Guid voteId)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.VoteCheck.FirstOrDefault(x => x.VoteId == voteId && x.UserId == _anonymousId);
                if (data == null)
                {
                    return result;
                }
                data.IsEnabled = true;

                _db.SaveChanges();
                result.Success = true;
                result.ID = voteId;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        #region 統計圖表-後台用

        /// <summary>
        /// 圖表
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public IQueryable<VoteGatherVm> GetGather(Guid id)
        {
            var data = (from x in _db.Vote
                        join f in _db.VoteFillOption on x.VoteId equals f.VoteId into pf
                        where !x.IsEnabled && x.VoteId == id
                        select new VoteGatherVm()
                        {
                            PageId = x.VoteId.ToString(),
                            Title = x.Title,
                            CanVote = x.CanVote,
                            Counts = pf.Select(c => c.UserId).Distinct().Count(),
                            Categories = x.VoteOption.OrderBy(c => c.Sort).Select(c => c.Title),
                            Series = x.VoteOption.OrderBy(c => c.Sort).Select(c => new VoteGatherChartsSeriesVm()
                            {
                                PageName = c.Title,
                                y = pf.Where(yy => yy.OptionId == c.OptionId).Count()
                            })
                        });

            return data;
        }

        /// <summary>
        /// 圖片-個資
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public IQueryable<VoteGatherVm> GetGatherBasic(Guid id)
        {
            var data = GetGatherBasicAll(id);

            return data;
        }

        /// <summary>
        /// 圖片-個資
        /// </summary>
        /// <param name="id">voteId</param>
        /// <param name="groupId">groupId</param>
        /// <returns></returns>
        public IQueryable<VoteGatherVm> GetGatherBasicDetail(Guid id, int groupId)
        {
            var data = GetGatherBasicAll(id);

            if (groupId > 0)
            {
                data = data.Where(x => x.PageId == groupId.ToString());
            }

            return data;
        }

        /// <summary>
        /// 圖表-個資-全部
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        private IQueryable<VoteGatherVm> GetGatherBasicAll(Guid id)
        {
            var data = (from x in _db.VoteBasicGroup
                        join v in _db.Vote on x.Votes.FirstOrDefault(c => c.VoteId == id).VoteId equals v.VoteId into pv
                        from v in pv
                        join b in _db.VoteFillBasic on v.VoteId equals b.VoteId into pb
                        where x.IsEnabled && x.Votes.FirstOrDefault(c => c.VoteId == id) != null
                        && x.VoteBasic.FirstOrDefault(c => !string.IsNullOrEmpty(c.Name)) != null
                        select new VoteGatherVm()
                        {
                            PageId = x.GroupId.ToString(),
                            Title = x.Name,
                            Counts = pb.Select(c => c.UserId).Distinct().Count(),
                            Categories = x.VoteBasic.OrderBy(c => c.Sort).Select(c => c.Name),
                            Series = x.VoteBasic.OrderBy(c => c.Sort).Select(c => new VoteGatherChartsSeriesVm()
                            {
                                PageName = c.Name,
                                y = pb.Where(bb => bb.BasicId == c.BasicId).Count()
                            })
                        });

            return data;
        }

        #endregion

        #region 共用

        /// <summary>
        /// 檢查-單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <param name="userMail">userMail</param>
        /// <returns></returns>
        public Result<string> GetDetailCheck(Guid id, string userMail)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var vote = _db.Vote.Find(id);
                if (vote == null)
                {
                    result.Message = "請重新操作";

                    return result;
                }

                switch (vote.VerifyType)
                {
                    case 1:
                        break;
                    case 2:
                        //使用匿名id查詢是否已填寫過
                        bool verify = GetVerifyCheck(id);
                        if (verify)
                        {
                            result.Message = "您已填寫過了";
                            return result;
                        }

                        ////驗證是否為email格式
                        //if (string.IsNullOrEmpty(userMail) && !VerifyRegex.VerifyEmail(userMail))
                        //{
                        //    result.Message = "請確認email格式是否正確";
                        //    return result;
                        //}
                        break;
                    case 3:
                        if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            result.Message = "請登入會員";
                            return result;
                        }
                        break;
                }

                var today = DateTime.Today;
                var data = (from x in _db.Vote
                            join o in _db.VoteFillOption on new { VoteId = x.VoteId, UserId = _userId } equals new { o.VoteId, o.UserId } into po
                            join b in _db.VoteFillBasic on new { VoteId = x.VoteId, UserId = _userId } equals new { b.VoteId, b.UserId } into pb
                            join h in _db.VoteCheck on new { VoteId = x.VoteId, UserId = _anonymousId } equals new { h.VoteId, h.UserId } into ph
                            where x.IsPublish && !x.IsEnabled && x.VoteId == id //&& (x.StartDate <= today && x.EndDate >= today)
                            select new VoteVm()
                            {
                                VoteId = x.VoteId,
                                ForumId = x.ForumId,
                                Title = x.Title,
                                Info = x.Info,
                                StartDate = x.StartDate,
                                EndDate = x.EndDate,
                                VerifyType = x.VerifyType,
                                IsVote = (po.FirstOrDefault() != null || pb.FirstOrDefault() != null),
                                SelectNumber = x.SelectNumber,
                                OptionArray = x.VoteOption.OrderBy(c => c.Sort).Where(c => c.IsEnabled).Select(c => new GuidNameVm()
                                {
                                    PageGuid = c.OptionId,
                                    PageName = c.Title
                                }),
                                BasicArray = x.VoteBasicGroups.OrderBy(c => c.Sort).Where(c => c.IsEnabled).Select(c => new VoteBasicGroupVm()
                                {
                                    GroupId = c.GroupId,
                                    Name = c.Name,
                                    VoteBasicArray = c.VoteBasic.OrderBy(b => b.Sort).Where(b => b.IsEnabled).Select(b => new VoteBasicVm()
                                    {
                                        BasicId = b.BasicId,
                                        Name = (pb.FirstOrDefault(cc => cc.BasicId == b.BasicId) != null ? pb.FirstOrDefault(cc => cc.BasicId == b.BasicId).BasicDesc : b.Name),
                                        IsExplain = b.IsExplain
                                    })
                                })
                            }).FirstOrDefault();

                if (data == null)
                {
                    result.Message = "請重新操作!";
                    return result;
                }

                if (!(data.StartDate <= today) || !(data.EndDate >= today))
                {
                    result.Message = "已超過填寫時間";
                    result.Data = "已超過填寫時間";
                    return result;
                }

                if (data.IsVote)
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
        /// 縣市
        /// </summary>
        /// <returns></returns>
        public IQueryable<SeqNameVm> GetCity()
        {
            var data = _db.CityTown.OrderBy(x => x.TownSeq).Where(x => x.IsEnable)
                .GroupBy(x => new { citySeq = x.CitySeq, cityName = x.CityName })
                .Select(x => new SeqNameVm()
                {
                    PageSeq = x.Key.citySeq,
                    PageName = x.Key.cityName
                });
            return data;
        }

        /// <summary>
        /// 鄉鎮區
        /// </summary>
        /// <param name="id">cityId</param>
        /// <returns></returns>
        public IQueryable<CityTownVm> GetTown(int id)
        {
            var data = _db.CityTown.OrderBy(x => x.TownSeq).Where(x => x.IsEnable).Select(x => new CityTownVm()
            {
                CityId = x.CitySeq,
                CityName = x.CityName,
                TownId = x.TownSeq,
                TownName = x.TownName
            });

            if (id > 0)
            {
                data = data.Where(x => x.CityId == id);
            }
            return data;
        }

        /// <summary>
        /// 縣市/鄉鎮區
        /// </summary>
        /// <returns></returns>
        public IQueryable<CityTownVm> GetCityTown()
        {
            var data = _db.CityTown.OrderBy(x => x.TownSeq).Where(x => x.IsEnable).Select(x => new CityTownVm()
            {
                CityId = x.CitySeq,
                CityName = x.CityName,
                TownId = x.TownSeq,
                TownName = x.TownName
            });
            return data;
        }

        #endregion

        #region 第二階段驗證

        /// <summary>
        /// 檢查是否已填過
        /// true:已填過
        /// </summary>
        /// <param name="voteId">voteId</param>
        /// <returns></returns>
        private bool GetVerifyCheck(Guid voteId)
        {
            var data = _db.VoteCheck.FirstOrDefault(x => x.VoteId == voteId && x.UserId == _anonymousId);
            return data != null;
        }

        /// <summary>
        /// 檢查是否已填過
        /// true:已填過
        /// </summary>
        /// <param name="voteId">投票編號</param>
        /// <param name="userMail">userMail</param>
        /// <returns></returns>
        private bool GetVerifyCheck(Guid voteId, string userMail)
        {
            if (!VerifyRegex.VerifyEmail(userMail))
            {
                return false;
            }
            var data = _db.VoteCheck.FirstOrDefault(x => x.VoteId == voteId && (x.UserId == _anonymousId || x.UserEmail == userMail));
            return data != null;
        }

        /// <summary>
        /// 傳送Email驗證信件
        /// </summary>
        /// <param name="voteId">投票id</param>
        /// <returns></returns>
        private Result<string> GetVerifyCheckEmailOne(Guid voteId)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.VoteCheck.FirstOrDefault(x => x.VoteId == voteId && x.UserId == _anonymousId);
                if (data != null)
                {
                    string urlName = _urlName;

                    string mailBody = FileToHtml.GetFileHtml("~/App_Data/ForumVoteEmail.html").Data;
                    mailBody = mailBody.Replace(@"{{urlName}}", urlName + "/Vote/_Check/" + voteId);

                    var mailData = new WaitingMail();
                    mailData.TypeName = "公民論壇-投票管理-第二階段驗證Email";
                    mailData.ToName = _anonymousId;
                    mailData.ToEmail = data.UserEmail;
                    mailData.Subject = "臺南開放政府-您有一封投票驗證信件" + DateTime.Today.ToString("(yyyy/MM/dd)");
                    mailData.MailContent = mailBody;

                    _db.WaitingMail.Add(mailData);
                    _db.SaveChanges();
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
        /// <param name="voteId">voteId</param>
        /// <returns></returns>
        public Result<string> GetVerifyCheckEmailTwo(Guid voteId)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.VoteCheck.FirstOrDefault(x => x.VoteId == voteId && x.UserId == _anonymousId);
                if (data != null)
                {
                    data.IsEnabled = true;
                    _db.SaveChanges();
                    result.Success = true;
                    result.ID = voteId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        #endregion
    }
}
