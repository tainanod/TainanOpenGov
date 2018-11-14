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
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;

namespace Geo.Grid.Tainan.OpenGov.Service.Admin
{
    /// <summary>
    /// 標籤管理
    /// </summary>
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tag;
        private readonly OpenGovContext _db;

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public TagService(IRepository<Tag> tag)
        {
            this._tag = tag;
            this._db = tag.Db;
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
        public IQueryable<TagVm> GetList(SearchVm key)
        {
            var data = _db.Tag.OrderBy(x => x.Sort).Where(x => x.IsEnabled && x.ForumId == key.Id).Select(x => new TagVm()
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
        public Result<string> GetCreate(TagVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                int? sort = _db.Tag.OrderByDescending(x => x.Sort).FirstOrDefault(x => x.ForumId == vmD.ForumId)?.Sort;

                var data = new Tag();
                data.TagId = Guid.NewGuid();
                data.TagName = vmD.TagName;
                data.ForumId = vmD.ForumId;
                data.Sort = (sort.HasValue ? sort.Value + 1 : 1);
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
        public TagVm GetDetail(Guid id)
        {
            var data = _db.Tag.Where(x => x.TagId == id).Select(x => new TagVm()
            {
                TagId = x.TagId,
                TagName = x.TagName,
                ForumId = x.ForumId,
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
        public Result<string> GetEdit(TagVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.Tag.Find(vmD.TagId);
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
                var data = _db.Tag.Find(id);
                if (data != null)
                {
                    result.ID = data.ForumId;

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
                var from = _db.Tag.Find(id);
                if (from != null)
                {
                    var query = _db.Tag.Where(x => x.Sort < from.Sort);

                    var to = query.OrderByDescending(x => x.Sort).FirstOrDefault();
                    if (to != null)
                    {
                        var tmp = from.Sort;
                        from.Sort = to.Sort;
                        to.Sort = tmp;
                        _db.SaveChanges();
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
        /// 排序降
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        public Result<string> GetSortDown(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var from = _db.Tag.Find(id);
                if (from != null)
                {
                    var query = _db.Tag.Where(x => x.Sort > from.Sort);
                    var to = query.OrderBy(x => x.Sort).FirstOrDefault();
                    if (to != null)
                    {
                        var tmp = from.Sort;
                        from.Sort = to.Sort;
                        to.Sort = tmp;
                        _db.SaveChanges();
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
    }
}
