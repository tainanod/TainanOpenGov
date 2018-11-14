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
    public interface IPhotoService
    {
        /// <summary>
        /// 圖片上傳
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        Result<Photo> PhotoUpload(HttpPostedFileBase postedFile, string alt);

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postedFile"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        Result<Photo> PhotoUpload(Guid id, HttpPostedFileBase postedFile, string alt);

        /// <summary>
        /// 上傳
        /// </summary>
        /// <param name="file">檔案</param>
        /// <param name="alt">alt(controllerName)</param>
        /// <returns></returns>
        Result<Photo> GetCreate(HttpPostedFileBase file, string alt);

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">guid</param>
        /// <returns></returns>
        PhotoNewVm GetDetail(Guid id);
    }
}