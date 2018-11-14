using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Service.Common;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class HyperlinkService : IHyperlinkService
    {
        private readonly IRepository<Hyperlink> repository;
        private readonly OpenGovContext _db = new OpenGovContext();
        private readonly string _anonymousId = new IdentityProfile().GetIdentityProfileAnonymousId();

        public HyperlinkService(IRepository<Hyperlink> repository)
        {
            this.repository = repository;
        }

        public Result<Hyperlink> Remove(Guid id)
        {
            var result = new Result<Hyperlink>(false);
            try
            {
                var act = repository.Get(x => x.HyperlinkId == id && x.IsEnabled);
                if (act == null)
                {
                    result.Message = "相關連結不存在!";
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

        #region 前台

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public Hyperlink GetDetail(Guid id)
        {
            var data = _db.Hyperlink.FirstOrDefault(x => x.HyperlinkId == id);
            return data;
        }

        /// <summary>
        /// 記錄是否有點擊過
        /// </summary>
        /// <param name="id">guid</param>
        /// <returns></returns>
        public Result<string> GetHyperlinkSave(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var linkData = GetDetail(id);
                if (linkData != null)
                {
                    var vmD = _db.AnonymousClick.FirstOrDefault(x => x.AnonymousId == _anonymousId && x.ClickId == id.ToString() && x.ClickType == "A");
                    if (vmD == null)
                    {
                        _db.AnonymousClick.Add(new AnonymousClick()
                        {
                            AnonymousId = _anonymousId,
                            ClickId = id.ToString(),
                            ClickType = "A",
                            ForumId = linkData.Forum.FirstOrDefault().ForumId
                        });
                        _db.SaveChanges();
                        result.Success = true;
                        result.ID = id;
                    }
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