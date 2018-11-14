using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ParticipationActivityService : IParticipationActivityService
    {
        private readonly IRepository<ParticipationActivity> repository;
        private readonly IRepository<Participation> participation;
        private readonly OpenGovContext _db = new OpenGovContext();

        public ParticipationActivityService(IRepository<ParticipationActivity> repository, IRepository<Participation> forumRepository)
        {
            this.repository = repository;
            this.participation = forumRepository;
        }

        public ParticipationActivity GetActivity(Guid id)
        {
            return _db.ParticipationActivities.Include(x=>x.ParticipationDocuments).FirstOrDefault(x => x.ActivityId == id && x.IsEnabled);
        }

        public IQueryable<ParticipationActivity> GetActivitiesByType(Guid id, ActivityType type)
        {
            return repository.Where(x => x.IsEnabled && x.ParticipationId == id && x.ActivityType == type).Include(x => x.ParticipationDocuments).AsNoTracking();
        }

        public Result<ParticipationActivity> Remove(Guid id)
        {
            var result = new Result<ParticipationActivity>(false);
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

        public Result<ParticipationActivity> CreateActivity(Guid id, ParticipationActivity model)
        {
            var result = new Result<ParticipationActivity>();
            try
            {
                model.ParticipationId = id;
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

        public Result<ParticipationActivity> UpdateActivity(Guid id, ParticipationActivity model)
        {
            var result = new Result<ParticipationActivity>(false);
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

                result.Success = _db.SaveChanges() > 0;
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

        public IQueryable<ParticipationDocument> GetDocument(Guid id)
        {
            return GetActivity(id).ParticipationDocuments.Where(x => x.IsEnabled).AsQueryable().AsNoTracking();
        }

        public ParticipationActivityVm GetActivityById(Guid id)
        {
            var act =
                repository.Where(x => x.ActivityId == id && x.IsEnabled).AsNoTracking().Select(x => new ParticipationActivityVm()
                {
                    ActivityId = x.ActivityId,
                    Subject = x.Subject,
                    Description = x.Description,
                    HoldDate = x.HoldDate,

                    Location = x.Location,
                    ActivityType = x.ActivityType,
                    Attachments = x.ParticipationDocuments.Where(d => d.IsEnabled).Select(d => new IdName()
                    {
                        Id = d.DocumentId.ToString(),
                        Name = d.FileName
                    }).ToList()
                }).FirstOrDefault();
            return act;
        }
    }
}