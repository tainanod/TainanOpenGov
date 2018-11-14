using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoService _service;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="service">service</param>
        public PhotoController(IPhotoService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 上傳
        /// </summary>
        /// <param name="file">檔案</param>
        /// <param name="controllerName">alt(controllerName)</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file, string controllerName)
        {
            var data = _service.GetCreate(file, controllerName);
            return Json(data);
            //return File(data.Data.PhotoExt.Original, data.Data.FileType, data.Data.FileName);
        }

        /// <summary>
        /// TinyMce PhotoUpload
        /// </summary>
        /// <param name="file"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadTinyMce(HttpPostedFileBase file, string alt)
        {
            var model = _service.GetCreate(file, alt);

            return Json(new { location = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + Url.Action("Photo", "Resource", new { id = model.ID ,size=2})});
        }

        /// <summary>
        /// 上傳
        /// </summary>
        /// <param name="alt">alt(controllerName)</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MdUpload()
        {
            var file = Request.Files["editormd-image-file"] as HttpPostedFileBase;
            var data = _service.GetCreate(file, "md");
            var jsonData = new MdUploadVm();
            jsonData.success = data.Success ? 1 : 0;
            jsonData.message = data.Message;
            jsonData.url = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + Url.Action("Detail", "Photo", new { id = data.ID });
            return Json(jsonData);
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">guid</param>
        /// <returns></returns>
        public FileResult Detail(Guid id)
        {
            var data = _service.GetDetail(id);
            return File(data.FileStream, data.ContentType);
            //return File(data.FileStream, data.ContentType, data.FileName);
        }
    }
}