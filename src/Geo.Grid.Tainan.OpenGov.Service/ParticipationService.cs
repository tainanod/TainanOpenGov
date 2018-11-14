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
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ParticipationService : IParticipationService
    {
        private readonly IRepository<Participation> _repository;
        private readonly IRepository<PhotoExt> _photorepository;
        private readonly OpenGovContext _db = new OpenGovContext();
        private string _anonymousId = new IdentityProfile().GetIdentityProfileAnonymousId();
        private string _userId = new IdentityProfile().GetIdentityProfile();
        private string _urlName = new WebSite().GetWebSite();
        private readonly Interface.Admin.IDataSetService dataSetService;

        public ParticipationService(IRepository<Participation> repository, IRepository<PhotoExt> photorepository, Interface.Admin.IDataSetService dataSetService)
        {
            this._repository = repository;
            this.dataSetService = dataSetService;
            
            _photorepository = photorepository;
        }

        /// <summary>
        /// 取得列表資料
        /// </summary>
        /// <returns></returns>
        public IQueryable<Participation> GetQuery()
        {
            var user = _db.AspNetUsers.FirstOrDefault(x => x.Id == _userId);
            var sysRole = user.AspNetRoles.Any(x => x.Name == "Office") ? SysRoles.局處管理員 : SysRoles.系統管理者;
            var unitId = sysRole == SysRoles.局處管理員 ? user.DataSetUnitId : null;
            return sysRole == SysRoles.局處管理員 ?
                    _repository.Where(x => x.IsEnabled && x.DataSetUnitId == unitId).AsNoTracking() :
                    _repository.Where(x => x.IsEnabled).AsNoTracking();
        }

        /// <summary>
        /// 建立 資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<Participation> Create(Participation model)
        {
            var result = new Result<Participation>(false);
            try
            {
                model.IsEnabled = true;
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

        /// <summary>
        /// 取得 市政參與 資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Participation GetData(Guid id)
        {
            return _db.Participations.Include(x=>x.Photos)
                    .Include(x=>x.ParticipationHyperlinks)
                    .Include(x=>x.ParticipationDocuments)
                    .FirstOrDefault(x => x.ParticipationId == id && x.IsEnabled);
        }

        /// <summary>
        /// 更新 市政參與議題
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<Participation> UpdateData(Participation model)
        {
            var result = new Result<Participation>(false);
            try
            {
                var data = GetData(model.ParticipationId);
                if (data == null)
                {
                    result.Message = "該市政參與議題不存在！";
                    return result;
                }
                data.Category = model.Category;
                data.DataSetUnitId = model.DataSetUnitId;
                data.Subject = model.Subject;
                data.Description = model.Description;
                data.Announcement = model.Announcement;
                data.EnableDiscuss = model.EnableDiscuss;
                data.OpenDate = model.OpenDate;
                data.CloseDate = model.CloseDate;
                data.IsMothball = model.IsMothball;

                result.Success = _db.SaveChanges() > 0;
                if (result.Success)
                {
                    result.Data = data;
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

        /// <summary>
        /// 移除 資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<Participation> RemoveData(Guid id)
        {
            var result = new Result<Participation>(false);
            try
            {
                var forum = _repository.Get(x => x.ParticipationId == id && x.IsEnabled);
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

        /// <summary>
        /// 封存或取消封存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<Participation> MothballData(Guid id)
        {
            var result = new Result<Participation>(false);
            try
            {
                var query = _repository.Get(x => x.ParticipationId == id && x.IsEnabled);
                if (query == null)
                {
                    result.Message = "該資料不存在!";
                    return result;
                }
                query.IsMothball = !query.IsMothball;
                result.Success = _repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 新增 圖片關聯
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool addPhoto(Guid id, Photo photo)
        {
            try
            {
                var data = _repository.Where(x => x.IsEnabled && x.ParticipationId == id).Include(x=>x.Photos).FirstOrDefault();
                foreach (var p in data.Photos.ToList())
                {
                    data.Photos.Remove(p);
                }
                data.Photos.Add(photo);
                return _repository.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 取得 附件 資訊
        /// </summary>
        /// <param name="id"></param>
        /// <param name="docType"></param>
        /// <returns></returns>
        public IQueryable<ParticipationDocument> GetDocumentWithType(Guid id, DocType docType)
        {
            return GetData(id).ParticipationDocuments.Where(x => x.IsEnabled && x.DocumentType == docType).AsQueryable().AsNoTracking();
        }

        /// <summary>
        /// 取得 超連結 資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<ParticipationHyperlink> GetHyperlink(Guid id)
        {
            return GetData(id).ParticipationHyperlinks.Where(x => x.IsEnabled).AsQueryable().AsNoTracking();
        }

        /// <summary>
        /// 建立超連結資料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<ParticipationHyperlink> CreateHyperlink(Guid id, ParticipationHyperlink model)
        {
            var result = new Result<ParticipationHyperlink>();
            try
            {
                var data = GetData(id);
                if (data == null)
                {
                    result.Message = "相關連結不存在!";
                    return result;
                }
                model.IsEnabled = true;
                _db.ParticipationHyperlinks.Add(model);
                data.ParticipationHyperlinks.Add(model);
                result.Success = _db.SaveChanges() > 0;
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Message = "新增失敗! \n" + ex.Message;
                throw;
            }
            return result;
        }

        /// <summary>
        /// 市政資料 論壇匯出
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Result<JObject> GetForumDiscuss(Guid id, DateTime startDate, DateTime endDate)
        {

            Result<JObject> result = new Result<JObject>();
            var data = _db.Participations.Include(x=>x.ParticipationDiscusses.Select(y=>y.AspNetUser))
                .FirstOrDefault(x => x.ParticipationId == id && x.IsEnabled);

            if (data == null)
            {
                result.Message = "資料不存在!";
                return result;
            }
            var jResult = new JObject
            {
                {"Name", data.Subject}
            };
            var ja = new JArray();
            var end = endDate.AddDays(1);
            var discuss = data.ParticipationDiscusses
                .Where(x => x.ParticipationId == id && x.IsEnabled && x.CreatedDate >= startDate && x.CreatedDate < end)
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
                    {"使用者", d.AspNetUser.NickName},
                    {"Email", d.AspNetUser.Email},
                    {"時間", d.CreatedDate}
                };
                ja.Add(jo);
            }
            jResult.Add("Data", ja);
            result.Success = true;
            result.Data = jResult;
            return result;
        }

        #region 前台

        /// <summary>
        /// 前台 取得 市政參與 資料
        /// </summary>
        /// <returns></returns>
        public IQueryable<Participation> GetDataByType()
        {
            return _repository.Where(x => x.IsEnabled && x.IsMothball == false).OrderByDescending(x => x.OpenDate).AsNoTracking();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<ParticipationDiscuss> GetDiscuss(Guid id)
        {
            return GetData(id).ParticipationDiscusses.Where(x => x.IsEnabled)
                .OrderByDescending(x => x.CreatedDate).AsQueryable().Include(x => x.AspNetUsers).AsNoTracking();
        }

        /// <summary>
        /// Restful-Api用，取得列表
        /// 取得公民論壇之清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiParticipationVm> GetList()
        {
            return _repository.Where(x => x.IsEnabled )
                              .OrderByDescending(x => x.OpenDate)
                              .Select(x => new ApiParticipationVm
                              {
                                  Id = x.ParticipationId,
                                  Subject = x.Subject,
                                  UnitName = _db.DataSetUnit.Where(y=>y.UnitId == x.DataSetUnitId).FirstOrDefault().Title,
                                  OpenDate = x.OpenDate
                              });
        }

        /// <summary>
        /// Restful-Api用，取得列表
        /// 取得單一公民論壇之欄位資料內容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiParticipationDetailVm GetDetail(Guid id)
        {
            return _repository.Where(x => x.IsEnabled && x.ParticipationId == id)
                              .ToList().Select(x => new ApiParticipationDetailVm
                              {
                                  Id = x.ParticipationId,
                                  Category = x.Category,
                                  Subject = x.Subject,
                                  UnitName = _db.DataSetUnit.Where(y => y.UnitId == x.DataSetUnitId).FirstOrDefault().Title,
                                  Description = x.Description,
                                  Announcement = x.Announcement,
                                  EnableDiscuss = x.EnableDiscuss,
                                  OpenDate = x.OpenDate,
                                  CloseDate = x.CloseDate,
                                  Documents = x.ParticipationDocuments.Where(d => d.DocumentType == DocType.一般文件 && d.IsEnabled)
                                                        .Select(d => new PageNameVm
                                                        {
                                                            PageGuid = d.DocumentId,
                                                            PageName = d.FileName,
                                                            PageUrl = $"{_urlName}/Document/{d.DocumentId}"
                                                        }),
                                  Links = x.ParticipationHyperlinks.Where(h => h.IsEnabled)
                                                     .Select(h => new HyperlinkVm
                                                     {
                                                         HyperlinkId = h.HyperlinkId,
                                                         Title = h.Title,
                                                         Url = h.Url
                                                     }),
                                  Replies = x.ParticipationDocuments.Where(d => d.DocumentType == DocType.市府回應 && d.IsEnabled)
                                                      .Select(d => new PageNameVm
                                                      {
                                                          PageGuid = d.DocumentId,
                                                          PageName = d.FileName,
                                                          PageUrl = $"{_urlName}/Document/{d.DocumentId}"
                                                      })
                              }).FirstOrDefault();
        }

        #endregion



        //public Forum GetForumWithRelation(Guid id)
        //{
        //    return _repository.Where(x => x.ForumId == id && x.IsEnabled)
        //        //.Include(x => x.Discuss)
        //        .Include(x => x.Hyperlink)
        //        .Include(x => x.Document)
        //        .Include(x => x.Activity)
        //        .FirstOrDefault();
        //}

        public Result<ParticipationVm> GetDataAndRelations(Guid id)
        {
            var result = new Result<ParticipationVm>();
            
            var oldData = DateTime.Today.AddDays(-7);

            var data = _db.Participations.Include(x => x.ParticipationHyperlinks).Include(x => x.ParticipationDocuments).Include(x => x.ParticipationActivities)
                .Include(x => x.ParticipationAnonymousClicks)
                .Where(x => x.IsEnabled && x.ParticipationId == id)
                .Select(x => new ParticipationVm()
                {
                    Category = x.Category,
                    EnableDiscuss = x.EnableDiscuss,
                    CloseDate = x.CloseDate,
                    Description = x.Description,
                    ParticipationId = x.ParticipationId,
                    OpenDate = x.OpenDate,
                    Subject = x.Subject,
                    Announcement = x.Announcement,
                    DataSetUnitId = x.DataSetUnitId,
                    DataSetUnitName = _db.DataSetUnit.Where(a => a.IsEnabled && a.UnitId == x.DataSetUnitId).FirstOrDefault().Title,
                    ParticipationActivities = x.ParticipationActivities.Where(xx => xx.ActivityType == ActivityType.論壇活動 && xx.IsEnabled).Select(xx => new ParticipationActivityVm()
                    {
                        CreatedDate = xx.CreatedDate,
                        ActivityId = xx.ActivityId,
                        Subject = xx.Subject
                    }).ToList(),
                    WorkActivity = x.ParticipationActivities.Where(xx => xx.ActivityType == ActivityType.工作坊 && xx.IsEnabled).Select(xx => new ParticipationActivityVm()
                    {
                        CreatedDate = xx.CreatedDate,
                        ActivityId = xx.ActivityId,
                        Subject = xx.Subject
                    }).ToList(),
                    NormalDocuments = x.ParticipationDocuments.Where(xx => xx.DocumentType == DocType.一般文件 && xx.IsEnabled).Select(xx => new ParticipationDocumentVm()
                    {
                        FileName = xx.FileName,
                        DocumentId = xx.DocumentId,
                        CreatedDate = xx.CreatedDate,
                        IsClick = x.ParticipationAnonymousClicks.FirstOrDefault(c => c.AnonymousId == _anonymousId && c.ClickType == "B" && c.ClickId == xx.DocumentId.ToString()) != null || (xx.CreatedDate > oldData ? false : true)
                    }).ToList(),
                    GovReplyDocuments = x.ParticipationDocuments.Where(xx => xx.DocumentType == DocType.市府回應 && xx.IsEnabled).Select(xx => new ParticipationDocumentVm()
                    {
                        FileName = xx.FileName,
                        DocumentId = xx.DocumentId,
                        CreatedDate = xx.CreatedDate,
                        IsClick = x.ParticipationAnonymousClicks.FirstOrDefault(c => c.AnonymousId == _anonymousId && c.ClickType == "C" && c.ClickId == xx.DocumentId.ToString()) != null || (xx.CreatedDate > oldData ? false : true)
                    }).ToList(),
                    HyperLinks = x.ParticipationHyperlinks.Where(xx => xx.IsEnabled).Select(xx => new ParticipationHyperlinkVm()
                    {
                        HyperlinkId = xx.HyperlinkId,
                        Title = xx.Title,
                        Url = xx.Url,
                        IsClick = x.ParticipationAnonymousClicks.FirstOrDefault(c => c.AnonymousId == _anonymousId && c.ClickType == "A" && c.ClickId == xx.HyperlinkId.ToString()) != null || (xx.CreatedDate > oldData ? false : true)
                    }).ToList(),
                    Photos = x.Photos.Select(xx => new PhotoVm()
                    {
                        PhotoId = xx.PhotoId
                    }).ToList(),
                });
            

            result.Success = true;
            result.Data = data.FirstOrDefault();
            
            return result;
        }


        ///// <summary>
        ///// 子議題清單
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IQueryable<Forum> GetSubForums(Guid id)
        //{
        //    return _repository.Where(x => x.IsEnabled && x.UpperId == id).AsNoTracking();
        //}

    }
}