using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;
using Geo.Grid.Tainan.OpenGov.Entity.Dictionary;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;

using OfficeOpenXml;

namespace Geo.Grid.Tainan.OpenGov.Service.Admin
{
    /// <summary>
    /// 投票管理
    /// </summary>
    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> _vote;
        private readonly OpenGovContext _db;
        private static string _userId = new IdentityProfile().GetIdentityProfile();

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db"></param>
        public VoteService(IRepository<Vote> vote)
        {
            this._vote = vote;
            this._db = vote.Db;
        }

        /// <summary>
        /// 公民論壇-單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public ForumVm GetForumDetail(Guid id)
        {
            var data = _db.Forum.Where(x => x.ForumId == id).Select(x => new ForumVm()
            {
                ForumId = x.ForumId,
                Subject = x.Subject
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public IQueryable<VoteVm> GetList(SearchVm key)
        {
            var data = _db.Vote.OrderByDescending(x => x.CreatedDate)
                .Where(x => !x.IsEnabled && x.ForumId == key.Id)
                .Select(x => new VoteVm()
                {
                    VoteId = x.VoteId,
                    Title = x.Title,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CanVote = x.CanVote,
                    SelectNumber = x.SelectNumber,
                    IsPublish = x.IsPublish,
                    CreatedDate = x.CreatedDate
                });
            return data;
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetCreate(VoteVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = new Vote();
                data.Title = vmD.Title;
                data.Info = vmD.Info;
                data.StartDate = vmD.StartDate;
                data.EndDate = vmD.EndDate;
                data.CanVote = vmD.CanVote;
                data.SelectNumber = vmD.SelectNumber;
                data.IsPublish = vmD.IsPublish;
                data.CreatedBy = _userId;
                data.UpdatedBy = _userId;
                data.ForumId = vmD.ForumId;
                data.VerifyType = vmD.VerifyType;

                if (vmD.GroupIdArray != null)
                {
                    var groupData = _db.VoteBasicGroup.Where(x => vmD.GroupIdArray.Contains(x.GroupId)).ToList();
                    data.VoteBasicGroups = groupData;
                }

                if (vmD.OptionArray != null)
                {
                    var newData = vmD.OptionArray.Where(x => !string.IsNullOrEmpty(x.PageName)).ToList();
                    foreach (var item in newData)
                    {
                        int i = newData.IndexOf(item);
                        _db.VoteOption.Add(new VoteOption()
                        {
                            VoteId = data.VoteId,
                            Title = item.PageName,
                            Sort = i,
                            CreatedBy = _userId,
                            UpdatedBy = _userId
                        });
                    }
                }

                _db.Vote.Add(data);
                _db.SaveChanges();
                result.Success = true;
                result.ID = data.VoteId;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 編輯-單筆
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public VoteVm GetDetail(Guid id)
        {
            var data = _db.Vote.Where(x => x.VoteId == id).Select(x => new VoteVm()
            {
                VoteId = x.VoteId,
                ForumId = x.ForumId,
                Title = x.Title,
                Info = x.Info,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                CanVote = x.CanVote,
                SelectNumber = x.SelectNumber,
                IsPublish = x.IsPublish,
                VerifyType = x.VerifyType,
                GroupIdArray = x.VoteBasicGroups.Select(c => c.GroupId)
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetEdit(VoteVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.Vote.Find(vmD.VoteId);
                if (data != null)
                {
                    //data.VoteBasicGroups.Clear();

                    //選項
                    var option = _db.VoteOption.Where(x => x.VoteId == vmD.VoteId).ToList();
                    foreach (var item in option)
                    {
                        item.IsEnabled = false;
                        item.UpdatedBy = _userId;
                        item.UpdatedDate = DateTime.Now;
                    }

                    _db.SaveChanges();

                    data.Title = vmD.Title;
                    data.Info = vmD.Info;
                    data.StartDate = vmD.StartDate;
                    data.EndDate = vmD.EndDate;
                    data.CanVote = vmD.CanVote;
                    data.SelectNumber = vmD.SelectNumber;
                    data.IsPublish = vmD.IsPublish;
                    data.UpdatedDate = DateTime.Now;
                    data.UpdatedBy = _userId;

                    //if (vmD.GroupIdArray != null)
                    //{
                    //    var groupData = _db.VoteBasicGroup.Where(x => vmD.GroupIdArray.Contains(x.GroupId)).ToList();
                    //    data.VoteBasicGroups = groupData;
                    //}

                    if (vmD.OptionArray != null)
                    {
                        var optionData = vmD.OptionArray.Where(x => !string.IsNullOrEmpty(x.PageName)).ToList();
                        var sourceData = option.Where(x => optionData.Select(c => c.PageGuid).Contains(x.OptionId));
                        var newData = optionData.Where(x => x.PageGuid.ToString() == "00000000-0000-0000-0000-000000000000").ToList();
                        int sourceSort = sourceData.FirstOrDefault() != null ? sourceData.OrderByDescending(x => x.Sort).FirstOrDefault().Sort + 1 : 1;
                        foreach (var item in sourceData)
                        {
                            item.Title = optionData.FirstOrDefault(x => x.PageGuid == item.OptionId).PageName;
                            item.IsEnabled = true;
                            item.UpdatedDate = DateTime.Now;
                            item.UpdatedBy = _userId;
                        }
                        foreach (var item in newData)
                        {
                            int i = newData.IndexOf(item) + sourceSort;
                            _db.VoteOption.Add(new VoteOption()
                            {
                                VoteId = vmD.VoteId,
                                Title = item.PageName,
                                Sort = i,
                                CreatedBy = _userId,
                                UpdatedBy = _userId,
                                UpdatedDate = DateTime.Now
                            });
                        }
                    }
                    _db.SaveChanges();
                    result.Success = true;
                    result.ID = data.VoteId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public Result<string> GetRemove(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.Vote.Find(id);
                if (data != null)
                {
                    data.IsEnabled = true;
                    data.UpdatedBy = _userId;
                    data.UpdatedDate = DateTime.Now;

                    _db.SaveChanges();
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

        #region 統計圖表

        /// <summary>
        /// 圖表
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public IQueryable<VoteGatherVm> GetGather(Guid id)
        {
            var data = GetGatherAll(id);

            return data;
        }

        /// <summary>
        /// 圖片-個資
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public IQueryable<VoteGatherVm> GetGatherBasic(Guid id)
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

        #region 匯出

        /// <summary>
        /// 匯出
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public Result<byte[]> GetExcel(Guid id)
        {
            Result<byte[]> result = new Result<byte[]>(false);
            try
            {
                var data = GetGatherExcelAll(id);
                if (data.FirstOrDefault() != null)
                {
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        #region 第一批

                        var vmD = data.ToList();
                        foreach (var item in vmD)
                        {
                            int ii = vmD.IndexOf(item);
                            string sheetName = (ii == 0 ? "1" : item.Title);
                            var sheet = excel.Workbook.Worksheets.Add(sheetName);
                            sheet.Cells[1, 1].Value = item.Title;
                            if (item.Categories.Count() > 0)
                            {
                                //選項                                
                                for (int i = 0; i < item.Categories.Count(); i++)
                                {
                                    sheet.Cells[1, i + 2].Value = item.Categories.Skip(i).Take(1);
                                }

                                //數量
                                int i1 = 0;
                                sheet.Cells[2, 1].Value = "人數";
                                sheet.Column(1).AutoFit(80);
                                var vmSeries = item.Series.ToList();
                                foreach (var itemSeries in vmSeries)
                                {
                                    i1 = vmSeries.IndexOf(itemSeries);
                                    sheet.Cells[2, i1 + 2].Value = itemSeries.y;
                                }
                            }
                            sheet.Cells.AutoFitColumns(0, 15);
                        }

                        #endregion

                        #region 所有回覆

                        var sheet2 = excel.Workbook.Worksheets.Add("全部回覆");
                        sheet2.Cells[1, 1].Value = "eMail";
                        sheet2.Cells[1, 2].Value = "填寫時間";

                        var optionData = GetFillOptionExcel(id).ToList();
                        var userData = GetFillUserExcel(id).ToList();
                        foreach (var item in optionData)
                        {
                            int ii = optionData.IndexOf(item) + 3;
                            sheet2.Cells[1, ii].Value = item.PageName;
                            foreach (var itemUser in userData)
                            {
                                int ii2 = userData.IndexOf(itemUser) + 2;
                                sheet2.Cells[ii2, 1].Value = itemUser.UserEmail;
                                sheet2.Cells[ii2, 2].Value = itemUser.UserCreate.ToString("yyyy/MM/dd hh:mm");

                                var option = itemUser.OptionArray.FirstOrDefault(x => x.PageGuid.ToString() == item.PageId);
                                var basic = itemUser.BasicArray.FirstOrDefault(x => x.PageId.ToString() == item.PageId);
                                sheet2.Cells[ii2, ii].Value = (option != null ? "同意" : basic != null ? basic.PageName : string.Empty);
                            }
                        }

                        sheet2.Cells.AutoFitColumns(0, 30);

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
        /// 投票+基本資料
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        private IEnumerable<VoteGatherVm> GetGatherExcelAll(Guid id)
        {
            var data = GetGatherAll(id).ToList();
            var basicData = GetGatherBasic(id).ToList();
            var vmD = data.Union(basicData);

            return vmD;
        }

        /// <summary>
        /// 匯出-所有回覆-題目
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        private IEnumerable<PageNameVm> GetFillOptionExcel(Guid id)
        {
            var data = _db.VoteOption.OrderBy(x => x.Sort).Where(x => x.VoteId == id).Select(x => new PageNameVm()
            {
                PageId = x.OptionId.ToString(),
                PageName = x.Title
            }).ToList();

            var basicData = _db.VoteBasicGroup.OrderBy(x => x.Sort).Where(x => x.Votes.FirstOrDefault(c => c.VoteId == id) != null).Select(x => new PageNameVm()
            {
                PageId = x.GroupId.ToString(),
                PageName = x.Name
            });

            var vmD = data.Union(basicData);

            return vmD;
        }

        /// <summary>
        /// 投票管理-所有回覆-user回答
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        private IQueryable<VoteFillUserExcelVm> GetFillUserExcel(Guid id)
        {
            var vote = _db.Vote.Find(id);
            int[] intArray = new int[] { 1, 2 };
            var data = (from x in _db.VoteFillOption
                        where x.VoteId == id
                        group x by x.UserId into g
                        join u in _db.AspNetUsers on g.Key equals u.Id into pu
                        from u in pu.DefaultIfEmpty()
                        join o in _db.VoteFillOption on id equals o.VoteId into po
                        join b in _db.VoteFillBasic on id equals b.VoteId into pb
                        select new VoteFillUserExcelVm()
                        {
                            UserId = intArray.Contains(vote.VerifyType) ? g.Key : u.Id,
                            UserNickName = intArray.Contains(vote.VerifyType) ? g.Key : u.NickName,
                            UserEmail = intArray.Contains(vote.VerifyType) ? g.Key : u.Email,
                            UserCreate = g.Max(x => x.CreatedDate),
                            OptionArray = po.Where(c => c.UserId == (intArray.Contains(vote.VerifyType) ? g.Key : u.Id)).Select(c => new GuidNameVm()
                            {
                                PageGuid = c.OptionId,
                                PageName = string.Empty
                            }),
                            BasicArray = pb.Where(c => c.UserId == (intArray.Contains(vote.VerifyType) ? g.Key : u.Id)).Select(c => new IdNameVm()
                            {
                                PageId = c.GroupId,
                                PageName = c.BasicDesc
                            })
                        });
            return data.OrderBy(x => x.UserCreate);
        }

        #endregion

        #region 共用

        /// <summary>
        /// 統計圖表
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        private IQueryable<VoteGatherVm> GetGatherAll(Guid id)
        {
            var data = (from x in _db.Vote
                        join f in _db.VoteFillOption on x.VoteId equals f.VoteId into pf
                        where !x.IsEnabled && x.VoteId == id
                        select new VoteGatherVm()
                        {
                            PageId = x.VoteId.ToString(),
                            ForumId = x.ForumId,
                            ForumTitle = x.Forum.Subject,
                            Title = x.Title,
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
        /// 個資-分類
        /// </summary>
        /// <returns></returns>
        public IQueryable<IdNameVm> GetGroup()
        {
            var data = _db.VoteBasicGroup.OrderBy(x => x.Sort).Where(x => x.IsEnabled).Select(x => new IdNameVm()
            {
                PageId = x.GroupId,
                PageName = x.Name
            });
            return data;
        }

        /// <summary>
        /// 選項
        /// </summary>
        /// <param name="id">voteId</param>
        /// <returns></returns>
        public IQueryable<GuidNameVm> GetOption(Guid id)
        {
            var data = _db.VoteOption.OrderBy(x => x.Sort).Where(x => x.VoteId == id && x.IsEnabled).Select(x => new GuidNameVm()
            {
                PageGuid = x.OptionId,
                PageName = x.Title
            });
            return data;
        }

        /// <summary>
        /// 驗證方式
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IdNameVm> GetVerifyType()
        {
            var data = VerifyType.VoteVerifyType.Select(x => new IdNameVm()
            {
                PageId = x.Key,
                PageName = x.Value
            });
            return data;
        }

        #endregion
    }
}
