using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ParticipationDiscussService : IParticipationDiscussService
    {
        private readonly IRepository<ParticipationDiscuss> repository;
        private readonly IRepository<Participation> participationRepository;
        private readonly IRepository<AspNetUsers> userRepository;
        private readonly IRepository<ParticipationTag> tagRepository;
        private readonly OpenGovContext _db;
        private readonly string _urlName = new WebSite().GetWebSite();

        public ParticipationDiscussService(IRepository<ParticipationDiscuss> repository, IRepository<Participation> participationRepository, IRepository<AspNetUsers> userReporsitory, IRepository<ParticipationTag> tagRepository)
        {
            this.repository = repository;
            this.participationRepository = participationRepository;
            this.userRepository = userReporsitory;
            this.tagRepository = tagRepository;
            this._db = repository.Db;
        }

        public Result<ParticipationDiscuss> AddMessage(Guid id, string message, string userId)
        {
            var result = new Result<ParticipationDiscuss>(false);
            try
            {
                var data = participationRepository.Get(x => x.ParticipationId == id && x.IsEnabled);
                if (data == null)
                {
                    result.Message = "論壇活動不存在!";
                    return result;
                }
                var discuss = new ParticipationDiscuss()
                {
                    ParticipationId = id,
                    UserId = userId,
                    UpperId = null,
                    Message = message.Trim(),
                    IsEnabled = true
                };
                data.ParticipationDiscusses.Add(discuss);
                result.Success = repository.Create(discuss) > 0;
                if (result.Success)
                {
                    result.Data = discuss;
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
        /// 新增留言 /回覆
        /// </summary>
        /// <param name="id">Participation ID</param>
        /// <param name="message">留言內容</param>
        /// <param name="userId">使用者ID</param>
        /// <param name="tagIds">標籤ID</param>
        /// <param name="upperID">回覆的留言的ID</param>
        /// <returns></returns>
        public Result<ParticipationDiscussVm> CreateMessage(Guid id, string message, string userId, List<Guid> tagIds, Guid? upperID = null)
        {
            var result = new Result<ParticipationDiscussVm>(false);
            try
            {
                var data = participationRepository.Get(x => x.ParticipationId == id && x.IsEnabled);
                if (data == null)
                {
                    result.Message = "論壇活動不存在!";
                    return result;
                }

                var discuss = new ParticipationDiscuss()
                {
                    DiscussId = Guid.NewGuid(),
                    ParticipationId = id,
                    UserId = userId,
                    UpperId = upperID,
                    Message = message.Trim(),
                    IsEnabled = true
                };

                tagIds.ForEach(tagId =>
                {
                    discuss.ParticipationTags.Add(tagRepository.Where(x => x.TagId == tagId).Single());
                });

                data.ParticipationDiscusses.Add(discuss);
                result.Success = repository.Create(discuss) > 0;
                if (result.Success)
                {
                    var user = userRepository.Where(x => x.Id == userId).Single();

                    result.ID = discuss.DiscussId;

                    result.Data = new ParticipationDiscussVm
                    {
                        DiscussId = discuss.DiscussId,
                        CreatedDate = DateTime.Now,
                        ParticipationId = id,
                        UserId = userId,
                        UpperId = upperID,
                        Message = message.Trim(),
                        AspNetUser = new AspNetUserVm
                        {
                            Id = userId,
                            ImageUrl = (user.ImageUrl.Contains("http") ? user.ImageUrl : _urlName + "/" + user.ImageUrl.Replace("~/", "/")),
                            NickName = user.NickName
                        },
                        ParticipationTags = discuss.ParticipationTags.OrderBy(x => x.Sort).Select(x => new ParticipationTagVm { TagId = x.TagId, TagName = x.TagName }).ToList()
                    };
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
        /// 取得主題下的留言
        /// </summary>
        /// <param name="id">Participation ID</param>
        /// <param name="currentPage">目前頁數</param>
        /// <param name="pageSize">一頁筆數</param>
        /// <returns></returns>
        public PageResult<ParticipationDiscussVm> GetAllDiscussion(Guid id, int currentPage, int pageSize)
        {
            var result = new PageResult<ParticipationDiscussVm>();

            var data = this.participationRepository.Where(x => x.ParticipationId == id).AsNoTracking().Single();
            if (data == null)
            {
                result.Message = "論壇活動不存在!";
                return result;
            }
            
            var context = repository.Where(x => x.ParticipationId == id).Include(x => x.ParticipationTags).Include(x => x.AspNetUsers);

            result.Data = context.OrderByDescending(x => x.IsTop).ThenByDescending(x => x.CreatedDate)
                .Where(x => x.UpperId == null).Skip((currentPage - 1) * pageSize).Take(pageSize)
                .Select(x => new ParticipationDiscussVm
                {
                    DiscussId = x.DiscussId,
                    ParticipationId = x.ParticipationId,
                    UserId = x.UserId,
                    Message = x.Message,
                    ParticipationTags = x.ParticipationTags.OrderBy(y => y.Sort).Select(y => new ParticipationTagVm { TagId = y.TagId, TagName = y.TagName }).ToList(),
                    PushUserIds = x.AspNetUsers.Select(y => y.Id),
                    CreatedDate = x.CreatedDate,
                    IsTop = x.IsTop,
                    AspNetUser = new AspNetUserVm
                    {
                        Id = x.AspNetUser.Id,
                        ImageUrl = (x.AspNetUser.ImageUrl.Contains("http") ? x.AspNetUser.ImageUrl : _urlName + "/" + x.AspNetUser.ImageUrl.Replace("~/", "/")),
                        NickName = x.AspNetUser.NickName
                    },
                    ReplyAmount = context.Where(y => y.UpperId == x.DiscussId).Count()
                }).ToList();
            result.Total = context.Where(x => x.UpperId == null).Count();
            result.Success = true;
            result.PageSize = pageSize;
            result.CurrentPage = currentPage;
            result.TotalPage = result.Total / pageSize + ((result.Total % pageSize == 0) ? 0 : 1);
            return result;
        }

        /// <summary>
        /// 取得某留言的回覆
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        public Result<List<ParticipationDiscussVm>> GetReply(Guid discussId)
        {
            var result = new Result<List<ParticipationDiscussVm>>(true);
            var context = repository.Where(x => x.UpperId == discussId).AsNoTracking().Include(x => x.AspNetUser)
                .Include(x => x.AspNetUsers).OrderByDescending(x => x.CreatedDate).ToList();

            result.Data = context.Select(x =>
                 new ParticipationDiscussVm
                 {
                     UserId = x.UserId,
                     DiscussId = x.DiscussId,
                     ParticipationId = x.ParticipationId,
                     UpperId = x.UpperId,
                     Message = x.Message,
                     PushUserIds = x.AspNetUsers.Select(y => y.Id),
                     CreatedDate = x.CreatedDate,
                     AspNetUser = new AspNetUserVm
                     {
                         Id = x.AspNetUser.Id,
                         ImageUrl = (x.AspNetUser.ImageUrl.Contains("http") ? x.AspNetUser.ImageUrl : _urlName + "/" + x.AspNetUser.ImageUrl.Replace("~/", "/")),
                         NickName = x.AspNetUser.NickName
                     }
                 }).ToList();

            return result;
        }

        /// <summary>
        /// 推文
        /// </summary>
        /// <param name="discussId">推文的ID</param>
        /// <param name="pushUserId">推文者ID</param>
        /// <returns></returns>
        public Result<ParticipationDiscussVm> PushMessage(Guid discussId, Guid pushUserId)
        {
            var result = new Result<ParticipationDiscussVm>();
            try
            {
                var pushUser = userRepository.Where(x => x.Id.Equals(pushUserId.ToString())).Single();
                if (pushUser == null)
                {
                    throw new Exception("Push_User Null");
                }

                var discuss = repository.Where(x => x.DiscussId == discussId).Include(x => x.AspNetUser).Single();
                if (discuss == null)
                {
                    throw new Exception("Discuss Null");
                }

                if (discuss.AspNetUser.Id == pushUserId.ToString())
                {
                    throw new Exception("Push Self Error");
                }

                discuss.AspNetUsers.Add(pushUser);

                if (repository.Update(discuss) > 1)
                {
                    result.Success = true;
                }
                else
                {
                    result.Message = "不可重複推文";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 取得標籤
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public IQueryable<ParticipationTagVm> GetAllTags(Guid id)
        {
            var data = tagRepository.Where(x => x.IsEnabled && x.ParticipationId == id).OrderBy(x => x.Sort).AsNoTracking()
                 .Select(x => new ParticipationTagVm
                 {
                     TagId = x.TagId,
                     TagName = x.TagName
                 });

            return data;
        }
        
        #region New

        /// <summary>
        /// 取得主題下的留言-New
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public PageResult<ParticipationDiscussVm> GetAllDiscussionNew(ParticipationDiscussSearchVm key)
        {
            var result = new PageResult<ParticipationDiscussVm>();
            var participationData = _db.Participations.Find(key.ParticipationId);
            if (participationData == null)
            {
                result.Message = "論壇活動不存在";
                return result;
            }

            var data = (from x in _db.ParticipationDiscusses
                        where x.ParticipationId == key.ParticipationId && x.UpperId == null
                        join u in _db.ParticipationDiscusses on x.DiscussId equals u.UpperId into pu
                        select new ParticipationDiscussVm()
                        {
                            DiscussId = x.DiscussId,
                            ParticipationId = x.ParticipationId,
                            UserId = x.UserId,
                            Message = x.Message,
                            IsTop = x.IsTop,
                            ParticipationTags = x.ParticipationTags.OrderBy(c => c.Sort).Select(c => new ParticipationTagVm()
                            {
                                TagId = c.TagId,
                                TagName = c.TagName
                            }).ToList(),
                            PushUserIds = x.AspNetUsers.Select(c => c.Id),
                            ReplyAmount = pu.Count(),
                            CreatedDate = x.CreatedDate,
                            AspNetUser = new AspNetUserVm()
                            {
                                Id = x.AspNetUser.Id,
                                ImageUrl = (x.AspNetUser.ImageUrl.Contains("http") ? x.AspNetUser.ImageUrl : _urlName + "/" + x.AspNetUser.ImageUrl.Replace("~/", "/")),
                                NickName = x.AspNetUser.NickName
                            }
                        });
            
            if (!string.IsNullOrEmpty(key.KeyWord))
            {
                data = data.Where(x => x.Message.Contains(key.KeyWord));
            }

            if (key.TagId.HasValue)
            {
                data = data.Where(x => x.ParticipationTags.Select(c => c.TagId).Contains(key.TagId.Value));
            }

            if (key.SortId <= 1)
            {
                data = data.OrderByDescending(x => x.IsTop).ThenByDescending(x => x.CreatedDate).ThenByDescending(x => x.PushUserIds.Count());
            }
            else
            {
                data = data.OrderByDescending(x => x.IsTop).ThenByDescending(x => x.PushUserIds.Count()).ThenByDescending(x => x.CreatedDate);
            }

            var vmD = data.ToList();

            result.Data = vmD.Skip((key.CurrentPage - 1) * key.PageSize).Take(key.PageSize);
            result.Total = data.Count();
            result.Success = true;
            result.PageSize = key.PageSize;
            result.CurrentPage = key.CurrentPage;
            result.TotalPage = result.Total / key.PageSize + ((result.Total % key.PageSize == 0) ? 0 : 1);

            return result;
        }

        /// <summary>
        /// 信件管理-回覆時寄信
        /// UPPER_ID-要有值
        /// </summary>
        /// <param name="id">DISCUSS_ID</param>
        /// <returns></returns>
        public Result<string> GetReplyEmail(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                //取得回覆
                var replyData = _db.ParticipationDiscusses.FirstOrDefault(x => x.DiscussId == id && x.UpperId.HasValue);
                if (replyData == null)
                {
                    return result;
                }
                //取得主題
                var data = _db.ParticipationDiscusses.FirstOrDefault(x => x.DiscussId == replyData.UpperId && !x.UpperId.HasValue);
                if (data != null)
                {
                    string toEmail = data.AspNetUser.Email;
                    string toName = data.AspNetUser.NickName;

                    //取得所有回覆內容
                    var reply = _db.ParticipationDiscusses.Where(x => x.UpperId == data.DiscussId);
                    string bccEmail = string.Join(",", reply.Where(x => x.AspNetUser.Email != toEmail).Select(x => x.AspNetUser.Email).Distinct().ToArray());

                    string mailBody = string.Empty;
                    using (FileStream fs = File.Open(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/ForumReply.html"), FileMode.Open))
                    {
                        string urlName = _urlName;
                        StreamReader sr = new StreamReader(fs);
                        mailBody = sr.ReadToEnd();
                        mailBody = mailBody.Replace(@"{{message}}", HtmlSplit.SplitWord(replyData.Message, 50, ".."));
                        mailBody = mailBody.Replace(@"{{urlName}}", urlName + "/Participation/Detail/" + replyData.ParticipationId + "?did=" + replyData.UpperId);
                    }

                    var mailData = new WaitingMail();
                    mailData.TypeName = "公民論壇-討論區回覆";
                    mailData.ToName = toName;
                    mailData.ToEmail = toEmail;
                    mailData.BccEmail = bccEmail;
                    mailData.Subject = "臺南開放政府-您有一封回覆留言" + DateTime.Today.ToString("(yyyy/MM/dd)");
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
        /// Restful-Api用
        /// 取得列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiParticipationDiscussVm> GetList()
        {
            return repository.Where(x => x.IsEnabled).OrderBy(x => x.ParticipationId).ThenByDescending(x => x.CreatedDate)
                             .ToList().Select(x => new ApiParticipationDiscussVm
                             {
                                 Id= x.DiscussId,
                                 ParticipationId = x.ParticipationId,
                                 Subject = _db.Participations.Where(a => a.ParticipationId == x.ParticipationId).SingleOrDefault().Subject,
                                 Tags = x.ParticipationTags.Where(t => t.IsEnabled).Select(t => t.TagName)
                             });
        }

        /// <summary>
        /// Restful-Api用
        /// 取得討論區留言之欄位資料內容
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiParticipationDiscussDetailVm> GetDetail(Guid id,string keywrod)
        {
            var kw = keywrod.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var query = repository.Where(x => x.IsEnabled && x.ParticipationId == id);
            return kw.Aggregate(query, (item, k) => item.Where(x => x.ParticipationTags.Any(t => t.TagName.Contains(k))))
                     .Select(x => new ApiParticipationDiscussDetailVm
                     {
                         Id=  x.DiscussId,
                         ParticipationId = x.ParticipationId,
                         Subject = _db.Participations.Where(a => a.ParticipationId == x.ParticipationId).SingleOrDefault().Subject,
                         UserName = x.AspNetUser.UserName,
                         CreateDate = x.CreatedDate,
                         Message = x.Message,
                         UserPush = x.AspNetUsers.Count,
                         Tags = x.ParticipationTags.Where(t=>t.IsEnabled).Select(t=>t.TagName)
                     });   
        }

        #endregion New

        #region 後台
        
        /// <summary>
        /// 公民論壇-單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public ParticipationDataVm GetParticipationDetail(Guid id)
        {
            var data = _db.Participations.Where(x => x.ParticipationId == id).Select(x => new ParticipationDataVm()
            {
                ParticipationId = x.ParticipationId,
                Subject = x.Subject
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">id:公民論壇編號</param>
        /// <returns></returns>
        public IQueryable<ParticipationDiscussVm> GetList(SearchVm key)
        {
            var data = _db.ParticipationDiscusses.OrderByDescending(x => x.IsTop).ThenByDescending(x => x.CreatedDate)
                .Where(x => x.ParticipationId == key.id && !x.UpperId.HasValue).Select(x => new ParticipationDiscussVm()
                {
                    DiscussId = x.DiscussId,
                    ParticipationId = x.ParticipationId,
                    UserId = x.UserId,
                    UserNickName = x.AspNetUser.NickName,
                    Message = x.Message,
                    IsEnabled = x.IsEnabled,
                    IsTop = x.IsTop,
                    CreatedDate = x.CreatedDate
                });
            return data;
        }

        /// <summary>
        /// 置頂/非置頂
        /// </summary>
        /// <param name="id">discussId</param>
        /// <returns></returns>
        public Result<string> GetUpdateTop(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.ParticipationDiscusses.Find(id);
                if (data != null)
                {
                    data.IsTop = (data.IsTop ? false : true);
                    result.Success = repository.SaveChanges() > 0;
                    result.ID = data.ParticipationId;
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