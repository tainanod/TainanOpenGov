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
    public class DiscussService : IDiscussService
    {
        private readonly IRepository<Discuss> _discuss;
        private readonly OpenGovContext _db;

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public DiscussService(IRepository<Discuss> discuss)
        {
            this._discuss = discuss;
            this._db = discuss.Db;
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
        /// <param name="key">id:公民論壇編號</param>
        /// <returns></returns>
        public IQueryable<DiscussVm> GetList(SearchVm key)
        {
            var data = _db.Discuss.OrderByDescending(x => x.IsTop).ThenByDescending(x => x.CreatedDate)
                .Where(x => x.ForumId == key.Id && !x.UpperId.HasValue).Select(x => new DiscussVm()
                {
                    DiscussId = x.DiscussId,
                    ForumId = x.ForumId,
                    UserId = x.UserId,
                    UserNickName = x.AspNetUsers.NickName,
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
                var data = _db.Discuss.Find(id);
                if (data != null)
                {
                    data.IsTop = (data.IsTop ? false : true);                    
                    result.Success = _discuss.SaveChanges() > 0;
                    result.ID = data.ForumId;
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
