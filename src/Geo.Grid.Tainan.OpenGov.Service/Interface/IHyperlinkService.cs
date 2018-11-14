using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IHyperlinkService
    {
        Result<Hyperlink> Remove(Guid id);

        #region web

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        Hyperlink GetDetail(Guid id);

        /// <summary>
        /// 記錄是否有點擊過
        /// </summary>
        /// <param name="id">guid</param>
        /// <returns></returns>
        Result<string> GetHyperlinkSave(Guid id);

        #endregion
    }
}