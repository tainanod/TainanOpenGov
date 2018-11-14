using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IResourceService
    {
        byte[] GetPhotoWithSize(Guid id, PhotoSize size);

        Document GetDocument(Guid id);

        /// <summary>
        /// 記錄點擊-不可重覆
        /// </summary>
        /// <param name="id">documentId</param>
        /// <returns></returns>
        Result<string> GetDocumentClickSave(Guid id);

        /// <summary>
        /// 取得 市政參與 附件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ParticipationDocument GetParticipationDocument(Guid id);

        /// <summary>
        /// 記錄點擊-不可重覆
        /// </summary>
        /// <param name="id">documentId</param>
        /// <returns></returns>
        Result<string> GetParticipationDocumentClickSave(Guid id);
    }
}