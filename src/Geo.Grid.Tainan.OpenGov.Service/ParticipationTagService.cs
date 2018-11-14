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
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    /// <summary>
    /// 標籤管理
    /// </summary>
    public class ParticipationTagService : IParticipationTagService
    {
        private readonly IRepository<ParticipationTag> _tag;
        private readonly OpenGovContext _db;

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public ParticipationTagService(IRepository<ParticipationTag> tag)
        {
            this._tag = tag;
            this._db = tag.Db;
        }

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
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public IQueryable<ParticipationTagVm> GetList(SearchVm key)
        {
            var data = _db.ParticipationTags.OrderBy(x => x.Sort).Where(x => x.IsEnabled && x.ParticipationId == key.Id).Select(x => new ParticipationTagVm()
            {
                TagId = x.TagId,
                TagName = x.TagName
            });
            return data;
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetCreate(ParticipationTagVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                int? sort = _db.ParticipationTags.OrderByDescending(x => x.Sort).FirstOrDefault(x => x.ParticipationId == vmD.ParticipationId)?.Sort;

                var data = new ParticipationTag();
                data.TagId = Guid.NewGuid();
                data.TagName = vmD.TagName;
                data.ParticipationId = vmD.ParticipationId;
                data.Sort = (sort.HasValue ? sort.Value + 1 : 1 );
                data.IsEnabled = true;

                result.Success = _tag.Create(data) > 0;
                result.ID = data.TagId;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        public ParticipationTagVm GetDetail(Guid id)
        {
            var data = _db.ParticipationTags.Where(x => x.TagId == id).Select(x => new ParticipationTagVm()
            {
                TagId = x.TagId,
                TagName = x.TagName,
                ParticipationId = x.ParticipationId,
                Sort = x.Sort,
                IsEnabled = x.IsEnabled
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetEdit(ParticipationTagVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.ParticipationTags.Find(vmD.TagId);
                if (data != null)
                {
                    data.TagName = vmD.TagName;

                    result.Success = _tag.Update(data) > 0;
                    result.ID = data.TagId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 刪除-回傳forumId
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        public Result<string> GetRemove(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.ParticipationTags.Find(id);
                if (data != null)
                {
                    result.ID = data.ParticipationId;

                    data.IsEnabled = false;
                    result.Success = _tag.Update(data) > 0;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 排序-升
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        public Result<string> GetSortUp(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var tag = _db.ParticipationTags.AsNoTracking().Single(x => x.TagId == id);
                var tags = _db.ParticipationTags.Where(x => x.ParticipationId == tag.ParticipationId).OrderBy(x => x.Sort).ToList();

                if (tags.Count() == 1)
                {
                    return result;
                }

                var targetTag = tags.Single(x => x.TagId == tag.TagId);
                var ind = tags.IndexOf(targetTag);
                if (ind == 0)
                {
                    return result;
                }
                var tempSort = tags[ind - 1].Sort;
                tags[ind - 1].Sort = tags[ind].Sort;
                tags[ind].Sort = tempSort;

                _db.SaveChanges();
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 排序降
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        public Result<string> GetSortDown(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var tag = _db.ParticipationTags.AsNoTracking().Single(x => x.TagId == id);
                var tags = _db.ParticipationTags.Where(x => x.ParticipationId == tag.ParticipationId).OrderBy(x => x.Sort).ToList();

                if (tags.Count() == 1)
                {
                    return result;
                }

                var targetTag = tags.Single(x => x.TagId == tag.TagId);
                var ind = tags.IndexOf(targetTag);
                if ((ind+1) == tags.Count())
                {
                    return result;
                }
                var tempSort = tags[ind + 1].Sort;
                tags[ind + 1].Sort = tags[ind].Sort;
                tags[ind].Sort = tempSort;

                _db.SaveChanges();
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
    }
}
