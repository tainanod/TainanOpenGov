using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Newtonsoft.Json.Linq;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ForumService : IForumService
    {
        private readonly IRepository<Forum> _repository;
        private readonly IRepository<PhotoExt> _photorepository;
        private readonly OpenGovContext _db = new OpenGovContext();
        private string _anonymousId = new IdentityProfile().GetIdentityProfileAnonymousId();
        private string _userId = new IdentityProfile().GetIdentityProfile();
        private string _urlName = new WebSite().GetWebSite();
        private readonly Interface.Admin.IDataSetService dataSetService;

        public ForumService(IRepository<Forum> repository, IRepository<PhotoExt> photorepository, Interface.Admin.IDataSetService dataSetService)
        {
            this._repository = repository;
            this.dataSetService = dataSetService;

            _photorepository = photorepository;
        }

        public IQueryable<Forum> QueryForum(ForumType type)
        {

            var user = _db.AspNetUsers.FirstOrDefault(x => x.Id == _userId);
            var sysRole = user.AspNetRoles.Any(x => x.Name == "Office") ? SysRoles.局處管理員 : SysRoles.系統管理者;
            var unitId = sysRole == SysRoles.局處管理員 ? user.DataSetUnitId : null;
            return sysRole == SysRoles.局處管理員 ?
                    _repository.Where(x => x.IsEnabled && x.ForumType == type && x.UpperId == null && x.DataSetUnitId == unitId).AsNoTracking() :
                    _repository.Where(x => x.IsEnabled && x.ForumType == type && x.UpperId == null).AsNoTracking();
        }

        public IQueryable<Forum> GetForumByType(ForumType type)
        {
            return _repository.Where(x => x.IsEnabled && x.OpenDate < DateTime.Now && x.ForumType == type && x.UpperId == null).OrderByDescending(x => x.OpenDate).AsNoTracking();
        }

        public Result<Forum> Create(Forum model, bool? isSubForum = null)
        {
            var result = new Result<Forum>(false);
            try
            {
                model.IsEnabled = true;

                if (isSubForum.HasValue && isSubForum.Value)
                {
                    model.DataSetUnitId = dataSetService.GetUnitList().Where(x => x.PageName == model.Holder)
                        .Select(x => x.PageGuid).Single();
                }

                result.Success = _repository.Create(model) == 1;
                if (result.Success)
                {
                    result.Data = model;
                }
                else
                {
                    result.Message = "新增失敗!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Forum GetForum(Guid id)
        {
            return _repository.Get(x => x.ForumId == id && x.IsEnabled);
        }

        public Forum GetForumWithRelation(Guid id)
        {
            return _repository.Where(x => x.ForumId == id && x.IsEnabled)
                //.Include(x => x.Discuss)
                .Include(x => x.Hyperlink)
                .Include(x => x.Document)
                .Include(x => x.Activity)
                .FirstOrDefault();
        }

        public Result<ForumVm> GetForumAndRelations(Guid forumId)
        {
            var result = new Result<ForumVm>();

            //var z = _repository.Where(x => x.ForumId == forumId && x.IsEnabled)
            //    .Include(x => x.Hyperlink)
            //    .Include(x => x.Document)
            //    .Include(x => x.Activity)
            //    .FirstOrDefault();

            var oldData = DateTime.Today.AddDays(-7);

            var data = _db.Forum.Include(x => x.Hyperlink).Include(x => x.Document).Include(x => x.Activity)
                .Include(x => x.AnonymousClick)
                .Where(x => x.IsEnabled && x.ForumId == forumId)
                .Select(x => new ForumVm()
                {
                    Category = x.Category,
                    CloseDate = x.CloseDate,
                    Description = x.Description,
                    ForumId = x.ForumId,
                    Holder = x.Holder,
                    OpenDate = x.OpenDate,
                    Subject = x.Subject,
                    UpperId = x.UpperId,
                    Announcement = x.Announcement,
                    ForumActivity = x.Activity.Where(xx => xx.ActivityType == ActivityType.論壇活動 && xx.IsEnabled).Select(xx => new ActivityVm()
                    {
                        CreatedDate = xx.CreatedDate,
                        ActivityId = xx.ActivityId,
                        Subject = xx.Subject
                    }),
                    WorkActivity = x.Activity.Where(xx => xx.ActivityType == ActivityType.工作坊 && xx.IsEnabled).Select(xx => new ActivityVm()
                    {
                        CreatedDate = xx.CreatedDate,
                        ActivityId = xx.ActivityId,
                        Subject = xx.Subject
                    }),
                    NormalDocuments = x.Document.Where(xx => xx.DocumentType == DocType.一般文件 && xx.IsEnabled).Select(xx => new DocumentVm()
                    {
                        FileName = xx.FileName,
                        DocumentId = xx.DocumentId,
                        CreatedDate = xx.CreatedDate,
                        IsClick = x.AnonymousClick.FirstOrDefault(c => c.AnonymousId == _anonymousId && c.ClickType == "B" && c.ClickId == xx.DocumentId.ToString()) != null || (xx.CreatedDate > oldData ? false : true)
                    }),
                    GovReplyDocuments = x.Document.Where(xx => xx.DocumentType == DocType.市府回應 && xx.IsEnabled).Select(xx => new DocumentVm()
                    {
                        FileName = xx.FileName,
                        DocumentId = xx.DocumentId,
                        CreatedDate = xx.CreatedDate,
                        IsClick = x.AnonymousClick.FirstOrDefault(c => c.AnonymousId == _anonymousId && c.ClickType == "C" && c.ClickId == xx.DocumentId.ToString()) != null || (xx.CreatedDate > oldData ? false : true)
                    }),
                    HyperLinks = x.Hyperlink.Where(xx => xx.IsEnabled).Select(xx => new HyperlinkVm()
                    {
                        HyperlinkId = xx.HyperlinkId,
                        Title = xx.Title,
                        Url = xx.Url,
                        IsClick = x.AnonymousClick.FirstOrDefault(c => c.AnonymousId == _anonymousId && c.ClickType == "A" && c.ClickId == xx.HyperlinkId.ToString()) != null || (xx.CreatedDate > oldData ? false : true)
                    }),
                    Photos = x.Photo.Select(xx => new PhotoVm()
                    {
                        PhotoId = xx.PhotoId
                    })
                });

            //var data = (from x in _db.Forum.Include(c => c.Hyperlink).Include(c => c.Document).Include(c => c.Activity)
            //                //join a in _db.AnonymousClick on new { AnonymousId = _anonymousId, ClickType = "A" } equals new { a.AnonymousId, a.ClickType } into pa
            //                //join a in _db.AnonymousClick on new { AnonymousId = _anonymousId, ClickType = "B" } equals new { a.AnonymousId, a.ClickType } into pb
            //                //join a in _db.AnonymousClick on new { AnonymousId = _anonymousId, ClickType = "C" } equals new { a.AnonymousId, a.ClickType } into pc
            //            where x.ForumId == forumId && x.IsEnabled
            //            select new ForumVm()
            //            {
            //                Category = x.Category,
            //                CloseDate = x.CloseDate,
            //                Description = x.Description,
            //                ForumId = x.ForumId,
            //                Holder = x.Holder,
            //                OpenDate = x.OpenDate,
            //                Subject = x.Subject,
            //                UpperId = x.UpperId,
            //                Announcement = x.Announcement,
            //                ForumActivity = x.Activity.Where(xx => xx.ActivityType == ActivityType.論壇活動 && xx.IsEnabled).Select(xx => new ActivityVm()
            //                {
            //                    CreatedDate = xx.CreatedDate,
            //                    ActivityId = xx.ActivityId,
            //                    Subject = xx.Subject
            //                }),
            //                WorkActivity = x.Activity.Where(xx => xx.ActivityType == ActivityType.工作坊 && x.IsEnabled).Select(xx => new ActivityVm()
            //                {
            //                    CreatedDate = xx.CreatedDate,
            //                    ActivityId = xx.ActivityId,
            //                    Subject = xx.Subject
            //                }),
            //                NormalDocuments = x.Document.Where(xx => xx.DocumentType == DocType.一般文件 && x.IsEnabled).Select(xx => new DocumentVm()
            //                {
            //                    FileName = xx.FileName,
            //                    DocumentId = xx.DocumentId,
            //                    CreatedDate = xx.CreatedDate
            //                }),
            //                GovReplyDocuments = x.Document.Where(xx => xx.DocumentType == DocType.市府回應 && x.IsEnabled).Select(xx => new DocumentVm()
            //                {
            //                    FileName = xx.FileName,
            //                    DocumentId = xx.DocumentId,
            //                    CreatedDate = xx.CreatedDate
            //                }),
            //                HyperLinks = x.Hyperlink.Where(xx => xx.IsEnabled).Select(xx => new HyperlinkVm()
            //                {
            //                    HyperlinkId = xx.HyperlinkId,
            //                    Title = xx.Title,
            //                    Url = xx.Url
            //                }),
            //                Photos = x.Photo.Select(xx => new PhotoVm()
            //                {
            //                    PhotoId = xx.PhotoId
            //                })
            //            });


            result.Success = true;
            result.Data = data.FirstOrDefault();
            //result.Data = new ForumVm
            //{
            //    Category = z.Category,
            //    CloseDate = z.CloseDate,
            //    Description = z.Description,
            //    ForumId = z.ForumId,
            //    Holder = z.Holder,
            //    OpenDate = z.OpenDate,
            //    Subject = z.Subject,
            //    UpperId = z.UpperId,
            //    Announcement = z.Announcement,
            //    ForumActivity = z.Activity.Where(x=>x.ActivityType==ActivityType.論壇活動 && x.IsEnabled).Select(x =>
            //    new ActivityVm
            //    {
            //        CreatedDate = x.CreatedDate,
            //        ActivityId = x.ActivityId,
            //        Subject = x.Subject
            //    }).ToList(),
            //    WorkActivity=z.Activity.Where(x=>x.ActivityType==ActivityType.工作坊 && x.IsEnabled).Select(x =>
            //    new ActivityVm
            //    {
            //        CreatedDate = x.CreatedDate,
            //        ActivityId = x.ActivityId,
            //        Subject = x.Subject
            //    }).ToList(),
            //    NormalDocuments = z.Document.Where(x => x.DocumentType == DocType.一般文件 && x.IsEnabled)
            //    .Select(x => new DocumentVm
            //    {
            //        FileName = x.FileName,
            //        DocumentId = x.DocumentId,
            //        CreatedDate = x.CreatedDate
            //    }).ToList(),
            //    GovReplyDocuments = z.Document.Where(x => x.DocumentType == DocType.市府回應 && x.IsEnabled)
            //    .Select(x => new DocumentVm
            //    {
            //        FileName = x.FileName,
            //        DocumentId = x.DocumentId,
            //        CreatedDate = x.CreatedDate
            //    }).ToList(),
            //    HyperLinks = z.Hyperlink.Where(x => x.IsEnabled)
            //     .Select(x => new HyperlinkVm
            //     {
            //         HyperlinkId = x.HyperlinkId,
            //         Title = x.Title,
            //         Url = x.Url
            //     }).ToList(),
            //    Photos = z.Photo.Select(x => new PhotoVm
            //    {
            //        PhotoId = x.PhotoId   
            //    }).ToList()
            //};

            return result;

        }

        public Result<Forum> UpdateForum(Forum model, bool? isSubForum = null)
        {
            var result = new Result<Forum>(false);
            try
            {
                var forum = GetForum(model.ForumId);
                if (forum == null)
                {
                    result.Message = "該公民論壇不存在！";
                    return result;
                }
                forum.Category = model.Category;

                if (isSubForum.HasValue && isSubForum.Value)
                {
                    forum.Holder = model.Holder;
                    forum.DataSetUnitId = dataSetService.GetUnitList().Where(x => x.PageName == model.Holder)
                        .Select(x => x.PageGuid).Single();
                }
                else
                {
                    forum.DataSetUnitId = model.DataSetUnitId;
                }

                forum.Subject = model.Subject;
                forum.Description = model.Description;
                forum.OpenDate = model.OpenDate;
                forum.CloseDate = model.CloseDate;
                forum.Announcement = model.Announcement;

                if (true)
                {
                    Document doc = new Document
                    {

                    };
                }

                result.Success = _repository.SaveChanges() > 0;
                if (result.Success)
                {
                    result.Data = forum;
                }
                else
                {
                    result.Message = "更新失敗!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<Forum> RemoveForum(Guid id)
        {
            var result = new Result<Forum>(false);
            try
            {
                var forum = _repository.Get(x => x.ForumId == id && x.IsEnabled);
                if (forum == null)
                {
                    result.Message = "該資料不存在!";
                    return result;
                }
                forum.IsEnabled = false;
                result.Success = _repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public IQueryable<Document> GetDocumentWithType(Guid id, DocType docType)
        {
            return GetForum(id).Document.Where(x => x.IsEnabled && x.DocumentType == docType).AsQueryable().AsNoTracking();
        }

        public Result<Hyperlink> CreateHyperlink(Guid id, Hyperlink model)
        {
            var result = new Result<Hyperlink>();
            try
            {
                var forum = GetForum(id);
                if (forum == null)
                {
                    result.Message = "相關連結不存在!";
                    return result;
                }
                //model.Forum = forum;
                model.IsEnabled = true;
                forum.Hyperlink.Add(model);
                result.Success = _repository.SaveChanges() > 0;
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Message = "新增失敗! \n" + ex.Message;
                throw;
            }
            return result;
        }

        public IQueryable<Hyperlink> GetHyperlink(Guid id)
        {
            return GetForum(id).Hyperlink.Where(x => x.IsEnabled).AsQueryable().AsNoTracking();
        }

        public IQueryable<Discuss> GetDiscuss(Guid id)
        {
            return GetForum(id).Discuss.Where(x => x.IsEnabled)
                .OrderByDescending(x => x.CreatedDate).AsQueryable().Include(x => x.AspNetUsers).AsNoTracking();
        }

        public Result<JObject> GetForumDiscuss(Guid id, DateTime startDate, DateTime endDate)
        {

            Result<JObject> result = new Result<JObject>();
            var forum = _repository.Where(x => x.ForumId == id && x.IsEnabled).FirstOrDefault();
            if (forum == null)
            {
                result.Message = "公民論壇不存在!";
                return result;
            }
            var jResult = new JObject
            {
                {"Name", forum.Subject}
            };
            var ja = new JArray();
            var end = endDate.AddDays(1);
            var discuss = forum.Discuss
                .Where(x => x.ForumId == id && x.IsEnabled && x.CreatedDate >= startDate && x.CreatedDate < end)
                .AsQueryable().Include(x => x.AspNetUsers)
                .OrderByDescending(x => x.CreatedDate).ToList();

            if (discuss.Count == 0)
            {
                result.Message = "該時間區間內沒有討論資料!";
                return result;
            }

            foreach (var d in discuss)
            {
                var jo = new JObject
                {
                    {"ID", d.DiscussId},
                    {"留言", d.Message.Replace("<br>",string.Empty)},
                    {"使用者", d.AspNetUsers.NickName},
                    {"Email", d.AspNetUsers.Email},
                    {"時間", d.CreatedDate}
                };
                ja.Add(jo);
            }
            jResult.Add("Data", ja);
            result.Success = true;
            result.Data = jResult;
            return result;
        }

        /// <summary>
        /// 子議題清單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Forum> GetSubForums(Guid id)
        {
            return _repository.Where(x => x.IsEnabled && x.UpperId == id).AsNoTracking();
        }

        /// <summary>
        /// Restful-Api用，取得列表
        /// 取得公民論壇之清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiForumVm> GetList()
        {
            return _repository.Where(x => x.IsEnabled && x.ForumType == ForumType.公民論壇 && x.UpperId == null)
                              .OrderByDescending(x => x.OpenDate)
                              .Select(x => new ApiForumVm {
                                  Id=x.ForumId,
                                  Subject=x.Subject,
                                  UnitName = x.DataSetUnit.Title,
                                  OpenDate = x.OpenDate
                              });
        }

        /// <summary>
        /// Restful-Api用，取得列表
        /// 取得單一公民論壇之欄位資料內容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiForumDetailVm GetDetail(Guid id)
        {
            return _repository.Where(x => x.IsEnabled && x.ForumId == id)
                              .ToList().Select(x => new ApiForumDetailVm {
                                  Id = x.ForumId,
                                  Category = x.Category,
                                  Subject = x.Subject,
                                  UnitName = x.DataSetUnit.Title,
                                  Description = x.Description,
                                  Announcement = x.Announcement,
                                  OpenDate = x.OpenDate,
                                  CloseDate = x.CloseDate,
                                  Documents = x.Document.Where(d => d.DocumentType == DocType.一般文件 && d.IsEnabled)
                                                        .Select(d => new PageNameVm {
                                                            PageGuid = d.DocumentId,
                                                            PageName = d.FileName,
                                                            PageUrl = $"{_urlName}/Document/{d.DocumentId}"
                                                        }),
                                  Links = x.Hyperlink.Where(h => h.IsEnabled)
                                                     .Select(h => new HyperlinkVm {
                                                         HyperlinkId = h.HyperlinkId,
                                                         Title = h.Title,
                                                         Url = h.Url
                                                     }),
                                  Replies = x.Document.Where(d => d.DocumentType == DocType.市府回應 && d.IsEnabled)
                                                      .Select(d => new PageNameVm {
                                                          PageGuid = d.DocumentId,
                                                          PageName = d.FileName,
                                                          PageUrl = $"{_urlName}/Document/{d.DocumentId}"
                                                      })
                              }).FirstOrDefault();
        }
    }
}