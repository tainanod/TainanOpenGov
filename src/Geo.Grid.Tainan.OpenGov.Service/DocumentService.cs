using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> repository;
        private readonly IRepository<Forum> forumRepository;
        private readonly IRepository<Activity> activityRepository;

        public DocumentService(IRepository<Document> repository, IRepository<Forum> forumRepository, IRepository<Activity> activityRepository)
        {
            this.repository = repository;
            this.forumRepository = forumRepository;
            this.activityRepository = activityRepository;
        }

        public Result<Document> Upload(Guid id, UploadDoc uploadDoc)
        {
            var result = new Result<Document>(false);
            var forum = forumRepository.Get(x => x.IsEnabled && x.ForumId == id);
            if (forum == null)
            {
                result.Message = "公民論壇不存在!";
                return result;
            }
            try
            {
                var fileName = uploadDoc.PostedFile.FileName;
                
                var doc = new Document
                {
                    Size = uploadDoc.PostedFile.ContentLength,
                    FileName = string.IsNullOrEmpty(uploadDoc.Name) ? uploadDoc.PostedFile.FileName : uploadDoc.Name,
                    FileType = ContentTypeExt.GetContentTypeFromDictionary(uploadDoc.PostedFile.FileName),
                    IsEnabled = true,
                    Alt = uploadDoc.Alt,
                    DocumentType = uploadDoc.DocType,
                    DocumentExt = new DocumentExt()
                };

                using (var ms = new MemoryStream())
                {
                    uploadDoc.PostedFile.InputStream.CopyTo(ms);
                    doc.DocumentExt.Original = ms.ToArray();
                }
                //doc.Forum.Add(forum);
                forum.Document.Add(doc);

                result.Success = repository.Create(doc) > 0;
                result.Data = doc;
                
            }
            catch (DbEntityValidationException es)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in es.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                result.Message = sb.ToString();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                //result.Exception = ex;
            }

            return result;
        }

        public Result<Document> Remove(Guid id)
        {
            var result = new Result<Document>(false);
            try
            {
                var doc = repository.Get(x => x.DocumentId == id && x.IsEnabled);
                if (doc == null)
                {
                    result.Message = "檔案不存在!";
                    return result;
                }
                foreach (var forum in doc.Forum)
                {
                    forum.Document.ToList().Remove(doc);
                }
                doc.IsEnabled = false;
                result.Success = repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 相關活動文件上傳
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uploadDoc"></param>
        /// <returns></returns>
        public Result<Document> UploadForActivity(Guid id, UploadDoc uploadDoc)
        {
            var result = new Result<Document>(false);
            var activity = activityRepository.Get(x => x.IsEnabled && x.ActivityId == id);
            if (activity == null)
            {
                result.Message = "資料不存在!";
                return result;
            }
            try
            {
                var fileName = uploadDoc.PostedFile.FileName;
                
                var doc = new Document
                {
                    Size = uploadDoc.PostedFile.ContentLength,
                    FileName = string.IsNullOrEmpty(uploadDoc.Name) ? uploadDoc.PostedFile.FileName : uploadDoc.Name,
                    FileType = ContentTypeExt.GetContentTypeFromDictionary(uploadDoc.PostedFile.FileName),
                    IsEnabled = true,
                    Alt = uploadDoc.Alt,
                    DocumentType = uploadDoc.DocType,
                    DocumentExt = new DocumentExt()
                };

                using (var ms = new MemoryStream())
                {
                    uploadDoc.PostedFile.InputStream.CopyTo(ms);
                    doc.DocumentExt.Original = ms.ToArray();
                }

                activity.Document.Add(doc);

                result.Success = repository.Create(doc) > 0;
                result.Data = doc;
                
            }
            catch (DbEntityValidationException es)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in es.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                result.Message = sb.ToString();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                //result.Exception = ex;
            }

            return result; ;
        }

        private bool CheckFileType(string ext)
        {
            var list = new string[] { ".pdf", ".zip", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
            return list.Contains(ext);
        }
    }
}