using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ActivityService : IActivityService
    {
        private readonly IRepository<Activity> repository;
        private readonly IRepository<Forum> forumRepository;

        public ActivityService(IRepository<Activity> repository, IRepository<Forum> forumRepository)
        {
            this.repository = repository;
            this.forumRepository = forumRepository;
        }

        public Activity GetActivity(Guid id)
        {
            return repository.Get(x => x.ActivityId == id && x.IsEnabled);
        }

        public IQueryable<Activity> GetForumActivitiesByType(Guid id, ActivityType type)
        {
            return repository.Where(x => x.IsEnabled && x.ForumId == id && x.ActivityType == type).Include(x => x.Document).AsNoTracking();
        }

        public Result<Activity> Remove(Guid id)
        {
            var result = new Result<Activity>(false);
            try
            {
                var act = repository.Get(x => x.ActivityId == id && x.IsEnabled);
                if (act == null)
                {
                    result.Message = "論壇活動不存在!";
                    return result;
                }

                act.IsEnabled = false;
                result.Success = repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<Activity> CreateActivity(Guid id, Activity model)
        {
            var result = new Result<Activity>();
            try
            {
                model.ForumId = id;
                model.IsEnabled = true;
                result.Success = repository.Create(model) > 0;
                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<Activity> UpdateActivity(Guid id, Activity model)
        {
            var result = new Result<Activity>(false);
            try
            {
                var act = GetActivity(model.ActivityId);
                if (act == null)
                {
                    result.Message = "該論壇活動不存在！";
                    return result;
                }
                act.Subject = model.Subject;
                act.HoldDate = model.HoldDate;
                act.Location = model.Location;
                act.Description = model.Description;

                result.Success = repository.SaveChanges() > 0;
                if (result.Success)
                {
                    result.Data = act;
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

        public IQueryable<Document> GetDocument(Guid id)
        {
            return GetActivity(id).Document.Where(x => x.IsEnabled).AsQueryable().AsNoTracking();
        }

        public ActivityVm GetActivityById(Guid id)
        {
            var act =
                repository.Where(x => x.ActivityId == id && x.IsEnabled).AsNoTracking().Select(x => new ActivityVm()
                {
                    ActivityId = x.ActivityId,
                    Subject = x.Subject,
                    Description = x.Description,
                    HoldDate = x.HoldDate,

                    Location = x.Location,
                    ActivityType = x.ActivityType,
                    Attachments = x.Document.Where(d => d.IsEnabled).Select(d => new IdName()
                    {
                        Id = d.DocumentId.ToString(),
                        Name = d.FileName
                    }).ToList()
                }).FirstOrDefault();
            return act;
        }
    }
}