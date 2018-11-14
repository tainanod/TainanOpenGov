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
    public class ParticipationDocumentService : IParticipationDocumentService
    {
        private readonly IRepository<ParticipationDocument> repository;
        private readonly IRepository<Participation> participationRepository;
        private readonly IRepository<ParticipationActivity> activityRepository;

        public ParticipationDocumentService(IRepository<ParticipationDocument> repository, 
            IRepository<Participation> participationRepository, IRepository<ParticipationActivity> activityRepository)
        {
            this.repository = repository;
            this.participationRepository = participationRepository;
            this.activityRepository = activityRepository;
        }

        /// <summary>
        /// 更新 ParticipationDocument
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uploadDoc"></param>
        /// <returns></returns>
        public Result<ParticipationDocument> Upload(Guid id, UploadDoc uploadDoc)
        {
            var result = new Result<ParticipationDocument>(false);
            var data = participationRepository.Get(x => x.IsEnabled && x.ParticipationId == id);
            if (data == null)
            {
                result.Message = "市政參與議題不存在!";
                return result;
            }
            try
            {
                var fileName = uploadDoc.PostedFile.FileName;
                
                var doc = new ParticipationDocument
                {
                    Size = uploadDoc.PostedFile.ContentLength,
                    FileName = string.IsNullOrEmpty(uploadDoc.Name) ? uploadDoc.PostedFile.FileName : uploadDoc.Name,
                    FileType = ContentTypeExt.GetContentTypeFromDictionary(uploadDoc.PostedFile.FileName),
                    IsEnabled = true,
                    Alt = uploadDoc.Alt,
                    DocumentType = uploadDoc.DocType,
                    ParticipationDocumentExt = new ParticipationDocumentExt()
                };

                using (var ms = new MemoryStream())
                {
                    uploadDoc.PostedFile.InputStream.CopyTo(ms);
                    doc.ParticipationDocumentExt.Original = ms.ToArray();
                }
                data.ParticipationDocuments.Add(doc);

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
            }
            return result;
        }

        /// <summary>
        /// 移除 文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<ParticipationDocument> Remove(Guid id)
        {
            var result = new Result<ParticipationDocument>(false);
            try
            {
                var doc = repository.Get(x => x.DocumentId == id && x.IsEnabled);
                if (doc == null)
                {
                    result.Message = "檔案不存在!";
                    return result;
                }
                foreach (var forum in doc.Participations)
                {
                    forum.ParticipationDocuments.ToList().Remove(doc);
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
        public Result<ParticipationDocument> UploadForActivity(Guid id, UploadDoc uploadDoc)
        {
            var result = new Result<ParticipationDocument>(false);
            var activity = activityRepository.Get(x => x.IsEnabled && x.ActivityId == id);
            if (activity == null)
            {
                result.Message = "資料不存在!";
                return result;
            }
            try
            {
                var fileName = uploadDoc.PostedFile.FileName;
                
                var doc = new ParticipationDocument
                {
                    Size = uploadDoc.PostedFile.ContentLength,
                    FileName = string.IsNullOrEmpty(uploadDoc.Name) ? uploadDoc.PostedFile.FileName : uploadDoc.Name,
                    FileType = ContentTypeExt.GetContentTypeFromDictionary(uploadDoc.PostedFile.FileName),
                    IsEnabled = true,
                    Alt = uploadDoc.Alt,
                    DocumentType = uploadDoc.DocType,
                    ParticipationDocumentExt = new ParticipationDocumentExt()
                };

                using (var ms = new MemoryStream())
                {
                    uploadDoc.PostedFile.InputStream.CopyTo(ms);
                    doc.ParticipationDocumentExt.Original = ms.ToArray();
                }

                activity.ParticipationDocuments.Add(doc);

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