using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Geo.Grid.Tainan.OpenGov.Dal;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly IRepository<Photo> repository;
        private readonly IRepository<Forum> forumRepository;
        private readonly OpenGovContext _db;

        public PhotoService(IRepository<Photo> repository, IRepository<Forum> forumRepository)
        {
            this.repository = repository;
            this.forumRepository = forumRepository;
            this._db = repository.Db;
        }

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public Result<Photo> PhotoUpload(HttpPostedFileBase postedFile, string alt)
        {
            var result = new Result<Photo>(false);
            try
            {
                if (!postedFile.FileName.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Message = "圖片僅能上傳JPG格式!";
                    return result;
                }

                var photo = new Photo()
                {
                    Size = postedFile.ContentLength,
                    FileName = postedFile.FileName,
                    FileType = "image/jpeg",
                    IsEnabled = true,
                    Alt = alt
                };
                photo.PhotoExt = new PhotoExt();
                using (MemoryStream ms = new MemoryStream())
                {
                    postedFile.InputStream.CopyTo(ms);
                    photo.PhotoExt.Original = ms.ToArray();
                    Image graph = Image.FromStream(ms);
                    //轉成寬120PX
                    photo.PhotoExt.Width120 = ResizeImage(graph, 120);
                    //轉成寬320PX
                    photo.PhotoExt.Width320 = ResizeImage(graph, 320);
                    //轉成寬768PX
                    photo.PhotoExt.Width768 = ResizeImage(graph, 768);
                    //轉成寬1024PX
                    photo.PhotoExt.Width1024 = ResizeImage(graph, 1024);
                }

                result.Success = (repository.Create(photo) > 1);
                if (!result.Success)
                {
                    result.Message = "圖片上傳失敗";
                }
                result.Data = photo;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<Photo> PhotoUpload(Guid id, HttpPostedFileBase postedFile, string alt)
        {
            var result = new Result<Photo>(false);
            try
            {
                if (!postedFile.FileName.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Message = "圖片僅能上傳JPG格式!";
                    return result;
                }

                var photo = new Photo()
                {
                    Size = postedFile.ContentLength,
                    FileName = postedFile.FileName,
                    FileType = "image/jpeg",
                    IsEnabled = true,
                    Alt = alt
                };
                photo.PhotoExt = new PhotoExt();
                using (MemoryStream ms = new MemoryStream())
                {
                    postedFile.InputStream.CopyTo(ms);
                    photo.PhotoExt.Original = ms.ToArray();
                    Image graph = Image.FromStream(ms);
                    //轉成寬120PX
                    photo.PhotoExt.Width120 = ResizeImage(graph, 120);
                    //轉成寬320PX
                    photo.PhotoExt.Width320 = ResizeImage(graph, 320);
                    //轉成寬768PX
                    photo.PhotoExt.Width768 = ResizeImage(graph, 768);
                    //轉成寬1024PX
                    photo.PhotoExt.Width1024 = ResizeImage(graph, 1024);
                }
                var forum = forumRepository.Get(x => x.IsEnabled && x.ForumId == id);
                foreach (var p in forum.Photo.ToList())
                {
                    forum.Photo.Remove(p);
                }
                forum.Photo.Add(photo);

                result.Success = (repository.Create(photo) > 1);
                if (!result.Success)
                {
                    result.Message = "圖片上傳失敗";
                }
                result.Data = photo;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Exception = e;
            }
            return result;
        }

        private byte[] ResizeImage(Image graph, int convertSize)
        {
            int resizeHeigh = (convertSize * (graph.Height)) / (graph.Width);
            Bitmap imageOut = new Bitmap(graph, convertSize, resizeHeigh);
            using (MemoryStream ms = new MemoryStream())
            {
                imageOut.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Close();
                return ms.ToArray();
            }
        }

        #region 共用

        /// <summary>
        /// 上傳
        /// </summary>
        /// <param name="file">檔案</param>
        /// <param name="alt">alt(controllerName)</param>
        /// <returns></returns>
        public Result<Photo> GetCreate(HttpPostedFileBase file, string alt)
        {
            Result<Photo> result = new Result<Photo>(false);
            try
            {
                if (!file.FileName.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) 
                    && !file.FileName.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Message = "圖片僅能上傳JPG 或 PNG格式!";
                    return result;
                }

               

                var data = new Photo();
                data.Size = file.ContentLength;
                data.FileName = file.FileName;
                data.FileType = file.ContentType;
                data.IsEnabled = true;
                data.Alt = alt;

                data.PhotoExt = new PhotoExt();
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    data.PhotoExt.Original = ms.ToArray();
                    Image graph = Image.FromStream(ms);
                    data.PhotoExt.Width120 = ResizeImage(graph, 120);
                    data.PhotoExt.Width320 = ResizeImage(graph, 320);
                    data.PhotoExt.Width768 = ResizeImage(graph, 768);
                    data.PhotoExt.Width1024 = ResizeImage(graph, 1024);
                }

                result.Success = repository.Create(data) > 0;
                result.ID = data.PhotoId;
                if (!result.Success)
                {
                    result.Message = "圖片上傳失敗";
                }
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
        /// <param name="id">guid</param>
        /// <returns></returns>
        public PhotoNewVm GetDetail(Guid id)
        {
            var data = _db.Photo.Where(x => x.PhotoId == id).Select(x => new PhotoNewVm()
            {
                PhotoGuid = x.PhotoId,
                FileName = x.FileName,
                ContentType = x.FileType,
                FileStream = x.PhotoExt.Original
            });
            return data.FirstOrDefault();
        }

        #endregion
    }
}