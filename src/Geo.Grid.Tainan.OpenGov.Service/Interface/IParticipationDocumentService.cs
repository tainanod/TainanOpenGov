using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IParticipationDocumentService
    {
        /// <summary>
        /// 更新 ParticipationDocument
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uploadDoc"></param>
        /// <returns></returns>
        Result<ParticipationDocument> Upload(Guid id, UploadDoc uploadDoc);

        /// <summary>
        /// 相關活動文件上傳
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uploadDoc"></param>
        /// <returns></returns>
        Result<ParticipationDocument> UploadForActivity(Guid id, UploadDoc uploadDoc);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<ParticipationDocument> Remove(Guid id);
    }
}