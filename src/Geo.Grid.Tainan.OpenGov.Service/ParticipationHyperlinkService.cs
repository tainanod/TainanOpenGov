using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Service.Common;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ParticipationHyperlinkService : IParticipationHyperlinkService
    {
        private readonly IRepository<ParticipationHyperlink> repository;
        private readonly OpenGovContext _db = new OpenGovContext();
        private readonly string _anonymousId = new IdentityProfile().GetIdentityProfileAnonymousId();

        public ParticipationHyperlinkService(IRepository<ParticipationHyperlink> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 刪除 連結
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<ParticipationHyperlink> Remove(Guid id)
        {
            var result = new Result<ParticipationHyperlink>(false);
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
        public ParticipationHyperlink GetDetail(Guid id)
        {
            var data = repository.Where(x => x.HyperlinkId == id && x.IsEnabled).Include(x=>x.Participations).FirstOrDefault();
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
                    var vmD = _db.ParticipationAnonymousClicks.FirstOrDefault(x => x.AnonymousId == _anonymousId && x.ClickId == id.ToString() && x.ClickType == "A");
                    if (vmD == null)
                    {
                        _db.ParticipationAnonymousClicks.Add(new ParticipationAnonymousClick()
                        {
                            AnonymousId = _anonymousId,
                            ClickId = id.ToString(),
                            ClickType = "A",
                            ParticipationId = linkData.Participations.FirstOrDefault().ParticipationId,
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