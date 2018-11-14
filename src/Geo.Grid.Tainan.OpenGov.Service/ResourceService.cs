using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ResourceService : IResourceService
    {
        private readonly IRepository<Photo> photoRepository;
        private readonly IRepository<Document> documentRepository;
        private readonly OpenGovContext _db = new OpenGovContext();
        private readonly string _anonymousId = new IdentityProfile().GetIdentityProfileAnonymousId();

        public ResourceService(IRepository<Photo> photoRepository, IRepository<Document> documentRepository)
        {
            this.photoRepository = photoRepository;
            this.documentRepository = documentRepository;
        }

        public byte[] GetPhotoWithSize(Guid id, PhotoSize size)
        {
            //Oh...reflection
            //http://stackoverflow.com/questions/18707471/selecting-distinct-entity-values-based-on-string-field-name
            var parameter = Expression.Parameter(typeof(PhotoExt));
            var body = Expression.Property(parameter, size.ToString());
            var lambda = Expression.Lambda<Func<PhotoExt, byte[]>>(body, parameter);
            var result = photoRepository.Where(x => x.PhotoId == id && x.IsEnabled).Select(x => x.PhotoExt).Select(lambda).FirstOrDefault();
            return result;
        }

        #region 公民論壇

        public Document GetDocument(Guid id)
        {
            return documentRepository.Get(x => x.DocumentId == id && x.IsEnabled);
        }
        
        /// <summary>
        /// 記錄點擊-不可重覆
        /// </summary>
        /// <param name="id">documentId</param>
        /// <returns></returns>
        public Result<string> GetDocumentClickSave(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var documentData = GetDocument(id);
                if (documentData != null)
                {
                    string typeName = documentData.DocumentType == 0 ? "B" : "C";
                    var data = _db.AnonymousClick.FirstOrDefault(x => x.AnonymousId == _anonymousId && x.ClickId == id.ToString() && x.ClickType == typeName);
                    if (data == null)
                    {
                        _db.AnonymousClick.Add(new AnonymousClick()
                        {
                            AnonymousId = _anonymousId,
                            ClickId = id.ToString(),
                            ClickType = typeName,
                            ForumId = documentData.Forum.FirstOrDefault().ForumId
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


        #region 市政參與

        /// <summary>
        /// 取得 市政參與 附件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ParticipationDocument GetParticipationDocument(Guid id)
        {
            return _db.ParticipationDocuments.Include(x => x.ParticipationDocumentExt)
                .Include(x => x.Participations)
                .FirstOrDefault(x => x.DocumentId == id && x.IsEnabled);
        }
        
        /// <summary>
        /// 記錄點擊-不可重覆
        /// </summary>
        /// <param name="id">documentId</param>
        /// <returns></returns>
        public Result<string> GetParticipationDocumentClickSave(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var documentData = GetParticipationDocument(id);
                if (documentData != null)
                {
                    string typeName = documentData.DocumentType == 0 ? "B" : "C";
                    var data = _db.ParticipationAnonymousClicks.FirstOrDefault(x => x.AnonymousId == _anonymousId && x.ClickId == id.ToString() && x.ClickType == typeName);
                    if (data == null)
                    {
                        _db.ParticipationAnonymousClicks.Add(new ParticipationAnonymousClick()
                        {
                            AnonymousId = _anonymousId,
                            ClickId = id.ToString(),
                            ClickType = typeName,
                            ParticipationId = documentData.Participations.FirstOrDefault().ParticipationId,
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