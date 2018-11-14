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
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class DiscussService : IDiscussService
    {
        private readonly IRepository<Discuss> repository;
        private readonly IRepository<Forum> forumRepository;
        private readonly IRepository<AspNetUsers> userRepository;
        private readonly IRepository<Tag> tagRepository;
        private readonly OpenGovContext _db;
        private readonly string _urlName = new WebSite().GetWebSite();

        public DiscussService(IRepository<Discuss> repository, IRepository<Forum> forumRepository, IRepository<AspNetUsers> userReporsitory, IRepository<Tag> tagRepository)
        {
            this.repository = repository;
            this.forumRepository = forumRepository;
            this.userRepository = userReporsitory;
            this.tagRepository = tagRepository;
            this._db = repository.Db;
        }

        public Result<Discuss> AddMessage(Guid id, string message, string userId)
        {
            var result = new Result<Discuss>(false);
            try
            {
                var forum = forumRepository.Get(x => x.ForumId == id && x.IsEnabled);
                if (forum == null)
                {
                    result.Message = "論壇活動不存在!";
                    return result;
                }
                var discuss = new Discuss()
                {
                    ForumId = id,
                    UserId = userId,
                    UpperId = null,
                    Message = message.Trim(),
                    IsEnabled = true
                };
                forum.Discuss.Add(discuss);
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
        /// <param name="id">Forum ID</param>
        /// <param name="message">留言內容</param>
        /// <param name="userId">使用者ID</param>
        /// <param name="tagIds">標籤ID</param>
        /// <param name="upperID">回覆的留言的ID</param>
        /// <returns></returns>
        public Result<DiscussVm> CreateMessage(Guid id, string message, string userId, List<Guid> tagIds, Guid? upperID = null)
        {
            var result = new Result<DiscussVm>(false);
            try
            {
                var forum = forumRepository.Get(x => x.ForumId == id && x.IsEnabled);
                if (forum == null)
                {
                    result.Message = "論壇活動不存在!";
                    return result;
                }

                var discuss = new Discuss()
                {
                    DiscussId = Guid.NewGuid(),
                    ForumId = id,
                    UserId = userId,
                    UpperId = upperID,
                    Message = message.Trim(),
                    IsEnabled = true
                };

                tagIds.ForEach(tagId =>
                {
                    discuss.Tag.Add(tagRepository.Where(x => x.TagId == tagId).Single());
                });

                forum.Discuss.Add(discuss);
                result.Success = repository.Create(discuss) > 0;
                if (result.Success)
                {
                    var user = userRepository.Where(x => x.Id == userId).Single();

                    result.ID = discuss.DiscussId;

                    result.Data = new DiscussVm
                    {
                        DiscussId = discuss.DiscussId,
                        CreatedDate = DateTime.Now,
                        ForumId = id,
                        UserId = userId,
                        UpperId = upperID,
                        Message = message.Trim(),
                        AspNetUser = new AspNetUserVm
                        {
                            Id = userId,
                            ImageUrl = (user.ImageUrl.Contains("http") ? user.ImageUrl : _urlName + "/" + user.ImageUrl.Replace("~/", "/")),
                            NickName = user.NickName
                        },
                        Tags = discuss.Tag.OrderBy(x => x.Sort).Select(x => new TagVm { TagId = x.TagId, TagName = x.TagName })
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
        /// <param name="id">Forum ID</param>
        /// <param name="currentPage">目前頁數</param>
        /// <param name="pageSize">一頁筆數</param>
        /// <returns></returns>
        public PageResult<DiscussVm> GetAllDiscussion(Guid id, int currentPage, int pageSize)
        {
            var result = new PageResult<DiscussVm>();

            var forum = this.forumRepository.Where(x => x.ForumId == id).AsNoTracking().Single();
            if (forum == null)
            {
                result.Message = "論壇活動不存在!";
                return result;
            }

            //var context = repository.Where(x => x.ForumId == id).Include(x => x.Tag).Include(x => x.AspNetUsers).OrderByDescending(x => x.IsTop).ThenByDescending(x => x.CreatedDate).ToList();

            var context = repository.Where(x => x.ForumId == id).Include(x => x.Tag).Include(x => x.AspNetUsers);

            result.Data = context.OrderByDescending(x => x.IsTop).ThenByDescending(x => x.CreatedDate)
                .Where(x => x.UpperId == null).Skip((currentPage - 1) * pageSize).Take(pageSize)
                .Select(x => new DiscussVm
                {
                    DiscussId = x.DiscussId,
                    ForumId = x.ForumId,
                    UserId = x.UserId,
                    Message = x.Message,
                    Tags = x.Tag.OrderBy(y => y.Sort).Select(y => new TagVm { TagId = y.TagId, TagName = y.TagName }),
                    PushUserIds = x.PushUser.Select(y => y.Id),
                    CreatedDate = x.CreatedDate,
                    IsTop = x.IsTop,
                    AspNetUser = new AspNetUserVm
                    {
                        Id = x.AspNetUsers.Id,
                        ImageUrl = (x.AspNetUsers.ImageUrl.Contains("http") ? x.AspNetUsers.ImageUrl : _urlName + "/" + x.AspNetUsers.ImageUrl.Replace("~/", "/")),
                        NickName = x.AspNetUsers.NickName
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
        public Result<List<DiscussVm>> GetReply(Guid discussId)
        {
            var result = new Result<List<DiscussVm>>(true);
            var context = repository.Where(x => x.UpperId == discussId).AsNoTracking().Include(x => x.AspNetUsers).OrderByDescending(x => x.CreatedDate).ToList();

            result.Data = context.Select(x =>
                 new DiscussVm
                 {
                     UserId = x.UserId,
                     DiscussId = x.DiscussId,
                     ForumId = x.ForumId,
                     UpperId = x.UpperId,
                     Message = x.Message,
                     PushUserIds = x.PushUser.Select(y => y.Id),
                     CreatedDate = x.CreatedDate,
                     AspNetUser = new AspNetUserVm
                     {
                         Id = x.AspNetUsers.Id,
                         ImageUrl = (x.AspNetUsers.ImageUrl.Contains("http") ? x.AspNetUsers.ImageUrl : _urlName + "/" + x.AspNetUsers.ImageUrl.Replace("~/", "/")),
                         NickName = x.AspNetUsers.NickName
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
        public Result<DiscussVm> PushMessage(Guid discussId, Guid pushUserId)
        {
            var result = new Result<DiscussVm>();
            try
            {
                var pushUser = userRepository.Where(x => x.Id.Equals(pushUserId.ToString())).Single();
                if (pushUser == null)
                {
                    throw new Exception("Push_User Null");
                }

                var discuss = repository.Where(x => x.DiscussId == discussId).Include(x => x.AspNetUsers).Single();
                if (discuss == null)
                {
                    throw new Exception("Discuss Null");
                }

                if (discuss.AspNetUsers.Id == pushUserId.ToString())
                {
                    throw new Exception("Push Self Error");
                }

                discuss.PushUser.Add(pushUser);

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
        /// <param name="forumId">forumId</param>
        /// <returns></returns>
        public IQueryable<TagVm> GetAllTags(Guid forumId)
        {
            var data = tagRepository.Where(x => x.IsEnabled && x.ForumId == forumId).OrderBy(x => x.Sort).AsNoTracking()
                 .Select(x => new TagVm
                 {
                     TagId = x.TagId,
                     TagName = x.TagName
                 });

            return data;
        }

        /// <summary>
        /// 取得子Forum
        /// </summary>
        /// <param name="superForumId">父Forum ID</param>
        /// <returns></returns>
        public List<ForumVm> GetSubForum(Guid superForumId)
        {
            return forumRepository.Where(x => x.UpperId == superForumId && x.IsEnabled).AsNoTracking().Select(x => new ForumVm
            {
                Subject = x.Subject,
                Description = x.Description,
                ForumId = x.ForumId,
                UpperId = x.UpperId,
                DiscussAmount = x.Discuss.Count()
            }).ToList();
        }

        #region New

        /// <summary>
        /// 取得主題下的留言-New
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public PageResult<DiscussVm> GetAllDiscussionNew(DiscussSearchVm key)
        {
            var result = new PageResult<DiscussVm>();
            var forumData = _db.Forum.Find(key.ForumId);
            if (forumData == null)
            {
                result.Message = "論壇活動不存在";
                return result;
            }

            var data = (from x in _db.Discuss
                        where x.ForumId == key.ForumId && x.UpperId == null
                        join u in _db.Discuss on x.DiscussId equals u.UpperId into pu
                        select new DiscussVm()
                        {
                            DiscussId = x.DiscussId,
                            ForumId = x.ForumId,
                            UserId = x.UserId,
                            Message = x.Message,
                            IsTop = x.IsTop,
                            Tags = x.Tag.OrderBy(c => c.Sort).Select(c => new TagVm()
                            {
                                TagId = c.TagId,
                                TagName = c.TagName
                            }),
                            PushUserIds = x.PushUser.Select(c => c.Id),
                            ReplyAmount = pu.Count(),
                            CreatedDate = x.CreatedDate,
                            AspNetUser = new AspNetUserVm()
                            {
                                Id = x.AspNetUsers.Id,
                                ImageUrl = (x.AspNetUsers.ImageUrl.Contains("http") ? x.AspNetUsers.ImageUrl : _urlName + "/" + x.AspNetUsers.ImageUrl.Replace("~/", "/")),
                                NickName = x.AspNetUsers.NickName
                            }
                        });

            //var data = _db.Discuss.Include(x => x.Tag).Include(x => x.AspNetUsers).Where(x => x.ForumId == key.ForumId && x.UpperId == null).Select(x => new DiscussVm()
            //{
            //    DiscussId = x.DiscussId,
            //    ForumId = x.ForumId,
            //    UserId = x.UserId,
            //    Message = x.Message,
            //    Tags = x.Tag.Select(c => new TagVm()
            //    {
            //        TagId = c.TagId,
            //        TagName = c.TagName
            //    }),
            //    PushUserIds = x.PushUser.Select(c => c.Id),
            //    CreatedDate = x.CreatedDate,
            //    AspNetUser = new AspNetUserVm()
            //    {
            //        Id = x.AspNetUsers.Id,
            //        ImageUrl = x.AspNetUsers.ImageUrl,
            //        NickName = x.AspNetUsers.NickName
            //    }
            //});

            if (!string.IsNullOrEmpty(key.KeyWord))
            {
                data = data.Where(x => x.Message.Contains(key.KeyWord));
            }

            if (key.TagId.HasValue)
            {
                data = data.Where(x => x.Tags.Select(c => c.TagId).Contains(key.TagId.Value));
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
                var replyData = _db.Discuss.FirstOrDefault(x => x.DiscussId == id && x.UpperId.HasValue);
                if (replyData == null)
                {
                    return result;
                }
                //取得主題
                var data = _db.Discuss.FirstOrDefault(x => x.DiscussId == replyData.UpperId && !x.UpperId.HasValue);
                if (data != null)
                {
                    string toEmail = data.AspNetUsers.Email;
                    string toName = data.AspNetUsers.NickName;

                    //取得所有回覆內容
                    var reply = _db.Discuss.Where(x => x.UpperId == data.DiscussId);
                    string bccEmail = string.Join(",", reply.Where(x => x.AspNetUsers.Email != toEmail).Select(x => x.AspNetUsers.Email).Distinct().ToArray());

                    string mailBody = string.Empty;
                    using (FileStream fs = File.Open(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/ForumReply.html"), FileMode.Open))
                    {
                        string urlName = _urlName;
                        StreamReader sr = new StreamReader(fs);
                        mailBody = sr.ReadToEnd();
                        mailBody = mailBody.Replace(@"{{message}}", HtmlSplit.SplitWord(replyData.Message, 50, ".."));
                        mailBody = mailBody.Replace(@"{{urlName}}", urlName + "/Forum/Detail/" + replyData.ForumId + "?did=" + replyData.UpperId);
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
        public IEnumerable<ApiDiscussVm> GetList()
        {
            return repository.Where(x => x.IsEnabled).OrderBy(x => x.ForumId).ThenByDescending(x => x.CreatedDate)
                             .ToList().Select(x => new ApiDiscussVm {
                                 Id= x.DiscussId,
                                 ForumId = x.ForumId,
                                 Subject = x.Forum.Subject,
                                 Tags = x.Tag.Where(t => t.IsEnabled).Select(t => t.TagName)
                             });
        }

        /// <summary>
        /// Restful-Api用
        /// 取得討論區留言之欄位資料內容
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiDiscussDetailVm> GetDetail(Guid id,string keywrod)
        {
            var kw = keywrod.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var query = repository.Where(x => x.IsEnabled && x.ForumId == id);
            return kw.Aggregate(query, (item, k) => item.Where(x => x.Tag.Any(t => t.TagName.Contains(k))))
                     .Select(x => new ApiDiscussDetailVm {
                         Id=  x.DiscussId,
                         ForumId = x.ForumId,
                         Subject = x.Forum.Subject,
                         UserName = x.AspNetUsers.UserName,
                         CreateDate = x.CreatedDate,
                         Message = x.Message,
                         UserPush = x.PushUser.Count,
                         Tags = x.Tag.Where(t=>t.IsEnabled).Select(t=>t.TagName)
                     });   
        }

        /// <summary>
        /// Restful-Api用
        /// 取得論壇子議題之清單內容
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiSubForumVm> GetSubforumList()
        {
            return forumRepository.Where(x => x.UpperId != null && x.IsEnabled).AsNoTracking().ToList()
                                  .Select(x => new ApiSubForumVm {
                                      Id =  x.UpperId.Value,
                                      Name = forumRepository.Where(y=>y.ForumId==x.UpperId.Value).FirstOrDefault().Subject,
                                      SubId = x.ForumId,
                                      SubName = x.Subject
                                  }).ToList();
        }

        /// <summary>
        /// Restful-Api用
        /// 取得論壇子議題之欄位資料內容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ApiSubForumDetailVm> GetSubforumDetail(Guid id)
        {
            return forumRepository.Where(x => x.ForumId == id).AsNoTracking().ToList()
                                  .Select(x => new ApiSubForumDetailVm {
                                      Id = x.UpperId.Value,
                                      Name = forumRepository.Where(y => y.ForumId == x.UpperId.Value).FirstOrDefault().Subject,
                                      SubId = x.ForumId,
                                      SubName = x.Subject,
                                      Category = x.Category,
                                      UnitName = x.DataSetUnit.Title,
                                      Description = x.Description,
                                      OpenDate = x.OpenDate,
                                      CloseDate = x.CloseDate,
                                      Documents  = x.Document.Where(d => d.DocumentType == DocType.一般文件 && d.IsEnabled)
                                                        .Select(d => new PageNameVm
                                                        {
                                                            PageGuid = d.DocumentId,
                                                            PageName = d.FileName,
                                                            PageUrl = $"{_urlName}/Document/{d.DocumentId}"
                                                        }),
                                  }).ToList();
        }

        #endregion New
    }
}